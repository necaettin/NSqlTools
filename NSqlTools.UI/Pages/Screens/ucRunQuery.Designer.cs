using NSqlTools.Lib.Controls;
using NSqlTools.Types;
using NSqlTools.UI.UserControls;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	partial class ucRunQuery
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRunQuery));
            this.scQueryAndResult = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucSqlNotePadControl = new NSqlTools.UI.UserControls.ucSqlNotePad();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbCriteriaCollapse = new System.Windows.Forms.ToolStripButton();
            this.tsbQueryResultOpenClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRunQuery = new System.Windows.Forms.ToolStripButton();
            this.tspParse = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cbFavoriteQueries = new System.Windows.Forms.ToolStripComboBox();
            this.btnRefreshFavoriteQueries = new System.Windows.Forms.ToolStripButton();
            this.btnGetFromFavoriteQueries = new System.Windows.Forms.ToolStripButton();
            this.btnSaveToFavoriteQueries = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlQueryResults = new System.Windows.Forms.Panel();
            this.tcQueryResults = new System.Windows.Forms.TabControl();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucDBObjectSelectControl = new NSqlTools.UI.UserControls.ucDBObjectSelect();
            this.scoQuery = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scQueryAndResult)).BeginInit();
            this.scQueryAndResult.Panel1.SuspendLayout();
            this.scQueryAndResult.Panel2.SuspendLayout();
            this.scQueryAndResult.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.pnlQueryResults.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoQuery)).BeginInit();
            this.scoQuery.Panel1.SuspendLayout();
            this.scoQuery.Panel2.SuspendLayout();
            this.scoQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // scQueryAndResult
            // 
            this.scQueryAndResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scQueryAndResult.Location = new System.Drawing.Point(0, 0);
            this.scQueryAndResult.Name = "scQueryAndResult";
            this.scQueryAndResult.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scQueryAndResult.Panel1
            // 
            this.scQueryAndResult.Panel1.Controls.Add(this.panel3);
            // 
            // scQueryAndResult.Panel2
            // 
            this.scQueryAndResult.Panel2.Controls.Add(this.pnlQueryResults);
            this.scQueryAndResult.Panel2Collapsed = true;
            this.scQueryAndResult.Size = new System.Drawing.Size(581, 635);
            this.scQueryAndResult.SplitterDistance = 317;
            this.scQueryAndResult.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(581, 635);
            this.panel3.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucSqlNotePadControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 604);
            this.panel1.TabIndex = 2;
            // 
            // ucSqlNotePadControl
            // 
            this.ucSqlNotePadControl.CaseSensitive = false;
            this.ucSqlNotePadControl.CompareTypeVisible = false;
            this.ucSqlNotePadControl.DBObjectContract = null;
            this.ucSqlNotePadControl.DBObjectKeywordList = null;
            this.ucSqlNotePadControl.DisplayFullScreen = true;
            this.ucSqlNotePadControl.DisplayStatus = true;
            this.ucSqlNotePadControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSqlNotePadControl.FontSize = 12;
            this.ucSqlNotePadControl.IsWraped = false;
            this.ucSqlNotePadControl.Location = new System.Drawing.Point(0, 0);
            this.ucSqlNotePadControl.MainForm = null;
            this.ucSqlNotePadControl.Margin = new System.Windows.Forms.Padding(4);
            this.ucSqlNotePadControl.Name = "ucSqlNotePadControl";
            this.ucSqlNotePadControl.ParentTabPage = null;
            this.ucSqlNotePadControl.SchemaKeywordList = null;
            this.ucSqlNotePadControl.scoSqlNotepadPanel2Collapsed = true;
            this.ucSqlNotePadControl.SearchKeyword = "";
            this.ucSqlNotePadControl.Size = new System.Drawing.Size(581, 604);
            this.ucSqlNotePadControl.TabIndex = 0;
            this.ucSqlNotePadControl.Title = "Sql Script";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.tsMenu);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(581, 31);
            this.panel2.TabIndex = 1;
            // 
            // tsMenu
            // 
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCriteriaCollapse,
            this.tsbQueryResultOpenClose,
            this.toolStripSeparator1,
            this.tsbRunQuery,
            this.tsbCancelQuery,
            this.tspParse,
            this.toolStripSeparator2,
            this.cbFavoriteQueries,
            this.btnRefreshFavoriteQueries,
            this.btnGetFromFavoriteQueries,
            this.btnSaveToFavoriteQueries,
            this.toolStripSeparator3});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(581, 31);
            this.tsMenu.TabIndex = 15;
            this.tsMenu.Text = "Expand Query Results Panel";
            // 
            // tsbCriteriaCollapse
            // 
            this.tsbCriteriaCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCriteriaCollapse.Image = ((System.Drawing.Image)(resources.GetObject("tsbCriteriaCollapse.Image")));
            this.tsbCriteriaCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCriteriaCollapse.Name = "tsbCriteriaCollapse";
            this.tsbCriteriaCollapse.Size = new System.Drawing.Size(28, 28);
            this.tsbCriteriaCollapse.Text = "Collapse Criteria Panel";
            this.tsbCriteriaCollapse.Click += new System.EventHandler(this.tsbCriteriaCollapse_Click);
            // 
            // tsbQueryResultOpenClose
            // 
            this.tsbQueryResultOpenClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbQueryResultOpenClose.Image = global::NSqlTools.UI.Properties.Resources.OpenDown;
            this.tsbQueryResultOpenClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQueryResultOpenClose.Name = "tsbQueryResultOpenClose";
            this.tsbQueryResultOpenClose.Size = new System.Drawing.Size(28, 28);
            this.tsbQueryResultOpenClose.Text = "Expand Query Results Panel";
            this.tsbQueryResultOpenClose.Click += new System.EventHandler(this.tsbQueryResultOpenClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbRunQuery
            // 
            this.tsbRunQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRunQuery.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
            this.tsbRunQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRunQuery.Name = "tsbRunQuery";
            this.tsbRunQuery.Size = new System.Drawing.Size(28, 28);
            this.tsbRunQuery.Text = "Run Query";
            this.tsbRunQuery.Click += new System.EventHandler(this.tsbRunQuery_Click);
            // 
            // tspParse
            // 
            this.tspParse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspParse.Image = global::NSqlTools.UI.Properties.Resources.Ok;
            this.tspParse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspParse.Name = "tspParse";
            this.tspParse.Size = new System.Drawing.Size(28, 28);
            this.tspParse.Text = "Parse Sql";
            this.tspParse.Click += new System.EventHandler(this.tspParse_Click);
            // 
            // tsbCancelQuery
            // 
            this.tsbCancelQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancelQuery.Enabled = false;
            this.tsbCancelQuery.Image = global::NSqlTools.UI.Properties.Resources.CloseBlue;
            this.tsbCancelQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelQuery.Name = "tsbCancelQuery";
            this.tsbCancelQuery.Size = new System.Drawing.Size(28, 28);
            this.tsbCancelQuery.Text = "Cancel Query";
            this.tsbCancelQuery.Click += new System.EventHandler(this.tsbCancelQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // cbFavoriteQueries
            // 
            this.cbFavoriteQueries.Name = "cbFavoriteQueries";
            this.cbFavoriteQueries.Size = new System.Drawing.Size(121, 31);
            // 
            // btnRefreshFavoriteQueries
            // 
            this.btnRefreshFavoriteQueries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshFavoriteQueries.Image = global::NSqlTools.UI.Properties.Resources.Refresh;
            this.btnRefreshFavoriteQueries.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshFavoriteQueries.Name = "btnRefreshFavoriteQueries";
            this.btnRefreshFavoriteQueries.Size = new System.Drawing.Size(28, 28);
            this.btnRefreshFavoriteQueries.Text = "Refresh favorite queries";
            this.btnRefreshFavoriteQueries.Click += new System.EventHandler(this.btnRefreshFavoriteQueries_Click);
            // 
            // btnGetFromFavoriteQueries
            // 
            this.btnGetFromFavoriteQueries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGetFromFavoriteQueries.Image = global::NSqlTools.UI.Properties.Resources.Down;
            this.btnGetFromFavoriteQueries.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetFromFavoriteQueries.Name = "btnGetFromFavoriteQueries";
            this.btnGetFromFavoriteQueries.Size = new System.Drawing.Size(28, 28);
            this.btnGetFromFavoriteQueries.Text = "Get from favorite queries";
            this.btnGetFromFavoriteQueries.Click += new System.EventHandler(this.btnGetFromFavoriteQueries_Click);
            // 
            // btnSaveToFavoriteQueries
            // 
            this.btnSaveToFavoriteQueries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveToFavoriteQueries.Image = global::NSqlTools.UI.Properties.Resources.FavoriteSave_16x16;
            this.btnSaveToFavoriteQueries.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveToFavoriteQueries.Name = "btnSaveToFavoriteQueries";
            this.btnSaveToFavoriteQueries.Size = new System.Drawing.Size(28, 28);
            this.btnSaveToFavoriteQueries.Text = "Save to favorite queries";
            this.btnSaveToFavoriteQueries.Click += new System.EventHandler(this.btnSaveToFavoriteQueries_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // pnlQueryResults
            // 
            this.pnlQueryResults.Controls.Add(this.tcQueryResults);
            this.pnlQueryResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQueryResults.Location = new System.Drawing.Point(0, 0);
            this.pnlQueryResults.Name = "pnlQueryResults";
            this.pnlQueryResults.Size = new System.Drawing.Size(150, 46);
            this.pnlQueryResults.TabIndex = 0;
            // 
            // tcQueryResults
            // 
            this.tcQueryResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcQueryResults.Location = new System.Drawing.Point(0, 0);
            this.tcQueryResults.Name = "tcQueryResults";
            this.tcQueryResults.SelectedIndex = 0;
            this.tcQueryResults.Size = new System.Drawing.Size(150, 46);
            this.tcQueryResults.TabIndex = 0;
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleName = "New item selection";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.miniToolStrip.Location = new System.Drawing.Point(127, 6);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(580, 31);
            this.miniToolStrip.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.ucDBObjectSelectControl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(249, 358);
            this.panel4.TabIndex = 1;
            // 
            // ucDBObjectSelectControl
            // 
            this.ucDBObjectSelectControl.AllowOnlyOneDBSelection = true;
            this.ucDBObjectSelectControl.Caption = "DB Select";
            this.ucDBObjectSelectControl.DBObjectVisibility = true;
            this.ucDBObjectSelectControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDBObjectSelectControl.IsRequiredConnectionString = true;
            this.ucDBObjectSelectControl.IsRequiredDB = true;
            this.ucDBObjectSelectControl.IsRequiredDBObject = false;
            this.ucDBObjectSelectControl.IsRequiredObjectType = false;
            this.ucDBObjectSelectControl.IsRequiredSchema = false;
            this.ucDBObjectSelectControl.Location = new System.Drawing.Point(0, 0);
            this.ucDBObjectSelectControl.MainForm = null;
            this.ucDBObjectSelectControl.Margin = new System.Windows.Forms.Padding(4);
            this.ucDBObjectSelectControl.Name = "ucDBObjectSelectControl";
            this.ucDBObjectSelectControl.ObjectTypeVisibility = true;
            this.ucDBObjectSelectControl.ParentTabPage = null;
            this.ucDBObjectSelectControl.SchemaVisibility = true;
            this.ucDBObjectSelectControl.SelectedConnectionNameValue = null;
            this.ucDBObjectSelectControl.SelectedDBIndexes = null;
            this.ucDBObjectSelectControl.SelectedDBObjectObjectId = null;
            this.ucDBObjectSelectControl.SelectedObjectType2 = 9;
            this.ucDBObjectSelectControl.SelectedSchemaId = null;
            this.ucDBObjectSelectControl.Size = new System.Drawing.Size(249, 356);
            this.ucDBObjectSelectControl.TabIndex = 1;
            this.ucDBObjectSelectControl.TabIndexConnectionString = 1;
            this.ucDBObjectSelectControl.TabIndexDB = 7;
            this.ucDBObjectSelectControl.TabIndexDBObject = 6;
            this.ucDBObjectSelectControl.TabIndexDBObjectFilter = 5;
            this.ucDBObjectSelectControl.TabIndexObjectType = 3;
            this.ucDBObjectSelectControl.TabIndexSchema = 4;
            this.ucDBObjectSelectControl.TitleVisibility = null;
            this.ucDBObjectSelectControl.OnDBObjectChanged += new System.EventHandler<NSqlTools.Types.DBObjectChangedEventArgs>(this.ucDBObjectSelectControl_OnDBObjectChanged);
            this.ucDBObjectSelectControl.OnDBChanged += new System.EventHandler(this.ucDBObjectSelectControl_OnDBChanged);
            this.ucDBObjectSelectControl.OnDBClear += new System.EventHandler(this.ucDBObjectSelectControl_OnDBClear);
            // 
            // scoQuery
            // 
            this.scoQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoQuery.Location = new System.Drawing.Point(0, 0);
            this.scoQuery.Name = "scoQuery";
            // 
            // scoQuery.Panel1
            // 
            this.scoQuery.Panel1.Controls.Add(this.panel4);
            this.scoQuery.Panel1MinSize = 0;
            // 
            // scoQuery.Panel2
            // 
            this.scoQuery.Panel2.Controls.Add(this.scQueryAndResult);
            this.scoQuery.Size = new System.Drawing.Size(834, 635);
            this.scoQuery.SplitterDistance = 249;
            this.scoQuery.TabIndex = 2;
            // 
            // ucRunQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scoQuery);
            this.Name = "ucRunQuery";
            this.Size = new System.Drawing.Size(834, 635);
            this.scQueryAndResult.Panel1.ResumeLayout(false);
            this.scQueryAndResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scQueryAndResult)).EndInit();
            this.scQueryAndResult.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.pnlQueryResults.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.scoQuery.Panel1.ResumeLayout(false);
            this.scoQuery.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scoQuery)).EndInit();
            this.scoQuery.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private SplitContainer scQueryAndResult;
		private Panel panel3;
		private Panel panel1;
		private ucSqlNotePad ucSqlNotePadControl;
		private Panel panel2;
		private ToolStrip tsMenu;
		private ToolStripButton tsbCriteriaCollapse;
		private ToolStripButton tsbQueryResultOpenClose;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton tsbRunQuery;
		private ToolStripButton tspParse;
		private ToolStripButton tsbCancelQuery;
		private Panel pnlQueryResults;
		private TabControl tcQueryResults;
		private ToolStrip miniToolStrip;
		private Panel panel4;
		private ucDBObjectSelect ucDBObjectSelectControl;
		private SplitContainer scoQuery;
		private ToolStripSeparator toolStripSeparator2;
		private ToolTip toolTip1;
		private ToolStripComboBox cbFavoriteQueries;
		private ToolStripButton btnRefreshFavoriteQueries;
		private ToolStripButton btnGetFromFavoriteQueries;
		private ToolStripButton btnSaveToFavoriteQueries;
		private ToolStripSeparator toolStripSeparator3;
	}
}
