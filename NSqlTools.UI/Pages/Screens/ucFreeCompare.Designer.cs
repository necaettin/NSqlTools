namespace NSqlTools.UI.Pages
{
	partial class ucFreeTextCompare
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
			this.ucFreeNotePadCompare = new NSqlTools.UI.UserControls.ucNotePadCompare();
			this.SuspendLayout();
			// 
			// ucFreeNotePadCompare
			// 
			this.ucFreeNotePadCompare.CaseSensitive = false;
			this.ucFreeNotePadCompare.CompareTypeVisible = true;
			this.ucFreeNotePadCompare.DisplayFullScreen = false;
			this.ucFreeNotePadCompare.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucFreeNotePadCompare.FindResultCollapsed = true;
			this.ucFreeNotePadCompare.FontSize = 12;
			this.ucFreeNotePadCompare.Location = new System.Drawing.Point(0, 0);
			this.ucFreeNotePadCompare.MainForm = null;
			this.ucFreeNotePadCompare.Name = "ucFreeNotePadCompare";
			this.ucFreeNotePadCompare.ParentTabPage = null;
			this.ucFreeNotePadCompare.Size = new System.Drawing.Size(995, 561);
			this.ucFreeNotePadCompare.SourceDBObjectName = null;
			this.ucFreeNotePadCompare.SourceSchemaName = null;
			this.ucFreeNotePadCompare.StatusBarPanelIsVisible = false;
			this.ucFreeNotePadCompare.TabIndex = 0;
			this.ucFreeNotePadCompare.TargetDBObjectName = null;
			this.ucFreeNotePadCompare.TargetSchemaName = null;
			this.ucFreeNotePadCompare.Load += new System.EventHandler(this.ucFreeNotePadCompare_Load);
			// 
			// ucFreeTextCompare
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ucFreeNotePadCompare);
			this.Name = "ucFreeTextCompare";
			this.Size = new System.Drawing.Size(995, 561);
			this.ResumeLayout(false);

		}

		#endregion

		private UserControls.ucNotePadCompare ucFreeNotePadCompare;
	}
}
