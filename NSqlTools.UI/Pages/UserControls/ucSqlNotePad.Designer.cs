namespace NSqlTools.UI.UserControls
{
	partial class ucSqlNotePad
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
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlBodyContainer = new System.Windows.Forms.Panel();
            this.pnlScQueryContainer = new System.Windows.Forms.Panel();
            this.scSqlQuery = new ScintillaNET.Scintilla();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbWrap = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenFindDialog = new System.Windows.Forms.ToolStripButton();
            this.tsbFullScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFormat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBiggerChars = new System.Windows.Forms.ToolStripButton();
            this.tsbSmallerChars = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCaseSensitive = new System.Windows.Forms.ToolStripButton();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.tsbFind = new System.Windows.Forms.ToolStripButton();
            this.tsbFindResultPanelOpenClose = new System.Windows.Forms.ToolStripButton();
            this.tsbDown = new System.Windows.Forms.ToolStripButton();
            this.tsbUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cbCompareType = new System.Windows.Forms.ToolStripComboBox();
            this.pnlFindResultBody = new System.Windows.Forms.Panel();
            this.gbSqlNotePad = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.scoSqlNotepad = new System.Windows.Forms.SplitContainer();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pnlBody.SuspendLayout();
            this.pnlBodyContainer.SuspendLayout();
            this.pnlScQueryContainer.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.gbSqlNotePad.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoSqlNotepad)).BeginInit();
            this.scoSqlNotepad.Panel1.SuspendLayout();
            this.scoSqlNotepad.Panel2.SuspendLayout();
            this.scoSqlNotepad.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlBodyContainer);
            this.pnlBody.Controls.Add(this.tsMenu);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(806, 402);
            this.pnlBody.TabIndex = 13;
            // 
            // pnlBodyContainer
            // 
            this.pnlBodyContainer.Controls.Add(this.pnlScQueryContainer);
            this.pnlBodyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBodyContainer.Location = new System.Drawing.Point(0, 31);
            this.pnlBodyContainer.Name = "pnlBodyContainer";
            this.pnlBodyContainer.Size = new System.Drawing.Size(806, 371);
            this.pnlBodyContainer.TabIndex = 14;
            // 
            // pnlScQueryContainer
            // 
            this.pnlScQueryContainer.Controls.Add(this.scSqlQuery);
            this.pnlScQueryContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScQueryContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlScQueryContainer.Name = "pnlScQueryContainer";
            this.pnlScQueryContainer.Size = new System.Drawing.Size(806, 371);
            this.pnlScQueryContainer.TabIndex = 14;
            // 
            // scSqlQuery
            // 
            this.scSqlQuery.AutoCMaxHeight = 15;
            this.scSqlQuery.AutoCSeparator = '\0';
            this.scSqlQuery.CaretLineBackColor = System.Drawing.Color.AntiqueWhite;
            this.scSqlQuery.CaretLineVisible = true;
            this.scSqlQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSqlQuery.Location = new System.Drawing.Point(0, 0);
            this.scSqlQuery.Name = "scSqlQuery";
            this.scSqlQuery.ScrollWidth = 1;
            this.scSqlQuery.Size = new System.Drawing.Size(806, 371);
            this.scSqlQuery.TabIndex = 12;
            this.scSqlQuery.CharAdded += new System.EventHandler<ScintillaNET.CharAddedEventArgs>(this.scSqlQuery_CharAdded);
            this.scSqlQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scSqlQuery_KeyDown);
            this.scSqlQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.scSqlQuery_KeyUp);
            // 
            // tsMenu
            // 
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbWrap,
            this.tsbCopy,
            this.tsbSave,
            this.tsbOpenFindDialog,
            this.tsbFullScreen,
            this.toolStripSeparator4,
            this.tsbFormat,
            this.toolStripSeparator3,
            this.tsbBiggerChars,
            this.tsbSmallerChars,
            this.toolStripSeparator1,
            this.tsbCaseSensitive,
            this.txtFind,
            this.tsbFind,
            this.tsbFindResultPanelOpenClose,
            this.tsbDown,
            this.tsbUp,
            this.toolStripSeparator2,
            this.cbCompareType});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(806, 31);
            this.tsMenu.TabIndex = 13;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tsbWrap
            // 
            this.tsbWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWrap.Image = global::NSqlTools.UI.Properties.Resources.Wrap;
            this.tsbWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWrap.Name = "tsbWrap";
            this.tsbWrap.Size = new System.Drawing.Size(28, 28);
            this.tsbWrap.Text = "Wrap";
            this.tsbWrap.Click += new System.EventHandler(this.tsbWrap_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = global::NSqlTools.UI.Properties.Resources.Copy;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(28, 28);
            this.tsbCopy.Text = "Copy";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::NSqlTools.UI.Properties.Resources.Save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(28, 28);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbOpenFindDialog
            // 
            this.tsbOpenFindDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenFindDialog.Image = global::NSqlTools.UI.Properties.Resources.OpenFindDialog;
            this.tsbOpenFindDialog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenFindDialog.Name = "tsbOpenFindDialog";
            this.tsbOpenFindDialog.Size = new System.Drawing.Size(28, 28);
            this.tsbOpenFindDialog.Text = "Open Find Dialog";
            this.tsbOpenFindDialog.Click += new System.EventHandler(this.tsbOpenFindDialog_Click);
            // 
            // tsbFullScreen
            // 
            this.tsbFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFullScreen.Image = global::NSqlTools.UI.Properties.Resources.FullScreen;
            this.tsbFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullScreen.Name = "tsbFullScreen";
            this.tsbFullScreen.Size = new System.Drawing.Size(28, 28);
            this.tsbFullScreen.Text = "Full Screen";
            this.tsbFullScreen.Click += new System.EventHandler(this.tsbFullScreen_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbFormat
            // 
            this.tsbFormat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFormat.Image = global::NSqlTools.UI.Properties.Resources.Format;
            this.tsbFormat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFormat.Name = "tsbFormat";
            this.tsbFormat.Size = new System.Drawing.Size(28, 28);
            this.tsbFormat.Text = "Format Text";
            this.tsbFormat.Click += new System.EventHandler(this.tsbFormat_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbBiggerChars
            // 
            this.tsbBiggerChars.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBiggerChars.Image = global::NSqlTools.UI.Properties.Resources.BiggerChars;
            this.tsbBiggerChars.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBiggerChars.Name = "tsbBiggerChars";
            this.tsbBiggerChars.Size = new System.Drawing.Size(28, 28);
            this.tsbBiggerChars.Text = "Bigger Chars";
            this.tsbBiggerChars.Click += new System.EventHandler(this.tsbBiggerChars_Click);
            // 
            // tsbSmallerChars
            // 
            this.tsbSmallerChars.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSmallerChars.Image = global::NSqlTools.UI.Properties.Resources.SmallerChars;
            this.tsbSmallerChars.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSmallerChars.Name = "tsbSmallerChars";
            this.tsbSmallerChars.Size = new System.Drawing.Size(28, 28);
            this.tsbSmallerChars.Text = "Smaller Chars";
            this.tsbSmallerChars.Click += new System.EventHandler(this.tsbSmallerChars_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbCaseSensitive
            // 
            this.tsbCaseSensitive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCaseSensitive.Image = global::NSqlTools.UI.Properties.Resources.CaseSensitive;
            this.tsbCaseSensitive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCaseSensitive.Name = "tsbCaseSensitive";
            this.tsbCaseSensitive.Size = new System.Drawing.Size(28, 28);
            this.tsbCaseSensitive.Text = "Case Sentitive";
            this.tsbCaseSensitive.Click += new System.EventHandler(this.tsbCaseSensitive_Click);
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(100, 31);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // tsbFind
            // 
            this.tsbFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFind.Image = global::NSqlTools.UI.Properties.Resources.Search;
            this.tsbFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFind.Name = "tsbFind";
            this.tsbFind.Size = new System.Drawing.Size(28, 28);
            this.tsbFind.Text = "Find";
            this.tsbFind.Click += new System.EventHandler(this.tsbFind_Click);
            // 
            // tsbFindResultPanelOpenClose
            // 
            this.tsbFindResultPanelOpenClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindResultPanelOpenClose.Image = global::NSqlTools.UI.Properties.Resources.SearchResultOpen;
            this.tsbFindResultPanelOpenClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindResultPanelOpenClose.Name = "tsbFindResultPanelOpenClose";
            this.tsbFindResultPanelOpenClose.Size = new System.Drawing.Size(28, 28);
            this.tsbFindResultPanelOpenClose.Text = "Open\\\\Close Find Results Panel";
            this.tsbFindResultPanelOpenClose.Click += new System.EventHandler(this.tsbFindResultPanelOpenClose_Click);
            // 
            // tsbDown
            // 
            this.tsbDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDown.Image = global::NSqlTools.UI.Properties.Resources.Down;
            this.tsbDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDown.Name = "tsbDown";
            this.tsbDown.Size = new System.Drawing.Size(28, 28);
            this.tsbDown.Text = "Find next";
            this.tsbDown.Click += new System.EventHandler(this.tsbDown_Click);
            // 
            // tsbUp
            // 
            this.tsbUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUp.Image = global::NSqlTools.UI.Properties.Resources.Up;
            this.tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUp.Name = "tsbUp";
            this.tsbUp.Size = new System.Drawing.Size(28, 28);
            this.tsbUp.Text = "Find previous";
            this.tsbUp.Click += new System.EventHandler(this.tsbUp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // cbCompareType
            // 
            this.cbCompareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompareType.Name = "cbCompareType";
            this.cbCompareType.Size = new System.Drawing.Size(121, 31);
            this.cbCompareType.Visible = false;
            this.cbCompareType.SelectedIndexChanged += new System.EventHandler(this.cbCompareType_SelectedIndexChanged);
            // 
            // pnlFindResultBody
            // 
            this.pnlFindResultBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFindResultBody.Location = new System.Drawing.Point(0, 0);
            this.pnlFindResultBody.Name = "pnlFindResultBody";
            this.pnlFindResultBody.Size = new System.Drawing.Size(806, 235);
            this.pnlFindResultBody.TabIndex = 1;
            // 
            // gbSqlNotePad
            // 
            this.gbSqlNotePad.Controls.Add(this.panel2);
            this.gbSqlNotePad.Controls.Add(this.pnlStatus);
            this.gbSqlNotePad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSqlNotePad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSqlNotePad.Location = new System.Drawing.Point(0, 0);
            this.gbSqlNotePad.Name = "gbSqlNotePad";
            this.gbSqlNotePad.Size = new System.Drawing.Size(812, 682);
            this.gbSqlNotePad.TabIndex = 14;
            this.gbSqlNotePad.TabStop = false;
            this.gbSqlNotePad.Text = "Sql Script";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.scoSqlNotepad);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(806, 641);
            this.panel2.TabIndex = 17;
            // 
            // scoSqlNotepad
            // 
            this.scoSqlNotepad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoSqlNotepad.Location = new System.Drawing.Point(0, 0);
            this.scoSqlNotepad.Name = "scoSqlNotepad";
            this.scoSqlNotepad.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scoSqlNotepad.Panel1
            // 
            this.scoSqlNotepad.Panel1.Controls.Add(this.pnlBody);
            // 
            // scoSqlNotepad.Panel2
            // 
            this.scoSqlNotepad.Panel2.Controls.Add(this.pnlFindResultBody);
            this.scoSqlNotepad.Size = new System.Drawing.Size(806, 641);
            this.scoSqlNotepad.SplitterDistance = 402;
            this.scoSqlNotepad.TabIndex = 14;
            // 
            // pnlStatus
            // 
            this.pnlStatus.AutoSize = true;
            this.pnlStatus.Controls.Add(this.ssStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(3, 657);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(806, 22);
            this.pnlStatus.TabIndex = 16;
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.ssStatus.Location = new System.Drawing.Point(0, 0);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(806, 22);
            this.ssStatus.TabIndex = 16;
            this.ssStatus.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(10, 17);
            this.lblStatus.Text = " ";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Text files (*.txt)|*.txt|Sql files (*.sql)|*.sql|All files (*.*)|*.*";
            // 
            // ucSqlNotePad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSqlNotePad);
            this.Name = "ucSqlNotePad";
            this.Size = new System.Drawing.Size(812, 682);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlBodyContainer.ResumeLayout(false);
            this.pnlScQueryContainer.ResumeLayout(false);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.gbSqlNotePad.ResumeLayout(false);
            this.gbSqlNotePad.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.scoSqlNotepad.Panel1.ResumeLayout(false);
            this.scoSqlNotepad.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scoSqlNotepad)).EndInit();
            this.scoSqlNotepad.ResumeLayout(false);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.ResumeLayout(false);

		}

		private void TsbWrap_Click1(object sender, System.EventArgs e)
		{
			throw new System.NotImplementedException();
		}

		private void TsbWrap_Click(object sender, System.EventArgs e)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		private System.Windows.Forms.Panel pnlBody;
		private System.Windows.Forms.Panel pnlBodyContainer;
		private System.Windows.Forms.Panel pnlScQueryContainer;
		public ScintillaNET.Scintilla scSqlQuery;
		private System.Windows.Forms.Panel pnlFindResultBody;
		private System.Windows.Forms.ToolStrip tsMenu;
		private System.Windows.Forms.ToolStripButton tsbWrap;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbFind;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbDown;
		private System.Windows.Forms.ToolStripButton tsbUp;
		private System.Windows.Forms.GroupBox gbSqlNotePad;
		private System.Windows.Forms.ToolStripTextBox txtFind;
		private System.Windows.Forms.ToolStripButton tsbOpenFindDialog;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripButton tsbCopy;
		private System.Windows.Forms.ToolStripButton tsbFullScreen;
		public System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Panel pnlStatus;
		private System.Windows.Forms.StatusStrip ssStatus;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.SplitContainer scoSqlNotepad;
		private System.Windows.Forms.ToolStripButton tsbFindResultPanelOpenClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsbBiggerChars;
		private System.Windows.Forms.ToolStripButton tsbSmallerChars;
		private System.Windows.Forms.ToolStripButton tsbCaseSensitive;
		public System.Windows.Forms.ToolStripComboBox cbCompareType;
		private System.Windows.Forms.ToolStripButton tsbFormat;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	}
}
