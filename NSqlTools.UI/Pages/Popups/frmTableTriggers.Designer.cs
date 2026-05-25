using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.Popups
{
	partial class frmTableTriggers
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
            this.gbTableTriggers = new System.Windows.Forms.GroupBox();
            this.dgvTableTriggers = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
            this._ucSqlNotePad = new NSqlTools.UI.UserControls.ucSqlNotePad();
            this.coldObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scTableInfo)).BeginInit();
            this.scTableInfo.Panel1.SuspendLayout();
            this.scTableInfo.Panel2.SuspendLayout();
            this.scTableInfo.SuspendLayout();
            this.gbTableTriggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableTriggers)).BeginInit();
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
            this.scTableInfo.Panel1.Controls.Add(this.gbTableTriggers);
            // 
            // scTableInfo.Panel2
            // 
            this.scTableInfo.Panel2.Controls.Add(this._ucSqlNotePad);
            this.scTableInfo.Size = new System.Drawing.Size(767, 450);
            this.scTableInfo.SplitterDistance = 266;
            this.scTableInfo.TabIndex = 0;
            // 
            // gbTableTriggers
            // 
            this.gbTableTriggers.Controls.Add(this.dgvTableTriggers);
            this.gbTableTriggers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTableTriggers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTableTriggers.Location = new System.Drawing.Point(0, 0);
            this.gbTableTriggers.Name = "gbTableTriggers";
            this.gbTableTriggers.Size = new System.Drawing.Size(767, 266);
            this.gbTableTriggers.TabIndex = 1;
            this.gbTableTriggers.TabStop = false;
            this.gbTableTriggers.Text = "Table Triggers";
            // 
            // dgvTableTriggers
            // 
            this.dgvTableTriggers.AllowUserToAddRows = false;
            this.dgvTableTriggers.AllowUserToDeleteRows = false;
            this.dgvTableTriggers.AllowUserToOrderColumns = true;
            this.dgvTableTriggers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableTriggers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coldObjectName,
            this.colObjectId});
            this.dgvTableTriggers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableTriggers.EnableHeadersVisualStyles = false;
            this.dgvTableTriggers.FilterAndSortEnabled = true;
            this.dgvTableTriggers.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvTableTriggers.Location = new System.Drawing.Point(3, 16);
            this.dgvTableTriggers.MaxFilterButtonImageHeight = 23;
            this.dgvTableTriggers.Name = "dgvTableTriggers";
            this.dgvTableTriggers.ReadOnly = true;
            this.dgvTableTriggers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTableTriggers.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTableTriggers.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTableTriggers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTableTriggers.Size = new System.Drawing.Size(761, 247);
            this.dgvTableTriggers.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dgvTableTriggers.TabIndex = 0;
            this.dgvTableTriggers.SelectionChanged += new System.EventHandler(this.dgvTableTriggers_SelectionChanged);
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
            // coldObjectName
            // 
            this.coldObjectName.DataPropertyName = "ObjectName";
            this.coldObjectName.HeaderText = "Trigger Name";
            this.coldObjectName.MinimumWidth = 24;
            this.coldObjectName.Name = "coldObjectName";
            this.coldObjectName.ReadOnly = true;
            this.coldObjectName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.coldObjectName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.coldObjectName.Width = 350;
            // 
            // colObjectId
            // 
            this.colObjectId.HeaderText = "ObjectId";
            this.colObjectId.MinimumWidth = 24;
            this.colObjectId.Name = "colObjectId";
            this.colObjectId.ReadOnly = true;
            this.colObjectId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colObjectId.Visible = false;
            // 
            // frmTableTriggers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 450);
            this.Controls.Add(this.scTableInfo);
            this.KeyPreview = true;
            this.Name = "frmTableTriggers";
            this.Text = "Table Triggers";
            this.Load += new System.EventHandler(this.frmTableTriggers_Load);
            this.scTableInfo.Panel1.ResumeLayout(false);
            this.scTableInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTableInfo)).EndInit();
            this.scTableInfo.ResumeLayout(false);
            this.gbTableTriggers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableTriggers)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scTableInfo;
		private System.Windows.Forms.GroupBox gbTableTriggers;
		private NAdvancedDataGridView dgvTableTriggers;
		private UserControls.ucSqlNotePad _ucSqlNotePad;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectId;
    }
}