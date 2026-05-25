using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.HelperContracts;
using NSqlTools.Types.IntellisenseContracts;
using NSqlTools.Types.Properties;
using NSqlTools.Types.RepoContracts;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;

namespace NSqlTools.UI.Pages
{
	public partial class ucSearchDB : BaseUserControl
	{
		#region Constructor
		public ucSearchDB()
		{
			InitializeComponent();
		}
		#endregion

		#region Properties
		private BackgroundWorker backgroundWorker;
		private List<SqlCommand> _activeCommands = new List<SqlCommand>();
		private CancellationTokenSource _cancellationTokenSource;

		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					txtSearchKeyword,
					txtNameFilter,
					_ucObjectType,
					chbCaseSensitive,
					chbDBSearch,
					chbRepoSearch,
					cbRepo,
					txtRepoExtraSearchKeyword,
					ucDBObjectSelectControl,
					dgvSearchResult
				};
			}
		}
		#endregion

		#region Methods
		private void frmMain_Resize(object sender, EventArgs e)
		{
			UIHelper.SafeSetSplitterDistance(spSearchDB, Constants.DefaultSplitterDistance);
		}

		private void InitializeBackgroundWorker()
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
			ucDBObjectSelectControl.ClearSchemaList();
			_ucObjectType.SelectedObjectType = null;
		}

		private Boolean isValid()
		{
			Boolean isValid = true;

			if (!UIHelper.ComponentIsValidString((MainForm as frmMain)?.errorProvider, txtSearchKeyword.Text, lblSearchKeyword, CommonResource.SearchKeywordIsRequired))
				isValid = false;

			if (!UIHelper.ComponentIsValidBoolean((MainForm as frmMain)?.errorProvider, chbDBSearch.Checked || chbRepoSearch.Checked, CommonResource.OneOfDBSearchOrRepoSearchShouldBeChecked, chbDBSearch, chbRepoSearch))
				isValid = false;

			if (chbDBSearch.Checked && !UIHelper.ComponentIsValidBoolean((MainForm as frmMain)?.errorProvider, ucDBObjectSelectControl.SelectedDB != null, CommonResource.DBShouldBeSelected, chbDBSearch))
				isValid = false;

			if (chbRepoSearch.Checked && !UIHelper.ComponentIsValidBoolean((MainForm as frmMain)?.errorProvider, cbRepo.SelectedValue != null, CommonResource.RepoSelectionIsRequired, cbRepo))
				isValid = false;

			return isValid;
		}

		private void clearResults()
		{
			BindSearchResultGrid(null);
			lblSearchResultStatus.Text = null;
			scSearchResult.Panel2Collapsed = true;
		}

		private void runSearch()
		{
			if (!isValid())
				return;

			clearResults();
			progressBar.Value = 0;
			progressBar.Visible = true;
			scSearchResult.Panel2Collapsed = true;

			// Create new cancellation token source
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = new CancellationTokenSource();

			// Clear active commands list
			lock (_activeCommands)
			{
				_activeCommands.Clear();
			}

			// Enable cancel button, disable search button
			tsbCancelSearch.Enabled = true;
			tsbSearchDB.Enabled = false;

			backgroundWorker.RunWorkerAsync(new SearchCriteriaContract() { SearchKeyword = txtSearchKeyword.Text, CaseSensitive = chbCaseSensitive.Checked });
		}

		private void BindColumnsGrid(List<ColumnContract> dataSource)
		{
			var list = dataSource ?? new List<ColumnContract>();
			dgvColumns.BindList(list);
		}

		private void BindSearchResultGrid(List<DBObjectContract> dataSource)
		{
			var list = dataSource ?? new List<DBObjectContract>();
			dgvSearchResult.BindList(list);
		}
		#endregion

		#region Events
		private void tsbSearchDB_Click(object sender, EventArgs e)
		{
			runSearch();
		}

		private void dgvSearchResult_SelectionChanged(object sender, EventArgs e)
		{
			if (dgvSearchResult.CurrentRow == null)
			{
				scSearchResult.Panel2Collapsed = true;
				
				return;
			}

			scSearchResult.Panel2Collapsed = false;

			DBObjectContract dbObjectContract = (DBObjectContract)dgvSearchResult.CurrentRow.DataBoundItem;
			switch (dbObjectContract.ObjectType)
			{
				case Enums.ObjectTypeEnum.U:
					ucSqlNotePadControl.Dock = DockStyle.None;
					ucSqlNotePadControl.Visible = false;
					pnlColumns.Dock = DockStyle.Fill;
					pnlColumns.Visible = true;
					BindColumnsGrid(dbObjectContract.ColumnList);
					lblColumnsStatus.Text = String.Format(CommonResource.TableAndColumns, dbObjectContract.DBName, dbObjectContract.SchemaName, dbObjectContract.Name, dbObjectContract.ColumnList?.Count ?? 0);

					break;
				case ObjectTypeEnum.REPO_FILE:
					RepoBusiness repoBusiness = new RepoBusiness();
					if (this.InvokeRequired)
					{
						this.Invoke(new MethodInvoker(() =>
						{
							Task<ContentResponse> contentResponse = repoBusiness.GetRepoFileContent(dbObjectContract.Path);
							dbObjectContract.Definition = contentResponse.Result.content;
						}));
					}
					else
					{
						Task<ContentResponse> contentResponse = repoBusiness.GetRepoFileContent(dbObjectContract.Path);
						dbObjectContract.Definition = contentResponse.Result.content;
					}

					pnlColumns.Dock = DockStyle.None;
					pnlColumns.Visible = false;
					BindColumnsGrid(null);
					lblColumnsStatus.Text = null;
					
					String ext = System.IO.Path.GetExtension(dbObjectContract.Path).ToUpper();
					ScintillaNET.Lexer lexer = fileExtensionToLexer(ext);

					ucSqlNotePadControl.Title = CommonResource.RepoFileContent;
					ucSqlNotePadControl.SetCompareType(lexer);
					ucSqlNotePadControl.InitialiseScintilla(lexer);
					ucSqlNotePadControl.Dock = DockStyle.Fill;
					ucSqlNotePadControl.Visible = true;
					ucSqlNotePadControl.CaseSensitive = chbCaseSensitive.Checked;
					ucSqlNotePadControl.SetDBObject(dbObjectContract);
					ucSqlNotePadControl.HighlightWordAndGotoNextFind(txtSearchKeyword.Text);
					ucSqlNotePadControl.SearchKeyword = txtSearchKeyword.Text;
					break;
				default:
					pnlColumns.Dock = DockStyle.None;
					pnlColumns.Visible = false;
					BindColumnsGrid(null);
					lblColumnsStatus.Text = null;

					ucSqlNotePadControl.Title = CommonResource.SqlScript;
					ucSqlNotePadControl.SetCompareType(Lexer.Sql);
					ucSqlNotePadControl.InitialiseScintilla(Lexer.Sql);
					ucSqlNotePadControl.Dock = DockStyle.Fill;
					ucSqlNotePadControl.Visible = true;
					ucSqlNotePadControl.CaseSensitive = chbCaseSensitive.Checked;
					ucSqlNotePadControl.SetDBObject(dbObjectContract);
					ucSqlNotePadControl.HighlightWordAndGotoNextFind(txtSearchKeyword.Text);
					ucSqlNotePadControl.SearchKeyword = txtSearchKeyword.Text;

					break;
			}
		}

		private void ucDBObjectSelect_OnDBChanged(object sender, EventArgs e)
		{
			clearResults();
		}

		private void ucDBObjectSelect_OnDBClear(object sender, EventArgs e)
		{
			clearResults();
		}

		private void tsbExportSearchResultToExcel_Click(object sender, EventArgs e)
		{
			frmMain frm = (frmMain)MainForm;
			frm.saveFileDialog.Filter = CommonResource.ExcelFiles;
			if (frm.saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Boolean exportResult = ExcelHelper.ExportDataGridViewToExcel(dgvSearchResult, frm.saveFileDialog.FileName, CommonResource.SearchResults);
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

		private void tsbCancelSearch_Click(object sender, EventArgs e)
		{
			if (backgroundWorker != null && backgroundWorker.IsBusy)
			{
				backgroundWorker.CancelAsync();
				_cancellationTokenSource?.Cancel();

				// Cancel all active SQL commands immediately
				lock (_activeCommands)
				{
					foreach (var cmd in _activeCommands.ToList())
					{
						try
						{
							cmd?.Cancel();
						}
						catch { }
					}
				}
			}
		}

		private void tsbCriteriaCollapse_Click(object sender, EventArgs e)
		{
			spSearchDB.Panel1Collapsed = !spSearchDB.Panel1Collapsed;
			tsbCriteriaCollapse.Image
				= spSearchDB.Panel1Collapsed
				? Properties.Resources.NotCollapse
				: Properties.Resources.Collapse;
		}

		private void txtSearchKeyword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				runSearch();
			}
		}

		private async void chbRepoSearch_CheckedChanged(object sender, EventArgs e)
		{
			cbRepo.Enabled = txtRepoExtraSearchKeyword.Enabled = chbRepoSearch.Checked;
			if (chbRepoSearch.Checked && cbRepo.DataSource == null)
			{
				var repoBusiness = new RepoBusiness();
				var depotResponseResult = await repoBusiness.GetDepots().ConfigureAwait(true);
				cbRepo.DataSource = depotResponseResult.value;
				cbRepo.DisplayMember = "path";
				cbRepo.ValueMember = "path";
			}
		}

		private void chbDBSearch_CheckedChanged(object sender, EventArgs e)
		{
			ucDBObjectSelectControl.Enabled = chbDBSearch.Checked;
		}

		private void _ucObjectType_OnObjectTypeChanged(object sender, EventArgs e)
		{
			ucDBObjectSelectControl.SelectedObjectType = _ucObjectType.SelectedObjectType;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			fillObjectTypeList();
			scSearchResult.Panel2Collapsed = true;
			ucDBObjectSelectControl.MainForm
				= ucSqlNotePadControl.MainForm
				= MainForm;

			InitializeBackgroundWorker();
			txtSearchKeyword.BackColor = Constants.ComponentRequiredColor;
			ucDBObjectSelectControl.InitForm();

			dgvSearchResult.AutoGenerateColumns = false;
			dgvColumns.AutoGenerateColumns = false;
			
			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);
		}

		public override BaseScreenDataContract GetFormData()
		{
			DBObjectSelectScreenDataContract dbObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectControl.GetFormData();
			return new SearchDBScreenDataContract
			{
				Name = CommonResource.SearchDBObject,

				SearchKeyword = txtSearchKeyword.Text,
				CaseSensitive = chbCaseSensitive.Checked,
				DBSearch = chbDBSearch.Checked,
				RepoSearch = chbRepoSearch.Checked,
				RepoPath = cbRepo.SelectedValue?.ToString(),
				RepoExtraSearchKeyword = txtRepoExtraSearchKeyword.Text,
				ObjectTypeOriginal = (Int32?)_ucObjectType.SelectedObjectType?.Type,
				NameFilter = txtNameFilter.Text,

				DataSourceName = dbObjectSelectFormDataContract.DataSourceName,
				DBIndexes = dbObjectSelectFormDataContract.DBIndexes,
				SchemaId = dbObjectSelectFormDataContract.SchemaId,
				ObjectId = dbObjectSelectFormDataContract.ObjectId
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as SearchDBScreenDataContract;
			if (data == null)
				return;

			data.ObjectType = data.ObjectTypeOriginal;
			ucDBObjectSelectControl.SetFormData(data);

			txtSearchKeyword.Text = data.SearchKeyword;
			chbCaseSensitive.Checked = data.CaseSensitive;
			chbDBSearch.Checked = data.DBSearch;
			chbRepoSearch.Checked = data.RepoSearch;
			if (data.RepoPath != null)
				cbRepo.SelectedValue = data.RepoPath;
			txtRepoExtraSearchKeyword.Text = data.RepoExtraSearchKeyword;
			if (data.ObjectTypeOriginal.HasValue)
			{
				_ucObjectType.OnObjectTypeChanged -= _ucObjectType_OnObjectTypeChanged;
				_ucObjectType.SelectedObjectType = _ucObjectType.ObjectTypes.FirstOrDefault(o => o.Type == (ObjectTypeEnum?)data.ObjectTypeOriginal.Value);
				_ucObjectType.OnObjectTypeChanged += _ucObjectType_OnObjectTypeChanged;
			}
			txtNameFilter.Text = data.NameFilter;
		}
		#endregion

		#region BackgroundWorker Methods
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			SearchCriteriaContract searchCriteria = (SearchCriteriaContract)e.Argument;
			String searchKeyword = searchCriteria.SearchKeyword;
			Boolean caseSensitive = searchCriteria.CaseSensitive;

			DBObjectBusiness dbObjectBusiness = new DBObjectBusiness();
			List<DBObjectContract> dbObjectContractDefinitionList = new List<DBObjectContract>();
			List<DBObjectContract> dbObjectContractColumnsList = new List<DBObjectContract>();
			List<DBObjectContract> dbObjectContractRepoList = new List<DBObjectContract>();

			try
			{
				//if (this.InvokeRequired)
				//{
				//	this.Invoke(new MethodInvoker(() => this.Enabled = false));
				//}

				#region DB Search
				if (chbDBSearch.Checked)
				{
					#region Parameters
					String connectionString = String.Empty;
					String dbName = String.Empty;
					int? schemaId = null;
					String objectType = null;
					String nameFilter = null;

					if (ucDBObjectSelectControl.InvokeRequired)
					{
						ucDBObjectSelectControl.Invoke(new MethodInvoker(() =>
						{
							connectionString = ucDBObjectSelectControl.SelectedConnectionString.ConnectionString;
							dbName = ucDBObjectSelectControl.SelectedDB.Name;
							schemaId = ucDBObjectSelectControl.SelectedSchema?.SchemaId;
							objectType = _ucObjectType.SelectedObjectType?.Type.ToString();
							nameFilter = txtNameFilter.Text;
						}));
					}
					else
					{
						connectionString = ucDBObjectSelectControl.SelectedConnectionString.ConnectionString;
						dbName = ucDBObjectSelectControl.SelectedDB.Name;
						schemaId = ucDBObjectSelectControl.SelectedSchema?.SchemaId;
						objectType = _ucObjectType.SelectedObjectType?.Type.ToString();
						nameFilter = txtNameFilter.Text;
					}
					#endregion

					#region DB Object & Table Column Search
					Int32 progressMax = chbDBSearch.Checked ? 90 : 100;
					Int32 dbCount = ucDBObjectSelectControl.SelectedDBList.Count;
					for (Int32 i = 0; i < dbCount; i++)
					{
						// Check for cancellation
						if (backgroundWorker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}

						DBContract dbContract = ucDBObjectSelectControl.SelectedDBList[i];

						if (objectType == null || objectType != nameof(ObjectTypeEnum.U))
						{
							dbObjectContractDefinitionList.AddRange(dbObjectBusiness.SearchDBObject(
								connectionString,
								dbContract.Name,
								schemaId,
								objectType,
								searchKeyword,
								nameFilter,
								caseSensitive,
								_cancellationTokenSource.Token,
								cmd => {
									lock (_activeCommands) { _activeCommands.Add(cmd); }
								}));
						}

						backgroundWorker.ReportProgress((progressMax / (2 * dbCount)) * (2 * i + 1));

						// Check for cancellation again
						if (backgroundWorker.CancellationPending)
						{
							e.Cancel = true;
							return;
						}

						if (objectType == null || objectType == nameof(ObjectTypeEnum.U))
						{
							dbObjectContractColumnsList.AddRange(dbObjectBusiness.GetTableDBObjectListByDBSchemaId(
							connectionString,
							dbContract.Name,
							schemaId,
							searchKeyword,
							nameFilter,
							caseSensitive,
							_cancellationTokenSource.Token,
							cmd => {
								lock (_activeCommands) { _activeCommands.Add(cmd); }
							}));
						}

						backgroundWorker.ReportProgress((progressMax / (2 * dbCount)) * (2 * i + 2));
					}
					#endregion
				}
				#endregion

				#region Repo Object Search
				if (chbRepoSearch.Checked)
				{
					RepoBusiness repoBusiness = new RepoBusiness();
					
					// Depo Path
					String depotPath = null;
					if (cbRepo.InvokeRequired)
					{
						cbRepo.Invoke(new MethodInvoker(() => { depotPath = cbRepo.SelectedValue.ToString(); }));
					}
					else
					{
						depotPath = cbRepo.SelectedValue.ToString();
					}

					String repoSearchKeyword = String.IsNullOrWhiteSpace(txtRepoExtraSearchKeyword.Text) ? searchKeyword: searchKeyword + " " + txtRepoExtraSearchKeyword.Text;
					Task<RepoSearchResponse> repoSearchResponse = repoBusiness.GetRepoSearchResult(repoSearchKeyword, depotPath, 100);
					RepoSearchResponse repoSearchResponseResult = repoSearchResponse.Result;
					String repoTypeName = EnumHelper.GetEnumDescription(ObjectTypeEnum.REPO_FILE);
					repoSearchResponseResult.results.values.ForEach(repoSearchResultValue =>
					{
						dbObjectContractRepoList.Add(new DBObjectContract()
						{
							ObjectType = ObjectTypeEnum.REPO_FILE,
							ObjectTypeName = repoTypeName,
							DBName = depotPath,
							Name = repoSearchResultValue.path,
							Path = repoSearchResultValue.path,
							HitCount = repoSearchResultValue.hitCount
						});
					});
				}
				#endregion

				backgroundWorker.ReportProgress(100);
			}
			catch (SqlException ex)
			{
				// SqlCommand.Cancel() iptal exception'ı - normal davranış
				if (ex.Number == -2 || ex.Message.Contains("severe error") || ex.Message.Contains("current command"))
				{
					LogHelper.Info("Search cancelled by user.");
					e.Cancel = true;
					return;
				}

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
			catch (OperationCanceledException)
			{
				// CancellationToken'dan gelen iptal - normal davranış
				LogHelper.Info("Search cancelled by user.");
				e.Cancel = true;
				return;
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
			finally
			{
				//if (this.InvokeRequired)
				//{
				//	this.Invoke(new MethodInvoker(() => this.Enabled = true));
				//}
			}

			e.Result = new { DefinitionList = dbObjectContractDefinitionList, ColumnsList = dbObjectContractColumnsList, RepoList = dbObjectContractRepoList };
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => backgroundWorker_RunWorkerCompleted(sender, e)));
				return;
			}

			progressBar.Visible = false;

			// Reset button states
			tsbCancelSearch.Enabled = false;
			tsbSearchDB.Enabled = true;

			// Handle cancellation
			if (e.Cancelled)
			{
				lblSearchResultStatus.Text = NSqlTools.Types.Properties.CommonResource.SearchCancelled;
				return;
			}

			// Handle error
			if (e.Error != null)
			{
				lblSearchResultStatus.Text = NSqlTools.Types.Properties.CommonResource.SearchFailed + e.Error.Message;
				return;
			}

			var result = (dynamic)e.Result;
			var dbObjectContractDefinitionList = (List<DBObjectContract>)result.DefinitionList;
			var dbObjectContractColumnsList = (List<DBObjectContract>)result.ColumnsList;
			var dbObjectContractRepoList = (List<DBObjectContract>)result.RepoList;

			var data = dbObjectContractDefinitionList == null || dbObjectContractColumnsList == null
				? null
				: dbObjectContractDefinitionList.Concat(dbObjectContractColumnsList).Concat(dbObjectContractRepoList).ToList();
			BindColumnsGrid(null);
			BindSearchResultGrid(data);

			if (data == null || data.Count == 0)
				scSearchResult.Panel2Collapsed = true;

			lblSearchResultStatus.Text = String.Format(CommonResource.SearchKeywordFoundIn, dbObjectContractDefinitionList?.Count, dbObjectContractColumnsList?.Count, dbObjectContractRepoList.Count);
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => backgroundWorker_ProgressChanged(sender, e)));
				return;
			}

			progressBar.Value = e.ProgressPercentage;
		}
		#endregion

		#region Private Methods
		private Lexer fileExtensionToLexer(String ext)
		{
			ScintillaNET.Lexer lexer = Lexer.Cpp;
			switch (ext)
			{
				case ".SQL":
					lexer = ScintillaNET.Lexer.Sql;

					break;
				case ".CS":
				case ".CPP":
					lexer = ScintillaNET.Lexer.Cpp;

					break;
				case ".XAML":
				case ".XML":
					lexer = ScintillaNET.Lexer.Xml;

					break;
				case ".JSON":
					lexer = ScintillaNET.Lexer.Json;

					break;
				case ".CSS":
					lexer = ScintillaNET.Lexer.Css;

					break;
				case ".HTML":
					lexer = ScintillaNET.Lexer.Html;

					break;
				case ".BAT":
					lexer = ScintillaNET.Lexer.Batch;

					break;
				case ".VBS":
				case ".VBE":
				case ".VB":
					lexer = ScintillaNET.Lexer.Vb;

					break;
				case ".JS":
					lexer = ScintillaNET.Lexer.Asm;

					break;
				case ".PHP":
					lexer = ScintillaNET.Lexer.PhpScript;

					break;
				case ".PY":
					lexer = ScintillaNET.Lexer.Python;

					break;
				case ".JSX":
					lexer = ScintillaNET.Lexer.R;

					break;
				default:
					break;
			}

			return lexer;
		}
		#endregion
	}
}
