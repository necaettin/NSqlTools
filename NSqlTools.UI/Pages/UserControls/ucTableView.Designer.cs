using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.UserControls
{
	partial class ucTableView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvColumns = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
            this.ColumnIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeNameCustomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsNullableColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsIdentityColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gbTableView = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbTableInfo = new System.Windows.Forms.ToolStripButton();
            this.tsbTableDependencies = new System.Windows.Forms.ToolStripButton();
            this.tsbTableTriggers = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.gbTableView.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToDeleteRows = false;
            this.dgvColumns.AllowUserToOrderColumns = true;
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIdColumn,
            this.NameColumn,
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
            this.dgvColumns.Size = new System.Drawing.Size(627, 335);
            this.dgvColumns.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvColumns.TabIndex = 0;
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
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 24;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.NameColumn.Width = 200;
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
            // gbTableView
            // 
            this.gbTableView.Controls.Add(this.panel2);
            this.gbTableView.Controls.Add(this.panel1);
            this.gbTableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTableView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTableView.Location = new System.Drawing.Point(0, 0);
            this.gbTableView.Name = "gbTableView";
            this.gbTableView.Size = new System.Drawing.Size(633, 407);
            this.gbTableView.TabIndex = 1;
            this.gbTableView.TabStop = false;
            this.gbTableView.Text = "Table Columns";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pnlToolstrip);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(627, 366);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvColumns);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(627, 335);
            this.panel3.TabIndex = 2;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.toolStrip1);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(627, 31);
            this.pnlToolstrip.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTableInfo,
            this.tsbTableDependencies,
            this.tsbTableTriggers});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(627, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbTableInfo
            // 
            this.tsbTableInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTableInfo.Image = global::NSqlTools.UI.Properties.Resources.Info;
            this.tsbTableInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTableInfo.Name = "tsbTableInfo";
            this.tsbTableInfo.Size = new System.Drawing.Size(28, 28);
            this.tsbTableInfo.Text = "Table Info";
            this.tsbTableInfo.Click += new System.EventHandler(this.tsbTableInfo_Click);
            // 
            // tsbTableDependencies
            // 
            this.tsbTableDependencies.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTableDependencies.Image = global::NSqlTools.UI.Properties.Resources.FreeCompare;
            this.tsbTableDependencies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTableDependencies.Name = "tsbTableDependencies";
            this.tsbTableDependencies.Size = new System.Drawing.Size(28, 28);
            this.tsbTableDependencies.Text = "Table Dependencies";
            this.tsbTableDependencies.Click += new System.EventHandler(this.tsbTableDependencies_Click);
            // 
            // tsbTableTriggers
            // 
            this.tsbTableTriggers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTableTriggers.Image = global::NSqlTools.UI.Properties.Resources.Trigger;
            this.tsbTableTriggers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTableTriggers.Name = "tsbTableTriggers";
            this.tsbTableTriggers.Size = new System.Drawing.Size(28, 28);
            this.tsbTableTriggers.Text = "Table Triggers";
            this.tsbTableTriggers.Click += new System.EventHandler(this.tsbTableTriggers_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 382);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 22);
            this.panel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(627, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(10, 17);
            this.lblStatus.Text = " ";
            // 
            // ucTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTableView);
            this.Name = "ucTableView";
            this.Size = new System.Drawing.Size(633, 407);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.gbTableView.ResumeLayout(false);
            this.gbTableView.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private NAdvancedDataGridView dgvColumns;
		private System.Windows.Forms.GroupBox gbTableView;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel pnlToolstrip;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbTableInfo;
		private System.Windows.Forms.ToolStripButton tsbTableDependencies;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIdColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn TypeNameCustomColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsNullableColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsIdentityColumn;
        private System.Windows.Forms.ToolStripButton tsbTableTriggers;
    }
}
