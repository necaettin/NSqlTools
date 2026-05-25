using NSqlTools.UI.UserControls;

namespace NSqlTools.UI.Pages
{
	partial class frmNotePadCompareFullScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotePadCompareFullScreen));
			this.ucNotePadCompareControl = new ucNotePadCompare();
			this.SuspendLayout();
			// 
			// ucNotePadCompare
			// 
			this.ucNotePadCompareControl.DisplayFullScreen = false;
			this.ucNotePadCompareControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucNotePadCompareControl.Location = new System.Drawing.Point(0, 0);
			this.ucNotePadCompareControl.MainForm = null;
			this.ucNotePadCompareControl.Name = "ucNotePadCompare";
			this.ucNotePadCompareControl.ParentTabPage = null;
			this.ucNotePadCompareControl.Size = new System.Drawing.Size(800, 450);
			this.ucNotePadCompareControl.TabIndex = 0;
			// 
			// frmNotePadCompareFullScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ucNotePadCompareControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "frmNotePadCompareFullScreen";
			this.Text = NSqlTools.Types.Properties.CommonResource.NotePadCompare;
			this.ResumeLayout(false);

		}

		#endregion

		private UserControls.ucNotePadCompare ucNotePadCompareControl;
	}
}