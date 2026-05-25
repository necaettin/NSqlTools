using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NSqlTools.Lib.Controls
{
	public class DataGridViewProgressColumn : DataGridViewImageColumn
	{
		public DataGridViewProgressColumn()
		{
			CellTemplate = new DataGridViewProgressCell();
		}

		public override DataGridViewCell CellTemplate
		{
			get => base.CellTemplate;
			set
			{
				if (value != null &&
					!value.GetType().IsAssignableFrom(typeof(DataGridViewProgressCell)))
				{
					throw new InvalidCastException("Must be a DataGridViewProgressCell");
				}
				base.CellTemplate = value;
			}
		}


		[Browsable(true)]
		public Color ProgressBarColor
		{
			get
			{

				if (this.ProgressBarCellTemplate == null)
				{
					throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
				}
				return this.ProgressBarCellTemplate.ProgressBarColor;

			}
			set
			{

				if (this.ProgressBarCellTemplate == null)
				{
					throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
				}
				this.ProgressBarCellTemplate.ProgressBarColor = value;
				if (this.DataGridView != null)
				{
					DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
					int rowCount = dataGridViewRows.Count;
					for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
					{
						DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
						if (dataGridViewRow.Cells[this.Index] is DataGridViewProgressCell dataGridViewCell)
						{
							dataGridViewCell.SetProgressBarColor(rowIndex, value);
						}
					}
					this.DataGridView.InvalidateColumn(this.Index);
				}
			}
		}


		private DataGridViewProgressCell ProgressBarCellTemplate
		{
			get => (DataGridViewProgressCell)this.CellTemplate;
		}
	}
}
