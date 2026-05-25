using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Zuby.ADGV;

namespace NSqlTools.Lib.Helpers
{
	public class AdvancedDataGridViewListBinder<T> where T : class
	{
		private readonly BindingSource _bindingSource;
		private readonly AdvancedDataGridView _grid;
		private readonly IList<T> _original;
		private List<T> _filtered;

		public AdvancedDataGridViewListBinder(
			AdvancedDataGridView grid,
			BindingSource bindingSource,
			IList<T> source)
		{
			_grid = grid ?? throw new ArgumentNullException(nameof(grid));
			_bindingSource = bindingSource ?? throw new ArgumentNullException(nameof(bindingSource));
			_original = source ?? new List<T>();
			_filtered = _original.ToList();

			_grid.FilterStringChanged += Grid_FilterStringChanged;
			_grid.SortStringChanged += Grid_SortStringChanged;

			Rebind();
		}

		private void ReapplyFilterAndSort()
		{
			_filtered = ApplyFilterAndSort(_original, _grid.FilterString, _grid.SortString).ToList();
			_bindingSource.DataSource = new SortableBindingList<T>(_filtered);
			_grid.DataSource = _bindingSource;
		}

		public void Rebind()
		{
			ReapplyFilterAndSort();
		}

		private void Grid_FilterStringChanged(object sender, AdvancedDataGridView.FilterEventArgs e)
		{
			ReapplyFilterAndSort();
		}

		private void Grid_SortStringChanged(object sender, AdvancedDataGridView.SortEventArgs e)
		{
			ReapplyFilterAndSort();
		}

		private static IEnumerable<T> ApplyFilterAndSort(
			IEnumerable<T> source,
			string filterString,
			string sortString)
		{
			if (source == null)
				return Enumerable.Empty<T>();

			var query = source;

			// 1) Filter
			if (!string.IsNullOrWhiteSpace(filterString))
			{
				query = ApplyFilter(query, filterString);
			}

			// 2) Sort
			if (!string.IsNullOrWhiteSpace(sortString))
			{
				query = ApplySort(query, sortString);
			}

			return query;
		}

		private static IEnumerable<T> ApplyFilter(IEnumerable<T> source, string filterString)
		{
			// Çok genel bir parser yazmak istersen uzar; burada ilk ad?mda:
			// sadece tek kolon + LIKE/=/IN senaryolar?n? reflection ile destekleyebilirsin.
			// Örnek filter: "([Name] LIKE '%abc%')" veya "([IntegratedSecurity] = TRUE)"

			// Basit: "AND" ile böl, her parça için predicate üret, hepsini AND'le.
			var parts = filterString
				.Split(new[] { " AND " }, StringSplitOptions.RemoveEmptyEntries)
				.Select(p => p.Trim())
				.ToList();

			var predicates = new List<Func<T, bool>>();

			foreach (var part in parts)
			{
				var pred = BuildPredicateFromFilterPart(part);
				if (pred != null)
					predicates.Add(pred);
			}

			if (predicates.Count == 0)
				return source;

			return source.Where(item => predicates.All(p => p(item)));
		}

