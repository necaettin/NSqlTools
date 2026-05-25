using NSqlTools.Lib.Controls;
using NSqlTools.UI.Properties;
using NSqlTools.UI.UserControls;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	partial class ucSearchDB
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSearchDB));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.spSearchDB = new System.Windows.Forms.SplitContainer();
            this.panel8 = new System.Windows.Forms.Panel();
            this.ucDBObjectSelectControl = new NSqlTools.UI.UserControls.ucDBObjectSelect();
            this.pnlRepoFilter = new System.Windows.Forms.Panel();
            this.gbRepoFilter = new System.Windows.Forms.GroupBox();
            this.txtRepoExtraSearchKeyword = new System.Windows.Forms.TextBox();
            this.lblRepoExtraSearchKeyword = new System.Windows.Forms.Label();
            this.lblRepo = new System.Windows.Forms.Label();
            this.cbRepo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this._ucObjectType = new NSqlTools.UI.UserControls.ucObjectType();
            this.chbRepoSearch = new System.Windows.Forms.CheckBox();
            this.chbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.txtNameFilter = new System.Windows.Forms.TextBox();
            this.lblNameFilter = new System.Windows.Forms.Label();
            this.lblDBObjectType = new System.Windows.Forms.Label();
            this.chbDBSearch = new System.Windows.Forms.CheckBox();
            this.txtSearchKeyword = new System.Windows.Forms.TextBox();
            this.lblSearchKeyword = new System.Windows.Forms.Label();
            this.scSearchResult = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbSearchResult = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgvSearchResult = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
            this.DBNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectTypeNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemaNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HitCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblSearchResultStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbCriteriaCollapse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSearchDB = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbExportSearchResultToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlColumns = new System.Windows.Forms.Panel();
            this.gbColumns = new System.Windows.Forms.GroupBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dgvColumns = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
            this.ColumnIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeNameCustomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsNullableColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsIdentityColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.lblColumnsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucSqlNotePadControl = new NSqlTools.UI.UserControls.ucSqlNotePad();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spSearchDB)).BeginInit();
            this.spSearchDB.Panel1.SuspendLayout();
            this.spSearchDB.Panel2.SuspendLayout();
            this.spSearchDB.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlRepoFilter.SuspendLayout();
            this.gbRepoFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSearchResult)).BeginInit();
            this.scSearchResult.Panel1.SuspendLayout();
            this.scSearchResult.Panel2.SuspendLayout();
            this.scSearchResult.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbSearchResult.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).BeginInit();
            this.panel4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.pnlColumns.SuspendLayout();
            this.gbColumns.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.panel5.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // spSearchDB
            // 
            this.spSearchDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spSearchDB.Location = new System.Drawing.Point(0, 0);
            this.spSearchDB.Name = "spSearchDB";
            // 
            // spSearchDB.Panel1
            // 
            this.spSearchDB.Panel1.Controls.Add(this.panel8);
            // 
            // spSearchDB.Panel2
            // 
            this.spSearchDB.Panel2.Controls.Add(this.scSearchResult);
            this.spSearchDB.Size = new System.Drawing.Size(1086, 567);
            this.spSearchDB.SplitterDistance = 278;
            this.spSearchDB.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.AutoScroll = true;
            this.panel8.Controls.Add(this.ucDBObjectSelectControl);
            this.panel8.Controls.Add(this.pnlRepoFilter);
            this.panel8.Controls.Add(this.panel1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(278, 567);
            this.panel8.TabIndex = 4;
            // 
            // ucDBObjectSelectControl
            // 
            this.ucDBObjectSelectControl.AllowOnlyOneDBSelection = false;
            this.ucDBObjectSelectControl.Caption = "DB Select";
            this.ucDBObjectSelectControl.DBObjectVisibility = false;
            this.ucDBObjectSelectControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDBObjectSelectControl.IsRequiredConnectionString = true;
            this.ucDBObjectSelectControl.IsRequiredDB = true;
            this.ucDBObjectSelectControl.IsRequiredDBObject = false;
            this.ucDBObjectSelectControl.IsRequiredObjectType = false;
            this.ucDBObjectSelectControl.IsRequiredSchema = false;
            this.ucDBObjectSelectControl.Location = new System.Drawing.Point(0, 309);
            this.ucDBObjectSelectControl.MainForm = null;
            this.ucDBObjectSelectControl.Name = "ucDBObjectSelectControl";
            this.ucDBObjectSelectControl.ObjectTypeVisibility = false;
            this.ucDBObjectSelectControl.ParentTabPage = null;
            this.ucDBObjectSelectControl.SchemaVisibility = true;
            this.ucDBObjectSelectControl.SelectedConnectionNameValue = null;
            this.ucDBObjectSelectControl.SelectedDBIndexes = null;
            this.ucDBObjectSelectControl.SelectedDBObjectObjectId = null;
            this.ucDBObjectSelectControl.SelectedObjectType2 = 9;
            this.ucDBObjectSelectControl.SelectedSchemaId = null;
            this.ucDBObjectSelectControl.Size = new System.Drawing.Size(278, 205);
            this.ucDBObjectSelectControl.TabIndex = 5;
            this.ucDBObjectSelectControl.TabIndexConnectionString = 6;
            this.ucDBObjectSelectControl.TabIndexDB = 7;
            this.ucDBObjectSelectControl.TabIndexDBObject = 60;
            this.ucDBObjectSelectControl.TabIndexDBObjectFilter = 50;
            this.ucDBObjectSelectControl.TabIndexObjectType = 30;
            this.ucDBObjectSelectControl.TabIndexSchema = 8;
            this.ucDBObjectSelectControl.TitleVisibility = null;
            this.ucDBObjectSelectControl.OnDBChanged += new System.EventHandler(this.ucDBObjectSelect_OnDBChanged);
            this.ucDBObjectSelectControl.OnDBClear += new System.EventHandler(this.ucDBObjectSelect_OnDBClear);
            // 
            // pnlRepoFilter
            // 
            this.pnlRepoFilter.Controls.Add(this.gbRepoFilter);
            this.pnlRepoFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRepoFilter.Location = new System.Drawing.Point(0, 205);
            this.pnlRepoFilter.Name = "pnlRepoFilter";
            this.pnlRepoFilter.Size = new System.Drawing.Size(278, 104);
            this.pnlRepoFilter.TabIndex = 6;
            // 
            // gbRepoFilter
            // 
            this.gbRepoFilter.Controls.Add(this.txtRepoExtraSearchKeyword);
            this.gbRepoFilter.Controls.Add(this.lblRepoExtraSearchKeyword);
            this.gbRepoFilter.Controls.Add(this.lblRepo);
            this.gbRepoFilter.Controls.Add(this.cbRepo);
            this.gbRepoFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRepoFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gbRepoFilter.Location = new System.Drawing.Point(0, 0);
            this.gbRepoFilter.Name = "gbRepoFilter";
            this.gbRepoFilter.Size = new System.Drawing.Size(278, 104);
            this.gbRepoFilter.TabIndex = 0;
            this.gbRepoFilter.TabStop = false;
            this.gbRepoFilter.Text = "Repo Filter";
            // 
            // txtRepoExtraSearchKeyword
            // 
            this.txtRepoExtraSearchKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRepoExtraSearchKeyword.Enabled = false;
            this.txtRepoExtraSearchKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtRepoExtraSearchKeyword.Location = new System.Drawing.Point(6, 71);
            this.txtRepoExtraSearchKeyword.Name = "txtRepoExtraSearchKeyword";
            this.txtRepoExtraSearchKeyword.Size = new System.Drawing.Size(266, 23);
            this.txtRepoExtraSearchKeyword.TabIndex = 22;
            this.ttToolTip.SetToolTip(this.txtRepoExtraSearchKeyword, "Searches for content within database objects containing Transact-SQL, and searche" +
        "s for column names in table-type database objects.");
            // 
            // lblRepoExtraSearchKeyword
            // 
            this.lblRepoExtraSearchKeyword.AutoSize = true;
            this.lblRepoExtraSearchKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepoExtraSearchKeyword.Location = new System.Drawing.Point(3, 56);
            this.lblRepoExtraSearchKeyword.Name = "lblRepoExtraSearchKeyword";
            this.lblRepoExtraSearchKeyword.Size = new System.Drawing.Size(141, 13);
            this.lblRepoExtraSearchKeyword.TabIndex = 23;
            this.lblRepoExtraSearchKeyword.Text = "Repo Extra Search Keyword";
            // 
            // lblRepo
            // 
            this.lblRepo.AutoSize = true;
            this.lblRepo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepo.Location = new System.Drawing.Point(3, 14);
            this.lblRepo.Name = "lblRepo";
            this.lblRepo.Size = new System.Drawing.Size(63, 13);
            this.lblRepo.TabIndex = 21;
            this.lblRepo.Text = "Azure Repo";
            // 
            // cbRepo
            // 
            this.cbRepo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRepo.DisplayMember = "TypeDescription";
            this.cbRepo.Enabled = false;
            this.cbRepo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbRepo.FormattingEnabled = true;
            this.cbRepo.Location = new System.Drawing.Point(6, 27);
            this.cbRepo.Name = "cbRepo";
            this.cbRepo.Size = new System.Drawing.Size(266, 24);
            this.cbRepo.TabIndex = 20;
            this.cbRepo.ValueMember = "Type";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 205);
            this.panel1.TabIndex = 2;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this._ucObjectType);
            this.gbFilter.Controls.Add(this.chbRepoSearch);
            this.gbFilter.Controls.Add(this.chbCaseSensitive);
            this.gbFilter.Controls.Add(this.txtNameFilter);
            this.gbFilter.Controls.Add(this.lblNameFilter);
            this.gbFilter.Controls.Add(this.lblDBObjectType);
            this.gbFilter.Controls.Add(this.chbDBSearch);
            this.gbFilter.Controls.Add(this.txtSearchKeyword);
            this.gbFilter.Controls.Add(this.lblSearchKeyword);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(0, 0);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(278, 205);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // _ucObjectType
            // 
            this._ucObjectType.IsNullable = true;
            this._ucObjectType.Location = new System.Drawing.Point(5, 111);
            this._ucObjectType.MainForm = null;
            this._ucObjectType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._ucObjectType.Name = "_ucObjectType";
            this._ucObjectType.ParentTabPage = null;
            this._ucObjectType.Size = new System.Drawing.Size(264, 25);
            this._ucObjectType.TabIndex = 21;
            this._ucObjectType.OnObjectTypeChanged += new System.EventHandler(this._ucObjectType_OnObjectTypeChanged);
            // 
            // chbRepoSearch
            // 
            this.chbRepoSearch.AutoSize = true;
            this.chbRepoSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbRepoSearch.Location = new System.Drawing.Point(6, 184);
            this.chbRepoSearch.Name = "chbRepoSearch";
            this.chbRepoSearch.Size = new System.Drawing.Size(119, 17);
            this.chbRepoSearch.TabIndex = 20;
            this.chbRepoSearch.Text = "Azure Repo Search";
            this.chbRepoSearch.UseVisualStyleBackColor = true;
            this.chbRepoSearch.CheckedChanged += new System.EventHandler(this.chbRepoSearch_CheckedChanged);
            // 
            // chbCaseSensitive
            // 
            this.chbCaseSensitive.AutoSize = true;
            this.chbCaseSensitive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbCaseSensitive.Location = new System.Drawing.Point(6, 138);
            this.chbCaseSensitive.Name = "chbCaseSensitive";
            this.chbCaseSensitive.Size = new System.Drawing.Size(94, 17);
            this.chbCaseSensitive.TabIndex = 4;
            this.chbCaseSensitive.Text = "Case Sentitive";
            this.chbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNameFilter.Location = new System.Drawing.Point(6, 72);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(266, 23);
            this.txtNameFilter.TabIndex = 2;
            // 
            // lblNameFilter
            // 
            this.lblNameFilter.AutoSize = true;
            this.lblNameFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameFilter.Location = new System.Drawing.Point(3, 57);
            this.lblNameFilter.Name = "lblNameFilter";
            this.lblNameFilter.Size = new System.Drawing.Size(60, 13);
            this.lblNameFilter.TabIndex = 17;
            this.lblNameFilter.Text = "Name Filter";
            // 
            // lblDBObjectType
            // 
            this.lblDBObjectType.AutoSize = true;
            this.lblDBObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBObjectType.Location = new System.Drawing.Point(3, 96);
            this.lblDBObjectType.Name = "lblDBObjectType";
            this.lblDBObjectType.Size = new System.Drawing.Size(83, 13);
            this.lblDBObjectType.TabIndex = 16;
            this.lblDBObjectType.Text = "DB Object Type";
            // 
            // chbDBSearch
            // 
            this.chbDBSearch.AutoSize = true;
            this.chbDBSearch.Checked = true;
            this.chbDBSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbDBSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbDBSearch.Location = new System.Drawing.Point(6, 161);
            this.chbDBSearch.Name = "chbDBSearch";
            this.chbDBSearch.Size = new System.Drawing.Size(78, 17);
            this.chbDBSearch.TabIndex = 19;
            this.chbDBSearch.Text = "DB Search";
            this.chbDBSearch.UseVisualStyleBackColor = true;
            this.chbDBSearch.CheckedChanged += new System.EventHandler(this.chbDBSearch_CheckedChanged);
            // 
            // txtSearchKeyword
            // 
            this.txtSearchKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchKeyword.BackColor = System.Drawing.Color.Linen;
            this.txtSearchKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchKeyword.Location = new System.Drawing.Point(6, 31);
            this.txtSearchKeyword.Name = "txtSearchKeyword";
            this.txtSearchKeyword.Size = new System.Drawing.Size(266, 23);
            this.txtSearchKeyword.TabIndex = 1;
            this.ttToolTip.SetToolTip(this.txtSearchKeyword, "Searches for content within database objects containing Transact-SQL, and searche" +
        "s for column names in table-type database objects.");
            this.txtSearchKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchKeyword_KeyDown);
            // 
            // lblSearchKeyword
            // 
            this.lblSearchKeyword.AutoSize = true;
            this.lblSearchKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchKeyword.Location = new System.Drawing.Point(3, 16);
            this.lblSearchKeyword.Name = "lblSearchKeyword";
            this.lblSearchKeyword.Size = new System.Drawing.Size(85, 13);
            this.lblSearchKeyword.TabIndex = 14;
            this.lblSearchKeyword.Text = "Search Keyword";
            // 
            // scSearchResult
            // 
            this.scSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSearchResult.Location = new System.Drawing.Point(0, 0);
            this.scSearchResult.Name = "scSearchResult";
            this.scSearchResult.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSearchResult.Panel1
            // 
            this.scSearchResult.Panel1.Controls.Add(this.panel3);
            this.scSearchResult.Panel1.Controls.Add(this.tsMenu);
            // 
            // scSearchResult.Panel2
            // 
            this.scSearchResult.Panel2.Controls.Add(this.pnlColumns);
            this.scSearchResult.Panel2.Controls.Add(this.ucSqlNotePadControl);
            this.scSearchResult.Size = new System.Drawing.Size(804, 567);
            this.scSearchResult.SplitterDistance = 283;
            this.scSearchResult.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbSearchResult);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(804, 252);
            this.panel3.TabIndex = 18;
            // 
            // gbSearchResult
            // 
            this.gbSearchResult.Controls.Add(this.panel6);
            this.gbSearchResult.Controls.Add(this.panel4);
            this.gbSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSearchResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSearchResult.Location = new System.Drawing.Point(0, 0);
            this.gbSearchResult.Name = "gbSearchResult";
            this.gbSearchResult.Size = new System.Drawing.Size(804, 252);
            this.gbSearchResult.TabIndex = 0;
            this.gbSearchResult.TabStop = false;
            this.gbSearchResult.Text = "Search Result";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dgvSearchResult);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 16);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(798, 211);
            this.panel6.TabIndex = 2;
            // 
            // dgvSearchResult
            // 
            this.dgvSearchResult.AllowUserToAddRows = false;
            this.dgvSearchResult.AllowUserToDeleteRows = false;
            this.dgvSearchResult.AllowUserToOrderColumns = true;
            this.dgvSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DBNameColumn,
            this.ObjectTypeNameCol,
            this.SchemaNameCol,
            this.NameColumn,
            this.HitCountColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSearchResult.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchResult.EnableHeadersVisualStyles = false;
            this.dgvSearchResult.FilterAndSortEnabled = true;
            this.dgvSearchResult.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvSearchResult.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchResult.MaxFilterButtonImageHeight = 23;
            this.dgvSearchResult.Name = "dgvSearchResult";
            this.dgvSearchResult.ReadOnly = true;
            this.dgvSearchResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSearchResult.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSearchResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchResult.Size = new System.Drawing.Size(798, 211);
            this.dgvSearchResult.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvSearchResult.TabIndex = 0;
            this.dgvSearchResult.TabStop = false;
            this.dgvSearchResult.SelectionChanged += new System.EventHandler(this.dgvSearchResult_SelectionChanged);
            // 
            // DBNameColumn
            // 
            this.DBNameColumn.DataPropertyName = "DBName";
            this.DBNameColumn.HeaderText = "DB";
            this.DBNameColumn.MinimumWidth = 24;
            this.DBNameColumn.Name = "DBNameColumn";
            this.DBNameColumn.ReadOnly = true;
            this.DBNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.DBNameColumn.Width = 101;
            // 
            // ObjectTypeNameCol
            // 
            this.ObjectTypeNameCol.DataPropertyName = "ObjectTypeName";
            this.ObjectTypeNameCol.HeaderText = "Object Type";
            this.ObjectTypeNameCol.MinimumWidth = 24;
            this.ObjectTypeNameCol.Name = "ObjectTypeNameCol";
            this.ObjectTypeNameCol.ReadOnly = true;
            this.ObjectTypeNameCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ObjectTypeNameCol.Width = 150;
            // 
            // SchemaNameCol
            // 
            this.SchemaNameCol.DataPropertyName = "SchemaName";
            this.SchemaNameCol.HeaderText = "Schema";
            this.SchemaNameCol.MinimumWidth = 24;
            this.SchemaNameCol.Name = "SchemaNameCol";
            this.SchemaNameCol.ReadOnly = true;
            this.SchemaNameCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 24;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.NameColumn.Width = 300;
            // 
            // HitCountColumn
            // 
            this.HitCountColumn.DataPropertyName = "HitCount";
            this.HitCountColumn.HeaderText = "Hit Count";
            this.HitCountColumn.MinimumWidth = 24;
            this.HitCountColumn.Name = "HitCountColumn";
            this.HitCountColumn.ReadOnly = true;
            this.HitCountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.HitCountColumn.Width = 60;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.statusStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 227);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(798, 22);
            this.panel4.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSearchResultStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(798, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblSearchResultStatus
            // 
            this.lblSearchResultStatus.Name = "lblSearchResultStatus";
            this.lblSearchResultStatus.Size = new System.Drawing.Size(10, 17);
            this.lblSearchResultStatus.Text = " ";
            // 
            // tsMenu
            // 
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCriteriaCollapse,
            this.toolStripSeparator3,
            this.tsbSearchDB,
            this.tsbCancelSearch,
            this.tsbExportSearchResultToExcel,
            this.toolStripSeparator2,
            this.progressBar});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(804, 31);
            this.tsMenu.TabIndex = 17;
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
            // tsbSearchDB
            // 
            this.tsbSearchDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSearchDB.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
            this.tsbSearchDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSearchDB.Name = "tsbSearchDB";
            this.tsbSearchDB.Size = new System.Drawing.Size(28, 28);
            this.tsbSearchDB.Text = "Start DB Search";
            this.tsbSearchDB.Click += new System.EventHandler(this.tsbSearchDB_Click);
            // 
            // tsbCancelSearch
            // 
            this.tsbCancelSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancelSearch.Enabled = false;
            this.tsbCancelSearch.Image = global::NSqlTools.UI.Properties.Resources.CloseBlue;
            this.tsbCancelSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelSearch.Name = "tsbCancelSearch";
            this.tsbCancelSearch.Size = new System.Drawing.Size(28, 28);
            this.tsbCancelSearch.Text = "Cancel Search";
            this.tsbCancelSearch.Click += new System.EventHandler(this.tsbCancelSearch_Click);
            // 
            // tsbExportSearchResultToExcel
            // 
            this.tsbExportSearchResultToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportSearchResultToExcel.Image = global::NSqlTools.UI.Properties.Resources.Excel;
            this.tsbExportSearchResultToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportSearchResultToExcel.Name = "tsbExportSearchResultToExcel";
            this.tsbExportSearchResultToExcel.Size = new System.Drawing.Size(28, 28);
            this.tsbExportSearchResultToExcel.Text = "Export Search Result To Excel";
            this.tsbExportSearchResultToExcel.Click += new System.EventHandler(this.tsbExportSearchResultToExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 28);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.Visible = false;
            // 
            // pnlColumns
            // 
            this.pnlColumns.Controls.Add(this.gbColumns);
            this.pnlColumns.Location = new System.Drawing.Point(47, 63);
            this.pnlColumns.Name = "pnlColumns";
            this.pnlColumns.Size = new System.Drawing.Size(487, 138);
            this.pnlColumns.TabIndex = 2;
            // 
            // gbColumns
            // 
            this.gbColumns.Controls.Add(this.panel7);
            this.gbColumns.Controls.Add(this.panel5);
            this.gbColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbColumns.Location = new System.Drawing.Point(0, 0);
            this.gbColumns.Name = "gbColumns";
            this.gbColumns.Size = new System.Drawing.Size(487, 138);
            this.gbColumns.TabIndex = 0;
            this.gbColumns.TabStop = false;
            this.gbColumns.Text = "Columns";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.dgvColumns);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 16);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(481, 97);
            this.panel7.TabIndex = 3;
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToDeleteRows = false;
            this.dgvColumns.AllowUserToOrderColumns = true;
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIdColumn,
            this.dataGridViewTextBoxColumn1,
            this.TypeNameCustomColumn,
            this.IsNullableColumn,
            this.IsIdentityColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvColumns.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColumns.EnableHeadersVisualStyles = false;
            this.dgvColumns.FilterAndSortEnabled = true;
            this.dgvColumns.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvColumns.Location = new System.Drawing.Point(0, 0);
            this.dgvColumns.MaxFilterButtonImageHeight = 23;
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.ReadOnly = true;
            this.dgvColumns.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvColumns.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColumns.Size = new System.Drawing.Size(481, 97);
            this.dgvColumns.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvColumns.TabIndex = 0;
            this.dgvColumns.TabStop = false;
            // 
            // ColumnIdColumn
            // 
            this.ColumnIdColumn.DataPropertyName = "ColumnId";
            this.ColumnIdColumn.HeaderText = "ID";
            this.ColumnIdColumn.MinimumWidth = 24;
            this.ColumnIdColumn.Name = "ColumnIdColumn";
            this.ColumnIdColumn.ReadOnly = true;
            this.ColumnIdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ColumnIdColumn.Visible = false;
            this.ColumnIdColumn.Width = 76;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Column Name";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 24;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // TypeNameCustomColumn
            // 
            this.TypeNameCustomColumn.DataPropertyName = "TypeNameCustom";
            this.TypeNameCustomColumn.HeaderText = "Type";
            this.TypeNameCustomColumn.MinimumWidth = 24;
            this.TypeNameCustomColumn.Name = "TypeNameCustomColumn";
            this.TypeNameCustomColumn.ReadOnly = true;
            this.TypeNameCustomColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // IsNullableColumn
            // 
            this.IsNullableColumn.DataPropertyName = "IsNullable";
            this.IsNullableColumn.HeaderText = "Nullable";
            this.IsNullableColumn.MinimumWidth = 24;
            this.IsNullableColumn.Name = "IsNullableColumn";
            this.IsNullableColumn.ReadOnly = true;
            this.IsNullableColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // IsIdentityColumn
            // 
            this.IsIdentityColumn.DataPropertyName = "IsIdentity";
            this.IsIdentityColumn.HeaderText = "Identity";
            this.IsIdentityColumn.MinimumWidth = 24;
            this.IsIdentityColumn.Name = "IsIdentityColumn";
            this.IsIdentityColumn.ReadOnly = true;
            this.IsIdentityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.statusStrip2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 113);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(481, 22);
            this.panel5.TabIndex = 2;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblColumnsStatus});
            this.statusStrip2.Location = new System.Drawing.Point(0, 0);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(481, 22);
            this.statusStrip2.TabIndex = 0;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // lblColumnsStatus
            // 
            this.lblColumnsStatus.Name = "lblColumnsStatus";
            this.lblColumnsStatus.Size = new System.Drawing.Size(10, 17);
            this.lblColumnsStatus.Text = " ";
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
            this.ucSqlNotePadControl.IsWraped = false;
            this.ucSqlNotePadControl.Location = new System.Drawing.Point(115, 21);
            this.ucSqlNotePadControl.MainForm = null;
            this.ucSqlNotePadControl.Name = "ucSqlNotePadControl";
            this.ucSqlNotePadControl.ParentTabPage = null;
            this.ucSqlNotePadControl.SchemaKeywordList = null;
            this.ucSqlNotePadControl.scoSqlNotepadPanel2Collapsed = true;
            this.ucSqlNotePadControl.SearchKeyword = "";
            this.ucSqlNotePadControl.Size = new System.Drawing.Size(405, 234);
            this.ucSqlNotePadControl.TabIndex = 0;
            this.ucSqlNotePadControl.TabStop = false;
            this.ucSqlNotePadControl.Title = "Sql Script";
            this.ucSqlNotePadControl.Visible = false;
            // 
            // ucSearchDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spSearchDB);
            this.Name = "ucSearchDB";
            this.Size = new System.Drawing.Size(1086, 567);
            this.spSearchDB.Panel1.ResumeLayout(false);
            this.spSearchDB.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spSearchDB)).EndInit();
            this.spSearchDB.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.pnlRepoFilter.ResumeLayout(false);
            this.gbRepoFilter.ResumeLayout(false);
            this.gbRepoFilter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.scSearchResult.Panel1.ResumeLayout(false);
            this.scSearchResult.Panel1.PerformLayout();
            this.scSearchResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scSearchResult)).EndInit();
            this.scSearchResult.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbSearchResult.ResumeLayout(false);
            this.gbSearchResult.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.pnlColumns.ResumeLayout(false);
            this.gbColumns.ResumeLayout(false);
            this.gbColumns.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);

		}


		#endregion
		private System.Windows.Forms.SplitContainer spSearchDB;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private ucSqlNotePad ucSqlNotePadControl;
		private UserControls.ucDBObjectSelect ucDBObjectSelectControl;
		private SplitContainer scSearchResult;
		private GroupBox gbSearchResult;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private GroupBox gbFilter;
		private Panel panel1;
		private TextBox txtSearchKeyword;
		private Label lblSearchKeyword;
		private Panel panel3;
		private ToolStrip tsMenu;
		private ToolStripButton tsbSearchDB;
		private ToolStripButton tsbCancelSearch;
		private Label lblDBObjectType;
		private Panel pnlColumns;
		private GroupBox gbColumns;
		private ToolStripButton tsbExportSearchResultToExcel;
		private TextBox txtNameFilter;
		private Label lblNameFilter;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripProgressBar progressBar;
		private Panel panel4;
		private StatusStrip statusStrip1;
		private ToolStripStatusLabel lblSearchResultStatus;
		private Panel panel5;
		private StatusStrip statusStrip2;
		private ToolStripStatusLabel lblColumnsStatus;
		private CheckBox chbCaseSensitive;
		private Panel panel6;
		private ToolStripButton tsbCriteriaCollapse;
		private ToolTip ttToolTip;
		private Panel panel7;
		private Panel panel8;
		private ToolStripSeparator toolStripSeparator3;
		private Panel pnlRepoFilter;
		private GroupBox gbRepoFilter;
		private TextBox txtRepoExtraSearchKeyword;
		private Label lblRepoExtraSearchKeyword;
		private Label lblRepo;
		private ComboBox cbRepo;
		private CheckBox chbDBSearch;
		private CheckBox chbRepoSearch;
		private ucObjectType _ucObjectType;
		private DataGridViewTextBoxColumn DBNameColumn;
		private DataGridViewTextBoxColumn ObjectTypeNameCol;
		private DataGridViewTextBoxColumn SchemaNameCol;
		private DataGridViewTextBoxColumn NameColumn;
		private DataGridViewTextBoxColumn HitCountColumn;
		private DataGridViewTextBoxColumn ColumnIdColumn;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private DataGridViewTextBoxColumn TypeNameCustomColumn;
		private DataGridViewCheckBoxColumn IsNullableColumn;
		private DataGridViewCheckBoxColumn IsIdentityColumn;
		private NAdvancedDataGridView dgvSearchResult;
		private NAdvancedDataGridView dgvColumns;
	}
}
