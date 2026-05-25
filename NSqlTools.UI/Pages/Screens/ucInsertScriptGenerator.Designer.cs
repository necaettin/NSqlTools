using NSqlTools.Lib.Controls;
using NSqlTools.Types;
using NSqlTools.UI.UserControls;

namespace NSqlTools.UI.Pages
{

	partial class ucInsertScriptGenerator
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucInsertScriptGenerator));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.panel7 = new System.Windows.Forms.Panel();
			this.panel8 = new System.Windows.Forms.Panel();
			this.gbTableView = new System.Windows.Forms.GroupBox();
			this.panel6 = new System.Windows.Forms.Panel();
			this.dgvColumns = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.statusStrip2 = new System.Windows.Forms.StatusStrip();
			this.lblColumns = new System.Windows.Forms.ToolStripStatusLabel();
			this.ucDBObjectSelectControl = new NSqlTools.UI.UserControls.ucDBObjectSelect();
			this.panel1 = new System.Windows.Forms.Panel();
			this.scInputScriptTableColumns = new System.Windows.Forms.SplitContainer();
			this._ucSqlNotePad = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.gbScriptResult = new System.Windows.Forms.GroupBox();
			this.panel5 = new System.Windows.Forms.Panel();
			this.dgvScriptResult = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblScriptResult = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel9 = new System.Windows.Forms.Panel();
			this.gbOutputSqlScript = new System.Windows.Forms.GroupBox();
			this.scOutputSqlScript = new ScintillaNET.Scintilla();
			this.panel4 = new System.Windows.Forms.Panel();
			this.statusStrip3 = new System.Windows.Forms.StatusStrip();
			this.lblOutputSqlScript = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbCriteriaCollapse = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbWriteSourceSqlScript = new System.Windows.Forms.ToolStripButton();
			this.tsbRunScript = new System.Windows.Forms.ToolStripButton();
			this.tsbCreateInsertScripts = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.tsbExportToExcel = new System.Windows.Forms.ToolStripButton();
			this.tsbImportFromExcel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbWithSquareBrackets = new System.Windows.Forms.ToolStripButton();
			this.tsbSeperate = new System.Windows.Forms.ToolStripButton();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.IsSelectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TypeNameCustomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IsNullableColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.IdentityColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.DefaultValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.panel7.SuspendLayout();
			this.panel8.SuspendLayout();
			this.gbTableView.SuspendLayout();
			this.panel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
			this.panel3.SuspendLayout();
			this.statusStrip2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scInputScriptTableColumns)).BeginInit();
			this.scInputScriptTableColumns.Panel1.SuspendLayout();
			this.scInputScriptTableColumns.Panel2.SuspendLayout();
			this.scInputScriptTableColumns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.gbScriptResult.SuspendLayout();
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvScriptResult)).BeginInit();
			this.panel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.panel9.SuspendLayout();
			this.gbOutputSqlScript.SuspendLayout();
			this.panel4.SuspendLayout();
			this.statusStrip3.SuspendLayout();
			this.tsMenu.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.Location = new System.Drawing.Point(0, 0);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.AutoScroll = true;
			this.scMain.Panel1.Controls.Add(this.panel7);
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.panel1);
			this.scMain.Panel2.Controls.Add(this.tsMenu);
			this.scMain.Size = new System.Drawing.Size(846, 684);
			this.scMain.SplitterDistance = 450;
			this.scMain.TabIndex = 1;
			// 
			// panel7
			// 
			this.panel7.AutoScroll = true;
			this.panel7.Controls.Add(this.panel8);
			this.panel7.Controls.Add(this.ucDBObjectSelectControl);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel7.Location = new System.Drawing.Point(0, 0);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(450, 684);
			this.panel7.TabIndex = 3;
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.gbTableView);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel8.Location = new System.Drawing.Point(0, 318);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(450, 366);
			this.panel8.TabIndex = 3;
			// 
			// gbTableView
			// 
			this.gbTableView.Controls.Add(this.panel6);
			this.gbTableView.Controls.Add(this.panel3);
			this.gbTableView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbTableView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbTableView.Location = new System.Drawing.Point(0, 0);
			this.gbTableView.Name = "gbTableView";
			this.gbTableView.Size = new System.Drawing.Size(450, 366);
			this.gbTableView.TabIndex = 2;
			this.gbTableView.TabStop = false;
			this.gbTableView.Text = "Table Columns";
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.dgvColumns);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(3, 16);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(444, 325);
			this.panel6.TabIndex = 3;
			// 
			// dgvColumns
			// 
			this.dgvColumns.AllowUserToAddRows = false;
			this.dgvColumns.AllowUserToDeleteRows = false;
			this.dgvColumns.AllowUserToOrderColumns = true;
			this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsSelectedColumn,
            this.NameColumn,
            this.TypeNameCustomColumn,
            this.IsNullableColumn,
            this.IdentityColumn,
            this.DefaultValueColumn});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvColumns.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvColumns.EnableHeadersVisualStyles = false;
			this.dgvColumns.FilterAndSortEnabled = true;
			this.dgvColumns.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvColumns.Location = new System.Drawing.Point(0, 0);
			this.dgvColumns.MaxFilterButtonImageHeight = 23;
			this.dgvColumns.Name = "dgvColumns";
			this.dgvColumns.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dgvColumns.RowHeadersVisible = false;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvColumns.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvColumns.ShowEditingIcon = false;
			this.dgvColumns.Size = new System.Drawing.Size(444, 325);
			this.dgvColumns.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvColumns.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.AutoSize = true;
			this.panel3.Controls.Add(this.statusStrip2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(3, 341);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(444, 22);
			this.panel3.TabIndex = 2;
			// 
			// statusStrip2
			// 
			this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblColumns});
			this.statusStrip2.Location = new System.Drawing.Point(0, 0);
			this.statusStrip2.Name = "statusStrip2";
			this.statusStrip2.Size = new System.Drawing.Size(444, 22);
			this.statusStrip2.TabIndex = 0;
			this.statusStrip2.Text = "statusStrip2";
			// 
			// lblColumns
			// 
			this.lblColumns.Name = "lblColumns";
			this.lblColumns.Size = new System.Drawing.Size(10, 17);
			this.lblColumns.Text = " ";
			// 
			// ucDBObjectSelectControl
			// 
			this.ucDBObjectSelectControl.AllowOnlyOneDBSelection = true;
			this.ucDBObjectSelectControl.Caption = "Table Select";
			this.ucDBObjectSelectControl.DBContractList = null;
			this.ucDBObjectSelectControl.DBObjectContractList = null;
			this.ucDBObjectSelectControl.DBObjectVisibility = true;
			this.ucDBObjectSelectControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDBObjectSelectControl.IsRequiredConnectionString = true;
			this.ucDBObjectSelectControl.IsRequiredDB = true;
			this.ucDBObjectSelectControl.IsRequiredDBObject = true;
			this.ucDBObjectSelectControl.IsRequiredObjectType = false;
			this.ucDBObjectSelectControl.IsRequiredSchema = true;
			this.ucDBObjectSelectControl.Location = new System.Drawing.Point(0, 0);
			this.ucDBObjectSelectControl.MainForm = null;
			this.ucDBObjectSelectControl.Name = "ucDBObjectSelectControl";
			this.ucDBObjectSelectControl.ObjectTypeVisibility = false;
			this.ucDBObjectSelectControl.ParentTabPage = null;
			this.ucDBObjectSelectControl.SchemaVisibility = true;
			this.ucDBObjectSelectControl.SelectedConnectionNameValue = null;
			this.ucDBObjectSelectControl.SelectedDBIndexes = null;
			this.ucDBObjectSelectControl.SelectedDBObjectObjectId = null;
			this.ucDBObjectSelectControl.SelectedObjectType = null;
			this.ucDBObjectSelectControl.SelectedObjectType2 = null;
			this.ucDBObjectSelectControl.SelectedSchemaId = null;
			this.ucDBObjectSelectControl.Size = new System.Drawing.Size(450, 318);
			this.ucDBObjectSelectControl.TabIndex = 1;
			this.ucDBObjectSelectControl.TabIndexConnectionString = 1;
			this.ucDBObjectSelectControl.TabIndexDB = 2;
			this.ucDBObjectSelectControl.TabIndexDBObject = 6;
			this.ucDBObjectSelectControl.TabIndexDBObjectFilter = 5;
			this.ucDBObjectSelectControl.TabIndexObjectType = 3;
			this.ucDBObjectSelectControl.TabIndexSchema = 4;
			this.ucDBObjectSelectControl.TitleVisibility = null;
			this.ucDBObjectSelectControl.OnDBObjectChanged += new System.EventHandler<NSqlTools.Types.DBObjectChangedEventArgs>(this.ucDBObjectSelect_OnDBObjectChanged);
			this.ucDBObjectSelectControl.OnDBObjectClear += new System.EventHandler(this.ucDBObjectSelect_OnDBObjectClear);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.scInputScriptTableColumns);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 31);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(392, 653);
			this.panel1.TabIndex = 17;
			// 
			// scInputScriptTableColumns
			// 
			this.scInputScriptTableColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scInputScriptTableColumns.Location = new System.Drawing.Point(0, 0);
			this.scInputScriptTableColumns.Name = "scInputScriptTableColumns";
			this.scInputScriptTableColumns.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scInputScriptTableColumns.Panel1
			// 
			this.scInputScriptTableColumns.Panel1.Controls.Add(this._ucSqlNotePad);
			// 
			// scInputScriptTableColumns.Panel2
			// 
			this.scInputScriptTableColumns.Panel2.Controls.Add(this.splitContainer1);
			this.scInputScriptTableColumns.Size = new System.Drawing.Size(392, 653);
			this.scInputScriptTableColumns.SplitterDistance = 120;
			this.scInputScriptTableColumns.TabIndex = 1;
			// 
			// _ucSqlNotePad
			// 
			this._ucSqlNotePad.CaseSensitive = false;
			this._ucSqlNotePad.CompareTypeVisible = false;
			this._ucSqlNotePad.DBObjectContract = null;
			this._ucSqlNotePad.DBObjectKeywordList = null;
			this._ucSqlNotePad.DisplayFullScreen = true;
			this._ucSqlNotePad.DisplayStatus = false;
			this._ucSqlNotePad.Dock = System.Windows.Forms.DockStyle.Fill;
			this._ucSqlNotePad.FontSize = 12;
			this._ucSqlNotePad.IsWraped = true;
			this._ucSqlNotePad.Location = new System.Drawing.Point(0, 0);
			this._ucSqlNotePad.MainForm = null;
			this._ucSqlNotePad.Name = "_ucSqlNotePad";
			this._ucSqlNotePad.ParentTabPage = null;
			this._ucSqlNotePad.SchemaKeywordList = null;
			this._ucSqlNotePad.scoSqlNotepadPanel2Collapsed = true;
			this._ucSqlNotePad.SearchKeyword = "";
			this._ucSqlNotePad.Size = new System.Drawing.Size(392, 120);
			this._ucSqlNotePad.TabIndex = 0;
			this._ucSqlNotePad.Title = "Source SQL Script";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gbScriptResult);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel9);
			this.splitContainer1.Panel2.Controls.Add(this.panel4);
			this.splitContainer1.Size = new System.Drawing.Size(392, 529);
			this.splitContainer1.SplitterDistance = 263;
			this.splitContainer1.TabIndex = 1;
			// 
			// gbScriptResult
			// 
			this.gbScriptResult.Controls.Add(this.panel5);
			this.gbScriptResult.Controls.Add(this.panel2);
			this.gbScriptResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbScriptResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbScriptResult.Location = new System.Drawing.Point(0, 0);
			this.gbScriptResult.Name = "gbScriptResult";
			this.gbScriptResult.Size = new System.Drawing.Size(392, 263);
			this.gbScriptResult.TabIndex = 0;
			this.gbScriptResult.TabStop = false;
			this.gbScriptResult.Text = "Source SQL Script Result";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.dgvScriptResult);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(3, 16);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(386, 222);
			this.panel5.TabIndex = 2;
			// 
			// dgvScriptResult
			// 
			this.dgvScriptResult.AllowUserToOrderColumns = true;
			this.dgvScriptResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvScriptResult.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvScriptResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvScriptResult.EnableHeadersVisualStyles = false;
			this.dgvScriptResult.FilterAndSortEnabled = true;
			this.dgvScriptResult.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvScriptResult.Location = new System.Drawing.Point(0, 0);
			this.dgvScriptResult.MaxFilterButtonImageHeight = 23;
			this.dgvScriptResult.Name = "dgvScriptResult";
			this.dgvScriptResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvScriptResult.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvScriptResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvScriptResult.Size = new System.Drawing.Size(386, 222);
			this.dgvScriptResult.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvScriptResult.TabIndex = 0;
			this.dgvScriptResult.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvScriptResult_CellValidating);
			this.dgvScriptResult.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvScriptResult_DataError);
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.statusStrip1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(3, 238);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(386, 22);
			this.panel2.TabIndex = 1;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblScriptResult});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(386, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblScriptResult
			// 
			this.lblScriptResult.Name = "lblScriptResult";
			this.lblScriptResult.Size = new System.Drawing.Size(10, 17);
			this.lblScriptResult.Text = " ";
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.gbOutputSqlScript);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel9.Location = new System.Drawing.Point(0, 0);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(392, 240);
			this.panel9.TabIndex = 3;
			// 
			// gbOutputSqlScript
			// 
			this.gbOutputSqlScript.Controls.Add(this.scOutputSqlScript);
			this.gbOutputSqlScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbOutputSqlScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbOutputSqlScript.Location = new System.Drawing.Point(0, 0);
			this.gbOutputSqlScript.Name = "gbOutputSqlScript";
			this.gbOutputSqlScript.Size = new System.Drawing.Size(392, 240);
			this.gbOutputSqlScript.TabIndex = 0;
			this.gbOutputSqlScript.TabStop = false;
			this.gbOutputSqlScript.Text = "Generated Insert Scripts";
			// 
			// scOutputSqlScript
			// 
			this.scOutputSqlScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scOutputSqlScript.Location = new System.Drawing.Point(3, 16);
			this.scOutputSqlScript.Name = "scOutputSqlScript";
			this.scOutputSqlScript.Size = new System.Drawing.Size(386, 221);
			this.scOutputSqlScript.TabIndex = 1;
			this.scOutputSqlScript.Text = "scintilla2";
			this.scOutputSqlScript.WrapMode = ScintillaNET.WrapMode.Word;
			// 
			// panel4
			// 
			this.panel4.AutoSize = true;
			this.panel4.Controls.Add(this.statusStrip3);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(0, 240);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(392, 22);
			this.panel4.TabIndex = 2;
			// 
			// statusStrip3
			// 
			this.statusStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblOutputSqlScript});
			this.statusStrip3.Location = new System.Drawing.Point(0, 0);
			this.statusStrip3.Name = "statusStrip3";
			this.statusStrip3.Size = new System.Drawing.Size(392, 22);
			this.statusStrip3.TabIndex = 0;
			this.statusStrip3.Text = "statusStrip3";
			// 
			// lblOutputSqlScript
			// 
			this.lblOutputSqlScript.Name = "lblOutputSqlScript";
			this.lblOutputSqlScript.Size = new System.Drawing.Size(10, 17);
			this.lblOutputSqlScript.Text = " ";
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCriteriaCollapse,
            this.toolStripSeparator3,
            this.tsbWriteSourceSqlScript,
            this.tsbRunScript,
            this.tsbCreateInsertScripts,
            this.toolStripSeparator2,
            this.tsbSave,
            this.tsbExportToExcel,
            this.tsbImportFromExcel,
            this.toolStripSeparator1,
            this.tsbWithSquareBrackets,
            this.tsbSeperate});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(392, 31);
			this.tsMenu.TabIndex = 16;
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
			// tsbWriteSourceSqlScript
			// 
			this.tsbWriteSourceSqlScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbWriteSourceSqlScript.Image = global::NSqlTools.UI.Properties.Resources.Write;
			this.tsbWriteSourceSqlScript.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbWriteSourceSqlScript.Name = "tsbWriteSourceSqlScript";
			this.tsbWriteSourceSqlScript.Size = new System.Drawing.Size(28, 28);
			this.tsbWriteSourceSqlScript.Text = "Write Source Sql Script";
			this.tsbWriteSourceSqlScript.Click += new System.EventHandler(this.tsbWriteSourceSqlScript_Click);
			// 
			// tsbRunScript
			// 
			this.tsbRunScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRunScript.Enabled = false;
			this.tsbRunScript.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
			this.tsbRunScript.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRunScript.Name = "tsbRunScript";
			this.tsbRunScript.Size = new System.Drawing.Size(28, 28);
			this.tsbRunScript.Text = "Run Source SQL Script";
			this.tsbRunScript.Click += new System.EventHandler(this.tsbRunScript_Click);
			// 
			// tsbCreateInsertScripts
			// 
			this.tsbCreateInsertScripts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCreateInsertScripts.Enabled = false;
			this.tsbCreateInsertScripts.Image = global::NSqlTools.UI.Properties.Resources.CreateInsertScripts;
			this.tsbCreateInsertScripts.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCreateInsertScripts.Name = "tsbCreateInsertScripts";
			this.tsbCreateInsertScripts.Size = new System.Drawing.Size(28, 28);
			this.tsbCreateInsertScripts.Text = "Generate insert scripts";
			this.tsbCreateInsertScripts.Click += new System.EventHandler(this.tsbCreateInsertScripts_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.Image = global::NSqlTools.UI.Properties.Resources.Save;
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(28, 28);
			this.tsbSave.Text = "Save generated insert scripts";
			this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
			// 
			// tsbExportToExcel
			// 
			this.tsbExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbExportToExcel.Enabled = false;
			this.tsbExportToExcel.Image = global::NSqlTools.UI.Properties.Resources.Excel;
			this.tsbExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExportToExcel.Name = "tsbExportToExcel";
			this.tsbExportToExcel.Size = new System.Drawing.Size(28, 28);
			this.tsbExportToExcel.Text = "Export To Excel";
			this.tsbExportToExcel.Click += new System.EventHandler(this.tsbExportToExcel_Click);
			// 
			// tsbImportFromExcel
			// 
			this.tsbImportFromExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbImportFromExcel.Enabled = false;
			this.tsbImportFromExcel.Image = global::NSqlTools.UI.Properties.Resources.ExcelImport;
			this.tsbImportFromExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbImportFromExcel.Name = "tsbImportFromExcel";
			this.tsbImportFromExcel.Size = new System.Drawing.Size(28, 28);
			this.tsbImportFromExcel.Text = "Import From Excel";
			this.tsbImportFromExcel.Click += new System.EventHandler(this.tsbImportFromExcel_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbWithSquareBrackets
			// 
			this.tsbWithSquareBrackets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbWithSquareBrackets.Image = global::NSqlTools.UI.Properties.Resources.WithSquareBrackets;
			this.tsbWithSquareBrackets.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbWithSquareBrackets.Name = "tsbWithSquareBrackets";
			this.tsbWithSquareBrackets.Size = new System.Drawing.Size(28, 28);
			this.tsbWithSquareBrackets.Text = "With Square Brackets";
			this.tsbWithSquareBrackets.Click += new System.EventHandler(this.tsbNoSquareBrackets_Click);
			// 
			// tsbSeperate
			// 
			this.tsbSeperate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSeperate.Image = global::NSqlTools.UI.Properties.Resources.InserScriptSeperate;
			this.tsbSeperate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSeperate.Name = "tsbSeperate";
			this.tsbSeperate.Size = new System.Drawing.Size(28, 28);
			this.tsbSeperate.Text = "Create Insert Scripts Seperately";
			this.tsbSeperate.Click += new System.EventHandler(this.tsbSeperate_Click);
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.scMain);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(846, 684);
			this.pnlMain.TabIndex = 16;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Text files (*.txt)|*.txt|Sql files (*.sql)|*.sql|All files (*.*)|*.*";
			// 
			// IsSelectedColumn
			// 
			this.IsSelectedColumn.DataPropertyName = "IsSelected";
			this.IsSelectedColumn.HeaderText = "Select";
			this.IsSelectedColumn.MinimumWidth = 24;
			this.IsSelectedColumn.Name = "IsSelectedColumn";
			this.IsSelectedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IsSelectedColumn.Width = 51;
			// 
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Name";
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.MinimumWidth = 24;
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.NameColumn.Width = 120;
			// 
			// TypeNameCustomColumn
			// 
			this.TypeNameCustomColumn.DataPropertyName = "TypeNameCustom";
			this.TypeNameCustomColumn.HeaderText = "Type";
			this.TypeNameCustomColumn.MinimumWidth = 24;
			this.TypeNameCustomColumn.Name = "TypeNameCustomColumn";
			this.TypeNameCustomColumn.ReadOnly = true;
			this.TypeNameCustomColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.TypeNameCustomColumn.Width = 80;
			// 
			// IsNullableColumn
			// 
			this.IsNullableColumn.DataPropertyName = "IsNullable";
			this.IsNullableColumn.HeaderText = "Null";
			this.IsNullableColumn.MinimumWidth = 24;
			this.IsNullableColumn.Name = "IsNullableColumn";
			this.IsNullableColumn.ReadOnly = true;
			this.IsNullableColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IsNullableColumn.Width = 50;
			// 
			// IdentityColumn
			// 
			this.IdentityColumn.DataPropertyName = "IsIdentity";
			this.IdentityColumn.HeaderText = "Ident.";
			this.IdentityColumn.MinimumWidth = 24;
			this.IdentityColumn.Name = "IdentityColumn";
			this.IdentityColumn.ReadOnly = true;
			this.IdentityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IdentityColumn.Width = 50;
			// 
			// DefaultValueColumn
			// 
			this.DefaultValueColumn.DataPropertyName = "DefaultValue";
			this.DefaultValueColumn.HeaderText = "Default Value";
			this.DefaultValueColumn.MinimumWidth = 24;
			this.DefaultValueColumn.Name = "DefaultValueColumn";
			this.DefaultValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// ucInsertScriptGenerator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlMain);
			this.Name = "ucInsertScriptGenerator";
			this.Size = new System.Drawing.Size(846, 684);
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			this.scMain.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
			this.scMain.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.panel8.ResumeLayout(false);
			this.gbTableView.ResumeLayout(false);
			this.gbTableView.PerformLayout();
			this.panel6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.statusStrip2.ResumeLayout(false);
			this.statusStrip2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.scInputScriptTableColumns.Panel1.ResumeLayout(false);
			this.scInputScriptTableColumns.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scInputScriptTableColumns)).EndInit();
			this.scInputScriptTableColumns.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.gbScriptResult.ResumeLayout(false);
			this.gbScriptResult.PerformLayout();
			this.panel5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvScriptResult)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel9.ResumeLayout(false);
			this.gbOutputSqlScript.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.statusStrip3.ResumeLayout(false);
			this.statusStrip3.PerformLayout();
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer scMain;
		private ucDBObjectSelect ucDBObjectSelectControl;
		private System.Windows.Forms.SplitContainer scInputScriptTableColumns;
		private System.Windows.Forms.GroupBox gbOutputSqlScript;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbCreateInsertScripts;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox gbScriptResult;
		private NAdvancedDataGridView dgvScriptResult;
		private System.Windows.Forms.ToolStripButton tsbRunScript;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbWithSquareBrackets;
		private System.Windows.Forms.GroupBox gbTableView;
		private NAdvancedDataGridView dgvColumns;
		private ScintillaNET.Scintilla scOutputSqlScript;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.StatusStrip statusStrip2;
		private System.Windows.Forms.ToolStripStatusLabel lblColumns;
		private System.Windows.Forms.ToolStripStatusLabel lblScriptResult;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.StatusStrip statusStrip3;
		private System.Windows.Forms.ToolStripStatusLabel lblOutputSqlScript;
		private System.Windows.Forms.ToolStripButton tsbCriteriaCollapse;
		public System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.ToolStripButton tsbExportToExcel;
		private System.Windows.Forms.ToolStripButton tsbImportFromExcel;
		private System.Windows.Forms.ToolStripButton tsbWriteSourceSqlScript;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsbSeperate;
		private ucSqlNotePad _ucSqlNotePad;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsSelectedColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn TypeNameCustomColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsNullableColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IdentityColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DefaultValueColumn;
	}
}
