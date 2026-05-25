using NSqlTools.BusinessLayer.Business;
using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Contracts;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages.Screens
{
	public partial class ucTfsChangesetSearch : BaseUserControl
	{
		#region Properties
		private List<TFSChangesetContract> dataSource;
		public List<TFSChangesetContract> DataSource
		{
			get
			{
				return dataSource;
			}
			set
			{
				dataSource = value;
				dgvChangesets.BindList(value);

				lblStatus.Text = value != null
					? String.Format(NSqlTools.Types.Properties.CommonResource.ValueCountChangesetFound, value.Count)
					: null;
			}
		}

		private BackgroundWorker backgroundWorker;
		private BackgroundWorker fileChangesWorker;
		private CancellationTokenSource _cancellationTokenSource;

		private TfsBusiness tfsBusiness;

		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					txtTFSUrl,
					txtTFSPath,
					txtCommentFilter,
					cmbOwnerFilter,
					dtpStartDate,
					dtpEndDate,
					chkShowOnlyUnmergedToTest,
					chkShowOnlyUnmergedToMain,
					dgvChangesets
				};
			}
		}

		public frmNotePadCompareFullScreen _frmNotePadCompareFullScreen { get; set; }

		public enum BranchType
		{
			Dev = 1,
			Test = 2,
			Main = 3
		}

		public enum VersionType
		{
			Old = 1,
			New = 2
		}

		public class CompareOption
		{
			public BranchType Branch { get; set; }
			public VersionType Version { get; set; }
			public string DisplayName { get; set; }

			public override string ToString() => DisplayName;
		}

		private CompareOption SelectedSource => tscbSource?.SelectedItem as CompareOption;
		private CompareOption SelectedTarget => tscbTarget?.SelectedItem as CompareOption;

		private Dictionary<int, List<TFSFileChangeContract>> currentFileChangesMap;
		private TFSChangesetContract currentContract;
		#endregion

        #region Constructor
        public ucTfsChangesetSearch()
		{
			InitializeComponent();

			InitForm();
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			setTextFromResource();
			tfsBusiness = new TfsBusiness();

			dgvChangesets.AutoGenerateColumns = false;
			dgvChangesets.SelectionChanged += dgvChangesets_SelectionChanged;

			tvFileChanges.NodeMouseClick += tvFileChanges_NodeMouseClick;
			tvFileChanges.NodeMouseDoubleClick += tvFileChanges_NodeMouseDoubleClick;
			tvFileChanges.BeforeSelect += tvFileChanges_BeforeSelect;

			this.txtTFSUrl.Text = ConfigurationManager.AppSettings["TFSUrl"].ToString();
			this.txtTFSPath.Text = ConfigurationManager.AppSettings["TFSPath"].ToString();

			InitializeCompareComboBoxes(); // Yeni metod

			cleanFilter();
		}

		private void InitializeCompareComboBoxes()
		{
			var compareOptions = new List<CompareOption>
			{
				new CompareOption { Branch = BranchType.Dev, Version = VersionType.Old, DisplayName = NSqlTools.Types.Properties.CommonResource.DevOld },
				new CompareOption { Branch = BranchType.Dev, Version = VersionType.New, DisplayName = NSqlTools.Types.Properties.CommonResource.DevNew },
				new CompareOption { Branch = BranchType.Test, Version = VersionType.Old, DisplayName = NSqlTools.Types.Properties.CommonResource.TestOld },
				new CompareOption { Branch = BranchType.Test, Version = VersionType.New, DisplayName = NSqlTools.Types.Properties.CommonResource.TestNew },
				new CompareOption { Branch = BranchType.Main, Version = VersionType.Old, DisplayName = NSqlTools.Types.Properties.CommonResource.MainOld },
				new CompareOption { Branch = BranchType.Main, Version = VersionType.New, DisplayName = NSqlTools.Types.Properties.CommonResource.MainNew }
			};

			// Önce DataSource'ları ayarla
			tscbSource.ComboBox.DataSource = new List<CompareOption>(compareOptions);
			tscbSource.ComboBox.DisplayMember = "DisplayName";
			tscbSource.SelectedIndex = 0; // Dev - Old

			tscbTarget.ComboBox.DataSource = new List<CompareOption>(compareOptions);
			tscbTarget.ComboBox.DisplayMember = "DisplayName";
			tscbTarget.SelectedIndex = 1; // Dev - New

			// Event handler'ları EN SON ekle (tüm başlatma işlemleri bittikten sonra)
			tscbSource.SelectedIndexChanged += CompareComboBox_SelectedIndexChanged;
			tscbTarget.SelectedIndexChanged += CompareComboBox_SelectedIndexChanged;
		}

		private void CompareComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Eğer henüz seçili bir changeset yoksa veya combobox'lar hazır değilse işlem yapma
			if (dgvChangesets == null || dgvChangesets.SelectedRows.Count == 0)
				return;

			// TreeView'ı yeniden yükle
			dgvChangesets_SelectionChanged(dgvChangesets, EventArgs.Empty);
		}

		public override BaseScreenDataContract GetFormData()
		{
			return new TfsChangesetSearchScreenDataContract
			{
				Name = NSqlTools.Types.Properties.CommonResource.TFS,

				TFSUrl = txtTFSUrl.Text,
				TFSPath = txtTFSPath.Text,
				CommentFilter = txtCommentFilter.Text,
				ChangesetId = int.TryParse(txtChangesetId.Text, out int csId) ? csId : (int?)null,
				OwnerFilter = cmbOwnerFilter.SelectedIndex > 0 ? cmbOwnerFilter.Text : null,
				ShowOnlyUnmergedToTest = chkShowOnlyUnmergedToTest.Checked,
				ShowOnlyUnmergedToMain = chkShowOnlyUnmergedToMain.Checked,
				StartDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null,
				EndDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var formData = formDataBaseContract as TfsChangesetSearchScreenDataContract;
			if (formData == null)
				return;

			txtTFSUrl.Text = formData.TFSUrl ?? string.Empty;
			txtTFSPath.Text = formData.TFSPath ?? string.Empty;
			txtCommentFilter.Text = formData.CommentFilter ?? string.Empty;
			txtChangesetId.Text = formData.ChangesetId.HasValue ? formData.ChangesetId.Value.ToString() : string.Empty;

			if (!string.IsNullOrEmpty(formData.OwnerFilter) && cmbOwnerFilter.Items.Contains(formData.OwnerFilter))
			{
				cmbOwnerFilter.SelectedItem = formData.OwnerFilter;
			}

			chkShowOnlyUnmergedToTest.Checked = formData.ShowOnlyUnmergedToTest;
			chkShowOnlyUnmergedToMain.Checked = formData.ShowOnlyUnmergedToMain;

			if (formData.StartDate.HasValue)
			{
				dtpStartDate.Checked = true;
				dtpStartDate.Value = formData.StartDate.Value;
			}

			if (formData.EndDate.HasValue)
			{
				dtpEndDate.Checked = true;
				dtpEndDate.Value = formData.EndDate.Value;
			}
		}
		#endregion

		#region Events
		private void tsbStartTFSSearch_Click(object sender, EventArgs e)
		{
			if (!isCriteriaValid())
				return;

			SearchChangesets();
		}

		private void tsbRefreshUsers_Click(object sender, EventArgs e)
		{
			if (!isCriteriaValid(true))
				return;

			LoadOwners();
		}

		private void tsbCriteriaCollapse_Click(object sender, EventArgs e)
		{
			scTFSMain.Panel1Collapsed = !scTFSMain.Panel1Collapsed;
			tsbCriteriaCollapse.Image
				= scTFSMain.Panel1Collapsed
				? Resources.NotCollapse
				: Resources.Collapse;
		}

		private void dgvChangesets_SelectionChanged(object sender, EventArgs e)
		{
			if (dgvChangesets.SelectedRows.Count == 0)
			{
				scTFSResult.Panel2Collapsed = true;
				return;
			}

			var row = dgvChangesets.SelectedRows[0];
			if (row.DataBoundItem is TFSChangesetContract contract)
			{
				// Sadece SOURCE branch'in changeset ID'sini yükle
				if (SelectedSource != null)
				{
					var sourceChangesetId = GetChangesetId(contract, SelectedSource.Branch);
					if (sourceChangesetId.HasValue)
					{
						LoadFileChangesAsync(contract, sourceChangesetId.Value);
					}
				}
			}
		}

		private int? GetChangesetId(TFSChangesetContract contract, BranchType branch)
		{
			switch (branch)
			{
				case BranchType.Dev:
					return contract.ChangesetId;
				case BranchType.Test:
					return contract.TestChangesetId;
				case BranchType.Main:
					return contract.MainChangesetId;
				default:
					return null;
			}
		}

		private void tsbExportToExcel_Click(object sender, EventArgs e)
		{
			frmMain frm = (frmMain)MainForm;
			frm.saveFileDialog.Filter = CommonResource.ExcelFiles;
			if (frm.saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Boolean exportResult = ExcelHelper.ExportDataGridViewToExcel(dgvChangesets, frm.saveFileDialog.FileName, NSqlTools.Types.Properties.CommonResource.TFSSearchResults);
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

		private void tsbCleanFilter_Click_1(object sender, EventArgs e)
		{
			cleanFilter();
		}

		private void tsbCancelSearch_Click(object sender, EventArgs e)
		{
			if (backgroundWorker != null && backgroundWorker.IsBusy)
			{
				backgroundWorker.CancelAsync();
				_cancellationTokenSource?.Cancel();
			}
		}
		#endregion

		#region Private Methods
		private void cleanFilter()
		{
			txtCommentFilter.Clear();
			txtChangesetId.Clear();
			dtpStartDate.Value = DateTime.Now.AddMonths(-1);
			dtpStartDate.Checked = false;
			dtpEndDate.Value = DateTime.Now;
			dtpEndDate.Checked = false;
			cmbOwnerFilter.SelectedValue = null;
			chkShowOnlyUnmergedToTest.Checked = false;
			chkShowOnlyUnmergedToMain.Checked = false;
			DataSource = null;
			tvFileChanges.Nodes.Clear();
			if (_frmNotePadCompareFullScreen != null)
			{
				_frmNotePadCompareFullScreen.Close();
				_frmNotePadCompareFullScreen = null;
			}
			scTFSResult.Panel2Collapsed = true;
		}

		private void setTextFromResource()
		{
			this.tsbCriteriaCollapse.Text = NSqlTools.Types.Properties.CommonResource.CollapseCriteriaPanel;
			this.tsbStartTFSSearch.Text = NSqlTools.Types.Properties.CommonResource.Ara;
			this.tsbRefreshUsers.Text = NSqlTools.Types.Properties.CommonResource.RefreshUsers;
			this.tsbExportToExcel.Text = NSqlTools.Types.Properties.CommonResource.ExportSearchResultToExcel;
			this.gbTFSSettings.Text = NSqlTools.Types.Properties.CommonResource.TFSSettings;
			this.lblTFSPath.Text = NSqlTools.Types.Properties.CommonResource.TFSPath;
			this.lblTFSUrl.Text = NSqlTools.Types.Properties.CommonResource.TFSURL;
			this.colChangesetId.HeaderText = NSqlTools.Types.Properties.CommonResource.ChangesetID;
			this.colComment.HeaderText = NSqlTools.Types.Properties.CommonResource.Comment;
			this.colOwner.HeaderText = NSqlTools.Types.Properties.CommonResource.DevCheckinUser;
			this.colCreationDate.HeaderText = NSqlTools.Types.Properties.CommonResource.CreateDate;
			this.colBranch.HeaderText = NSqlTools.Types.Properties.CommonResource.Branch;
			this.colMergedToTest.HeaderText = NSqlTools.Types.Properties.CommonResource.MergedToTest;
			this.colMergedToMain.HeaderText = NSqlTools.Types.Properties.CommonResource.MergedToMain;
			this.gbFilter.Text = NSqlTools.Types.Properties.CommonResource.Filter;
			this.lblOwnerFilter.Text = NSqlTools.Types.Properties.CommonResource.Owner;
			this.lblCommentFilter.Text = NSqlTools.Types.Properties.CommonResource.ChangesetComment;
			this.lblChangesetId.Text = NSqlTools.Types.Properties.CommonResource.ChangesetID;
			this.chkShowOnlyUnmergedToMain.Text = NSqlTools.Types.Properties.CommonResource.ShowOnlyUnmergedToMain;
			this.chkShowOnlyUnmergedToTest.Text = NSqlTools.Types.Properties.CommonResource.ShowOnlyUnmergedToTest;
			this.lblEndDate.Text = NSqlTools.Types.Properties.CommonResource.EndDate;
			this.lblStartDate.Text = NSqlTools.Types.Properties.CommonResource.StartDate;
			this.tsbCleanFilter.Text = NSqlTools.Types.Properties.CommonResource.Clean;
			this.colTestMergeUser.HeaderText = NSqlTools.Types.Properties.CommonResource.TestMergeUser;
			this.colMainMergeUser.HeaderText = NSqlTools.Types.Properties.CommonResource.MainMergeUser;
			this.colTestMergeDate.HeaderText = NSqlTools.Types.Properties.CommonResource.TestMergeDate;
			this.colMainMergeDate.HeaderText = NSqlTools.Types.Properties.CommonResource.MainMergeDate;
			tslSource.Text = "Source";
            tslTarget.Text = "Target";
        }

        private Boolean isCriteriaValid(Boolean onlyUrlAndPath = false)
		{
			Boolean isValid = true;

				if (!UIHelper.ComponentIsValidString((MainForm as frmMain)?.errorProvider, txtTFSUrl.Text, lblTFSUrl, NSqlTools.Types.Properties.CommonResource.FillTFSUrl))
					isValid = false;

				if (!UIHelper.ComponentIsValidString((MainForm as frmMain)?.errorProvider, txtTFSPath.Text, lblTFSPath, NSqlTools.Types.Properties.CommonResource.PleaseFillTFSPathExProductAndDeliveryDestek))
					isValid = false;

					return isValid;
				}

				private void LoadFileChangesAsync(TFSChangesetContract contract, int changesetId)
				{
					if (fileChangesWorker != null && fileChangesWorker.IsBusy)
						return;

					currentContract = contract;
					lblStatus.Text = String.Format(NSqlTools.Types.Properties.CommonResource.LoadingFileChangesForChangeset0, changesetId);
					dgvChangesets.Enabled = false;

					var tfsUrl = txtTFSUrl.Text;

					fileChangesWorker = new BackgroundWorker();
					fileChangesWorker.DoWork += (s, ev) =>
					{
						try
						{
							ev.Result = tfsBusiness.GetFileChanges(tfsUrl, changesetId);
						}
						catch (Exception ex)
						{
							ev.Result = ex;
							LogHelper.Error(ex);
						}
					};
					fileChangesWorker.RunWorkerCompleted += (s, ev) =>
					{
						dgvChangesets.Enabled = true;

						if (ev.Result is Exception ex)
						{
							LogHelper.Error(ex);
							scTFSResult.Panel2Collapsed = true;
							lblStatus.Text = NSqlTools.Types.Properties.CommonResource.ErrorOccured;
						}
						else if (ev.Result is List<TFSFileChangeContract> fileChanges)
						{
							currentFileChangesMap = new Dictionary<int, List<TFSFileChangeContract>>
							{
								{ changesetId, fileChanges }
							};
							PopulateFileChangesTree(fileChanges);
							scTFSResult.Panel2Collapsed = false;
							lblStatus.Text = String.Format(NSqlTools.Types.Properties.CommonResource._0FileChangesFound, fileChanges?.Count ?? 0);
						}

						fileChangesWorker?.Dispose();
						fileChangesWorker = null;
					};
					fileChangesWorker.RunWorkerAsync();
				}

				private void PopulateFileChangesTree(List<TFSFileChangeContract> fileChanges)
				{
					tvFileChanges.ShowNodeToolTips = true;
					tvFileChanges.BeginUpdate();
					tvFileChanges.Nodes.Clear();

					var folderNodes = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);

					if (fileChanges != null)
					{
						foreach (var fileChange in fileChanges)
						{
							string serverPath = fileChange.ServerPath ?? fileChange.FileName;
							string folderPath = Path.GetDirectoryName(serverPath)?.Replace('\\', '/') ?? string.Empty;
							string fileName = Path.GetFileName(serverPath);

							TreeNode parentNode = GetOrCreateFolderNode(folderNodes, folderPath);

							string nodeText = string.IsNullOrEmpty(fileChange.ChangeType)
								? fileName
								: $"{fileName} [{fileChange.ChangeType}]";

							var fileNode = new TreeNode(nodeText)
							{
								Tag = serverPath,
								ToolTipText = NSqlTools.Types.Properties.CommonResource.ToViewTheChangesDoubleClickOrLeftClickIfTheWindowIsAlreadyOpen
							};

							parentNode.Nodes.Add(fileNode);
						}
					}

						tvFileChanges.ExpandAll();
						tvFileChanges.EndUpdate();
					}

					private TreeNode GetOrCreateFolderNode(Dictionary<string, TreeNode> folderNodes, string folderPath)
		{
			if (string.IsNullOrEmpty(folderPath))
			{
				const string rootKey = "";
				if (!folderNodes.TryGetValue(rootKey, out TreeNode rootNode))
				{
					rootNode = new TreeNode("/");
					tvFileChanges.Nodes.Add(rootNode);
					folderNodes[rootKey] = rootNode;
				}
				return rootNode;
			}

			if (folderNodes.TryGetValue(folderPath, out TreeNode existing))
				return existing;

			string parentPath = Path.GetDirectoryName(folderPath)?.Replace('\\', '/') ?? string.Empty;
			string folderName = Path.GetFileName(folderPath);

			if (string.IsNullOrEmpty(folderName))
				folderName = folderPath;

			var node = new TreeNode(folderName);

			if (string.IsNullOrEmpty(parentPath) || parentPath == folderPath)
			{
				tvFileChanges.Nodes.Add(node);
			}
			else
			{
				TreeNode parentNode = GetOrCreateFolderNode(folderNodes, parentPath);
				parentNode.Nodes.Add(node);
			}

			folderNodes[folderPath] = node;
			return node;
		}

		private void tvFileChanges_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			// Only allow selecting file nodes, not folder nodes
			if (e.Node.Tag == null)
				e.Cancel = true;
		}

		private void tvFileChanges_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag is string serverPath)
			{
				tvFileChanges.SelectedNode = e.Node;

				if (_frmNotePadCompareFullScreen != null && _frmNotePadCompareFullScreen.Visible)
				{
					LoadAndCompareSelectedVersions(serverPath);
				}
			}
		}

		private void tvFileChanges_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag is string serverPath)
			{
				LoadAndCompareSelectedVersions(serverPath);
			}
		}

		private void LoadAndCompareSelectedVersions(string serverPath)
		{
			if (SelectedSource == null || SelectedTarget == null)
			{
				MessageBox.Show(NSqlTools.Types.Properties.CommonResource.LütfenSourceVeTargetSeçiniz, NSqlTools.Types.Properties.CommonResource.Uyarı, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (currentContract == null || currentFileChangesMap == null)
			{
				MessageBox.Show(NSqlTools.Types.Properties.CommonResource.LütfenÖnceBirChangesetSeçiniz, NSqlTools.Types.Properties.CommonResource.Uyarı, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			lblStatus.Text = String.Format(NSqlTools.Types.Properties.CommonResource.LoadingFileContent0, Path.GetFileName(serverPath));
			Cursor = Cursors.WaitCursor;
			tvFileChanges.Enabled = false;

			var tfsUrl = txtTFSUrl.Text;
			var sourceOption = SelectedSource;
			var targetOption = SelectedTarget;

			var contentWorker = new BackgroundWorker();
			contentWorker.DoWork += (s, ev) =>
			{
				try
				{
					var sourceContent = GetContentForOption(tfsUrl, serverPath, sourceOption);
					var targetContent = GetContentForOption(tfsUrl, serverPath, targetOption);

					ev.Result = new Tuple<string, string>(sourceContent, targetContent);
				}
				catch (Exception ex)
				{
					ev.Result = ex;
					LogHelper.Error(ex);
				}
			};
			contentWorker.RunWorkerCompleted += (s, ev) =>
			{
				Cursor = Cursors.Default;
				tvFileChanges.Enabled = true;

				if (ev.Result is Exception ex)
				{
					lblStatus.Text = NSqlTools.Types.Properties.CommonResource.ErrorOccured;
					MessageBox.Show(ex.Message, NSqlTools.Types.Properties.CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (ev.Result is Tuple<string, string> contents)
				{
					ShowCompareForm(serverPath, contents.Item1, contents.Item2);
				}

				contentWorker?.Dispose();
			};
			contentWorker.RunWorkerAsync();
		}

		private string GetContentForOption(string tfsUrl, string serverPath, CompareOption option)
		{
			var contract = currentContract;
			var fileChangesMap = currentFileChangesMap;

			if (contract == null || fileChangesMap == null)
				return string.Empty;

			var changesetId = GetChangesetId(contract, option.Branch);
			if (!changesetId.HasValue)
				return string.Empty;

			// Eğer bu changeset için dosya değişiklikleri yüklenmemişse, yükle
			if (!fileChangesMap.ContainsKey(changesetId.Value))
			{
				try
				{
					var fileChanges = tfsBusiness.GetFileChanges(tfsUrl, changesetId.Value);
					fileChangesMap[changesetId.Value] = fileChanges;
				}
				catch (Exception ex)
				{
					LogHelper.Error(ex);
					return string.Empty;
				}
			}

			var fileChangesList = fileChangesMap[changesetId.Value];
			var fileChange = fileChangesList?.FirstOrDefault(fc => 
				(fc.ServerPath ?? fc.FileName).Equals(serverPath, StringComparison.OrdinalIgnoreCase));

			// Eğer bu changeset'te dosya değişmemişse, direkt TFS'ten o changeset'teki halini al
			if (fileChange == null)
			{
				try
				{
					bool isOldVersion = option.Version == VersionType.Old;
					return tfsBusiness.GetFileContentAtChangeset(tfsUrl, serverPath, changesetId.Value, isOldVersion);
				}
				catch (Exception ex)
				{
					LogHelper.Error(ex);
					return string.Empty;
				}
			}

			// İçerik henüz yüklenmediyse yükle
			if (fileChange.OldContent == null && fileChange.NewContent == null)
			{
				tfsBusiness.LoadFileContent(tfsUrl, fileChange);
			}

			return option.Version == VersionType.Old ? fileChange.OldContent : fileChange.NewContent;
		}

		private void ShowCompareForm(string serverPath, string sourceContent, string targetContent)
		{
			String fileName = $"{Path.GetFileName(serverPath)} ({SelectedSource.DisplayName} vs {SelectedTarget.DisplayName})";
			
			if (_frmNotePadCompareFullScreen == null || !_frmNotePadCompareFullScreen.Visible)
			{
				_frmNotePadCompareFullScreen = new frmNotePadCompareFullScreen(
					fileName,
					sourceContent,
					null,
					null,
					targetContent,
					null,
					null,
					ScintillaNET.Lexer.Cpp)
				{
					WindowState = FormWindowState.Maximized
				};
			}
			else
			{
				_frmNotePadCompareFullScreen.InitForm(fileName,
					sourceContent,
					null,
					null,
					targetContent,
					null,
					null,
					ScintillaNET.Lexer.Cpp);
			}
			_frmNotePadCompareFullScreen.Show();

			lblStatus.Text = String.Format(NSqlTools.Types.Properties.CommonResource.DisplayingFile0, fileName);
		}
		#endregion

		#region Search Worker Methods
		private void SearchChangesets()
		{
			if (backgroundWorker != null && backgroundWorker.IsBusy)
			{
				MessageBox.Show(NSqlTools.Types.Properties.CommonResource.ASearchingOperationIsRunningPleaseWait, NSqlTools.Types.Properties.CommonResource.Information,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			tsbStartTFSSearch.Enabled = false;
			tsbCancelSearch.Enabled = true;
			lblStatus.Text = NSqlTools.Types.Properties.CommonResource.Searching;
			DataSource = null;

			// Create new cancellation token source
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = new CancellationTokenSource();

			// Collect UI values BEFORE starting background thread to avoid cross-thread issues
			var parameters = new SearchParameters
			{
				TfsUrl = txtTFSUrl.Text,
				BasePath = txtTFSPath.Text,
				CommentFilter = txtCommentFilter.Text,
				ChangesetId = int.TryParse(txtChangesetId.Text, out int csId) ? csId : (int?)null,
				OwnerFilter = cmbOwnerFilter.SelectedIndex > 0 ? cmbOwnerFilter.SelectedValue.ToString() : null,
				StartDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null,
				EndDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null,
				ShowOnlyUnmergedToTest = chkShowOnlyUnmergedToTest.Checked,
				ShowOnlyUnmergedToMain = chkShowOnlyUnmergedToMain.Checked
			};

			backgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
			backgroundWorker.DoWork += BackgroundWorker_Search_DoWork;
			backgroundWorker.RunWorkerCompleted += BackgroundWorker_Search_RunWorkerCompleted;
			backgroundWorker.RunWorkerAsync(parameters);
		}

		private void BackgroundWorker_Search_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				//if (this.InvokeRequired)
				//{
				//	this.Invoke(new MethodInvoker(() => this.Enabled = false));
				//}

				// Check for cancellation
				if (backgroundWorker.CancellationPending)
				{
					e.Cancel = true;
					return;
				}

				var parameters = (SearchParameters)e.Argument;

				var results = tfsBusiness.SearchChangesets(
					parameters.TfsUrl,
					parameters.BasePath,
					parameters.CommentFilter,
					parameters.OwnerFilter,
					parameters.StartDate,
					parameters.EndDate,
					parameters.ShowOnlyUnmergedToTest,
					parameters.ShowOnlyUnmergedToMain,
					parameters.ChangesetId,
					_cancellationTokenSource.Token);

				e.Result = results;
			}
			catch (OperationCanceledException)
			{
				e.Cancel = true;
			}
			catch (Exception ex)
			{
				e.Result = ex;

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
		}

		private void BackgroundWorker_Search_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			tsbStartTFSSearch.Enabled = true;
			tsbCancelSearch.Enabled = false;

			// Handle cancellation
			if (e.Cancelled)
			{
				lblStatus.Text = NSqlTools.Types.Properties.CommonResource.SearchCancelled;
				backgroundWorker?.Dispose();
				backgroundWorker = null;
				return;
			}

			if (e.Result is Exception ex)
			{
				lblStatus.Text = NSqlTools.Types.Properties.CommonResource.ErrorOccured;
				MessageBox.Show(String.Format(NSqlTools.Types.Properties.CommonResource.ErrorOccuredDetail0, ex.Message), NSqlTools.Types.Properties.CommonResource.Error,
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (e.Result is List<TFSChangesetContract> results)
			{
				DataSource = results;
			}

			backgroundWorker?.Dispose();
			backgroundWorker = null;
		}
		#endregion

		#region Owners Worker Methods
		private void LoadOwners()
		{
			if (backgroundWorker != null && backgroundWorker.IsBusy)
			{
				MessageBox.Show(NSqlTools.Types.Properties.CommonResource.AOperationIsRunningPleaseWait, NSqlTools.Types.Properties.CommonResource.Information,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			tsbRefreshUsers.Enabled = false;
			tsbCancelSearch.Enabled = true;
			lblStatus.Text = NSqlTools.Types.Properties.CommonResource.UsersAreLoading;

			// Create new cancellation token source
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = new CancellationTokenSource();

			// Collect UI values BEFORE starting background thread to avoid cross-thread issues
			var parameters = new OwnerParameters
			{
				TfsUrl = txtTFSUrl.Text,
				BasePath = txtTFSPath.Text,
				StartDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null,
				EndDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null
			};

			backgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
			backgroundWorker.DoWork += BackgroundWorker_LoadOwners_DoWork;
			backgroundWorker.RunWorkerCompleted += BackgroundWorker_LoadOwners_RunWorkerCompleted;
			backgroundWorker.RunWorkerAsync(parameters);
		}

		private void BackgroundWorker_LoadOwners_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				// Check for cancellation
				if (backgroundWorker.CancellationPending)
				{
					e.Cancel = true;
					return;
				}

				var parameters = (OwnerParameters)e.Argument;

				var owners = tfsBusiness.GetDistinctOwners(
					parameters.TfsUrl,
					parameters.BasePath,
					parameters.StartDate,
					parameters.EndDate,
					_cancellationTokenSource.Token);

				e.Result = owners;
			}
			catch (OperationCanceledException)
			{
				e.Cancel = true;
			}
			catch (Exception ex)
			{
				e.Result = ex;

				LogHelper.Error(ex);
			}
		}

		private void BackgroundWorker_LoadOwners_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			tsbRefreshUsers.Enabled = true;
			tsbCancelSearch.Enabled = false;

			// Handle cancellation
			if (e.Cancelled)
			{
				lblStatus.Text = NSqlTools.Types.Properties.CommonResource.OperationCancelled;
				backgroundWorker?.Dispose();
				backgroundWorker = null;
				return;
			}

			if (e.Result is Exception ex)
			{
				lblStatus.Text = NSqlTools.Types.Properties.CommonResource.ErrorOccured;
				MessageBox.Show(String.Format(NSqlTools.Types.Properties.CommonResource.ErrorOccuredErrorDetail, ex.Message), NSqlTools.Types.Properties.CommonResource.Error,
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (e.Result is List<TFSUser> owners)
			{
				cmbOwnerFilter.ValueMember = nameof(TFSUser.UserName);
				cmbOwnerFilter.DisplayMember = nameof(TFSUser.DisplayName);
				cmbOwnerFilter.DataSource = owners;
				cmbOwnerFilter.SelectedIndex = -1;

				lblStatus.Text = String.Format(NSqlTools.Types.Properties.CommonResource._0UsersAreFound, owners.Count);
			}

			backgroundWorker?.Dispose();
			backgroundWorker = null;
		}
        #endregion


    }
}
