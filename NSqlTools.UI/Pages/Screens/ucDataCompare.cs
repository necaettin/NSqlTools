using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.HelperContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing; // Added for coloring
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;

namespace NSqlTools.UI.Pages
{
	public partial class ucDataCompare : BaseUserControl
	{
		#region Properties
		public List<ColumnContract> CompareColumnsList { get; set; } = new List<ColumnContract>();


		public DataTable compareResultDataSource;
		public DataTable CompareResultDataSource
		{
			get
			{
				return compareResultDataSource;
			}
			set
			{
				compareResultDataSource = value;
				dgvCompareResult.BindDataTable(value);

				clearEqualButtons();
			}
		}

		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					ucDBObjectSelectSource,
					ucDBObjectSelectTarget,
					dgvColumns,
					_ucSqlNotePadSource,
					dgvCompareResult
				};
			}
		}

		public Boolean SqlIsDifferent 
		{ 
			get
			{
				return tsbDiffSql.CheckState == CheckState.Checked;
			} 
		}
		#endregion

		#region Constructors
		public ucDataCompare()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Events
		private void frmMain_Resize(object sender, EventArgs e)
		{
			UIHelper.SafeSetSplitterDistance(scDBObjectCompare, 300);
		}

		private void tsbRunScript_Click(object sender, EventArgs e)
		{
			runQuery();
		}

		private void dgvCompareResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			if (dgvCompareResult.Rows.Count > 0)
			{
				var firstVal = dgvCompareResult.Rows[0].Cells[0].Value;
				Debug.WriteLine("First row, first col: " + (firstVal ?? "<null>"));
			}
			highlightDifferences();
		}

		private void tsbExportToExcel_Click(object sender, EventArgs e)
		{
			frmMain frm = (frmMain)MainForm;
			frm.saveFileDialog.Filter = CommonResource.ExcelFiles;
			if (frm.saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					setCompareResultDifferenceTypeImageTags();

					Boolean exportResult = ExcelHelper.ExportDataGridViewToExcel(dgvCompareResult, frm.saveFileDialog.FileName, CommonResource.DataCompareResults);
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

		private void tsbDisplayOnlyNotEqualColumns_Click(object sender, EventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;
			button.CheckState = button.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;

			highlightDifferences();
		}

		private void ucDBObjectSelectSource_OnDBChanged(object sender, EventArgs e)
		{
			_ucSqlNotePadSource.SchemaKeywordList = _ucSqlNotePadTarget.SchemaKeywordList = ucDBObjectSelectSource.SchemaNameList;
			_ucSqlNotePadSource.DBObjectKeywordList = _ucSqlNotePadTarget.DBObjectKeywordList = ucDBObjectSelectSource.DBObjectNameList;
		}

		private void ucDBObjectSelectSource_OnDBClear(object sender, EventArgs e)
		{
			_ucSqlNotePadSource.SchemaKeywordList = _ucSqlNotePadTarget.SchemaKeywordList = null;
			_ucSqlNotePadSource.DBObjectKeywordList = _ucSqlNotePadTarget.DBObjectKeywordList = null;
		}

		private void tsbDiffSql_Click(object sender, EventArgs e)
		{
			tsbDiffSql.CheckState = tsbDiffSql.CheckState
				== CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;

			scSqlScript.Panel2Collapsed = !tsbDiffSql.Checked;
			_ucSqlNotePadSource.Title = SqlIsDifferent ? NSqlTools.Types.Properties.CommonResource.SQLScriptSource : NSqlTools.Types.Properties.CommonResource.SQLScriptSourceTarget;	
		}

		private void tsbCriteriaCollapse_Click(object sender, EventArgs e)
		{
			scDataCompare.Panel1Collapsed = !scDataCompare.Panel1Collapsed;
			tsbCriteriaCollapse.Image
				= scDataCompare.Panel1Collapsed
				? Resources.NotCollapse
				: Resources.Collapse;
		}
		#endregion

		#region Methods
		private void runQuery()
		{
			#region Validation
			if (ucDBObjectSelectSource.SelectedDB == null 
				|| ucDBObjectSelectTarget.SelectedDB == null 
				|| String.IsNullOrEmpty(_ucSqlNotePadSource.scSqlQuery.Text)
				|| (SqlIsDifferent && String.IsNullOrEmpty(_ucSqlNotePadTarget.scSqlQuery.Text)))
			{
				MessageBox.Show(CommonResource.FillSourceDBTargetDBAndSQL, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}

			if (CompareColumnsList == null || CompareColumnsList.Where(c => !String.IsNullOrWhiteSpace(c.Name)).ToList().Count == 0)
			{
				MessageBox.Show(CommonResource.PleaseFillCompareColumns, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}

			#endregion

			#region Query Results
			CompareResultDataSource = null;
			try
			{
				SQLScriptBusiness sqlScriptBusiness = new SQLScriptBusiness();
				String runQueryStringSource = _ucSqlNotePadSource.scSqlQuery.Text;
				String runQueryStringTarget = _ucSqlNotePadTarget.scSqlQuery.Text;

				#region Source Data Table
				RunSqlResultContract runSqlResultSourceContract = sqlScriptBusiness.RunSqlQuery(
					ucDBObjectSelectSource.SelectedConnectionString.ConnectionString,
					ucDBObjectSelectSource.SelectedDB.Name,
					runQueryStringSource);

				if (runSqlResultSourceContract == null || runSqlResultSourceContract.TableCollection == null || runSqlResultSourceContract.TableCollection.Count != 1)
				{
					MessageBox.Show(CommonResource.SourceSQLShouldReturnOneTable, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				DataTable sourceDataTable = runSqlResultSourceContract.TableCollection[0];
				#endregion

				#region Target Data Table
				RunSqlResultContract runSqlResultTargetContract = sqlScriptBusiness.RunSqlQuery(
					ucDBObjectSelectTarget.SelectedConnectionString.ConnectionString,
					ucDBObjectSelectTarget.SelectedDB.Name,
					!SqlIsDifferent ? runQueryStringSource : runQueryStringTarget);

				// Bug fix: previously used runSqlResultSourceContract again
				DataTable targetDataTable = runSqlResultTargetContract?.TableCollection?[0];
				#endregion

				#region Add Columns to Result Data Table
				DataTable resultsDataTable = new DataTable("ResultsDataTable");
				dgvCompareResult.Columns.Clear();

				DataColumn compareResultColumn = new DataColumn("DifferenceType", typeof(Int32)) { DefaultValue = (Int32)ColumnDifferenceTypeEnum.Equal};
				resultsDataTable.Columns.Add(compareResultColumn);
				addColumnToDataGrid("DifferenceType", false, DataGridViewColumnSortMode.NotSortable);
				DataColumn compareImageResultColumn = new DataColumn("DifferenceTypeImage", typeof(Bitmap)) {  Caption="", DefaultValue = Resources.Equality_Equal };
				resultsDataTable.Columns.Add(compareImageResultColumn);
				addColumnToDataGrid("DifferenceTypeImage", false, DataGridViewColumnSortMode.NotSortable);

				foreach (DataColumn dataColumn in sourceDataTable.Columns)
				{
					String sourceColumnName = CommonResource.Source + dataColumn.ColumnName;
					DataColumn compareSourceColumn = new DataColumn(sourceColumnName, dataColumn.DataType);
					compareSourceColumn.Prefix = dataColumn.ColumnName;
					resultsDataTable.Columns.Add(compareSourceColumn);
					addColumnToDataGrid(sourceColumnName);

					String targetColumnName = CommonResource.Target + dataColumn.ColumnName;
					DataColumn compareTargetColumn = new DataColumn(targetColumnName, dataColumn.DataType);
					compareTargetColumn.Prefix = dataColumn.ColumnName;
					resultsDataTable.Columns.Add(compareTargetColumn);
					addColumnToDataGrid(targetColumnName);
				}

				#endregion

				#region Add Rows to Result Data Table
				// Exists in source
				foreach (DataRow sourceDataRow in sourceDataTable.Rows)
				{
					DataRow resultsDataRow = resultsDataTable.NewRow();
					foreach (DataColumn dataColumn in sourceDataTable.Columns)
					{
						resultsDataRow[CommonResource.Source + dataColumn.ColumnName] = sourceDataRow[dataColumn.ColumnName];
					}

					String filterExpression = String.Join(" AND ", 
						CompareColumnsList
						.Where(c => !String.IsNullOrWhiteSpace(c.Name))
						.Select(cc => $"[{cc.Name}] = '{sourceDataRow[cc.Name].ToString().Replace("'", "''")}'"));

					DataRow[] matchingTargetRows = targetDataTable != null 
						? targetDataTable.Select(filterExpression) 
						: Array.Empty<DataRow>();

					if (matchingTargetRows.Length > 0 && targetDataTable != null)
					{
						DataRow targetDataRow = matchingTargetRows[0];
						foreach (DataColumn dataColumn in targetDataTable.Columns)
						{
							resultsDataRow[CommonResource.Target + dataColumn.ColumnName] = targetDataRow[dataColumn.ColumnName];
						}
					}
					else
					{
						resultsDataRow["DifferenceType"] = (Int32)ColumnDifferenceTypeEnum.SourceExists;
						resultsDataRow["DifferenceTypeImage"] = Resources.Equality_SourceExists;
					}

					resultsDataTable.Rows.Add(resultsDataRow);
				}

				// Exists in target only
				if (targetDataTable != null)
				{
					foreach (DataRow targetDataRow in targetDataTable.Rows)
					{
						String filterExpression = String.Join(" AND ",
							CompareColumnsList
								.Where(c => !String.IsNullOrWhiteSpace(c.Name))
								.Select(cc =>
									$"[{cc.Name}] = '{targetDataRow[cc.Name].ToString().Replace("'", "''")}'"));

						DataRow[] matchingTargetRows = sourceDataTable.Select(filterExpression);
						if (matchingTargetRows.Length > 0)
							continue;

						DataRow resultsDataRow = resultsDataTable.NewRow();
						foreach (DataColumn dataColumn in targetDataTable.Columns)
						{
							resultsDataRow[CommonResource.Target + dataColumn.ColumnName] =
								targetDataRow[dataColumn.ColumnName];
						}

						resultsDataRow["DifferenceType"] = (Int32)ColumnDifferenceTypeEnum.TargetExists;
						resultsDataRow["DifferenceTypeImage"] = Resources.Equality_TargetExists;

						resultsDataTable.Rows.Add(resultsDataRow);
					}
				}


				// Set default value for DifferenceType
				foreach (DataRow row in resultsDataTable.Rows)
				{
					if (row["DifferenceType"] == DBNull.Value)
					{
						row["DifferenceType"] = (Int32)ColumnDifferenceTypeEnum.Equal;
						row["DifferenceTypeImage"] = Resources.Equality_Equal;
					}
				}
				#endregion

				#region Highlight Differences
				dgvCompareResult.DataBindingComplete -= dgvCompareResult_DataBindingComplete;
				CompareResultDataSource = resultsDataTable;
				dgvCompareResult.DataBindingComplete += dgvCompareResult_DataBindingComplete;
				highlightDifferences(true);
				#endregion
			}
			catch (SqlException ex)
			{
				LogHelper.Error(ex);
				StringBuilder sb = new StringBuilder();
				foreach (SqlError error in ex.Errors)
				{
					sb.AppendLine($"Message: {error.Message}");
					sb.AppendLine($"Line Number: {error.LineNumber}");
					sb.AppendLine($"Number: {error.Number}");
					sb.AppendLine($"Source: {error.Source}");
					sb.AppendLine($"State: {error.State}");
					sb.AppendLine($"Procedure: {error.Procedure}");
					sb.AppendLine("----------------------");
				}
				MessageBox.Show(String.Format(CommonResource.ErrorOccuredWhileRunningSqlToDataCompare, sb), CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(ex.Message);
				MessageBox.Show(String.Format(CommonResource.ErrorOccuredWhileRunningSqlToDataCompare, sb), CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			#endregion
		}

		private void highlightDifferences(Boolean setData = false)
		{
			if (CompareResultDataSource == null || CompareResultDataSource.Columns.Count == 0)
				return;

			// Build a map of source/target column pairs
			var sourceColumns = dgvCompareResult.Columns
				.Cast<DataGridViewColumn>()
				.Where(c => c.Name.StartsWith(CommonResource.Source))
				.ToList();

			dgvCompareResult.Columns["DifferenceType"].Visible = false;
			dgvCompareResult.Columns["DifferenceTypeImage"].HeaderText = CommonResource.Result;

			Int32 diffColumnCount = 0;
			Int32 diffCellCount = 0;
			foreach (var sourceCol in sourceColumns)
			{
				string baseName = sourceCol.Name.Substring(CommonResource.Source.Length);
				var targetCol = dgvCompareResult.Columns
					.Cast<DataGridViewColumn>()
					.FirstOrDefault(c => c.Name == CommonResource.Target + baseName);

				if (targetCol == null)
					continue;

				Boolean hasAnyColumnDifferences = false;
				foreach (DataGridViewRow row in dgvCompareResult.Rows)
				{
					object sourceVal = row.Cells[sourceCol.Index].Value;
					object targetVal = row.Cells[targetCol.Index].Value;

					Boolean valuesAreEqual = areValuesEqual(sourceVal, targetVal);
					if (!valuesAreEqual)
					{
						row.Cells[sourceCol.Index].Style.BackColor = Color.LightCoral;
						row.Cells[targetCol.Index].Style.BackColor = Color.LightCoral;
						row.Cells[sourceCol.Index].Style.ForeColor = Color.White;
						row.Cells[targetCol.Index].Style.ForeColor = Color.White;

						// Set DifferenceType if required
						if (setData && (Int32)row.Cells["DifferenceType"].Value == (Int32)ColumnDifferenceTypeEnum.Equal)
						{
							row.Cells["DifferenceType"].Value = (Int32)ColumnDifferenceTypeEnum.NotEqual;
							row.Cells["DifferenceTypeImage"].Value = Resources.Equality_NotEqual;
						}
						hasAnyColumnDifferences = true;
						diffCellCount++;
					}
					else
					{
						// Optional: reset style if equal (in case of refresh)
						row.Cells[sourceCol.Index].Style.BackColor = dgvCompareResult.DefaultCellStyle.BackColor;
						row.Cells[targetCol.Index].Style.BackColor = dgvCompareResult.DefaultCellStyle.BackColor;
						row.Cells[sourceCol.Index].Style.ForeColor = dgvCompareResult.DefaultCellStyle.ForeColor;
						row.Cells[targetCol.Index].Style.ForeColor = dgvCompareResult.DefaultCellStyle.ForeColor;
					}
				}

				if (hasAnyColumnDifferences)
					diffColumnCount++;

				// Kapatılacak kolonlar
				if (CompareColumnsList.Any(c => c.Name == baseName))
					sourceCol.Visible = targetCol.Visible = true;
				else
					sourceCol.Visible = targetCol.Visible =
						!(tsbDisplayOnlyNotEqualColumns.CheckState == CheckState.Checked
						&& !hasAnyColumnDifferences);
			}

			// Sonuç
			Int32 diffRowCount = dgvCompareResult.Rows
				.Cast<DataGridViewRow>()
				.Count(c => (ColumnDifferenceTypeEnum)c.Cells["DifferenceType"].Value != ColumnDifferenceTypeEnum.Equal);
			lblStatus.Text = String.Format(CommonResource.TotalRecordCount0NumberOfColumnsContainingDifferences1NumberOfRowsContainingDifferences2NumberOfCellsContainingDifferences3,
				CompareResultDataSource.Rows.Count,
				diffColumnCount,
				diffRowCount,
				diffCellCount
			);
		}

		private void filterColumnsGrid(Object sender, EventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;
			button.CheckState = button.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;

			filterDataSource();
		}

		private void filterDataSource()
		{
			if (CompareResultDataSource == null)
			{
				CompareResultDataSource = null;
				lblStatus.Text = null;

				return;
			}

			var filteredRows = CompareResultDataSource.AsEnumerable()
			.Where(row =>
				(tsbEqual.CheckState == CheckState.Unchecked && tsbNotEqual.CheckState == CheckState.Unchecked && tsbSourceExists.CheckState == CheckState.Unchecked && tsbTargetExists.CheckState == CheckState.Unchecked)
				|| (tsbEqual.CheckState == CheckState.Checked && row.Field<Int32>("DifferenceType") == (Int32)ColumnDifferenceTypeEnum.Equal)
				|| (tsbNotEqual.CheckState == CheckState.Checked && row.Field<Int32>("DifferenceType") == (Int32)ColumnDifferenceTypeEnum.NotEqual)
				|| (tsbSourceExists.CheckState == CheckState.Checked && row.Field<Int32>("DifferenceType") == (Int32)ColumnDifferenceTypeEnum.SourceExists)
				|| (tsbTargetExists.CheckState == CheckState.Checked && row.Field<Int32>("DifferenceType") == (Int32)ColumnDifferenceTypeEnum.TargetExists)
			);

			dgvCompareResult.BindDataTable(filteredRows.Any()
				? filteredRows.CopyToDataTable()
				: CompareResultDataSource.Clone()
			); // returns empty table with same schema
		}

		private void clearEqualButtons()
		{
			tsbEqual.CheckState
				= tsbNotEqual.CheckState
				= tsbSourceExists.CheckState
				= tsbTargetExists.CheckState
				= CheckState.Unchecked;
		}

		private bool areValuesEqual(object a, object b)
		{
			// Treat DBNull and null as equivalent
			bool isNullA = a == null || a == DBNull.Value;
			bool isNullB = b == null || b == DBNull.Value;

			if (isNullA && isNullB)
				return true;
			if (isNullA || isNullB)
				return false;

			// Compare by string representation for simplicity
			return String.Equals(Convert.ToString(a), Convert.ToString(b), StringComparison.Ordinal);
		}

		private void setCompareResultDifferenceTypeImageTags()
		{
			foreach (DataGridViewRow row in dgvCompareResult.Rows)
			{
				if (row.Cells["DifferenceType"].Value == DBNull.Value)
					continue;

				ColumnDifferenceTypeEnum columnDifferenceType = (ColumnDifferenceTypeEnum)row.Cells["DifferenceType"].Value;
				switch (columnDifferenceType)
				{
					case ColumnDifferenceTypeEnum.Equal:
						row.Cells["DifferenceTypeImage"].Tag = CommonResource.Equal;

						break;
					case ColumnDifferenceTypeEnum.NotEqual:
						row.Cells["DifferenceTypeImage"].Tag = CommonResource.NotEqual;

						break;
					case ColumnDifferenceTypeEnum.SourceExists:
						row.Cells["DifferenceTypeImage"].Tag = CommonResource.ExistsInSource;

						break;
					case ColumnDifferenceTypeEnum.TargetExists:
						row.Cells["DifferenceTypeImage"].Tag = CommonResource.ExistsInTarget;

						break;
					default:
						row.Cells["DifferenceTypeImage"].Tag = String.Empty;

						break;
				}
			}
		}

		private void addColumnToDataGrid(String columnName, Boolean visibility = true, DataGridViewColumnSortMode sortMode = DataGridViewColumnSortMode.Automatic)
		{
			var colDiffType = new DataGridViewTextBoxColumn
			{
				Name = columnName,
				DataPropertyName = columnName,
				Visible = visibility,
				SortMode = sortMode
			};
			dgvCompareResult.Columns.Add(colDiffType);
		}

		private void setTextFromResource()
		{
			this.gbTableView.Text = CommonResource.ComparisonColumns;
			this.ucDBObjectSelectTarget.Caption = CommonResource.TargetDB;

			this.ucDBObjectSelectSource.Caption = CommonResource.SourceDB;
			this.tsbEqual.Text = CommonResource.Equal;
			this.tsbNotEqual.Text = CommonResource.NotEqual;
			this.tsbSourceExists.Text = CommonResource.ExistsInSource;
			this.tsbTargetExists.Text = CommonResource.ExistsInTarget;

			this.tsbExportToExcel.Text = CommonResource.ExportToExcel;
			this.tsbCriteriaCollapse.Text = CommonResource.CollapseCriteriaPanel;
			this.tsbRunScript.Text = CommonResource.RunSourceSQLScript;
			this.NameColumn.HeaderText = CommonResource.ColumnName;
			this.tsbDisplayOnlyNotEqualColumns.Text = CommonResource.DisplayOnlyNotEqualColumns;
			this._ucSqlNotePadSource.Title = NSqlTools.Types.Properties.CommonResource.SQLScriptSource;
			this._ucSqlNotePadTarget.Title = NSqlTools.Types.Properties.CommonResource.SQLScriptTarget;
			this.tsbDiffSql.Text = NSqlTools.Types.Properties.CommonResource.SqlScriptIsDifferent;
		}

		#endregion

		#region Override Methods
		public override void InitForm()
		{
			ucDBObjectSelectSource.MainForm
				= ucDBObjectSelectTarget.MainForm
				= MainForm;

			ucDBObjectSelectSource.InitForm();
			ucDBObjectSelectSource.SelectedObjectType = ucDBObjectSelectSource.ObjectTypes.First(ot => ot.Type == ObjectTypeEnum.U);

			ucDBObjectSelectSource.InitForm();
			ucDBObjectSelectTarget.InitForm();

			dgvColumns.AutoGenerateColumns = false;
			dgvColumns.DataSource = new SortableBindingList<ColumnContract>(CompareColumnsList);

			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);

			dgvCompareResult.DataBindingComplete += dgvCompareResult_DataBindingComplete;
		}

		public override BaseScreenDataContract GetFormData()
		{
			DBObjectSelectScreenDataContract sourceDBObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectSource.GetFormData();
			DBObjectSelectScreenDataContract targetDBObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectTarget.GetFormData();

			return new DataCompareScreenDataContract
			{
				Name = CommonResource.DataCompare,
				SourceDBObjectSelectFormDataContract = sourceDBObjectSelectFormDataContract,
				TargetDBObjectSelectFormDataContract = targetDBObjectSelectFormDataContract,
				ComparisonColumns = ((SortableBindingList<ColumnContract>)dgvColumns.DataSource).ToList(),
				InputSqlScriptSource = _ucSqlNotePadSource.scSqlQuery.Text,
				InputSqlScriptTarget = _ucSqlNotePadTarget.scSqlQuery.Text,
				IsDiffSql = tsbDiffSql.Checked,
				//CompareResult = CompareResultDataSource
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as DataCompareScreenDataContract;
			if (data == null)
				return;

			ucDBObjectSelectSource.SetFormData(data.SourceDBObjectSelectFormDataContract);
			ucDBObjectSelectTarget.SetFormData(data.TargetDBObjectSelectFormDataContract);

			_ucSqlNotePadSource.scSqlQuery.Text = data.InputSqlScriptSource;
			if (data.IsDiffSql)
			{
				tsbDiffSql_Click(tsbDiffSql, EventArgs.Empty);
				_ucSqlNotePadTarget.scSqlQuery.Text = data.InputSqlScriptTarget;
			}

			CompareColumnsList = data.ComparisonColumns;
			dgvColumns.DataSource = new SortableBindingList<ColumnContract>(CompareColumnsList);
		}
		#endregion
	}
}
