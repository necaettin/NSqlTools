using NSqlTools.UI.UserControls;

namespace NSqlTools.UI.Pages
{
	partial class frmNotePadFullScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotePadFullScreen));
			this.ucSqlNotePadControl = new NSqlTools.UI.UserControls.ucSqlNotePad();
			this.SuspendLayout();
			// 
			// ucSqlNotePadControl
			// 
			this.ucSqlNotePadControl.CaseSensitive = false;
			this.ucSqlNotePadControl.CompareTypeVisible = false;
			this.ucSqlNotePadControl.DBObjectContract = null;
			this.ucSqlNotePadControl.DBObjectKeywordList = null;
			this.ucSqlNotePadControl.DisplayFullScreen = false;
			this.ucSqlNotePadControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucSqlNotePadControl.FontSize = 12;
			this.ucSqlNotePadControl.Location = new System.Drawing.Point(0, 0);
			this.ucSqlNotePadControl.MainForm = null;
			this.ucSqlNotePadControl.Name = "ucSqlNotePadControl";
			this.ucSqlNotePadControl.ParentTabPage = null;
			this.ucSqlNotePadControl.SchemaKeywordList = null;
			this.ucSqlNotePadControl.scoSqlNotepadPanel2Collapsed = true;
			this.ucSqlNotePadControl.SearchKeyword = "";
			this.ucSqlNotePadControl.Size = new System.Drawing.Size(800, 450);
			this.ucSqlNotePadControl.TabIndex = 0;
			this.ucSqlNotePadControl.Title = "Sql Script";
			// 
			// frmNotePadFullScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ucSqlNotePadControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "frmNotePadFullScreen";
			this.Text = "Note Pad";
			this.ResumeLayout(false);

		}

		#endregion

		private ucSqlNotePad ucSqlNotePadControl;
	}
}