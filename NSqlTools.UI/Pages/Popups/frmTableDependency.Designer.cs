using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.Popups
{
	partial class frmTableDependency
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
			this.scTableInfo = new System.Windows.Forms.SplitContainer();
			this.gbTableIndexes = new System.Windows.Forms.GroupBox();
			this.dgvTableDependency = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this._ucSqlNotePad = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.ColumnNamesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IndexNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IsUniqueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IsPrimaryKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ObjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.scTableInfo)).BeginInit();
			this.scTableInfo.Panel1.SuspendLayout();
			this.scTableInfo.Panel2.SuspendLayout();
			this.scTableInfo.SuspendLayout();
			this.gbTableIndexes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTableDependency)).BeginInit();
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
			this.scTableInfo.Panel2.Controls.Add(this._ucSqlNotePad);
			this.scTableInfo.Size = new System.Drawing.Size(767, 450);
			this.scTableInfo.SplitterDistance = 266;
			this.scTableInfo.TabIndex = 0;
			// 
			// gbTableIndexes
			// 
			this.gbTableIndexes.Controls.Add(this.dgvTableDependency);
			this.gbTableIndexes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbTableIndexes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbTableIndexes.Location = new System.Drawing.Point(0, 0);
			this.gbTableIndexes.Name = "gbTableIndexes";
			this.gbTableIndexes.Size = new System.Drawing.Size(767, 266);
			this.gbTableIndexes.TabIndex = 1;
			this.gbTableIndexes.TabStop = false;
			this.gbTableIndexes.Text = "Table Dependencies";
			// 
			// dgvTableDependency
			// 
			this.dgvTableDependency.AllowUserToAddRows = false;
			this.dgvTableDependency.AllowUserToDeleteRows = false;
			this.dgvTableDependency.AllowUserToOrderColumns = true;
			this.dgvTableDependency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTableDependency.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexNameColumn,
            this.IsUniqueColumn,
            this.IsPrimaryKeyColumn,
            this.ObjectId});
			this.dgvTableDependency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvTableDependency.EnableHeadersVisualStyles = false;
			this.dgvTableDependency.FilterAndSortEnabled = true;
			this.dgvTableDependency.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTableDependency.Location = new System.Drawing.Point(3, 16);
			this.dgvTableDependency.MaxFilterButtonImageHeight = 23;
			this.dgvTableDependency.Name = "dgvTableDependency";
			this.dgvTableDependency.ReadOnly = true;
			this.dgvTableDependency.RightToLeft = System.Windows.Forms.RightToLeft.No;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvTableDependency.RowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvTableDependency.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvTableDependency.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvTableDependency.Size = new System.Drawing.Size(761, 247);
			this.dgvTableDependency.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvTableDependency.TabIndex = 0;
			this.dgvTableDependency.SelectionChanged += new System.EventHandler(this.dgvTableDependency_SelectionChanged);
			// 
			// _ucSqlNotePad
			// 
			this._ucSqlNotePad.CaseSensitive = false;
			this._ucSqlNotePad.CompareTypeVisible = false;
			this._ucSqlNotePad.DBObjectContract = null;
			this._ucSqlNotePad.DBObjectKeywordList = null;
			this._ucSqlNotePad.DisplayFullScreen = true;
			this._ucSqlNotePad.DisplayStatus = true;
			this._ucSqlNotePad.Dock = System.Windows.Forms.DockStyle.Fill;
			this._ucSqlNotePad.FontSize = 12;
			this._ucSqlNotePad.IsWraped = false;
			this._ucSqlNotePad.Location = new System.Drawing.Point(0, 0);
			this._ucSqlNotePad.MainForm = null;
			this._ucSqlNotePad.Name = "_ucSqlNotePad";
			this._ucSqlNotePad.ParentTabPage = null;
			this._ucSqlNotePad.SchemaKeywordList = null;
			this._ucSqlNotePad.scoSqlNotepadPanel2Collapsed = true;
			this._ucSqlNotePad.SearchKeyword = "";
			this._ucSqlNotePad.Size = new System.Drawing.Size(767, 180);
			this._ucSqlNotePad.TabIndex = 0;
			this._ucSqlNotePad.Title = "Object Content";
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
			this.IndexNameColumn.DataPropertyName = "TypeDescription";
			this.IndexNameColumn.HeaderText = "Type";
			this.IndexNameColumn.MinimumWidth = 24;
			this.IndexNameColumn.Name = "IndexNameColumn";
			this.IndexNameColumn.ReadOnly = true;
			this.IndexNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IndexNameColumn.Width = 151;
			// 
			// IsUniqueColumn
			// 
			this.IsUniqueColumn.DataPropertyName = "SchemaName";
			this.IsUniqueColumn.HeaderText = "Schema Name";
			this.IsUniqueColumn.MinimumWidth = 24;
			this.IsUniqueColumn.Name = "IsUniqueColumn";
			this.IsUniqueColumn.ReadOnly = true;
			this.IsUniqueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.IsUniqueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// IsPrimaryKeyColumn
			// 
			this.IsPrimaryKeyColumn.DataPropertyName = "ObjectName";
			this.IsPrimaryKeyColumn.HeaderText = "Object Name";
			this.IsPrimaryKeyColumn.MinimumWidth = 24;
			this.IsPrimaryKeyColumn.Name = "IsPrimaryKeyColumn";
			this.IsPrimaryKeyColumn.ReadOnly = true;
			this.IsPrimaryKeyColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.IsPrimaryKeyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.IsPrimaryKeyColumn.Width = 250;
			// 
			// ObjectId
			// 
			this.ObjectId.HeaderText = "ObjectId";
			this.ObjectId.MinimumWidth = 24;
			this.ObjectId.Name = "ObjectId";
			this.ObjectId.ReadOnly = true;
			this.ObjectId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ObjectId.Visible = false;
			// 
			// frmTableDependency
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 450);
			this.Controls.Add(this.scTableInfo);
			this.KeyPreview = true;
			this.Name = "frmTableDependency";
			this.Text = "Table Dependency";
			this.Load += new System.EventHandler(this.frmTableDependency_Load);
			this.scTableInfo.Panel1.ResumeLayout(false);
			this.scTableInfo.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scTableInfo)).EndInit();
			this.scTableInfo.ResumeLayout(false);
			this.gbTableIndexes.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvTableDependency)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scTableInfo;
		private System.Windows.Forms.GroupBox gbTableIndexes;
		private NAdvancedDataGridView dgvTableDependency;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNamesColumn;
		private UserControls.ucSqlNotePad _ucSqlNotePad;
		private System.Windows.Forms.DataGridViewTextBoxColumn IndexNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn IsUniqueColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn IsPrimaryKeyColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ObjectId;
	}
}