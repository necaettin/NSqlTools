namespace NSqlTools.UI.Pages
{
	partial class ucTextViewer
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
			this.ucNotePadControl = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.SuspendLayout();
			// 
			// ucNotePadControl
			// 
			this.ucNotePadControl.CaseSensitive = false;
			this.ucNotePadControl.CompareTypeVisible = true;
			this.ucNotePadControl.DBObjectContract = null;
			this.ucNotePadControl.DBObjectKeywordList = null;
			this.ucNotePadControl.DisplayFullScreen = true;
			this.ucNotePadControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucNotePadControl.findAllResultsPanel = null;
			this.ucNotePadControl.FontSize = 12;
			this.ucNotePadControl.Location = new System.Drawing.Point(0, 0);
			this.ucNotePadControl.MainForm = null;
			this.ucNotePadControl.Name = "ucNotePadControl";
			this.ucNotePadControl.ParentTabPage = null;
			this.ucNotePadControl.SchemaKeywordList = null;
			this.ucNotePadControl.scoSqlNotepadPanel2Collapsed = true;
			this.ucNotePadControl.SearchKeyword = "";
			this.ucNotePadControl.Size = new System.Drawing.Size(995, 561);
			this.ucNotePadControl.TabIndex = 0;
			this.ucNotePadControl.Title = "";
			// 
			// ucTextViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ucNotePadControl);
			this.Name = "ucTextViewer";
			this.Size = new System.Drawing.Size(995, 561);
			this.ResumeLayout(false);

		}

		#endregion

		private UserControls.ucSqlNotePad ucNotePadControl;
	}
}
