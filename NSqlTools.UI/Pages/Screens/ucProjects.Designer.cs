using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.Pages
{
    partial class ucProjects
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
			this.tsbAddProject = new System.Windows.Forms.ToolStripButton();
			this.tsbEditProject = new System.Windows.Forms.ToolStripButton();
			this.tsbDeleteProject = new System.Windows.Forms.ToolStripButton();
			this.tsbProjectOpen = new System.Windows.Forms.ToolStripButton();
			this.pnlFavoriteQueries = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.dgvProjects = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CreatedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UpdateDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tsMenu.SuspendLayout();
			this.pnlFavoriteQueries.SuspendLayout();
			this.panel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).BeginInit();
			this.SuspendLayout();
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddProject,
            this.tsbEditProject,
            this.tsbDeleteProject,
            this.tsbProjectOpen});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(611, 31);
			this.tsMenu.TabIndex = 20;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbAddProject
			// 
			this.tsbAddProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAddProject.Image = global::NSqlTools.UI.Properties.Resources.Add;
			this.tsbAddProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAddProject.Name = "tsbAddProject";
			this.tsbAddProject.Size = new System.Drawing.Size(28, 28);
			this.tsbAddProject.Text = "Add Screen Package";
			this.tsbAddProject.Click += new System.EventHandler(this.tsbAddProject_Click);
			// 
			// tsbEditProject
			// 
			this.tsbEditProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbEditProject.Enabled = false;
			this.tsbEditProject.Image = global::NSqlTools.UI.Properties.Resources.Edit;
			this.tsbEditProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbEditProject.Name = "tsbEditProject";
			this.tsbEditProject.Size = new System.Drawing.Size(28, 28);
			this.tsbEditProject.Text = "Update Screen Package";
			this.tsbEditProject.Click += new System.EventHandler(this.tsbEditProject_Click);
			// 
			// tsbDeleteProject
			// 
			this.tsbDeleteProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDeleteProject.Enabled = false;
			this.tsbDeleteProject.Image = global::NSqlTools.UI.Properties.Resources.Delete;
			this.tsbDeleteProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDeleteProject.Name = "tsbDeleteProject";
			this.tsbDeleteProject.Size = new System.Drawing.Size(28, 28);
			this.tsbDeleteProject.Text = "Delete Screen Package";
			this.tsbDeleteProject.Click += new System.EventHandler(this.tsbDeleteProject_Click);
			// 
			// tsbProjectOpen
			// 
			this.tsbProjectOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbProjectOpen.Enabled = false;
			this.tsbProjectOpen.Image = global::NSqlTools.UI.Properties.Resources.ProjectOpen;
			this.tsbProjectOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbProjectOpen.Name = "tsbProjectOpen";
			this.tsbProjectOpen.Size = new System.Drawing.Size(28, 28);
			this.tsbProjectOpen.Text = "Open Project";
			this.tsbProjectOpen.Click += new System.EventHandler(this.tsbProjectOpen_Click);
			// 
			// pnlFavoriteQueries
			// 
			this.pnlFavoriteQueries.Controls.Add(this.panel2);
			this.pnlFavoriteQueries.Controls.Add(this.dgvProjects);
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
			// dgvProjects
			// 
			this.dgvProjects.AllowUserToAddRows = false;
			this.dgvProjects.AllowUserToOrderColumns = true;
			this.dgvProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.Column1,
            this.DescriptionColumn,
            this.CreatedDateColumn,
            this.UpdateDateColumn});
			this.dgvProjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvProjects.EnableHeadersVisualStyles = false;
			this.dgvProjects.FilterAndSortEnabled = true;
			this.dgvProjects.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvProjects.Location = new System.Drawing.Point(0, 0);
			this.dgvProjects.MaxFilterButtonImageHeight = 23;
			this.dgvProjects.Name = "dgvProjects";
			this.dgvProjects.ReadOnly = true;
			this.dgvProjects.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dgvProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvProjects.Size = new System.Drawing.Size(611, 375);
			this.dgvProjects.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvProjects.TabIndex = 0;
			this.dgvProjects.SelectionChanged += new System.EventHandler(this.dgvProjects_SelectionChanged);
			this.dgvProjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvProjects_MouseDoubleClick);
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
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.MinimumWidth = 24;
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// DescriptionColumn
			// 
			this.DescriptionColumn.DataPropertyName = "Description";
			this.DescriptionColumn.HeaderText = "Description";
			this.DescriptionColumn.MinimumWidth = 24;
			this.DescriptionColumn.Name = "DescriptionColumn";
			this.DescriptionColumn.ReadOnly = true;
			this.DescriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.DescriptionColumn.Width = 200;
			// 
			// CreatedDateColumn
			// 
			this.CreatedDateColumn.DataPropertyName = "CreatedDate";
			this.CreatedDateColumn.HeaderText = "Created Date";
			this.CreatedDateColumn.MinimumWidth = 24;
			this.CreatedDateColumn.Name = "CreatedDateColumn";
			this.CreatedDateColumn.ReadOnly = true;
			this.CreatedDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.CreatedDateColumn.Width = 120;
			// 
			// UpdateDateColumn
			// 
			this.UpdateDateColumn.DataPropertyName = "UpdateDate";
			this.UpdateDateColumn.HeaderText = "Update Date";
			this.UpdateDateColumn.MinimumWidth = 24;
			this.UpdateDateColumn.Name = "UpdateDateColumn";
			this.UpdateDateColumn.ReadOnly = true;
			this.UpdateDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// ucProjects
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlFavoriteQueries);
			this.Controls.Add(this.tsMenu);
			this.Name = "ucProjects";
			this.Size = new System.Drawing.Size(611, 406);
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlFavoriteQueries.ResumeLayout(false);
			this.pnlFavoriteQueries.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.Panel pnlFavoriteQueries;
        private NAdvancedDataGridView dgvProjects;
        private System.Windows.Forms.ToolStripButton tsbAddProject;
        private System.Windows.Forms.ToolStripButton tsbEditProject;
        private System.Windows.Forms.ToolStripButton tsbDeleteProject;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripButton tsbProjectOpen;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDateColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn UpdateDateColumn;
	}
}
