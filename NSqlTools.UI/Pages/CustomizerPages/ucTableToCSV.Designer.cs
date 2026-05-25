using NSqlTools.Lib.Controls;
using NSqlTools.Types;
using NSqlTools.Types.Properties;
using NSqlTools.UI.UserControls;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	partial class ucTableToCSV
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTableToCSV));
			this.scoQuery = new System.Windows.Forms.SplitContainer();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbCreateZip = new System.Windows.Forms.CheckBox();
			this.btnPathSelect = new System.Windows.Forms.Button();
			this.nudCommandTimeout = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.cbAddTableColumnsToCSV = new System.Windows.Forms.CheckBox();
			this.nudThreadCount = new System.Windows.Forms.NumericUpDown();
			this.lblThreadCount = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.lblPath = new System.Windows.Forms.Label();
			this.cbOnlyNotEmptyTables = new System.Windows.Forms.CheckBox();
			this.ucDBObjectSelectControl = new NSqlTools.UI.UserControls.ucDBObjectSelect();
			this.scQueryAndResult = new System.Windows.Forms.SplitContainer();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.dgvDBProgress = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.panel6 = new System.Windows.Forms.Panel();
			this.dgvTables = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbCompleted = new System.Windows.Forms.ToolStripButton();
			this.tsbNotCompleted = new System.Windows.Forms.ToolStripButton();
			this.tsbRunning = new System.Windows.Forms.ToolStripButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtLogs = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbRun = new System.Windows.Forms.ToolStripButton();
			this.tsbStop = new System.Windows.Forms.ToolStripButton();
			this.pnlQueryResults = new System.Windows.Forms.Panel();
			this.tcQueryResults = new System.Windows.Forms.TabControl();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.DBNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ProgressColumn = new NSqlTools.Lib.Controls.DataGridViewProgressColumn();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.scoQuery)).BeginInit();
			this.scoQuery.Panel1.SuspendLayout();
			this.scoQuery.Panel2.SuspendLayout();
			this.scoQuery.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCommandTimeout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudThreadCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.scQueryAndResult)).BeginInit();
			this.scQueryAndResult.Panel1.SuspendLayout();
			this.scQueryAndResult.Panel2.SuspendLayout();
			this.scQueryAndResult.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvDBProgress)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.panel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tsMenu.SuspendLayout();
			this.pnlQueryResults.SuspendLayout();
			this.SuspendLayout();
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
			// 
			// scoQuery.Panel2
			// 
			this.scoQuery.Panel2.Controls.Add(this.scQueryAndResult);
			this.scoQuery.Size = new System.Drawing.Size(834, 635);
			this.scoQuery.SplitterDistance = 250;
			this.scoQuery.TabIndex = 2;
			// 
			// panel4
			// 
			this.panel4.AutoScroll = true;
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Controls.Add(this.ucDBObjectSelectControl);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(250, 635);
			this.panel4.TabIndex = 1;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.groupBox1);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 160);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(250, 219);
			this.panel5.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbCreateZip);
			this.groupBox1.Controls.Add(this.btnPathSelect);
			this.groupBox1.Controls.Add(this.nudCommandTimeout);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cbAddTableColumnsToCSV);
			this.groupBox1.Controls.Add(this.nudThreadCount);
			this.groupBox1.Controls.Add(this.lblThreadCount);
			this.groupBox1.Controls.Add(this.txtPath);
			this.groupBox1.Controls.Add(this.lblPath);
			this.groupBox1.Controls.Add(this.cbOnlyNotEmptyTables);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(250, 219);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Criteria";
			// 
			// cbCreateZip
			// 
			this.cbCreateZip.AutoSize = true;
			this.cbCreateZip.Checked = true;
			this.cbCreateZip.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCreateZip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.cbCreateZip.Location = new System.Drawing.Point(9, 65);
			this.cbCreateZip.Name = "cbCreateZip";
			this.cbCreateZip.Size = new System.Drawing.Size(75, 17);
			this.cbCreateZip.TabIndex = 11;
			this.cbCreateZip.Text = "Create Zip";
			this.cbCreateZip.UseVisualStyleBackColor = true;
			// 
			// btnPathSelect
			// 
			this.btnPathSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPathSelect.Location = new System.Drawing.Point(220, 105);
			this.btnPathSelect.Name = "btnPathSelect";
			this.btnPathSelect.Size = new System.Drawing.Size(24, 22);
			this.btnPathSelect.TabIndex = 10;
			this.btnPathSelect.Text = "...";
			this.btnPathSelect.UseVisualStyleBackColor = true;
			this.btnPathSelect.Click += new System.EventHandler(this.btnPathSelect_Click);
			// 
			// nudCommandTimeout
			// 
			this.nudCommandTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nudCommandTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.nudCommandTimeout.Location = new System.Drawing.Point(9, 189);
			this.nudCommandTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudCommandTimeout.Name = "nudCommandTimeout";
			this.nudCommandTimeout.Size = new System.Drawing.Size(235, 20);
			this.nudCommandTimeout.TabIndex = 9;
			this.nudCommandTimeout.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label1.Location = new System.Drawing.Point(6, 173);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Command Timeout";
			// 
			// cbAddTableColumnsToCSV
			// 
			this.cbAddTableColumnsToCSV.AutoSize = true;
			this.cbAddTableColumnsToCSV.Checked = true;
			this.cbAddTableColumnsToCSV.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAddTableColumnsToCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.cbAddTableColumnsToCSV.Location = new System.Drawing.Point(9, 42);
			this.cbAddTableColumnsToCSV.Name = "cbAddTableColumnsToCSV";
			this.cbAddTableColumnsToCSV.Size = new System.Drawing.Size(149, 17);
			this.cbAddTableColumnsToCSV.TabIndex = 7;
			this.cbAddTableColumnsToCSV.Text = "Add table columns to CSV";
			this.cbAddTableColumnsToCSV.UseVisualStyleBackColor = true;
			// 
			// nudThreadCount
			// 
			this.nudThreadCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nudThreadCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.nudThreadCount.Location = new System.Drawing.Point(9, 146);
			this.nudThreadCount.Name = "nudThreadCount";
			this.nudThreadCount.Size = new System.Drawing.Size(235, 20);
			this.nudThreadCount.TabIndex = 6;
			this.nudThreadCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// lblThreadCount
			// 
			this.lblThreadCount.AutoSize = true;
			this.lblThreadCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblThreadCount.Location = new System.Drawing.Point(6, 130);
			this.lblThreadCount.Name = "lblThreadCount";
			this.lblThreadCount.Size = new System.Drawing.Size(72, 13);
			this.lblThreadCount.TabIndex = 4;
			this.lblThreadCount.Text = "Thread Count";
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.txtPath.Location = new System.Drawing.Point(9, 106);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(209, 20);
			this.txtPath.TabIndex = 3;
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblPath.Location = new System.Drawing.Point(6, 89);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(29, 13);
			this.lblPath.TabIndex = 2;
			this.lblPath.Text = "Path";
			// 
			// cbOnlyNotEmptyTables
			// 
			this.cbOnlyNotEmptyTables.AutoSize = true;
			this.cbOnlyNotEmptyTables.Checked = true;
			this.cbOnlyNotEmptyTables.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbOnlyNotEmptyTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.cbOnlyNotEmptyTables.Location = new System.Drawing.Point(9, 19);
			this.cbOnlyNotEmptyTables.Name = "cbOnlyNotEmptyTables";
			this.cbOnlyNotEmptyTables.Size = new System.Drawing.Size(127, 17);
			this.cbOnlyNotEmptyTables.TabIndex = 1;
			this.cbOnlyNotEmptyTables.Text = "Only not empty tables";
			this.cbOnlyNotEmptyTables.UseVisualStyleBackColor = true;
			// 
			// ucDBObjectSelectControl
			// 
			this.ucDBObjectSelectControl.AllowOnlyOneDBSelection = false;
			this.ucDBObjectSelectControl.Caption = "DB Select";
			this.ucDBObjectSelectControl.DBContractList = null;
			this.ucDBObjectSelectControl.DBObjectContractList = null;
			this.ucDBObjectSelectControl.DBObjectVisibility = false;
			this.ucDBObjectSelectControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucDBObjectSelectControl.IsRequiredConnectionString = true;
			this.ucDBObjectSelectControl.IsRequiredDB = true;
			this.ucDBObjectSelectControl.IsRequiredDBObject = false;
			this.ucDBObjectSelectControl.IsRequiredObjectType = false;
			this.ucDBObjectSelectControl.IsRequiredSchema = false;
			this.ucDBObjectSelectControl.Location = new System.Drawing.Point(0, 0);
			this.ucDBObjectSelectControl.MainForm = null;
			this.ucDBObjectSelectControl.Name = "ucDBObjectSelectControl";
			this.ucDBObjectSelectControl.ObjectTypeVisibility = false;
			this.ucDBObjectSelectControl.ParentTabPage = null;
			this.ucDBObjectSelectControl.SchemaVisibility = false;
			this.ucDBObjectSelectControl.SelectedConnectionNameValue = null;
			this.ucDBObjectSelectControl.SelectedDBIndexes = null;
			this.ucDBObjectSelectControl.SelectedDBObjectObjectId = null;
			this.ucDBObjectSelectControl.SelectedObjectType = null;
			this.ucDBObjectSelectControl.SelectedObjectType2 = null;
			this.ucDBObjectSelectControl.SelectedSchemaId = null;
			this.ucDBObjectSelectControl.Size = new System.Drawing.Size(250, 160);
			this.ucDBObjectSelectControl.TabIndex = 1;
			this.ucDBObjectSelectControl.TabIndexConnectionString = 1;
			this.ucDBObjectSelectControl.TabIndexDB = 7;
			this.ucDBObjectSelectControl.TabIndexDBObject = 6;
			this.ucDBObjectSelectControl.TabIndexDBObjectFilter = 5;
			this.ucDBObjectSelectControl.TabIndexObjectType = 3;
			this.ucDBObjectSelectControl.TabIndexSchema = 4;
			this.ucDBObjectSelectControl.TitleVisibility = null;
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
			this.scQueryAndResult.Size = new System.Drawing.Size(580, 635);
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
			this.panel3.Size = new System.Drawing.Size(580, 635);
			this.panel3.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.splitContainer1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 31);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(580, 604);
			this.panel1.TabIndex = 2;
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
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Size = new System.Drawing.Size(580, 604);
			this.splitContainer1.SplitterDistance = 302;
			this.splitContainer1.TabIndex = 2;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
			this.splitContainer2.Size = new System.Drawing.Size(580, 302);
			this.splitContainer2.SplitterDistance = 151;
			this.splitContainer2.TabIndex = 4;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.dgvDBProgress);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.groupBox4.Location = new System.Drawing.Point(0, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(580, 151);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "DB List";
			// 
			// dgvDBProgress
			// 
			this.dgvDBProgress.AllowUserToAddRows = false;
			this.dgvDBProgress.AllowUserToDeleteRows = false;
			this.dgvDBProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvDBProgress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DBNameColumn,
            this.ProgressColumn});
			this.dgvDBProgress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvDBProgress.EnableHeadersVisualStyles = false;
			this.dgvDBProgress.FilterAndSortEnabled = true;
			this.dgvDBProgress.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvDBProgress.Location = new System.Drawing.Point(3, 16);
			this.dgvDBProgress.MaxFilterButtonImageHeight = 23;
			this.dgvDBProgress.Name = "dgvDBProgress";
			this.dgvDBProgress.ReadOnly = true;
			this.dgvDBProgress.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dgvDBProgress.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.dgvDBProgress.Size = new System.Drawing.Size(574, 132);
			this.dgvDBProgress.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvDBProgress.TabIndex = 2;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.panel6);
			this.groupBox3.Controls.Add(this.toolStrip1);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(580, 147);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Tables";
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.dgvTables);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(3, 47);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(574, 97);
			this.panel6.TabIndex = 15;
			// 
			// dgvTables
			// 
			this.dgvTables.AllowUserToAddRows = false;
			this.dgvTables.AllowUserToDeleteRows = false;
			this.dgvTables.AllowUserToOrderColumns = true;
			this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.StatusColumn});
			this.dgvTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvTables.EnableHeadersVisualStyles = false;
			this.dgvTables.FilterAndSortEnabled = true;
			this.dgvTables.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTables.Location = new System.Drawing.Point(0, 0);
			this.dgvTables.MaxFilterButtonImageHeight = 23;
			this.dgvTables.Name = "dgvTables";
			this.dgvTables.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dgvTables.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.dgvTables.Size = new System.Drawing.Size(574, 97);
			this.dgvTables.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTables.TabIndex = 2;
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCompleted,
            this.tsbNotCompleted,
            this.tsbRunning});
			this.toolStrip1.Location = new System.Drawing.Point(3, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(574, 31);
			this.toolStrip1.TabIndex = 14;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbCompleted
			// 
			this.tsbCompleted.CheckOnClick = true;
			this.tsbCompleted.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCompleted.Image = global::NSqlTools.UI.Properties.Resources.Ok;
			this.tsbCompleted.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCompleted.Name = "tsbCompleted";
			this.tsbCompleted.Size = new System.Drawing.Size(28, 28);
			this.tsbCompleted.Text = "Completed";
			this.tsbCompleted.Click += new System.EventHandler(this.tsbCompleted_Click);
			// 
			// tsbNotCompleted
			// 
			this.tsbNotCompleted.CheckOnClick = true;
			this.tsbNotCompleted.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNotCompleted.Image = global::NSqlTools.UI.Properties.Resources.NotCompleted;
			this.tsbNotCompleted.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNotCompleted.Name = "tsbNotCompleted";
			this.tsbNotCompleted.Size = new System.Drawing.Size(28, 28);
			this.tsbNotCompleted.Text = "Not Completed";
			this.tsbNotCompleted.Click += new System.EventHandler(this.tsbNotCompleted_Click);
			// 
			// tsbRunning
			// 
			this.tsbRunning.CheckOnClick = true;
			this.tsbRunning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRunning.Image = global::NSqlTools.UI.Properties.Resources.Running;
			this.tsbRunning.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRunning.Name = "tsbRunning";
			this.tsbRunning.Size = new System.Drawing.Size(28, 28);
			this.tsbRunning.Text = "Running";
			this.tsbRunning.Click += new System.EventHandler(this.tsbRunning_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtLogs);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(580, 298);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Logs";
			// 
			// txtLogs
			// 
			this.txtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.txtLogs.Location = new System.Drawing.Point(3, 16);
			this.txtLogs.Multiline = true;
			this.txtLogs.Name = "txtLogs";
			this.txtLogs.ReadOnly = true;
			this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLogs.Size = new System.Drawing.Size(574, 279);
			this.txtLogs.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.tsMenu);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(580, 31);
			this.panel2.TabIndex = 1;
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRun,
            this.tsbStop});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(580, 31);
			this.tsMenu.TabIndex = 15;
			this.tsMenu.Text = "Expand Query Results Panel";
			// 
			// tsbRun
			// 
			this.tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRun.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
			this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRun.Name = "tsbRun";
			this.tsbRun.Size = new System.Drawing.Size(28, 28);
			this.tsbRun.Text = "Run";
			this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
			// 
			// tsbStop
			// 
			this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStop.Enabled = false;
			this.tsbStop.Image = global::NSqlTools.UI.Properties.Resources.Stop;
			this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStop.Name = "tsbStop";
			this.tsbStop.Size = new System.Drawing.Size(28, 28);
			this.tsbStop.Text = "Stop";
			this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
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
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// DBNameColumn
			// 
			this.DBNameColumn.DataPropertyName = "Name";
			this.DBNameColumn.HeaderText = "DB Name";
			this.DBNameColumn.MinimumWidth = 24;
			this.DBNameColumn.Name = "DBNameColumn";
			this.DBNameColumn.ReadOnly = true;
			this.DBNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.DBNameColumn.Width = 151;
			// 
			// ProgressColumn
			// 
			this.ProgressColumn.DataPropertyName = "Progress";
			this.ProgressColumn.HeaderText = "Progress";
			this.ProgressColumn.MinimumWidth = 24;
			this.ProgressColumn.Name = "ProgressColumn";
			this.ProgressColumn.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.ProgressColumn.ReadOnly = true;
			this.ProgressColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ProgressColumn.Width = 250;
			// 
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Name";
			this.NameColumn.HeaderText = "Table Name";
			this.NameColumn.MinimumWidth = 24;
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.NameColumn.Width = 201;
			// 
			// StatusColumn
			// 
			this.StatusColumn.DataPropertyName = "StatusName";
			this.StatusColumn.HeaderText = "Status";
			this.StatusColumn.MinimumWidth = 24;
			this.StatusColumn.Name = "StatusColumn";
			this.StatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.StatusColumn.Width = 200;
			// 
			// ucTableToCSV
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scoQuery);
			this.Name = "ucTableToCSV";
			this.Size = new System.Drawing.Size(834, 635);
			this.scoQuery.Panel1.ResumeLayout(false);
			this.scoQuery.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scoQuery)).EndInit();
			this.scoQuery.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCommandTimeout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudThreadCount)).EndInit();
			this.scQueryAndResult.Panel1.ResumeLayout(false);
			this.scQueryAndResult.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scQueryAndResult)).EndInit();
			this.scQueryAndResult.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvDBProgress)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.panel6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlQueryResults.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UserControls.ucDBObjectSelect ucDBObjectSelectControl;
		private System.Windows.Forms.SplitContainer scoQuery;
		private System.Windows.Forms.SplitContainer scQueryAndResult;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlQueryResults;
		private TabControl tcQueryResults;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbRun;
		private System.Windows.Forms.Panel panel3;
		private Panel panel4;
		private Panel panel5;
		private GroupBox groupBox1;
		private NumericUpDown nudThreadCount;
		private Label lblThreadCount;
		private TextBox txtPath;
		private Label lblPath;
		private CheckBox cbOnlyNotEmptyTables;
		private ContextMenuStrip contextMenuStrip1;
		private TextBox txtLogs;
		private CheckBox cbAddTableColumnsToCSV;
		private SplitContainer splitContainer1;
		private GroupBox groupBox3;
		private GroupBox groupBox2;
		private NumericUpDown nudCommandTimeout;
		private Label label1;
		private Button btnPathSelect;
		private FolderBrowserDialog folderBrowserDialog;
		private ColorDialog colorDialog1;
		private CheckBox cbCreateZip;
		private NAdvancedDataGridView dgvDBProgress;
		private GroupBox groupBox4;
		private SplitContainer splitContainer2;
		private NAdvancedDataGridView dgvTables;
		private Panel panel6;
		private ToolStrip toolStrip1;
		private ToolStripButton tsbCompleted;
		private ToolStripButton tsbNotCompleted;
		private ToolStripButton tsbRunning;
		private ToolStripButton tsbStop;
		private DataGridViewTextBoxColumn DBNameColumn;
		private DataGridViewProgressColumn ProgressColumn;
		private DataGridViewTextBoxColumn NameColumn;
		private DataGridViewTextBoxColumn StatusColumn;
	}
}
