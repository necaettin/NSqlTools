using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.Pages
{
    partial class ucFavoriteQueries
    {
        private System.ComponentModel.IContainer components = null;

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
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbAddFavoriteQuery = new System.Windows.Forms.ToolStripButton();
			this.tsbEditFavoriteQuery = new System.Windows.Forms.ToolStripButton();
			this.tsbDeleteFavoriteQuery = new System.Windows.Forms.ToolStripButton();
			this.pnlFavoriteQueries = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.dgvFavoriteQueries = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CreatedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tsMenu.SuspendLayout();
			this.pnlFavoriteQueries.SuspendLayout();
			this.panel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvFavoriteQueries)).BeginInit();
			this.SuspendLayout();
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddFavoriteQuery,
            this.tsbEditFavoriteQuery,
            this.tsbDeleteFavoriteQuery});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(544, 31);
			this.tsMenu.TabIndex = 20;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbAddFavoriteQuery
			// 
			this.tsbAddFavoriteQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAddFavoriteQuery.Image = global::NSqlTools.UI.Properties.Resources.Add;
			this.tsbAddFavoriteQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAddFavoriteQuery.Name = "tsbAddFavoriteQuery";
			this.tsbAddFavoriteQuery.Size = new System.Drawing.Size(28, 28);
			this.tsbAddFavoriteQuery.Text = "Add Favorite Query";
			this.tsbAddFavoriteQuery.Click += new System.EventHandler(this.tsbAddFavoriteQuery_Click);
			// 
			// tsbEditFavoriteQuery
			// 
			this.tsbEditFavoriteQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbEditFavoriteQuery.Enabled = false;
			this.tsbEditFavoriteQuery.Image = global::NSqlTools.UI.Properties.Resources.Edit;
			this.tsbEditFavoriteQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbEditFavoriteQuery.Name = "tsbEditFavoriteQuery";
			this.tsbEditFavoriteQuery.Size = new System.Drawing.Size(28, 28);
			this.tsbEditFavoriteQuery.Text = "Edit Favorite Query";
			this.tsbEditFavoriteQuery.Click += new System.EventHandler(this.tsbEditFavoriteQuery_Click);
			// 
			// tsbDeleteFavoriteQuery
			// 
			this.tsbDeleteFavoriteQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDeleteFavoriteQuery.Enabled = false;
			this.tsbDeleteFavoriteQuery.Image = global::NSqlTools.UI.Properties.Resources.Delete;
			this.tsbDeleteFavoriteQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDeleteFavoriteQuery.Name = "tsbDeleteFavoriteQuery";
			this.tsbDeleteFavoriteQuery.Size = new System.Drawing.Size(28, 28);
			this.tsbDeleteFavoriteQuery.Text = "Delete Favorite Query";
			this.tsbDeleteFavoriteQuery.Click += new System.EventHandler(this.tsbDeleteFavoriteQuery_Click);
			// 
			// pnlFavoriteQueries
			// 
			this.pnlFavoriteQueries.Controls.Add(this.panel2);
			this.pnlFavoriteQueries.Controls.Add(this.dgvFavoriteQueries);
			this.pnlFavoriteQueries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFavoriteQueries.Location = new System.Drawing.Point(0, 31);
			this.pnlFavoriteQueries.Name = "pnlFavoriteQueries";
			this.pnlFavoriteQueries.Size = new System.Drawing.Size(544, 373);
			this.pnlFavoriteQueries.TabIndex = 21;
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
			// dgvFavoriteQueries
			// 
			this.dgvFavoriteQueries.AllowUserToAddRows = false;
			this.dgvFavoriteQueries.AllowUserToOrderColumns = true;
			this.dgvFavoriteQueries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvFavoriteQueries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.DescriptionColumn,
            this.CreatedDateColumn});
			this.dgvFavoriteQueries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvFavoriteQueries.EnableHeadersVisualStyles = false;
			this.dgvFavoriteQueries.FilterAndSortEnabled = true;
			this.dgvFavoriteQueries.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvFavoriteQueries.Location = new System.Drawing.Point(0, 0);
			this.dgvFavoriteQueries.MaxFilterButtonImageHeight = 23;
			this.dgvFavoriteQueries.Name = "dgvFavoriteQueries";
			this.dgvFavoriteQueries.ReadOnly = true;
			this.dgvFavoriteQueries.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dgvFavoriteQueries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvFavoriteQueries.Size = new System.Drawing.Size(544, 373);
			this.dgvFavoriteQueries.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvFavoriteQueries.TabIndex = 0;
			this.dgvFavoriteQueries.SelectionChanged += new System.EventHandler(this.dgvFavoriteQueries_SelectionChanged);
			this.dgvFavoriteQueries.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvFavoriteQueries_MouseDoubleClick);
			// 
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Name";
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.MinimumWidth = 24;
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// DescriptionColumn
			// 
			this.DescriptionColumn.DataPropertyName = "Description";
			this.DescriptionColumn.HeaderText = "Description";
			this.DescriptionColumn.MinimumWidth = 24;
			this.DescriptionColumn.Name = "DescriptionColumn";
			this.DescriptionColumn.ReadOnly = true;
			this.DescriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.DescriptionColumn.Width = 200;
			// 
			// CreatedDateColumn
			// 
			this.CreatedDateColumn.DataPropertyName = "CreatedDate";
			this.CreatedDateColumn.HeaderText = "Created Date";
			this.CreatedDateColumn.MinimumWidth = 24;
			this.CreatedDateColumn.Name = "CreatedDateColumn";
			this.CreatedDateColumn.ReadOnly = true;
			this.CreatedDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.CreatedDateColumn.Width = 120;
			// 
			// ucFavoriteQueries
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlFavoriteQueries);
			this.Controls.Add(this.tsMenu);
			this.Name = "ucFavoriteQueries";
			this.Size = new System.Drawing.Size(544, 404);
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlFavoriteQueries.ResumeLayout(false);
			this.pnlFavoriteQueries.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvFavoriteQueries)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.Panel pnlFavoriteQueries;
        private NAdvancedDataGridView dgvFavoriteQueries;
        private System.Windows.Forms.ToolStripButton tsbAddFavoriteQuery;
        private System.Windows.Forms.ToolStripButton tsbEditFavoriteQuery;
        private System.Windows.Forms.ToolStripButton tsbDeleteFavoriteQuery;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDateColumn;
	}
}
