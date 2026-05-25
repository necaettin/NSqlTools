using System;
using System.Collections.Generic;
using System.Linq;

namespace NSqlTools.Lib.Helpers
{
	public static class AdvancedDataGridViewHelper
	{
		// Sadece basit "Column LIKE '%value%'" ve "Column = value" senaryolarını destekler.
		// İhtiyaca göre genişletilebilir.
		public static IEnumerable<T> ApplyFilter<T>(
			IEnumerable<T> source,
			string filterString,
			Func<string, string, StringComparison, Func<T, bool>> perColumnPredicateBuilder,
			StringComparison comparison = StringComparison.OrdinalIgnoreCase)
		{
			if (source == null)
				return Enumerable.Empty<T>();

			if (string.IsNullOrWhiteSpace(filterString) || perColumnPredicateBuilder == null)
				return source;

			// ADGV tipik format: "([Name] LIKE '%ABC%') AND ([DataSource] LIKE '%XYZ%')"
			// Burada çok basit bir parçalama: AND ile ayır, her birini tek şart olarak işle.
			var parts = filterString
				.Split(new[] { " AND " }, StringSplitOptions.RemoveEmptyEntries)
				.Select(p => p.Trim(' ', '(', ')'))
				.ToList();

			var predicates = new List<Func<T, bool>>();

			foreach (var part in parts)
			{
				ParseFilterPart(part, comparison, perColumnPredicateBuilder, predicates);
			}

			if (predicates.Count == 0)
				return source;

			return source.Where(item => predicates.All(p => p(item)));
		}

		private static void ParseFilterPart<T>(
			string part,
			StringComparison comparison,
			Func<string, string, StringComparison, Func<T, bool>> perColumnPredicateBuilder,
			List<Func<T, bool>> predicates)
		{
			if (string.IsNullOrWhiteSpace(part))
				return;

			// Örnek part:
			// [Name] LIKE '%ABC%'
			// [DataSource] = 'LOCALHOST'
			int colStart = part.IndexOf('[');
			int colEnd = part.IndexOf(']', colStart + 1);
			if (colStart < 0 || colEnd <= colStart)
				return;

			var column = part.Substring(colStart + 1, colEnd - colStart - 1);

			// Operatör
			string op = null;
			if (part.IndexOf("LIKE", StringComparison.OrdinalIgnoreCase) >= 0)
				op = "LIKE";
			else if (part.IndexOf("=", StringComparison.OrdinalIgnoreCase) >= 0)
				op = "=";

			if (op == null)
				return;

			// Değeri bul
			int opIndex = part.IndexOf(op, StringComparison.OrdinalIgnoreCase);
			var afterOp = part.Substring(opIndex + op.Length).Trim();

			// Expect: 'value', '%value%', 'value%'
			int q1 = afterOp.IndexOf('\'');
			int q2 = afterOp.IndexOf('\'', q1 + 1);
			if (q1 < 0 || q2 <= q1)
				return;

			var rawValue = afterOp.Substring(q1 + 1, q2 - q1 - 1);
			string value;

			if (op.Equals("LIKE", StringComparison.OrdinalIgnoreCase))
			{
				// '%ABC%' → ABC, 'ABC%' → ABC, '%ABC' → ABC
				value = rawValue.Trim('%');
			}
			else
			{
				value = rawValue;
			}

			if (string.IsNullOrEmpty(column))
				return;

			var predicate = perColumnPredicateBuilder(column, value, comparison);
			if (predicate != null)
				predicates.Add(predicate);
		}

		public static IEnumerable<T> ApplySort<T>(
			IEnumerable<T> source,
			string sortString)
		{
			if (source == null)
				return Enumerable.Empty<T>();

			if (string.IsNullOrWhiteSpace(sortString))
				return source;

			// Örnek: "Name ASC" veya "InitialCatalog DESC"
			var parts = sortString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			IOrderedEnumerable<T> ordered = null;

			foreach (var part in parts)
			{
				var seg = part.Trim();
				if (string.IsNullOrEmpty(seg))
					continue;

				var tokens = seg.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length == 0)
					continue;

				var column = tokens[0];
				bool desc = tokens.Length > 1 && tokens[1].Equals("DESC", StringComparison.OrdinalIgnoreCase);

				Func<T, object> keySelector = item => GetPropertyValue(item, column);

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

		private static object GetPropertyValue<T>(T item, string propertyName)
		{
			if (item == null || string.IsNullOrEmpty(propertyName))
				return null;

			var prop = typeof(T).GetProperty(propertyName);
			return prop != null ? prop.GetValue(item, null) : null;
		}
	}
} 