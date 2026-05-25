using NSqlTools.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Zuby.ADGV;

namespace NSqlTools.Lib.Controls
{
	public class NAdvancedDataGridView : AdvancedDataGridView
	{
		private readonly BindingSource _bindingSource = new BindingSource();
		private readonly Dictionary<string, bool> _columnSortDescending =
			new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

		private DataTable _boundDataTable;

		[Browsable(false)]
		public BindingSource BindingSource => _bindingSource;

		[Browsable(false)]
		public object ListBinder => _listBinder;
		private object _listBinder;

		public NAdvancedDataGridView()
		{
			FilterAndSortEnabled = true;
			AutoGenerateColumns = false;
			EnableHeadersVisualStyles = false;

			ColumnHeaderMouseClick += OnColumnHeaderMouseClick;
		}

		public void BindList<T>(IList<T> list) where T : class
		{
			_listBinder = new AdvancedDataGridViewListBinder<T>(
				this,
				_bindingSource,
				list ?? new List<T>());
		}

		// DataTable için özel bind
		public void BindDataTable(DataTable table)
		{
			_boundDataTable = table;

			if (table == null)
			{
				_listBinder = null;
				_bindingSource.DataSource = null;
				DataSource = null;
				return;
			}

			// DataTable'ı doğrudan BindingSource'a bağla
			_bindingSource.DataSource = table;
			DataSource = _bindingSource;

			// AllowUserToAddRows'u etkinleştir
			AllowUserToAddRows = true;
			AllowUserToDeleteRows = true;
		}

		public DataTable GetBoundDataTable() => _boundDataTable;

		private void OnColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			var col = Columns[e.ColumnIndex];
			if (col.SortMode == DataGridViewColumnSortMode.NotSortable)
				return;

			var propertyName = col.DataPropertyName;
			if (string.IsNullOrEmpty(propertyName))
				return;

			bool desc;
			if (!_columnSortDescending.TryGetValue(propertyName, out desc))
				desc = false;   // ilk tıklama ASC
			else
				desc = !desc;   // toggle

			_columnSortDescending[propertyName] = desc;

			string dir = desc ? "DESC" : "ASC";
			string sortExpression = "[" + propertyName + "] " + dir;

			// Binder'a sort uygula
			var binderType = _listBinder?.GetType();
			if (binderType != null)
			{
				var method = binderType.GetMethod("ApplySort", new[] { typeof(string) });
				if (method != null)
					method.Invoke(_listBinder, new object[] { sortExpression });
			}

			// Binder DataSource'u yenilemiş olabilir; Columns koleksiyonunu yeniden kullan
			foreach (DataGridViewColumn c in Columns)
			{
				var headerCell = c.HeaderCell;
				if (headerCell == null || headerCell.DataGridView != this)
					continue; // detach olmuş hücreyi atla

				headerCell.SortGlyphDirection = SortOrder.None;
			}

			var clickedCol = Columns.Cast<DataGridViewColumn>()
				.FirstOrDefault(c => c.DataPropertyName == propertyName);

			if (clickedCol != null &&
				clickedCol.HeaderCell != null &&
				clickedCol.HeaderCell.DataGridView == this)
			{
				clickedCol.HeaderCell.SortGlyphDirection = desc ? SortOrder.Descending : SortOrder.Ascending;
				InvalidateCell(clickedCol.HeaderCell);
			}
		}
	}
}
