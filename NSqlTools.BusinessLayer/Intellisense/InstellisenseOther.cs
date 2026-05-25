using NSqlTools.Types.IntellisenseContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSqlTools.BusinessLayer.Intellisense
{
	public static partial class SimpleSqlIntellisenseEngine
	{
		private static class VariableCollector
		{
			public static List<string> CollectVariables(string sql, int caret)
			{
				var variables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				var upToCaret = caret > 0 && caret <= sql.Length ? sql.Substring(0, caret) : string.Empty;
				var tokens = Tokenize(upToCaret);
				for (int i = 0; i < tokens.Count; i++)
				{
					if (tokens[i].Equals("declare", StringComparison.OrdinalIgnoreCase))
					{
						for (int j = i + 1; j < tokens.Count; j++)
						{
							var token = tokens[j];
							if (token.Equals("select", StringComparison.OrdinalIgnoreCase) || token.Equals("insert", StringComparison.OrdinalIgnoreCase) || token.Equals("update", StringComparison.OrdinalIgnoreCase) || token.Equals("delete", StringComparison.OrdinalIgnoreCase) || token.Equals("set", StringComparison.OrdinalIgnoreCase) || token.Equals("from", StringComparison.OrdinalIgnoreCase) || token.Equals("where", StringComparison.OrdinalIgnoreCase) || token.StartsWith(";")) break;
							if (token.StartsWith("@"))
							{
								var varName = token.Trim(',', ';', '=');
								if (!string.IsNullOrEmpty(varName)) variables.Add(varName);
							}
						}
					}
				}
				return variables.ToList();
			}
		}

		private static class SelectListColumnEnricher
		{
			public static List<string> CollectColumnsFromUpcomingTables(string segText, List<IntellisenseTableContract> metas, Dictionary<string, IntellisenseTableContract> fullMeta, int caretInSeg)
			{
				var lower = segText.ToLowerInvariant();
				int fromPosScan = lower.IndexOf(" from ", StringComparison.Ordinal);
				if (fromPosScan < 0 && lower.EndsWith(" from")) fromPosScan = lower.Length - 5;
				if (fromPosScan < 0 || caretInSeg > fromPosScan) return new List<string>();
				int startIdx = (lower.EndsWith(" from") && fromPosScan == lower.Length - 5) ? fromPosScan + 5 : fromPosScan + 6;
				if (startIdx > segText.Length) return new List<string>();
				var afterFromFull = segText.Substring(startIdx);
				var tablesFound = FallbackDetectTables(afterFromFull, metas);
				return tablesFound
					.Where(t => fullMeta.ContainsKey(t))
					.SelectMany(t => fullMeta[t].ColumnList != null ? fullMeta[t].ColumnList.Select(c => c.ColumnName) : Enumerable.Empty<string>())
					.Distinct(StringComparer.OrdinalIgnoreCase)
					.ToList();
			}
		}

		private static class ContextDetector
		{
			public static SqlContext Detect(string sql, int caret, ParsedEnvironment env, SegmentInfo segment)
			{
				if (env.ActiveStatement != null && caret <= env.ActiveStatement.StartOffset + env.ActiveStatement.FragmentLength)
				{
					var byScriptDom = ContextUtil.DetermineContext(env.ActiveStatement, caret);
					if (byScriptDom != SqlContext.Other) return byScriptDom;
				}

				var segText = segment.Text ?? string.Empty;
				var caretInSeg = Math.Max(0, caret - segment.Start);
				var upToCaretFull = caretInSeg <= segText.Length ? segText.Substring(0, caretInSeg) : segText;
				var upLower = upToCaretFull.ToLowerInvariant();
				int declIdx = upLower.LastIndexOf("declare", StringComparison.Ordinal);
				if (declIdx >= 0)
				{
					bool sameLine = upToCaretFull.LastIndexOf('\n') < declIdx && upToCaretFull.LastIndexOf('\r') < declIdx;
					bool beforeSemicolon = upToCaretFull.LastIndexOf(';') < declIdx;
					if ((sameLine || declIdx > upToCaretFull.LastIndexOf('\n')) && beforeSemicolon && caretInSeg > declIdx + 7)
						return SqlContext.Declare;
				}

				var segLower = segText.ToLowerInvariant();
				var upToCaret = caretInSeg <= segLower.Length ? segLower.Substring(0, caretInSeg) : segLower;

				if (segLower.Contains("insert"))
				{
					int insertIdx = segLower.IndexOf("insert", StringComparison.Ordinal);
					int intoIdx = segLower.IndexOf(" into ", insertIdx, StringComparison.Ordinal);
					if (intoIdx >= 0 && caretInSeg > intoIdx + 5)
					{
						int openParen = segLower.IndexOf('(', intoIdx);
						int closeParen = openParen >= 0 ? segLower.IndexOf(')', openParen) : -1;
						if (openParen >= 0 && caretInSeg > openParen && (closeParen < 0 || caretInSeg <= closeParen))
						{
							int valuesIdx = segLower.IndexOf(" values ", openParen, StringComparison.Ordinal);
							if (valuesIdx < 0) valuesIdx = segLower.IndexOf("\nvalues", openParen, StringComparison.Ordinal);
							if (valuesIdx < 0) valuesIdx = segLower.IndexOf("\rvalues", openParen, StringComparison.Ordinal);
							if (valuesIdx < 0 && segLower.IndexOf("values", openParen, StringComparison.Ordinal) >= 0)
								valuesIdx = segLower.IndexOf("values", openParen, StringComparison.Ordinal);
							if (valuesIdx >= 0 && caretInSeg > valuesIdx + 6)
							{
								int valuesOpenParen = segLower.IndexOf('(', valuesIdx);
								if (valuesOpenParen >= 0 && caretInSeg > valuesOpenParen) return SqlContext.InsertValues;
								return SqlContext.Other;
							}
							return SqlContext.InsertColumns;
						}
						if (openParen < 0 || caretInSeg < openParen) return SqlContext.InsertInto;
					}
					else if (intoIdx < 0 && caretInSeg > insertIdx + 6)
					{
						return SqlContext.InsertInto;
					}
				}

				if (segLower.Contains("update"))
				{
					int updIdx = segLower.IndexOf("update", StringComparison.Ordinal);
					int setIdx = segLower.IndexOf(" set ", StringComparison.Ordinal);
					if (setIdx < 0 && segLower.EndsWith(" set")) setIdx = segLower.Length - 4;
					if (setIdx < 0 && caretInSeg >= updIdx + 6) return SqlContext.From;
					if (setIdx >= 0 && caretInSeg <= setIdx + 3) return SqlContext.From;
					if (setIdx >= 0 && caretInSeg > setIdx + 3)
					{
						int whereIdx = upToCaret.LastIndexOf(" where ", StringComparison.Ordinal);
						if (whereIdx < 0 && upToCaret.EndsWith(" where")) whereIdx = upToCaret.Length - 6;
						if (whereIdx < 0 || whereIdx < setIdx) return SqlContext.SelectList; else return SqlContext.Where;
					}
				}

				var analysis = TokenAnalysis.Analyze(upToCaret);
				if (analysis.WhereAfterFrom) return SqlContext.Where;
				if (analysis.HasFrom) return analysis.CaretBeforeFrom ? SqlContext.SelectList : SqlContext.From;
				return SqlContext.SelectList;
			}

			private static int FindNextTopLevelKeyword(string lower, int start)
			{
				foreach (var kw in TopLevelStatementKeywords)
				{
					var idx = lower.IndexOf(kw.ToLowerInvariant(), start + 1, StringComparison.Ordinal);
					if (idx >= 0) return idx;
				}
				return -1;
			}
		}

		private static class TokenAnalysis
		{
			public static TokenContextInfo Analyze(string upToCaretSeg)
			{
				var info = new TokenContextInfo();
				int lastFromSeg = upToCaretSeg.LastIndexOf(" from ", StringComparison.Ordinal);
				if (lastFromSeg < 0 && upToCaretSeg.EndsWith(" from")) lastFromSeg = upToCaretSeg.Length - 5;
				int lastWhereSeg = upToCaretSeg.LastIndexOf(" where ", StringComparison.Ordinal);
				if (lastWhereSeg < 0 && upToCaretSeg.EndsWith(" where")) lastWhereSeg = upToCaretSeg.Length - 6;
				info.HasFrom = lastFromSeg >= 0;
				info.CaretBeforeFrom = info.HasFrom && upToCaretSeg.Length <= lastFromSeg;
				var tokens = upToCaretSeg.Replace('\n', ' ').Replace('\r', ' ').Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
				int lastWhereTokenIndex = -1, lastFromTokenIndex = -1;
				for (int i = 0; i < tokens.Length; i++)
				{
					var tk = tokens[i];
					if (string.Equals(tk, "from", StringComparison.OrdinalIgnoreCase)) lastFromTokenIndex = i;
					if (string.Equals(tk, "where", StringComparison.OrdinalIgnoreCase)) lastWhereTokenIndex = i;
				}
				info.WhereAfterFrom = lastWhereTokenIndex >= 0 && (lastFromTokenIndex < 0 || lastWhereTokenIndex > lastFromTokenIndex) || upToCaretSeg.Contains("\nwhere") || upToCaretSeg.Contains("\rwhere");
				return info;
			}
		}

        private static class FromContextHandler
        {
            public static IEnumerable<string> SuggestFrom(string segText, int caretInSeg, List<IntellisenseTableContract> metaList, string prefix, bool noPrefix, string currentDatabaseName)
			{
				var tail = ExtractAfterFrom(segText, caretInSeg);

				// If a complete qualified table (db.schema.table) has already been typed, don't suggest anything
				if (!string.IsNullOrEmpty(tail))
				{
					var tokens = tail.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					if (tokens.Length > 0)
					{
						var checkToken = tokens[tokens.Length - 1];
						var checkParts = checkToken.Split('.').Select(p => p.Trim('[', ']')).ToArray();
						// If fully qualified table (3 parts) exists in metadata, stop suggesting
						if (checkParts.Length == 3 && metaList.Any(t => 
							string.Equals(t.DBName ?? string.Empty, checkParts[0], StringComparison.OrdinalIgnoreCase) &&
							string.Equals(t.SchemaName ?? string.Empty, checkParts[1], StringComparison.OrdinalIgnoreCase) &&
							string.Equals(t.TableName ?? string.Empty, checkParts[2], StringComparison.OrdinalIgnoreCase)))
						{
							#if DEBUG
							System.Diagnostics.Debug.WriteLine($"[FromContextHandler] Fully qualified table '{checkToken}' detected, returning empty suggestions");
							#endif
							return Enumerable.Empty<string>();
						}
					}
				}

				// If user has typed db.schema. (i.e. ends with dot after schema), then only suggest tables under that db.schema
				var upToCaret = caretInSeg <= segText.Length ? segText.Substring(0, caretInSeg) : segText;
				var upLast = upToCaret.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? string.Empty;
                // If token contains a dot and first part is a known DB name, and user is typing the second part (schema),
                // then only suggest schemas from that DB (do not suggest DB names or tables yet).
                var upPartsCheck = upLast.TrimEnd('.').Split('.').Select(p => p.Trim('[', ']')).ToArray();
                if (upPartsCheck.Length >= 2)
                {
                    var maybeDb = upPartsCheck[0];
                    var maybeSchemaPrefix = upPartsCheck[1];
                    if (metaList.Any(t => string.Equals(t.DBName ?? string.Empty, maybeDb, StringComparison.OrdinalIgnoreCase)))
                    {
                        var schemas = metaList.Where(t => string.Equals(t.DBName ?? string.Empty, maybeDb, StringComparison.OrdinalIgnoreCase)).Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase);
                        return SuggestionUtil.FilterByPrefix(schemas.Where(s => string.IsNullOrEmpty(maybeSchemaPrefix) || s.StartsWith(maybeSchemaPrefix, StringComparison.OrdinalIgnoreCase)), prefix, noPrefix);
                    }
                }
                if (upLast.EndsWith(".") && upLast.Count(c => c == '.') >= 2)
                {
                    var upParts = upLast.TrimEnd('.').Split('.').Select(p => p.Trim('[', ']')).ToArray();
                    if (upParts.Length >= 2)
                    {
                        var db = upParts[0]; var schema = upParts[1];
                        var tablesUnder = metaList.Where(t => string.Equals(t.DBName ?? string.Empty, db, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SchemaName ?? string.Empty, schema, StringComparison.OrdinalIgnoreCase)).Select(t => t.TableName).Distinct(StringComparer.OrdinalIgnoreCase);
                        return SuggestionUtil.FilterByPrefix(tablesUnder, prefix, noPrefix);
                    }
                }
				// If a full table identifier is already present in the tail, stop suggesting db/schema/table
				if (!string.IsNullOrEmpty(tail) && FallbackDetectTables(tail, metaList).Any())
					return Enumerable.Empty<string>();
				if (string.IsNullOrEmpty(tail))
				{
					// Right after FROM (or JOIN) with no identifier typed: suggest dbs and schemas; if none available, fall back to tables
                    var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    // Prefer schemas from the current database
                    if (!string.IsNullOrEmpty(currentDatabaseName))
                    {
                        foreach (var sch in metaList.Where(t => string.Equals(t.DBName ?? string.Empty, currentDatabaseName, StringComparison.OrdinalIgnoreCase)).Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase))
                            if (noPrefix || sch.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(sch);
                    }

                    // Also suggest database names
                    foreach (var db in metaList.Select(t => t.DBName).Where(d => !string.IsNullOrEmpty(d)).Distinct(StringComparer.OrdinalIgnoreCase))
                        if (noPrefix || db.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(db);

                    // If we couldn't find anything (no current-db schemas or db names), fall back to all schemas then tables
                    if (set.Count == 0)
                    {
                        foreach (var sch in metaList.Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase))
                            if (noPrefix || sch.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(sch);

                        if (set.Count == 0)
                        {
                            foreach (var tn in metaList.Select(t => t.TableName).Where(tn => !string.IsNullOrEmpty(tn)).Distinct(StringComparer.OrdinalIgnoreCase))
                                if (noPrefix || tn.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(tn);
                        }
                    }

                    return set.OrderBy(s => s, StringComparer.OrdinalIgnoreCase);
				}
				var lastToken = tail.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? string.Empty;
				var parts = lastToken.Split('.').Select(p => p.Trim('[', ']')).ToArray();
			if (parts.Length == 1)
			{
				var token = parts[0];
			var dbsByPrefix = metaList.Select(t => t.DBName).Where(db => !string.IsNullOrEmpty(db)).Distinct(StringComparer.OrdinalIgnoreCase).Where(db => db.StartsWith(token, StringComparison.OrdinalIgnoreCase)).ToList();
			if (dbsByPrefix.Any())
			{
				// If user started typing something that matches a DB name, prefer DB suggestions only
				return SuggestionUtil.FilterByPrefix(dbsByPrefix, prefix, noPrefix);
			}
			var schemasByPrefix = metaList.Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase).Where(s => s.StartsWith(token, StringComparison.OrdinalIgnoreCase)).ToList();
			if (schemasByPrefix.Any())
			{
				return SuggestionUtil.FilterByPrefix(schemasByPrefix, prefix, noPrefix);
			}
			// No db/schema matches: suggest table names by token, excluding tables that are exactly matching (already fully typed)
			var tableNames = metaList
				.Select(t => t.TableName)
				.Where(tn => !string.IsNullOrEmpty(tn) && !string.Equals(tn, token, StringComparison.OrdinalIgnoreCase) && tn.StartsWith(token, StringComparison.OrdinalIgnoreCase))
				.Distinct(StringComparer.OrdinalIgnoreCase);
			return SuggestionUtil.FilterByPrefix(tableNames, prefix, noPrefix);
			}
				if (parts.Length == 2)
				{
				var first = parts[0]; var second = parts[1];
				// If first matches a DB name, suggest schemas from that DB filtered by second (schema prefix)
				var schemasUnderDb = metaList.Where(t => string.Equals(t.DBName ?? string.Empty, first, StringComparison.OrdinalIgnoreCase)).Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase);
				if (schemasUnderDb.Any())
				{
					return SuggestionUtil.FilterByPrefix(schemasUnderDb.Where(s => string.IsNullOrEmpty(second) || s.StartsWith(second, StringComparison.OrdinalIgnoreCase)), prefix, noPrefix);
				}
				// If first matches a schema name, suggest tables under that schema (use second as table prefix)
				var tablesUnderSchema = metaList.Where(t => string.Equals(t.SchemaName ?? string.Empty, first, StringComparison.OrdinalIgnoreCase)).Select(t => t.TableName).Where(tn => string.IsNullOrEmpty(second) || tn.StartsWith(second, StringComparison.OrdinalIgnoreCase)).Distinct(StringComparer.OrdinalIgnoreCase);
				if (tablesUnderSchema.Any()) return SuggestionUtil.FilterByPrefix(tablesUnderSchema, prefix, noPrefix);
				// Fallback: if nothing matched, try tables under db.schema
				var tablesUnderDbSchema = metaList.Where(t => string.Equals(t.DBName ?? string.Empty, first, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SchemaName ?? string.Empty, second, StringComparison.OrdinalIgnoreCase)).Select(t => t.TableName).Distinct(StringComparer.OrdinalIgnoreCase);
				return SuggestionUtil.FilterByPrefix(tablesUnderDbSchema, prefix, noPrefix);
				}
			if (parts.Length >= 3)
			{
				var db = parts[0]; var schema = parts[1]; var typedPrefix = parts[2];
				// Exclude the table already being typed to avoid suggesting it again
				var scopedTables = metaList
					.Where(t => string.Equals(t.DBName ?? string.Empty, db, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SchemaName ?? string.Empty, schema, StringComparison.OrdinalIgnoreCase))
					.Select(t => t.TableName)
					.Where(tn => !string.Equals(tn, typedPrefix, StringComparison.OrdinalIgnoreCase) && (string.IsNullOrEmpty(typedPrefix) || tn.StartsWith(typedPrefix, StringComparison.OrdinalIgnoreCase)))
					.Distinct(StringComparer.OrdinalIgnoreCase);
				return SuggestionUtil.FilterByPrefix(scopedTables, prefix, noPrefix);
			}
				return Enumerable.Empty<string>();
			}

			public static string ExtractAfterFrom(string segText, int caretInSeg)
			{
				var upToCaret = caretInSeg <= segText.Length ? segText.Substring(0, caretInSeg) : segText;
				// Normalize whitespace so number of spaces doesn't change detection
				var tokens = upToCaret.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length == 0) return string.Empty;
				var normalized = string.Join(" ", tokens);
				var lower = normalized.ToLowerInvariant();
				int lastFrom = lower.LastIndexOf(" from ", StringComparison.Ordinal);
				if (lastFrom < 0 && lower.EndsWith(" from")) lastFrom = lower.Length - 5;
				int lastJoin = lower.LastIndexOf(" join ", StringComparison.Ordinal);
				if (lastJoin < 0 && lower.EndsWith(" join")) lastJoin = lower.Length - 5;
				int startPos = Math.Max(lastFrom, lastJoin);
				if (startPos < 0) return string.Empty;
				bool isEndToken = (lastFrom >= 0 && (lower.EndsWith(" from") && startPos == lower.Length - 5)) || (lastJoin >= 0 && (lower.EndsWith(" join") && startPos == lower.Length - 5));
				int startIdx = isEndToken ? startPos + 5 : startPos + 6;
				if (startIdx > normalized.Length) return string.Empty;
				return normalized.Substring(startIdx);
			}
		}
	}
}