		private static Func<T, bool> BuildPredicateFromFilterPart(string part)
		{
			if (string.IsNullOrWhiteSpace(part))
				return null;

			// Kolon adı
			int colStart = part.IndexOf('[');
			int colEnd = part.IndexOf(']', colStart + 1);
			if (colStart < 0 || colEnd <= colStart)
				return null;

			var column = part.Substring(colStart + 1, colEnd - colStart - 1).Trim();
			if (string.IsNullOrEmpty(column))
				return null;

			// DataRowView senaryosu
			if (typeof(T) == typeof(DataRowView))
			{
				string upper = part.ToUpperInvariant();

				if (upper.Contains(" LIKE "))
				{
					int likeIdx = upper.IndexOf(" LIKE ", StringComparison.Ordinal);
					var after = part.Substring(likeIdx + 6).Trim();
					int q1 = after.IndexOf('\'');
					int q2 = after.IndexOf('\'', q1 + 1);
					if (q1 < 0 || q2 <= q1) return null;
					var raw = after.Substring(q1 + 1, q2 - q1 - 1);
					var val = raw.Trim('%');

					return item =>
					{
						var drv = item as DataRowView;
						if (drv == null) return false;
						if (!drv.Row.Table.Columns.Contains(column)) return false;
						var cell = drv.Row[column];
						if (cell == null || cell == DBNull.Value) return false;
						var s = cell.ToString();
						return s.IndexOf(val, StringComparison.OrdinalIgnoreCase) >= 0;
					};
				}

				if (upper.Contains(" IN "))
				{
					int inIdx = upper.IndexOf(" IN ", StringComparison.Ordinal);
					var after = part.Substring(inIdx + 4).Trim();
					int p1 = after.IndexOf('(');
					int p2 = after.IndexOf(')', p1 + 1);
					if (p1 < 0 || p2 <= p1) return null;
					var list = after.Substring(p1 + 1, p2 - p1 - 1)
						.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
						.Select(x => x.Trim().Trim('\''))
						.ToList();

					return item =>
					{
						var drv = item as DataRowView;
						if (drv == null) return false;
						if (!drv.Row.Table.Columns.Contains(column)) return false;
						var cell = drv.Row[column];
						if (cell == null || cell == DBNull.Value) return false;
						var s = cell.ToString();
						return list.Any(x => string.Equals(x, s, StringComparison.OrdinalIgnoreCase));
					};
				}

				if (upper.Contains("="))
				{
					var segments = part.Split(new[] { '=' }, 2);
					if (segments.Length != 2) return null;
					var right = segments[1].Trim().Trim('(', ')');

					string raw;
					if (right.StartsWith("'") && right.EndsWith("'"))
						raw = right.Substring(1, right.Length - 2);
					else
						raw = right;

					return item =>
					{
						var drv = item as DataRowView;
						if (drv == null) return false;
						if (!drv.Row.Table.Columns.Contains(column)) return false;
						var cell = drv.Row[column];
						if (cell == null && raw == null) return true;
						if (cell == null || raw == null) return false;
						return string.Equals(cell.ToString(), raw, StringComparison.OrdinalIgnoreCase);
					};
				}

				return null;
			}

			// Normal POCO / contract senaryosu (mevcut kodun)
			var prop = typeof(T).GetProperty(column, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (prop == null)
				return null;

			// Skip properties that don't implement IComparable
			var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
			if (!typeof(IComparable).IsAssignableFrom(propType) && propType != typeof(string))
				return null;

			string upperRest = part.ToUpperInvariant();
			// ... burada mevcut LIKE / IN / = blokların senin şu anki hali kalabilir,
			// sadece üstteki DataRowView bloğundan sonra gelmeli.
			// (Şu an dosyada duran kodu aynen bırak, sadece DataRowView bloğunu
			//  prop tanımının önüne eklemiş olacağız.)
			if (upperRest.Contains(" LIKE "))
			{
				// LIKE '%value%'
				int likeIdx = upperRest.IndexOf(" LIKE ", StringComparison.Ordinal);
				var after = part.Substring(likeIdx + 6).Trim();
				int q1 = after.IndexOf('\'');
				int q2 = after.IndexOf('\'', q1 + 1);
				if (q1 < 0 || q2 <= q1) return null;
				var raw = after.Substring(q1 + 1, q2 - q1 - 1);
				var val = raw.Trim('%');

				return item =>
				{
					var v = prop.GetValue(item, null);
					if (v == null) return false;
					var s = v.ToString();
					return s.IndexOf(val, StringComparison.OrdinalIgnoreCase) >= 0;
				};
			}

			if (upperRest.Contains(" IN "))
			{
				// IN (v1, v2, v3)
				int inIdx = upperRest.IndexOf(" IN ", StringComparison.Ordinal);
				var after = part.Substring(inIdx + 4).Trim();
				int p1 = after.IndexOf('(');
				int p2 = after.IndexOf(')', p1 + 1);
				if (p1 < 0 || p2 <= p1) return null;
				var list = after.Substring(p1 + 1, p2 - p1 - 1)
					.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(x => x.Trim().Trim('\''))
					.ToList();

				var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

				// string vs numeric vs bool hepsine generic yakla??m
				return item =>
				{
					var v = prop.GetValue(item, null);
					if (v == null) return false;
					var s = v.ToString();
					// string kar??la?t?rma
					if (targetType == typeof(string))
						return list.Any(x => string.Equals(x, s, StringComparison.OrdinalIgnoreCase));

					// numeric/bool/date türleri için parse denemesi
					return list.Any(x => string.Equals(x, s, StringComparison.OrdinalIgnoreCase));
				};
			}

			if (upperRest.Contains("="))
			{
				// Equals: [Col] = 'val' veya = TRUE / FALSE / 123
				var segments = part.Split(new[] { '=' }, 2);
				if (segments.Length != 2) return null;
				var right = segments[1].Trim().Trim('(', ')');

				string raw;
				if (right.StartsWith("'") && right.EndsWith("'"))
					raw = right.Substring(1, right.Length - 2);
				else
					raw = right;

				var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
				object constant;
				try
				{
					if (targetType == typeof(string))
						constant = raw;
					else if (targetType == typeof(bool))
						constant = bool.Parse(raw);
					else if (targetType.IsEnum)
						constant = Enum.Parse(targetType, raw, true);
					else
						constant = Convert.ChangeType(raw, targetType);
				}
				catch
				{
					constant = raw;
				}

				return item =>
				{
					var v = prop.GetValue(item, null);
					if (v == null && constant == null) return true;
					if (v == null || constant == null) return false;
					return string.Equals(v.ToString(), constant.ToString(), StringComparison.OrdinalIgnoreCase);
				};
			}

			return null;
		}

		private static IEnumerable<T> ApplySort(IEnumerable<T> source, string sortString)
		{
			// Örnek sortString: "Name ASC" veya "Name ASC, InitialCatalog DESC"
			var parts = sortString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(p => p.Trim())
				.Where(p => !string.IsNullOrEmpty(p))
				.ToList();

			IOrderedEnumerable<T> ordered = null;

			foreach (var part in parts)
			{
				var tokens = part.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length == 0) continue;

				var column = tokens[0].Trim('[', ']');
				bool desc = tokens.Length > 1 && tokens[1].Equals("DESC", StringComparison.OrdinalIgnoreCase);

				Func<T, object> keySelector;

				if (typeof(T) == typeof(DataRowView))
				{
					// DataRowView senaryosu (BindDataTable için)
					keySelector = item =>
					{
						var drv = item as DataRowView;
						if (drv == null) return null;
						// Kolon yoksa null; varsa ilgili hücre değeri
						return drv.Row.Table.Columns.Contains(column) ? drv.Row[column] : null;
					};
				}
				else
				{
					// Normal POCO/contract senaryosu
					var prop = typeof(T).GetProperty(column, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
					if (prop == null) continue;

					// Skip properties that don't implement IComparable
					var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
					if (!typeof(IComparable).IsAssignableFrom(propType) && propType != typeof(string))
						continue;

					keySelector = item => prop.GetValue(item, null);
				}

				if (ordered == null)
				{
					ordered = desc
						? source.OrderByDescending(keySelector)
						: source.OrderBy(keySelector);
				}
				else
				{
					ordered = desc
						? ordered.ThenByDescending(keySelector)
						: ordered.ThenBy(keySelector);
				}
			}

			return ordered ?? source;
		}

		public void ApplySort(string sortString)
		{
			// _grid.FilterString devrede kalabilir, önce filtre uygula
			var baseQuery = ApplyFilter(_original, _grid.FilterString);
			_filtered = ApplySort(baseQuery, sortString).ToList();
			_bindingSource.DataSource = new SortableBindingList<T>(_filtered);
			_grid.DataSource = _bindingSource;
		}
	}
}