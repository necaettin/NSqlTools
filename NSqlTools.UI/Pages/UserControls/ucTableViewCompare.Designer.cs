using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.UserControls
{
	partial class ucTableViewCompare
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
			this.dgvColumns = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbEqual = new System.Windows.Forms.ToolStripButton();
			this.tsbNotEqual = new System.Windows.Forms.ToolStripButton();
			this.tsbSourceExists = new System.Windows.Forms.ToolStripButton();
			this.tsbTargetExists = new System.Windows.Forms.ToolStripButton();
			this.pnlColumns = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ColumnIdSourceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NameSourceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TypeNameCustomSourceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IsNullableSourceColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.IsIdentitySourceColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Diff = new System.Windows.Forms.DataGridViewImageColumn();
			this.ColumnIdTargetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NameTargetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TypeNameCustomTargetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IsNullableTargetColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.IsIdentityTargetColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
			this.tsMenu.SuspendLayout();
			this.pnlColumns.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvColumns
			// 
			this.dgvColumns.AllowUserToAddRows = false;
			this.dgvColumns.AllowUserToDeleteRows = false;
			this.dgvColumns.AllowUserToOrderColumns = true;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvColumns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIdSourceColumn,
            this.NameSourceColumn,
            this.TypeNameCustomSourceColumn,
            this.IsNullableSourceColumn,
            this.IsIdentitySourceColumn,
            this.Diff,
            this.ColumnIdTargetColumn,
            this.NameTargetColumn,
            this.TypeNameCustomTargetColumn,
            this.IsNullableTargetColumn,
            this.IsIdentityTargetColumn});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvColumns.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvColumns.EnableHeadersVisualStyles = false;
			this.dgvColumns.FilterAndSortEnabled = true;
			this.dgvColumns.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvColumns.Location = new System.Drawing.Point(0, 0);
			this.dgvColumns.MaxFilterButtonImageHeight = 23;
			this.dgvColumns.Name = "dgvColumns";
			this.dgvColumns.ReadOnly = true;
			this.dgvColumns.RightToLeft = System.Windows.Forms.RightToLeft.No;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvColumns.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvColumns.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvColumns.RowTemplate.Height = 26;
			this.dgvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvColumns.Size = new System.Drawing.Size(850, 466);
			this.dgvColumns.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvColumns.TabIndex = 1;
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEqual,
            this.tsbNotEqual,
            this.tsbSourceExists,
            this.tsbTargetExists});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(850, 31);
			this.tsMenu.TabIndex = 14;
			this.tsMenu.Text = "toolStrip1";
			// 
			// tsbEqual
			// 
			this.tsbEqual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbEqual.Image = global::NSqlTools.UI.Properties.Resources.Equality_Equal;
			this.tsbEqual.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbEqual.Name = "tsbEqual";
			this.tsbEqual.Size = new System.Drawing.Size(28, 28);
			this.tsbEqual.Text = "Equal";
			this.tsbEqual.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// tsbNotEqual
			// 
			this.tsbNotEqual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNotEqual.Image = global::NSqlTools.UI.Properties.Resources.Equality_NotEqual;
			this.tsbNotEqual.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNotEqual.Name = "tsbNotEqual";
			this.tsbNotEqual.Size = new System.Drawing.Size(28, 28);
			this.tsbNotEqual.Text = "Not Equal";
			this.tsbNotEqual.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// tsbSourceExists
			// 
			this.tsbSourceExists.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSourceExists.Image = global::NSqlTools.UI.Properties.Resources.Equality_SourceExists;
			this.tsbSourceExists.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSourceExists.Name = "tsbSourceExists";
			this.tsbSourceExists.Size = new System.Drawing.Size(28, 28);
			this.tsbSourceExists.Text = "Exists In Source";
			this.tsbSourceExists.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// tsbTargetExists
			// 
			this.tsbTargetExists.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbTargetExists.Image = global::NSqlTools.UI.Properties.Resources.Equality_TargetExists;
			this.tsbTargetExists.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbTargetExists.Name = "tsbTargetExists";
			this.tsbTargetExists.Size = new System.Drawing.Size(28, 28);
			this.tsbTargetExists.Text = "Exists In Target";
			this.tsbTargetExists.Click += new System.EventHandler(this.filterColumnsGrid);
			// 
			// pnlColumns
			// 
			this.pnlColumns.Controls.Add(this.panel4);
			this.pnlColumns.Controls.Add(this.panel2);
			this.pnlColumns.Controls.Add(this.panel1);
			this.pnlColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlColumns.Location = new System.Drawing.Point(0, 31);
			this.pnlColumns.Name = "pnlColumns";
			this.pnlColumns.Size = new System.Drawing.Size(850, 488);
			this.pnlColumns.TabIndex = 15;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.dgvColumns);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(850, 466);
			this.panel4.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.statusStrip1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 466);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(850, 22);
			this.panel2.TabIndex = 16;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(850, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 17);
			this.lblStatus.Text = " ";
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 488);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(850, 0);
			this.panel1.TabIndex = 3;
			// 
			// ColumnIdSourceColumn
			// 
			this.ColumnIdSourceColumn.DataPropertyName = "ColumnIdSource";
			this.ColumnIdSourceColumn.HeaderText = "ID Source";
			this.ColumnIdSourceColumn.MinimumWidth = 24;
			this.ColumnIdSourceColumn.Name = "ColumnIdSourceColumn";
			this.ColumnIdSourceColumn.ReadOnly = true;
			this.ColumnIdSourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.ColumnIdSourceColumn.Visible = false;
			this.ColumnIdSourceColumn.Width = 76;
			// 
			// NameSourceColumn
			// 
			this.NameSourceColumn.DataPropertyName = "NameSource";
			this.NameSourceColumn.HeaderText = "Name Source";
			this.NameSourceColumn.MinimumWidth = 24;
			this.NameSourceColumn.Name = "NameSourceColumn";
			this.NameSourceColumn.ReadOnly = true;
			this.NameSourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.NameSourceColumn.Width = 150;
			// 
			// TypeNameCustomSourceColumn
			// 
			this.TypeNameCustomSourceColumn.DataPropertyName = "TypeNameCustomSource";
			this.TypeNameCustomSourceColumn.HeaderText = "Type Source";
			this.TypeNameCustomSourceColumn.MinimumWidth = 24;
			this.TypeNameCustomSourceColumn.Name = "TypeNameCustomSourceColumn";
			this.TypeNameCustomSourceColumn.ReadOnly = true;
			this.TypeNameCustomSourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// IsNullableSourceColumn
			// 
			this.IsNullableSourceColumn.DataPropertyName = "IsNullableSource";
			this.IsNullableSourceColumn.HeaderText = "Nullable Source";
			this.IsNullableSourceColumn.MinimumWidth = 24;
			this.IsNullableSourceColumn.Name = "IsNullableSourceColumn";
			this.IsNullableSourceColumn.ReadOnly = true;
			this.IsNullableSourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.IsNullableSourceColumn.Width = 50;
			// 
			// IsIdentitySourceColumn
			// 
			this.IsIdentitySourceColumn.DataPropertyName = "IsIdentitySource";
			this.IsIdentitySourceColumn.HeaderText = "Identity Source";
			this.IsIdentitySourceColumn.MinimumWidth = 24;
			this.IsIdentitySourceColumn.Name = "IsIdentitySourceColumn";
			this.IsIdentitySourceColumn.ReadOnly = true;
			this.IsIdentitySourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.IsIdentitySourceColumn.Width = 50;
			// 
			// Diff
			// 
			this.Diff.DataPropertyName = "Difference";
			this.Diff.HeaderText = "Difference";
			this.Diff.MinimumWidth = 24;
			this.Diff.Name = "Diff";
			this.Diff.ReadOnly = true;
			this.Diff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColumnIdTargetColumn
			// 
			this.ColumnIdTargetColumn.DataPropertyName = "ColumnIdTarget";
			this.ColumnIdTargetColumn.HeaderText = "ID Target";
			this.ColumnIdTargetColumn.MinimumWidth = 24;
			this.ColumnIdTargetColumn.Name = "ColumnIdTargetColumn";
			this.ColumnIdTargetColumn.ReadOnly = true;
			this.ColumnIdTargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.ColumnIdTargetColumn.Visible = false;
			// 
			// NameTargetColumn
			// 
			this.NameTargetColumn.DataPropertyName = "NameTarget";
			this.NameTargetColumn.HeaderText = "Name Target";
			this.NameTargetColumn.MinimumWidth = 24;
			this.NameTargetColumn.Name = "NameTargetColumn";
			this.NameTargetColumn.ReadOnly = true;
			this.NameTargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.NameTargetColumn.Width = 150;
			// 
			// TypeNameCustomTargetColumn
			// 
			this.TypeNameCustomTargetColumn.DataPropertyName = "TypeNameCustomTarget";
			this.TypeNameCustomTargetColumn.HeaderText = "Type Target";
			this.TypeNameCustomTargetColumn.MinimumWidth = 24;
			this.TypeNameCustomTargetColumn.Name = "TypeNameCustomTargetColumn";
			this.TypeNameCustomTargetColumn.ReadOnly = true;
			this.TypeNameCustomTargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// IsNullableTargetColumn
			// 
			this.IsNullableTargetColumn.DataPropertyName = "IsNullableTarget";
			this.IsNullableTargetColumn.HeaderText = "Nullable Target";
			this.IsNullableTargetColumn.MinimumWidth = 24;
			this.IsNullableTargetColumn.Name = "IsNullableTargetColumn";
			this.IsNullableTargetColumn.ReadOnly = true;
			this.IsNullableTargetColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.IsNullableTargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.IsNullableTargetColumn.Width = 50;
			// 
			// IsIdentityTargetColumn
			// 
			this.IsIdentityTargetColumn.DataPropertyName = "IsIdentityTarget";
			this.IsIdentityTargetColumn.HeaderText = "Identity Target";
			this.IsIdentityTargetColumn.MinimumWidth = 24;
			this.IsIdentityTargetColumn.Name = "IsIdentityTargetColumn";
			this.IsIdentityTargetColumn.ReadOnly = true;
			this.IsIdentityTargetColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.IsIdentityTargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.IsIdentityTargetColumn.Width = 50;
			// 
			// ucTableViewCompare
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlColumns);
			this.Controls.Add(this.tsMenu);
			this.Name = "ucTableViewCompare";
			this.Size = new System.Drawing.Size(850, 519);
			((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlColumns.ResumeLayout(false);
			this.pnlColumns.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private NAdvancedDataGridView dgvColumns;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbEqual;
		private System.Windows.Forms.ToolStripButton tsbNotEqual;
		private System.Windows.Forms.ToolStripButton tsbSourceExists;
		private System.Windows.Forms.ToolStripButton tsbTargetExists;
		private System.Windows.Forms.Panel pnlColumns;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIdSourceColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameSourceColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn TypeNameCustomSourceColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsNullableSourceColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsIdentitySourceColumn;
		private System.Windows.Forms.DataGridViewImageColumn Diff;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIdTargetColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameTargetColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn TypeNameCustomTargetColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsNullableTargetColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsIdentityTargetColumn;
	}
}
