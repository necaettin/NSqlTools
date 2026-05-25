using NSqlTools.Lib.Controls;
using NSqlTools.Types;
using NSqlTools.UI.UserControls;

namespace NSqlTools.UI.Pages
{
	partial class ucDataCompare
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDataCompare));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.scDBObjectCompare = new System.Windows.Forms.SplitContainer();
			this.panel2 = new System.Windows.Forms.Panel();
			this.gbTableView = new System.Windows.Forms.GroupBox();
			this.panel6 = new System.Windows.Forms.Panel();
			this.dgvColumns = new System.Windows.Forms.DataGridView();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel4 = new System.Windows.Forms.Panel();
			this.ucDBObjectSelectTarget = new NSqlTools.UI.UserControls.ucDBObjectSelect();
			this.ucDBObjectSelectSource = new NSqlTools.UI.UserControls.ucDBObjectSelect();
			this.panel1 = new System.Windows.Forms.Panel();
			this.scDataCompare = new System.Windows.Forms.SplitContainer();
			this.scSqlScript = new System.Windows.Forms.SplitContainer();
			this._ucSqlNotePadSource = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this._ucSqlNotePadTarget = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.dgvCompareResult = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.ssStatus = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbEqual = new System.Windows.Forms.ToolStripButton();
			this.tsbNotEqual = new System.Windows.Forms.ToolStripButton();
			this.tsbSourceExists = new System.Windows.Forms.ToolStripButton();
			this.tsbTargetExists = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbDisplayOnlyNotEqualColumns = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbExportToExcel = new System.Windows.Forms.ToolStripButton();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbCriteriaCollapse = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbRunScript = new System.Windows.Forms.ToolStripButton();
			this.tsbDiffSql = new System.Windows.Forms.ToolStripButton();
			this.scInputSqlScript = new ScintillaNET.Scintilla();
			((System.ComponentModel.ISupportInitialize)(this.scDBObjectCompare)).BeginInit();
			this.scDBObjectCompare.Panel1.SuspendLayout();
			this.scDBObjectCompare.Panel2.SuspendLayout();
			this.scDBObjectCompare.SuspendLayout();
			this.panel2.SuspendLayout();
			this.gbTableView.SuspendLayout();
			this.panel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scDataCompare)).BeginInit();
			this.scDataCompare.Panel1.SuspendLayout();
			this.scDataCompare.Panel2.SuspendLayout();
			this.scDataCompare.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scSqlScript)).BeginInit();
			this.scSqlScript.Panel1.SuspendLayout();
			this.scSqlScript.Panel2.SuspendLayout();
			this.scSqlScript.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvCompareResult)).BeginInit();
			this.ssStatus.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tsMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// scDBObjectCompare
			// 
			this.scDBObjectCompare.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scDBObjectCompare.Location = new System.Drawing.Point(0, 0);
			this.scDBObjectCompare.Name = "scDBObjectCompare";
			// 
			// scDBObjectCompare.Panel1
			// 
			this.scDBObjectCompare.Panel1.Controls.Add(this.panel2);
			// 
			// scDBObjectCompare.Panel2
			// 
			this.scDBObjectCompare.Panel2.Controls.Add(this.panel1);
			this.scDBObjectCompare.Panel2.Controls.Add(this.tsMenu);
			this.scDBObjectCompare.Size = new System.Drawing.Size(973, 798);
			this.scDBObjectCompare.SplitterDistance = 419;
			this.scDBObjectCompare.TabIndex = 17;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.gbTableView);
			this.panel2.Controls.Add(this.ucDBObjectSelectTarget);
			this.panel2.Controls.Add(this.ucDBObjectSelectSource);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(419, 798);
			this.panel2.TabIndex = 17;
			// 
			// gbTableView
			// 
			this.gbTableView.Controls.Add(this.panel6);
			this.gbTableView.Controls.Add(this.panel4);
			this.gbTableView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbTableView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbTableView.Location = new System.Drawing.Point(0, 321);
			this.gbTableView.Name = "gbTableView";
			this.gbTableView.Size = new System.Drawing.Size(419, 477);
			this.gbTableView.TabIndex = 4;
			this.gbTableView.TabStop = false;
			this.gbTableView.Text = "Comparison Columns";
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.dgvColumns);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(3, 16);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(413, 458);
			this.panel6.TabIndex = 3;
			// 
			// dgvColumns
			// 
			this.dgvColumns.AllowUserToOrderColumns = true;
			this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvColumns.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvColumns.Location = new System.Drawing.Point(0, 0);
			this.dgvColumns.Name = "dgvColumns";
			this.dgvColumns.RowHeadersVisible = false;
			this.dgvColumns.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvColumns.ShowEditingIcon = false;
			this.dgvColumns.Size = new System.Drawing.Size(413, 458);
			this.dgvColumns.TabIndex = 0;
			// 
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Name";
			this.NameColumn.HeaderText = "Column Name";
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.Width = 300;
			// 
			// panel4
			// 
			this.panel4.AutoSize = true;
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(3, 474);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(413, 0);
			this.panel4.TabIndex = 2;
			// 
			// ucDBObjectSelectTarget
			// 
			this.ucDBObjectSelectTarget.AllowOnlyOneDBSelection = true;
			this.ucDBObjectSelectTarget.Caption = "Target DB";
			this.ucDBObjectSelectTarget.DBContractList = null;
			this.ucDBObjectSelectTarget.DBObjectContractList = null;
			this.ucDBObjectSelectTarget.DBObjectVisibility = false;
			this.ucDBObjectSelectTarget.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDBObjectSelectTarget.IsRequiredConnectionString = true;
			this.ucDBObjectSelectTarget.IsRequiredDB = true;
			this.ucDBObjectSelectTarget.IsRequiredDBObject = true;
			this.ucDBObjectSelectTarget.IsRequiredObjectType = false;
			this.ucDBObjectSelectTarget.IsRequiredSchema = true;
			this.ucDBObjectSelectTarget.Location = new System.Drawing.Point(0, 159);
			this.ucDBObjectSelectTarget.MainForm = null;
			this.ucDBObjectSelectTarget.Name = "ucDBObjectSelectTarget";
			this.ucDBObjectSelectTarget.ObjectTypeVisibility = false;
			this.ucDBObjectSelectTarget.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.ucDBObjectSelectTarget.ParentTabPage = null;
			this.ucDBObjectSelectTarget.SchemaVisibility = false;
			this.ucDBObjectSelectTarget.SelectedConnectionNameValue = null;
			this.ucDBObjectSelectTarget.SelectedDBIndexes = null;
			this.ucDBObjectSelectTarget.SelectedDBObjectObjectId = null;
			this.ucDBObjectSelectTarget.SelectedObjectType = null;
			this.ucDBObjectSelectTarget.SelectedObjectType2 = null;
			this.ucDBObjectSelectTarget.SelectedSchemaId = null;
			this.ucDBObjectSelectTarget.Size = new System.Drawing.Size(419, 162);
			this.ucDBObjectSelectTarget.TabIndex = 3;
			this.ucDBObjectSelectTarget.TabIndexConnectionString = 1;
			this.ucDBObjectSelectTarget.TabIndexDB = 7;
			this.ucDBObjectSelectTarget.TabIndexDBObject = 6;
			this.ucDBObjectSelectTarget.TabIndexDBObjectFilter = 5;
			this.ucDBObjectSelectTarget.TabIndexObjectType = 3;
			this.ucDBObjectSelectTarget.TabIndexSchema = 4;
			this.ucDBObjectSelectTarget.TitleVisibility = null;
			// 
			// ucDBObjectSelectSource
			// 
			this.ucDBObjectSelectSource.AllowOnlyOneDBSelection = true;
			this.ucDBObjectSelectSource.Caption = "Source DB";
			this.ucDBObjectSelectSource.DBContractList = null;
			this.ucDBObjectSelectSource.DBObjectContractList = null;
			this.ucDBObjectSelectSource.DBObjectVisibility = false;
			this.ucDBObjectSelectSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDBObjectSelectSource.IsRequiredConnectionString = true;
			this.ucDBObjectSelectSource.IsRequiredDB = true;
			this.ucDBObjectSelectSource.IsRequiredDBObject = false;
			this.ucDBObjectSelectSource.IsRequiredObjectType = true;
			this.ucDBObjectSelectSource.IsRequiredSchema = true;
			this.ucDBObjectSelectSource.Location = new System.Drawing.Point(0, 0);
			this.ucDBObjectSelectSource.MainForm = null;
			this.ucDBObjectSelectSource.Name = "ucDBObjectSelectSource";
			this.ucDBObjectSelectSource.ObjectTypeVisibility = false;
			this.ucDBObjectSelectSource.ParentTabPage = null;
			this.ucDBObjectSelectSource.SchemaVisibility = false;
			this.ucDBObjectSelectSource.SelectedConnectionNameValue = null;
			this.ucDBObjectSelectSource.SelectedDBIndexes = null;
			this.ucDBObjectSelectSource.SelectedDBObjectObjectId = null;
			this.ucDBObjectSelectSource.SelectedObjectType = null;
			this.ucDBObjectSelectSource.SelectedObjectType2 = null;
			this.ucDBObjectSelectSource.SelectedSchemaId = null;
			this.ucDBObjectSelectSource.Size = new System.Drawing.Size(419, 159);
			this.ucDBObjectSelectSource.TabIndex = 2;
			this.ucDBObjectSelectSource.TabIndexConnectionString = 1;
			this.ucDBObjectSelectSource.TabIndexDB = 7;
			this.ucDBObjectSelectSource.TabIndexDBObject = 6;
			this.ucDBObjectSelectSource.TabIndexDBObjectFilter = 5;
			this.ucDBObjectSelectSource.TabIndexObjectType = 3;
			this.ucDBObjectSelectSource.TabIndexSchema = 4;
			this.ucDBObjectSelectSource.TitleVisibility = null;
			this.ucDBObjectSelectSource.OnDBChanged += new System.EventHandler(this.ucDBObjectSelectSource_OnDBChanged);
			this.ucDBObjectSelectSource.OnDBClear += new System.EventHandler(this.ucDBObjectSelectSource_OnDBClear);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.scDataCompare);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 31);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(550, 767);
			this.panel1.TabIndex = 25;
			// 
			// scDataCompare
			// 
			this.scDataCompare.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scDataCompare.Location = new System.Drawing.Point(0, 0);
			this.scDataCompare.Name = "scDataCompare";
			this.scDataCompare.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scDataCompare.Panel1
			// 
			this.scDataCompare.Panel1.Controls.Add(this.scSqlScript);
			// 
			// scDataCompare.Panel2
			// 
			this.scDataCompare.Panel2.Controls.Add(this.panel3);
			this.scDataCompare.Panel2.Controls.Add(this.toolStrip1);
			this.scDataCompare.Size = new System.Drawing.Size(550, 767);
			this.scDataCompare.SplitterDistance = 300;
			this.scDataCompare.TabIndex = 23;
			// 
			// scSqlScript
			// 
			this.scSqlScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scSqlScript.Location = new System.Drawing.Point(0, 0);
			this.scSqlScript.Name = "scSqlScript";
			this.scSqlScript.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scSqlScript.Panel1
			// 
			this.scSqlScript.Panel1.Controls.Add(this._ucSqlNotePadSource);
			// 
			// scSqlScript.Panel2
			// 
			this.scSqlScript.Panel2.Controls.Add(this._ucSqlNotePadTarget);
			this.scSqlScript.Panel2Collapsed = true;
			this.scSqlScript.Size = new System.Drawing.Size(550, 300);
			this.scSqlScript.SplitterDistance = 150;
			this.scSqlScript.TabIndex = 3;
			// 
			// _ucSqlNotePadSource
			// 
			this._ucSqlNotePadSource.CaseSensitive = false;
			this._ucSqlNotePadSource.CompareTypeVisible = false;
			this._ucSqlNotePadSource.DBObjectContract = null;
			this._ucSqlNotePadSource.DBObjectKeywordList = null;
			this._ucSqlNotePadSource.DisplayFullScreen = true;
			this._ucSqlNotePadSource.DisplayStatus = false;
			this._ucSqlNotePadSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this._ucSqlNotePadSource.FontSize = 12;
			this._ucSqlNotePadSource.IsWraped = true;
			this._ucSqlNotePadSource.Location = new System.Drawing.Point(0, 0);
			this._ucSqlNotePadSource.MainForm = null;
			this._ucSqlNotePadSource.Name = "_ucSqlNotePadSource";
			this._ucSqlNotePadSource.ParentTabPage = null;
			this._ucSqlNotePadSource.SchemaKeywordList = null;
			this._ucSqlNotePadSource.scoSqlNotepadPanel2Collapsed = true;
			this._ucSqlNotePadSource.SearchKeyword = "";
			this._ucSqlNotePadSource.Size = new System.Drawing.Size(550, 300);
			this._ucSqlNotePadSource.TabIndex = 3;
			this._ucSqlNotePadSource.Title = "SQL Script - Source";
			// 
			// _ucSqlNotePadTarget
			// 
			this._ucSqlNotePadTarget.CaseSensitive = false;
			this._ucSqlNotePadTarget.CompareTypeVisible = false;
			this._ucSqlNotePadTarget.DBObjectContract = null;
			this._ucSqlNotePadTarget.DBObjectKeywordList = null;
			this._ucSqlNotePadTarget.DisplayFullScreen = true;
			this._ucSqlNotePadTarget.DisplayStatus = false;
			this._ucSqlNotePadTarget.Dock = System.Windows.Forms.DockStyle.Fill;
			this._ucSqlNotePadTarget.FontSize = 12;
			this._ucSqlNotePadTarget.IsWraped = true;
			this._ucSqlNotePadTarget.Location = new System.Drawing.Point(0, 0);
			this._ucSqlNotePadTarget.MainForm = null;
			this._ucSqlNotePadTarget.Name = "_ucSqlNotePadTarget";
			this._ucSqlNotePadTarget.ParentTabPage = null;
			this._ucSqlNotePadTarget.SchemaKeywordList = null;
			this._ucSqlNotePadTarget.scoSqlNotepadPanel2Collapsed = true;
			this._ucSqlNotePadTarget.SearchKeyword = "";
			this._ucSqlNotePadTarget.Size = new System.Drawing.Size(150, 46);
			this._ucSqlNotePadTarget.TabIndex = 4;
			this._ucSqlNotePadTarget.Title = "SQL Script - Target";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel5);
			this.panel3.Controls.Add(this.ssStatus);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 31);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(550, 432);
			this.panel3.TabIndex = 16;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.dgvCompareResult);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(0, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(550, 410);
			this.panel5.TabIndex = 18;
			// 
			// dgvCompareResult
			// 
			this.dgvCompareResult.AllowUserToAddRows = false;
			this.dgvCompareResult.AllowUserToDeleteRows = false;
			this.dgvCompareResult.AllowUserToOrderColumns = true;
			this.dgvCompareResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvCompareResult.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvCompareResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvCompareResult.EnableHeadersVisualStyles = false;
			this.dgvCompareResult.FilterAndSortEnabled = true;
			this.dgvCompareResult.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvCompareResult.Location = new System.Drawing.Point(0, 0);
			this.dgvCompareResult.MaxFilterButtonImageHeight = 23;
			this.dgvCompareResult.Name = "dgvCompareResult";
			this.dgvCompareResult.ReadOnly = true;
			this.dgvCompareResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvCompareResult.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvCompareResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dgvCompareResult.Size = new System.Drawing.Size(550, 410);
			this.dgvCompareResult.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvCompareResult.TabIndex = 1;
			// 
			// ssStatus
			// 
			this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.ssStatus.Location = new System.Drawing.Point(0, 410);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new System.Drawing.Size(550, 22);
			this.ssStatus.TabIndex = 17;
			this.ssStatus.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 17);
			this.lblStatus.Text = " ";
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEqual,
            this.tsbNotEqual,
            this.tsbSourceExists,
            this.tsbTargetExists,
            this.toolStripSeparator4,
            this.tsbDisplayOnlyNotEqualColumns,
            this.toolStripSeparator1,
            this.tsbExportToExcel});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(550, 31);
			this.toolStrip1.TabIndex = 15;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbEqual
			// 
			this.tsbEqual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbEqual.Image = global::NSqlTools.UI.Properties.Resources.Equality_Equal;
			this.tsbEqual.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbEqual.Name = "tsbEqual";
			this.tsbEqual.Size = new System.Drawing.Size(28, 28);
			this.tsbEqual.Text = "Equal";
			this.tsbEqual.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// tsbNotEqual
			// 
			this.tsbNotEqual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNotEqual.Image = global::NSqlTools.UI.Properties.Resources.Equality_NotEqual;
			this.tsbNotEqual.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNotEqual.Name = "tsbNotEqual";
			this.tsbNotEqual.Size = new System.Drawing.Size(28, 28);
			this.tsbNotEqual.Text = "Not Equal";
			this.tsbNotEqual.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// tsbSourceExists
			// 
			this.tsbSourceExists.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSourceExists.Image = global::NSqlTools.UI.Properties.Resources.Equality_SourceExists;
			this.tsbSourceExists.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSourceExists.Name = "tsbSourceExists";
			this.tsbSourceExists.Size = new System.Drawing.Size(28, 28);
			this.tsbSourceExists.Text = "Exists In Source";
			this.tsbSourceExists.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// tsbTargetExists
			// 
			this.tsbTargetExists.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbTargetExists.Image = global::NSqlTools.UI.Properties.Resources.Equality_TargetExists;
			this.tsbTargetExists.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbTargetExists.Name = "tsbTargetExists";
			this.tsbTargetExists.Size = new System.Drawing.Size(28, 28);
			this.tsbTargetExists.Text = "Exists In Target";
			this.tsbTargetExists.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbDisplayOnlyNotEqualColumns
			// 
			this.tsbDisplayOnlyNotEqualColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDisplayOnlyNotEqualColumns.Image = global::NSqlTools.UI.Properties.Resources.DisplayNotEqualColumns;
			this.tsbDisplayOnlyNotEqualColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDisplayOnlyNotEqualColumns.Name = "tsbDisplayOnlyNotEqualColumns";
			this.tsbDisplayOnlyNotEqualColumns.Size = new System.Drawing.Size(28, 28);
			this.tsbDisplayOnlyNotEqualColumns.Text = "Display only not equal columns";
			this.tsbDisplayOnlyNotEqualColumns.Click += new System.EventHandler(this.tsbDisplayOnlyNotEqualColumns_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbExportToExcel
			// 
			this.tsbExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbExportToExcel.Image = global::NSqlTools.UI.Properties.Resources.Excel;
			this.tsbExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExportToExcel.Name = "tsbExportToExcel";
			this.tsbExportToExcel.Size = new System.Drawing.Size(28, 28);
			this.tsbExportToExcel.Text = "Export To Excel";
			this.tsbExportToExcel.Click += new System.EventHandler(this.tsbExportToExcel_Click);
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCriteriaCollapse,
            this.toolStripSeparator3,
            this.tsbRunScript,
            this.tsbDiffSql});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(550, 31);
			this.tsMenu.TabIndex = 24;
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
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbRunScript
			// 
			this.tsbRunScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRunScript.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
			this.tsbRunScript.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRunScript.Name = "tsbRunScript";
			this.tsbRunScript.Size = new System.Drawing.Size(28, 28);
			this.tsbRunScript.Text = "Run Source SQL Script";
			this.tsbRunScript.Click += new System.EventHandler(this.tsbRunScript_Click);
			// 
			// tsbDiffSql
			// 
			this.tsbDiffSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDiffSql.Image = global::NSqlTools.UI.Properties.Resources.DifferentSql;
			this.tsbDiffSql.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDiffSql.Name = "tsbDiffSql";
			this.tsbDiffSql.Size = new System.Drawing.Size(28, 28);
			this.tsbDiffSql.Text = "Sql Script Is Different";
			this.tsbDiffSql.Click += new System.EventHandler(this.tsbDiffSql_Click);
			// 
			// scInputSqlScript
			// 
			this.scInputSqlScript.Lexer = ScintillaNET.Lexer.Sql;
			this.scInputSqlScript.Location = new System.Drawing.Point(3, 16);
			this.scInputSqlScript.Name = "scInputSqlScript";
			this.scInputSqlScript.Size = new System.Drawing.Size(544, 60);
			this.scInputSqlScript.TabIndex = 1;
			this.scInputSqlScript.WrapMode = ScintillaNET.WrapMode.Word;
			// 
			// ucDataCompare
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scDBObjectCompare);
			this.Name = "ucDataCompare";
			this.Size = new System.Drawing.Size(973, 798);
			this.scDBObjectCompare.Panel1.ResumeLayout(false);
			this.scDBObjectCompare.Panel2.ResumeLayout(false);
			this.scDBObjectCompare.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.scDBObjectCompare)).EndInit();
			this.scDBObjectCompare.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.gbTableView.ResumeLayout(false);
			this.gbTableView.PerformLayout();
			this.panel6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
			this.panel1.ResumeLayout(false);
			this.scDataCompare.Panel1.ResumeLayout(false);
			this.scDataCompare.Panel2.ResumeLayout(false);
			this.scDataCompare.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.scDataCompare)).EndInit();
			this.scDataCompare.ResumeLayout(false);
			this.scSqlScript.Panel1.ResumeLayout(false);
			this.scSqlScript.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scSqlScript)).EndInit();
			this.scSqlScript.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvCompareResult)).EndInit();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scDBObjectCompare;
		private System.Windows.Forms.Panel panel2;
		private ucDBObjectSelect ucDBObjectSelectTarget;
		private ucDBObjectSelect ucDBObjectSelectSource;
		private System.Windows.Forms.SplitContainer scDataCompare;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbCriteriaCollapse;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsbRunScript;
		private ScintillaNET.Scintilla scInputSqlScript;
		private NAdvancedDataGridView dgvCompareResult;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbEqual;
		private System.Windows.Forms.ToolStripButton tsbNotEqual;
		private System.Windows.Forms.ToolStripButton tsbSourceExists;
		private System.Windows.Forms.ToolStripButton tsbTargetExists;
		private System.Windows.Forms.GroupBox gbTableView;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.DataGridView dgvColumns;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton tsbExportToExcel;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbDisplayOnlyNotEqualColumns;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.StatusStrip ssStatus;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripButton tsbDiffSql;
		private System.Windows.Forms.SplitContainer scSqlScript;
		private ucSqlNotePad _ucSqlNotePadSource;
		private ucSqlNotePad _ucSqlNotePadTarget;
	}
}
