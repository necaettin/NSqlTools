using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.Popups
{
	partial class frmTableInfo
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.scTableInfo = new System.Windows.Forms.SplitContainer();
			this.gbTableIndexes = new System.Windows.Forms.GroupBox();
			this.dgvTableIndex = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.gbTableRelationships = new System.Windows.Forms.GroupBox();
			this.dgvTableRelationship = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.FKNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ReferencedTableColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnNamesColumnRelationship = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RelationshipNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnNamesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IndexNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IsUniqueColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.IsPrimaryKeyColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.IndexTypeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.scTableInfo)).BeginInit();
			this.scTableInfo.Panel1.SuspendLayout();
			this.scTableInfo.Panel2.SuspendLayout();
			this.scTableInfo.SuspendLayout();
			this.gbTableIndexes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTableIndex)).BeginInit();
			this.gbTableRelationships.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTableRelationship)).BeginInit();
			this.SuspendLayout();
			// 
			// scTableInfo
			// 
			this.scTableInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scTableInfo.Location = new System.Drawing.Point(0, 0);
			this.scTableInfo.Name = "scTableInfo";
			this.scTableInfo.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scTableInfo.Panel1
			// 
			this.scTableInfo.Panel1.Controls.Add(this.gbTableIndexes);
			// 
			// scTableInfo.Panel2
			// 
			this.scTableInfo.Panel2.Controls.Add(this.gbTableRelationships);
			this.scTableInfo.Size = new System.Drawing.Size(1059, 450);
			this.scTableInfo.SplitterDistance = 266;
			this.scTableInfo.TabIndex = 0;
			// 
			// gbTableIndexes
			// 
			this.gbTableIndexes.Controls.Add(this.dgvTableIndex);
			this.gbTableIndexes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbTableIndexes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbTableIndexes.Location = new System.Drawing.Point(0, 0);
			this.gbTableIndexes.Name = "gbTableIndexes";
			this.gbTableIndexes.Size = new System.Drawing.Size(1059, 266);
			this.gbTableIndexes.TabIndex = 1;
			this.gbTableIndexes.TabStop = false;
			this.gbTableIndexes.Text = "Table Indexes";
			// 
			// dgvTableIndex
			// 
			this.dgvTableIndex.AllowUserToAddRows = false;
			this.dgvTableIndex.AllowUserToDeleteRows = false;
			this.dgvTableIndex.AllowUserToOrderColumns = true;
			this.dgvTableIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTableIndex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexNameColumn,
            this.IsUniqueColumn,
            this.IsPrimaryKeyColumn,
            this.IndexTypeNameColumn});
			this.dgvTableIndex.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvTableIndex.EnableHeadersVisualStyles = false;
			this.dgvTableIndex.FilterAndSortEnabled = true;
			this.dgvTableIndex.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTableIndex.Location = new System.Drawing.Point(3, 16);
			this.dgvTableIndex.MaxFilterButtonImageHeight = 23;
			this.dgvTableIndex.Name = "dgvTableIndex";
			this.dgvTableIndex.ReadOnly = true;
			this.dgvTableIndex.RightToLeft = System.Windows.Forms.RightToLeft.No;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvTableIndex.RowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvTableIndex.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvTableIndex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvTableIndex.Size = new System.Drawing.Size(1053, 247);
			this.dgvTableIndex.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTableIndex.TabIndex = 0;
			// 
			// gbTableRelationships
			// 
			this.gbTableRelationships.Controls.Add(this.dgvTableRelationship);
			this.gbTableRelationships.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbTableRelationships.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbTableRelationships.Location = new System.Drawing.Point(0, 0);
			this.gbTableRelationships.Name = "gbTableRelationships";
			this.gbTableRelationships.Size = new System.Drawing.Size(1059, 180);
			this.gbTableRelationships.TabIndex = 1;
			this.gbTableRelationships.TabStop = false;
			this.gbTableRelationships.Text = "Table Relationships";
			// 
			// dgvTableRelationship
			// 
			this.dgvTableRelationship.AllowUserToAddRows = false;
			this.dgvTableRelationship.AllowUserToDeleteRows = false;
			this.dgvTableRelationship.AllowUserToOrderColumns = true;
			this.dgvTableRelationship.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTableRelationship.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FKNameColumn,
            this.ReferencedTableColumn,
            this.ColumnNamesColumnRelationship,
            this.RelationshipNameColumn});
			this.dgvTableRelationship.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvTableRelationship.EnableHeadersVisualStyles = false;
			this.dgvTableRelationship.FilterAndSortEnabled = true;
			this.dgvTableRelationship.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTableRelationship.Location = new System.Drawing.Point(3, 16);
			this.dgvTableRelationship.MaxFilterButtonImageHeight = 23;
			this.dgvTableRelationship.Name = "dgvTableRelationship";
			this.dgvTableRelationship.ReadOnly = true;
			this.dgvTableRelationship.RightToLeft = System.Windows.Forms.RightToLeft.No;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvTableRelationship.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvTableRelationship.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvTableRelationship.Size = new System.Drawing.Size(1053, 161);
			this.dgvTableRelationship.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTableRelationship.TabIndex = 0;
			// 
			// FKNameColumn
			// 
			this.FKNameColumn.DataPropertyName = "FKName";
			this.FKNameColumn.HeaderText = "FK Name";
			this.FKNameColumn.MinimumWidth = 24;
			this.FKNameColumn.Name = "FKNameColumn";
			this.FKNameColumn.ReadOnly = true;
			this.FKNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.FKNameColumn.Width = 200;
			// 
			// ReferencedTableColumn
			// 
			this.ReferencedTableColumn.DataPropertyName = "ReferencedTable";
			this.ReferencedTableColumn.HeaderText = "Referenced Table";
			this.ReferencedTableColumn.MinimumWidth = 24;
			this.ReferencedTableColumn.Name = "ReferencedTableColumn";
			this.ReferencedTableColumn.ReadOnly = true;
			this.ReferencedTableColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// ColumnNamesColumnRelationship
			// 
			this.ColumnNamesColumnRelationship.DataPropertyName = "ColumnNames";
			this.ColumnNamesColumnRelationship.HeaderText = "Column Names";
			this.ColumnNamesColumnRelationship.MinimumWidth = 24;
			this.ColumnNamesColumnRelationship.Name = "ColumnNamesColumnRelationship";
			this.ColumnNamesColumnRelationship.ReadOnly = true;
			this.ColumnNamesColumnRelationship.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ColumnNamesColumnRelationship.Width = 400;
			// 
			// RelationshipNameColumn
			// 
			this.RelationshipNameColumn.DataPropertyName = "RelationshipName";
			this.RelationshipNameColumn.HeaderText = "Relationship Name";
			this.RelationshipNameColumn.MinimumWidth = 24;
			this.RelationshipNameColumn.Name = "RelationshipNameColumn";
			this.RelationshipNameColumn.ReadOnly = true;
			this.RelationshipNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.RelationshipNameColumn.Width = 200;
			// 
			// ColumnNamesColumn
			// 
			this.ColumnNamesColumn.DataPropertyName = "ColumnNames";
			this.ColumnNamesColumn.HeaderText = "Column Names";
			this.ColumnNamesColumn.Name = "ColumnNamesColumn";
			this.ColumnNamesColumn.ReadOnly = true;
			this.ColumnNamesColumn.Width = 400;
			// 
			// IndexNameColumn
			// 
			this.IndexNameColumn.DataPropertyName = "IndexName";
			this.IndexNameColumn.HeaderText = "Index Name";
			this.IndexNameColumn.MinimumWidth = 24;
			this.IndexNameColumn.Name = "IndexNameColumn";
			this.IndexNameColumn.ReadOnly = true;
			this.IndexNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IndexNameColumn.Width = 201;
			// 
			// IsUniqueColumn
			// 
			this.IsUniqueColumn.DataPropertyName = "IsUnique";
			this.IsUniqueColumn.HeaderText = "Is Unique";
			this.IsUniqueColumn.MinimumWidth = 24;
			this.IsUniqueColumn.Name = "IsUniqueColumn";
			this.IsUniqueColumn.ReadOnly = true;
			this.IsUniqueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.IsUniqueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IsUniqueColumn.Width = 75;
			// 
			// IsPrimaryKeyColumn
			// 
			this.IsPrimaryKeyColumn.DataPropertyName = "IsPrimaryKey";
			this.IsPrimaryKeyColumn.HeaderText = "Is Primary Key";
			this.IsPrimaryKeyColumn.MinimumWidth = 24;
			this.IsPrimaryKeyColumn.Name = "IsPrimaryKeyColumn";
			this.IsPrimaryKeyColumn.ReadOnly = true;
			this.IsPrimaryKeyColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.IsPrimaryKeyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IsPrimaryKeyColumn.Width = 75;
			// 
			// IndexTypeNameColumn
			// 
			this.IndexTypeNameColumn.DataPropertyName = "IndexTypeName";
			this.IndexTypeNameColumn.HeaderText = "Index Type";
			this.IndexTypeNameColumn.MinimumWidth = 24;
			this.IndexTypeNameColumn.Name = "IndexTypeNameColumn";
			this.IndexTypeNameColumn.ReadOnly = true;
			this.IndexTypeNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// frmTableInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1059, 450);
			this.Controls.Add(this.scTableInfo);
			this.KeyPreview = true;
			this.Name = "frmTableInfo";
			this.Text = "Table Info";
			this.Load += new System.EventHandler(this.frmTableInfo_Load);
			this.scTableInfo.Panel1.ResumeLayout(false);
			this.scTableInfo.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scTableInfo)).EndInit();
			this.scTableInfo.ResumeLayout(false);
			this.gbTableIndexes.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvTableIndex)).EndInit();
			this.gbTableRelationships.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvTableRelationship)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scTableInfo;
		private System.Windows.Forms.GroupBox gbTableIndexes;
		private NAdvancedDataGridView dgvTableIndex;
		private System.Windows.Forms.DataGridViewTextBoxColumn FKNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ReferencedTableColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNamesColumnRelationship;
		private System.Windows.Forms.DataGridViewTextBoxColumn RelationshipNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNamesColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn IndexNameColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsUniqueColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsPrimaryKeyColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn IndexTypeNameColumn;
		private System.Windows.Forms.GroupBox gbTableRelationships;
		private NAdvancedDataGridView dgvTableRelationship;
	}
}