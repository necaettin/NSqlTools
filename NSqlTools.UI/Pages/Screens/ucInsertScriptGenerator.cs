using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;

namespace NSqlTools.UI.Pages
{
	public partial class ucInsertScriptGenerator : BaseUserControl
	{
		#region Constructor
		public ucInsertScriptGenerator()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Properties
		public Boolean UseNoSquareBrackets
		{
			get
			{
				return tsbWithSquareBrackets.CheckState != CheckState.Checked;
			}
		}

		public Boolean SeperateInsertScripts
		{
			get
			{
				return tsbSeperate.CheckState == CheckState.Checked;
			}
		}

		public List<ColumnContract> ColumnsDataSource 
		{
			get 
			{
				return ((BindingList<ColumnContract>)(((BindingSource)dgvColumns.DataSource).DataSource)).ToList();
			}
			set 
			{
				dgvColumns.BindList(value);

				lblColumns.Text = null;

				tsbWriteSourceSqlScript.Enabled = value != null;
				tsbRunScript.Enabled = value != null;
				
				InputSqlScript = null;
				
				OutputSqlScript = null;
				lblOutputSqlScript.Text = null;

				ScriptResultDataSource = null;
			}
		}

		public DataTable ScriptResultDataSource 
		{
			get 
			{
				return dgvScriptResult.GetBoundDataTable();
			}
			set 
			{
				dgvScriptResult.BindDataTable(value);
				if(value != null)
					value.TableName = "Default Table";
				lblScriptResult.Text = null;
				
				tsbExportToExcel.Enabled = value != null;
				tsbImportFromExcel.Enabled = value != null;
				tsbCreateInsertScripts.Enabled = value != null;
			}
		}

		public String InputSqlScript
		{
			get
			{
				return _ucSqlNotePad.scSqlQuery.Text;
			}
			set
			{
				_ucSqlNotePad.scSqlQuery.Text = value;
				tsbRunScript.Enabled = !String.IsNullOrWhiteSpace(value);
				ScriptResultDataSource = null;
				OutputSqlScript = null;
			}
		}


