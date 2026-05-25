using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSqlTools.UI.UserControls
{
	public partial class ucQueryResult : BaseUserControl
	{
		#region Constructors
		public ucQueryResult()
		{
			InitializeComponent();
		}
		#endregion

		#region Properties
		public DataTable DataSource
		{
			get
			{
				return dgvQueryResult.DataSource as DataTable;
			}
			set
			{
				dgvQueryResult.AutoGenerateColumns = true;

				if (value == null)
				{
					dgvQueryResult.DataSource = null;
					lblStatus.Text = string.Format(CommonResource.XRows, 0);
					return;
				}

				// DataTable'ı her zamanki gibi bağla
				dgvQueryResult.BindDataTable(value);

				// Tüm kolonları TextBoxCell olacak şekilde normalize et
				NormalizeAllColumnsToTextBox();

				// timestamp/rowversion (byte[]) kolonları için gösterim düzeltmesi
				HookTimestampFormatting();

				lblStatus.Text = string.Format(CommonResource.XRows, value.Rows.Count);
			}
		}
		#endregion

		private void HookTimestampFormatting()
		{
			// Tek bir kez bağlanması yeterli
			dgvQueryResult.CellFormatting -= dgvQueryResult_CellFormatting;
			dgvQueryResult.CellFormatting += dgvQueryResult_CellFormatting;
		}

		private void dgvQueryResult_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;

			// Sadece gerçekten byte[] olan hücreleri ele al
			var bytes = e.Value as byte[];
			if (bytes == null || bytes.Length == 0)
				return;

			e.Value = "0x" + BitConverter.ToString(bytes).Replace("-", string.Empty);
			e.FormattingApplied = true;
		}

		private void NormalizeAllColumnsToTextBox()
		{
			for (int i = 0; i < dgvQueryResult.Columns.Count; i++)
			{
				var col = dgvQueryResult.Columns[i];

				// CheckBox kolonları checkbox olarak kalsın
				if (col is System.Windows.Forms.DataGridViewCheckBoxColumn)
					continue;

				// Image kolonlarını TextBoxColumn ile değiştir
				if (col is System.Windows.Forms.DataGridViewImageColumn)
				{
					var imgCol = (System.Windows.Forms.DataGridViewImageColumn)col;

					var textCol = new System.Windows.Forms.DataGridViewTextBoxColumn
					{
						Name = imgCol.Name,
						DataPropertyName = imgCol.DataPropertyName,
						HeaderText = imgCol.HeaderText,
						AutoSizeMode = imgCol.AutoSizeMode,
						Visible = imgCol.Visible,
						ReadOnly = imgCol.ReadOnly,
						Width = imgCol.Width,
						FillWeight = imgCol.FillWeight
					};

					dgvQueryResult.Columns.RemoveAt(i);
					dgvQueryResult.Columns.Insert(i, textCol);
					col = textCol;
				}

				// Geri kalan tüm kolonları text tabanlı yap
				col.ValueType = typeof(object);
				if (!(col.CellTemplate is System.Windows.Forms.DataGridViewTextBoxCell))
				{
					col.CellTemplate = new System.Windows.Forms.DataGridViewTextBoxCell();
				}
			}
		}
	}
}
