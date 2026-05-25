using NSqlTools.Lib.Controls;
using NSqlTools.Types.Properties;
using Zuby.ADGV;

namespace NSqlTools.UI.Pages
{
	partial class ucConnectionStrings
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbAddConnectionString = new System.Windows.Forms.ToolStripButton();
			this.tsbEditConnectionString = new System.Windows.Forms.ToolStripButton();
			this.tsbDeleteConnectionString = new System.Windows.Forms.ToolStripButton();
			this.pnlConnectionStrings = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.dgvConnectionStrings = new NAdvancedDataGridView();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DataSourceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IntegratedSecurityColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.UserNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.InitialCatalogColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tsMenu.SuspendLayout();
			this.pnlConnectionStrings.SuspendLayout();
			this.panel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvConnectionStrings)).BeginInit();
			this.SuspendLayout();
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddConnectionString,
            this.tsbEditConnectionString,
            this.tsbDeleteConnectionString});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(544, 31);
			this.tsMenu.TabIndex = 20;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbAddConnectionString
			// 
			this.tsbAddConnectionString.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAddConnectionString.Image = global::NSqlTools.UI.Properties.Resources.Add;
			this.tsbAddConnectionString.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAddConnectionString.Name = "tsbAddConnectionString";
			this.tsbAddConnectionString.Size = new System.Drawing.Size(28, 28);
			this.tsbAddConnectionString.Text = "Add Data Source";
			this.tsbAddConnectionString.Click += new System.EventHandler(this.tsbAddDataSource_Click);
			// 
			// tsbEditConnectionString
			// 
			this.tsbEditConnectionString.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbEditConnectionString.Enabled = false;
			this.tsbEditConnectionString.Image = global::NSqlTools.UI.Properties.Resources.Edit;
			this.tsbEditConnectionString.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbEditConnectionString.Name = "tsbEditConnectionString";
			this.tsbEditConnectionString.Size = new System.Drawing.Size(28, 28);
			this.tsbEditConnectionString.Text = "Edit Data Source";
			this.tsbEditConnectionString.Click += new System.EventHandler(this.tsbEditDataSource_Click);
			// 
			// tsbDeleteConnectionString
			// 
			this.tsbDeleteConnectionString.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDeleteConnectionString.Enabled = false;
			this.tsbDeleteConnectionString.Image = global::NSqlTools.UI.Properties.Resources.Delete;
			this.tsbDeleteConnectionString.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDeleteConnectionString.Name = "tsbDeleteConnectionString";
			this.tsbDeleteConnectionString.Size = new System.Drawing.Size(28, 28);
			this.tsbDeleteConnectionString.Text = "Delete Data Source";
			this.tsbDeleteConnectionString.Click += new System.EventHandler(this.tsbDeleteDataSource_Click);
			// 
			// pnlConnectionStrings
			// 
			this.pnlConnectionStrings.Controls.Add(this.panel2);
			this.pnlConnectionStrings.Controls.Add(this.dgvConnectionStrings);
			this.pnlConnectionStrings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlConnectionStrings.Location = new System.Drawing.Point(0, 31);
			this.pnlConnectionStrings.Name = "pnlConnectionStrings";
			this.pnlConnectionStrings.Size = new System.Drawing.Size(544, 373);
			this.pnlConnectionStrings.TabIndex = 21;
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.statusStrip1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 351);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(544, 22);
			this.panel2.TabIndex = 2;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(544, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 17);
			this.lblStatus.Text = " ";
			// 
			// dgvConnectionStrings
			// 
			this.dgvConnectionStrings.AllowUserToAddRows = false;
			this.dgvConnectionStrings.AllowUserToOrderColumns = true;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvConnectionStrings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvConnectionStrings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvConnectionStrings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.DataSourceColumn,
            this.IntegratedSecurityColumn,
            this.UserNameColumn,
            this.InitialCatalogColumn});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvConnectionStrings.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvConnectionStrings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvConnectionStrings.FilterAndSortEnabled = true;
			this.dgvConnectionStrings.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvConnectionStrings.Location = new System.Drawing.Point(0, 0);
			this.dgvConnectionStrings.MaxFilterButtonImageHeight = 23;
			this.dgvConnectionStrings.Name = "dgvConnectionStrings";
			this.dgvConnectionStrings.ReadOnly = true;
			this.dgvConnectionStrings.RightToLeft = System.Windows.Forms.RightToLeft.No;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvConnectionStrings.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvConnectionStrings.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvConnectionStrings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvConnectionStrings.Size = new System.Drawing.Size(544, 373);
			this.dgvConnectionStrings.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvConnectionStrings.TabIndex = 0;
			this.dgvConnectionStrings.SelectionChanged += new System.EventHandler(this.dgvDataSource_SelectionChanged);
			this.dgvConnectionStrings.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvDataSource_MouseDoubleClick);
			// 
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Name";
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.MinimumWidth = 24;
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// DataSourceColumn
			// 
			this.DataSourceColumn.DataPropertyName = "DataSource";
			this.DataSourceColumn.HeaderText = "Data Source";
			this.DataSourceColumn.MinimumWidth = 24;
			this.DataSourceColumn.Name = "DataSourceColumn";
			this.DataSourceColumn.ReadOnly = true;
			this.DataSourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// IntegratedSecurityColumn
			// 
			this.IntegratedSecurityColumn.DataPropertyName = "IntegratedSecurity";
			this.IntegratedSecurityColumn.HeaderText = "Integrated Security";
			this.IntegratedSecurityColumn.MinimumWidth = 24;
			this.IntegratedSecurityColumn.Name = "IntegratedSecurityColumn";
			this.IntegratedSecurityColumn.ReadOnly = true;
			this.IntegratedSecurityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.IntegratedSecurityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// UserNameColumn
			// 
			this.UserNameColumn.DataPropertyName = "UserName";
			this.UserNameColumn.HeaderText = "User Name";
			this.UserNameColumn.MinimumWidth = 24;
			this.UserNameColumn.Name = "UserNameColumn";
			this.UserNameColumn.ReadOnly = true;
			this.UserNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.UserNameColumn.Width = 75;
			// 
			// InitialCatalogColumn
			// 
			this.InitialCatalogColumn.DataPropertyName = "InitialCatalog";
			this.InitialCatalogColumn.HeaderText = "Initial Catalog";
			this.InitialCatalogColumn.MinimumWidth = 24;
			this.InitialCatalogColumn.Name = "InitialCatalogColumn";
			this.InitialCatalogColumn.ReadOnly = true;
			this.InitialCatalogColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// ucConnectionStrings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlConnectionStrings);
			this.Controls.Add(this.tsMenu);
			this.Name = "ucConnectionStrings";
			this.Size = new System.Drawing.Size(544, 404);
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlConnectionStrings.ResumeLayout(false);
			this.pnlConnectionStrings.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvConnectionStrings)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.Panel pnlConnectionStrings;
		private System.Windows.Forms.ToolStripButton tsbAddConnectionString;
		private System.Windows.Forms.ToolStripButton tsbEditConnectionString;
		private System.Windows.Forms.ToolStripButton tsbDeleteConnectionString;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private NAdvancedDataGridView dgvConnectionStrings;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DataSourceColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IntegratedSecurityColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn UserNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn InitialCatalogColumn;
	}
}