		public String OutputSqlScript
		{
			get
			{
				return scOutputSqlScript.Text;
			}
			set
			{
				scOutputSqlScript.Text = value;
			}
		}

		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					ucDBObjectSelectControl,
					dgvColumns,
					_ucSqlNotePad.scSqlQuery,
					dgvScriptResult,
					scOutputSqlScript               
				};
			}
		}
		#endregion

		#region Events
		private void ucDBObjectSelect_OnDBObjectChanged(object sender, DBObjectChangedEventArgs e)
		{
			ColumnsDataSource = e.DBObjectContract != null ? e.DBObjectContract.ColumnList : null;
			lblColumns.Text = e.DBObjectContract == null ? null : String.Format(CommonResource.ColumnInfo, e.DBObjectContract.SchemaName, e.DBObjectContract.Name, e.DBObjectContract.ColumnList.Count);
			ParentTabPage.Text = CommonResource.InsertSQL + $"- {ucDBObjectSelectControl.SelectedSchema.Name}.{ucDBObjectSelectControl.SelectedDBObject.Name}";

			tsbWriteSourceSqlScript_Click(tsbWriteSourceSqlScript, EventArgs.Empty);
		}

		private void ucDBObjectSelect_OnDBObjectClear(object sender, EventArgs e)
		{
			ColumnsDataSource = null;
		}

        private void tsbWriteSourceSqlScript_Click(object sender, EventArgs e)
		{
			if(ColumnsDataSource == null)
			{
				InputSqlScript = null;
				
				return;
			}

			#region Create Insert Script
			String columnsString = getTableColumnsAsString(ColumnsDataSource);
			String inputSqlScript = UseNoSquareBrackets ? "SELECT TOP 0 {0} FROM [{1}].[{2}].[{3}]" : "SELECT TOP 0 {0} FROM {1}.{2}.{3}";
			InputSqlScript = String.Format(inputSqlScript,
				columnsString,
				ucDBObjectSelectControl.SelectedDB.Name,
				ucDBObjectSelectControl.SelectedSchema.Name,
				ucDBObjectSelectControl.SelectedDBObject.Name);
			#endregion

			#region Fill script result grid with empty rows
			runScript();
			#endregion
		}

		private void tsbRunScript_Click(object sender, EventArgs e)
		{
			runScript();
		}

		private void tsbCreateInsertScripts_Click(object sender, EventArgs e)
		{
			#region Validation
			if (ucDBObjectSelectControl.SelectedDBObject == null || String.IsNullOrWhiteSpace(InputSqlScript))
			{
				MessageBox.Show(CommonResource.SelectATableAndEnterSourceSqlScript, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}
			#endregion

			#region Create Insert Scripts
			DataTable dataTable = ScriptResultDataSource;

			// Sadece aktif satırları al (Deleted olmayan)
			DataRow[] activeRows = dataTable.Select("", "", DataViewRowState.CurrentRows);

			List<ColumnContract> columnContracts = ucDBObjectSelectControl.SelectedDBObject.ColumnList;
			StringBuilder sbScript = new StringBuilder();
			try
			{
				#region Validations
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					DataColumn column = dataTable.Columns[j];
					ColumnContract columnContract = columnContracts.First(c => c.Name == column.ColumnName);

					if (columnContract.SystemTypeId != null)
					{
						SqlColumnDataTypeEnum aqlColumnDataTypeEnum = (SqlColumnDataTypeEnum)columnContract.SystemTypeId;
						if (aqlColumnDataTypeEnum == SqlColumnDataTypeEnum.sql_sysname
						    || aqlColumnDataTypeEnum == SqlColumnDataTypeEnum.sql_hierarchyid
						    || aqlColumnDataTypeEnum == SqlColumnDataTypeEnum.sql_geometry
						    || aqlColumnDataTypeEnum == SqlColumnDataTypeEnum.sql_sql_variant
						    || aqlColumnDataTypeEnum == SqlColumnDataTypeEnum.sql_binary
						    || aqlColumnDataTypeEnum == SqlColumnDataTypeEnum.sql_image
						    || aqlColumnDataTypeEnum == SqlColumnDataTypeEnum.sql_varbinary)
						{
							MessageBox.Show(CommonResource.TableContaisUndefined, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

							return;
						}
					}
				}
				#endregion

				#region Create Columns String
				String columnsString = getScriptResultColumns();
				#endregion

				#region Create Rows Script
				StringBuilder sbRows = new StringBuilder();
				for (int i = 0; i < activeRows.Length; i++)
				{
					DataRow row = activeRows[i];
					if (i > 0)
						sbRows.AppendLine();

					sbRows.Append("(");
					for (int j = 0; j < dataTable.Columns.Count; j++)
					{
						DataColumn column = dataTable.Columns[j];
						ColumnContract columnContract = columnContracts.First(c => c.Name == column.ColumnName);

						Object cellValue = row[column];
						try
						{
							Tuple<Boolean, String> formatResult = formatCellValue(columnContract, cellValue);
							String cellValueString = formatResult.Item2;
							Boolean stringValue = formatResult.Item1;
							if (j != 0)
								sbRows.Append(", ");

							sbRows.Append(stringValue ? $"'{cellValueString}'" : $"{cellValueString}");
						}
						catch(Exception ex)
						{
							throw new Exception(String.Format(CommonResource.ErrorOccuredOnRowColumnValue, i + 1, column.ColumnName, cellValue), ex);
						}
					}
					sbRows.Append(")");
				}
				#endregion

				#region Has Identity Column?
				Boolean hasIdentityColumn = false;
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					DataColumn column = dataTable.Columns[j];
					ColumnContract columnContract = columnContracts.First(c => c.Name == column.ColumnName);
					if (columnContract.IsIdentity ?? false)
					{
						hasIdentityColumn = true;

						break;
					}
				}
				#endregion

				#region Create SQL Strings
				String tableName = String.Format(
					UseNoSquareBrackets ? "{0}.{1}.{2}" : "[{0}].[{1}].[{2}]",
					ucDBObjectSelectControl.SelectedDB.Name,
					ucDBObjectSelectControl.SelectedSchema.Name,
					ucDBObjectSelectControl.SelectedDBObject.Name);
				if (hasIdentityColumn)
					sbScript.AppendLine($"SET IDENTITY_INSERT {tableName} ON");

				String[] delim = { Environment.NewLine, "\n" };
				String[] rows = sbRows.ToString().Split(delim, StringSplitOptions.None);
				if (!SeperateInsertScripts)
				{
					sbScript.AppendLine($"INSERT INTO {tableName} ");
					sbScript.AppendLine($"({columnsString}) ");

					for (Int32 i = 0; i < rows.Length; i++)
					{
						if (i == 0)
						{
							sbScript.Append("VALUES");
							sbScript.AppendLine();
						}
						else
							sbScript.AppendLine(",");

						sbScript.Append(rows[i]);
					}
				}
				else
				{
					for (Int32 i = 0; i < rows.Length; i++)
					{
						if (i != 0)
						{
							sbScript.AppendLine();
							sbScript.AppendLine();
						}

						sbScript.AppendLine($"INSERT INTO {tableName} ");
						sbScript.AppendLine($"({columnsString}) ");
						sbScript.Append("VALUES");
						sbScript.AppendLine();
						sbScript.Append(rows[i]);
					}
				}

				if (hasIdentityColumn)
				{
					sbScript.AppendLine();
					sbScript.AppendLine($"SET IDENTITY_INSERT {tableName} OFF");
				}
				#endregion
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
			#endregion

			#region Display Result
			OutputSqlScript = sbScript.ToString();
			lblOutputSqlScript.Text = String.Format(CommonResource.InsertScriptsWereCreated, activeRows.Length);
			#endregion
		}

        private void tsbSave_Click(object sender, EventArgs e)
		{
			UIHelper.SaveText(saveFileDialog, OutputSqlScript);
		}

		//private void tsbWordWrap_Click(object sender, EventArgs e)
		//{
		//	tsbWordWrap.CheckState = tsbWordWrap.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
		//	scOutputSqlScript.WrapMode
		//		= _ucSqlNotePad.scSqlQuery.WrapMode
		//		= tsbWordWrap.CheckState == CheckState.Checked ? ScintillaNET.WrapMode.Word : ScintillaNET.WrapMode.None;
		//}

		private void tsbNoSquareBrackets_Click(object sender, EventArgs e)
		{
			tsbWithSquareBrackets.CheckState = tsbWithSquareBrackets.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
		}

		private void tsbCriteriaCollapse_Click(object sender, EventArgs e)
		{
			scMain.Panel1Collapsed = !scMain.Panel1Collapsed;
			tsbCriteriaCollapse.Image
				= scMain.Panel1Collapsed
				? Properties.Resources.NotCollapse
				: Properties.Resources.Collapse;
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			UIHelper.SafeSetSplitterDistance(scMain, 450);
		}

		private void tsbExportToExcel_Click(object sender, EventArgs e)
		{
			frmMain frm = (frmMain)MainForm;
			frm.saveFileDialog.Filter = CommonResource.ExcelFiles;
			if (frm.saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Boolean exportResult = ExcelHelper.ExportDataGridViewToExcel(dgvScriptResult, frm.saveFileDialog.FileName, CommonResource.SqlScriptResults);
					if (exportResult)
					{
						if (MessageBox.Show(CommonResource.DataHasBeenSuccessfullyExportedToExcelWouldYouLikeToOpenExcel, CommonResource.ExportSuccessful, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							Process.Start(frm.saveFileDialog.FileName);
						}
					}
				}
				catch (Exception ex)
				{
					LogHelper.Error(ex);
					MessageBox.Show(ex.Message, CommonResource.ExportFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void tsbImportFromExcel_Click(object sender, EventArgs e)
		{
			frmMain frm = (frmMain)MainForm;
			frm.openFileDialog.Filter = CommonResource.ExcelFiles;
			if (frm.openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					DataTable dtImportedExcel = ExcelHelper.ImportFromExcel(frm.openFileDialog.FileName, ColumnsDataSource.ToList());
					if (dtImportedExcel != null)
					{
						MessageBox.Show(CommonResource.DataHasBeenSuccessfullyImported, CommonResource.ImportSuccessful, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}

					ScriptResultDataSource = dtImportedExcel;
				}
				catch (Exception ex)
				{
					LogHelper.Error(ex);
					MessageBox.Show(ex.Message, CommonResource.ImportFromExcelFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void tsbSeperate_Click(object sender, EventArgs e)
		{
			tsbSeperate.CheckState = tsbSeperate.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
		}

		private void dgvScriptResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			UIHelper.ShowException(new Exception(CommonResource.DataTypeMismatchedErrorOccured, e.Exception));
			e.ThrowException = false;
		}

		private void dgvScriptResult_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			// DataTable'dan da sil
			if (ScriptResultDataSource != null && e.Row.DataBoundItem is DataRowView rowView)
			{
				rowView.Row.Delete();
			}
		}

		private void dgvScriptResult_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			Type dataType = dgvScriptResult.Columns[e.ColumnIndex].ValueType;
			if (e.FormattedValue == null || e.FormattedValue.ToString() == String.Empty)
				return;

			if (dataType == typeof(int))
			{
				if (!int.TryParse(Convert.ToString(e.FormattedValue), out _))
				{
					UIHelper.ShowException(new Exception(String.Format(CommonResource.DataTypeMismatchedErrorOccuredDataTypeShouldBe, "Integer")));

					e.Cancel = true;
				}
			}
			else if (dataType == typeof(byte))
			{
				if (!byte.TryParse(Convert.ToString(e.FormattedValue), out _))
				{
					UIHelper.ShowException(new Exception(String.Format(CommonResource.DataTypeMismatchedErrorOccuredDataTypeShouldBe, "Byte")));

					e.Cancel = true;
				}
			}
			else if (dataType == typeof(Int64))
			{
				if (!Int64.TryParse(Convert.ToString(e.FormattedValue), out _))
				{
					UIHelper.ShowException(new Exception(String.Format(CommonResource.DataTypeMismatchedErrorOccuredDataTypeShouldBe, "LongInt")));

					e.Cancel = true;
				}
			}
			else if (dataType == typeof(decimal) || dataType == typeof(float))
			{
				if (!decimal.TryParse(Convert.ToString(e.FormattedValue), out _))
				{
					UIHelper.ShowException(new Exception(String.Format(CommonResource.DataTypeMismatchedErrorOccuredDataTypeShouldBe, "Decimal")));

					e.Cancel = true;
				}
			}
			else if (dataType == typeof(DateTime))
			{
				if (!DateTime.TryParse(Convert.ToString(e.FormattedValue), out _))
				{
					UIHelper.ShowException(new Exception(String.Format(CommonResource.DataTypeMismatchedErrorOccuredDataTypeShouldBe, "DateTime")));

					e.Cancel = true;
				}
			}
			else if (dataType == typeof(Boolean))
			{
				if (!Boolean.TryParse(Convert.ToString(e.FormattedValue), out _))
				{
					UIHelper.ShowException(new Exception(String.Format(CommonResource.DataTypeMismatchedErrorOccuredDataTypeShouldBe, "Boolean")));

					e.Cancel = true;
				}
			}
		}
		#endregion

		#region Methods
		private Tuple<Boolean, String> formatCellValue(ColumnContract columnContract, Object cellValue)
		{
			String cellValueString = null;
			Boolean isStringValue = false;

			if (cellValue == DBNull.Value)
			{
				cellValueString = "NULL";
			}
			else
			{
				if (columnContract.SystemTypeId != null)
					switch ((SqlColumnDataTypeEnum)columnContract.SystemTypeId)
					{
						case SqlColumnDataTypeEnum.sql_date:
							isStringValue = true;
							cellValueString = Convert.ToDateTime(cellValue).ToString("yyyy-MM-dd");

							break;
						case SqlColumnDataTypeEnum.sql_time:
							isStringValue = true;
							cellValueString = Convert.ToDateTime(cellValue).ToString("HH:mm");

							break;
						case SqlColumnDataTypeEnum.sql_datetime:
						case SqlColumnDataTypeEnum.sql_datetime2:
						case SqlColumnDataTypeEnum.sql_datetimeoffset:
						case SqlColumnDataTypeEnum.sql_smalldatetime:
						case SqlColumnDataTypeEnum.sql_timestamp:
							isStringValue = true;
							cellValueString = Convert.ToDateTime(cellValue).ToString("yyyy-MM-dd HH:mm:ss");

							break;
						case SqlColumnDataTypeEnum.sql_tinyint:
							cellValueString = Convert.ToByte(cellValue).ToString();

							break;
						case SqlColumnDataTypeEnum.sql_smallint:
							cellValueString = Convert.ToInt16(cellValue).ToString();

							break;
						case SqlColumnDataTypeEnum.sql_bigint:
							cellValueString = Convert.ToInt64(cellValue).ToString();

							break;
						case SqlColumnDataTypeEnum.sql_int:
							cellValueString = Convert.ToInt32(cellValue).ToString();

							break;
						case SqlColumnDataTypeEnum.sql_bit:
							cellValueString = Convert.ToBoolean(cellValue) ? "1" : "0";
							break;
						case SqlColumnDataTypeEnum.sql_decimal:
						case SqlColumnDataTypeEnum.sql_numeric:
						case SqlColumnDataTypeEnum.sql_float:
						case SqlColumnDataTypeEnum.sql_real:
						case SqlColumnDataTypeEnum.sql_money:
						case SqlColumnDataTypeEnum.sql_smallmoney:
							NumberFormatInfo nfi = new NumberFormatInfo
							{
								NumberDecimalSeparator = "."
							};
							nfi.CurrencyGroupSeparator =
								nfi.NumberGroupSeparator =
									nfi.PercentGroupSeparator = "";

							Decimal decimalValue = Convert.ToDecimal(cellValue);
							if (decimalValue % 1 == 0)
							{
								nfi.NumberDecimalDigits = 0;
								cellValueString = decimalValue.ToString("N", nfi);
							}
							else
							{
								cellValueString = decimalValue.ToString(nfi).TrimEnd('0');
							}

							break;
						case SqlColumnDataTypeEnum.sql_varchar:
						case SqlColumnDataTypeEnum.sql_nvarchar:
						case SqlColumnDataTypeEnum.sql_char:
						case SqlColumnDataTypeEnum.sql_nchar:
						case SqlColumnDataTypeEnum.sql_text:
						case SqlColumnDataTypeEnum.sql_ntext:
						case SqlColumnDataTypeEnum.sql_uniqueidentifier:
							isStringValue = true;
							cellValueString = cellValue.ToString().Replace("'", "''");

							break;
						case SqlColumnDataTypeEnum.sql_xml:
							isStringValue = true;
							cellValueString = cellValue.ToString().Replace("'", "''");

							break;
						default:
							cellValueString = "NULL";

							break;
					}
			}

			return new Tuple<Boolean, String>(isStringValue, cellValueString);
		}

		private Boolean runScript()
		{
			Boolean result = true;

			ScriptResultDataSource = null;
			try
			{
				SQLScriptBusiness sqlScriptBusiness = new SQLScriptBusiness();
				DataTable dataTable = sqlScriptBusiness.RunSqlQueryForOneTable(ucDBObjectSelectControl.SelectedConnectionString.ConnectionString, ucDBObjectSelectControl.SelectedDB.Name, InputSqlScript);
				dgvScriptResult.AutoGenerateColumns = true;
				ScriptResultDataSource = dataTable;

				lblScriptResult.Text = String.Format(CommonResource.XRows, dataTable.Rows.Count);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);

				result = false;
			}

			return result;
		}

		private String getTableColumnsAsString(List<ColumnContract> columnList)
		{
			StringBuilder sbColumns = new StringBuilder();
			String columnFormat = UseNoSquareBrackets ? "{0}" : "[{0}]";
			if (columnList != null)
			{
				foreach (ColumnContract column in columnList.Where(c => c.IsSelected))
				{
					sbColumns.Append(sbColumns.Length != 0 ? ", " : "");
					if (String.IsNullOrWhiteSpace(column.DefaultValue))
						sbColumns.Append(String.Format(columnFormat, column.Name));
					else
					{
						Tuple<Boolean, String> formatResult = formatCellValue(column, column.DefaultValue);
						String columnFormatWithDefaultValue = UseNoSquareBrackets ? "{0} AS {1}" : "{0} AS [{1}]";
						if(formatResult.Item1)
							columnFormatWithDefaultValue = UseNoSquareBrackets ? "'{0}' AS {1}" : "'{0}' AS [{1}]";

						sbColumns.Append(String.Format(columnFormatWithDefaultValue, column.DefaultValue, column.Name));
					}
				}
			}

			return sbColumns.ToString();
		}

		private String getScriptResultColumns()
		{
			StringBuilder sbColumns = new StringBuilder();
			DataTable dataTable = ScriptResultDataSource;
			String columnFormat = UseNoSquareBrackets ? "{0}" : "[{0}]";
			foreach (var column in dataTable.Columns)
			{
				sbColumns.Append((sbColumns.Length != 0 ? ", " : "") + String.Format(columnFormat, ((DataColumn)column).ColumnName));
			}

			return sbColumns.ToString();
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			ucDBObjectSelectControl.InitForm();
			ucDBObjectSelectControl.SelectedObjectType = ucDBObjectSelectControl.ObjectTypes.First(ot => ot.Type == Enums.ObjectTypeEnum.U);
			ucDBObjectSelectControl.MainForm
				= MainForm;

			UIHelper.InitialiseScintilla(scOutputSqlScript);
			//_ucSqlNotePad.scSqlQuery.Lexer = ScintillaNET.Lexer.Sql;

			dgvColumns.AutoGenerateColumns = false;
			dgvScriptResult.AutoGenerateColumns = true;

			// DataGridView satır silme olayını dinle
			dgvScriptResult.UserDeletingRow += dgvScriptResult_UserDeletingRow;

			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);
		}

		public override BaseScreenDataContract GetFormData()
		{
			DBObjectSelectScreenDataContract dbObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectControl.GetFormData();

			//ScriptResultDataSource.TableName = "DefaultTableName";
			return new InsertScriptGeneratorScreenDataContract
			{
				Name = CommonResource.InsertScriptGenerator,
				Description = ParentTabPage.Text,

				DataSourceName = dbObjectSelectFormDataContract.DataSourceName,
				DBIndexes = dbObjectSelectFormDataContract.DBIndexes,
				ObjectType = dbObjectSelectFormDataContract.ObjectType,
				SchemaId = dbObjectSelectFormDataContract.SchemaId,
				ObjectId = dbObjectSelectFormDataContract.ObjectId,

				UseNoSquareBrackets = this.UseNoSquareBrackets,
				SeperateInsertScripts = this.SeperateInsertScripts,

				Columns = ColumnsDataSource?.ToList(),
				ScriptResultDataSource = ScriptResultDataSource,

				InputSqlScript = this.InputSqlScript,
				OutputSqlScript = this.OutputSqlScript,
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as InsertScriptGeneratorScreenDataContract;
			if (data == null)
				return;

			ucDBObjectSelectControl.SetFormData(data);

			tsbWithSquareBrackets.CheckState = data.UseNoSquareBrackets ? CheckState.Unchecked : CheckState.Checked;
			tsbSeperate.CheckState = data.SeperateInsertScripts ? CheckState.Checked : CheckState.Unchecked;

			this.ColumnsDataSource = data.Columns ?? new List<ColumnContract>();
			this.InputSqlScript = data.InputSqlScript;
			this.OutputSqlScript = data.OutputSqlScript;
			this.ScriptResultDataSource = data.ScriptResultDataSource;
		}

		private void setTextFromResource()
		{
			this.gbTableView.Text = CommonResource.TableColumns;
			this.IsSelectedColumn.HeaderText = CommonResource.Select;
			this.NameColumn.HeaderText = CommonResource.Name;
			this.TypeNameCustomColumn.HeaderText = CommonResource.Type;
			this.IsNullableColumn.HeaderText = CommonResource.Null;
			this.IdentityColumn.HeaderText = CommonResource.Ident;
			this.DefaultValueColumn.HeaderText = CommonResource.DefaultValue;
			this.ucDBObjectSelectControl.Caption = CommonResource.TableSelect;
			this.gbScriptResult.Text = CommonResource.SourceSQLScriptResult;
			this.gbOutputSqlScript.Text = CommonResource.GeneratedInsertScripts;
			this.tsbCriteriaCollapse.Text = CommonResource.CollapseCriteriaPanel;
			this.tsbWriteSourceSqlScript.Text = CommonResource.WriteSourceSqlScript;
			this.tsbRunScript.Text = CommonResource.RunSourceSQLScript;
			this.tsbCreateInsertScripts.Text = CommonResource.GenerateInsertScripts;
			this.tsbSave.Text = CommonResource.SaveGeneratedInsertScripts;
			this.tsbExportToExcel.Text = CommonResource.ExportToExcel;
			this.tsbImportFromExcel.Text = CommonResource.ImportFromExcel;
			this.tsbWithSquareBrackets.Text = CommonResource.WithSquareBrackets;
			this.saveFileDialog.Filter = CommonResource.SaveFileDialogFilter;
			this.tsbSeperate.Text = CommonResource.CreateInsertScriptsSeperately;
			this._ucSqlNotePad.Title = CommonResource.SourceSQLScript;
		}
		#endregion
	}
}
