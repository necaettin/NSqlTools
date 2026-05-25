using ScintillaNET;

namespace ScintillaDiff
{
    partial class ScintillaDiffControl
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.scintillaOne = new ScintillaDiff.ScrollSyncScintilla();
            this.diffMapPanel = new ScintillaDiff.DiffMapPanel();
            this.scintillaTwo = new ScintillaDiff.ScrollSyncScintilla();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.scintillaOne, 0, 0);
            this.tlpMain.Controls.Add(this.diffMapPanel, 1, 0);
            this.tlpMain.Controls.Add(this.scintillaTwo, 2, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Size = new System.Drawing.Size(918, 492);
            this.tlpMain.TabIndex = 0;
            this.tlpMain.Padding = new System.Windows.Forms.Padding(0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            // 
            // scintillaOne
            // 
            this.scintillaOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaOne.Location = new System.Drawing.Point(0, 0);
            this.scintillaOne.Margin = new System.Windows.Forms.Padding(0);
            this.scintillaOne.Name = "scintillaOne";
            this.scintillaOne.ScrollSync = this.scintillaTwo;
            this.scintillaOne.Size = new System.Drawing.Size(452, 492);
            this.scintillaOne.TabIndex = 0;
            this.scintillaOne.TextChanged += new System.EventHandler(this.Scintilla_TextChanged);
            // 
            // diffMapPanel
            // 
            this.diffMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diffMapPanel.Location = new System.Drawing.Point(452, 0);
            this.diffMapPanel.Margin = new System.Windows.Forms.Padding(0);
            this.diffMapPanel.Name = "diffMapPanel";
            this.diffMapPanel.Size = new System.Drawing.Size(14, 492);
            this.diffMapPanel.TabIndex = 1;
            // 
            // scintillaTwo
            // 
            this.scintillaTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaTwo.Location = new System.Drawing.Point(466, 0);
            this.scintillaTwo.Margin = new System.Windows.Forms.Padding(0);
            this.scintillaTwo.Name = "scintillaTwo";
            this.scintillaTwo.ScrollSync = this.scintillaOne;
            this.scintillaTwo.Size = new System.Drawing.Size(452, 492);
            this.scintillaTwo.TabIndex = 2;
            this.scintillaTwo.TextChanged += new System.EventHandler(this.Scintilla_TextChanged);
            // 
            // ScintillaDiffControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ScintillaDiffControl";
            this.Size = new System.Drawing.Size(918, 492);
            this.SizeChanged += new System.EventHandler(this.ScintillaDiffer_SizeChanged);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private ScrollSyncScintilla scintillaOne;
        private DiffMapPanel diffMapPanel;
        private ScrollSyncScintilla scintillaTwo;
    }
}
