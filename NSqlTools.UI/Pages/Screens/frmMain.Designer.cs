using MetroFramework.Controls;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Properties;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	partial class frmMain
	{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.tcPages = new ClosableMetroTabControl();
            this.tsTools = new System.Windows.Forms.ToolStrip();
            this.tsbScreenPackages = new System.Windows.Forms.ToolStripButton();
            this.tsbDataSources = new System.Windows.Forms.ToolStripButton();
            this.tsbFavoriteQueries = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTextViewer = new System.Windows.Forms.ToolStripButton();
            this.tsbFreeTextCompare = new System.Windows.Forms.ToolStripButton();
            this.tsOptions = new System.Windows.Forms.ToolStrip();
            this.tsbSqlViewer = new System.Windows.Forms.ToolStripButton();
            this.tsbRunQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDBSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbSqlCompare = new System.Windows.Forms.ToolStripButton();
            this.tsbBatchCompare = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertScriptGenerator = new System.Windows.Forms.ToolStripButton();
            this.tsbTFSSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDataCompare = new System.Windows.Forms.ToolStripButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearCache = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLastOpenedScreensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tbsmiSqlViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRunQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDBSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tbsmiSqlCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.tbsmiBachCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDataCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiInsertScriptGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.freeTextCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTextViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFavoriteQueries = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSnippets = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiConnectionString = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMultiRowTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowToolsToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowOptionsToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTurkish = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBOATools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTableToCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEncryptDecrypt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTFS = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.tsTools.SuspendLayout();
            this.tsOptions.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.tcPages);
            resources.ApplyResources(this.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel");
            resources.ApplyResources(this.toolStripContainer, "toolStripContainer");
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.tsTools);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.tsOptions);
            // 
            // tcPages
            // 
            this.tcPages.AllowDrop = true;
            resources.ApplyResources(this.tcPages, "tcPages");
            this.tcPages.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcPages.Multiline = true;
            this.tcPages.Name = "tcPages";
            this.tcPages.DragDrop += new System.Windows.Forms.DragEventHandler(this.tcPages_DragDrop);
            this.tcPages.DragOver += new System.Windows.Forms.DragEventHandler(this.tcPages_DragOver);
            this.tcPages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tcPages_MouseDown);
            this.tcPages.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tcPages_MouseMove);
            // 
            // tsTools
            // 
            resources.ApplyResources(this.tsTools, "tsTools");
            this.tsTools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbScreenPackages,
            this.tsbDataSources,
            this.tsbFavoriteQueries,
            this.toolStripSeparator4,
            this.tsbTextViewer,
            this.tsbFreeTextCompare});
            this.tsTools.Name = "tsTools";
            // 
            // tsbScreenPackages
            // 
            this.tsbScreenPackages.Image = global::NSqlTools.UI.Properties.Resources.Package;
            resources.ApplyResources(this.tsbScreenPackages, "tsbScreenPackages");
            this.tsbScreenPackages.Name = "tsbScreenPackages";
            this.tsbScreenPackages.Click += new System.EventHandler(this.tsmiProjects_Click);
            // 
            // tsbDataSources
            // 
            this.tsbDataSources.Image = global::NSqlTools.UI.Properties.Resources.ConnectionString;
            resources.ApplyResources(this.tsbDataSources, "tsbDataSources");
            this.tsbDataSources.Name = "tsbDataSources";
            this.tsbDataSources.Click += new System.EventHandler(this.tsbDataSource_Click);
            // 
            // tsbFavoriteQueries
            // 
            resources.ApplyResources(this.tsbFavoriteQueries, "tsbFavoriteQueries");
            this.tsbFavoriteQueries.Name = "tsbFavoriteQueries";
            this.tsbFavoriteQueries.Click += new System.EventHandler(this.tsmiFavoriteQueries_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tsbTextViewer
            // 
            this.tsbTextViewer.Image = global::NSqlTools.UI.Properties.Resources.TextViewer;
            resources.ApplyResources(this.tsbTextViewer, "tsbTextViewer");
            this.tsbTextViewer.Name = "tsbTextViewer";
            this.tsbTextViewer.Click += new System.EventHandler(this.tsbTextViewer_Click);
            // 
            // tsbFreeTextCompare
            // 
            this.tsbFreeTextCompare.Image = global::NSqlTools.UI.Properties.Resources.FreeCompare;
            resources.ApplyResources(this.tsbFreeTextCompare, "tsbFreeTextCompare");
            this.tsbFreeTextCompare.Name = "tsbFreeTextCompare";
            this.tsbFreeTextCompare.Click += new System.EventHandler(this.tsbFreeTextCompare_Click);
            // 
            // tsOptions
            // 
            resources.ApplyResources(this.tsOptions, "tsOptions");
            this.tsOptions.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSqlViewer,
            this.tsbRunQuery,
            this.toolStripSeparator2,
            this.tsbDBSearch,
            this.tsbSqlCompare,
            this.tsbBatchCompare,
            this.tsbInsertScriptGenerator,
            this.tsbTFSSearch,
            this.toolStripSeparator3,
            this.tsbDataCompare});
            this.tsOptions.Name = "tsOptions";
            // 
            // tsbSqlViewer
            // 
            this.tsbSqlViewer.Image = global::NSqlTools.UI.Properties.Resources.SqlViewer;
            resources.ApplyResources(this.tsbSqlViewer, "tsbSqlViewer");
            this.tsbSqlViewer.Name = "tsbSqlViewer";
            this.tsbSqlViewer.Click += new System.EventHandler(this.SqlViewer_Click);
            // 
            // tsbRunQuery
            // 
            this.tsbRunQuery.Image = global::NSqlTools.UI.Properties.Resources.Query;
            resources.ApplyResources(this.tsbRunQuery, "tsbRunQuery");
            this.tsbRunQuery.Name = "tsbRunQuery";
            this.tsbRunQuery.Click += new System.EventHandler(this.RunQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsbDBSearch
            // 
            this.tsbDBSearch.Image = global::NSqlTools.UI.Properties.Resources.DBSearch;
            resources.ApplyResources(this.tsbDBSearch, "tsbDBSearch");
            this.tsbDBSearch.Name = "tsbDBSearch";
            this.tsbDBSearch.Click += new System.EventHandler(this.DBSearch_Click);
            // 
            // tsbSqlCompare
            // 
            this.tsbSqlCompare.Image = global::NSqlTools.UI.Properties.Resources.SqlCompare;
            resources.ApplyResources(this.tsbSqlCompare, "tsbSqlCompare");
            this.tsbSqlCompare.Name = "tsbSqlCompare";
            this.tsbSqlCompare.Click += new System.EventHandler(this.SqlCompare_Click);
            // 
            // tsbBatchCompare
            // 
            this.tsbBatchCompare.Image = global::NSqlTools.UI.Properties.Resources.BatchCompare;
            resources.ApplyResources(this.tsbBatchCompare, "tsbBatchCompare");
            this.tsbBatchCompare.Name = "tsbBatchCompare";
            this.tsbBatchCompare.Click += new System.EventHandler(this.BatchCompare_Click);
            // 
            // tsbInsertScriptGenerator
            // 
            this.tsbInsertScriptGenerator.Image = global::NSqlTools.UI.Properties.Resources.CreateInsertScripts;
            resources.ApplyResources(this.tsbInsertScriptGenerator, "tsbInsertScriptGenerator");
            this.tsbInsertScriptGenerator.Name = "tsbInsertScriptGenerator";
            this.tsbInsertScriptGenerator.Click += new System.EventHandler(this.InsertScriptGenerator_Click);
            // 
            // tsbTFSSearch
            // 
            this.tsbTFSSearch.Image = global::NSqlTools.UI.Properties.Resources.TFS;
            resources.ApplyResources(this.tsbTFSSearch, "tsbTFSSearch");
            this.tsbTFSSearch.Name = "tsbTFSSearch";
            this.tsbTFSSearch.Click += new System.EventHandler(this.tsbTFSSearch_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsbDataCompare
            // 
            this.tsbDataCompare.Image = global::NSqlTools.UI.Properties.Resources.DataCompare;
            resources.ApplyResources(this.tsbDataCompare, "tsbDataCompare");
            this.tsbDataCompare.Name = "tsbDataCompare";
            this.tsbDataCompare.Click += new System.EventHandler(this.tsmiDataCompare_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.toolsMenu,
            this.tsmiTools,
            this.viewMenu,
            this.tsmiBOATools,
            this.helpMenu});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClearCache,
            this.loadLastOpenedScreensToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            resources.ApplyResources(this.fileMenu, "fileMenu");
            this.fileMenu.Name = "fileMenu";
            // 
            // tsmiClearCache
            // 
            this.tsmiClearCache.Name = "tsmiClearCache";
            resources.ApplyResources(this.tsmiClearCache, "tsmiClearCache");
            this.tsmiClearCache.Click += new System.EventHandler(this.tsmiClearCache_Click);
            // 
            // loadLastOpenedScreensToolStripMenuItem
            // 
            this.loadLastOpenedScreensToolStripMenuItem.Name = "loadLastOpenedScreensToolStripMenuItem";
            resources.ApplyResources(this.loadLastOpenedScreensToolStripMenuItem, "loadLastOpenedScreensToolStripMenuItem");
            this.loadLastOpenedScreensToolStripMenuItem.Click += new System.EventHandler(this.loadLastOpenedScreensToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbsmiSqlViewer,
            this.tsmiRunQuery,
            this.toolStripMenuItem2,
            this.tsmiDBSearch,
            this.tbsmiSqlCompare,
            this.tbsmiBachCompare,
            this.tsmiDataCompare,
            this.toolStripMenuItem1,
            this.tsmiInsertScriptGenerator,
            this.freeTextCompareToolStripMenuItem,
            this.tsmiTextViewer});
            this.toolsMenu.Name = "toolsMenu";
            resources.ApplyResources(this.toolsMenu, "toolsMenu");
            // 
            // tbsmiSqlViewer
            // 
            this.tbsmiSqlViewer.Image = global::NSqlTools.UI.Properties.Resources.SqlViewer;
            this.tbsmiSqlViewer.Name = "tbsmiSqlViewer";
            resources.ApplyResources(this.tbsmiSqlViewer, "tbsmiSqlViewer");
            this.tbsmiSqlViewer.Click += new System.EventHandler(this.SqlViewer_Click);
            // 
            // tsmiRunQuery
            // 
            this.tsmiRunQuery.Image = global::NSqlTools.UI.Properties.Resources.Query;
            this.tsmiRunQuery.Name = "tsmiRunQuery";
            resources.ApplyResources(this.tsmiRunQuery, "tsmiRunQuery");
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // tsmiDBSearch
            // 
            this.tsmiDBSearch.Image = global::NSqlTools.UI.Properties.Resources.DBSearch;
            this.tsmiDBSearch.Name = "tsmiDBSearch";
            resources.ApplyResources(this.tsmiDBSearch, "tsmiDBSearch");
            this.tsmiDBSearch.Click += new System.EventHandler(this.DBSearch_Click);
            // 
            // tbsmiSqlCompare
            // 
            this.tbsmiSqlCompare.Image = global::NSqlTools.UI.Properties.Resources.SqlCompare;
            this.tbsmiSqlCompare.Name = "tbsmiSqlCompare";
            resources.ApplyResources(this.tbsmiSqlCompare, "tbsmiSqlCompare");
            this.tbsmiSqlCompare.Click += new System.EventHandler(this.SqlCompare_Click);
            // 
            // tbsmiBachCompare
            // 
            this.tbsmiBachCompare.Image = global::NSqlTools.UI.Properties.Resources.BatchCompare;
            this.tbsmiBachCompare.Name = "tbsmiBachCompare";
            resources.ApplyResources(this.tbsmiBachCompare, "tbsmiBachCompare");
            this.tbsmiBachCompare.Click += new System.EventHandler(this.BatchCompare_Click);
            // 
            // tsmiDataCompare
            // 
            this.tsmiDataCompare.Image = global::NSqlTools.UI.Properties.Resources.DataCompare;
            this.tsmiDataCompare.Name = "tsmiDataCompare";
            resources.ApplyResources(this.tsmiDataCompare, "tsmiDataCompare");
            this.tsmiDataCompare.Click += new System.EventHandler(this.tsmiDataCompare_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // tsmiInsertScriptGenerator
            // 
            this.tsmiInsertScriptGenerator.Image = global::NSqlTools.UI.Properties.Resources.CreateInsertScripts;
            this.tsmiInsertScriptGenerator.Name = "tsmiInsertScriptGenerator";
            resources.ApplyResources(this.tsmiInsertScriptGenerator, "tsmiInsertScriptGenerator");
            this.tsmiInsertScriptGenerator.Click += new System.EventHandler(this.InsertScriptGenerator_Click);
            // 
            // freeTextCompareToolStripMenuItem
            // 
            this.freeTextCompareToolStripMenuItem.Image = global::NSqlTools.UI.Properties.Resources.FreeCompare;
            this.freeTextCompareToolStripMenuItem.Name = "freeTextCompareToolStripMenuItem";
            resources.ApplyResources(this.freeTextCompareToolStripMenuItem, "freeTextCompareToolStripMenuItem");
            this.freeTextCompareToolStripMenuItem.Click += new System.EventHandler(this.tsbFreeTextCompare_Click);
            // 
            // tsmiTextViewer
            // 
            this.tsmiTextViewer.Image = global::NSqlTools.UI.Properties.Resources.TextViewer;
            this.tsmiTextViewer.Name = "tsmiTextViewer";
            resources.ApplyResources(this.tsmiTextViewer, "tsmiTextViewer");
            this.tsmiTextViewer.Click += new System.EventHandler(this.tsbTextViewer_Click);
            // 
            // tsmiTools
            // 
            this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFavoriteQueries,
            this.tsmiProjects,
            this.tsmiSnippets,
            this.toolStripMenuItem4,
            this.tsmiConnectionString});
            this.tsmiTools.Name = "tsmiTools";
            resources.ApplyResources(this.tsmiTools, "tsmiTools");
            // 
            // tsmiFavoriteQueries
            // 
            resources.ApplyResources(this.tsmiFavoriteQueries, "tsmiFavoriteQueries");
            this.tsmiFavoriteQueries.Name = "tsmiFavoriteQueries";
            this.tsmiFavoriteQueries.Click += new System.EventHandler(this.tsmiFavoriteQueries_Click);
            // 
            // tsmiProjects
            // 
            this.tsmiProjects.Image = global::NSqlTools.UI.Properties.Resources.Package;
            this.tsmiProjects.Name = "tsmiProjects";
            resources.ApplyResources(this.tsmiProjects, "tsmiProjects");
            this.tsmiProjects.Click += new System.EventHandler(this.tsmiProjects_Click);
            // 
            // tsmiSnippets
            // 
            this.tsmiSnippets.Image = global::NSqlTools.UI.Properties.Resources.Format;
            this.tsmiSnippets.Name = "tsmiSnippets";
            resources.ApplyResources(this.tsmiSnippets, "tsmiSnippets");
            this.tsmiSnippets.Click += new System.EventHandler(this.tsmiSnippets_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // tsmiConnectionString
            // 
            this.tsmiConnectionString.Image = global::NSqlTools.UI.Properties.Resources.ConnectionString;
            this.tsmiConnectionString.Name = "tsmiConnectionString";
            resources.ApplyResources(this.tsmiConnectionString, "tsmiConnectionString");
            this.tsmiConnectionString.Click += new System.EventHandler(this.tsbDataSource_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.CheckOnClick = true;
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.toolStripMenuItem5,
            this.tsmiMultiRowTabs,
            this.toolStripMenuItem6,
            this.tsmiShowToolsToolbar,
            this.tsmiShowOptionsToolbar,
            this.toolStripMenuItem8,
            this.tsmiTurkish,
            this.tsmiEnglish});
            this.viewMenu.Name = "viewMenu";
            resources.ApplyResources(this.viewMenu, "viewMenu");
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            resources.ApplyResources(this.toolBarToolStripMenuItem, "toolBarToolStripMenuItem");
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Checked = true;
            this.toolStripMenuItem5.CheckOnClick = true;
            this.toolStripMenuItem5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // tsmiMultiRowTabs
            // 
            this.tsmiMultiRowTabs.Checked = true;
            this.tsmiMultiRowTabs.CheckOnClick = true;
            this.tsmiMultiRowTabs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiMultiRowTabs.Name = "tsmiMultiRowTabs";
            resources.ApplyResources(this.tsmiMultiRowTabs, "tsmiMultiRowTabs");
            this.tsmiMultiRowTabs.Click += new System.EventHandler(this.tsmiMultiRowTabs_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // tsmiShowToolsToolbar
            // 
            this.tsmiShowToolsToolbar.Checked = true;
            this.tsmiShowToolsToolbar.CheckOnClick = true;
            this.tsmiShowToolsToolbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowToolsToolbar.Name = "tsmiShowToolsToolbar";
            resources.ApplyResources(this.tsmiShowToolsToolbar, "tsmiShowToolsToolbar");
            this.tsmiShowToolsToolbar.Click += new System.EventHandler(this.tsmiShowToolsToolbar_Click);
            // 
            // tsmiShowOptionsToolbar
            // 
            this.tsmiShowOptionsToolbar.Checked = true;
            this.tsmiShowOptionsToolbar.CheckOnClick = true;
            this.tsmiShowOptionsToolbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowOptionsToolbar.Name = "tsmiShowOptionsToolbar";
            resources.ApplyResources(this.tsmiShowOptionsToolbar, "tsmiShowOptionsToolbar");
            this.tsmiShowOptionsToolbar.Click += new System.EventHandler(this.tsmiShowOptionsToolbar_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // tsmiTurkish
            // 
            this.tsmiTurkish.CheckOnClick = true;
            this.tsmiTurkish.Image = global::NSqlTools.UI.Properties.Resources.Turkish;
            this.tsmiTurkish.Name = "tsmiTurkish";
            resources.ApplyResources(this.tsmiTurkish, "tsmiTurkish");
            this.tsmiTurkish.Click += new System.EventHandler(this.tsmiTurkish_Click);
            // 
            // tsmiEnglish
            // 
            this.tsmiEnglish.CheckOnClick = true;
            this.tsmiEnglish.Image = global::NSqlTools.UI.Properties.Resources.English;
            this.tsmiEnglish.Name = "tsmiEnglish";
            resources.ApplyResources(this.tsmiEnglish, "tsmiEnglish");
            this.tsmiEnglish.Click += new System.EventHandler(this.tsmiEnglish_Click);
            // 
            // tsmiBOATools
            // 
            this.tsmiBOATools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTableToCSV,
            this.tsmiEncryptDecrypt,
            this.tsmiTFS});
            this.tsmiBOATools.Name = "tsmiBOATools";
            resources.ApplyResources(this.tsmiBOATools, "tsmiBOATools");
            // 
            // tsmiTableToCSV
            // 
            this.tsmiTableToCSV.Image = global::NSqlTools.UI.Properties.Resources.Table;
            this.tsmiTableToCSV.Name = "tsmiTableToCSV";
            resources.ApplyResources(this.tsmiTableToCSV, "tsmiTableToCSV");
            this.tsmiTableToCSV.Click += new System.EventHandler(this.tsmiTableToCSV_Click);
            // 
            // tsmiEncryptDecrypt
            // 
            this.tsmiEncryptDecrypt.Image = global::NSqlTools.UI.Properties.Resources.Encrypt;
            this.tsmiEncryptDecrypt.Name = "tsmiEncryptDecrypt";
            resources.ApplyResources(this.tsmiEncryptDecrypt, "tsmiEncryptDecrypt");
            this.tsmiEncryptDecrypt.Click += new System.EventHandler(this.tsmiEncryptDecrypt_Click);
            // 
            // tsmiTFS
            // 
            this.tsmiTFS.Image = global::NSqlTools.UI.Properties.Resources.TFS;
            this.tsmiTFS.Name = "tsmiTFS";
            resources.ApplyResources(this.tsmiTFS, "tsmiTFS");
            this.tsmiTFS.Click += new System.EventHandler(this.tsbTFSSearch_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChangeLog,
            this.tsmiLogFiles,
            this.toolStripMenuItem7,
            this.tsmiHelp,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            resources.ApplyResources(this.helpMenu, "helpMenu");
            // 
            // tsmiChangeLog
            // 
            this.tsmiChangeLog.Name = "tsmiChangeLog";
            resources.ApplyResources(this.tsmiChangeLog, "tsmiChangeLog");
            this.tsmiChangeLog.Click += new System.EventHandler(this.tsmiChangeLog_Click);
            // 
            // tsmiLogFiles
            // 
            this.tsmiLogFiles.Name = "tsmiLogFiles";
            resources.ApplyResources(this.tsmiLogFiles, "tsmiLogFiles");
            this.tsmiLogFiles.Click += new System.EventHandler(this.tsmiLogFiles_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.Name = "tsmiHelp";
            resources.ApplyResources(this.tsmiHelp, "tsmiHelp");
            this.tsmiHelp.Click += new System.EventHandler(this.tsmiHelp_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::NSqlTools.UI.Properties.Resources.About1;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // saveFileDialog
            // 
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.tsTools.ResumeLayout(false);
            this.tsTools.PerformLayout();
            this.tsOptions.ResumeLayout(false);
            this.tsOptions.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStrip tsOptions;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileMenu;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewMenu;
		private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsMenu;
		private System.Windows.Forms.ToolStripMenuItem helpMenu;
		private System.Windows.Forms.ToolStripButton tsbSqlViewer;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ToolStripButton tsbSqlCompare;
		private System.Windows.Forms.ToolStripButton tsbBatchCompare;
		private System.Windows.Forms.ToolStripButton tsbInsertScriptGenerator;
		private System.Windows.Forms.ToolStripButton tsbDBSearch;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiInsertScriptGenerator;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tbsmiSqlViewer;
		private System.Windows.Forms.ToolStripMenuItem tbsmiSqlCompare;
		private System.Windows.Forms.ToolStripMenuItem tbsmiBachCompare;
		private System.Windows.Forms.ToolStripMenuItem tsmiDBSearch;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		public System.Windows.Forms.ErrorProvider errorProvider;
		public System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripButton tsbRunQuery;
		private System.Windows.Forms.ToolStripMenuItem tsmiRunQuery;
		private ToolStripMenuItem tsmiHelp;
		public OpenFileDialog openFileDialog;
		private ToolStripMenuItem freeTextCompareToolStripMenuItem;
		private ToolStripMenuItem tsmiClearCache;
		private ToolStripSeparator toolStripMenuItem3;
		private ToolStripMenuItem tsmiMultiRowTabs;
		private ToolStripMenuItem tsmiTextViewer;
		private ToolStripMenuItem tsmiTurkish;
		private ToolStripMenuItem tsmiEnglish;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripSeparator toolStripMenuItem6;
		private ToolStripMenuItem tsmiTools;
		private ToolStripMenuItem tsmiFavoriteQueries;
		private ToolStripMenuItem tsmiTableToCSV;
		private ToolStripSeparator toolStripMenuItem4;
		private ToolStripMenuItem tsmiConnectionString;
		private ToolStripMenuItem tsmiProjects;
		private ToolStripMenuItem toolStripMenuItem5;
		private ClosableMetroTabControl tcPages;
		private ToolStrip tsTools;
		private ToolStripButton tsbScreenPackages;
		private ToolStripButton tsbDataSources;
		private ToolStripButton tsbFavoriteQueries;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripButton tsbTextViewer;
		private ToolStripButton tsbFreeTextCompare;
		private ToolStripMenuItem loadLastOpenedScreensToolStripMenuItem;
		private ToolStripMenuItem tsmiDataCompare;
		private ToolStripButton tsbDataCompare;
		private ToolStripMenuItem tsmiSnippets;
		private ToolStripMenuItem tsmiChangeLog;
		private ToolStripMenuItem tsmiLogFiles;
		private ToolStripSeparator toolStripMenuItem7;
		private ToolStripContainer toolStripContainer;
		private ToolStripMenuItem tsmiShowToolsToolbar;
		private ToolStripSeparator toolStripMenuItem8;
		private ToolStripMenuItem tsmiShowOptionsToolbar;
		private ToolStripMenuItem tsmiEncryptDecrypt;
		private ToolStripMenuItem tsmiBOATools;
		private ToolStripMenuItem tsmiTFS;
		private ToolStripButton tsbTFSSearch;
	}
}



