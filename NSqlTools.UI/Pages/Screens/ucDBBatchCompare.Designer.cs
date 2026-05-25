using NSqlTools.Lib.Controls;
using NSqlTools.UI.UserControls;

namespace NSqlTools.UI.Pages
{
	partial class ucDBBatchCompare
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
			if (disposing)
			{
				// Cancel and dispose BackgroundWorker
				if (backgroundWorker != null)
				{
					if (backgroundWorker.IsBusy)
					{
						backgroundWorker.CancelAsync();
					}
					backgroundWorker.DoWork -= backgroundWorker_DoWork;
					backgroundWorker.ProgressChanged -= backgroundWorker_ProgressChanged;
					backgroundWorker.RunWorkerCompleted -= backgroundWorker_RunWorkerCompleted;
					backgroundWorker.Dispose();
					backgroundWorker = null;
				}

				// Cancel and dispose CancellationTokenSource
				if (_cancellationTokenSource != null)
				{
					_cancellationTokenSource.Cancel();
					_cancellationTokenSource.Dispose();
					_cancellationTokenSource = null;
				}

				if (components != null)
				{
					components.Dispose();
				}
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDBBatchCompare));
            this.pnlBatchSourceSelect = new System.Windows.Forms.Panel();
            this.ucDBObjectSelectTarget = new NSqlTools.UI.UserControls.ucDBObjectSelect();
            this.ucDBObjectSelectSource = new NSqlTools.UI.UserControls.ucDBObjectSelect();
            this.pnlObjectType = new System.Windows.Forms.Panel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this._ucObjectType = new NSqlTools.UI.UserControls.ucObjectType();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.txtNameFilter = new System.Windows.Forms.TextBox();
            this.lblNameFilter = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvBatchCompare = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
            this.SchemaNameSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Difference = new System.Windows.Forms.DataGridViewImageColumn();
            this.SchemaNameTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbCriteriaCollapse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStartBatchCompare = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelBatchCompare = new System.Windows.Forms.ToolStripButton();
            this.tsbExportBatchCompareResultToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEqual = new System.Windows.Forms.ToolStripButton();
            this.tsbNotEqual = new System.Windows.Forms.ToolStripButton();
            this.tsbSourceExists = new System.Windows.Forms.ToolStripButton();
            this.tsbTargetExists = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlBodyContainer = new System.Windows.Forms.Panel();
            this.scCompareListAndViewer = new System.Windows.Forms.SplitContainer();
            this.ucTableViewCompareControl = new NSqlTools.UI.UserControls.ucTableViewCompare();
            this.ucNotePadCompareControl = new NSqlTools.UI.UserControls.ucNotePadCompare();
            this.scBatchCompare = new System.Windows.Forms.SplitContainer();
            this.pnlBatchSourceSelect.SuspendLayout();
            this.pnlObjectType.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchCompare)).BeginInit();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.pnlBodyContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCompareListAndViewer)).BeginInit();
            this.scCompareListAndViewer.Panel1.SuspendLayout();
            this.scCompareListAndViewer.Panel2.SuspendLayout();
            this.scCompareListAndViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBatchCompare)).BeginInit();
            this.scBatchCompare.Panel1.SuspendLayout();
            this.scBatchCompare.Panel2.SuspendLayout();
            this.scBatchCompare.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBatchSourceSelect
            // 
            this.pnlBatchSourceSelect.AutoScroll = true;
            this.pnlBatchSourceSelect.Controls.Add(this.ucDBObjectSelectTarget);
            this.pnlBatchSourceSelect.Controls.Add(this.ucDBObjectSelectSource);
            this.pnlBatchSourceSelect.Controls.Add(this.pnlObjectType);
            this.pnlBatchSourceSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBatchSourceSelect.Location = new System.Drawing.Point(0, 0);
            this.pnlBatchSourceSelect.Name = "pnlBatchSourceSelect";
            this.pnlBatchSourceSelect.Size = new System.Drawing.Size(250, 627);
            this.pnlBatchSourceSelect.TabIndex = 17;
            // 
            // ucDBObjectSelectTarget
            // 
            this.ucDBObjectSelectTarget.AllowOnlyOneDBSelection = true;
            this.ucDBObjectSelectTarget.Caption = "Target Schema";
            this.ucDBObjectSelectTarget.DBObjectVisibility = false;
            this.ucDBObjectSelectTarget.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDBObjectSelectTarget.IsRequiredConnectionString = true;
            this.ucDBObjectSelectTarget.IsRequiredDB = true;
            this.ucDBObjectSelectTarget.IsRequiredDBObject = false;
            this.ucDBObjectSelectTarget.IsRequiredObjectType = false;
            this.ucDBObjectSelectTarget.IsRequiredSchema = true;
            this.ucDBObjectSelectTarget.Location = new System.Drawing.Point(0, 298);
            this.ucDBObjectSelectTarget.MainForm = null;
            this.ucDBObjectSelectTarget.Name = "ucDBObjectSelectTarget";
            this.ucDBObjectSelectTarget.ObjectTypeVisibility = false;
            this.ucDBObjectSelectTarget.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.ucDBObjectSelectTarget.ParentTabPage = null;
            this.ucDBObjectSelectTarget.SchemaVisibility = true;
            this.ucDBObjectSelectTarget.SelectedConnectionNameValue = null;
            this.ucDBObjectSelectTarget.SelectedDBIndexes = null;
            this.ucDBObjectSelectTarget.SelectedDBObjectObjectId = null;
            this.ucDBObjectSelectTarget.SelectedObjectType2 = null;
            this.ucDBObjectSelectTarget.SelectedSchemaId = null;
            this.ucDBObjectSelectTarget.Size = new System.Drawing.Size(250, 206);
            this.ucDBObjectSelectTarget.TabIndex = 4;
            this.ucDBObjectSelectTarget.TabIndexConnectionString = 1;
            this.ucDBObjectSelectTarget.TabIndexDB = 2;
            this.ucDBObjectSelectTarget.TabIndexDBObject = 6;
            this.ucDBObjectSelectTarget.TabIndexDBObjectFilter = 5;
            this.ucDBObjectSelectTarget.TabIndexObjectType = 3;
            this.ucDBObjectSelectTarget.TabIndexSchema = 4;
            this.ucDBObjectSelectTarget.TitleVisibility = null;
            this.ucDBObjectSelectTarget.OnSchemaChanged += new System.EventHandler(this.ucDBObjectSelectTarget_OnSchemaChanged);
            this.ucDBObjectSelectTarget.OnSchemaClear += new System.EventHandler(this.ucDBObjectSelectTarget_OnSchemaClear);
            // 
            // ucDBObjectSelectSource
            // 
            this.ucDBObjectSelectSource.AllowOnlyOneDBSelection = true;
            this.ucDBObjectSelectSource.Caption = "Source Schema";
            this.ucDBObjectSelectSource.DBObjectVisibility = false;
            this.ucDBObjectSelectSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDBObjectSelectSource.IsRequiredConnectionString = true;
            this.ucDBObjectSelectSource.IsRequiredDB = true;
            this.ucDBObjectSelectSource.IsRequiredDBObject = false;
            this.ucDBObjectSelectSource.IsRequiredObjectType = false;
            this.ucDBObjectSelectSource.IsRequiredSchema = true;
            this.ucDBObjectSelectSource.Location = new System.Drawing.Point(0, 99);
            this.ucDBObjectSelectSource.MainForm = null;
            this.ucDBObjectSelectSource.Name = "ucDBObjectSelectSource";
            this.ucDBObjectSelectSource.ObjectTypeVisibility = false;
            this.ucDBObjectSelectSource.ParentTabPage = null;
            this.ucDBObjectSelectSource.SchemaVisibility = true;
            this.ucDBObjectSelectSource.SelectedConnectionNameValue = null;
            this.ucDBObjectSelectSource.SelectedDBIndexes = null;
            this.ucDBObjectSelectSource.SelectedDBObjectObjectId = null;
            this.ucDBObjectSelectSource.SelectedObjectType2 = null;
            this.ucDBObjectSelectSource.SelectedSchemaId = null;
            this.ucDBObjectSelectSource.Size = new System.Drawing.Size(250, 199);
            this.ucDBObjectSelectSource.TabIndex = 3;
            this.ucDBObjectSelectSource.TabIndexConnectionString = 1;
            this.ucDBObjectSelectSource.TabIndexDB = 2;
            this.ucDBObjectSelectSource.TabIndexDBObject = 6;
            this.ucDBObjectSelectSource.TabIndexDBObjectFilter = 5;
            this.ucDBObjectSelectSource.TabIndexObjectType = 3;
            this.ucDBObjectSelectSource.TabIndexSchema = 4;
            this.ucDBObjectSelectSource.TitleVisibility = null;
            this.ucDBObjectSelectSource.OnSchemaChanged += new System.EventHandler(this.ucDBObjectSelectSource_OnSchemaChanged);
            this.ucDBObjectSelectSource.OnSchemaClear += new System.EventHandler(this.ucDBObjectSelectSource_OnSchemaClear);
            this.ucDBObjectSelectSource.OnObjectTypeChanged += new System.EventHandler(this.ucDBObjectSelectSource_OnObjectTypeChanged);
            // 
            // pnlObjectType
            // 
            this.pnlObjectType.Controls.Add(this.gbFilter);
            this.pnlObjectType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlObjectType.Location = new System.Drawing.Point(0, 0);
            this.pnlObjectType.Name = "pnlObjectType";
            this.pnlObjectType.Size = new System.Drawing.Size(250, 99);
            this.pnlObjectType.TabIndex = 18;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this._ucObjectType);
            this.gbFilter.Controls.Add(this.lblObjectType);
            this.gbFilter.Controls.Add(this.txtNameFilter);
            this.gbFilter.Controls.Add(this.lblNameFilter);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(0, 0);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(250, 99);
            this.gbFilter.TabIndex = 10;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // _ucObjectType
            // 
            this._ucObjectType.IsNullable = false;
            this._ucObjectType.Location = new System.Drawing.Point(6, 27);
            this._ucObjectType.MainForm = null;
            this._ucObjectType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._ucObjectType.Name = "_ucObjectType";
            this._ucObjectType.ParentTabPage = null;
            this._ucObjectType.Size = new System.Drawing.Size(239, 25);
            this._ucObjectType.TabIndex = 24;
            this._ucObjectType.OnObjectTypeChanged += new System.EventHandler(this._ucObjectType_OnObjectTypeChanged);
            // 
            // lblObjectType
            // 
            this.lblObjectType.AutoSize = true;
            this.lblObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObjectType.Location = new System.Drawing.Point(6, 14);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(65, 13);
            this.lblObjectType.TabIndex = 23;
            this.lblObjectType.Text = "Object Type";
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNameFilter.Location = new System.Drawing.Point(6, 69);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(240, 23);
            this.txtNameFilter.TabIndex = 2;
            // 
            // lblNameFilter
            // 
            this.lblNameFilter.AutoSize = true;
            this.lblNameFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameFilter.Location = new System.Drawing.Point(6, 55);
            this.lblNameFilter.Name = "lblNameFilter";
            this.lblNameFilter.Size = new System.Drawing.Size(60, 13);
            this.lblNameFilter.TabIndex = 21;
            this.lblNameFilter.Text = "Name Filter";
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlGrid);
            this.pnlBody.Controls.Add(this.tsMenu);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(809, 484);
            this.pnlBody.TabIndex = 18;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.panel1);
            this.pnlGrid.Controls.Add(this.panel2);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 31);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(809, 453);
            this.pnlGrid.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvBatchCompare);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 431);
            this.panel1.TabIndex = 3;
            // 
            // dgvBatchCompare
            // 
            this.dgvBatchCompare.AllowUserToAddRows = false;
            this.dgvBatchCompare.AllowUserToDeleteRows = false;
            this.dgvBatchCompare.AllowUserToOrderColumns = true;
            this.dgvBatchCompare.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBatchCompare.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBatchCompare.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchCompare.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SchemaNameSource,
            this.NameSource,
            this.Difference,
            this.SchemaNameTarget,
            this.NameTarget});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBatchCompare.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBatchCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBatchCompare.EnableHeadersVisualStyles = false;
            this.dgvBatchCompare.FilterAndSortEnabled = true;
            this.dgvBatchCompare.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvBatchCompare.Location = new System.Drawing.Point(0, 0);
            this.dgvBatchCompare.MaxFilterButtonImageHeight = 23;
            this.dgvBatchCompare.Name = "dgvBatchCompare";
            this.dgvBatchCompare.ReadOnly = true;
            this.dgvBatchCompare.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dgvBatchCompare.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBatchCompare.RowTemplate.Height = 26;
            this.dgvBatchCompare.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatchCompare.Size = new System.Drawing.Size(809, 431);
            this.dgvBatchCompare.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvBatchCompare.TabIndex = 0;
            this.dgvBatchCompare.SelectionChanged += new System.EventHandler(this.gvBatchCompare_SelectionChanged);
            // 
            // SchemaNameSource
            // 
            this.SchemaNameSource.DataPropertyName = "SchemaNameSource";
            this.SchemaNameSource.HeaderText = "Schema Source";
            this.SchemaNameSource.MinimumWidth = 24;
            this.SchemaNameSource.Name = "SchemaNameSource";
            this.SchemaNameSource.ReadOnly = true;
            this.SchemaNameSource.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.SchemaNameSource.Width = 101;
            // 
            // NameSource
            // 
            this.NameSource.DataPropertyName = "NameSource";
            this.NameSource.HeaderText = "Name Source";
            this.NameSource.MinimumWidth = 24;
            this.NameSource.Name = "NameSource";
            this.NameSource.ReadOnly = true;
            this.NameSource.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.NameSource.Width = 200;
            // 
            // Difference
            // 
            this.Difference.DataPropertyName = "Difference";
            this.Difference.HeaderText = "Difference";
            this.Difference.MinimumWidth = 24;
            this.Difference.Name = "Difference";
            this.Difference.ReadOnly = true;
            this.Difference.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // SchemaNameTarget
            // 
            this.SchemaNameTarget.DataPropertyName = "SchemaNameTarget";
            this.SchemaNameTarget.HeaderText = "Schema Target";
            this.SchemaNameTarget.MinimumWidth = 24;
            this.SchemaNameTarget.Name = "SchemaNameTarget";
            this.SchemaNameTarget.ReadOnly = true;
            this.SchemaNameTarget.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // NameTarget
            // 
            this.NameTarget.DataPropertyName = "NameTarget";
            this.NameTarget.HeaderText = "Name Target";
            this.NameTarget.MinimumWidth = 24;
            this.NameTarget.Name = "NameTarget";
            this.NameTarget.ReadOnly = true;
            this.NameTarget.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.NameTarget.Width = 200;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(809, 22);
            this.panel2.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(809, 22);
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
            this.tsbStartBatchCompare,
            this.tsbCancelBatchCompare,
            this.tsbExportBatchCompareResultToExcel,
            this.toolStripSeparator1,
            this.tsbEqual,
            this.tsbNotEqual,
            this.tsbSourceExists,
            this.tsbTargetExists,
            this.toolStripSeparator2,
            this.progressBar});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(809, 31);
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
            // tsbStartBatchCompare
            // 
            this.tsbStartBatchCompare.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStartBatchCompare.Enabled = false;
            this.tsbStartBatchCompare.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
            this.tsbStartBatchCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStartBatchCompare.Name = "tsbStartBatchCompare";
            this.tsbStartBatchCompare.Size = new System.Drawing.Size(28, 28);
            this.tsbStartBatchCompare.Text = "Start DB Batch Compare";
            this.tsbStartBatchCompare.Click += new System.EventHandler(this.tsbStartBatchCompare_Click);
            // 
            // tsbCancelBatchCompare
            // 
            this.tsbCancelBatchCompare.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancelBatchCompare.Enabled = false;
            this.tsbCancelBatchCompare.Image = global::NSqlTools.UI.Properties.Resources.CloseBlue;
            this.tsbCancelBatchCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelBatchCompare.Name = "tsbCancelBatchCompare";
            this.tsbCancelBatchCompare.Size = new System.Drawing.Size(28, 28);
            this.tsbCancelBatchCompare.Text = "Cancel Comparison";
            this.tsbCancelBatchCompare.Click += new System.EventHandler(this.tsbCancelBatchCompare_Click);
            // 
            // tsbExportBatchCompareResultToExcel
            // 
            this.tsbExportBatchCompareResultToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportBatchCompareResultToExcel.Image = global::NSqlTools.UI.Properties.Resources.Excel;
            this.tsbExportBatchCompareResultToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportBatchCompareResultToExcel.Name = "tsbExportBatchCompareResultToExcel";
            this.tsbExportBatchCompareResultToExcel.Size = new System.Drawing.Size(28, 28);
            this.tsbExportBatchCompareResultToExcel.Text = "Export Search Result To Excel";
            this.tsbExportBatchCompareResultToExcel.Click += new System.EventHandler(this.tsbExportBatchCompareResultToExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbEqual
            // 
            this.tsbEqual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEqual.Image = global::NSqlTools.UI.Properties.Resources.Equality_Equal_Batch;
            this.tsbEqual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEqual.Name = "tsbEqual";
            this.tsbEqual.Size = new System.Drawing.Size(28, 28);
            this.tsbEqual.Text = "Equal";
            this.tsbEqual.Click += new System.EventHandler(this.filterColumnsGrid);
            // 
            // tsbNotEqual
            // 
            this.tsbNotEqual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNotEqual.Image = global::NSqlTools.UI.Properties.Resources.Equality_NotEqual_Batch;
            this.tsbNotEqual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNotEqual.Name = "tsbNotEqual";
            this.tsbNotEqual.Size = new System.Drawing.Size(28, 28);
            this.tsbNotEqual.Text = "Not Equal";
            this.tsbNotEqual.Click += new System.EventHandler(this.filterColumnsGrid);
            // 
            // tsbSourceExists
            // 
            this.tsbSourceExists.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSourceExists.Image = global::NSqlTools.UI.Properties.Resources.Equality_SourceExists_Batch;
            this.tsbSourceExists.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSourceExists.Name = "tsbSourceExists";
            this.tsbSourceExists.Size = new System.Drawing.Size(28, 28);
            this.tsbSourceExists.Text = "Exists In Source";
            this.tsbSourceExists.Click += new System.EventHandler(this.filterColumnsGrid);
            // 
            // tsbTargetExists
            // 
            this.tsbTargetExists.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTargetExists.Image = global::NSqlTools.UI.Properties.Resources.Equality_TargetExists_Batch;
            this.tsbTargetExists.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTargetExists.Name = "tsbTargetExists";
            this.tsbTargetExists.Size = new System.Drawing.Size(28, 28);
            this.tsbTargetExists.Text = "Exists In Target";
            this.tsbTargetExists.Click += new System.EventHandler(this.filterColumnsGrid);
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
            // pnlBodyContainer
            // 
            this.pnlBodyContainer.Controls.Add(this.pnlBody);
            this.pnlBodyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBodyContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlBodyContainer.Name = "pnlBodyContainer";
            this.pnlBodyContainer.Size = new System.Drawing.Size(809, 484);
            this.pnlBodyContainer.TabIndex = 20;
            // 
            // scCompareListAndViewer
            // 
            this.scCompareListAndViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCompareListAndViewer.Location = new System.Drawing.Point(0, 0);
            this.scCompareListAndViewer.Name = "scCompareListAndViewer";
            this.scCompareListAndViewer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCompareListAndViewer.Panel1
            // 
            this.scCompareListAndViewer.Panel1.Controls.Add(this.pnlBodyContainer);
            // 
            // scCompareListAndViewer.Panel2
            // 
            this.scCompareListAndViewer.Panel2.Controls.Add(this.ucTableViewCompareControl);
            this.scCompareListAndViewer.Panel2.Controls.Add(this.ucNotePadCompareControl);
            this.scCompareListAndViewer.Size = new System.Drawing.Size(809, 627);
            this.scCompareListAndViewer.SplitterDistance = 484;
            this.scCompareListAndViewer.TabIndex = 21;
            // 
            // ucTableViewCompareControl
            // 
            this.ucTableViewCompareControl.DataSource = null;
            this.ucTableViewCompareControl.Location = new System.Drawing.Point(449, 12);
            this.ucTableViewCompareControl.MainForm = null;
            this.ucTableViewCompareControl.Name = "ucTableViewCompareControl";
            this.ucTableViewCompareControl.ParentTabPage = null;
            this.ucTableViewCompareControl.Size = new System.Drawing.Size(850, 474);
            this.ucTableViewCompareControl.TabIndex = 22;
            // 
            // ucNotePadCompareControl
            // 
            this.ucNotePadCompareControl.CaseSensitive = false;
            this.ucNotePadCompareControl.CompareTypeVisible = false;
            this.ucNotePadCompareControl.DisplayFullScreen = true;
            this.ucNotePadCompareControl.FindResultCollapsed = true;
            this.ucNotePadCompareControl.FontSize = 12;
            this.ucNotePadCompareControl.Location = new System.Drawing.Point(14, 12);
            this.ucNotePadCompareControl.MainForm = null;
            this.ucNotePadCompareControl.Name = "ucNotePadCompareControl";
            this.ucNotePadCompareControl.ParentTabPage = null;
            this.ucNotePadCompareControl.Size = new System.Drawing.Size(399, 217);
            this.ucNotePadCompareControl.SourceDBObjectName = null;
            this.ucNotePadCompareControl.SourceSchemaName = null;
            this.ucNotePadCompareControl.StatusBarPanelIsVisible = true;
            this.ucNotePadCompareControl.TabIndex = 21;
            this.ucNotePadCompareControl.TargetDBObjectName = null;
            this.ucNotePadCompareControl.TargetSchemaName = null;
            // 
            // scBatchCompare
            // 
            this.scBatchCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBatchCompare.Location = new System.Drawing.Point(0, 0);
            this.scBatchCompare.Name = "scBatchCompare";
            // 
            // scBatchCompare.Panel1
            // 
            this.scBatchCompare.Panel1.Controls.Add(this.pnlBatchSourceSelect);
            // 
            // scBatchCompare.Panel2
            // 
            this.scBatchCompare.Panel2.Controls.Add(this.scCompareListAndViewer);
            this.scBatchCompare.Size = new System.Drawing.Size(1063, 627);
            this.scBatchCompare.SplitterDistance = 250;
            this.scBatchCompare.TabIndex = 22;
            // 
            // ucDBBatchCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scBatchCompare);
            this.Name = "ucDBBatchCompare";
            this.Size = new System.Drawing.Size(1063, 627);
            this.pnlBatchSourceSelect.ResumeLayout(false);
            this.pnlObjectType.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchCompare)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.pnlBodyContainer.ResumeLayout(false);
            this.scCompareListAndViewer.Panel1.ResumeLayout(false);
            this.scCompareListAndViewer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scCompareListAndViewer)).EndInit();
            this.scCompareListAndViewer.ResumeLayout(false);
            this.scBatchCompare.Panel1.ResumeLayout(false);
            this.scBatchCompare.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scBatchCompare)).EndInit();
            this.scBatchCompare.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBatchSourceSelect;
		private ucDBObjectSelect ucDBObjectSelectTarget;
		private ucDBObjectSelect ucDBObjectSelectSource;
		private System.Windows.Forms.Panel pnlObjectType;
		private System.Windows.Forms.GroupBox gbFilter;
		private System.Windows.Forms.Panel pnlBody;
		private System.Windows.Forms.Panel pnlGrid;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbEqual;
		private System.Windows.Forms.ToolStripButton tsbNotEqual;
		private System.Windows.Forms.ToolStripButton tsbSourceExists;
		private System.Windows.Forms.ToolStripButton tsbTargetExists;
		private System.Windows.Forms.Panel pnlBodyContainer;
		private System.Windows.Forms.SplitContainer scCompareListAndViewer;
		private System.Windows.Forms.SplitContainer scBatchCompare;
		private ucTableViewCompare ucTableViewCompareControl;
		private ucNotePadCompare ucNotePadCompareControl;
		private System.Windows.Forms.TextBox txtNameFilter;
		private System.Windows.Forms.Label lblNameFilter;
		private System.Windows.Forms.Label lblObjectType;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbStartBatchCompare;
		private System.Windows.Forms.ToolStripButton tsbCancelBatchCompare;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripButton tsbCriteriaCollapse;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripButton tsbExportBatchCompareResultToExcel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private ucObjectType _ucObjectType;
		private System.Windows.Forms.DataGridViewTextBoxColumn SchemaNameSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameSource;
		private System.Windows.Forms.DataGridViewImageColumn Difference;
		private System.Windows.Forms.DataGridViewTextBoxColumn SchemaNameTarget;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameTarget;
		private NAdvancedDataGridView dgvBatchCompare;
	}
}
