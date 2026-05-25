using NSqlTools.Types;
using NSqlTools.UI.UserControls;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	partial class ucSqlViewer
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

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSqlViewer));
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.scSqlViewer = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ucDBObjectSelect = new NSqlTools.UI.UserControls.ucDBObjectSelect();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ucSqlNotePadControl = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.ucTableViewControl = new NSqlTools.UI.UserControls.ucTableView();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbCriteriaCollapse = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.scSqlViewer)).BeginInit();
			this.scSqlViewer.Panel1.SuspendLayout();
			this.scSqlViewer.Panel2.SuspendLayout();
			this.scSqlViewer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tsMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// scSqlViewer
			// 
			this.scSqlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scSqlViewer.Location = new System.Drawing.Point(0, 0);
			this.scSqlViewer.Name = "scSqlViewer";
			// 
			// scSqlViewer.Panel1
			// 
			this.scSqlViewer.Panel1.Controls.Add(this.panel1);
			// 
			// scSqlViewer.Panel2
			// 
			this.scSqlViewer.Panel2.Controls.Add(this.panel2);
			this.scSqlViewer.Panel2.Controls.Add(this.tsMenu);
			this.scSqlViewer.Size = new System.Drawing.Size(975, 536);
			this.scSqlViewer.SplitterDistance = 250;
			this.scSqlViewer.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.ucDBObjectSelect);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(250, 536);
			this.panel1.TabIndex = 2;
			// 
			// ucDBObjectSelect
			// 
			this.ucDBObjectSelect.AllowOnlyOneDBSelection = true;
			this.ucDBObjectSelect.Caption = "DB Object";
			this.ucDBObjectSelect.DBContractList = null;
			this.ucDBObjectSelect.DBObjectContractList = null;
			this.ucDBObjectSelect.DBObjectVisibility = true;
			this.ucDBObjectSelect.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDBObjectSelect.IsRequiredConnectionString = true;
			this.ucDBObjectSelect.IsRequiredDB = true;
			this.ucDBObjectSelect.IsRequiredDBObject = true;
			this.ucDBObjectSelect.IsRequiredObjectType = true;
			this.ucDBObjectSelect.IsRequiredSchema = true;
			this.ucDBObjectSelect.Location = new System.Drawing.Point(0, 0);
			this.ucDBObjectSelect.MainForm = null;
			this.ucDBObjectSelect.Name = "ucDBObjectSelect";
			this.ucDBObjectSelect.ObjectTypeVisibility = true;
			this.ucDBObjectSelect.ParentTabPage = null;
			this.ucDBObjectSelect.SchemaVisibility = true;
			this.ucDBObjectSelect.SelectedConnectionNameValue = null;
			this.ucDBObjectSelect.SelectedDBIndexes = null;
			this.ucDBObjectSelect.SelectedDBObjectObjectId = null;
			this.ucDBObjectSelect.SelectedObjectType2 = 9;
			this.ucDBObjectSelect.SelectedSchemaId = null;
			this.ucDBObjectSelect.Size = new System.Drawing.Size(250, 369);
			this.ucDBObjectSelect.TabIndex = 1;
			this.ucDBObjectSelect.TabIndexConnectionString = 1;
			this.ucDBObjectSelect.TabIndexDB = 7;
			this.ucDBObjectSelect.TabIndexDBObject = 6;
			this.ucDBObjectSelect.TabIndexDBObjectFilter = 5;
			this.ucDBObjectSelect.TabIndexObjectType = 3;
			this.ucDBObjectSelect.TabIndexSchema = 4;
			this.ucDBObjectSelect.TitleVisibility = null;
			this.ucDBObjectSelect.OnDBObjectChanged += new System.EventHandler<NSqlTools.Types.DBObjectChangedEventArgs>(this.ucDBObjectSelect_OnDBObjectChanged);
			this.ucDBObjectSelect.OnDBObjectClear += new System.EventHandler(this.ucDBObjectSelect_OnDBObjectClear);
			this.ucDBObjectSelect.OnObjectTypeChanged += new System.EventHandler(this.ucDBObjectSelect_OnObjectTypeChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ucSqlNotePadControl);
			this.panel2.Controls.Add(this.ucTableViewControl);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 31);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(721, 505);
			this.panel2.TabIndex = 19;
			// 
			// ucSqlNotePadControl
			// 
			this.ucSqlNotePadControl.CaseSensitive = false;
			this.ucSqlNotePadControl.CompareTypeVisible = false;
			this.ucSqlNotePadControl.DBObjectContract = null;
			this.ucSqlNotePadControl.DBObjectKeywordList = null;
			this.ucSqlNotePadControl.DisplayFullScreen = true;
			this.ucSqlNotePadControl.DisplayStatus = true;
			this.ucSqlNotePadControl.FontSize = 12;
			this.ucSqlNotePadControl.Location = new System.Drawing.Point(19, 20);
			this.ucSqlNotePadControl.MainForm = null;
			this.ucSqlNotePadControl.Name = "ucSqlNotePadControl";
			this.ucSqlNotePadControl.ParentTabPage = null;
			this.ucSqlNotePadControl.SchemaKeywordList = null;
			this.ucSqlNotePadControl.scoSqlNotepadPanel2Collapsed = true;
			this.ucSqlNotePadControl.SearchKeyword = "";
			this.ucSqlNotePadControl.Size = new System.Drawing.Size(405, 234);
			this.ucSqlNotePadControl.TabIndex = 0;
			this.ucSqlNotePadControl.Title = "Sql Script";
			this.ucSqlNotePadControl.Visible = false;
			// 
			// ucTableViewControl
			// 
			this.ucTableViewControl.DBObjectContract = null;
			this.ucTableViewControl.Location = new System.Drawing.Point(35, 271);
			this.ucTableViewControl.MainForm = null;
			this.ucTableViewControl.Name = "ucTableViewControl";
			this.ucTableViewControl.ParentTabPage = null;
			this.ucTableViewControl.Size = new System.Drawing.Size(433, 132);
			this.ucTableViewControl.TabIndex = 1;
			this.ucTableViewControl.Visible = false;
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCriteriaCollapse});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(721, 31);
			this.tsMenu.TabIndex = 18;
			this.tsMenu.Text = "toolStrip1";
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
			// ucSqlViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scSqlViewer);
			this.Name = "ucSqlViewer";
			this.Size = new System.Drawing.Size(975, 536);
			this.scSqlViewer.Panel1.ResumeLayout(false);
			this.scSqlViewer.Panel2.ResumeLayout(false);
			this.scSqlViewer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.scSqlViewer)).EndInit();
			this.scSqlViewer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.ResumeLayout(false);

		}


		#endregion
		private System.Windows.Forms.SplitContainer scSqlViewer;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private ucSqlNotePad ucSqlNotePadControl;
		private UserControls.ucDBObjectSelect ucDBObjectSelect;
		private UserControls.ucTableView ucTableViewControl;
		private Panel panel1;
		private Panel panel2;
		private ToolStrip tsMenu;
		private ToolStripButton tsbCriteriaCollapse;
	}
}
