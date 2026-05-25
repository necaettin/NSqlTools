namespace NSqlTools.UI.UserControls
{
	partial class ucDBObjectSelect
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDBObjectSelect));
			this.pnlSearch = new System.Windows.Forms.Panel();
			this.gbDBObject = new System.Windows.Forms.GroupBox();
			this.pnlDBObject = new System.Windows.Forms.Panel();
			this.txtDBObjectFilter = new System.Windows.Forms.TextBox();
			this.lbDBObject = new System.Windows.Forms.ListBox();
			this.lblDBObject = new System.Windows.Forms.Label();
			this.pnlSchema = new System.Windows.Forms.Panel();
			this.lblSchema = new System.Windows.Forms.Label();
			this.cbSchema = new System.Windows.Forms.ComboBox();
			this.pnlObjectType = new System.Windows.Forms.Panel();
			this.lblObjectType = new System.Windows.Forms.Label();
			this.pnlDB = new System.Windows.Forms.Panel();
			this.txtDBFilter = new System.Windows.Forms.TextBox();
			this.lblCheckedDBList = new System.Windows.Forms.Label();
			this.clbDB = new System.Windows.Forms.CheckedListBox();
			this.lblDB = new System.Windows.Forms.Label();
			this.pnlDataSource = new System.Windows.Forms.Panel();
			this.lblConnectionString = new System.Windows.Forms.Label();
			this.cbConnectionStrings = new System.Windows.Forms.ComboBox();
			this._ucObjectType = new NSqlTools.UI.UserControls.ucObjectType();
			this.pnlSearch.SuspendLayout();
			this.gbDBObject.SuspendLayout();
			this.pnlDBObject.SuspendLayout();
			this.pnlSchema.SuspendLayout();
			this.pnlObjectType.SuspendLayout();
			this.pnlDB.SuspendLayout();
			this.pnlDataSource.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlSearch
			// 
			this.pnlSearch.Controls.Add(this.gbDBObject);
			this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSearch.Location = new System.Drawing.Point(0, 0);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Size = new System.Drawing.Size(251, 501);
			this.pnlSearch.TabIndex = 12;
			// 
			// gbDBObject
			// 
			this.gbDBObject.AutoSize = true;
			this.gbDBObject.Controls.Add(this.pnlDBObject);
			this.gbDBObject.Controls.Add(this.pnlSchema);
			this.gbDBObject.Controls.Add(this.pnlObjectType);
			this.gbDBObject.Controls.Add(this.pnlDB);
			this.gbDBObject.Controls.Add(this.pnlDataSource);
			this.gbDBObject.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbDBObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbDBObject.Location = new System.Drawing.Point(0, 0);
			this.gbDBObject.Name = "gbDBObject";
			this.gbDBObject.Size = new System.Drawing.Size(251, 355);
			this.gbDBObject.TabIndex = 15;
			this.gbDBObject.TabStop = false;
			this.gbDBObject.Text = "groupBox1";
			// 
			// pnlDBObject
			// 
			this.pnlDBObject.Controls.Add(this.txtDBObjectFilter);
			this.pnlDBObject.Controls.Add(this.lbDBObject);
			this.pnlDBObject.Controls.Add(this.lblDBObject);
			this.pnlDBObject.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlDBObject.Location = new System.Drawing.Point(3, 243);
			this.pnlDBObject.Margin = new System.Windows.Forms.Padding(1);
			this.pnlDBObject.Name = "pnlDBObject";
			this.pnlDBObject.Size = new System.Drawing.Size(245, 109);
			this.pnlDBObject.TabIndex = 19;
			// 
			// txtDBObjectFilter
			// 
			this.txtDBObjectFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDBObjectFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.txtDBObjectFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.txtDBObjectFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDBObjectFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.txtDBObjectFilter.Location = new System.Drawing.Point(3, 17);
			this.txtDBObjectFilter.Name = "txtDBObjectFilter";
			this.txtDBObjectFilter.Size = new System.Drawing.Size(240, 20);
			this.txtDBObjectFilter.TabIndex = 5;
			this.txtDBObjectFilter.TextChanged += new System.EventHandler(this.txtDBObjectFilter_TextChanged);
			// 
			// lbDBObject
			// 
			this.lbDBObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbDBObject.DisplayMember = "Name";
			this.lbDBObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbDBObject.FormattingEnabled = true;
			this.lbDBObject.Location = new System.Drawing.Point(3, 36);
			this.lbDBObject.Name = "lbDBObject";
			this.lbDBObject.Size = new System.Drawing.Size(240, 69);
			this.lbDBObject.TabIndex = 6;
			this.lbDBObject.ValueMember = "ObjectId";
			this.lbDBObject.SelectedIndexChanged += new System.EventHandler(this.lbDBObject_SelectedIndexChanged);
			this.lbDBObject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbDBObject_KeyDown);
			// 
			// lblDBObject
			// 
			this.lblDBObject.AutoSize = true;
			this.lblDBObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDBObject.Location = new System.Drawing.Point(3, 2);
			this.lblDBObject.Name = "lblDBObject";
			this.lblDBObject.Size = new System.Drawing.Size(56, 13);
			this.lblDBObject.TabIndex = 12;
			this.lblDBObject.Text = "DB Object";
			// 
			// pnlSchema
			// 
			this.pnlSchema.Controls.Add(this.lblSchema);
			this.pnlSchema.Controls.Add(this.cbSchema);
			this.pnlSchema.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSchema.Location = new System.Drawing.Point(3, 200);
			this.pnlSchema.Margin = new System.Windows.Forms.Padding(1);
			this.pnlSchema.Name = "pnlSchema";
			this.pnlSchema.Size = new System.Drawing.Size(245, 43);
			this.pnlSchema.TabIndex = 10;
			// 
			// lblSchema
			// 
			this.lblSchema.AutoSize = true;
			this.lblSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSchema.Location = new System.Drawing.Point(3, 0);
			this.lblSchema.Name = "lblSchema";
			this.lblSchema.Size = new System.Drawing.Size(46, 13);
			this.lblSchema.TabIndex = 14;
			this.lblSchema.Text = "Schema";
			// 
			// cbSchema
			// 
			this.cbSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.cbSchema.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbSchema.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSchema.DisplayMember = "Name";
			this.cbSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.cbSchema.FormattingEnabled = true;
			this.cbSchema.Location = new System.Drawing.Point(3, 15);
			this.cbSchema.Name = "cbSchema";
			this.cbSchema.Size = new System.Drawing.Size(240, 24);
			this.cbSchema.TabIndex = 4;
			this.cbSchema.ValueMember = "SchemaId";
			this.cbSchema.SelectedIndexChanged += new System.EventHandler(this.cbSchema_SelectedIndexChanged);
			this.cbSchema.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSchema_KeyDown);
			// 
			// pnlObjectType
			// 
			this.pnlObjectType.Controls.Add(this._ucObjectType);
			this.pnlObjectType.Controls.Add(this.lblObjectType);
			this.pnlObjectType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlObjectType.Location = new System.Drawing.Point(3, 158);
			this.pnlObjectType.Margin = new System.Windows.Forms.Padding(1);
			this.pnlObjectType.Name = "pnlObjectType";
			this.pnlObjectType.Size = new System.Drawing.Size(245, 42);
			this.pnlObjectType.TabIndex = 17;
			// 
			// lblObjectType
			// 
			this.lblObjectType.AutoSize = true;
			this.lblObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblObjectType.Location = new System.Drawing.Point(3, 1);
			this.lblObjectType.Name = "lblObjectType";
			this.lblObjectType.Size = new System.Drawing.Size(65, 13);
			this.lblObjectType.TabIndex = 8;
			this.lblObjectType.Text = "Object Type";
			// 
			// pnlDB
			// 
			this.pnlDB.Controls.Add(this.txtDBFilter);
			this.pnlDB.Controls.Add(this.lblCheckedDBList);
			this.pnlDB.Controls.Add(this.clbDB);
			this.pnlDB.Controls.Add(this.lblDB);
			this.pnlDB.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlDB.Location = new System.Drawing.Point(3, 58);
			this.pnlDB.Margin = new System.Windows.Forms.Padding(1);
			this.pnlDB.Name = "pnlDB";
			this.pnlDB.Size = new System.Drawing.Size(245, 100);
			this.pnlDB.TabIndex = 16;
			// 
			// txtDBFilter
			// 
			this.txtDBFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDBFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.txtDBFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.txtDBFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDBFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.txtDBFilter.Location = new System.Drawing.Point(3, 15);
			this.txtDBFilter.Name = "txtDBFilter";
			this.txtDBFilter.Size = new System.Drawing.Size(240, 20);
			this.txtDBFilter.TabIndex = 9;
			this.txtDBFilter.TextChanged += new System.EventHandler(this.txtDBFilter_TextChanged);
			// 
			// lblCheckedDBList
			// 
			this.lblCheckedDBList.AutoSize = true;
			this.lblCheckedDBList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCheckedDBList.Location = new System.Drawing.Point(27, 1);
			this.lblCheckedDBList.Name = "lblCheckedDBList";
			this.lblCheckedDBList.Size = new System.Drawing.Size(0, 13);
			this.lblCheckedDBList.TabIndex = 8;
			// 
			// clbDB
			// 
			this.clbDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.clbDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.clbDB.FormattingEnabled = true;
			this.clbDB.Location = new System.Drawing.Point(3, 34);
			this.clbDB.Name = "clbDB";
			this.clbDB.Size = new System.Drawing.Size(239, 64);
			this.clbDB.TabIndex = 7;
			this.clbDB.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDB_ItemCheck);
			this.clbDB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clbDB_KeyDown);
			// 
			// lblDB
			// 
			this.lblDB.AutoSize = true;
			this.lblDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDB.Location = new System.Drawing.Point(3, 1);
			this.lblDB.Name = "lblDB";
			this.lblDB.Size = new System.Drawing.Size(22, 13);
			this.lblDB.TabIndex = 6;
			this.lblDB.Text = "DB";
			// 
			// pnlDataSource
			// 
			this.pnlDataSource.Controls.Add(this.lblConnectionString);
			this.pnlDataSource.Controls.Add(this.cbConnectionStrings);
			this.pnlDataSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlDataSource.Location = new System.Drawing.Point(3, 16);
			this.pnlDataSource.Margin = new System.Windows.Forms.Padding(1);
			this.pnlDataSource.Name = "pnlDataSource";
			this.pnlDataSource.Size = new System.Drawing.Size(245, 42);
			this.pnlDataSource.TabIndex = 15;
			// 
			// lblConnectionString
			// 
			this.lblConnectionString.AutoSize = true;
			this.lblConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblConnectionString.Location = new System.Drawing.Point(3, 0);
			this.lblConnectionString.Name = "lblConnectionString";
			this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
			this.lblConnectionString.TabIndex = 14;
			this.lblConnectionString.Text = "Connection String";
			// 
			// cbConnectionStrings
			// 
			this.cbConnectionStrings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbConnectionStrings.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbConnectionStrings.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbConnectionStrings.DisplayMember = "Name";
			this.cbConnectionStrings.DropDownWidth = 200;
			this.cbConnectionStrings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.cbConnectionStrings.FormattingEnabled = true;
			this.cbConnectionStrings.Location = new System.Drawing.Point(3, 14);
			this.cbConnectionStrings.Name = "cbConnectionStrings";
			this.cbConnectionStrings.Size = new System.Drawing.Size(240, 24);
			this.cbConnectionStrings.TabIndex = 1;
			this.cbConnectionStrings.ValueMember = "Name";
			this.cbConnectionStrings.SelectedIndexChanged += new System.EventHandler(this.cbConnectionString_SelectedIndexChanged);
			this.cbConnectionStrings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbConnectionStrings_KeyDown);
			// 
			// _ucObjectType
			// 
			this._ucObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._ucObjectType.IsNullable = false;
			this._ucObjectType.Location = new System.Drawing.Point(2, 15);
			this._ucObjectType.MainForm = null;
			this._ucObjectType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._ucObjectType.Name = "_ucObjectType";
			this._ucObjectType.ParentTabPage = null;
			this._ucObjectType.Size = new System.Drawing.Size(241, 26);
			this._ucObjectType.TabIndex = 17;
			this._ucObjectType.OnObjectTypeChanged += new System.EventHandler(this._ucObjectType_OnObjectTypeChanged);
			// 
			// ucDBObjectSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlSearch);
			this.Name = "ucDBObjectSelect";
			this.Size = new System.Drawing.Size(251, 501);
			this.pnlSearch.ResumeLayout(false);
			this.pnlSearch.PerformLayout();
			this.gbDBObject.ResumeLayout(false);
			this.pnlDBObject.ResumeLayout(false);
			this.pnlDBObject.PerformLayout();
			this.pnlSchema.ResumeLayout(false);
			this.pnlSchema.PerformLayout();
			this.pnlObjectType.ResumeLayout(false);
			this.pnlObjectType.PerformLayout();
			this.pnlDB.ResumeLayout(false);
			this.pnlDB.PerformLayout();
			this.pnlDataSource.ResumeLayout(false);
			this.pnlDataSource.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlSearch;
		private System.Windows.Forms.GroupBox gbDBObject;
		private System.Windows.Forms.Panel pnlDataSource;
		private System.Windows.Forms.Label lblConnectionString;
		private System.Windows.Forms.ComboBox cbConnectionStrings;
		private System.Windows.Forms.Panel pnlDB;
		private System.Windows.Forms.Label lblDB;
		private System.Windows.Forms.Panel pnlObjectType;
		private System.Windows.Forms.Label lblObjectType;
		private System.Windows.Forms.Panel pnlSchema;
		private System.Windows.Forms.Label lblSchema;
		private System.Windows.Forms.ComboBox cbSchema;
		private System.Windows.Forms.Panel pnlDBObject;
		private System.Windows.Forms.Label lblDBObject;
		private System.Windows.Forms.TextBox txtDBObjectFilter;
		private System.Windows.Forms.ListBox lbDBObject;
		private System.Windows.Forms.CheckedListBox clbDB;
		private System.Windows.Forms.Label lblCheckedDBList;
		private ucObjectType _ucObjectType;
		private System.Windows.Forms.TextBox txtDBFilter;
	}
}
