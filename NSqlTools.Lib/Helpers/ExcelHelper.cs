using NSqlTools.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EXC = Microsoft.Office.Interop.Excel;

namespace NSqlTools.Lib.Helpers
{
	public class ExcelHelper
	{
		#region Methods
		public static Boolean ExportDataGridViewToExcel(DataGridView dataGridView, String fileName, String workSheetName)
		{
			Boolean result = false;	
			EXC.Application excelApp = null;
			EXC.Workbook workbook = null;
			EXC.Worksheet worksheet = null;

			try
			{
				excelApp = new EXC.Application { Visible = false };

				workbook = excelApp.Workbooks.Add(Type.Missing);
				worksheet = workbook.Sheets[1];
				worksheet = workbook.ActiveSheet;
				worksheet.Name = workSheetName;

				// Headers + hide invisible columns
				for (int i = 0; i < dataGridView.Columns.Count; i++)
				{
					DataGridViewColumn gridCol = dataGridView.Columns[i];
					worksheet.Cells[1, i + 1] = gridCol.HeaderText;

					if (!gridCol.Visible)
					{
						EXC.Range colRange = worksheet.Columns[i + 1];
						colRange.EntireColumn.Hidden = true;
						System.Runtime.InteropServices.Marshal.ReleaseComObject(colRange);
					}
				}

				// Data
				for (int r = 0; r < dataGridView.Rows.Count; r++)
				{
					for (int c = 0; c < dataGridView.Columns.Count; c++)
					{
						DataGridViewCell cell = dataGridView.Rows[r].Cells[c];
						Object raw = cell.Value;
						Object excelValue = ConvertToExcelExportValue(raw, cell);
						worksheet.Cells[r + 2, c + 1] = excelValue;
					}
				}

				workbook.SaveAs(fileName);
				result = true;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception("Export to Excel failed", ex);
			}
			finally
			{
				if (worksheet != null)
				{
					System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
					worksheet = null;
				}
				if (workbook != null)
				{
					workbook.Close();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
					workbook = null;
				}
				if (excelApp != null)
				{
					excelApp.Quit();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
					excelApp = null;
				}

				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			return result;
		}

		private static Object ConvertToExcelExportValue(Object value, DataGridViewCell cell)
		{
			if (value == null || value == DBNull.Value)
				return null;

			// Bitmap: use Tag text
			if (value is System.Drawing.Bitmap)
			{
				Object tag = cell?.Tag;
				return tag?.ToString();
			}

			// Byte[] -> hex string (Excel shows as text)
			if (value is byte[])
			{
				var bytes = (byte[])value;
				var sb = new StringBuilder(bytes.Length * 2 + 2);
				sb.Append("0x");
				foreach (var t in bytes)
					sb.Append(t.ToString("X2"));

				return sb.ToString();
			}

			// Directly supported types
			if (value is DateTime || value is Boolean)
				return value;

			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return value;

				case TypeCode.Decimal:
					{
						string g = ((decimal)value).ToString("G29", System.Globalization.CultureInfo.InvariantCulture);
						if (Regex.Replace(g, "[^0-9]", "").Length >= 12)
							return "'" + g;
						return value;
					}
				case TypeCode.Double:
					{
						string g = ((double)value).ToString("G17", System.Globalization.CultureInfo.InvariantCulture);
						if (Regex.Replace(g, "[^0-9]", "").Length >= 12)
							return "'" + g;
						return value;
					}
				case TypeCode.Single:
					{
						string g = ((float)value).ToString("G9", System.Globalization.CultureInfo.InvariantCulture);
						if (Regex.Replace(g, "[^0-9]", "").Length >= 12)
							return "'" + g;
						return value;
					}
			}

			// Try to parse strings to number/date/bool
			String s = value.ToString();
			if (s.Length == 0)
				return null;

			var culture = System.Globalization.CultureInfo.InvariantCulture;

			if (Decimal.TryParse(s, System.Globalization.NumberStyles.Number, culture, out var dec))
				return dec;

			if (DateTime.TryParse(s, culture, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out var dt))
				return dt;

			if (Boolean.TryParse(s, out var b))
				return b;

			// Fallback: string
			return s;
		}

