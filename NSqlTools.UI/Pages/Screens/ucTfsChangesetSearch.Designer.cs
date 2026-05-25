using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.Pages.Screens
{
	partial class ucTfsChangesetSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTfsChangesetSearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbCriteriaCollapse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStartTFSSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbCleanFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvChangesets = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
            this.colChangesetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSolutions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMergedToTest = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTestChangesetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestMergeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestMergeUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMergedToMain = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMainChangesetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMainMergeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMainMergeUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBodyContainer = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblChangesetId = new System.Windows.Forms.Label();
            this.txtChangesetId = new System.Windows.Forms.TextBox();
            this.scTFSResult = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tvFileChanges = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslSource = new System.Windows.Forms.ToolStripLabel();
            this.tscbSource = new System.Windows.Forms.ToolStripComboBox();
            this.tslTarget = new System.Windows.Forms.ToolStripLabel();
            this.tscbTarget = new System.Windows.Forms.ToolStripComboBox();
            this.scTFSMain = new System.Windows.Forms.SplitContainer();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.lblOwnerFilter = new System.Windows.Forms.Label();
            this.txtCommentFilter = new System.Windows.Forms.TextBox();
            this.lblCommentFilter = new System.Windows.Forms.Label();
            this.chkShowOnlyUnmergedToMain = new System.Windows.Forms.CheckBox();
            this.chkShowOnlyUnmergedToTest = new System.Windows.Forms.CheckBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbOwnerFilter = new System.Windows.Forms.ComboBox();
            this.pnlObjectType = new System.Windows.Forms.Panel();
            this.gbTFSSettings = new System.Windows.Forms.GroupBox();
            this.txtTFSPath = new System.Windows.Forms.TextBox();
            this.txtTFSUrl = new System.Windows.Forms.TextBox();
            this.lblTFSPath = new System.Windows.Forms.Label();
            this.lblTFSUrl = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChangesets)).BeginInit();
            this.pnlBodyContainer.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTFSResult)).BeginInit();
            this.scTFSResult.Panel1.SuspendLayout();
            this.scTFSResult.Panel2.SuspendLayout();
            this.scTFSResult.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTFSMain)).BeginInit();
            this.scTFSMain.Panel1.SuspendLayout();
            this.scTFSMain.Panel2.SuspendLayout();
            this.scTFSMain.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.pnlObjectType.SuspendLayout();
            this.gbTFSSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(946, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(10, 17);
            this.lblStatus.Text = " ";
            // 
            // tsMenu
            // 
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCriteriaCollapse,
            this.toolStripSeparator3,
            this.tsbStartTFSSearch,
            this.tsbCancelSearch,
            this.tsbCleanFilter,
            this.tsbRefreshUsers,
            this.toolStripSeparator2,
            this.tsbExportToExcel,
            this.toolStripSeparator1,
            this.progressBar});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(946, 31);
            this.tsMenu.TabIndex = 15;
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
            // tsbStartTFSSearch
            // 
            this.tsbStartTFSSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStartTFSSearch.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
            this.tsbStartTFSSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStartTFSSearch.Name = "tsbStartTFSSearch";
            this.tsbStartTFSSearch.Size = new System.Drawing.Size(28, 28);
            this.tsbStartTFSSearch.Text = "Ara";
            this.tsbStartTFSSearch.Click += new System.EventHandler(this.tsbStartTFSSearch_Click);
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
            // tsbCleanFilter
            // 
            this.tsbCleanFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCleanFilter.Image = global::NSqlTools.UI.Properties.Resources.Clean;
            this.tsbCleanFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCleanFilter.Name = "tsbCleanFilter";
            this.tsbCleanFilter.Size = new System.Drawing.Size(28, 28);
            this.tsbCleanFilter.Text = "Clean";
            this.tsbCleanFilter.Click += new System.EventHandler(this.tsbCleanFilter_Click_1);
            // 
            // tsbRefreshUsers
            // 
            this.tsbRefreshUsers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefreshUsers.Image = global::NSqlTools.UI.Properties.Resources.Users;
            this.tsbRefreshUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshUsers.Name = "tsbRefreshUsers";
            this.tsbRefreshUsers.Size = new System.Drawing.Size(28, 28);
            this.tsbRefreshUsers.Text = "Refresh Users";
            this.tsbRefreshUsers.Click += new System.EventHandler(this.tsbRefreshUsers_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbExportToExcel
            // 
            this.tsbExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportToExcel.Image = global::NSqlTools.UI.Properties.Resources.Excel;
            this.tsbExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportToExcel.Name = "tsbExportToExcel";
            this.tsbExportToExcel.Size = new System.Drawing.Size(28, 28);
            this.tsbExportToExcel.Text = "Export Search Result To Excel";
            this.tsbExportToExcel.Click += new System.EventHandler(this.tsbExportToExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 28);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvChangesets);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 347);
            this.panel1.TabIndex = 3;
            // 
            // dgvChangesets
            // 
            this.dgvChangesets.AllowUserToAddRows = false;
            this.dgvChangesets.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvChangesets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChangesets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChangesets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChangesetId,
            this.colCreationDate,
            this.colOwner,
            this.colComment,
            this.colBranch,
            this.colSolutions,
            this.colMergedToTest,
            this.colTestChangesetId,
            this.colTestMergeDate,
            this.colTestMergeUser,
            this.colMergedToMain,
            this.colMainChangesetId,
            this.colMainMergeDate,
            this.colMainMergeUser});
            this.dgvChangesets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChangesets.EnableHeadersVisualStyles = false;
            this.dgvChangesets.FilterAndSortEnabled = true;
            this.dgvChangesets.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvChangesets.Location = new System.Drawing.Point(0, 0);
            this.dgvChangesets.MaxFilterButtonImageHeight = 23;
            this.dgvChangesets.Name = "dgvChangesets";
            this.dgvChangesets.ReadOnly = true;
            this.dgvChangesets.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvChangesets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChangesets.Size = new System.Drawing.Size(946, 347);
            this.dgvChangesets.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvChangesets.TabIndex = 1;
            // 
            // colChangesetId
            // 
            this.colChangesetId.DataPropertyName = "ChangesetId";
            this.colChangesetId.HeaderText = "Changeset ID";
            this.colChangesetId.MinimumWidth = 24;
            this.colChangesetId.Name = "colChangesetId";
            this.colChangesetId.ReadOnly = true;
            this.colChangesetId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colChangesetId.Width = 75;
            // 
            // colCreationDate
            // 
            this.colCreationDate.DataPropertyName = "CreationDate";
            this.colCreationDate.HeaderText = "Create Date";
            this.colCreationDate.MinimumWidth = 24;
            this.colCreationDate.Name = "colCreationDate";
            this.colCreationDate.ReadOnly = true;
            this.colCreationDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colOwner
            // 
            this.colOwner.DataPropertyName = "Owner";
            this.colOwner.HeaderText = "Dev Checkin User";
            this.colOwner.MinimumWidth = 24;
            this.colOwner.Name = "colOwner";
            this.colOwner.ReadOnly = true;
            this.colOwner.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colComment
            // 
            this.colComment.DataPropertyName = "Comment";
            this.colComment.HeaderText = "Comment";
            this.colComment.MinimumWidth = 24;
            this.colComment.Name = "colComment";
            this.colComment.ReadOnly = true;
            this.colComment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colComment.Width = 250;
            // 
            // colBranch
            // 
            this.colBranch.DataPropertyName = "Branch";
            this.colBranch.HeaderText = "Branch";
            this.colBranch.MinimumWidth = 24;
            this.colBranch.Name = "colBranch";
            this.colBranch.ReadOnly = true;
            this.colBranch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colBranch.Width = 150;
            // 
            // colSolutions
            // 
            this.colSolutions.DataPropertyName = "Solutions";
            this.colSolutions.HeaderText = "Solutions";
            this.colSolutions.MinimumWidth = 24;
            this.colSolutions.Name = "colSolutions";
            this.colSolutions.ReadOnly = true;
            this.colSolutions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colSolutions.Width = 200;
            // 
            // colMergedToTest
            // 
            this.colMergedToTest.DataPropertyName = "MergedToTest";
            this.colMergedToTest.HeaderText = "Merged To Test";
            this.colMergedToTest.MinimumWidth = 24;
            this.colMergedToTest.Name = "colMergedToTest";
            this.colMergedToTest.ReadOnly = true;
            this.colMergedToTest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colMergedToTest.Width = 75;
            // 
            // colTestChangesetId
            // 
            this.colTestChangesetId.DataPropertyName = "TestChangesetId";
            this.colTestChangesetId.HeaderText = "Test Changeset ID";
            this.colTestChangesetId.MinimumWidth = 24;
            this.colTestChangesetId.Name = "colTestChangesetId";
            this.colTestChangesetId.ReadOnly = true;
            this.colTestChangesetId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colTestChangesetId.Width = 75;
            // 
            // colTestMergeDate
            // 
            this.colTestMergeDate.DataPropertyName = "TestMergeDate";
            this.colTestMergeDate.HeaderText = "Test Merge Date";
            this.colTestMergeDate.MinimumWidth = 24;
            this.colTestMergeDate.Name = "colTestMergeDate";
            this.colTestMergeDate.ReadOnly = true;
            this.colTestMergeDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colTestMergeUser
            // 
            this.colTestMergeUser.DataPropertyName = "TestMergeUser";
            this.colTestMergeUser.HeaderText = "Test Merge User";
            this.colTestMergeUser.MinimumWidth = 24;
            this.colTestMergeUser.Name = "colTestMergeUser";
            this.colTestMergeUser.ReadOnly = true;
            this.colTestMergeUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colMergedToMain
            // 
            this.colMergedToMain.DataPropertyName = "MergedToMain";
            this.colMergedToMain.HeaderText = "Merged To Main";
            this.colMergedToMain.MinimumWidth = 24;
            this.colMergedToMain.Name = "colMergedToMain";
            this.colMergedToMain.ReadOnly = true;
            this.colMergedToMain.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colMergedToMain.Width = 75;
            // 
            // colMainChangesetId
            // 
            this.colMainChangesetId.DataPropertyName = "MainChangesetId";
            this.colMainChangesetId.HeaderText = "Main Changeset ID";
            this.colMainChangesetId.MinimumWidth = 24;
            this.colMainChangesetId.Name = "colMainChangesetId";
            this.colMainChangesetId.ReadOnly = true;
            this.colMainChangesetId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colMainChangesetId.Width = 75;
            // 
            // colMainMergeDate
            // 
            this.colMainMergeDate.DataPropertyName = "MainMergeDate";
            this.colMainMergeDate.HeaderText = "Main Merge Date";
            this.colMainMergeDate.MinimumWidth = 24;
            this.colMainMergeDate.Name = "colMainMergeDate";
            this.colMainMergeDate.ReadOnly = true;
            this.colMainMergeDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colMainMergeUser
            // 
            this.colMainMergeUser.DataPropertyName = "MainMergeUser";
            this.colMainMergeUser.HeaderText = "Main Merge User";
            this.colMainMergeUser.MinimumWidth = 24;
            this.colMainMergeUser.Name = "colMainMergeUser";
            this.colMainMergeUser.ReadOnly = true;
            this.colMainMergeUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pnlBodyContainer
            // 
            this.pnlBodyContainer.Controls.Add(this.pnlBody);
            this.pnlBodyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBodyContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlBodyContainer.Name = "pnlBodyContainer";
            this.pnlBodyContainer.Size = new System.Drawing.Size(946, 400);
            this.pnlBodyContainer.TabIndex = 20;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlGrid);
            this.pnlBody.Controls.Add(this.tsMenu);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(946, 400);
            this.pnlBody.TabIndex = 18;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.panel1);
            this.pnlGrid.Controls.Add(this.panel2);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 31);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(946, 369);
            this.pnlGrid.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 347);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(946, 22);
            this.panel2.TabIndex = 2;
            // 
            // lblChangesetId
            // 
            this.lblChangesetId.AutoSize = true;
            this.lblChangesetId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangesetId.Location = new System.Drawing.Point(7, 58);
            this.lblChangesetId.Name = "lblChangesetId";
            this.lblChangesetId.Size = new System.Drawing.Size(72, 13);
            this.lblChangesetId.TabIndex = 51;
            this.lblChangesetId.Text = "Changeset ID";
            // 
            // txtChangesetId
            // 
            this.txtChangesetId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChangesetId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtChangesetId.Location = new System.Drawing.Point(10, 74);
            this.txtChangesetId.Name = "txtChangesetId";
            this.txtChangesetId.Size = new System.Drawing.Size(234, 23);
            this.txtChangesetId.TabIndex = 50;
            // 
            // scTFSResult
            // 
            this.scTFSResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTFSResult.Location = new System.Drawing.Point(0, 0);
            this.scTFSResult.Name = "scTFSResult";
            this.scTFSResult.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTFSResult.Panel1
            // 
            this.scTFSResult.Panel1.Controls.Add(this.pnlBodyContainer);
            // 
            // scTFSResult.Panel2
            // 
            this.scTFSResult.Panel2.Controls.Add(this.panel3);
            this.scTFSResult.Panel2.Controls.Add(this.toolStrip1);
            this.scTFSResult.Size = new System.Drawing.Size(946, 700);
            this.scTFSResult.SplitterDistance = 400;
            this.scTFSResult.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tvFileChanges);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(946, 271);
            this.panel3.TabIndex = 16;
            // 
            // tvFileChanges
            // 
            this.tvFileChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFileChanges.FullRowSelect = true;
            this.tvFileChanges.HideSelection = false;
            this.tvFileChanges.Location = new System.Drawing.Point(0, 0);
            this.tvFileChanges.Name = "tvFileChanges";
            this.tvFileChanges.Size = new System.Drawing.Size(946, 271);
            this.tvFileChanges.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslSource,
            this.tscbSource,
            this.tslTarget,
            this.tscbTarget});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(946, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslSource
            // 
            this.tslSource.Name = "tslSource";
            this.tslSource.Size = new System.Drawing.Size(43, 22);
            this.tslSource.Text = "Source";
            // 
            // tscbSource
            // 
            this.tscbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbSource.Items.AddRange(new object[] {
            "Dev - Old",
            "Dev - New",
            "Test - Old",
            "Test - New",
            "Main - Old",
            "Main - New"});
            this.tscbSource.Name = "tscbSource";
            this.tscbSource.Size = new System.Drawing.Size(121, 25);
            // 
            // tslTarget
            // 
            this.tslTarget.Name = "tslTarget";
            this.tslTarget.Size = new System.Drawing.Size(40, 22);
            this.tslTarget.Text = "Target";
            // 
            // tscbTarget
            // 
            this.tscbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbTarget.Items.AddRange(new object[] {
            "Dev - Old",
            "Dev - New",
            "Test - Old",
            "Test - New",
            "Main - Old",
            "Main - New"});
            this.tscbTarget.Name = "tscbTarget";
            this.tscbTarget.Size = new System.Drawing.Size(121, 25);
            // 
            // scTFSMain
            // 
            this.scTFSMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTFSMain.Location = new System.Drawing.Point(0, 0);
            this.scTFSMain.Name = "scTFSMain";
            // 
            // scTFSMain.Panel1
            // 
            this.scTFSMain.Panel1.Controls.Add(this.pnlCriteria);
            // 
            // scTFSMain.Panel2
            // 
            this.scTFSMain.Panel2.Controls.Add(this.scTFSResult);
            this.scTFSMain.Size = new System.Drawing.Size(1200, 700);
            this.scTFSMain.SplitterDistance = 250;
            this.scTFSMain.TabIndex = 23;
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.AutoScroll = true;
            this.pnlCriteria.Controls.Add(this.pnlFilter);
            this.pnlCriteria.Controls.Add(this.pnlObjectType);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 0);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Size = new System.Drawing.Size(250, 700);
            this.pnlCriteria.TabIndex = 17;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.gbFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 106);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(250, 282);
            this.pnlFilter.TabIndex = 2;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.lblOwnerFilter);
            this.gbFilter.Controls.Add(this.txtCommentFilter);
            this.gbFilter.Controls.Add(this.lblCommentFilter);
            this.gbFilter.Controls.Add(this.lblChangesetId);
            this.gbFilter.Controls.Add(this.txtChangesetId);
            this.gbFilter.Controls.Add(this.chkShowOnlyUnmergedToMain);
            this.gbFilter.Controls.Add(this.chkShowOnlyUnmergedToTest);
            this.gbFilter.Controls.Add(this.dtpEndDate);
            this.gbFilter.Controls.Add(this.lblEndDate);
            this.gbFilter.Controls.Add(this.dtpStartDate);
            this.gbFilter.Controls.Add(this.lblStartDate);
            this.gbFilter.Controls.Add(this.cmbOwnerFilter);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(0, 0);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(250, 282);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // lblOwnerFilter
            // 
            this.lblOwnerFilter.AutoSize = true;
            this.lblOwnerFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOwnerFilter.Location = new System.Drawing.Point(7, 100);
            this.lblOwnerFilter.Name = "lblOwnerFilter";
            this.lblOwnerFilter.Size = new System.Drawing.Size(29, 13);
            this.lblOwnerFilter.TabIndex = 49;
            this.lblOwnerFilter.Text = "User";
            // 
            // txtCommentFilter
            // 
            this.txtCommentFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommentFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCommentFilter.Location = new System.Drawing.Point(10, 32);
            this.txtCommentFilter.Name = "txtCommentFilter";
            this.txtCommentFilter.Size = new System.Drawing.Size(234, 23);
            this.txtCommentFilter.TabIndex = 48;
            // 
            // lblCommentFilter
            // 
            this.lblCommentFilter.AutoSize = true;
            this.lblCommentFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommentFilter.Location = new System.Drawing.Point(7, 16);
            this.lblCommentFilter.Name = "lblCommentFilter";
            this.lblCommentFilter.Size = new System.Drawing.Size(105, 13);
            this.lblCommentFilter.TabIndex = 47;
            this.lblCommentFilter.Text = "Changeset Comment";
            // 
            // chkShowOnlyUnmergedToMain
            // 
            this.chkShowOnlyUnmergedToMain.AutoSize = true;
            this.chkShowOnlyUnmergedToMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowOnlyUnmergedToMain.Location = new System.Drawing.Point(10, 252);
            this.chkShowOnlyUnmergedToMain.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.chkShowOnlyUnmergedToMain.Name = "chkShowOnlyUnmergedToMain";
            this.chkShowOnlyUnmergedToMain.Size = new System.Drawing.Size(163, 17);
            this.chkShowOnlyUnmergedToMain.TabIndex = 46;
            this.chkShowOnlyUnmergedToMain.Text = "Show only unmerged to Main";
            this.chkShowOnlyUnmergedToMain.UseVisualStyleBackColor = true;
            // 
            // chkShowOnlyUnmergedToTest
            // 
            this.chkShowOnlyUnmergedToTest.AutoSize = true;
            this.chkShowOnlyUnmergedToTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowOnlyUnmergedToTest.Location = new System.Drawing.Point(10, 229);
            this.chkShowOnlyUnmergedToTest.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.chkShowOnlyUnmergedToTest.Name = "chkShowOnlyUnmergedToTest";
            this.chkShowOnlyUnmergedToTest.Size = new System.Drawing.Size(161, 17);
            this.chkShowOnlyUnmergedToTest.TabIndex = 45;
            this.chkShowOnlyUnmergedToTest.Text = "Show only unmerged to Test";
            this.chkShowOnlyUnmergedToTest.UseVisualStyleBackColor = true;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(10, 198);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowCheckBox = true;
            this.dtpEndDate.Size = new System.Drawing.Size(234, 20);
            this.dtpEndDate.TabIndex = 44;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(7, 181);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblEndDate.TabIndex = 43;
            this.lblEndDate.Text = "End Date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(10, 158);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowCheckBox = true;
            this.dtpStartDate.Size = new System.Drawing.Size(234, 20);
            this.dtpStartDate.TabIndex = 42;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(7, 141);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 41;
            this.lblStartDate.Text = "Start Date";
            // 
            // cmbOwnerFilter
            // 
            this.cmbOwnerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOwnerFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOwnerFilter.FormattingEnabled = true;
            this.cmbOwnerFilter.Location = new System.Drawing.Point(10, 117);
            this.cmbOwnerFilter.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.cmbOwnerFilter.Name = "cmbOwnerFilter";
            this.cmbOwnerFilter.Size = new System.Drawing.Size(234, 21);
            this.cmbOwnerFilter.TabIndex = 40;
            // 
            // pnlObjectType
            // 
            this.pnlObjectType.Controls.Add(this.gbTFSSettings);
            this.pnlObjectType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlObjectType.Location = new System.Drawing.Point(0, 0);
            this.pnlObjectType.Name = "pnlObjectType";
            this.pnlObjectType.Size = new System.Drawing.Size(250, 106);
            this.pnlObjectType.TabIndex = 18;
            // 
            // gbTFSSettings
            // 
            this.gbTFSSettings.Controls.Add(this.txtTFSPath);
            this.gbTFSSettings.Controls.Add(this.txtTFSUrl);
            this.gbTFSSettings.Controls.Add(this.lblTFSPath);
            this.gbTFSSettings.Controls.Add(this.lblTFSUrl);
            this.gbTFSSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTFSSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTFSSettings.Location = new System.Drawing.Point(0, 0);
            this.gbTFSSettings.Name = "gbTFSSettings";
            this.gbTFSSettings.Size = new System.Drawing.Size(250, 106);
            this.gbTFSSettings.TabIndex = 10;
            this.gbTFSSettings.TabStop = false;
            this.gbTFSSettings.Text = "TFS Settings";
            // 
            // txtTFSPath
            // 
            this.txtTFSPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTFSPath.BackColor = System.Drawing.Color.Linen;
            this.txtTFSPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTFSPath.Location = new System.Drawing.Point(9, 76);
            this.txtTFSPath.Name = "txtTFSPath";
            this.txtTFSPath.Size = new System.Drawing.Size(235, 23);
            this.txtTFSPath.TabIndex = 36;
            this.txtTFSPath.Text = "ProductAndDelivery/Destek";
            // 
            // txtTFSUrl
            // 
            this.txtTFSUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTFSUrl.BackColor = System.Drawing.Color.Linen;
            this.txtTFSUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTFSUrl.Location = new System.Drawing.Point(9, 34);
            this.txtTFSUrl.Name = "txtTFSUrl";
            this.txtTFSUrl.Size = new System.Drawing.Size(235, 23);
            this.txtTFSUrl.TabIndex = 35;
            this.txtTFSUrl.Text = "https://dev.azure.com/arc-product";
            // 
            // lblTFSPath
            // 
            this.lblTFSPath.AutoSize = true;
            this.lblTFSPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTFSPath.Location = new System.Drawing.Point(6, 60);
            this.lblTFSPath.Name = "lblTFSPath";
            this.lblTFSPath.Size = new System.Drawing.Size(52, 13);
            this.lblTFSPath.TabIndex = 23;
            this.lblTFSPath.Text = "TFS Path";
            // 
            // lblTFSUrl
            // 
            this.lblTFSUrl.AutoSize = true;
            this.lblTFSUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTFSUrl.Location = new System.Drawing.Point(6, 18);
            this.lblTFSUrl.Name = "lblTFSUrl";
            this.lblTFSUrl.Size = new System.Drawing.Size(52, 13);
            this.lblTFSUrl.TabIndex = 21;
            this.lblTFSUrl.Text = "TFS URL";
            // 
            // ucTfsChangesetSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scTFSMain);
            this.Name = "ucTfsChangesetSearch";
            this.Size = new System.Drawing.Size(1200, 700);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChangesets)).EndInit();
            this.pnlBodyContainer.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.scTFSResult.Panel1.ResumeLayout(false);
            this.scTFSResult.Panel2.ResumeLayout(false);
            this.scTFSResult.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTFSResult)).EndInit();
            this.scTFSResult.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.scTFSMain.Panel1.ResumeLayout(false);
            this.scTFSMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTFSMain)).EndInit();
            this.scTFSMain.ResumeLayout(false);
            this.pnlCriteria.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.pnlObjectType.ResumeLayout(false);
            this.gbTFSSettings.ResumeLayout(false);
            this.gbTFSSettings.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbCriteriaCollapse;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsbExportToExcel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.Panel panel1;
		private NAdvancedDataGridView dgvChangesets;
		private System.Windows.Forms.Panel pnlBodyContainer;
		private System.Windows.Forms.Panel pnlBody;
		private System.Windows.Forms.Panel pnlGrid;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.SplitContainer scTFSResult;
		private System.Windows.Forms.SplitContainer scTFSMain;
		private System.Windows.Forms.Panel pnlCriteria;
		private System.Windows.Forms.Panel pnlObjectType;
		private System.Windows.Forms.GroupBox gbTFSSettings;
		private System.Windows.Forms.Label lblTFSUrl;
		private System.Windows.Forms.Label lblTFSPath;
		private System.Windows.Forms.ToolStripButton tsbStartTFSSearch;
		private System.Windows.Forms.ToolStripButton tsbCancelSearch;
		private System.Windows.Forms.ToolStripButton tsbRefreshUsers;
		private System.Windows.Forms.TextBox txtTFSUrl;
		private System.Windows.Forms.TextBox txtTFSPath;
		private System.Windows.Forms.Panel pnlFilter;
		private System.Windows.Forms.GroupBox gbFilter;
		private System.Windows.Forms.Label lblOwnerFilter;
		private System.Windows.Forms.TextBox txtCommentFilter;
		private System.Windows.Forms.Label lblCommentFilter;
		private System.Windows.Forms.CheckBox chkShowOnlyUnmergedToMain;
		private System.Windows.Forms.CheckBox chkShowOnlyUnmergedToTest;
		private System.Windows.Forms.DateTimePicker dtpEndDate;
		private System.Windows.Forms.Label lblEndDate;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.Label lblStartDate;
		private System.Windows.Forms.ComboBox cmbOwnerFilter;
		private System.Windows.Forms.ToolStripButton tsbCleanFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.TreeView tvFileChanges;
		private System.Windows.Forms.Label lblChangesetId;
		private System.Windows.Forms.TextBox txtChangesetId;
		private System.Windows.Forms.DataGridViewTextBoxColumn colChangesetId;
		private System.Windows.Forms.DataGridViewTextBoxColumn colCreationDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn colOwner;
		private System.Windows.Forms.DataGridViewTextBoxColumn colComment;
		private System.Windows.Forms.DataGridViewTextBoxColumn colBranch;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSolutions;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colMergedToTest;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTestChangesetId;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTestMergeDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTestMergeUser;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colMergedToMain;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMainChangesetId;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMainMergeDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMainMergeUser;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslSource;
        private System.Windows.Forms.ToolStripComboBox tscbSource;
        private System.Windows.Forms.ToolStripLabel tslTarget;
        private System.Windows.Forms.ToolStripComboBox tscbTarget;
    }
}
