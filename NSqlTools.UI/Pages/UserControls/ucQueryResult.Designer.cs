using NSqlTools.Lib.Controls;

namespace NSqlTools.UI.UserControls
{
	partial class ucQueryResult
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvQueryResult = new NSqlTools.Lib.Controls.NAdvancedDataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlQueryResult = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.dgvQueryResult)).BeginInit();
			this.panel1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.pnlQueryResult.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvQueryResult
			// 
			this.dgvQueryResult.AllowUserToAddRows = false;
			this.dgvQueryResult.AllowUserToDeleteRows = false;
			this.dgvQueryResult.AllowUserToOrderColumns = true;
			this.dgvQueryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvQueryResult.DefaultCellStyle = dataGridViewCellStyle1;
			this.dgvQueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvQueryResult.EnableHeadersVisualStyles = false;
			this.dgvQueryResult.FilterAndSortEnabled = true;
			this.dgvQueryResult.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvQueryResult.Location = new System.Drawing.Point(0, 0);
			this.dgvQueryResult.MaxFilterButtonImageHeight = 23;
			this.dgvQueryResult.Name = "dgvQueryResult";
			this.dgvQueryResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dgvQueryResult.RowHeadersWidth = 51;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvQueryResult.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvQueryResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvQueryResult.Size = new System.Drawing.Size(525, 327);
			this.dgvQueryResult.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.dgvQueryResult.TabIndex = 3;
			this.dgvQueryResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvQueryResult_CellFormatting);
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.statusStrip1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 327);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(525, 22);
			this.panel1.TabIndex = 3;
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(525, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 17);
			this.lblStatus.Text = " ";
			// 
			// pnlQueryResult
			// 
			this.pnlQueryResult.Controls.Add(this.dgvQueryResult);
			this.pnlQueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlQueryResult.Location = new System.Drawing.Point(0, 0);
			this.pnlQueryResult.Name = "pnlQueryResult";
			this.pnlQueryResult.Size = new System.Drawing.Size(525, 327);
			this.pnlQueryResult.TabIndex = 4;
			// 
			// ucQueryResult
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.pnlQueryResult);
			this.Controls.Add(this.panel1);
			this.Name = "ucQueryResult";
			this.Size = new System.Drawing.Size(525, 349);
			((System.ComponentModel.ISupportInitialize)(this.dgvQueryResult)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.pnlQueryResult.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private NAdvancedDataGridView dgvQueryResult;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Panel pnlQueryResult;
	}
}