		public static DataTable ImportFromExcel(String fileName, List<ColumnContract> columnContractList)
		{
			DataTable result = new DataTable();

			EXC.Application excelApp = null;
			EXC.Workbook workbook = null;
			EXC._Worksheet worksheet = null;
			EXC.Range range = null;

			try
			{
				excelApp = new EXC.Application();
				workbook = excelApp.Workbooks.Open(fileName);
				worksheet = workbook.Sheets[1];
				range = worksheet.UsedRange;

				Int32 colCount = 0;
				for (int col = 1; col <= range.Columns.Count; col++)
				{
					Object val = (range.Cells[1, col] as EXC.Range).Value2;
					if (val == null)
						break;

					colCount++;
					String columnName = val.ToString();
					ColumnContract columnContract = columnContractList?.FirstOrDefault(c => c.Name == columnName);
					Type dataType = GetDataColumnType(columnContract);
					result.Columns.Add(new DataColumn(columnName, dataType));
				}

				for (int row = 2; row <= range.Rows.Count; row++)
				{
					DataRow dataRow = result.NewRow();

					for (int col = 1; col <= colCount; col++)
					{
						String columnName = result.Columns[col - 1].ColumnName;
						ColumnContract columnContract = columnContractList?.FirstOrDefault(c => c.Name == columnName);

						EXC.Range cell = range.Cells[row, col] as EXC.Range;
						Object rawValue = cell != null ? cell.Value2 : null;

						if (rawValue != null)
						{
							String rawStr = rawValue.ToString();
							Object converted = null;
							if (rawStr == "NULL")
							{
								converted = DBNull.Value;
							}
							else
							{
								converted = ConvertExcelValue(rawValue, columnContract);
								if (converted == null || (converted is String && ((String)converted).Length == 0))
									converted = DBNull.Value;
							}

							dataRow[col - 1] = converted ?? DBNull.Value;
						}
						else
						{
							dataRow[col - 1] = DBNull.Value;
						}
					}

					result.Rows.Add(dataRow);
				}
			}
			finally
			{
				if (range != null)
				{
					System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
					range = null;
				}
				if (worksheet != null)
				{
					System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
					worksheet = null;
				}
				if (workbook != null)
				{
					workbook.Close(false);
					System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
					workbook = null;
				}
				if (excelApp != null)
				{
					excelApp.Quit();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
					excelApp = null;
				}

				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			return result;
		}

		private static Type GetDataColumnType(ColumnContract column)
		{
			if (column == null || String.IsNullOrEmpty(column.TypeName))
				return typeof(String);

			switch (column.TypeName.ToLowerInvariant())
			{
				case "int":
				case "integer": return typeof(Int32);
				case "bigint": return typeof(Int64);
				case "smallint": return typeof(Int16);
				case "tinyint": return typeof(Byte);
				case "bit": return typeof(Boolean);
				case "decimal":
				case "numeric":
				case "money":
				case "smallmoney": return typeof(Decimal);
				case "float": return typeof(Double);
				case "real": return typeof(Single);
				case "uniqueidentifier":
				case "guid": return typeof(Guid);
				case "date":
				case "datetime":
				case "datetime2":
				case "smalldatetime": return typeof(DateTime);
				case "time": return typeof(TimeSpan);
				case "binary":
				case "varbinary": return typeof(Byte[]);
				// default (char, nchar, varchar, nvarchar, text, etc.)
				default: return typeof(String);
			}
		}

		private static Object ConvertExcelValue(Object rawValue, ColumnContract column)
		{
			if (rawValue == null)
				return null;

			if (column == null || String.IsNullOrEmpty(column.TypeName))
				return rawValue.ToString();

			String typeName = column.TypeName.ToLowerInvariant();
			String s = rawValue.ToString().Trim();

			if (s.Length == 0)
				return null;

			var culture = System.Globalization.CultureInfo.InvariantCulture;

			try
			{
				switch (typeName)
				{
					case "int":
					case "integer":
						Int32 i32;
						if (Int32.TryParse(s, System.Globalization.NumberStyles.Any, culture, out i32)) return i32;
						break;
					case "bigint":
						Int64 i64;
						if (Int64.TryParse(s, System.Globalization.NumberStyles.Any, culture, out i64)) return i64;
						break;
					case "smallint":
						Int16 i16;
						if (Int16.TryParse(s, System.Globalization.NumberStyles.Any, culture, out i16)) return i16;
						break;
					case "tinyint":
						Byte b;
						if (Byte.TryParse(s, System.Globalization.NumberStyles.Any, culture, out b)) return b;
						break;
					case "bit":
						if (s == "1" || s.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
						if (s == "0" || s.Equals("false", StringComparison.OrdinalIgnoreCase)) return false;
						break;
					case "decimal":
					case "numeric":
					case "money":
					case "smallmoney":
						Decimal dec;
						if (Decimal.TryParse(s, System.Globalization.NumberStyles.Any, culture, out dec)) return dec;
						break;
					case "float":
						Double dbl;
						if (Double.TryParse(s, System.Globalization.NumberStyles.Any, culture, out dbl)) return dbl;
						break;
					case "real":
						Single fl;
						if (Single.TryParse(s, System.Globalization.NumberStyles.Any, culture, out fl)) return fl;
						break;
					case "uniqueidentifier":
					case "guid":
						Guid g;
						if (Guid.TryParse(s, out g)) return g;
						break;
					case "date":
					case "datetime":
					case "datetime2":
					case "smalldatetime":
						// Excel'den gelen seri tarih (ör: 44153.8945833333) OLE Automation Date olarak dönüştürülür
						if (rawValue is double dRaw)
						{
							if (dRaw >= -657434 && dRaw <= 2958465) return DateTime.FromOADate(dRaw);
						}
						else
						{
							Double oa;
							if (Double.TryParse(s, System.Globalization.NumberStyles.Any, culture, out oa)
							&& oa >= -657434 && oa <= 2958465)
								return DateTime.FromOADate(oa);
						}

						break;
					case "time":
						TimeSpan ts;
						if (TimeSpan.TryParse(s, out ts)) return ts;
						break;
					case "binary":
					case "varbinary":
						if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
						{
							try
							{
								String hex = s.Substring(2);
								if (hex.Length % 2 == 0)
								{
									byte[] bytes = new byte[hex.Length / 2];
									for (int idx = 0; idx < bytes.Length; idx++)
										bytes[idx] = Convert.ToByte(hex.Substring(idx * 2, 2), 16);
									return bytes;
								}
							}
							catch { }
						}
						break;
					default:
						break;
				}
			}
			catch
			{
			}

			return s;
		}
		#endregion
	}
}
