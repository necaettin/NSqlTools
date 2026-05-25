using BOA.Common.Types;
using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Contracts;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;
using Constants = NSqlTools.Types.Constants;

namespace NSqlTools.UI.Pages
{
	public partial class ucDBBatchCompare : BaseUserControl
	{
		#region Properties
		public List<DBObjectCompareContract> dataSource;
		public List<DBObjectCompareContract> DataSource
		{
			get
			{
				return dataSource;
			}
			set
			{
				dataSource = value;
				
				dgvBatchCompare.BindList(value);

				Boolean viewerIsVisible = dgvBatchCompare.CurrentRow != null && dgvBatchCompare.CurrentRow.Index >= 0;
				scCompareListAndViewer.Panel2Collapsed = !viewerIsVisible;

				lblStatus.Text = null;
				clearEqualButtons();
			}
		}

		private BackgroundWorker backgroundWorker;
		private CancellationTokenSource _cancellationTokenSource;

		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					_ucObjectType,
					txtNameFilter,
					ucDBObjectSelectSource,
					ucDBObjectSelectTarget,
					dgvBatchCompare
				};
			}
		}
		#endregion

		#region Constructor
		public ucDBBatchCompare()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Events
		private void _ucObjectType_OnObjectTypeChanged(object sender, EventArgs e)
		{
			ucDBObjectSelectSource.SelectedObjectType = _ucObjectType.SelectedObjectType;

			clearEqualButtons();
		}

		private void ucDBObjectSelectSource_OnObjectTypeChanged(object sender, EventArgs e)
		{
			ucDBObjectSelectTarget.SelectedObjectType = ucDBObjectSelectSource.SelectedObjectType;

			clearEqualButtons();
		}

		private void ucDBObjectSelectSource_OnSchemaChanged(object sender, EventArgs e)
		{
			fillGrid();
		}

		private void ucDBObjectSelectSource_OnSchemaClear(object sender, EventArgs e)
		{
			fillGrid();
		}

		private void ucDBObjectSelectTarget_OnSchemaChanged(object sender, EventArgs e)
		{
			fillGrid();
		}

		private void ucDBObjectSelectTarget_OnSchemaClear(object sender, EventArgs e)
		{
			fillGrid();
		}

		private void filterColumnsGrid(Object sender, EventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;
			button.CheckState = button.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;

			filterDataSource();
		}

		private void gvBatchCompare_SelectionChanged(object sender, EventArgs e)
		{
			Boolean viewerIsVisible = dgvBatchCompare.CurrentRow != null && dgvBatchCompare.CurrentRow.Index >= 0;

			scCompareListAndViewer.Panel2Collapsed = !viewerIsVisible;
			if (!viewerIsVisible)
				return;

			DBObjectCompareContract dbObjectCompareContract = (DBObjectCompareContract)dgvBatchCompare.CurrentRow.DataBoundItem;
			ObjectTypeEnum type = _ucObjectType.SelectedObjectType.Type;
			switch (type)
			{
				case ObjectTypeEnum.U:
					ucNotePadCompareControl.Dock = DockStyle.None;
					ucNotePadCompareControl.Visible = false;
					ucTableViewCompareControl.Dock = DockStyle.Fill;
					ucTableViewCompareControl.Visible = true;
					ucTableViewCompareControl.FillGrid(dbObjectCompareContract.ColumnCompareResultList);

					break;
				default:
					ucTableViewCompareControl.Dock = DockStyle.None;
					ucTableViewCompareControl.Visible = false;
					ucNotePadCompareControl.Dock = DockStyle.Fill;
					ucNotePadCompareControl.Visible = true;
					ucNotePadCompareControl.PrepareBothNotePads(dbObjectCompareContract.DefinitionSource, dbObjectCompareContract.SchemaNameSource, dbObjectCompareContract.NameSource, dbObjectCompareContract.DefinitionTarget, dbObjectCompareContract.SchemaNameTarget, dbObjectCompareContract.NameTarget);

					break;
			}
		}

		private void tsbExportBatchCompareResultToExcel_Click(object sender, EventArgs e)
		{
			frmMain frm = (frmMain)MainForm;
			frm.saveFileDialog.Filter = CommonResource.ExcelFiles;
			if (frm.saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Boolean exportResult = ExcelHelper.ExportDataGridViewToExcel(dgvBatchCompare, frm.saveFileDialog.FileName, CommonResource.BatchCompareResults);
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

		private void tsbStartBatchCompare_Click(object sender, EventArgs e)
		{
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = new CancellationTokenSource();

			DataSource = null;
			lblStatus.Text = null;
            progressBar.Value = 0;
			progressBar.Visible = true;
			tsbStartBatchCompare.Enabled = false;
			tsbCancelBatchCompare.Enabled = true;
			backgroundWorker.RunWorkerAsync();
		}

		private void tsbCancelBatchCompare_Click(object sender, EventArgs e)
		{
			tsbCancelBatchCompare.Enabled = false;
			backgroundWorker.CancelAsync();
			_cancellationTokenSource?.Cancel();
		}

		private void tsbCriteriaCollapse_Click(object sender, EventArgs e)
		{
			scBatchCompare.Panel1Collapsed = !scBatchCompare.Panel1Collapsed;
			tsbCriteriaCollapse.Image
				= scBatchCompare.Panel1Collapsed
				? Resources.NotCollapse
				: Resources.Collapse;
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			UIHelper.SafeSetSplitterDistance(scBatchCompare, Constants.DefaultSplitterDistance);
		}
		#endregion

		#region Methods
		private void fillGrid()
		{
			Boolean canCompare = _ucObjectType.SelectedObjectType != null && (ucDBObjectSelectSource.SelectedSchema != null && ucDBObjectSelectTarget.SelectedSchema != null);
			tsbStartBatchCompare.Enabled = canCompare;
			ParentTabPage.Text = 
				canCompare 
				? CommonResource.BatchCompare + $" - {ucDBObjectSelectSource.SelectedConnectionString.Name}.{ucDBObjectSelectSource.SelectedSchema.Name}-{ucDBObjectSelectTarget.SelectedConnectionString.Name}.{ucDBObjectSelectTarget.SelectedSchema.Name}"
				: CommonResource.BatchCompare;
			DataSource = null;
		}

		private void initializeBackgroundWorker()
		{
			backgroundWorker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
			backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
			backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
		}

		private void fillObjectTypeList()
		{
			ucDBObjectSelectSource.ClearSchemaList();
		}

		public List<DBObjectCompareContract> CompareTextBasedDBObjectLists(List<DBObjectContract> dbObjectTextSourceList, List<DBObjectContract> dbObjectTextTargetList)
		{
			var columListResult = dbObjectTextSourceList.FullOuterJoin(
				dbObjectTextTargetList, source => source.Name, target => target.Name,
				(source, target, Name) => new DBObjectCompareContract
				{
					ObjectIdSource = source?.ObjectId,
					NameSource = source?.Name,
					SchemaIdSource = source?.SchemaId,
					SchemaNameSource = source?.SchemaName,
					DefinitionSource = source?.Definition,

					Difference = Resources.Equality_Equal,

					ObjectIdTarget = target?.ObjectId,
					NameTarget = target?.Name,
					SchemaIdTarget = target?.SchemaId,
					SchemaNameTarget = target?.SchemaName,
					DefinitionTarget = target?.Definition,
				},
				null,
				null
			).ToList();

			// Set equality image
			columListResult.ForEach(c =>
			{
				if (c.ObjectIdSource == null)
				{
					c.Difference = Resources.Equality_TargetExists;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.TargetExists;
				}
				else if (c.ObjectIdTarget == null)
				{
					c.Difference = Resources.Equality_SourceExists;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.SourceExists;
				}
				else if (c.DefinitionSource != c.DefinitionTarget)
				{
					c.Difference = Resources.Equality_NotEqual;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.NotEqual;
				}
				else
				{
					c.Difference = Resources.Equality_Equal;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.Equal;
				}
			});

			return columListResult;
		}

		public List<DBObjectCompareContract> CompareTableDBObjectLists(List<DBObjectContract> dbObjectTableSourceList, List<DBObjectContract> dbObjectTableTargetList)
		{

			if (dbObjectTableSourceList == null || dbObjectTableTargetList == null)
			{
				return null;
			}

			var dbObjectCompareContractList = dbObjectTableSourceList.FullOuterJoin(
				dbObjectTableTargetList, source => source.Name, target => target.Name,
				(source, target, Name) => new DBObjectCompareContract
				{
					ObjectIdSource = source?.ObjectId,
					NameSource = source?.Name,
					SchemaNameSource = source?.SchemaName,
					ColumnSourceList =	source?.ColumnList,

					Difference = Resources.Equality_Equal,

					ObjectIdTarget = target?.ObjectId,
					NameTarget = target?.Name,
					SchemaNameTarget = target?.SchemaName,
					ColumnTargetList = target?.ColumnList,
				},
				null,
				null
			).ToList();

			// Set equality image
			dbObjectCompareContractList.ForEach(c =>
			{
				if (c.ObjectIdSource == null)
				{
					c.Difference = Resources.Equality_TargetExists;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.TargetExists;
				}
				else if (c.ObjectIdTarget == null)
				{
					c.Difference = Resources.Equality_SourceExists;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.SourceExists;
				}
				else if (c.NameSource != c.NameTarget)
				{
					c.Difference = Resources.Equality_NotEqual;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.NotEqual;
				}
				else
				{
					c.Difference = Resources.Equality_Equal;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.Equal;
				}

				List<ColumnContract> columnSourceList = c.ColumnSourceList ?? new List<ColumnContract>();
				List<ColumnContract> columnTargetList = c.ColumnTargetList ?? new List<ColumnContract>();
				c.ColumnCompareResultList = columnSourceList.FullOuterJoin(
				columnTargetList, source => source.Name, target => target.Name,
				(source, target, Name) => new ColumnCompareContract
				{
					ColumnIdSource = source?.ColumnId,
					NameSource = source?.Name,
					SystemTypeIdSource = source?.SystemTypeId,
					UserTypeIdSource = source?.UserTypeId,
					MaxLengthSource = source?.MaxLength,
					PrecisionSource = source?.Precision,
					IsNullableSource = source?.IsNullable,
					IsIdentitySource = source?.IsIdentity,
					TypeNameSource = source?.TypeName,

					Difference = Resources.Equality_Equal,

					ColumnIdTarget = target?.ColumnId,
					NameTarget = target?.Name,
					SystemTypeIdTarget = target?.SystemTypeId,
					UserTypeIdTarget = target?.UserTypeId,
					MaxLengthTarget = target?.MaxLength,
					PrecisionTarget = target?.Precision,
					IsNullableTarget = target?.IsNullable,
					IsIdentityTarget = target?.IsIdentity,
					TypeNameTarget = target?.TypeName
				},
					null,
					null
				).ToList();

				// Set equality image
				c.ColumnCompareResultList.ForEach(col =>
				{
					if (col.ColumnIdSource == null)
					{
						col.Difference = Resources.Equality_TargetExists;
						col.ColumnDifferenceType = ColumnDifferenceTypeEnum.TargetExists;
					}
					else if (col.ColumnIdTarget == null)
					{
						col.Difference = Resources.Equality_SourceExists;
						col.ColumnDifferenceType = ColumnDifferenceTypeEnum.SourceExists;
					}
					else if (col.UserTypeIdSource != col.UserTypeIdTarget || col.NameSource != col.NameTarget || col.MaxLengthSource != col.MaxLengthTarget || col.PrecisionSource != col.PrecisionTarget || col.IsNullableSource != col.IsNullableTarget)
					{
						col.Difference = Resources.Equality_NotEqual;
						col.ColumnDifferenceType = ColumnDifferenceTypeEnum.NotEqual;
					}
					else
					{
						col.Difference = Resources.Equality_Equal;
						col.ColumnDifferenceType = ColumnDifferenceTypeEnum.Equal;
					}
				});
				if (c.ColumnSourceList != null && c.ColumnTargetList != null && c.ColumnCompareResultList.Any(col => col.ColumnDifferenceType != ColumnDifferenceTypeEnum.Equal))
				{
					c.Difference = Resources.Equality_NotEqual;
					c.ColumnDifferenceType = ColumnDifferenceTypeEnum.NotEqual;
				}
			});

			return dbObjectCompareContractList;
		}

		private void filterDataSource()
		{
			if (DataSource == null)
			{
				DataSource = null;
				lblStatus.Text = null;

				return;
			}

			dgvBatchCompare.BindList(
				DataSource == null ? null : new SortableBindingList<DBObjectCompareContract>(DataSource.Where(d =>
					(tsbEqual.CheckState == CheckState.Unchecked && tsbNotEqual.CheckState == CheckState.Unchecked && tsbSourceExists.CheckState == CheckState.Unchecked && tsbTargetExists.CheckState == CheckState.Unchecked)
					|| (tsbEqual.CheckState == CheckState.Checked && d.ColumnDifferenceType == ColumnDifferenceTypeEnum.Equal)
					|| (tsbNotEqual.CheckState == CheckState.Checked && d.ColumnDifferenceType == ColumnDifferenceTypeEnum.NotEqual)
					|| (tsbSourceExists.CheckState == CheckState.Checked && d.ColumnDifferenceType == ColumnDifferenceTypeEnum.SourceExists)
					|| (tsbTargetExists.CheckState == CheckState.Checked && d.ColumnDifferenceType == ColumnDifferenceTypeEnum.TargetExists)
			).ToList()));
		}

		private void clearEqualButtons()
		{
			tsbEqual.CheckState
				= tsbNotEqual.CheckState
				= tsbSourceExists.CheckState
				= tsbTargetExists.CheckState
				= CheckState.Unchecked;
		}

		private void setTextFromResource()
		{
			this.ucDBObjectSelectTarget.Caption = CommonResource.TargetSchema;
			this.ucDBObjectSelectSource.Caption = CommonResource.SourceSchema;
			this.gbFilter.Text = CommonResource.Filter;
			this.lblObjectType.Text = CommonResource.ObjectType;
			this.lblNameFilter.Text = CommonResource.NameFilter;
			this.SchemaNameSource.HeaderText = CommonResource.SchemaSource;
			this.NameSource.HeaderText = CommonResource.NameSource;
			this.Difference.HeaderText = CommonResource.Difference;
			this.SchemaNameTarget.HeaderText = CommonResource.SchemaTarget;
			this.NameTarget.HeaderText = CommonResource.NameTarget;
			this.tsbCriteriaCollapse.Text = CommonResource.CollapseCriteriaPanel;
			this.tsbStartBatchCompare.Text = CommonResource.StartDBBatchCompare;
			this.tsbExportBatchCompareResultToExcel.Text = CommonResource.ExportSearchResultToExcel;
			this.tsbEqual.Text = CommonResource.Equal;
			this.tsbNotEqual.Text = CommonResource.NotEqual;
			this.tsbSourceExists.Text = CommonResource.ExistsInSource;
			this.tsbTargetExists.Text = CommonResource.ExistsInTarget;
		}

		#endregion

		#region BackgroundWorker Methods
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			List<DBObjectCompareContract> _dataSource = new List<DBObjectCompareContract>();

			// Capture UI values on UI thread before background work
			String sourceConnectionString = null;
			String sourceDBName = null;
			Int32 sourceSchemaId = 0;
			String targetConnectionString = null;
			String targetDBName = null;
			Int32 targetSchemaId = 0;
			String nameFilter = null;
			ObjectTypeEnum objectType = ObjectTypeEnum.UNDEFINED;
			String objectTypeString = null;

			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					sourceConnectionString = ucDBObjectSelectSource.SelectedConnectionString.ConnectionString;
					sourceDBName = ucDBObjectSelectSource.SelectedDB.Name;
					sourceSchemaId = ucDBObjectSelectSource.SelectedSchema.SchemaId;
					targetConnectionString = ucDBObjectSelectTarget.SelectedConnectionString.ConnectionString;
					targetDBName = ucDBObjectSelectTarget.SelectedDB.Name;
					targetSchemaId = ucDBObjectSelectTarget.SelectedSchema.SchemaId;
					nameFilter = txtNameFilter.Text;
					objectType = ucDBObjectSelectSource.SelectedObjectType.Type;
					objectTypeString = ucDBObjectSelectSource.SelectedObjectType.Type.ToString();
				}));
			}
			else
			{
				sourceConnectionString = ucDBObjectSelectSource.SelectedConnectionString.ConnectionString;
				sourceDBName = ucDBObjectSelectSource.SelectedDB.Name;
				sourceSchemaId = ucDBObjectSelectSource.SelectedSchema.SchemaId;
				targetConnectionString = ucDBObjectSelectTarget.SelectedConnectionString.ConnectionString;
				targetDBName = ucDBObjectSelectTarget.SelectedDB.Name;
				targetSchemaId = ucDBObjectSelectTarget.SelectedSchema.SchemaId;
				nameFilter = txtNameFilter.Text;
				objectType = ucDBObjectSelectSource.SelectedObjectType.Type;
				objectTypeString = ucDBObjectSelectSource.SelectedObjectType.Type.ToString();
			}

			try
			{
				if (backgroundWorker.CancellationPending)
				{
					e.Cancel = true;
					return;
				}

				DBObjectBusiness dBObjectBusiness = new DBObjectBusiness();

				switch (objectType)
				{
					case ObjectTypeEnum.U:
						List<DBObjectContract> dbObjectTableSourceList = dBObjectBusiness.GetTableDBObjectListByDBSchemaId(
							sourceConnectionString,
							sourceDBName,
							sourceSchemaId,
							null,
							nameFilter,
							false,
							_cancellationTokenSource.Token);

						if (backgroundWorker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}

						backgroundWorker.ReportProgress(30);

						List<DBObjectContract> dbObjectTableTargetList = dBObjectBusiness.GetTableDBObjectListByDBSchemaId(
							targetConnectionString,
							targetDBName,
							targetSchemaId,
							null,
							nameFilter,
							false,
							_cancellationTokenSource.Token);

						if (backgroundWorker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}

						backgroundWorker.ReportProgress(60);

						_dataSource = CompareTableDBObjectLists(dbObjectTableSourceList, dbObjectTableTargetList);

						break;
					default:
						List<DBObjectContract> dbObjectTextSourceList = dBObjectBusiness.GetDBObjectListWithDefinitionByDBSchemaAndObjectType(
							sourceConnectionString,
							sourceDBName,
							sourceSchemaId,
							objectTypeString,
							nameFilter,
							_cancellationTokenSource.Token);

						if (backgroundWorker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}

						backgroundWorker.ReportProgress(30);

						List<DBObjectContract> dbObjectTextTargetList = dBObjectBusiness.GetDBObjectListWithDefinitionByDBSchemaAndObjectType(
							targetConnectionString,
							targetDBName,
							targetSchemaId,
							objectTypeString,
							nameFilter,
							_cancellationTokenSource.Token);

						if (backgroundWorker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}

						backgroundWorker.ReportProgress(60);

						_dataSource = CompareTextBasedDBObjectLists(dbObjectTextSourceList, dbObjectTextTargetList);

						break;
				}

				backgroundWorker.ReportProgress(100);
			}
			catch (OperationCanceledException)
			{
				LogHelper.Info(CommonResource.BatchCompareCancelledByUser);
				e.Cancel = true;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(() =>
						UIHelper.ShowException(ex)
					));
				}
				else
				{
					UIHelper.ShowException(ex);
				}
			}

			e.Result = new { DataSource = _dataSource };
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => backgroundWorker_RunWorkerCompleted(sender, e)));

				return;
			}

			// Check if control is disposed before accessing UI elements
			if (this.IsDisposed)
			{
				return;
			}

			if (progressBar != null && !progressBar.IsDisposed)
			{
				progressBar.Visible = false;
			}

			if (tsbStartBatchCompare != null && !tsbStartBatchCompare.IsDisposed)
			{
				tsbStartBatchCompare.Enabled = true;
			}

			if (tsbCancelBatchCompare != null && !tsbCancelBatchCompare.IsDisposed)
			{
				tsbCancelBatchCompare.Enabled = false;
			}

			if (e.Cancelled)
			{
				if (lblStatus != null && !lblStatus.IsDisposed)
				{
					lblStatus.Text = NSqlTools.Types.Properties.CommonResource.ComparisonCancelled;
				}
				return;
			}

			var result = (dynamic)e.Result;
			DataSource = (List<DBObjectCompareContract>)result.DataSource;

			if (lblStatus != null && !lblStatus.IsDisposed)
			{
				lblStatus.Text = String.Format(CommonResource.XObjectsWereCompared, DataSource.Count);
			}
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => backgroundWorker_ProgressChanged(sender, e)));
				return;
			}

			// Check if control is disposed before accessing it
			if (!this.IsDisposed && progressBar != null && !progressBar.IsDisposed)
			{
				progressBar.Value = e.ProgressPercentage;
			}
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			fillObjectTypeList();

			ucDBObjectSelectSource.MainForm
				= ucDBObjectSelectTarget.MainForm
				= ucNotePadCompareControl.MainForm
				= ucTableViewCompareControl.MainForm
				= MainForm;

			scCompareListAndViewer.Panel2Collapsed = true;

			initializeBackgroundWorker();
			dgvBatchCompare.AutoGenerateColumns = false;

			ucDBObjectSelectSource.InitForm();
			ucDBObjectSelectTarget.InitForm();

			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);

			_ucObjectType.BackColor = Constants.ComponentRequiredColor;
		}

		public override BaseScreenDataContract GetFormData()
		{
			Int32? objectType = (Int32?)_ucObjectType.SelectedObjectType?.Type;
			DBObjectSelectScreenDataContract sourceDBObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectSource.GetFormData();
			DBObjectSelectScreenDataContract targetDBObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectTarget.GetFormData();

			return new DBBatchCompareScreenDataContract
			{
				Name = CommonResource.BatchCompare,
				SourceDBObjectSelectFormDataContract = sourceDBObjectSelectFormDataContract,
				TargetDBObjectSelectFormDataContract = targetDBObjectSelectFormDataContract,
				ObjectTypeOriginal = objectType, 
				NameFilter = txtNameFilter.Text
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as DBBatchCompareScreenDataContract;
			if (data == null)
				return;

			txtNameFilter.Text = data.NameFilter;
			
			ucDBObjectSelectSource.SetFormData(data.SourceDBObjectSelectFormDataContract);
			ucDBObjectSelectTarget.SetFormData(data.TargetDBObjectSelectFormDataContract);

			if (data.ObjectTypeOriginal.HasValue)
			{
				_ucObjectType.OnObjectTypeChanged -= _ucObjectType_OnObjectTypeChanged;
				_ucObjectType.SelectedObjectType = _ucObjectType.ObjectTypes.FirstOrDefault(o => o.Type == (ObjectTypeEnum?)data.ObjectTypeOriginal.Value);
				_ucObjectType.OnObjectTypeChanged += _ucObjectType_OnObjectTypeChanged;
			}
		}
		#endregion
	}
}
