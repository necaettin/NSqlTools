namespace NSqlTools.UI.Pages.Screens
{
	partial class frmEncryption
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
			this.components = new System.ComponentModel.Container();
			this.rbEncrypt = new System.Windows.Forms.RadioButton();
			this.rbDecrypt = new System.Windows.Forms.RadioButton();
			this.txtInput = new MetroFramework.Controls.MetroTextBox();
			this.txtKey = new MetroFramework.Controls.MetroTextBox();
			this.lblKey = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.gbInput = new System.Windows.Forms.GroupBox();
			this.gbOutput = new System.Windows.Forms.GroupBox();
			this.txtOutput = new MetroFramework.Controls.MetroTextBox();
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbRun = new System.Windows.Forms.ToolStripButton();
			this.cbCompareType = new System.Windows.Forms.ToolStripComboBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.gbEncryptDecrypt = new System.Windows.Forms.GroupBox();
			this.gbMethod = new System.Windows.Forms.GroupBox();
			this.rbJournal = new System.Windows.Forms.RadioButton();
			this.rbAES = new System.Windows.Forms.RadioButton();
			this.rbSData = new System.Windows.Forms.RadioButton();
			this.rbSha256 = new System.Windows.Forms.RadioButton();
			this.rbRijndael = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.gbInput.SuspendLayout();
			this.gbOutput.SuspendLayout();
			this.tsMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.gbEncryptDecrypt.SuspendLayout();
			this.gbMethod.SuspendLayout();
			this.SuspendLayout();
			// 
			// rbEncrypt
			// 
			this.rbEncrypt.AutoSize = true;
			this.rbEncrypt.Checked = true;
			this.rbEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbEncrypt.Location = new System.Drawing.Point(6, 18);
			this.rbEncrypt.Name = "rbEncrypt";
			this.rbEncrypt.Size = new System.Drawing.Size(61, 17);
			this.rbEncrypt.TabIndex = 1;
			this.rbEncrypt.TabStop = true;
			this.rbEncrypt.Text = "Encrypt";
			this.rbEncrypt.UseVisualStyleBackColor = true;
			// 
			// rbDecrypt
			// 
			this.rbDecrypt.AutoSize = true;
			this.rbDecrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbDecrypt.Location = new System.Drawing.Point(116, 18);
			this.rbDecrypt.Name = "rbDecrypt";
			this.rbDecrypt.Size = new System.Drawing.Size(62, 17);
			this.rbDecrypt.TabIndex = 2;
			this.rbDecrypt.Text = "Decrypt";
			this.rbDecrypt.UseVisualStyleBackColor = true;
			// 
			// txtInput
			// 
			this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtInput.Location = new System.Drawing.Point(3, 16);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(668, 194);
			this.txtInput.TabIndex = 4;
			// 
			// txtKey
			// 
			this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKey.Location = new System.Drawing.Point(15, 146);
			this.txtKey.Name = "txtKey";
			this.txtKey.Size = new System.Drawing.Size(671, 23);
			this.txtKey.TabIndex = 14;
			// 
			// lblKey
			// 
			this.lblKey.AutoSize = true;
			this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblKey.Location = new System.Drawing.Point(12, 130);
			this.lblKey.Name = "lblKey";
			this.lblKey.Size = new System.Drawing.Size(28, 13);
			this.lblKey.TabIndex = 13;
			this.lblKey.Text = "Key";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 175);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gbInput);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gbOutput);
			this.splitContainer1.Size = new System.Drawing.Size(674, 427);
			this.splitContainer1.SplitterDistance = 213;
			this.splitContainer1.TabIndex = 15;
			// 
			// gbInput
			// 
			this.gbInput.Controls.Add(this.txtInput);
			this.gbInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbInput.Location = new System.Drawing.Point(0, 0);
			this.gbInput.Name = "gbInput";
			this.gbInput.Size = new System.Drawing.Size(674, 213);
			this.gbInput.TabIndex = 5;
			this.gbInput.TabStop = false;
			this.gbInput.Text = "Input";
			// 
			// gbOutput
			// 
			this.gbOutput.Controls.Add(this.txtOutput);
			this.gbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbOutput.Location = new System.Drawing.Point(0, 0);
			this.gbOutput.Name = "gbOutput";
			this.gbOutput.Size = new System.Drawing.Size(674, 210);
			this.gbOutput.TabIndex = 6;
			this.gbOutput.TabStop = false;
			this.gbOutput.Text = "Output";
			// 
			// txtOutput
			// 
			this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutput.Location = new System.Drawing.Point(3, 16);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.Size = new System.Drawing.Size(668, 191);
			this.txtOutput.TabIndex = 4;
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRun,
            this.cbCompareType});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(694, 31);
			this.tsMenu.TabIndex = 20;
			this.tsMenu.Text = "Navigation";
			// 
			// tsbRun
			// 
			this.tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRun.Image = global::NSqlTools.UI.Properties.Resources.RunScript;
			this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRun.Name = "tsbRun";
			this.tsbRun.Size = new System.Drawing.Size(28, 28);
			this.tsbRun.Text = "Run";
			this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
			// 
			// cbCompareType
			// 
			this.cbCompareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCompareType.Name = "cbCompareType";
			this.cbCompareType.Size = new System.Drawing.Size(121, 31);
			this.cbCompareType.Visible = false;
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// gbEncryptDecrypt
			// 
			this.gbEncryptDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbEncryptDecrypt.Controls.Add(this.rbEncrypt);
			this.gbEncryptDecrypt.Controls.Add(this.rbDecrypt);
			this.gbEncryptDecrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbEncryptDecrypt.Location = new System.Drawing.Point(12, 34);
			this.gbEncryptDecrypt.Name = "gbEncryptDecrypt";
			this.gbEncryptDecrypt.Size = new System.Drawing.Size(674, 42);
			this.gbEncryptDecrypt.TabIndex = 21;
			this.gbEncryptDecrypt.TabStop = false;
			this.gbEncryptDecrypt.Text = "Encrypt \\ Decrypt";
			// 
			// gbMethod
			// 
			this.gbMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbMethod.Controls.Add(this.rbJournal);
			this.gbMethod.Controls.Add(this.rbAES);
			this.gbMethod.Controls.Add(this.rbSData);
			this.gbMethod.Controls.Add(this.rbSha256);
			this.gbMethod.Controls.Add(this.rbRijndael);
			this.gbMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gbMethod.Location = new System.Drawing.Point(12, 82);
			this.gbMethod.Name = "gbMethod";
			this.gbMethod.Size = new System.Drawing.Size(674, 42);
			this.gbMethod.TabIndex = 22;
			this.gbMethod.TabStop = false;
			this.gbMethod.Text = "Method";
			// 
			// rbJournal
			// 
			this.rbJournal.AutoSize = true;
			this.rbJournal.Location = new System.Drawing.Point(399, 19);
			this.rbJournal.Name = "rbJournal";
			this.rbJournal.Size = new System.Drawing.Size(66, 17);
			this.rbJournal.TabIndex = 21;
			this.rbJournal.Tag = "5";
			this.rbJournal.Text = "Journal";
			this.rbJournal.UseVisualStyleBackColor = true;
			this.rbJournal.CheckedChanged += new System.EventHandler(this.encryptionAlgorithm_CheckedChanged);
			// 
			// rbAES
			// 
			this.rbAES.AutoSize = true;
			this.rbAES.Location = new System.Drawing.Point(314, 19);
			this.rbAES.Name = "rbAES";
			this.rbAES.Size = new System.Drawing.Size(49, 17);
			this.rbAES.TabIndex = 20;
			this.rbAES.Tag = "4";
			this.rbAES.Text = "AES";
			this.rbAES.UseVisualStyleBackColor = true;
			this.rbAES.CheckedChanged += new System.EventHandler(this.encryptionAlgorithm_CheckedChanged);
			// 
			// rbSData
			// 
			this.rbSData.AutoSize = true;
			this.rbSData.Location = new System.Drawing.Point(226, 19);
			this.rbSData.Name = "rbSData";
			this.rbSData.Size = new System.Drawing.Size(60, 17);
			this.rbSData.TabIndex = 19;
			this.rbSData.Tag = "3";
			this.rbSData.Text = "SData";
			this.rbSData.UseVisualStyleBackColor = true;
			this.rbSData.CheckedChanged += new System.EventHandler(this.encryptionAlgorithm_CheckedChanged);
			// 
			// rbSha256
			// 
			this.rbSha256.AutoSize = true;
			this.rbSha256.Location = new System.Drawing.Point(118, 19);
			this.rbSha256.Name = "rbSha256";
			this.rbSha256.Size = new System.Drawing.Size(68, 17);
			this.rbSha256.TabIndex = 18;
			this.rbSha256.Tag = "2";
			this.rbSha256.Text = "Sha256";
			this.rbSha256.UseVisualStyleBackColor = true;
			this.rbSha256.CheckedChanged += new System.EventHandler(this.encryptionAlgorithm_CheckedChanged);
			// 
			// rbRijndael
			// 
			this.rbRijndael.AutoSize = true;
			this.rbRijndael.Checked = true;
			this.rbRijndael.Location = new System.Drawing.Point(8, 19);
			this.rbRijndael.Name = "rbRijndael";
			this.rbRijndael.Size = new System.Drawing.Size(71, 17);
			this.rbRijndael.TabIndex = 17;
			this.rbRijndael.TabStop = true;
			this.rbRijndael.Tag = "1";
			this.rbRijndael.Text = "Rijndael";
			this.rbRijndael.UseVisualStyleBackColor = true;
			this.rbRijndael.CheckedChanged += new System.EventHandler(this.encryptionAlgorithm_CheckedChanged);
			// 
			// frmEncryption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(694, 614);
			this.Controls.Add(this.gbMethod);
			this.Controls.Add(this.gbEncryptDecrypt);
			this.Controls.Add(this.tsMenu);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.txtKey);
			this.Controls.Add(this.lblKey);
			this.Name = "frmEncryption";
			this.Text = "BOA Encrypt\\Decrypt";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.gbInput.ResumeLayout(false);
			this.gbOutput.ResumeLayout(false);
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.gbEncryptDecrypt.ResumeLayout(false);
			this.gbEncryptDecrypt.PerformLayout();
			this.gbMethod.ResumeLayout(false);
			this.gbMethod.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.RadioButton rbEncrypt;
		private System.Windows.Forms.RadioButton rbDecrypt;
		private MetroFramework.Controls.MetroTextBox txtInput;
		private MetroFramework.Controls.MetroTextBox txtKey;
		private System.Windows.Forms.Label lblKey;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox gbInput;
		private System.Windows.Forms.GroupBox gbOutput;
		private MetroFramework.Controls.MetroTextBox txtOutput;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbRun;
		public System.Windows.Forms.ToolStripComboBox cbCompareType;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.GroupBox gbEncryptDecrypt;
		private System.Windows.Forms.GroupBox gbMethod;
		private System.Windows.Forms.RadioButton rbAES;
		private System.Windows.Forms.RadioButton rbSData;
		private System.Windows.Forms.RadioButton rbSha256;
		private System.Windows.Forms.RadioButton rbRijndael;
		private System.Windows.Forms.RadioButton rbJournal;
	}
}