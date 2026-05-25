namespace NSqlTools.UI.Popups
{
	partial class frmSnippetPopup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSnippetPopup));
			this.tsMenu = new System.Windows.Forms.ToolStrip();
			this.tsbOK = new System.Windows.Forms.ToolStripButton();
			this.tsbClose = new System.Windows.Forms.ToolStripButton();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.lblExpansion = new System.Windows.Forms.Label();
			this.lblShortcut = new System.Windows.Forms.Label();
			this.txtShortcut = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.ucSqlNotePad = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.tsMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// tsMenu
			// 
			this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOK,
            this.tsbClose});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(1060, 31);
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
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// lblExpansion
			// 
			this.lblExpansion.AutoSize = true;
			this.lblExpansion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblExpansion.Location = new System.Drawing.Point(13, 156);
			this.lblExpansion.Name = "lblExpansion";
			this.lblExpansion.Size = new System.Drawing.Size(52, 13);
			this.lblExpansion.TabIndex = 25;
			this.lblExpansion.Text = "Sql Script";
			// 
			// lblShortcut
			// 
			this.lblShortcut.AutoSize = true;
			this.lblShortcut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblShortcut.Location = new System.Drawing.Point(12, 40);
			this.lblShortcut.Name = "lblShortcut";
			this.lblShortcut.Size = new System.Drawing.Size(47, 13);
			this.lblShortcut.TabIndex = 21;
			this.lblShortcut.Text = "Shortcut";
			// 
			// txtShortcut
			// 
			this.txtShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtShortcut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.txtShortcut.Location = new System.Drawing.Point(109, 37);
			this.txtShortcut.Name = "txtShortcut";
			this.txtShortcut.Size = new System.Drawing.Size(922, 20);
			this.txtShortcut.TabIndex = 22;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblDescription.Location = new System.Drawing.Point(12, 70);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(60, 13);
			this.lblDescription.TabIndex = 23;
			this.lblDescription.Text = "Description";
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.txtDescription.Location = new System.Drawing.Point(109, 67);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(924, 80);
			this.txtDescription.TabIndex = 24;
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
			this.ucSqlNotePad.Location = new System.Drawing.Point(109, 156);
			this.ucSqlNotePad.MainForm = null;
			this.ucSqlNotePad.Name = "ucSqlNotePad";
			this.ucSqlNotePad.ParentTabPage = null;
			this.ucSqlNotePad.SchemaKeywordList = null;
			this.ucSqlNotePad.scoSqlNotepadPanel2Collapsed = true;
			this.ucSqlNotePad.SearchKeyword = "";
			this.ucSqlNotePad.Size = new System.Drawing.Size(922, 318);
			this.ucSqlNotePad.TabIndex = 27;
			this.ucSqlNotePad.Title = "";
			// 
			// frmSnippetPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1060, 486);
			this.Controls.Add(this.ucSqlNotePad);
			this.Controls.Add(this.lblExpansion);
			this.Controls.Add(this.lblShortcut);
			this.Controls.Add(this.txtShortcut);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.tsMenu);
			this.KeyPreview = true;
			this.Name = "frmSnippetPopup";
			this.Text = "Snippet Definition";
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbOK;
		private System.Windows.Forms.ToolStripButton tsbClose;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Label lblExpansion;
		private System.Windows.Forms.Label lblShortcut;
		private System.Windows.Forms.TextBox txtShortcut;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtDescription;
		private UserControls.ucSqlNotePad ucSqlNotePad;
	}
}
