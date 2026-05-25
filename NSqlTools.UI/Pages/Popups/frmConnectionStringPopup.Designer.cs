namespace NSqlTools.UI.Popups
{
	partial class frmConnectionStringDefinitionPopup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectionStringDefinitionPopup));
			this.lblConnectionString = new System.Windows.Forms.Label();
			this.txtConnectionString = new System.Windows.Forms.TextBox();
			this.lblUserName = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbOK = new System.Windows.Forms.ToolStripButton();
			this.tsbClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbConnectionTest = new System.Windows.Forms.ToolStripButton();
			this.txtInitialCatalog = new System.Windows.Forms.TextBox();
			this.lblInitialCatalog = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.txtDataSource = new System.Windows.Forms.TextBox();
			this.lblDataSource = new System.Windows.Forms.Label();
			this.txtConnectionStringName = new System.Windows.Forms.TextBox();
			this.lblConnectionStringName = new System.Windows.Forms.Label();
			this.cbIntegratedSecurity = new System.Windows.Forms.CheckBox();
			this.txtAddDatabase = new System.Windows.Forms.TextBox();
			this.lstDatabases = new System.Windows.Forms.ListBox();
			this.btnAddDb = new System.Windows.Forms.Button();
			this.btnDbUp = new System.Windows.Forms.Button();
			this.btnDbDown = new System.Windows.Forms.Button();
			this.btnDbRemove = new System.Windows.Forms.Button();
			this.lblDatabaseOrder = new System.Windows.Forms.Label();
			this.tsMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// lblConnectionString
			// 
			this.lblConnectionString.AutoSize = true;
			this.lblConnectionString.Location = new System.Drawing.Point(9, 82);
			this.lblConnectionString.Name = "lblConnectionString";
			this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
			this.lblConnectionString.TabIndex = 0;
			this.lblConnectionString.Text = "Connection String";
			// 
			// txtConnectionString
			// 
			this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConnectionString.BackColor = System.Drawing.Color.Linen;
			this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtConnectionString.Location = new System.Drawing.Point(118, 77);
			this.txtConnectionString.Multiline = true;
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.Size = new System.Drawing.Size(533, 102);
			this.txtConnectionString.TabIndex = 2;
			// 
			// lblUserName
			// 
			this.lblUserName.AutoSize = true;
			this.lblUserName.Location = new System.Drawing.Point(12, 242);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(60, 13);
			this.lblUserName.TabIndex = 4;
			this.lblUserName.Text = "User Name";
			// 
			// txtUserName
			// 
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtUserName.Location = new System.Drawing.Point(118, 237);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(379, 23);
			this.txtUserName.TabIndex = 5;
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtPassword.Location = new System.Drawing.Point(118, 269);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(379, 23);
			this.txtPassword.TabIndex = 6;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(12, 274);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(53, 13);
			this.lblPassword.TabIndex = 6;
			this.lblPassword.Text = "Password";
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOK,
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbConnectionTest});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(663, 31);
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
			this.tsbOK.Text = "Ok";
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
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbConnectionTest
			// 
			this.tsbConnectionTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbConnectionTest.Image = global::NSqlTools.UI.Properties.Resources.ConnectionStringTest;
			this.tsbConnectionTest.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbConnectionTest.Name = "tsbConnectionTest";
			this.tsbConnectionTest.Size = new System.Drawing.Size(28, 28);
			this.tsbConnectionTest.Text = "Test The Connection String";
			this.tsbConnectionTest.Click += new System.EventHandler(this.tsbConnectionTest_Click);
			// 
			// txtInitialCatalog
			// 
			this.txtInitialCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtInitialCatalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtInitialCatalog.Location = new System.Drawing.Point(118, 301);
			this.txtInitialCatalog.Name = "txtInitialCatalog";
			this.txtInitialCatalog.Size = new System.Drawing.Size(379, 23);
			this.txtInitialCatalog.TabIndex = 7;
			// 
			// lblInitialCatalog
			// 
			this.lblInitialCatalog.AutoSize = true;
			this.lblInitialCatalog.Location = new System.Drawing.Point(12, 306);
			this.lblInitialCatalog.Name = "lblInitialCatalog";
			this.lblInitialCatalog.Size = new System.Drawing.Size(90, 13);
			this.lblInitialCatalog.TabIndex = 21;
			this.lblInitialCatalog.Text = "Default Database";
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// txtDataSource
			// 
			this.txtDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDataSource.BackColor = System.Drawing.Color.Linen;
			this.txtDataSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtDataSource.Location = new System.Drawing.Point(118, 185);
			this.txtDataSource.Name = "txtDataSource";
			this.txtDataSource.Size = new System.Drawing.Size(379, 23);
			this.txtDataSource.TabIndex = 3;
			// 
			// lblDataSource
			// 
			this.lblDataSource.AutoSize = true;
			this.lblDataSource.Location = new System.Drawing.Point(12, 190);
			this.lblDataSource.Name = "lblDataSource";
			this.lblDataSource.Size = new System.Drawing.Size(67, 13);
			this.lblDataSource.TabIndex = 23;
			this.lblDataSource.Text = "Data Source";
			// 
			// txtConnectionStringName
			// 
			this.txtConnectionStringName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConnectionStringName.BackColor = System.Drawing.Color.Linen;
			this.txtConnectionStringName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtConnectionStringName.Location = new System.Drawing.Point(118, 48);
			this.txtConnectionStringName.Name = "txtConnectionStringName";
			this.txtConnectionStringName.Size = new System.Drawing.Size(533, 23);
			this.txtConnectionStringName.TabIndex = 1;
			// 
			// lblConnectionStringName
			// 
			this.lblConnectionStringName.AutoSize = true;
			this.lblConnectionStringName.Location = new System.Drawing.Point(9, 53);
			this.lblConnectionStringName.Name = "lblConnectionStringName";
			this.lblConnectionStringName.Size = new System.Drawing.Size(35, 13);
			this.lblConnectionStringName.TabIndex = 25;
			this.lblConnectionStringName.Text = typeof(NSqlTools.Types.Properties.CommonResource).Name;
			// 
			// cbIntegratedSecurity
			// 
			this.cbIntegratedSecurity.AutoSize = true;
			this.cbIntegratedSecurity.Location = new System.Drawing.Point(118, 219);
			this.cbIntegratedSecurity.Name = "cbIntegratedSecurity";
			this.cbIntegratedSecurity.Size = new System.Drawing.Size(115, 17);
			this.cbIntegratedSecurity.TabIndex = 4;
			this.cbIntegratedSecurity.Text = "Integrated Security";
			this.cbIntegratedSecurity.UseVisualStyleBackColor = true;
			this.cbIntegratedSecurity.CheckedChanged += new System.EventHandler(this.cbIntegratedSecurity_CheckedChanged);
			// 
			// txtAddDatabase
			// 
			this.txtAddDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAddDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtAddDatabase.Location = new System.Drawing.Point(118, 330);
			this.txtAddDatabase.Name = "txtAddDatabase";
			this.txtAddDatabase.Size = new System.Drawing.Size(379, 23);
			this.txtAddDatabase.TabIndex = 8;
			// 
			// lstDatabases
			// 
			this.lstDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstDatabases.FormattingEnabled = true;
			this.lstDatabases.IntegralHeight = false;
			this.lstDatabases.Location = new System.Drawing.Point(118, 357);
			this.lstDatabases.Name = "lstDatabases";
			this.lstDatabases.Size = new System.Drawing.Size(379, 100);
			this.lstDatabases.TabIndex = 9;
			// 
			// btnAddDb
			// 
			this.btnAddDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddDb.Image = global::NSqlTools.UI.Properties.Resources.Add;
			this.btnAddDb.Location = new System.Drawing.Point(503, 330);
			this.btnAddDb.Name = "btnAddDb";
			this.btnAddDb.Size = new System.Drawing.Size(70, 23);
			this.btnAddDb.TabIndex = 10;
			this.btnAddDb.UseVisualStyleBackColor = true;
			this.btnAddDb.Click += new System.EventHandler(this.btnAddDb_Click);
			// 
			// btnDbUp
			// 
			this.btnDbUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDbUp.Image = global::NSqlTools.UI.Properties.Resources.Up;
			this.btnDbUp.Location = new System.Drawing.Point(503, 360);
			this.btnDbUp.Name = "btnDbUp";
			this.btnDbUp.Size = new System.Drawing.Size(70, 23);
			this.btnDbUp.TabIndex = 11;
			this.btnDbUp.UseVisualStyleBackColor = true;
			this.btnDbUp.Click += new System.EventHandler(this.btnDbUp_Click);
			// 
			// btnDbDown
			// 
			this.btnDbDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDbDown.Image = global::NSqlTools.UI.Properties.Resources.Down;
			this.btnDbDown.Location = new System.Drawing.Point(503, 389);
			this.btnDbDown.Name = "btnDbDown";
			this.btnDbDown.Size = new System.Drawing.Size(70, 23);
			this.btnDbDown.TabIndex = 12;
			this.btnDbDown.UseVisualStyleBackColor = true;
			this.btnDbDown.Click += new System.EventHandler(this.btnDbDown_Click);
			// 
			// btnDbRemove
			// 
			this.btnDbRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDbRemove.Image = global::NSqlTools.UI.Properties.Resources.Minus;
			this.btnDbRemove.Location = new System.Drawing.Point(503, 418);
			this.btnDbRemove.Name = "btnDbRemove";
			this.btnDbRemove.Size = new System.Drawing.Size(70, 23);
			this.btnDbRemove.TabIndex = 13;
			this.btnDbRemove.UseVisualStyleBackColor = true;
			this.btnDbRemove.Click += new System.EventHandler(this.btnDbRemove_Click);
			// 
			// lblDatabaseOrder
			// 
			this.lblDatabaseOrder.AutoSize = true;
			this.lblDatabaseOrder.Location = new System.Drawing.Point(12, 335);
			this.lblDatabaseOrder.Name = "lblDatabaseOrder";
			this.lblDatabaseOrder.Size = new System.Drawing.Size(82, 13);
			this.lblDatabaseOrder.TabIndex = 26;
			this.lblDatabaseOrder.Text = "Database Order";
			// 
			// frmConnectionStringDefinitionPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(663, 489);
			this.Controls.Add(this.lblDatabaseOrder);
			this.Controls.Add(this.btnDbRemove);
			this.Controls.Add(this.btnDbDown);
			this.Controls.Add(this.btnDbUp);
			this.Controls.Add(this.btnAddDb);
			this.Controls.Add(this.lstDatabases);
			this.Controls.Add(this.txtAddDatabase);
			this.Controls.Add(this.cbIntegratedSecurity);
			this.Controls.Add(this.txtConnectionStringName);
			this.Controls.Add(this.lblConnectionStringName);
			this.Controls.Add(this.txtDataSource);
			this.Controls.Add(this.lblDataSource);
			this.Controls.Add(this.txtInitialCatalog);
			this.Controls.Add(this.lblInitialCatalog);
			this.Controls.Add(this.tsMenu);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.lblUserName);
			this.Controls.Add(this.txtConnectionString);
			this.Controls.Add(this.lblConnectionString);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "frmConnectionStringDefinitionPopup";
			this.Text = "Connection String Definition";
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.Label lblConnectionString;
		private System.Windows.Forms.TextBox txtConnectionString;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbOK;
		private System.Windows.Forms.ToolStripButton tsbClose;
		private System.Windows.Forms.ToolStripButton tsbConnectionTest;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.TextBox txtInitialCatalog;
		private System.Windows.Forms.Label lblInitialCatalog;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.TextBox txtDataSource;
		private System.Windows.Forms.Label lblDataSource;
		private System.Windows.Forms.TextBox txtConnectionStringName;
		private System.Windows.Forms.Label lblConnectionStringName;
		private System.Windows.Forms.CheckBox cbIntegratedSecurity;
		private System.Windows.Forms.TextBox txtAddDatabase;
		private System.Windows.Forms.ListBox lstDatabases;
		private System.Windows.Forms.Button btnAddDb;
		private System.Windows.Forms.Button btnDbUp;
		private System.Windows.Forms.Button btnDbDown;
		private System.Windows.Forms.Button btnDbRemove;
		private System.Windows.Forms.Label lblDatabaseOrder;
	}
}