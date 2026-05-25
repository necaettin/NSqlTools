using NSqlTools.BusinessLayer;
using NSqlTools.BusinessLayer.Cache;
using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.IntellisenseContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Pages.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	public partial class frmMain : Form
	{
		#region Properties
		private int draggedTabIndex = -1; // Sürüklenen sekmenin indeksi

		public List<Control> TabSequence;

		private const String ToolsToolbar = "ToolsToolbar";

		private const String OptionsToolbar = "OptionsToolbar";
		#endregion

		#region Constructor
		public frmMain()
		{
			try
			{
				LogHelper.Info("frmMain initializing...");
				
				InitializeComponent();
				setTextFromResource();
				initForm();

				LogHelper.Info("frmMain initialized successfully");
			}
			catch (Exception ex)
			{
				LogHelper.Error("frmMain initialization failed", ex);
				throw;
			}
		}

		#endregion

		#region Events
		private void SqlViewer_Click(object sender, EventArgs e)
		{
			showUserControl<ucSqlViewer>(CommonResource.SqlViewer, "SqlViewer");
		}

		private void SqlCompare_Click(object sender, EventArgs e)
		{
			showUserControl<ucDBObjectCompare>(CommonResource.SqlCompare, "SqlCompare");
		}

		private void BatchCompare_Click(object sender, EventArgs e)
		{
			showUserControl<ucDBBatchCompare>(CommonResource.SqlBatchCompare, "BatchCompare");
		}

		private void InsertScriptGenerator_Click(object sender, EventArgs e)
		{
			showUserControl<ucInsertScriptGenerator>(CommonResource.InsertScriptGenerator, "InsertScriptGenerator");
		}

		private void DBSearch_Click(object sender, EventArgs e)
		{
			showUserControl<ucSearchDB>(CommonResource.SearchDBObject, "SearchDBObject");
		}

		private void RunQuery_Click(object sender, EventArgs e)
		{
			showUserControl<ucRunQuery>(CommonResource.RunQuery, "RunSqlQuery");
		}

		private void tsbDataSource_Click(object sender, EventArgs e)
		{
			showUserControl<ucConnectionStrings>(CommonResource.ConnectionStrings, "ConnectionStrings", false);
		}

		private void tsbTextViewer_Click(object sender, EventArgs e)
		{
			showUserControl<ucTextViewer>(CommonResource.TextViewer, "TextViewer");
		}

		private void tsbFreeTextCompare_Click(object sender, EventArgs e)
		{
			showUserControl<ucFreeTextCompare>(CommonResource.FreeTextCompare, "FreeCompare");
		}

		private void tsmiTableToCSV_Click(object sender, EventArgs e)
		{
			showUserControl<ucTableToCSV>(CommonResource.TableToCSV, "TableToCSV");
		}

		private void tsmiFavoriteQueries_Click(object sender, EventArgs e)
		{
			showUserControl<ucFavoriteQueries>(CommonResource.FavoriteQueries, "FavoriteQueries");
		}

		private void tsmiProjects_Click(object sender, EventArgs e)
		{
			showUserControl<ucProjects>(CommonResource.Projects, "Projects");
		}

		private void tsmiDataCompare_Click(object sender, EventArgs e)
		{
			showUserControl<ucDataCompare>(CommonResource.DataCompare, "DataCompare");
		}

		private void tsmiSnippets_Click(object sender, EventArgs e)
		{
			showUserControl<ucSnippet>(CommonResource.Snippets, "Snippet");
		}

		private void tsbTFSSearch_Click(object sender, EventArgs e)
		{
			showUserControl<ucTfsChangesetSearch>(CommonResource.TFSSearch, "TFSSearch");
		}

		private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tsOptions.Visible = toolBarToolStripMenuItem.Checked;
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmAboutBox frm = new frmAboutBox();
			frm.ShowDialog();
		}

		private void tsmiHelp_Click(object sender, EventArgs e)
		{
			frmHelp frm = new frmHelp();
			frm.ShowDialog();
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyValue == (Int32)Keys.F1)
				tsmiHelp_Click(this, EventArgs.Empty);
		}

		private void tsmiClearCache_Click(object sender, EventArgs e)
		{
			try
			{
				MemoryCacheHelper.Clear();

                TableMetadataCache.Clear();

				UIHelper.DbCacheInfo = new List<IntellisenseDatabaseContract>();

				MessageBox.Show(CommonResource.CacheCleared);
			}
			catch(Exception ex)
			{
				UIHelper.ShowException(ex);
			}
		}

		private void tsmiTurkish_Click(object sender, EventArgs e)
		{
			tsmiEnglish.Checked = false;
			changeCulture("tr-TR");
		}

		private void tsmiEnglish_Click(object sender, EventArgs e)
		{
			tsmiTurkish.Checked = false;
			changeCulture("en-US");
		}

		private void tsmiMultiRowTabs_Click(object sender, EventArgs e)
		{
			tcPages.Multiline = tsmiMultiRowTabs.Checked;
		}

		private void tsmiChangeLog_Click(object sender, EventArgs e)
		{
			Process p = new Process();
			p.StartInfo.FileName = Constants.ChangeLogFileName;
			p.Start();
		}

		private void tsmiLogFiles_Click(object sender, EventArgs e)
		{
			Process p = new Process();
			p.StartInfo.FileName = Constants.LogsFolder;
			p.Start();
		}

		private void tsmiShowToolsToolbar_Click(object sender, EventArgs e)
		{
			tsTools.Visible = tsmiShowToolsToolbar.Checked;
		}

		private void tsmiShowOptionsToolbar_Click(object sender, EventArgs e)
		{
			tsOptions.Visible = tsmiShowOptionsToolbar.Checked;
		}

		private void tsmiEncryptDecrypt_Click(object sender, EventArgs e)
		{
			frmEncryption frm = new frmEncryption();
			frm.ShowDialog();
		}
		#endregion

		#region Tab Control Events
		private void tcPages_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Middle)
			{
				TabPage clickedTab = getClickedTab(e.Location);
				if(clickedTab != null)
				{
					tcPages.TabPages.Remove(clickedTab);
				}
			}
			else if (e.Button == MouseButtons.Left)
			{
				// Tıklanan sekmenin indeksini bul
				for (int i = 0; i < tcPages.TabPages.Count; i++)
				{
					if (tcPages.GetTabRect(i).Contains(e.Location))
					{
						draggedTabIndex = i; // Sürüklenen sekme indeksi
						break;
					}
				}
			}
		}

		private void tcPages_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && draggedTabIndex >= 0 && draggedTabIndex <= tcPages.TabPages.Count - 1)
			{
				// Sürükleme başlat
				tcPages.DoDragDrop(tcPages.TabPages[draggedTabIndex], DragDropEffects.Move);
			}
		}

		private void tcPages_DragOver(object sender, DragEventArgs e)
		{
			// Sürükleme sırasında efekt göstermek
			e.Effect = DragDropEffects.Move;
		}

		private void tcPages_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				// Hedef noktayı al
				Point point = tcPages.PointToClient(new Point(e.X, e.Y));
				int targetIndex = -1;

				// Hedef indeksini belirle
				for (int i = 0; i < tcPages.TabPages.Count; i++)
				{
					if (tcPages.GetTabRect(i).Contains(point))
					{
						targetIndex = i;
						break;
					}
				}

				// Geçerli kaynak ve hedef indeksler kontrolü
				if (draggedTabIndex >= 0 && targetIndex >= 0 && draggedTabIndex != targetIndex)
				{
					if (tcPages.TabPages.Count == 2)
						SwapTabsFor2Tabs(draggedTabIndex, targetIndex);
					else
						SwapTabs(draggedTabIndex, targetIndex);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				// Hata detaylarını göster
				MessageBox.Show(String.Format(CommonResource.TabDragError, ex.Message, draggedTabIndex));
			}
			finally
			{
				// İşlem sıfırlama
				draggedTabIndex = -1;
			}
		}

		private void SwapTabsFor2Tabs(int sourceIndex, int targetIndex)
		{
			// Kaynak sekmeyi al
			var sourceTab = tcPages.TabPages[sourceIndex];
			var targetTab = tcPages.TabPages[targetIndex];

			// Kaynak sekmeyi kaldır
			tcPages.TabPages.Clear();

			// Hedef pozisyona ekle
			if (sourceIndex > targetIndex)
			{
				tcPages.TabPages.Add(sourceTab);
				tcPages.TabPages.Add(targetTab);
			}
			else
			{
				tcPages.TabPages.Add(targetTab);
				tcPages.TabPages.Add(sourceTab);
			}

			// Yeni sekmeyi seç
			tcPages.SelectedTab = sourceTab;
		}
		#endregion

		#region Form Events
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveOpenedFormsInfo(Constants.LastFormDataFileName);
			
			//ToolStripManager.SaveSettings(this, "ToolBarSettings");
			UIHelper.SaveToolbarVisibilityToRegistry(ToolsToolbar, tsmiShowToolsToolbar.Checked);
			UIHelper.SaveToolbarVisibilityToRegistry(OptionsToolbar, tsmiShowOptionsToolbar.Checked);
		}

		private void loadLastOpenedScreensToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(Constants.LastFormDataFileName))
			{
				FormDataBusiness formDataBusiness = new FormDataBusiness();
				ScreenDataListContract formDataContract = formDataBusiness.GetAll(Constants.LastFormDataFileName);
				if (formDataContract.BaseScreenDataContractList == null || formDataContract.BaseScreenDataContractList.Count == 0)
					return;

				if (MessageBox.Show(CommonResource.DoYouWantToOpenLastSessionSForms, CommonResource.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
					return;

				LoadFormsInfo(formDataContract.BaseScreenDataContractList);
			}
		}
		#endregion

		#region Private Methods
		private TabPage getClickedTab(Point location)
		{
			TabPage clickedTab = null;
			for (int i = 0; i < tcPages.TabPages.Count; i++)
			{
				if (tcPages.GetTabRect(i).Contains(location))
				{
					// i: tıklanan tab'ın indeksidir
					clickedTab = tcPages.TabPages[i];
					// Burada istediğiniz işlemi yapabilirsiniz
					break;
				}
			}

			return clickedTab;
		}

		private void changeCulture(String culture)
		{
			if(MessageBox.Show(CommonResource.ExistingScreensWillBeClosed, CommonResource.Approve, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			UIHelper.SaveCultureToRegistry(culture);

			Application.Restart();
		}

		private void initForm()
		{
			Text = String.Format($"{UIHelper.AssemblyProduct} {UIHelper.AssemblyVersion}");

			tsmiTurkish.Checked = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "tr";
			tsmiEnglish.Checked = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName != "tr";

			tsmiShowToolsToolbar.Checked = UIHelper.GetToolbarVisibilityFromRegistry(ToolsToolbar);
			tsmiShowOptionsToolbar.Checked = UIHelper.GetToolbarVisibilityFromRegistry(OptionsToolbar);

			//ToolStripManager.LoadSettings(this, "ToolBarSettings");
		}

		private void setTextFromResource()
		{
			this.fileMenu.Text = CommonResource.FileMenu;
			this.tsmiClearCache.Text = CommonResource.ClearCache;
			this.exitToolStripMenuItem.Text = CommonResource.ExitMenu;
			this.toolsMenu.Text = CommonResource.SQLTools;
			this.tsmiTools.Text = CommonResource.Tools;
			this.tbsmiSqlViewer.Text = CommonResource.SqlViewerMenu;
			this.tsmiRunQuery.Text = CommonResource.RunQuery;
			this.tsmiDBSearch.Text = CommonResource.DBSearch;
			this.tbsmiSqlCompare.Text = CommonResource.SqlCompare;
			this.tbsmiBachCompare.Text = CommonResource.SqlBatchCompare;
			this.tsmiInsertScriptGenerator.Text = CommonResource.InsertScriptGenerator;
			this.tsmiInsertScriptGenerator.ToolTipText = CommonResource.InsertScriptGeneratorToolTip;
			this.freeTextCompareToolStripMenuItem.Text = CommonResource.FreeTextCompare;
			this.tsmiConnectionString.Text = CommonResource.ConnectionStrings;
			this.tsmiConnectionString.ToolTipText = CommonResource.DataSourcesToolTip;
			this.tsmiTextViewer.Text = CommonResource.TextViewer;
			this.viewMenu.Text = CommonResource.OptionsMenu;
			this.toolBarToolStripMenuItem.Text = CommonResource.ToolbarMenu;
			this.tsmiMultiRowTabs.Text = CommonResource.AlwaysOpenNewPage;
			this.helpMenu.Text = CommonResource.HelpMenu;
			this.tsmiHelp.Text = CommonResource.Help;
			this.aboutToolStripMenuItem.Text = CommonResource.AboutNSqlTools;
			this.tsOptions.Text = CommonResource.ToolStrip;
			this.tsbSqlViewer.Text = CommonResource.SqlViewer;
			this.tsbSqlViewer.ToolTipText = CommonResource.SqlViewerToolTip;
			this.tsbRunQuery.Text = CommonResource.RunQuery;
			this.tsbRunQuery.ToolTipText = CommonResource.RunQueryToolTip;
			this.tsbDBSearch.Text = CommonResource.DBSearch;
			this.tsbDBSearch.ToolTipText = CommonResource.DBSearchToolTip;
			this.tsbSqlCompare.Text = CommonResource.SqlCompare;
			this.tsbSqlCompare.ToolTipText = CommonResource.SqlCompareTooltip;
			this.tsbBatchCompare.Text = CommonResource.SqlBatchCompare;
			this.tsbBatchCompare.ToolTipText = CommonResource.BatchCompareToolTip;
			this.tsbInsertScriptGenerator.Text = CommonResource.CreateInsertScripts;
			this.tsbInsertScriptGenerator.ToolTipText = CommonResource.InsertScriptGeneratorToolTip;
			this.tsbTextViewer.Text = CommonResource.TextViewer;
			this.tsbFreeTextCompare.Text = CommonResource.FreeTextCompare;
			this.saveFileDialog.Filter = CommonResource.SaveFileDialogFilter;
			this.openFileDialog.Filter = CommonResource.OpenFileDialogFilter;
			this.tsmiTableToCSV.Text = CommonResource.TableToCSV;
			this.tsmiFavoriteQueries.Text = CommonResource.FavoriteQueries;
			this.tsmiProjects.Text = CommonResource.Projects;
			this.tsmiMultiRowTabs.Text = CommonResource.MultiRowTabs;
			this.tsbScreenPackages.Text = CommonResource.Projects;
			this.tsbScreenPackages.ToolTipText = CommonResource.Savesandopenstheopenedscreenstogetherwiththeselectedcriteria;
			this.tsbDataSources.Text = CommonResource.ConnectionStrings;
			this.tsbDataSources.ToolTipText = CommonResource.DataSourcesToolTip;
			this.tsbFavoriteQueries.Text = CommonResource.FavoriteQueries;
			this.tsbFavoriteQueries.ToolTipText = CommonResource.FavoriteQueriesCanBeSavedForLaterUse;
			this.loadLastOpenedScreensToolStripMenuItem.Text = CommonResource.LoadLastOpenedScreens;
			this.tsmiDataCompare.Text = CommonResource.DataCompare;
			this.tsbDataCompare.Text = CommonResource.DataCompare;
			this.tsmiChangeLog.Text = CommonResource.ChangeLog;
			this.tsmiLogFiles.Text = CommonResource.LogFiles;
			this.tsmiShowToolsToolbar.Text = CommonResource.ShowToolsToolbar;
			this.tsmiShowOptionsToolbar.Text = CommonResource.ShowOptionsToolbar;
			this.tsmiEncryptDecrypt.Text = CommonResource.EncryptDecrypt;
			this.tsmiBOATools.Text = NSqlTools.Types.Properties.CommonResource.BOATools;
		}

		private T showUserControl<T>(String caption, String name, Boolean openNew = true, BaseScreenDataContract formDataBaseContract = null) where T : BaseUserControl, new()
		{
			if (tcPages.TabPages.ContainsKey(name))
			{
				if((name == "ConnectionStrings" || name == "FavoriteQueries" || name == "Projects")
					|| (!openNew || !tsmiMultiRowTabs.Checked))
				{
					tcPages.SelectedIndex = tcPages.TabPages.IndexOfKey(name);

					return (T)tcPages.TabPages[name].Controls[0];
				}
			}

			TabPage tp = new TabPage(caption + "  ");
			T uc = new T
			{
				MainForm = this,
				Dock = DockStyle.Fill,
				ParentTabPage = tp
			};
			tp.Padding = new Padding(2, 5, 2, 2);
			tp.Controls.Add(uc);
			tp.Name = name;
			tcPages.TabPages.Add(tp);
			tcPages.SelectedTab = tp;
			uc.InitForm();
			BuildLeftSideTabSequence(uc);
			uc.Show();
			if(formDataBaseContract != null)
				uc.SetFormData(formDataBaseContract);

			return uc;
		}

		private void SwapTabs(int sourceIndex, int targetIndex)
		{
			// Kaynak sekmeyi al
			var draggedTab = tcPages.TabPages[sourceIndex];

			// Kaynak sekmeyi kaldır
			tcPages.TabPages.RemoveAt(sourceIndex);

			// Hedef indeksi ayarla
			if (sourceIndex < targetIndex) targetIndex--;

			// Hedef pozisyona ekle
			tcPages.TabPages.Insert(targetIndex, draggedTab);

			// Yeni sekmeyi seç
			tcPages.SelectedTab = draggedTab;
		}

		private Control GetFocusedLeaf(Control root)
		{
			if (root == null) return null;
			if (root.Focused) return root;

			foreach (Control child in root.Controls)
			{
				if (child.ContainsFocus)
				{
					var deeper = GetFocusedLeaf(child);
					return deeper ?? child;
				}
			}
			return null;
		}

		private bool IsScintillaFocused()
		{
			var focused = GetFocusedLeaf(this);
			return focused is ScintillaNET.Scintilla;
		}
		#endregion

		#region Public Methods
		public ScreenDataListContract GetFormsInfo(String fileName)
		{
			FormDataBusiness formDataBusiness = new FormDataBusiness();
		
			return formDataBusiness.GetAll(fileName);
		}

		public void LoadFormsInfo(List<BaseScreenDataContract> baseScreenDataContractList)
		{
			tcPages.TabPages.Clear();

			foreach (BaseScreenDataContract formDataBaseContract in baseScreenDataContractList)
			{
				switch (formDataBaseContract.GetType())
				{
					case Type t when t == typeof(ConnectionStringsScreenDataContract):
						showUserControl<ucConnectionStrings>(CommonResource.ConnectionStrings, "ConnectionStrings", false, formDataBaseContract);
						break;
					case Type t when t == typeof(SqlViewerScreenDataContract):
						showUserControl<ucSqlViewer>(CommonResource.SqlViewer, "SqlViewer", false, formDataBaseContract);
						break;
					case Type t when t == typeof(TextViewerScreenDataContract):
						showUserControl<ucTextViewer>(CommonResource.TextViewer, "TextViewer", false, formDataBaseContract);
						break;
					case Type t when t == typeof(RunQueryScreenDataContract):
						showUserControl<ucRunQuery>(CommonResource.RunQuery, "RunSqlQuery", false, formDataBaseContract);
						break;
					case Type t when t == typeof(SearchDBScreenDataContract):
						showUserControl<ucSearchDB>(CommonResource.SearchDBObject, "SearchDBObject", false, formDataBaseContract);
						break;
					case Type t when t == typeof(InsertScriptGeneratorScreenDataContract):
						showUserControl<ucInsertScriptGenerator>(CommonResource.InsertScriptGenerator, "InsertScriptGenerator", false, formDataBaseContract);
						break;
					case Type t when t == typeof(FreeTextCompareScreenDataContract):
						showUserControl<ucFreeTextCompare>(CommonResource.FreeTextCompare, "FreeCompare", false, formDataBaseContract);
						break;
					case Type t when t == typeof(FavoriteQueryScreenDataContract):
						showUserControl<ucFavoriteQueries>(CommonResource.FavoriteQueries, "FavoriteQueries", false, formDataBaseContract);
						break;
					case Type t when t == typeof(DBObjectCompareScreenDataContract):
						showUserControl<ucDBObjectCompare>(CommonResource.SqlCompare, "SqlCompare", false, formDataBaseContract);
						break;
					case Type t when t == typeof(DBBatchCompareScreenDataContract):
						showUserControl<ucDBBatchCompare>(CommonResource.SqlBatchCompare, "BatchCompare", false, formDataBaseContract);
						break;
					case Type t when t == typeof(ProjectsScreenDataContract):
						showUserControl<ucProjects>(CommonResource.Projects, "Projects", false, formDataBaseContract);
						break;
					case Type t when t == typeof(SnippetScreenDataContract):
						showUserControl<ucSnippet>(CommonResource.Snippets, "Snippets", false, formDataBaseContract);
						break;
					case Type t when t == typeof(DataCompareScreenDataContract):
						showUserControl<ucDataCompare>(CommonResource.DataCompare, "DataCompare", false, formDataBaseContract);
						break;
				}
			}
		}

		public void LoadFormsInfo(String fileName)
		{
			FormDataBusiness formDataBusiness = new FormDataBusiness();
			ScreenDataListContract formDataContract = formDataBusiness.GetAll(fileName);
			LoadFormsInfo(formDataContract.BaseScreenDataContractList);
		}

		public ScreenDataListContract GetOpenedFormsInfo()
		{
			ScreenDataListContract formDataContract = new ScreenDataListContract();
			foreach (TabPage tabPage in tcPages.TabPages)
			{
				BaseUserControl baseUserControl = tabPage.Controls[0] as BaseUserControl;
				if (baseUserControl is ucProjects || baseUserControl == null)
					continue;

				formDataContract.BaseScreenDataContractList.Add(baseUserControl.GetFormData());
			}

			return formDataContract;
		}

		public void SaveOpenedFormsInfo(String fileName)
		{
			FormDataBusiness formDataBusiness = new FormDataBusiness();
			
			ScreenDataListContract formDataContract = GetOpenedFormsInfo();
			formDataBusiness.SaveAll(formDataContract, fileName);
		}

		public void BuildLeftSideTabSequence(BaseUserControl userControl)
		{
			if (userControl.TabProviders == null)
				return;

			TabSequence = new List<Control>();
			foreach (var tabProvider in userControl.TabProviders)
			{
				if (tabProvider is ICustomTabSequenceProvider provider)
				{
					TabSequence.AddRange(provider.GetCustomTabSequence()
													.Where(c => c != null && c.CanSelect && c.TabStop));
				}
				else
				{
					TabSequence.Add((Control)tabProvider);
				}
			}
		}

		// Replace the existing ProcessCmdKey override with this:
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Tab || keyData == (Keys.Shift | Keys.Tab))
			{
				// Let Scintilla handle Tab / Shift+Tab itself (indent/outdent)
				if (IsScintillaFocused())
					return false; // do not mark handled; pass through to control

				if (TabSequence == null || TabSequence.Count == 0)
					return base.ProcessCmdKey(ref msg, keyData);

				Control active = GetFocusedLeaf(this) ?? TabSequence.FirstOrDefault();

				if (!TabSequence.Contains(active))
				{
					var inner = TabSequence.FirstOrDefault(c => c.ContainsFocus);
					if (inner != null)
						active = inner;
				}

				int idx = TabSequence.IndexOf(active);
				if (idx < 0)
				{
					TabSequence[0].Focus();
					return true;
				}

				bool backwards = (keyData & Keys.Shift) == Keys.Shift;
				int nextIdx = backwards
					? (idx - 1 + TabSequence.Count) % TabSequence.Count
					: (idx + 1) % TabSequence.Count;

				TabSequence[nextIdx].Focus();
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
		#endregion
	}
}
