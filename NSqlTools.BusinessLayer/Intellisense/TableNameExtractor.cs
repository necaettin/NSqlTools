using System.Collections.Generic;
using System.Linq;

namespace NSqlTools.BusinessLayer.Intellisense
{
	internal static class TableNameExtractor
	{
		public static string ExtractUpdateTableName(string segmentText)
		{
			if (string.IsNullOrEmpty(segmentText)) return string.Empty;

			var lower = segmentText.ToLowerInvariant();
			int updateIdx = lower.IndexOf(SqlKeywords.Update, System.StringComparison.Ordinal);
			if (updateIdx < 0) return string.Empty;

			int setIdx = FindSetKeywordIndex(lower);
			if (setIdx < 0) return string.Empty;

			int endPos = setIdx > updateIdx ? setIdx : segmentText.Length;
			string tableName = segmentText.Substring(updateIdx + 6, endPos - updateIdx - 6).Trim();
			
			return ExtractFirstToken(tableName, SqlKeywords.QuoteChars);
		}

		public static string ExtractInsertTableName(string segmentText)
		{
			if (string.IsNullOrEmpty(segmentText)) return string.Empty;

			var lower = segmentText.ToLowerInvariant();
			int insertIdx = lower.IndexOf(SqlKeywords.Insert, System.StringComparison.Ordinal);
			if (insertIdx < 0) return string.Empty;

			int intoIdx = lower.IndexOf($" {SqlKeywords.Into} ", insertIdx, System.StringComparison.Ordinal);
			if (intoIdx < 0) return string.Empty;

			int startIdx = intoIdx + 6;
			int endIdx = FindInsertTableEnd(lower, startIdx, segmentText.Length);
			if (startIdx >= endIdx) return string.Empty;

			string tableName = segmentText.Substring(startIdx, endIdx - startIdx).Trim();
			return ExtractFirstToken(tableName, SqlKeywords.AllQuoteChars);
		}

		public static List<string> BuildSearchKeys(string tableName)
		{
			var searchKeys = new List<string> { tableName };
			if (tableName.Contains(SqlKeywords.Dot))
			{
				var parts = tableName.Split(SqlKeywords.Dot);
				if (parts.Length >= 2)
				{
					searchKeys.Add($"{parts[parts.Length - 2]}.{parts[parts.Length - 1]}");
					searchKeys.Add(parts[parts.Length - 1]);
				}
			}
			return searchKeys;
		}

		private static int FindSetKeywordIndex(string lowerText)
		{
			int setIdx = lowerText.IndexOf($" {SqlKeywords.Set} ", System.StringComparison.Ordinal);
			if (setIdx >= 0) return setIdx;

			if (lowerText.EndsWith($" {SqlKeywords.Set}")) 
				return lowerText.Length - 4;

			var lines = lowerText.Split(new[] { SqlKeywords.Newline, SqlKeywords.CarriageReturn }, 
			                             System.StringSplitOptions.RemoveEmptyEntries);
			int currentPos = 0;
			foreach (var line in lines)
			{
				var trimmed = line.Trim();
				if (trimmed.StartsWith($"{SqlKeywords.Set} ") || trimmed == SqlKeywords.Set)
					return currentPos + line.IndexOf(SqlKeywords.Set, System.StringComparison.Ordinal);
				currentPos += line.Length + 1;
			}

			return -1;
		}

		private static int FindInsertTableEnd(string lower, int startIdx, int defaultEnd)
		{
			int endIdx = defaultEnd;
			int openParen = lower.IndexOf(SqlKeywords.OpenParen, startIdx);
			int valuesIdx = lower.IndexOf(SqlKeywords.Values, startIdx, System.StringComparison.Ordinal);

			if (openParen >= 0 && openParen < endIdx) endIdx = openParen;
			if (valuesIdx >= 0 && valuesIdx < endIdx) endIdx = valuesIdx;

			return endIdx;
		}

		private static string ExtractFirstToken(string tableName, char[] trimChars)
		{
			var tokens = tableName.Split(SqlKeywords.TokenDelimiters, System.StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length == 0) return string.Empty;

			var cleanTableName = tokens[0].Trim(trimChars);
			while (cleanTableName.Length > 0 && 
			       !char.IsLetterOrDigit(cleanTableName[cleanTableName.Length - 1]) && 
			       cleanTableName[cleanTableName.Length - 1] != '_' &&
			       cleanTableName[cleanTableName.Length - 1] != SqlKeywords.Dot)
			{
				cleanTableName = cleanTableName.Substring(0, cleanTableName.Length - 1);
			}

			return cleanTableName;
		}
	}
}
