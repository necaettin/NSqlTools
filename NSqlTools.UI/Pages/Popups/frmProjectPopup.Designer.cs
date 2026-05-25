namespace NSqlTools.UI.Popups
{
	partial class frmProjectPopup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectPopup));
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbOK = new System.Windows.Forms.ToolStripButton();
			this.tsbClose = new System.Windows.Forms.ToolStripButton();
			this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.lblCreateDate = new System.Windows.Forms.Label();
			this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
			this.dtpUpdateDate = new System.Windows.Forms.DateTimePicker();
			this.lblUpdateDate = new System.Windows.Forms.Label();
			this.gbScreens = new System.Windows.Forms.GroupBox();
			this.flpScreens = new System.Windows.Forms.FlowLayoutPanel();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.gbScreenPackageInfo = new System.Windows.Forms.GroupBox();
			this.tsMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.gbScreens.SuspendLayout();
			this.gbScreenPackageInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblName.Location = new System.Drawing.Point(6, 25);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.txtName.Location = new System.Drawing.Point(103, 22);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(651, 20);
			this.txtName.TabIndex = 1;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblDescription.Location = new System.Drawing.Point(6, 55);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(60, 13);
			this.lblDescription.TabIndex = 2;
			this.lblDescription.Text = "Description";
			// 
			// txtDescription
			// 
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.txtDescription.Location = new System.Drawing.Point(103, 52);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(650, 80);
			this.txtDescription.TabIndex = 2;
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOK,
            this.tsbClose,
            this.tsbRefresh});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(789, 31);
			this.tsMenu.TabIndex = 20;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbOK
			// 
			this.tsbOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOK.Image = ((System.Drawing.Image)(resources.GetObject("tsbOK.Image")));
			this.tsbOK.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOK.Name = "tsbOK";
			this.tsbOK.Size = new System.Drawing.Size(28, 28);
			this.tsbOK.Text = "OK";
			this.tsbOK.Click += new System.EventHandler(this.tsbOK_Click);
			// 
			// tsbClose
			// 
			this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
			this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbClose.Name = "tsbClose";
			this.tsbClose.Size = new System.Drawing.Size(28, 28);
			this.tsbClose.Text = "Close";
			this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
			this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new System.Drawing.Size(28, 28);
			this.tsbRefresh.Text = "Fill with current opened documents";
			this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// lblCreateDate
			// 
			this.lblCreateDate.AutoSize = true;
			this.lblCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblCreateDate.Location = new System.Drawing.Point(6, 144);
			this.lblCreateDate.Name = "lblCreateDate";
			this.lblCreateDate.Size = new System.Drawing.Size(64, 13);
			this.lblCreateDate.TabIndex = 22;
			this.lblCreateDate.Text = "Create Date";
			// 
			// dtpCreateDate
			// 
			this.dtpCreateDate.CustomFormat = "dddd MMMM dd, yyyy hh:mm:ss";
			this.dtpCreateDate.Enabled = false;
			this.dtpCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCreateDate.Location = new System.Drawing.Point(103, 138);
			this.dtpCreateDate.Name = "dtpCreateDate";
			this.dtpCreateDate.ShowCheckBox = true;
			this.dtpCreateDate.Size = new System.Drawing.Size(264, 20);
			this.dtpCreateDate.TabIndex = 3;
			// 
			// dtpUpdateDate
			// 
			this.dtpUpdateDate.CustomFormat = "dddd MMMM dd, yyyy hh:mm:ss";
			this.dtpUpdateDate.Enabled = false;
			this.dtpUpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.dtpUpdateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpUpdateDate.Location = new System.Drawing.Point(506, 138);
			this.dtpUpdateDate.Name = "dtpUpdateDate";
			this.dtpUpdateDate.ShowCheckBox = true;
			this.dtpUpdateDate.Size = new System.Drawing.Size(247, 20);
			this.dtpUpdateDate.TabIndex = 4;
			// 
			// lblUpdateDate
			// 
			this.lblUpdateDate.AutoSize = true;
			this.lblUpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblUpdateDate.Location = new System.Drawing.Point(399, 144);
			this.lblUpdateDate.Name = "lblUpdateDate";
			this.lblUpdateDate.Size = new System.Drawing.Size(68, 13);
			this.lblUpdateDate.TabIndex = 24;
			this.lblUpdateDate.Text = "Update Date";
			// 
			// gbScreens
			// 
			this.gbScreens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbScreens.Controls.Add(this.flpScreens);
			this.gbScreens.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.gbScreens.Location = new System.Drawing.Point(10, 211);
			this.gbScreens.Name = "gbScreens";
			this.gbScreens.Size = new System.Drawing.Size(763, 294);
			this.gbScreens.TabIndex = 28;
			this.gbScreens.TabStop = false;
			this.gbScreens.Text = "Screens";
			// 
			// flpScreens
			// 
			this.flpScreens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flpScreens.AutoScroll = true;
			this.flpScreens.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flpScreens.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.flpScreens.Location = new System.Drawing.Point(3, 16);
			this.flpScreens.Name = "flpScreens";
			this.flpScreens.Size = new System.Drawing.Size(757, 275);
			this.flpScreens.TabIndex = 5;
			this.flpScreens.WrapContents = false;
			// 
			// gbScreenPackageInfo
			// 
			this.gbScreenPackageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbScreenPackageInfo.Controls.Add(this.lblName);
			this.gbScreenPackageInfo.Controls.Add(this.txtName);
			this.gbScreenPackageInfo.Controls.Add(this.dtpUpdateDate);
			this.gbScreenPackageInfo.Controls.Add(this.lblDescription);
			this.gbScreenPackageInfo.Controls.Add(this.lblUpdateDate);
			this.gbScreenPackageInfo.Controls.Add(this.txtDescription);
			this.gbScreenPackageInfo.Controls.Add(this.dtpCreateDate);
			this.gbScreenPackageInfo.Controls.Add(this.lblCreateDate);
			this.gbScreenPackageInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.gbScreenPackageInfo.Location = new System.Drawing.Point(13, 34);
			this.gbScreenPackageInfo.Name = "gbScreenPackageInfo";
			this.gbScreenPackageInfo.Size = new System.Drawing.Size(760, 168);
			this.gbScreenPackageInfo.TabIndex = 29;
			this.gbScreenPackageInfo.TabStop = false;
			this.gbScreenPackageInfo.Text = "Screen Package Info";
			// 
			// frmProjectPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(789, 510);
			this.Controls.Add(this.gbScreenPackageInfo);
			this.Controls.Add(this.gbScreens);
			this.Controls.Add(this.tsMenu);
			this.KeyPreview = true;
			this.Name = "frmProjectPopup";
			this.Text = "Screen Package";
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.gbScreens.ResumeLayout(false);
			this.gbScreenPackageInfo.ResumeLayout(false);
			this.gbScreenPackageInfo.PerformLayout();
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
		private System.Windows.Forms.DateTimePicker dtpUpdateDate;
		private System.Windows.Forms.Label lblUpdateDate;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.GroupBox gbScreens;
		private System.Windows.Forms.FlowLayoutPanel flpScreens;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.GroupBox gbScreenPackageInfo;
	}
}
