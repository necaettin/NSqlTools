namespace NSqlTools.UI.Pages
{
	partial class frmSqlIntellisenseTester
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
			this.scSqlQuery = new ScintillaNET.Scintilla();
			this.SuspendLayout();
			// 
			// scSqlQuery
			// 
			this.scSqlQuery.AutoCMaxHeight = 9;
			this.scSqlQuery.CaretLineBackColor = System.Drawing.Color.AntiqueWhite;
			this.scSqlQuery.CaretLineVisible = true;
			this.scSqlQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scSqlQuery.Lexer = ScintillaNET.Lexer.Sql;
			this.scSqlQuery.Location = new System.Drawing.Point(0, 0);
			this.scSqlQuery.Name = "scSqlQuery";
			this.scSqlQuery.ScrollWidth = 1;
			this.scSqlQuery.Size = new System.Drawing.Size(800, 450);
			this.scSqlQuery.TabIndex = 13;
			// 
			// frmSqlIntellisenseTester
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.scSqlQuery);
			this.Name = "frmSqlIntellisenseTester";
			this.Text = "frmSqlIntellisenseTester";
			this.ResumeLayout(false);

		}

		#endregion

		public ScintillaNET.Scintilla scSqlQuery;
	}
}