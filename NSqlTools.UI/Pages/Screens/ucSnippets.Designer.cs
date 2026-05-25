using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.Pages
{
    partial class ucSnippet
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
			this.tsbAddSnippet = new System.Windows.Forms.ToolStripButton();
			this.tsbEditSnippet = new System.Windows.Forms.ToolStripButton();
			this.tsbDeleteSnippet = new System.Windows.Forms.ToolStripButton();
			this.pnlFavoriteQueries = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.dgvSnippets = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CreatedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tsMenu.SuspendLayout();
			this.pnlFavoriteQueries.SuspendLayout();
			this.panel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvSnippets)).BeginInit();
			this.SuspendLayout();
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddSnippet,
            this.tsbEditSnippet,
            this.tsbDeleteSnippet});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(611, 31);
			this.tsMenu.TabIndex = 20;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbAddSnippet
			// 
			this.tsbAddSnippet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAddSnippet.Image = global::NSqlTools.UI.Properties.Resources.Add;
			this.tsbAddSnippet.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAddSnippet.Name = "tsbAddSnippet";
			this.tsbAddSnippet.Size = new System.Drawing.Size(28, 28);
			this.tsbAddSnippet.Text = "Add Snippet";
			this.tsbAddSnippet.Click += new System.EventHandler(this.tsbAddSnippet_Click);
			// 
			// tsbEditSnippet
			// 
			this.tsbEditSnippet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbEditSnippet.Enabled = false;
			this.tsbEditSnippet.Image = global::NSqlTools.UI.Properties.Resources.Edit;
			this.tsbEditSnippet.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbEditSnippet.Name = "tsbEditSnippet";
			this.tsbEditSnippet.Size = new System.Drawing.Size(28, 28);
			this.tsbEditSnippet.Text = "Update Snippet";
			this.tsbEditSnippet.Click += new System.EventHandler(this.tsbEditSnippet_Click);
			// 
			// tsbDeleteSnippet
			// 
			this.tsbDeleteSnippet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDeleteSnippet.Enabled = false;
			this.tsbDeleteSnippet.Image = global::NSqlTools.UI.Properties.Resources.Delete;
			this.tsbDeleteSnippet.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDeleteSnippet.Name = "tsbDeleteSnippet";
			this.tsbDeleteSnippet.Size = new System.Drawing.Size(28, 28);
			this.tsbDeleteSnippet.Text = "Delete Snippet";
			this.tsbDeleteSnippet.Click += new System.EventHandler(this.tsbDeleteSnippet_Click);
			// 
			// pnlFavoriteQueries
			// 
			this.pnlFavoriteQueries.Controls.Add(this.panel2);
			this.pnlFavoriteQueries.Controls.Add(this.dgvSnippets);
			this.pnlFavoriteQueries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFavoriteQueries.Location = new System.Drawing.Point(0, 31);
			this.pnlFavoriteQueries.Name = "pnlFavoriteQueries";
			this.pnlFavoriteQueries.Size = new System.Drawing.Size(611, 375);
			this.pnlFavoriteQueries.TabIndex = 21;
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.statusStrip1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 353);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(611, 22);
			this.panel2.TabIndex = 2;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(611, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 17);
			this.lblStatus.Text = " ";
			// 
			// dgvSnippets
			// 
			this.dgvSnippets.AllowUserToAddRows = false;
			this.dgvSnippets.AllowUserToOrderColumns = true;
			this.dgvSnippets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSnippets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CreatedDateColumn,
            this.DescriptionColumn});
			this.dgvSnippets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvSnippets.EnableHeadersVisualStyles = false;
			this.dgvSnippets.FilterAndSortEnabled = true;
			this.dgvSnippets.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvSnippets.Location = new System.Drawing.Point(0, 0);
			this.dgvSnippets.MaxFilterButtonImageHeight = 23;
			this.dgvSnippets.Name = "dgvSnippets";
			this.dgvSnippets.ReadOnly = true;
			this.dgvSnippets.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dgvSnippets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvSnippets.Size = new System.Drawing.Size(611, 375);
			this.dgvSnippets.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvSnippets.TabIndex = 0;
			this.dgvSnippets.SelectionChanged += new System.EventHandler(this.dgvSnippet_SelectionChanged);
			this.dgvSnippets.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSnippet_MouseDoubleClick);
			// 
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Shortcut";
			this.NameColumn.HeaderText = "Shortcut";
			this.NameColumn.MinimumWidth = 24;
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// CreatedDateColumn
			// 
			this.CreatedDateColumn.DataPropertyName = "Description";
			this.CreatedDateColumn.HeaderText = "Description";
			this.CreatedDateColumn.MinimumWidth = 24;
			this.CreatedDateColumn.Name = "CreatedDateColumn";
			this.CreatedDateColumn.ReadOnly = true;
			this.CreatedDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.CreatedDateColumn.Width = 200;
			// 
			// DescriptionColumn
			// 
			this.DescriptionColumn.DataPropertyName = "Expansion";
			this.DescriptionColumn.HeaderText = "Sql Script";
			this.DescriptionColumn.MinimumWidth = 24;
			this.DescriptionColumn.Name = "DescriptionColumn";
			this.DescriptionColumn.ReadOnly = true;
			this.DescriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.DescriptionColumn.Width = 300;
			// 
			// ucSnippet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlFavoriteQueries);
			this.Controls.Add(this.tsMenu);
			this.Name = "ucSnippet";
			this.Size = new System.Drawing.Size(611, 406);
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlFavoriteQueries.ResumeLayout(false);
			this.pnlFavoriteQueries.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvSnippets)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.Panel pnlFavoriteQueries;
        private NAdvancedDataGridView dgvSnippets;
        private System.Windows.Forms.ToolStripButton tsbAddSnippet;
        private System.Windows.Forms.ToolStripButton tsbEditSnippet;
        private System.Windows.Forms.ToolStripButton tsbDeleteSnippet;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDateColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
	}
}
