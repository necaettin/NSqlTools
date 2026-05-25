using Microsoft.SqlServer.TransactSql.ScriptDom;
using NSqlTools.Types.IntellisenseContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NSqlTools.BusinessLayer.Intellisense
{
	public static partial class SimpleSqlIntellisenseEngine
	{ 
		public static class MetaUtil
		{
			public static Dictionary<string, IntellisenseTableContract> BuildMetaLookup(List<IntellisenseTableContract> metas)
			{
				var dict = new Dictionary<string, IntellisenseTableContract>(StringComparer.OrdinalIgnoreCase);
				foreach (var m in metas)
				{
					var table = m.TableName ?? string.Empty;
					var schema = m.SchemaName ?? string.Empty;
					var db = m.DBName ?? string.Empty;
					if (!dict.ContainsKey(table)) dict[table] = m;
					var schemaTable = string.IsNullOrEmpty(schema) ? table : schema + "." + table;
					if (!dict.ContainsKey(schemaTable)) dict[schemaTable] = m;
					var dbSchemaTable = string.IsNullOrEmpty(db) ? schemaTable : db + "." + schemaTable;
					if (!dict.ContainsKey(dbSchemaTable)) dict[dbSchemaTable] = m;
				}
				return dict;
			}
		}

		public static class TextUtil
		{
			public static bool IsAfterDot(string sql, int caret) { return caret > 0 && sql[caret - 1] == '.'; }
			public static string GetPrefix(string sql, int caret)
			{
				int start = caret - 1;
				while (start >= 0 && (char.IsLetterOrDigit(sql[start]) || sql[start] == '_' || sql[start] == '@')) start--;
				start++;
				var prefix = start <= caret ? sql.Substring(start, caret - start) : string.Empty;
				if (!string.IsNullOrEmpty(prefix) && (prefix.Equals("by", StringComparison.OrdinalIgnoreCase) || prefix.Equals("b", StringComparison.OrdinalIgnoreCase)))
				{
					int before = start - 1; while (before >= 0 && char.IsWhiteSpace(sql[before])) before--; int wordEnd = before; while (before >= 0 && (char.IsLetter(sql[before]) || sql[before] == '_')) before--; string prevWord = wordEnd >= 0 ? sql.Substring(before + 1, wordEnd - before) : string.Empty; if (prevWord.Equals("order", StringComparison.OrdinalIgnoreCase) || prevWord.Equals("group", StringComparison.OrdinalIgnoreCase)) return string.Empty;
				}
				return prefix;
			}
			public static bool IsInsideString(string sql, int caret)
			{
				int count = 0;
				for (int i = 0; i < caret; i++)
				{
					if (sql[i] == '\'') { if (i + 1 < sql.Length && sql[i + 1] == '\'') { i++; continue; } count++; }
				}
				return (count & 1) == 1;
			}
		}

		private static class SuggestionUtil
		{
            // Simple in-memory cache to preserve base candidate set while user keeps typing within same token
            private static string _lastBaseKey;
            private static HashSet<string> _lastBaseSet;
            private static string _lastPrefix;

			public static IEnumerable<string> BuildFinalSuggestions(SqlContext context, Dictionary<string, string> tableAliases, List<IntellisenseTableContract> tableMetaList, List<string> allColumns, Dictionary<string, IntellisenseTableContract> fullMeta, string prefix, bool noPrefix, string segmentText = "", List<string> variables = null, int caretInSeg = -1, string currentDatabaseName = null, bool typingAfterAliasDot = false)
			{
                // Two-stage suggestion: build base candidate set (no prefix filtering), then apply prefix filter
                var realAliases = tableAliases.Keys.Where(k => !k.StartsWith(InternalTablePrefix));
                bool isUpdateContext = !string.IsNullOrEmpty(segmentText) && segmentText.ToLowerInvariant().Contains("update");
                bool afterDotInSeg = caretInSeg > 0 && segmentText != null && segmentText.Length >= caretInSeg && segmentText[caretInSeg - 1] == '.';

                // Determine token start (position where current prefix begins) within segment
                var segTextSafeForKey = segmentText ?? string.Empty;
                // Determine stable token start by scanning backward from caret to token boundary
                int tokenStart = Math.Max(0, caretInSeg);
                int scanBack = tokenStart - 1;
                while (scanBack >= 0 && (char.IsLetterOrDigit(segTextSafeForKey[scanBack]) || segTextSafeForKey[scanBack] == '_' || segTextSafeForKey[scanBack] == ']' || segTextSafeForKey[scanBack] == '[')) scanBack--;
                tokenStart = Math.Max(0, scanBack + 1);
                if (tokenStart > segTextSafeForKey.Length) tokenStart = segTextSafeForKey.Length;
                var leftContext = segTextSafeForKey.Substring(0, tokenStart);
                // normalize leftContext by trimming any trailing dot so base key is stable while typing after a dot
                var leftContextNorm = leftContext.TrimEnd('.');
                var baseKey = string.Join("|", new[] { context.ToString(), leftContextNorm, currentDatabaseName ?? string.Empty });
                var useCache = !string.IsNullOrEmpty(_lastBaseKey) && _lastBaseKey == baseKey && _lastBaseSet != null;

                // Special case: variables starting with @ keep previous behavior
                if (!string.IsNullOrEmpty(prefix) && prefix.StartsWith("@"))
                {
                    var vars = variables ?? new List<string>();
                    var prefixWithoutAt = prefix.Length > 0 && prefix[0] == '@' ? prefix.Substring(1) : prefix;
                    var temp = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    if (vars.Count == 0)
                    {
                        if (string.IsNullOrEmpty(prefix) || "@".StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) temp.Add("@");
                    }
                    else
                    {
                        foreach (var v in vars)
                        {
                            var nameNoAt = v.StartsWith("@") ? v.Substring(1) : v;
                            if (string.IsNullOrEmpty(prefixWithoutAt) || nameNoAt.StartsWith(prefixWithoutAt, StringComparison.OrdinalIgnoreCase)) temp.Add(nameNoAt);
                        }
                    }
                    AddContextualKeywords(temp, context, prefix, noPrefix, segmentText, caretInSeg);
                    return Order(temp);
                }

                if (context == SqlContext.Declare)
                {
                    var temp = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    AddContextualKeywords(temp, context, prefix, noPrefix, segmentText, caretInSeg);
                    return Order(temp);
                }

                if (isUpdateContext && (context == SqlContext.SelectList || context == SqlContext.Where))
                {
                    var temp = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    AddColumns(temp, allColumns, string.Empty, true);
                    AddVariables(temp, variables ?? new List<string>(), string.Empty, true);
                    AddContextualKeywords(temp, context, string.Empty, true, segmentText, caretInSeg);
                    var filtered = new HashSet<string>(FilterByPrefix(temp, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                    return OrderWithPriority(filtered, allColumns, tableAliases);
                }

				var baseSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

				Action<IEnumerable<string>> addAll = items => { if (items == null) return; foreach (var it in items) baseSet.Add(it); };

				#if DEBUG
				System.Diagnostics.Debug.WriteLine($"[BuildFinalSuggestions] context={context} useCache={useCache} prefix='{prefix}'");
				#endif

                switch (context)
                {
                    case SqlContext.InsertInto:
                        {
                            string tail = string.Empty;
                            var segTextSafe = segmentText ?? string.Empty;
                            var lower = segTextSafe.ToLowerInvariant();
                            int insertIdx = lower.IndexOf("insert", StringComparison.Ordinal);
                            int intoIdx = insertIdx >= 0 ? lower.IndexOf(" into ", insertIdx, StringComparison.Ordinal) : -1;
                            if (intoIdx >= 0 && caretInSeg > intoIdx + 6)
                            {
                                int startIdx = intoIdx + 6;
                                tail = segTextSafe.Substring(startIdx, Math.Min(caretInSeg - startIdx, Math.Max(0, segTextSafe.Length - startIdx)));
                            }
                            if (!string.IsNullOrEmpty(tail) && FallbackDetectTables(tail, tableMetaList).Any())
                            {
                                var temp = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                                AddContextualKeywords(temp, context, string.Empty, true, segmentText, caretInSeg);
                                var filtered = new HashSet<string>(FilterByPrefix(temp, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                                return Order(filtered);
                            }
						// Use same From-context suggestion logic for INSERT INTO to prefer current DB schemas, etc.
						var insertCandidates = FromContextHandler.SuggestFrom(segTextSafe, caretInSeg, tableMetaList, string.Empty, true, currentDatabaseName).ToList();
                        if (useCache)
                        {
                            // reuse base set
                            baseSet = new HashSet<string>(_lastBaseSet, StringComparer.OrdinalIgnoreCase);
                        }
                        else
                        {
                            addAll(insertCandidates);
							_lastBaseKey = baseKey;
							_lastBaseSet = new HashSet<string>(baseSet, StringComparer.OrdinalIgnoreCase);
						}
						AddContextualKeywords(baseSet, context, string.Empty, true, segmentText, caretInSeg);
						var filtered2 = new HashSet<string>(FilterByPrefix(baseSet, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                        _lastPrefix = prefix;
                        return OrderDbSchema(filtered2, tableMetaList);
                        }
					case SqlContext.InsertColumns:
						AddColumns(baseSet, allColumns, string.Empty, true);
						AddContextualKeywords(baseSet, context, string.Empty, true, segmentText, caretInSeg);
						var filtered3 = new HashSet<string>(FilterByPrefix(baseSet, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                        return OrderWithPriority(filtered3, allColumns, tableAliases);
					case SqlContext.InsertValues:
						AddVariables(baseSet, variables ?? new List<string>(), string.Empty, true);
						AddContextualKeywords(baseSet, context, string.Empty, true, segmentText, caretInSeg);
						var filtered4 = new HashSet<string>(FilterByPrefix(baseSet, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                        return Order(filtered4);
                    case SqlContext.SelectList:
                        // When typing after alias.dot (e.g., "a.acc"), exclude aliases and keywords - only suggest columns
                        if (!typingAfterAliasDot)
                        {
                            AddTableAliases(baseSet, realAliases, string.Empty, true);
                            AddAggregates(baseSet, string.Empty, true);
                            AddContextualKeywords(baseSet, context, string.Empty, true, segmentText, caretInSeg);
                        }
                        AddColumns(baseSet, allColumns, string.Empty, true);
                        AddAliasDotColumns(baseSet, tableAliases.Where(a => !a.Key.StartsWith(InternalTablePrefix)).ToDictionary(a => a.Key, a => a.Value), fullMeta, string.Empty, true);
                        AddVariables(baseSet, variables ?? new List<string>(), string.Empty, true);
                        var filtered5 = new HashSet<string>(FilterByPrefix(baseSet, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
						#if DEBUG
						System.Diagnostics.Debug.WriteLine($"[BuildFinalSuggestions] SelectList case: typingAfterAliasDot={typingAfterAliasDot} filtered5 count={filtered5.Count} items=[{string.Join(", ", filtered5.Take(10))}]");
						#endif
                        return OrderWithPriority(filtered5, allColumns, tableAliases);
                    case SqlContext.Where:
                    case SqlContext.JoinOn:
                    case SqlContext.Having:
                    case SqlContext.GroupBy:
                    case SqlContext.OrderBy:
                        // When typing after alias.dot (e.g., "a.acc"), exclude aliases and keywords - only suggest columns
                        if (!typingAfterAliasDot)
                        {
                            AddTableAliases(baseSet, realAliases, string.Empty, true);
                            AddContextualKeywords(baseSet, context, string.Empty, true, segmentText, caretInSeg);
                        }
                        AddColumns(baseSet, allColumns, string.Empty, true);
                        if (afterDotInSeg)
                            AddAliasDotColumns(baseSet, tableAliases.Where(a => !a.Key.StartsWith(InternalTablePrefix)).ToDictionary(a => a.Key, a => a.Value), fullMeta, string.Empty, true);
                        AddVariables(baseSet, variables ?? new List<string>(), string.Empty, true);
                        var filtered6 = new HashSet<string>(FilterByPrefix(baseSet, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                        #if DEBUG
                        System.Diagnostics.Debug.WriteLine($"[BuildFinalSuggestions] {context} case: typingAfterAliasDot={typingAfterAliasDot} filtered6 count={filtered6.Count}");
                        #endif
                        return OrderWithPriority(filtered6, allColumns, tableAliases);
                    case SqlContext.From:
                        {
                            var seg = segmentText ?? string.Empty;
                            var tailList = FromContextHandler.SuggestFrom(seg, caretInSeg, tableMetaList, string.Empty, true, currentDatabaseName).ToList();
                        if (useCache)
                        {
                            baseSet = new HashSet<string>(_lastBaseSet, StringComparer.OrdinalIgnoreCase);
                        }
                        else
                        {
                            addAll(tailList);
                            _lastBaseKey = baseKey;
                            _lastBaseSet = new HashSet<string>(baseSet, StringComparer.OrdinalIgnoreCase);
                        }
                            var afterTokenText = FromContextHandler.ExtractAfterFrom(seg, caretInSeg);
                            if (!string.IsNullOrEmpty(afterTokenText) && FallbackDetectTables(afterTokenText, tableMetaList).Any())
                            {
                                var temp = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                                AddContextualKeywords(temp, context, string.Empty, true, segmentText, caretInSeg);
							var filtered7 = new HashSet<string>(FilterByPrefix(temp, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
							return Order(filtered7);
						}
						AddContextualKeywords(baseSet, context, string.Empty, true, segmentText, caretInSeg);
						var filtered8 = new HashSet<string>(FilterByPrefix(baseSet, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                            _lastPrefix = prefix;
                            return OrderDbSchema(filtered8, tableMetaList);
                        }
                    default:
                        AddTableAliases(baseSet, realAliases, string.Empty, true);
						AddColumns(baseSet, allColumns, string.Empty, true);
                        AddAliasDotColumns(baseSet, tableAliases.Where(a => !a.Key.StartsWith(InternalTablePrefix)).ToDictionary(a => a.Key, a => a.Value), fullMeta, string.Empty, true);
						AddVariables(baseSet, variables ?? new List<string>(), string.Empty, true);
						AddContextualKeywords(baseSet, context, string.Empty, true, segmentText, caretInSeg);
						#if DEBUG
						System.Diagnostics.Debug.WriteLine($"[BuildFinalSuggestions] default case: baseSet count after keywords={baseSet.Count}");
						#endif
						var filtered9 = new HashSet<string>(FilterByPrefix(baseSet, prefix, noPrefix), StringComparer.OrdinalIgnoreCase);
                        _lastPrefix = prefix;
                        // cache base set
                        if (!useCache)
                        {
                            _lastBaseKey = baseKey;
                            _lastBaseSet = new HashSet<string>(baseSet, StringComparer.OrdinalIgnoreCase);
                        }
                        return OrderWithPriority(filtered9, allColumns, tableAliases);
                }
			}

			private static IEnumerable<string> OrderWithPriority(HashSet<string> set, List<string> allColumns, Dictionary<string, string> tableAliases)
			{
				var result = new List<string>();
				var remaining = new HashSet<string>(set, StringComparer.OrdinalIgnoreCase);
				// Add explicit columns first
				foreach (var c in (allColumns ?? new List<string>())) if (remaining.Remove(c)) result.Add(c);
				// Then alias.column items
				foreach (var alias in (tableAliases ?? new Dictionary<string, string>()).Keys.Where(a => !a.StartsWith(InternalTablePrefix)))
				{
					var aliasItems = remaining.Where(r => r.StartsWith(alias + ".", StringComparison.OrdinalIgnoreCase)).OrderBy(r => r, StringComparer.OrdinalIgnoreCase).ToList();
					foreach (var ai in aliasItems) { remaining.Remove(ai); result.Add(ai); }
				}
				// Put non-reserved items next, reserved keywords last
				var reserved = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				try { reserved.UnionWith(TSqlReservedKeywordsProvider.GetAll()); }
				catch
				{
					// ignored
				}
				reserved.UnionWith(TopLevelStatementKeywords);
				var nonReserved = remaining.Where(r => !reserved.Contains(r)).OrderBy(s => s, StringComparer.OrdinalIgnoreCase);
				var reservedRemaining = remaining.Where(r => reserved.Contains(r)).OrderBy(s => s, StringComparer.OrdinalIgnoreCase);
				result.AddRange(nonReserved);
				result.AddRange(reservedRemaining);
				return result;
			}

			private static IEnumerable<string> OrderDbSchema(HashSet<string> set, List<IntellisenseTableContract> tableMetaList)
			{
				var result = new List<string>();
				var items = new HashSet<string>(set, StringComparer.OrdinalIgnoreCase);
				var schemas = new HashSet<string>(tableMetaList.Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)), StringComparer.OrdinalIgnoreCase);
				var dbs = new HashSet<string>(tableMetaList.Select(t => t.DBName).Where(d => !string.IsNullOrEmpty(d)), StringComparer.OrdinalIgnoreCase);
				// reserved keywords
				var reserved = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				try { reserved.UnionWith(TSqlReservedKeywordsProvider.GetAll()); }
				catch
				{
					// ignored
				}
				reserved.UnionWith(TopLevelStatementKeywords);

				// add schemas first
				foreach (var s in items.Where(i => schemas.Contains(i)).OrderBy(i => i, StringComparer.OrdinalIgnoreCase)) { result.Add(s); items.Remove(s); }
				// then dbs
				foreach (var d in items.Where(i => dbs.Contains(i)).OrderBy(i => i, StringComparer.OrdinalIgnoreCase)) { result.Add(d); items.Remove(d); }
				// then remaining non-reserved
				foreach (var r in items.Where(i => !reserved.Contains(i)).OrderBy(i => i, StringComparer.OrdinalIgnoreCase)) { result.Add(r); items.Remove(r); }
				// finally reserved keywords
				foreach (var r in items.Where(i => reserved.Contains(i)).OrderBy(i => i, StringComparer.OrdinalIgnoreCase)) { result.Add(r); items.Remove(r); }

				return result;
			}

			private static void AddContextualKeywords(HashSet<string> set, SqlContext context, string prefix, bool noPrefix, string segmentText, int caretInSeg)
			{
				segmentText = segmentText ?? string.Empty;
				var upToCaret = caretInSeg >= 0 && caretInSeg <= segmentText.Length ? segmentText.Substring(0, caretInSeg) : segmentText;
				var lower = TokUtil.ToLower(upToCaret);
				var tokens = TokUtil.SplitTokens(lower);
				int len = tokens.Length;
				string last = len == 0 ? string.Empty : tokens[len - 1];
				string prev = len > 1 ? tokens[len - 2] : string.Empty;
				if (last == SqlTokens.Order.ToLowerInvariant() || (prev == SqlTokens.Order.ToLowerInvariant() && last != SqlTokens.By.ToLowerInvariant() && SqlTokens.By.ToLowerInvariant().StartsWith(last))) { set.Add(SqlTokens.By); }
				if (last == SqlTokens.Group.ToLowerInvariant() || (prev == SqlTokens.Group.ToLowerInvariant() && last != SqlTokens.By.ToLowerInvariant() && SqlTokens.By.ToLowerInvariant().StartsWith(last))) { set.Add(SqlTokens.By); }
				if (last == SqlTokens.Join.ToLowerInvariant() && (context == SqlContext.From || context == SqlContext.SelectList || context == SqlContext.Other)) { if (noPrefix || SqlTokens.On.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) { set.Add(SqlTokens.On); } }
				if ((context == SqlContext.From || context == SqlContext.SelectList || context == SqlContext.Other) && (last == SqlTokens.Inner.ToLowerInvariant() || last == SqlTokens.Left.ToLowerInvariant() || last == SqlTokens.Right.ToLowerInvariant() || last == SqlTokens.Full.ToLowerInvariant() || last == SqlTokens.Outer.ToLowerInvariant() || last == SqlTokens.Cross.ToLowerInvariant())) { if (noPrefix || SqlTokens.Join.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) { set.Add(SqlTokens.Join); } }

				// If user typed just "join" (especially on a new line), proactively suggest concrete join variants
				if (string.Equals(last, SqlTokens.Join.ToLowerInvariant(), StringComparison.Ordinal))
				{
					set.Add("JOIN");
					set.Add("INNER JOIN");
					set.Add("LEFT JOIN");
					set.Add("RIGHT JOIN");
					set.Add("FULL JOIN");
					set.Add("FULL OUTER JOIN");
					set.Add("CROSS JOIN");
					set.Add("CROSS APPLY");
					set.Add("OUTER APPLY");
				}

				// When editing an UPDATE statement, ensure SET is suggested in From-like context right after target table
				if ((context == SqlContext.From || context == SqlContext.SelectList || context == SqlContext.Other)
					&& !string.IsNullOrEmpty(segmentText)
					&& segmentText.IndexOf("update", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					if (noPrefix || SqlTokens.Set.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
						set.Add(SqlTokens.Set);
				}

				// Always add top-level keywords that match the current prefix (to support typing on new lines even if segment detection fails)
				var topLevelKeywords = new[] { "SELECT", "INSERT", "UPDATE", "DELETE", "MERGE" };
				foreach (var kw in topLevelKeywords)
				{
					if (noPrefix || kw.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
						set.Add(kw);
				}

				foreach (var k in GetKeywordsForContext(context)) if (noPrefix || k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(k);
			}

			private static IEnumerable<string> GetKeywordsForContext(SqlContext context)
			{
				switch (context)
				{
					case SqlContext.SelectList: 
						// Include all top-level statement keywords since parser often defaults to SelectList in empty/incomplete statements
						return new[] { "SELECT", "INSERT", "UPDATE", "DELETE", "MERGE", "FROM", "DISTINCT", "TOP", "AS", "CASE", "CONVERT", "CAST" };
					case SqlContext.From:
						// Expanded to include combined JOIN variants for better suggestions
						return new[]
						{
							"JOIN",
							"ON", "WHERE",
							// combined variants
							"INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "FULL JOIN", "FULL OUTER JOIN",
							"CROSS JOIN", "CROSS APPLY", "OUTER APPLY"
						};
					case SqlContext.Where: return new[] { "AND", "OR", "NOT", "EXISTS", "IN", "LIKE", "BETWEEN", "IS", "NULL" };
					case SqlContext.GroupBy: return new[] { "GROUP BY", "ROLLUP", "CUBE", "GROUPING SETS", "HAVING" };
					case SqlContext.Having: return new[] { "AND", "OR", "NOT" };
					case SqlContext.OrderBy: return new[] { "ASC", "DESC", "OFFSET", "FETCH" };
					case SqlContext.JoinOn: return new[] { "AND", "OR", "ON" };
					case SqlContext.InsertInto: return new[] { "INSERT", "INTO", "VALUES", "OUTPUT" };
					case SqlContext.InsertColumns: return new[] { "OUTPUT" };
					case SqlContext.InsertValues: return new[] { "VALUES" };
					case SqlContext.Declare: return new[] { "@", "INT", "BIGINT", "SMALLINT", "TINYINT", "BIT", "DECIMAL", "NUMERIC", "FLOAT", "REAL", "MONEY", "SMALLMONEY", "CHAR", "NCHAR", "VARCHAR", "NVARCHAR", "TEXT", "NTEXT", "DATE", "DATETIME", "DATETIME2", "SMALLDATETIME", "TIME", "DATETIMEOFFSET", "BINARY", "VARBINARY", "UNIQUEIDENTIFIER", "XML" };
					default: 
						// For Other/unknown contexts, suggest common top-level SQL keywords
						return new[] { "SELECT", "INSERT", "UPDATE", "DELETE", "MERGE", "FROM", "WHERE", "JOIN", "ORDER BY", "GROUP BY" };
				}
			}

			private static void AddVariables(HashSet<string> set, List<string> variables, string prefix, bool noPrefix)
			{ if (variables == null) return; foreach (var v in variables) if (noPrefix || v.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(v); }
			private static void AddTableAliases(HashSet<string> set, IEnumerable<string> aliases, string prefix, bool noPrefix)
			{ if (aliases == null) return; foreach (var a in aliases) if (noPrefix || a.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(a); }
			private static void AddColumns(HashSet<string> set, IEnumerable<string> columns, string prefix, bool noPrefix)
			{ if (columns == null) return; foreach (var c in columns) if (noPrefix || c.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(c); }
			private static void AddAliasDotColumns(HashSet<string> set, Dictionary<string, string> aliasMap, Dictionary<string, IntellisenseTableContract> fullMeta, string prefix, bool noPrefix)
			{
				if (aliasMap == null || fullMeta == null) return;
				foreach (var alias in aliasMap.Keys.Where(a => !a.StartsWith(InternalTablePrefix)))
				{
					var full = aliasMap[alias];
					if (!fullMeta.TryGetValue(full, out var meta) || meta.ColumnList == null) continue;
					foreach (var col in meta.ColumnList)
					{
						var composed = alias + "." + col.ColumnName;
						if (noPrefix || composed.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) || col.ColumnName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(composed);
					}
				}
			}
			private static void AddAggregates(HashSet<string> set, string prefix, bool noPrefix)
			{ foreach (var a in AggregateFunctions) if (noPrefix || a.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) set.Add(a); }
			public static IEnumerable<string> FilterByPrefix(IEnumerable<string> items, string prefix, bool noPrefix)
			{
				if (items == null) return Enumerable.Empty<string>();
				if (noPrefix || string.IsNullOrEmpty(prefix)) return items;

				// Önce baştan eşleşenleri al, sonra ortadan eşleşenleri ekle
				// Baştan eşleşenler her zaman öncelikli!
				var startsWith = new List<string>();
				var contains = new List<string>();

				foreach (var item in items)
				{
					if (item.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					{
						startsWith.Add(item);
					}
					else if (item.IndexOf(prefix, StringComparison.OrdinalIgnoreCase) >= 0)
					{
						contains.Add(item);
					}
				}

				// Baştan eşleşenleri önce, sonra ortadan eşleşenleri döndür
				return startsWith.Concat(contains);
			}
			private static IEnumerable<string> Order(HashSet<string> set) { return set.OrderBy(s => s, StringComparer.OrdinalIgnoreCase); }
		}

		private static class ContextUtil
		{
			public static SqlContext DetermineContext(TSqlStatement stmt, int caret)
			{
				if (stmt is SelectStatement sel && sel.QueryExpression is QuerySpecification qsRoot && qsRoot.OrderByClause != null)
				{
					if (caret >= qsRoot.OrderByClause.StartOffset && caret <= qsRoot.OrderByClause.StartOffset + qsRoot.OrderByClause.FragmentLength)
						return SqlContext.OrderBy;
				}
				else if (stmt is SelectStatement sel2 && sel2.QueryExpression is BinaryQueryExpression bqe)
				{
					if (bqe.SecondQueryExpression is QuerySpecification qs2 && qs2.OrderByClause != null)
					{
						if (caret >= qs2.OrderByClause.StartOffset && caret <= qs2.OrderByClause.StartOffset + qs2.OrderByClause.FragmentLength)
							return SqlContext.OrderBy;
					}
					if (bqe.FirstQueryExpression is QuerySpecification qs1 && qs1.OrderByClause != null)
					{
						if (caret >= qs1.OrderByClause.StartOffset && caret <= qs1.OrderByClause.StartOffset + qs1.OrderByClause.FragmentLength)
							return SqlContext.OrderBy;
					}
				}

				if (stmt is InsertStatement insertStmt && insertStmt.InsertSpecification != null)
				{
					var spec = insertStmt.InsertSpecification;
					var target = spec.Target as NamedTableReference;
					if (target != null && target.SchemaObject != null)
					{
						int tableEnd = target.StartOffset + target.FragmentLength;
						if (caret < tableEnd) return SqlContext.InsertInto;
					}
					if (spec.Columns != null && spec.Columns.Count > 0)
					{
						var firstCol = spec.Columns[0];
						var lastCol = spec.Columns[spec.Columns.Count - 1];
						if (caret >= firstCol.StartOffset && caret <= lastCol.StartOffset + lastCol.FragmentLength)
							return SqlContext.InsertColumns;
					}
					if (spec.InsertSource != null)
					{
						if (caret >= spec.InsertSource.StartOffset && caret <= spec.InsertSource.StartOffset + spec.InsertSource.FragmentLength)
							return SqlContext.InsertValues;
					}
				}

				if (stmt is UpdateStatement up && up.UpdateSpecification != null)
				{
					var spec = up.UpdateSpecification;
					if (spec.SetClauses != null && spec.SetClauses.Count > 0)
					{
						var setStart = spec.SetClauses[0].StartOffset;
						if (caret >= setStart) return SqlContext.From;
						var lastSet = spec.SetClauses[spec.SetClauses.Count - 1];
						if (caret >= setStart && caret <= lastSet.StartOffset + lastSet.FragmentLength) return SqlContext.SelectList;
					}
					if (spec.WhereClause != null && caret >= spec.WhereClause.StartOffset && caret <= spec.WhereClause.StartOffset + spec.WhereClause.FragmentLength)
						return SqlContext.Where;
				}

				foreach (var qs in ContextUtil.GetQuerySpecifications(stmt))
				{
					if (caret < qs.StartOffset || caret > qs.StartOffset + qs.FragmentLength) continue;
					var fromStart = qs.FromClause?.StartOffset ?? int.MaxValue;
					if (caret < fromStart) return SqlContext.SelectList;
					if (qs.FromClause != null && caret >= qs.FromClause.StartOffset && caret <= qs.FromClause.StartOffset + qs.FromClause.FragmentLength)
					{
						foreach (var tr in qs.FromClause.TableReferences)
						{
							if (IsCaretInJoinOn(tr, caret)) return SqlContext.JoinOn;
						}
						return SqlContext.From;
					}
					if (qs.WhereClause != null && caret >= qs.WhereClause.StartOffset && caret <= qs.WhereClause.StartOffset + qs.WhereClause.FragmentLength) return SqlContext.Where;
					if (qs.GroupByClause != null && caret >= qs.GroupByClause.StartOffset && caret <= qs.GroupByClause.StartOffset + qs.GroupByClause.FragmentLength) return SqlContext.GroupBy;
					if (qs.HavingClause != null && caret >= qs.HavingClause.StartOffset && caret <= qs.HavingClause.StartOffset + qs.HavingClause.FragmentLength) return SqlContext.Having;
				}
				return SqlContext.Other;
			}

			public static IEnumerable<QuerySpecification> GetQuerySpecifications(TSqlStatement stmt)
			{
				var list = new List<QuerySpecification>();
				stmt.Accept(new QuerySpecVisitor(list));
				return list;
			}

			private class QuerySpecVisitor : TSqlFragmentVisitor
			{
				private readonly List<QuerySpecification> _list;
				public QuerySpecVisitor(List<QuerySpecification> list) { _list = list; }
				public override void Visit(QuerySpecification node) { _list.Add(node); }
			}

			private static bool IsCaretInJoinOn(TableReference tr, int caret)
			{
				if (tr is QualifiedJoin qj && qj.SearchCondition != null)
					return caret >= qj.SearchCondition.StartOffset && caret <= qj.SearchCondition.StartOffset + qj.SearchCondition.FragmentLength;
				if (tr is QualifiedJoin qj2)
					return IsCaretInJoinOn(qj2.FirstTableReference, caret) || IsCaretInJoinOn(qj2.SecondTableReference, caret);
				return false;
			}
		}

		private static class AliasUtil
		{
			public static Dictionary<string, string> BuildAliasMap(ParsedEnvironment env, SegmentInfo segment, string sql, int caret, List<IntellisenseTableContract> metas, int manualStart)
			{
				bool insideParsed = env.ActiveStatement != null && caret <= env.ActiveStatement.StartOffset + env.ActiveStatement.FragmentLength && manualStart < 0;
				var map = insideParsed ? CollectTableAliases(env.ActiveStatement) : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				if (manualStart >= 0 && env.ActiveStatement != null && map.Count > 0)
				{
					var priorText = sql.Substring(env.ActiveStatement.StartOffset, manualStart - env.ActiveStatement.StartOffset);
					var priorTokens = Tokenize(priorText);
					var toRemove = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
					foreach (var a in map.Keys)
						if (priorTokens.Contains(a)) toRemove.Add(a);
					foreach (var rem in toRemove) map.Remove(rem);
				}

				bool treatAsNew = (!insideParsed) || manualStart >= 0;
				if (treatAsNew && segment.Text.Length > 0)
				{
					var upToCaret = segment.Text.Substring(0, Math.Min(segment.Text.Length, Math.Max(0, caret - segment.Start)));
					foreach (var kv in FallbackAliasScan(upToCaret, metas))
						if (!map.ContainsKey(kv.Key)) map[kv.Key] = kv.Value;
				}

				if (env.ActiveStatement is UpdateStatement upStmt && upStmt.UpdateSpecification != null)
				{
					var target = upStmt.UpdateSpecification.Target as NamedTableReference;
					if (target != null)
					{
						var fullTarget = GetFullName(target.SchemaObject);
						var key = InternalTablePrefix + fullTarget;
						if (!map.ContainsKey(key)) map[key] = fullTarget;
						var alias = target.Alias?.Value ?? target.SchemaObject.BaseIdentifier?.Value;
						if (!string.IsNullOrEmpty(alias) && !TokUtil.IsSqlKeyword(alias) && !map.ContainsKey(alias)) map[alias] = fullTarget;
					}
				}
				else if (segment.Text.ToLowerInvariant().Contains("update"))
				{
					var updateTableName = ExtractUpdateTableName(segment.Text, caret - segment.Start);
					if (!string.IsNullOrEmpty(updateTableName))
					{
						var foundMeta = metas.FirstOrDefault(m => string.Equals(m.TableName, updateTableName, StringComparison.OrdinalIgnoreCase) || string.Equals($"{m.SchemaName}.{m.TableName}", updateTableName, StringComparison.OrdinalIgnoreCase) || string.Equals($"{m.DBName}.{m.SchemaName}.{m.TableName}", updateTableName, StringComparison.OrdinalIgnoreCase));
						string fullKey = updateTableName;
						if (foundMeta != null)
						{
							if (!string.IsNullOrEmpty(foundMeta.DBName) && !string.IsNullOrEmpty(foundMeta.SchemaName)) fullKey = $"{foundMeta.DBName}.{foundMeta.SchemaName}.{foundMeta.TableName}";
							else if (!string.IsNullOrEmpty(foundMeta.SchemaName)) fullKey = $"{foundMeta.SchemaName}.{foundMeta.TableName}";
							else fullKey = foundMeta.TableName;
						}
						var key = InternalTablePrefix + fullKey;
						if (!map.ContainsKey(key)) map[key] = fullKey;
					}
				}
				return map;
			}

			public static List<string> AggregateColumns(IEnumerable<string> fullNames, Dictionary<string, IntellisenseTableContract> fullMeta)
			{
				return fullNames
					.Select(f => f != null && f.StartsWith(InternalTablePrefix) ? f.Substring(InternalTablePrefix.Length) : f)
					.Distinct(StringComparer.OrdinalIgnoreCase)
					.SelectMany(f => fullMeta.TryGetValue(f, out var m) && m.ColumnList != null ? m.ColumnList.Select(c => c.ColumnName) : Enumerable.Empty<string>())
					.Distinct(StringComparer.OrdinalIgnoreCase)
					.ToList();
			}

			private static Dictionary<string, string> CollectTableAliases(TSqlStatement stmt)
			{
				var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				foreach (var qs in ContextUtil.GetQuerySpecifications(stmt))
				{
					if (qs.FromClause == null) continue;
					foreach (var tr in qs.FromClause.TableReferences) CollectAlias(dict, tr);
				}
				if (stmt is UpdateStatement up && up.UpdateSpecification != null)
				{
					var target = up.UpdateSpecification.Target as NamedTableReference;
					if (target != null)
					{
						var full = GetFullName(target.SchemaObject);
						var alias = target.Alias?.Value ?? target.SchemaObject.BaseIdentifier?.Value;
						if (!string.IsNullOrEmpty(alias)) dict[alias] = full;
					}
				}
				return dict;
			}

			private static void CollectAlias(Dictionary<string, string> dict, TableReference tre)
			{
				if (tre is NamedTableReference ntr)
				{
					var full = GetFullName(ntr.SchemaObject);
					var alias = ntr.Alias?.Value ?? ntr.SchemaObject.BaseIdentifier?.Value;
					if (!string.IsNullOrEmpty(alias) && !TokUtil.IsSqlKeyword(alias) && !dict.ContainsKey(alias)) dict[alias] = full;
				}
				else if (tre is QualifiedJoin qj)
				{
					CollectAlias(dict, qj.FirstTableReference);
					CollectAlias(dict, qj.SecondTableReference);
				}
				else if (tre is QueryDerivedTable qdt)
				{
					var alias = qdt.Alias?.Value;
					if (!string.IsNullOrEmpty(alias) && !TokUtil.IsSqlKeyword(alias) && !dict.ContainsKey(alias)) dict[alias] = alias;
				}
			}

			private static string GetFullName(SchemaObjectName so)
			{
				if (so == null) return string.Empty;
				var parts = new List<string>();
				if (so.DatabaseIdentifier != null && !string.IsNullOrEmpty(so.DatabaseIdentifier.Value)) parts.Add(so.DatabaseIdentifier.Value);
				if (so.SchemaIdentifier != null && !string.IsNullOrEmpty(so.SchemaIdentifier.Value)) parts.Add(so.SchemaIdentifier.Value);
				if (so.BaseIdentifier != null && !string.IsNullOrEmpty(so.BaseIdentifier.Value)) parts.Add(so.BaseIdentifier.Value);
				return string.Join(".", parts);
			}
		}

		private static class IdentifierUtil
		{
			public static IEnumerable<string> ExtractIdentifierChain(string sql, int dotIndex, int segmentStart)
			{
				int scan = dotIndex - 1;
				while (scan >= segmentStart)
				{
					char ch = sql[scan];
					if (char.IsWhiteSpace(ch) || ch == '\n' || ch == '\r' || ch == ',' || ch == '(' || ch == ')' || ch == '=' || ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '<' || ch == '>') break;
					scan--;
				}
				int idStart = Math.Max(segmentStart, scan + 1);
				string chain = sql.Substring(idStart, dotIndex - idStart);
				return chain.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		private static class SegmentationUtil
		{
			public static LogicalSegment GetLogicalSegment(string sql, int caret)
			{
				var starts = new List<int>();
				AddStartIfKeyword(sql, 0, starts);
				for (int i = 0; i < sql.Length; i++)
				{
					if (sql[i] == '\n' || sql[i] == '\r')
					{
						int pos = i + 1;
						while (pos < sql.Length && char.IsWhiteSpace(sql[pos]) && sql[pos] != '\n' && sql[pos] != '\r') pos++;
						AddStartIfKeyword(sql, pos, starts);
					}
				}
				if (starts.Count == 0) starts.Add(0);
				starts.Sort();
				int segStart = starts.Where(s => s <= caret).DefaultIfEmpty(0).Last();
				int segEndCandidate = starts.Where(s => s > segStart).DefaultIfEmpty(sql.Length).First() - 1;
				if (segEndCandidate < segStart) segEndCandidate = sql.Length - 1;
				return new LogicalSegment { Start = segStart, End = segEndCandidate };
			}
			private static void AddStartIfKeyword(string sql, int pos, List<int> list)
			{
				if (pos >= sql.Length) return;
				if (!TopLevelStatementKeywords.Any(kw => IsKeywordAt(sql, pos, kw))) return;
				if (!list.Contains(pos)) list.Add(pos);
			}
			private static bool IsKeywordAt(string sql, int pos, string kw)
			{
				if (pos + kw.Length > sql.Length) return false;
				if (!sql.Substring(pos, kw.Length).Equals(kw, StringComparison.OrdinalIgnoreCase)) return false;
				int next = pos + kw.Length;
				if (next < sql.Length && (char.IsLetterOrDigit(sql[next]) || sql[next] == '_')) return false;
				return true;
			}
		}

		private static class ParserUtil
		{
			public static ParsedEnvironment Parse(string sql, int caret)
			{
				IList<ParseError> errors;
				var parser = new TSql150Parser(false);
				TSqlFragment root;
				using (var reader = new StringReader(sql))
				{
					root = parser.Parse(reader, out errors);
				}
				var statements = root != null ? ExtractStatements(root).ToList() : new List<TSqlStatement>();
				var active = statements.FirstOrDefault(s => caret >= s.StartOffset && caret < s.StartOffset + s.FragmentLength);
				return new ParsedEnvironment { Root = root, Statements = statements, ActiveStatement = active };
			}

			private static IEnumerable<TSqlStatement> ExtractStatements(TSqlFragment root)
			{
				var list = new List<TSqlStatement>();
				root.Accept(new StatementCollector(list));
				return list;
			}

			private class StatementCollector : TSqlFragmentVisitor
			{
				private readonly List<TSqlStatement> _list;
				public StatementCollector(List<TSqlStatement> list) { _list = list; }
				public override void Visit(TSqlStatement node) { _list.Add(node); }
			}
		}

		private static class TokUtil
		{
			public static string[] SplitTokens(string text)
			{
				if (string.IsNullOrEmpty(text)) return Array.Empty<string>();
				return text.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			}
			public static string ToLower(string s) => string.IsNullOrEmpty(s) ? string.Empty : s.ToLowerInvariant();
			public static bool IsSqlKeyword(string s)
			{
				if (string.IsNullOrWhiteSpace(s)) return false;
				var val = s.Trim("[] \")".ToCharArray()).ToUpperInvariant();
				var known = new HashSet<string>(new[]
				{
					SqlTokens.Select, SqlTokens.Update, SqlTokens.Delete, SqlTokens.Insert, SqlTokens.Merge,
					SqlTokens.From, SqlTokens.Where, SqlTokens.Group, SqlTokens.Order, SqlTokens.By,
					SqlTokens.Join, SqlTokens.Inner, SqlTokens.Left, SqlTokens.Right, SqlTokens.Full,
					SqlTokens.Outer, SqlTokens.Cross, SqlTokens.Apply, SqlTokens.On, SqlTokens.Into, SqlTokens.Values,
					SqlTokens.Declare, SqlTokens.Set
				}, StringComparer.OrdinalIgnoreCase);
				if (known.Contains(val)) return true;
				// include reserved keywords if provider available
				try
				{
					var reserved = new HashSet<string>(TSqlReservedKeywordsProvider.GetAll(), StringComparer.OrdinalIgnoreCase);
					return reserved.Contains(val);
				}
				catch { return false; }
			}
		}
	}
}
