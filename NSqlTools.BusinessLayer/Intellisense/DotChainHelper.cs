using NSqlTools.Types.IntellisenseContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSqlTools.BusinessLayer.Intellisense
{
	internal static class DotChainHelper
	{
		public static bool ShouldSkipDotChainHandling(string upToCaretLower)
		{
			bool insideInsertColumns = upToCaretLower.Contains(SqlKeywords.Insert) && 
			                            upToCaretLower.Contains(SqlKeywords.Into) && 
			                            upToCaretLower.Contains(SqlKeywords.OpenParen.ToString());
			bool afterJoin = upToCaretLower.Contains(SqlKeywords.Join) && 
			                 upToCaretLower.LastIndexOf(SqlKeywords.Join, StringComparison.Ordinal) < upToCaretLower.Length;
			
			return upToCaretLower.Contains(SqlKeywords.Where) || 
			       upToCaretLower.Contains(SqlKeywords.Set) || 
			       insideInsertColumns || 
			       afterJoin;
		}

		private static IEnumerable<string> ResolveFromVariants(
			string resolved, 
			Dictionary<string, IntellisenseTableContract> fullMeta)
		{
			var parts = resolved.Split(SqlKeywords.Dot);
			var variants = new List<string>();
			
			if (parts.Length == 3) 
				variants.Add($"{parts[1]}.{parts[2]}");
			if (parts.Length >= 1) 
				variants.Add(parts[parts.Length - 1]);

			foreach (var variant in variants)
			{
				if (fullMeta.TryGetValue(variant, out var metaVar) && metaVar.ColumnList != null)
					return metaVar.ColumnList.Select(c => c.ColumnName);
			}

			return null;
		}

		private static bool IsIdentifierChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_' || c == ']' || c == '[';
		}

		private const string InternalTablePrefix = "__INTERNAL_TABLE__";
	}
}
