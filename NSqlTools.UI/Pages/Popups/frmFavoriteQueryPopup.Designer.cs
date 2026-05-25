namespace NSqlTools.UI.Popups
{
	partial class frmFavoriteQueryPopup
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

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbOK = new System.Windows.Forms.ToolStripButton();
			this.tsbClose = new System.Windows.Forms.ToolStripButton();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.ucSqlNotePad = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.lblCreateDate = new System.Windows.Forms.Label();
			this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
			this.lblQuery = new System.Windows.Forms.Label();
			this.tsMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblName.Location = new System.Drawing.Point(12, 50);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(109, 47);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(812, 20);
			this.txtName.TabIndex = 1;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblDescription.Location = new System.Drawing.Point(12, 80);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(60, 13);
			this.lblDescription.TabIndex = 2;
			this.lblDescription.Text = "Description";
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Location = new System.Drawing.Point(109, 77);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(812, 20);
			this.txtDescription.TabIndex = 2;
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOK,
            this.tsbClose});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(932, 31);
			this.tsMenu.TabIndex = 20;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbOK
			// 
			this.tsbOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOK.Image = global::NSqlTools.UI.Properties.Resources.Ok;
			this.tsbOK.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOK.Name = "tsbOK";
			this.tsbOK.Size = new System.Drawing.Size(28, 28);
			this.tsbOK.Text = "OK";
			this.tsbOK.Click += new System.EventHandler(this.tsbOK_Click);
			// 
			// tsbClose
			// 
			this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbClose.Image = global::NSqlTools.UI.Properties.Resources.CloseBlue;
			this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbClose.Name = "tsbClose";
			this.tsbClose.Size = new System.Drawing.Size(28, 28);
			this.tsbClose.Text = "Close";
			this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// ucSqlNotePad
			// 
			this.ucSqlNotePad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ucSqlNotePad.CaseSensitive = false;
			this.ucSqlNotePad.CompareTypeVisible = false;
			this.ucSqlNotePad.DBObjectContract = null;
			this.ucSqlNotePad.DBObjectKeywordList = null;
			this.ucSqlNotePad.DisplayFullScreen = true;
			this.ucSqlNotePad.DisplayStatus = false;
			this.ucSqlNotePad.FontSize = 12;
			this.ucSqlNotePad.Location = new System.Drawing.Point(15, 155);
			this.ucSqlNotePad.MainForm = null;
			this.ucSqlNotePad.Name = "ucSqlNotePad";
			this.ucSqlNotePad.ParentTabPage = null;
			this.ucSqlNotePad.SchemaKeywordList = null;
			this.ucSqlNotePad.scoSqlNotepadPanel2Collapsed = true;
			this.ucSqlNotePad.SearchKeyword = "";
			this.ucSqlNotePad.Size = new System.Drawing.Size(906, 293);
			this.ucSqlNotePad.TabIndex = 4;
			this.ucSqlNotePad.Title = "";
			// 
			// lblCreateDate
			// 
			this.lblCreateDate.AutoSize = true;
			this.lblCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblCreateDate.Location = new System.Drawing.Point(12, 109);
			this.lblCreateDate.Name = "lblCreateDate";
			this.lblCreateDate.Size = new System.Drawing.Size(64, 13);
			this.lblCreateDate.TabIndex = 22;
			this.lblCreateDate.Text = "Create Date";
			// 
			// dtpCreateDate
			// 
			this.dtpCreateDate.CustomFormat = "dddd MMMM dd, yyyy hh:mm:ss";
			this.dtpCreateDate.Enabled = false;
			this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCreateDate.Location = new System.Drawing.Point(109, 103);
			this.dtpCreateDate.Name = "dtpCreateDate";
			this.dtpCreateDate.Size = new System.Drawing.Size(264, 20);
			this.dtpCreateDate.TabIndex = 3;
			// 
			// lblQuery
			// 
			this.lblQuery.AutoSize = true;
			this.lblQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblQuery.Location = new System.Drawing.Point(12, 139);
			this.lblQuery.Name = "lblQuery";
			this.lblQuery.Size = new System.Drawing.Size(35, 13);
			this.lblQuery.TabIndex = 24;
			this.lblQuery.Text = "Query";
			// 
			// frmFavoriteQueryPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(932, 460);
			this.Controls.Add(this.lblQuery);
			this.Controls.Add(this.dtpCreateDate);
			this.Controls.Add(this.lblCreateDate);
			this.Controls.Add(this.ucSqlNotePad);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.tsMenu);
			this.KeyPreview = true;
			this.Name = "frmFavoriteQueryPopup";
			this.Text = "Favorite Query";
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbOK;
		private System.Windows.Forms.ToolStripButton tsbClose;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.DateTimePicker dtpCreateDate;
		private System.Windows.Forms.Label lblCreateDate;
		private UserControls.ucSqlNotePad ucSqlNotePad;
		private System.Windows.Forms.Label lblQuery;
	}
}
