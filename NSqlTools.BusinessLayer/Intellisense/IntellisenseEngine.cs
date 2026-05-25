using Microsoft.SqlServer.TransactSql.ScriptDom;
using NSqlTools.Types.IntellisenseContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSqlTools.BusinessLayer.Intellisense
{
	public static partial class SimpleSqlIntellisenseEngine
	{
		private const string JoinKeyword = "join";
		private const string OnKeyword = "on";
		private const string ValuesKeyword = "values";

		public static IEnumerable<string> GetSuggestions(string sql, int caret, IEnumerable<IntellisenseDatabaseContract> databases, IntellisenseDatabaseContract currentDatabase)
		{
			var dbList = databases == null ? new List<IntellisenseDatabaseContract>() : databases.ToList();
			var tableList = new List<IntellisenseTableContract>();
			foreach (var db in dbList)
			{
				if (db?.TableList == null) continue;
				foreach (var t in db.TableList)
				{
					if (t == null) continue;
					if (string.IsNullOrEmpty(t.DBName)) t.DBName = db.DbName;
					tableList.Add(t);
				}
			}
			if (currentDatabase?.TableList != null)
			{
				foreach (var t in currentDatabase.TableList)
				{
					if (t == null) continue;
					var exists = tableList.Any(x => ReferenceEquals(x, t)) || tableList.Any(x => string.Equals(x.TableName, t.TableName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.SchemaName, t.SchemaName, StringComparison.OrdinalIgnoreCase));
					if (!exists)
					{
						if (string.IsNullOrEmpty(t.DBName)) t.DBName = currentDatabase.DbName;
						tableList.Add(t);
					}
				}
			}
			return GetSuggestions(sql, caret, tableList, currentDatabase?.DbName);
		}

		public static IEnumerable<string> GetSuggestions(string sql, int caret, IEnumerable<IntellisenseTableContract> tables, string currentDatabaseName = null)
		{
			// Basic validation
			if (string.IsNullOrWhiteSpace(sql) || caret < 0 || caret > sql.Length) return Enumerable.Empty<string>();
			if (TextUtil.IsInsideString(sql, caret)) return Enumerable.Empty<string>();

			// Build shared environment for suggestion pipeline
			BuildEnvironment(sql, caret, tables,
				out var metaList,
				out var fullMeta,
				out var _,
				out var segment,
				out var prefix,
				out var noPrefix,
				out var aliases,
				out var variables,
				out var caretInSeg,
				out var parseEnv);

			return BuildSuggestionsInternal(sql, caret, currentDatabaseName, metaList, fullMeta, segment, prefix, noPrefix, aliases, variables, caretInSeg, parseEnv);
		}

		// ===== Helper methods extracted from GetSuggestions for readability =====

		private static void BuildEnvironment(
			string sql,
			int caret,
			IEnumerable<IntellisenseTableContract> tables,
			out List<IntellisenseTableContract> metaList,
			out Dictionary<string, IntellisenseTableContract> fullMeta,
			out LogicalSegment logicalSeg,
			out SegmentInfo segment,
			out string prefix,
			out bool noPrefix,
			out Dictionary<string, string> aliases,
			out List<string> variables,
			out int caretInSeg,
			out ParsedEnvironment parseEnv)
		{
			metaList = tables == null ? new List<IntellisenseTableContract>() : tables.ToList();
			fullMeta = MetaUtil.BuildMetaLookup(metaList);
			var localParseEnv = ParserUtil.Parse(sql, caret);
			logicalSeg = SegmentationUtil.GetLogicalSegment(sql, caret);
			if (localParseEnv.ActiveStatement != null && (localParseEnv.ActiveStatement.StartOffset < logicalSeg.Start || localParseEnv.ActiveStatement.StartOffset + localParseEnv.ActiveStatement.FragmentLength - 1 > logicalSeg.End))
				localParseEnv.ActiveStatement = null; // statement leaking into other segment

			segment = BuildSegment(sql, logicalSeg);
			prefix = TextUtil.GetPrefix(sql, caret);
			noPrefix = prefix.Length == 0;
			aliases = AliasUtil.BuildAliasMap(localParseEnv, segment, sql, caret, metaList, localParseEnv.ActiveStatement == null ? logicalSeg.Start : -1);
			variables = VariableCollector.CollectVariables(sql, caret) ?? new List<string>();
			var procParams = CollectProcedureParameters(sql);
			if (procParams.Count > 0)
				variables = variables.Concat(procParams).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
			caretInSeg = Math.Max(0, caret - segment.Start);
			parseEnv = localParseEnv;
		}

		private static IEnumerable<string> BuildSuggestionsInternal(
			string sql,
			int caret,
			string currentDatabaseName,
			List<IntellisenseTableContract> metaList,
			Dictionary<string, IntellisenseTableContract> fullMeta,
			SegmentInfo segment,
			string prefix,
			bool noPrefix,
			Dictionary<string, string> aliases,
			List<string> variables,
			int caretInSeg,
			ParsedEnvironment parseEnv)
		{
			var preStageSuggestions = CollectPreStageSuggestions(sql, caret, segment, metaList, currentDatabaseName, prefix, noPrefix);

			if (!TextUtil.IsAfterDot(sql, caret) && caretInSeg > 0)
			{
				var nonDot = HandleNonDotChain(sql, segment, caretInSeg, aliases, fullMeta, metaList, prefix, noPrefix, currentDatabaseName);
				if (nonDot != null) return nonDot;
			}

			if (TextUtil.IsAfterDot(sql, caret))
			{
				var dotResult = HandleDotStage(sql, segment, aliases, fullMeta, metaList, prefix, noPrefix, currentDatabaseName, caret);
				if (dotResult != null) return dotResult;
			}

			var allColumns = AliasUtil.AggregateColumns(aliases.Values, fullMeta);
			var context = ContextDetector.Detect(sql, caret, parseEnv, segment);
			context = ForceWhereContextForDml(segment, caret, context);
			context = ForceJoinOnFallbackContext(sql, caret, segment, caretInSeg, context);
			context = ForceInsertValuesContext(segment, caret, context);
			HydrateColumnsForContext(sql, caret, segment, metaList, fullMeta, parseEnv, ref allColumns, ref context);
			context = MaybeOverrideFromAndJoinContext(sql, caret, segment, caretInSeg, metaList, currentDatabaseName, prefix, noPrefix, context, out var fromJoinOverride);
			if (fromJoinOverride != null && fromJoinOverride.Any()) return fromJoinOverride;

			EnsureOrderGroupColumns(context, aliases, metaList, fullMeta, ref allColumns);
			ForceOrderGroupContext(segment, caret, ref context);

			bool typingAfterAliasDot = DetectTypingAfterAliasDot(segment, caretInSeg, sql, aliases);
			var final = SuggestionUtil.BuildFinalSuggestions(
				context,
				aliases,
				metaList,
				allColumns,
				fullMeta,
				prefix,
				noPrefix,
				segment.Text,
				variables,
				caret - segment.Start,
				currentDatabaseName,
				typingAfterAliasDot).ToList();

			if ((final == null || final.Count == 0)
				&& TryBuildJoinOnFallbackSuggestions(sql, segment, caret, context, allColumns, aliases, fullMeta, prefix, noPrefix, out var joinFallback))
			{
				final = joinFallback;
			}
			if (preStageSuggestions.Count > 0)
				final = final.Concat(preStageSuggestions).Distinct(StringComparer.OrdinalIgnoreCase).ToList();

			// Snippet önerileri (sadece yeni sorgu başında, en üstte)
			var snippetSuggestions = BuildSnippetSuggestionsForQueryStart(sql, caret, segment);
			if (snippetSuggestions.Count > 0)
			{
				// Snippet'ları en üste al; tekrarları (case-insensitive) önle
				var existing = new HashSet<string>(final, StringComparer.OrdinalIgnoreCase);
				var prepend = snippetSuggestions.Where(s => !existing.Contains(s)).ToList();
				if (prepend.Count > 0)
					final = prepend.Concat(final).ToList();
			}

			return PostProcessSuggestions(final, context, aliases, metaList, fullMeta, prefix, noPrefix, segment.Text, typingAfterAliasDot, currentDatabaseName);
		}

		private static List<string> CollectPreStageSuggestions(string sql, int caret, SegmentInfo segment, List<IntellisenseTableContract> metaList, string currentDatabaseName, string prefix, bool noPrefix)
		{
			var list = new List<string>();
			if (!IsAfterKeyword(sql, caret, out var _)) return list;
			var specialized = FromContextHandler.SuggestFrom(segment.Text, caret - segment.Start, metaList, prefix, noPrefix, currentDatabaseName);
			if (specialized.Any()) list.AddRange(specialized);
			var kwSuggest = KeywordStageSuggest(metaList, currentDatabaseName, prefix, noPrefix);
			if (kwSuggest.Any()) list.AddRange(kwSuggest);
			return list;
		}

		private static SqlContext ForceWhereContextForDml(SegmentInfo segment, int caret, SqlContext context)
		{
			try
			{
				var segLower = segment.Text.ToLowerInvariant();
				int caretInSegLocal = caret - segment.Start;
				int whereIdx = segLower.LastIndexOf(" where ", StringComparison.Ordinal);
				if (whereIdx < 0) whereIdx = segLower.LastIndexOf(" where\r", StringComparison.Ordinal);
				if (whereIdx < 0) whereIdx = segLower.LastIndexOf(" where\n", StringComparison.Ordinal);
				if (whereIdx < 0 && segLower.EndsWith(" where")) whereIdx = caretInSegLocal - 6;
				if (whereIdx >= 0 && caretInSegLocal > whereIdx + 6)
				{
					var beforeWhere = segLower.Substring(0, whereIdx);
					bool hasDelete = beforeWhere.Contains("delete");
					bool hasUpdate = beforeWhere.Contains("update");
					if (hasDelete || hasUpdate)
					{
						context = SqlContext.Where;
					}
				}
			}
			catch
			{
				// ignored
			}

			return context;
		}

		private static SqlContext ForceJoinOnFallbackContext(string sql, int caret, SegmentInfo segment, int caretInSeg, SqlContext context)
		{
			// Parser bazen JOIN ve ON farkli satirlarda oldugunda JoinOn baglamini veremiyor.
			// Segment içinde son JOIN ve ON konumlarını bulup, caret ON'dan sonra ise
			// bağlamı JoinOn'a zorluyoruz.
			if (context == SqlContext.JoinOn) return context;

			// Use the whole SQL up to caret so we detect JOIN written in previous segments/lines.
			try
			{
				var upToCaret = caret > 0 && caret <= sql.Length ? sql.Substring(0, caret).ToLowerInvariant() : sql.ToLowerInvariant();
				int lastJoinIdx = FindLastKeyword(upToCaret, JoinKeyword);
				int lastOnIdx = FindLastKeyword(upToCaret, OnKeyword);
				if (lastJoinIdx < 0 || lastOnIdx < 0 || lastOnIdx <= lastJoinIdx) return context;

				int onEndPos = lastOnIdx + OnKeyword.Length;
				if (caret >= onEndPos)
					context = SqlContext.JoinOn;
			}
			catch
			{
				// ignored
			}

			return context;
		}

		private static SqlContext ForceInsertValuesContext(SegmentInfo segment, int caret, SqlContext context)
		{
			if (context == SqlContext.InsertValues || string.IsNullOrEmpty(segment.Text))
				return context;

			var caretInSeg = Math.Max(0, caret - segment.Start);
			var upToCaret = caretInSeg <= segment.Text.Length ? segment.Text.Substring(0, caretInSeg) : segment.Text;
			if (upToCaret.Length == 0) return context;

			var upLower = upToCaret.ToLowerInvariant();
			int lastValuesIdx = FindLastKeyword(upLower, ValuesKeyword);
			if (lastValuesIdx < 0) return context;

			int depth = 0;
			bool enteredValuesParen = false;
			for (int i = lastValuesIdx; i < upToCaret.Length; i++)
			{
				char ch = upToCaret[i];
				if (ch == '(')
				{
					enteredValuesParen = true;
					depth++;
				}
				else if (ch == ')')
				{
					if (depth > 0) depth--;
					if (depth == 0 && enteredValuesParen)
						break;
				}
			}

			if (enteredValuesParen && depth > 0)
			{
				context = SqlContext.InsertValues;
			}

			return context;
		}

		private static void HydrateColumnsForContext(string sql, int caret, SegmentInfo segment, List<IntellisenseTableContract> metaList, Dictionary<string, IntellisenseTableContract> fullMeta, ParsedEnvironment parseEnv, ref List<string> allColumns, ref SqlContext context)
		{
			// INSERT column list
			if (context == SqlContext.InsertInto)
			{
				var segLower = segment.Text.ToLowerInvariant();
				int openParen = segLower.IndexOf('(');
				if (openParen >= 0 && (caret - segment.Start) > openParen)
				{
					context = SqlContext.InsertColumns;
					HydrateInsertColumns(segment, caret, fullMeta, ref allColumns);
				}
			}
			if (context == SqlContext.InsertColumns)
				HydrateInsertColumns(segment, caret, fullMeta, ref allColumns);

			// UPDATE SET list
			if (parseEnv.ActiveStatement is UpdateStatement upDom && context == SqlContext.SelectList)
				HydrateUpdateColumnsDom(upDom, fullMeta, ref allColumns);
			if (context == SqlContext.SelectList && segment.Text.ToLowerInvariant().Contains("update"))
				HydrateUpdateColumnsFallback(segment, caret, fullMeta, ref allColumns, metaList);

			// DELETE ... WHERE
			if (context == SqlContext.Where && (allColumns == null || allColumns.Count == 0))
			{
				string deleteTableName = null;
				var lower = segment.Text.ToLowerInvariant();
				int delIdx = lower.IndexOf("delete", StringComparison.Ordinal);
				int fromIdx = lower.IndexOf(" from ", delIdx >= 0 ? delIdx : 0, StringComparison.Ordinal);
				if (fromIdx >= 0)
				{
					int start = fromIdx + 6;
					int end = start;
					while (end < segment.Text.Length)
					{
						char ch = segment.Text[end];
						if (char.IsLetterOrDigit(ch) || ch == '_' || ch == '.' || ch == '[' || ch == ']') { end++; continue; }
						break;
					}
					deleteTableName = segment.Text.Substring(start, Math.Max(0, end - start)).Trim('[', ']');
				}
				if (!string.IsNullOrEmpty(deleteTableName))
				{
					var searchKeys = TableNameExtractor.BuildSearchKeys(deleteTableName);
					foreach (var key in searchKeys)
					{
						if (fullMeta.TryGetValue(key, out var meta) && meta.ColumnList != null)
						{
							allColumns = meta.ColumnList.Select(c => c.ColumnName).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
							break;
						}
					}
				}
			}

			// SELECT list enrichment
			if (context == SqlContext.SelectList)
				EnrichSelectList(segment, metaList, fullMeta, caret, ref allColumns);
		}

		private static SqlContext MaybeOverrideFromAndJoinContext(string sql, int caret, SegmentInfo segment, int caretInSeg, List<IntellisenseTableContract> metaList, string currentDatabaseName, string prefix, bool noPrefix, SqlContext context, out IEnumerable<string> overrideSuggestions)
		{
			overrideSuggestions = null;
			if (context == SqlContext.From)
			{
				var uptoCaret = segment.Text.Substring(0, Math.Max(0, caret - segment.Start));
				var trimmedEnd = uptoCaret.TrimEnd();
				var lastToken = trimmedEnd.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
				var plainLast = (lastToken ?? string.Empty).Trim('[', ']');
				var partsLast = plainLast.Split('.');
				bool isFullQualifiedTable = partsLast.Length == 3 && metaList.Any(t =>
					string.Equals(t.DBName ?? string.Empty, partsLast[0], StringComparison.OrdinalIgnoreCase)
					&& string.Equals(t.SchemaName ?? string.Empty, partsLast[1], StringComparison.OrdinalIgnoreCase)
					&& string.Equals(t.TableName ?? string.Empty, partsLast[2], StringComparison.OrdinalIgnoreCase));
				if (!isFullQualifiedTable)
				{
					var specialized = FromContextHandler.SuggestFrom(segment.Text, caret - segment.Start, metaList, prefix, noPrefix, currentDatabaseName);
					if (specialized.Any()) { overrideSuggestions = specialized; return context; }
				}
			}
			if (context == SqlContext.JoinOn || context == SqlContext.From)
			{
				var segLower = segment.Text.ToLowerInvariant();
				int caretInSegLocal = caret - segment.Start;
				int lastJoinIdx = FindLastKeyword(segLower, JoinKeyword);
				if (lastJoinIdx >= 0 && caretInSegLocal >= lastJoinIdx + JoinKeyword.Length)
				{
					int nextOnIdx = FindKeyword(segLower, OnKeyword, lastJoinIdx + JoinKeyword.Length);
					bool beforeOn = nextOnIdx < 0 || caretInSegLocal <= nextOnIdx;
					if (beforeOn)
					{
						var specialized = FromContextHandler.SuggestFrom(segment.Text, caretInSegLocal, metaList, prefix, noPrefix, currentDatabaseName);
						if (specialized.Any()) { overrideSuggestions = specialized; return context; }
					}
					else
					{
						// ON'dan SONRA: normal akışın kolon/alias'ları işlemesine izin ver
						return context;
					}
				}
				else
				{
					var uptoCaret = segment.Text.Substring(0, Math.Max(0, caretInSegLocal));
					var trimmedEnd = uptoCaret.TrimEnd();
					var lastToken = trimmedEnd.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
					// Eğer kullanıcı sadece JOIN yazdıysa (ON'dan önce), tabloları önermeye devam et
					if (!string.IsNullOrEmpty(lastToken) && lastToken.Equals("join", StringComparison.OrdinalIgnoreCase))
					{
						var specialized = FromContextHandler.SuggestFrom(segment.Text, caretInSegLocal, metaList, prefix, noPrefix, currentDatabaseName);
						if (specialized.Any()) { overrideSuggestions = specialized; return context; }
					}
				}
			}
			return context;
		}

		private static SegmentInfo BuildSegment(string sql, LogicalSegment logicalSeg)
		{
			return new SegmentInfo
			{
				Start = logicalSeg.Start,
				End = logicalSeg.End,
				Text = logicalSeg.Start < sql.Length ? sql.Substring(logicalSeg.Start, Math.Max(0, logicalSeg.End - logicalSeg.Start + 1)) : string.Empty
			};
		}

		private static IEnumerable<string> HandleNonDotChain(string sql, SegmentInfo segment, int caretInSeg, Dictionary<string, string> aliases, Dictionary<string, IntellisenseTableContract> fullMeta, List<IntellisenseTableContract> metaList, string prefix, bool noPrefix, string currentDatabaseName)
		{
			var upToCaretSeg = caretInSeg <= segment.Text.Length ? segment.Text.Substring(0, caretInSeg) : segment.Text;
			var upLower = upToCaretSeg.ToLowerInvariant();
			if (DotChainHelper.ShouldSkipDotChainHandling(upLower)) return null;
			int lastDot = upToCaretSeg.LastIndexOf('.');
			if (lastDot < 0) return null;
			int lastNewline = Math.Max(upToCaretSeg.LastIndexOf('\n'), upToCaretSeg.LastIndexOf('\r'));
			if (lastNewline >= 0 && lastDot <= lastNewline) return null;

			var afterDot = upToCaretSeg.Substring(lastDot + 1);
			bool hasOperatorAfterDot = afterDot.Contains("=") || afterDot.Contains("<") || afterDot.Contains(">") || afterDot.Contains("!");
			if (!hasOperatorAfterDot)
			{
				int scan = lastDot - 1;
				while (scan >= 0 && (char.IsLetterOrDigit(upToCaretSeg[scan]) || upToCaretSeg[scan] == '_' || upToCaretSeg[scan] == ']' || upToCaretSeg[scan] == '[')) scan--;
				string aliasToken = upToCaretSeg.Substring(scan + 1, lastDot - (scan + 1)).Trim('[', ']');
				IEnumerable<string> cols = null;
				if (!string.IsNullOrEmpty(aliasToken) && aliases.TryGetValue(aliasToken, out var aliasFull))
				{
					var resolved = aliasFull.StartsWith(InternalTablePrefix) ? aliasFull.Substring(InternalTablePrefix.Length) : aliasFull;
					if (fullMeta.TryGetValue(resolved, out var metaResolved) && metaResolved.ColumnList != null)
						cols = metaResolved.ColumnList.Select(c => c.ColumnName);
					else
					{
						var partsVar = resolved.Split('.');
						var variants = new List<string>();
						if (partsVar.Length == 3) variants.Add(partsVar[1] + "." + partsVar[2]);
						if (partsVar.Length >= 1) variants.Add(partsVar[partsVar.Length - 1]);
						foreach (var v in variants)
						{
							if (fullMeta.TryGetValue(v, out var metaVar) && metaVar.ColumnList != null)
							{
								cols = metaVar.ColumnList.Select(c => c.ColumnName);
								break;
							}
						}
					}
				}
				if (cols != null) return SuggestionUtil.FilterByPrefix(cols, prefix, false);
				return SuggestionUtil.FilterByPrefix(cols, prefix, noPrefix);
			}

			var localAfterDot = upToCaretSeg.Substring(lastDot + 1);
			int lpScan = 0;
			while (lpScan < localAfterDot.Length && (char.IsLetterOrDigit(localAfterDot[lpScan]) || localAfterDot[lpScan] == '_' || localAfterDot[lpScan] == ']' || localAfterDot[lpScan] == '[')) lpScan++;
			var localPrefix = (lpScan > 0 ? localAfterDot.Substring(0, lpScan) : string.Empty).Trim('[', ']');
			var chainUpToLastDot = IdentifierUtil.ExtractIdentifierChain(sql, segment.Start + lastDot - 1, 0).ToArray();
			if (!string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(localPrefix) && !localPrefix.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				return null;
			var chainWithCurrent = string.IsNullOrEmpty(localPrefix) ? chainUpToLastDot : chainUpToLastDot.Concat(new[] { localPrefix }).ToArray();
			if (chainWithCurrent.Length > 0)
			{
				var dotSuggest = DotStageSuggest(chainWithCurrent, metaList, fullMeta, localPrefix, string.IsNullOrEmpty(localPrefix) ? true : false, currentDatabaseName);
				if (dotSuggest != null && dotSuggest.Any()) return dotSuggest;
			}
			return null;
		}

		private static IEnumerable<string> HandleDotStage(string sql, SegmentInfo segment, Dictionary<string, string> aliases, Dictionary<string, IntellisenseTableContract> fullMeta, List<IntellisenseTableContract> metaList, string prefix, bool noPrefix, string currentDatabaseName, int caret)
		{
			var chain = IdentifierUtil.ExtractIdentifierChain(sql, caret - 1, segment.Start).Select(p => p.Trim('[', ']')).ToArray();
			if (chain.Length == 0) return Enumerable.Empty<string>();
			if (chain.Length == 1 && aliases.TryGetValue(chain[0], out var fullAlias))
			{
				var resolved = fullAlias.StartsWith(InternalTablePrefix) ? fullAlias.Substring(InternalTablePrefix.Length) : fullAlias;
				IEnumerable<string> cols = null;
				if (fullMeta.TryGetValue(resolved, out var metaResolved) && metaResolved.ColumnList != null)
					cols = metaResolved.ColumnList.Select(c => c.ColumnName);
				else
				{
					var partsVar = resolved.Split('.');
					var variants = new List<string>();
					if (partsVar.Length == 3) variants.Add(partsVar[1] + "." + partsVar[2]);
					if (partsVar.Length >= 1) variants.Add(partsVar[partsVar.Length - 1]);
					foreach (var v in variants)
					{
						if (fullMeta.TryGetValue(v, out var metaVar) && metaVar.ColumnList != null)
						{
							cols = metaVar.ColumnList.Select(c => c.ColumnName);
							break;
						}
					}
				}
				return cols != null ? SuggestionUtil.FilterByPrefix(cols, prefix, noPrefix) : Enumerable.Empty<string>();
			}
			return DotStageSuggest(chain, metaList, fullMeta, prefix, noPrefix, currentDatabaseName);
		}

		private static bool DetectTypingAfterAliasDot(SegmentInfo segment, int caretInSeg, string sql, Dictionary<string, string> aliases)
		{
			bool typingAfterAliasDot = false;
			if (caretInSeg > 0)
			{
				var upToCaretSeg = caretInSeg <= segment.Text.Length ? segment.Text.Substring(0, caretInSeg) : segment.Text;
				int scanBack = upToCaretSeg.Length - 1;
				while (scanBack >= 0)
				{
					char ch = upToCaretSeg[scanBack];
					if (char.IsLetterOrDigit(ch) || ch == '_' || ch == '[' || ch == ']' || ch == '.') { scanBack--; continue; }
					break;
				}
				int tokenStart = Math.Max(0, scanBack + 1);
				var tokenSpan = upToCaretSeg.Substring(tokenStart);
				int dotIdx = tokenSpan.LastIndexOf('.');
				if (dotIdx >= 0)
				{
					var beforeDot = tokenSpan.Substring(0, dotIdx).Trim('[', ']');
					if (!string.IsNullOrEmpty(beforeDot) && aliases.ContainsKey(beforeDot)) typingAfterAliasDot = true;
				}
				else if (TextUtil.IsAfterDot(sql, tokenStart + segment.Start + tokenSpan.Length))
				{
					int lastDot = upToCaretSeg.LastIndexOf('.');
					if (lastDot > 0)
					{
						int scan = lastDot - 1;
						while (scan >= 0 && (char.IsLetterOrDigit(upToCaretSeg[scan]) || upToCaretSeg[scan] == '_' || upToCaretSeg[scan] == ']' || upToCaretSeg[scan] == '[')) scan--;
						string tokenBeforeDot = upToCaretSeg.Substring(scan + 1, lastDot - (scan + 1)).Trim('[', ']');
						if (!string.IsNullOrEmpty(tokenBeforeDot) && aliases.ContainsKey(tokenBeforeDot)) typingAfterAliasDot = true;
					}
				}
			}
			return typingAfterAliasDot;
		}

		private static bool TryBuildJoinOnFallbackSuggestions(
			string sql,
			SegmentInfo segment,
			int caret,
			SqlContext context,
			List<string> allColumns,
			Dictionary<string, string> aliases,
			Dictionary<string, IntellisenseTableContract> fullMeta,
			string prefix,
			bool noPrefix,
			out List<string> fallback)
		{
			fallback = null;
			// If parser/context detection doesn't mark JoinOn, check whole SQL as fallback
			if (!IsJoinOnCaretPosition(segment, caret, context))
			{
				// look in full sql up to caret
				var upLower = caret > 0 && caret <= sql.Length ? sql.Substring(0, caret).ToLowerInvariant() : sql.ToLowerInvariant();
				int lastJoinIdx = FindLastKeyword(upLower, JoinKeyword);
				int lastOnIdx = FindLastKeyword(upLower, OnKeyword);
				if (lastJoinIdx < 0 || lastOnIdx < 0 || lastOnIdx <= lastJoinIdx) return false;
				int onEndPos = lastOnIdx + OnKeyword.Length;
				if (caret < onEndPos) return false;
			}

			try
			{
				// Önce caret etrafındaki token'dan hangi alias ile çalıştığımızı bulmaya çalış
				string currentAlias = null;
				try
				{
					int caretInSegLocal = Math.Max(0, caret - segment.Start);
					var upToCaretSeg = caretInSegLocal <= (segment.Text ?? string.Empty).Length
						? (segment.Text ?? string.Empty).Substring(0, caretInSegLocal)
						: (segment.Text ?? string.Empty);
					int scanBack = upToCaretSeg.Length - 1;
					while (scanBack >= 0)
					{
						char ch = upToCaretSeg[scanBack];
						if (char.IsLetterOrDigit(ch) || ch == '_' || ch == '[' || ch == ']' || ch == '.') { scanBack--; continue; }
						break;
					}
					int tokenStart = Math.Max(0, scanBack + 1);
					var tokenSpan = upToCaretSeg.Substring(tokenStart);
					int dotIdx = tokenSpan.LastIndexOf('.');
					if (dotIdx >= 0)
					{
						var beforeDot = tokenSpan.Substring(0, dotIdx).Trim('[', ']');
						if (!string.IsNullOrEmpty(beforeDot) && aliases.ContainsKey(beforeDot))
							currentAlias = beforeDot;
					}
				}
				catch
				{
					// ignored
				}

				var buffer = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				// Eğer spesifik bir alias yakaladıysak, sadece o alias'ın kolonlarını prefer et
				if (!string.IsNullOrEmpty(currentAlias) && aliases.TryGetValue(currentAlias, out var aliasFullName))
				{
					var full = aliasFullName?.StartsWith(InternalTablePrefix) == true
						? aliasFullName.Substring(InternalTablePrefix.Length)
						: aliasFullName;
					if (!string.IsNullOrEmpty(full) && fullMeta.TryGetValue(full, out var meta) && meta.ColumnList != null)
					{
						foreach (var col in meta.ColumnList)
						{
							if (string.IsNullOrWhiteSpace(col.ColumnName)) continue;
							buffer.Add(col.ColumnName);
							buffer.Add(currentAlias + "." + col.ColumnName);
						}
					}
				}
				else
				{
					// Eski davranış: alias ayrımı yapılamıyorsa, tüm alias'lardan kolon topla
					if (allColumns != null)
						foreach (var column in allColumns)
							buffer.Add(column);

					foreach (var kv in aliases)
					{
						var alias = kv.Key;
						if (alias.StartsWith(InternalTablePrefix)) continue;
						var full = kv.Value?.StartsWith(InternalTablePrefix) == true
							? kv.Value.Substring(InternalTablePrefix.Length)
							: kv.Value;
						if (string.IsNullOrEmpty(full)) continue;
						if (!fullMeta.TryGetValue(full, out var meta) || meta.ColumnList == null) continue;

						foreach (var col in meta.ColumnList)
						{
							if (string.IsNullOrWhiteSpace(col.ColumnName)) continue;
							buffer.Add(col.ColumnName);
							buffer.Add(alias + "." + col.ColumnName);
						}
					}
				}

				// If we still have no candidates (no aliases found), try to detect the table
				// referenced in the JOIN clause (between JOIN and ON) and add its columns.
				if (buffer.Count == 0)
				{
					try
					{
						var upLower = caret > 0 && caret <= sql.Length ? sql.Substring(0, caret).ToLowerInvariant() : sql.ToLowerInvariant();
						int lastJoinIdx = FindLastKeyword(upLower, JoinKeyword);
						int lastOnIdx = FindLastKeyword(upLower, OnKeyword);
						if (lastJoinIdx >= 0 && lastOnIdx > lastJoinIdx)
						{
							int start = lastJoinIdx + JoinKeyword.Length;
							string between = upLower.Substring(start, lastOnIdx - start).Trim();
							var firstTok = between.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
							if (!string.IsNullOrEmpty(firstTok))
							{
								var tableToken = firstTok.Trim('[', ']', ',');
								var searchKeys = TableNameExtractor.BuildSearchKeys(tableToken);
								foreach (var key in searchKeys)
								{
									if (fullMeta.TryGetValue(key, out var meta2) && meta2.ColumnList != null)
									{
										foreach (var col2 in meta2.ColumnList)
										{
											if (!string.IsNullOrWhiteSpace(col2.ColumnName)) buffer.Add(col2.ColumnName);
										}
										break;
									}
								}
							}
						}
					}
					catch
					{
						// ignored
					}
				}

				var filtered = SuggestionUtil.FilterByPrefix(buffer, prefix, noPrefix)
					.Distinct(StringComparer.OrdinalIgnoreCase)
					.ToList();
				if (filtered.Count == 0) return false;
				fallback = filtered;
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static bool IsJoinOnCaretPosition(SegmentInfo segment, int caret, SqlContext context)
		{
			if (string.IsNullOrEmpty(segment.Text)) return false;
			// Eğer bağlam zaten JoinOn ise direkt true dön.
			if (context == SqlContext.JoinOn) return true;

			// Segment içinde JOIN ve ON var mı kontrol et.
			var segLower = segment.Text.ToLowerInvariant();
			int caretInSeg = Math.Max(0, caret - segment.Start);
			int lastJoinIdx = FindLastKeyword(segLower, JoinKeyword);
			int lastOnIdx = FindLastKeyword(segLower, OnKeyword);
			if (lastOnIdx < 0 || lastOnIdx <= lastJoinIdx) return false;

			// Caret ON kelimesinin bitişinden sonra mı?
			int onEnd = lastOnIdx + OnKeyword.Length;
			return caretInSeg >= onEnd;
		}

		private static IEnumerable<string> DotStageSuggest(string[] parts, List<IntellisenseTableContract> metaList, Dictionary<string, IntellisenseTableContract> fullMeta, string prefix, bool noPrefix, string currentDatabaseName)
		{
			// If user is typing (there is a non-empty prefix) and there are at least 2 parts,
			// prefer the candidate set that was shown when caret was just after the previous dot
			// (i.e., same chain but with empty last token). Compute that set and filter it by current prefix.
			if (!noPrefix && parts.Length >= 2)
			{
				// simulate the suggestions immediately after the previous dot by removing the current token
				var baseParts = parts.Take(parts.Length - 1).ToArray();
				var baseSuggest = DotStageSuggest(baseParts, metaList, fullMeta, string.Empty, true, currentDatabaseName);
				if (baseSuggest != null && baseSuggest.Any())
					return SuggestionUtil.FilterByPrefix(baseSuggest, prefix, noPrefix);
			}

			if (parts.Length == 1)
			{
				var token = parts[0];
				// If token matches a schema in current database, suggest its tables
				if (!string.IsNullOrEmpty(currentDatabaseName) && metaList.Any(t => string.Equals(t.DBName ?? string.Empty, currentDatabaseName, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SchemaName ?? string.Empty, token, StringComparison.OrdinalIgnoreCase)))
					return SuggestionUtil.FilterByPrefix(metaList.Where(t => string.Equals(t.DBName ?? string.Empty, currentDatabaseName, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SchemaName ?? string.Empty, token, StringComparison.OrdinalIgnoreCase)).Select(t => t.TableName).Distinct(StringComparer.OrdinalIgnoreCase), prefix, noPrefix);

				// If token matches any schema in any DB, suggest tables under those schemas
				if (metaList.Any(t => string.Equals(t.SchemaName ?? string.Empty, token, StringComparison.OrdinalIgnoreCase)))
					return SuggestionUtil.FilterByPrefix(metaList.Where(t => string.Equals(t.SchemaName ?? string.Empty, token, StringComparison.OrdinalIgnoreCase)).Select(t => t.TableName).Distinct(StringComparer.OrdinalIgnoreCase), prefix, noPrefix);

				// If token matches a DB name, suggest its schemas (and also include current DB name)
				if (metaList.Any(t => string.Equals(t.DBName ?? string.Empty, token, StringComparison.OrdinalIgnoreCase)))
					return SuggestionUtil.FilterByPrefix(metaList.Where(t => string.Equals(t.DBName ?? string.Empty, token, StringComparison.OrdinalIgnoreCase)).Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase), prefix, noPrefix);

				// Fallback: schema prefix match
				return SuggestionUtil.FilterByPrefix(metaList.Where(t => (t.SchemaName ?? string.Empty).StartsWith(token, StringComparison.OrdinalIgnoreCase)).Select(t => t.TableName).Distinct(StringComparer.OrdinalIgnoreCase), prefix, noPrefix);
			}
			if (parts.Length == 2)
			{
				var first = parts[0]; var second = parts[1];
				// tables under db.schema (most specific)
				// tables under db.schema (most specific)
				// allow schema to be a prefix of actual schema name to keep table suggestions stable while typing
				var tablesUnderDbSchema = metaList
					.Where(t => string.Equals(t.DBName ?? string.Empty, first, StringComparison.OrdinalIgnoreCase)
							 && (string.IsNullOrEmpty(second) || (t.SchemaName ?? string.Empty).StartsWith(second, StringComparison.OrdinalIgnoreCase)))
					.Select(t => t.TableName)
					.Distinct(StringComparer.OrdinalIgnoreCase);
				// Prefer tables under db.schema whenever available (both when caret is just after dot and while typing table name)
				if (tablesUnderDbSchema.Any())
				{
					// Exclude exact table match to avoid re-suggesting what is already typed
					var filteredTables = tablesUnderDbSchema.Where(tn => !string.Equals(tn, second, StringComparison.OrdinalIgnoreCase));
					var result = SuggestionUtil.FilterByPrefix(filteredTables, prefix, noPrefix).ToList();
					if (result.Count > 0) return result;
					// If nothing left after exclusion, return empty to let keyword suggestions proceed
					return Enumerable.Empty<string>();
				}

				// If first matches a schema name, suggest tables under that schema (use second as prefix)
				var tablesUnderSchema = metaList
					.Where(t => string.Equals(t.SchemaName ?? string.Empty, first, StringComparison.OrdinalIgnoreCase))
					.Select(t => t.TableName)
					.Where(tn => string.IsNullOrEmpty(second) || tn.StartsWith(second, StringComparison.OrdinalIgnoreCase))
					.Distinct(StringComparer.OrdinalIgnoreCase);
				if (tablesUnderSchema.Any()) return SuggestionUtil.FilterByPrefix(tablesUnderSchema, prefix, noPrefix);

				// If first matches a DB name, prefer schema suggestions from that DB only when user is right after dot (second empty)
				var schemasUnderDb = metaList.Where(t => string.Equals(t.DBName ?? string.Empty, first, StringComparison.OrdinalIgnoreCase)).Select(t => t.SchemaName).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase);
				if (string.IsNullOrEmpty(second) && schemasUnderDb.Any())
				{
					return SuggestionUtil.FilterByPrefix(schemasUnderDb, prefix, noPrefix);
				}

				// Fallback to tables under db.schema
				var fallbackFiltered = tablesUnderDbSchema.Where(tn => !string.Equals(tn, second, StringComparison.OrdinalIgnoreCase));
				var fallbackResult = SuggestionUtil.FilterByPrefix(fallbackFiltered, prefix, noPrefix).ToList();
				return fallbackResult.Count > 0 ? fallbackResult : Enumerable.Empty<string>();
			}
			if (parts.Length >= 3)
			{
				var db = parts[0]; var schema = parts[1]; var tableToken = parts[2];
				// Always prefer table names under db.schema filtered by the third token to keep suggestions stable while typing
				var tablesUnderDbSchema3 = metaList
					.Where(t => string.Equals(t.DBName ?? string.Empty, db, StringComparison.OrdinalIgnoreCase)
							 && string.Equals(t.SchemaName ?? string.Empty, schema, StringComparison.OrdinalIgnoreCase))
					.Select(t => t.TableName)
					.Where(tn => string.IsNullOrEmpty(tableToken) || tn.StartsWith(tableToken, StringComparison.OrdinalIgnoreCase))
					.Distinct(StringComparer.OrdinalIgnoreCase);
				if (tablesUnderDbSchema3.Any())
					return SuggestionUtil.FilterByPrefix(tablesUnderDbSchema3, prefix, noPrefix);

				// If caret is just after a dot following a full table (noPrefix == true), then suggest columns
				var fullKey = string.IsNullOrEmpty(db)
					? (string.IsNullOrEmpty(schema) ? tableToken : schema + "." + tableToken)
					: db + "." + schema + "." + tableToken;
				if (fullMeta.TryGetValue(fullKey, out var metaResolved) && metaResolved.ColumnList != null)
				{
					return SuggestionUtil.FilterByPrefix(metaResolved.ColumnList.Select(c => c.ColumnName), prefix, noPrefix);
				}
			}
			return Enumerable.Empty<string>();
		}

		private static void HydrateInsertColumns(SegmentInfo segment, int caret, Dictionary<string, IntellisenseTableContract> fullMeta, ref List<string> allColumns)
		{
			var insertTableName = ExtractInsertTableName(segment.Text, caret - segment.Start);
			if (string.IsNullOrEmpty(insertTableName)) return;

			var searchKeys = TableNameExtractor.BuildSearchKeys(insertTableName);
			foreach (var key in searchKeys)
			{
				if (fullMeta.TryGetValue(key, out var meta) && meta.ColumnList != null)
				{
					allColumns = meta.ColumnList.Select(c => c.ColumnName).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
					break;
				}
			}
		}

		private static void HydrateUpdateColumnsDom(UpdateStatement upStmtDom, Dictionary<string, IntellisenseTableContract> fullMeta, ref List<string> allColumns)
		{
			var ntr = upStmtDom.UpdateSpecification?.Target as NamedTableReference;
			if (ntr == null) return;
			var so = ntr.SchemaObject;
			var partsList = new List<string>();
			if (so.DatabaseIdentifier != null && !string.IsNullOrEmpty(so.DatabaseIdentifier.Value)) partsList.Add(so.DatabaseIdentifier.Value);
			if (so.SchemaIdentifier != null && !string.IsNullOrEmpty(so.SchemaIdentifier.Value)) partsList.Add(so.SchemaIdentifier.Value);
			if (so.BaseIdentifier != null && !string.IsNullOrEmpty(so.BaseIdentifier.Value)) partsList.Add(so.BaseIdentifier.Value);
			var fullTarget = string.Join(".", partsList);
			AddColumnsFromMetaVariants(fullTarget, fullMeta, ref allColumns);
		}


		private static void HydrateUpdateColumnsFallback(SegmentInfo segment, int caret, Dictionary<string, IntellisenseTableContract> fullMeta, ref List<string> allColumns, List<IntellisenseTableContract> metaList)
		{
			var updateTableName = ExtractUpdateTableName(segment.Text, caret - segment.Start);
			if (string.IsNullOrEmpty(updateTableName)) return;

			var searchKeys = TableNameExtractor.BuildSearchKeys(updateTableName);
			foreach (var key in searchKeys)
			{
				if (fullMeta.TryGetValue(key, out var meta) && meta.ColumnList != null)
				{
					var updCols = meta.ColumnList.Select(c => c.ColumnName).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
					allColumns = (allColumns == null || allColumns.Count == 0) ? updCols : allColumns.Concat(updCols).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
					break;
				}

			}
		}

		private static void EnrichSelectList(SegmentInfo segment, List<IntellisenseTableContract> metaList, Dictionary<string, IntellisenseTableContract> fullMeta, int caret, ref List<string> allColumns)
		{
			var cols = SelectListColumnEnricher.CollectColumnsFromUpcomingTables(segment.Text, metaList, fullMeta, caret - segment.Start);
			if (cols.Count > 0)
				allColumns = (allColumns == null || allColumns.Count == 0) ? cols : allColumns.Concat(cols).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		}

		private static void EnsureOrderGroupColumns(SqlContext context, Dictionary<string, string> aliases, List<IntellisenseTableContract> metaList, Dictionary<string, IntellisenseTableContract> fullMeta, ref List<string> allColumns)
		{
			if (!(context == SqlContext.OrderBy || context == SqlContext.GroupBy)) return;
			if (allColumns != null && allColumns.Count > 0) return;
			var fromAliasCols = AliasUtil.AggregateColumns(aliases.Values, fullMeta);
			if (fromAliasCols.Count > 0) { allColumns = fromAliasCols; return; }
			allColumns = metaList.Where(m => m.ColumnList != null).SelectMany(m => m.ColumnList.Select(c => c.ColumnName)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		}

		private static void ForceOrderGroupContext(SegmentInfo segment, int caret, ref SqlContext context)
		{
			var segLower = segment.Text.ToLowerInvariant();
			int caretInSeg = caret - segment.Start;
			if (context != SqlContext.OrderBy)
			{
				int orderIdx = segLower.IndexOf(" order", StringComparison.Ordinal);
				if (orderIdx >= 0 && caretInSeg >= orderIdx + 6)
				{
					var afterOrder = segLower.Substring(orderIdx + 6, Math.Max(0, caretInSeg - (orderIdx + 6)));
					if (afterOrder.StartsWith("b") || afterOrder.Trim().StartsWith("by") || afterOrder.Length == 0 || (segLower.Contains(" order by ") && caretInSeg >= segLower.IndexOf(" order by ", StringComparison.Ordinal) + 10))
						context = SqlContext.OrderBy;
				}
			}
			if (context != SqlContext.GroupBy)
			{
				int groupIdx = segLower.IndexOf(" group", StringComparison.Ordinal);
				if (groupIdx >= 0 && caretInSeg >= groupIdx + 6)
				{
					var afterGroup = segLower.Substring(groupIdx + 6, Math.Max(0, caretInSeg - (groupIdx + 6)));
					if (afterGroup.StartsWith("b") || afterGroup.Trim().StartsWith("by") || afterGroup.Length == 0 || (segLower.Contains(" group by ") && caretInSeg >= segLower.IndexOf(" group by ", StringComparison.Ordinal) + 10))
						context = SqlContext.GroupBy;
				}
			}
		}

		private static void AddColumnsFromMetaVariants(string fullTarget, Dictionary<string, IntellisenseTableContract> fullMeta, ref List<string> allColumns)
		{
			if (string.IsNullOrEmpty(fullTarget)) return;
			var variants = new List<string> { fullTarget };
			var parts = fullTarget.Split('.');
			if (parts.Length == 3) variants.Add(parts[1] + "." + parts[2]);
			if (parts.Length >= 1) variants.Add(parts[parts.Length - 1]);
			foreach (var v in variants)
			{
				if (fullMeta.TryGetValue(v, out var meta) && meta.ColumnList != null)
				{
					var cols = meta.ColumnList.Select(c => c.ColumnName).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
					allColumns = (allColumns == null || allColumns.Count == 0) ? cols : allColumns.Concat(cols).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
					break;
				}
			}
		}

		private static Dictionary<string, string> FallbackAliasScan(string fragment, List<IntellisenseTableContract> metas)
		{
			var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			var keywords = new HashSet<string>(TSqlReservedKeywordsProvider.GetAll(), StringComparer.OrdinalIgnoreCase);
			var tokens = Tokenize(fragment);
			for (int i = 0; i < tokens.Count - 1; i++)
			{
				string first = tokens[i];
				string second = tokens[i + 1];
				if (keywords.Contains(first.ToUpperInvariant())) continue;
				// Do not treat SQL keywords as aliases (e.g., 'WHERE' after table)
				if (keywords.Contains(second.ToUpperInvariant())) continue;
				var cleanFirst = first.Trim('[', ']');
				var parts = cleanFirst.Split('.');
				if (parts.Length == 3)
				{
					var db = parts[0]; var schema = parts[1]; var table = parts[2];
					bool exists = metas.Any(m => (m.DBName ?? string.Empty).Equals(db, StringComparison.OrdinalIgnoreCase) && (m.SchemaName ?? string.Empty).Equals(schema, StringComparison.OrdinalIgnoreCase) && (m.TableName ?? string.Empty).Equals(table, StringComparison.OrdinalIgnoreCase));
					if (exists)
					{
						var full = db + "." + schema + "." + table;
						if (!result.ContainsKey(second)) result[second] = full;
					}
				}
				else if (parts.Length == 2)
				{
					var schema = parts[0]; var table = parts[1];
					bool exists = metas.Any(m => (m.SchemaName ?? string.Empty).Equals(schema, StringComparison.OrdinalIgnoreCase) && (m.TableName ?? string.Empty).Equals(table, StringComparison.OrdinalIgnoreCase));
					if (exists)
					{
						var full = schema + "." + table;
						if (!result.ContainsKey(second)) result[second] = full;
					}
				}
				else
				{
					var meta = metas.FirstOrDefault(m => (m.TableName ?? string.Empty).Equals(cleanFirst, StringComparison.OrdinalIgnoreCase));
					if (meta != null && !keywords.Contains(second.ToUpperInvariant()) && !result.ContainsKey(second))
					{
						string fullKey;
						if (!string.IsNullOrEmpty(meta.DBName) && !string.IsNullOrEmpty(meta.SchemaName)) fullKey = meta.DBName + "." + meta.SchemaName + "." + meta.TableName;
						else if (!string.IsNullOrEmpty(meta.SchemaName)) fullKey = meta.SchemaName + "." + meta.TableName;
						else fullKey = meta.TableName;
						result[second] = fullKey;
					}
				}
			}
			return result;
		}

		private static List<string> FallbackDetectTables(string fragment, List<IntellisenseTableContract> metas)
		{
			var list = new List<string>();
			if (string.IsNullOrEmpty(fragment) || metas == null) return list;
			foreach (var tok in Tokenize(fragment))
			{
				var plain = tok.Trim('[', ']');
				var parts = plain.Split('.');
				if (parts.Length == 3)
				{
					var db = parts[0]; var schema = parts[1]; var table = parts[2];
					bool exists = metas.Any(m => (m.DBName ?? string.Empty).Equals(db, StringComparison.OrdinalIgnoreCase) && (m.SchemaName ?? string.Empty).Equals(schema, StringComparison.OrdinalIgnoreCase) && (m.TableName ?? string.Empty).Equals(table, StringComparison.OrdinalIgnoreCase));
					if (exists)
					{
						var full = db + "." + schema + "." + table;
						if (!list.Contains(full)) list.Add(full);
					}
				}
				else if (parts.Length == 2)
				{
					var schema = parts[0]; var table = parts[1];
					bool exists = metas.Any(m => (m.SchemaName ?? string.Empty).Equals(schema, StringComparison.OrdinalIgnoreCase) && (m.TableName ?? string.Empty).Equals(table, StringComparison.OrdinalIgnoreCase));
					if (exists)
					{
						var full = schema + "." + table;
						if (!list.Contains(full)) list.Add(full);
					}
				}
				else
				{
					var meta = metas.FirstOrDefault(m => (m.TableName ?? string.Empty).Equals(plain, StringComparison.OrdinalIgnoreCase));
					if (meta != null)
					{
						string fullKey;
						if (!string.IsNullOrEmpty(meta.DBName) && !string.IsNullOrEmpty(meta.SchemaName)) fullKey = meta.DBName + "." + meta.SchemaName + "." + meta.TableName;
						else if (!string.IsNullOrEmpty(meta.SchemaName)) fullKey = meta.SchemaName + "." + meta.TableName;
						else fullKey = meta.TableName;
						if (!list.Contains(fullKey)) list.Add(fullKey);
					}
				}
			}
			return list;
		}

		private static List<string> Tokenize(string fragment)
		{
			return fragment.Replace("(", " ").Replace(")", " ").Replace(",", " ").Split(new[] { '\n', '\r', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		/// <summary>
		/// Final ordering and filtering of suggestion list: aliases → columns → variables → reserved → others.
		/// Reserved keyword'ler sadece kendi bucket'larında tutulur ve en sonda, büyük harf ile gösterilir.
		/// </summary>
		private static List<string> PostProcessSuggestions(List<string> suggestions, SqlContext context, Dictionary<string, string> aliases, List<IntellisenseTableContract> metaList, Dictionary<string, IntellisenseTableContract> fullMeta, string prefix, bool noPrefix, string segmentText, bool typingAfterAliasDot, string currentDb)
		{
			if (suggestions == null) return new List<string>();

			// '@' karakterinin kendisini asla önerme. Kullanıcı zaten '@' yazdıysa, asıl ihtiyaç değişken adıdır.
			suggestions = suggestions.Where(s => !string.Equals(s, "@", StringComparison.Ordinal)).ToList();
			if (suggestions.Count == 0) return suggestions;

			// 1) Ortak sınıflandırma setlerini hazırla
			BuildClassificationSets(metaList, aliases, fullMeta, segmentText,
				out var tableNames,
				out var aliasNames,
				out var allColsSet,
				out var variablesSet,
				out var reservedSet,
				out var reservedArray);

			// 2) Önerileri kategorize edip tek listede sırala
			var ordered = OrderSuggestionsByCategory(suggestions, aliasNames, allColsSet, variablesSet, reservedSet, reservedArray, prefix, noPrefix);

			// 3) Seçilen tablo isminden hemen sonra tekrar tablo önermeyi engelle
			ordered = SuppressTableAfterSelectionIfNeeded(ordered, noPrefix, segmentText, metaList, tableNames);

			// 4) Varolan prefix ile başlayan en iyi eşleşmeyi listenin başına al.
			// Kural:
			//   - Önce prefix ile birebir aynı olanı (case-insensitive) bul, varsa onu seç.
			//   - Yoksa prefix ile başlayanlar içinden en kısa olanı seç.
			if (!string.IsNullOrEmpty(prefix) && ordered.Count > 1)
			{
				var exact = ordered
					.FirstOrDefault(s => string.Equals(s, prefix, StringComparison.OrdinalIgnoreCase));
				string best = exact;
				if (string.IsNullOrEmpty(best))
				{
					best = ordered
						.Where(s => s.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
						.OrderBy(s => s.Length)
						.ThenBy(s => s, StringComparer.OrdinalIgnoreCase)
						.FirstOrDefault();
				}
				if (!string.IsNullOrEmpty(best))
				{
					ordered.RemoveAll(s => string.Equals(s, best, StringComparison.OrdinalIgnoreCase));
					ordered.Insert(0, best);
				}
			}

			return ordered;
		}

		/// <summary>
		/// Tüm sınıflandırma setlerini hazırlar: tablolar, alias'lar, kolonlar, değişkenler, reserved keyword'ler.
		/// </summary>
		private static void BuildClassificationSets(
			List<IntellisenseTableContract> metaList,
			Dictionary<string, string> aliases,
			Dictionary<string, IntellisenseTableContract> fullMeta,
			string segmentText,
			out HashSet<string> tableNames,
			out HashSet<string> aliasNames,
			out HashSet<string> allColsSet,
			out HashSet<string> variablesSet,
			out HashSet<string> reservedSet,
			out string[] reservedArray)
		{
			tableNames = new HashSet<string>(metaList.Select(m => m.TableName).Where(n => !string.IsNullOrEmpty(n)), StringComparer.OrdinalIgnoreCase);
			aliasNames = new HashSet<string>(aliases.Keys.Where(k => !k.StartsWith(InternalTablePrefix)), StringComparer.OrdinalIgnoreCase);

			// Kolonlar: hem tüm meta'lardan hem alias'ların bağlı tablolarından
			allColsSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (var m in metaList)
			{
				if (m.ColumnList == null) continue;
				foreach (var c in m.ColumnList)
					if (!string.IsNullOrWhiteSpace(c.ColumnName))
						allColsSet.Add(c.ColumnName);
			}
			var aliasCols = AliasUtil.AggregateColumns(aliases.Values, fullMeta) ?? new List<string>();
			foreach (var c in aliasCols)
				if (!string.IsNullOrWhiteSpace(c))
					allColsSet.Add(c);

			// Değişkenler: inline + procedure parametreleri
			variablesSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			try
			{
				var seg = segmentText ?? string.Empty;
				var inlineVars = VariableCollector.CollectVariables(seg, seg.Length) ?? new List<string>();
				foreach (var v in inlineVars) if (!string.IsNullOrWhiteSpace(v)) variablesSet.Add(v);
			}
			catch
			{
				// ignored
			}
			try
			{
				var procVars = CollectProcedureParameters(segmentText ?? string.Empty) ?? new List<string>();
				foreach (var v in procVars) if (!string.IsNullOrWhiteSpace(v)) variablesSet.Add(v);
			}
			catch
			{
				// ignored
			}

			reservedArray = NSqlTools.Types.Constants.SqlKeywords ?? Array.Empty<string>();
			reservedSet = new HashSet<string>(reservedArray, StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Önerileri alias/kolon/değişken/reserved/diğer kategorilerine ayırıp alfabetik ve öncelikli sıraya sokar.
		/// </summary>
		private static List<string> OrderSuggestionsByCategory(
			List<string> suggestions,
			HashSet<string> aliasNames,
			HashSet<string> allColsSet,
			HashSet<string> variablesSet,
			HashSet<string> reservedSet,
			string[] reservedArray,
			string prefix,
			bool noPrefix)
		{
			// 1) Motor çıkışından gelen reserved'leri tamamen at
			var distinct = suggestions
				.Where(s => s == null || !reservedSet.Contains(s))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();

			var aliasNameList = new List<string>();
			var colList = new List<string>();
			var variableList = new List<string>();
			var reservedList = new List<string>();
			var otherList = new List<string>();

			foreach (var s in distinct)
			{
				if (string.IsNullOrEmpty(s)) continue;
				if (aliasNames.Contains(s)) { aliasNameList.Add(s); continue; }
				if (allColsSet.Contains(s)) { colList.Add(s); continue; }
				if (variablesSet.Contains(s)) { variableList.Add(s); continue; }
				if (reservedSet.Contains(s)) { reservedList.Add(s); continue; }
				otherList.Add(s);
			}

			// 2) Eksik reserved keyword'leri sadece reserved bucket'ına ekle
			try
			{
				var extraReserved = SuggestionUtil.FilterByPrefix(reservedArray, prefix, noPrefix)
					.Where(k => !reservedList.Contains(k, StringComparer.OrdinalIgnoreCase))
					.ToList();
				if (extraReserved.Count > 0)
					reservedList.AddRange(extraReserved);
			}
			catch
			{
				// ignored
			}
			// 3) Her bucket'ı alfabetik sırala ve tek listeye yapıştır
			aliasNameList = aliasNameList.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
			colList = colList.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
			variableList = variableList.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
			reservedList = reservedList.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
			otherList = otherList.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();

			var result = new List<string>();
			result.AddRange(aliasNameList);
			result.AddRange(colList);
			result.AddRange(variableList);
			result.AddRange(otherList);
			result.AddRange(reservedList);

			// 4) Reserved keyword'leri büyük harfe çevir
			try
			{
				result = result.Select(s => reservedSet.Contains(s) ? s.ToUpperInvariant() : s).ToList();
			}
			catch
			{
				// ignored
			}
			return result;
		}

		/// <summary>
		/// Kullanıcı bir tablo adını seçip hemen boşluk bastığında, tekrar tablo önermeyi engeller.
		/// </summary>
		private static List<string> SuppressTableAfterSelectionIfNeeded(List<string> suggestions, bool noPrefix, string segmentText, List<IntellisenseTableContract> metaList, HashSet<string> tableNames)
		{
			if (!noPrefix) return suggestions;
			try
			{
				var lastToken = (segmentText ?? string.Empty).TrimEnd().Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
				var plainLast = (lastToken ?? string.Empty).Trim('[', ']');
				var isLastATable = metaList.Any(m =>
					string.Equals(m.TableName ?? string.Empty, plainLast, StringComparison.OrdinalIgnoreCase)
					|| string.Equals((m.SchemaName ?? string.Empty) + "." + (m.TableName ?? string.Empty), plainLast, StringComparison.OrdinalIgnoreCase)
					|| string.Equals((m.DBName ?? string.Empty) + "." + (m.SchemaName ?? string.Empty) + "." + (m.TableName ?? string.Empty), plainLast, StringComparison.OrdinalIgnoreCase));
				if (isLastATable)
					return suggestions.Where(s => !tableNames.Contains(s)).ToList();
			}
			catch
			{
				// ignored
			}
			return suggestions;
		}

		private static IEnumerable<string> KeywordStageSuggest(List<IntellisenseTableContract> metaList, string currentDatabaseName, string prefix, bool noPrefix)
		{
			var dbNames = metaList.Select(m => m.DBName ?? string.Empty).Where(n => !string.IsNullOrEmpty(n)).Distinct(StringComparer.OrdinalIgnoreCase);
			var schemas = string.IsNullOrEmpty(currentDatabaseName)
				? metaList.Select(m => m.SchemaName ?? string.Empty).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase)
				: metaList.Where(m => string.Equals(m.DBName ?? string.Empty, currentDatabaseName, StringComparison.OrdinalIgnoreCase))
					.Select(m => m.SchemaName ?? string.Empty).Where(s => !string.IsNullOrEmpty(s)).Distinct(StringComparer.OrdinalIgnoreCase);
			var union = dbNames.Concat(schemas).Distinct(StringComparer.OrdinalIgnoreCase);
			return SuggestionUtil.FilterByPrefix(union, prefix, noPrefix);
		}

		private static int FindLastKeyword(string text, string keyword)
		{
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(keyword)) return -1;
			for (int idx = text.Length - keyword.Length; idx >= 0; idx--)
			{
				if (IsWholeKeywordMatch(text, keyword, idx)) return idx;
			}
			return -1;
		}

		private static int FindKeyword(string text, string keyword, int startIndex)
		{
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(keyword)) return -1;
			int begin = Math.Max(0, startIndex);
			for (int idx = begin; idx <= text.Length - keyword.Length; idx++)
			{
				if (IsWholeKeywordMatch(text, keyword, idx)) return idx;
			}
			return -1;
		}

		private static bool IsWholeKeywordMatch(string text, string keyword, int index)
		{
			if (index < 0 || index + keyword.Length > text.Length) return false;
			if (string.Compare(text, index, keyword, 0, keyword.Length, StringComparison.OrdinalIgnoreCase) != 0) return false;
			bool startOk = index == 0 || !IsKeywordIdentifierChar(text[index - 1]);
			int after = index + keyword.Length;
			bool endOk = after >= text.Length || !IsKeywordIdentifierChar(text[after]);
			return startOk && endOk;
		}

		private static bool IsKeywordIdentifierChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '_' || ch == '#' ;
		}

		private static bool IsAfterKeyword(string sql, int caret, out string keyword)
		{
			keyword = null;
			if (string.IsNullOrEmpty(sql) || caret < 0 || caret > sql.Length) return false;
			// Look backwards from caret to find the last non-whitespace token
			int pos = caret - 1;
			while (pos >= 0 && char.IsWhiteSpace(sql[pos])) pos--;
			// Now scan backwards to token start
			int end = pos;
			while (pos >= 0 && !char.IsWhiteSpace(sql[pos])) pos--;
			var token = sql.Substring(pos + 1, Math.Max(0, end - pos)).Trim('[', ']');
			if (string.IsNullOrEmpty(token)) return false;
			var t = token.ToLowerInvariant();
			// Do not treat 'on' as a FROM-like keyword here because ON should allow column/alias suggestions
			if (t == "from" || t == "join" || t == "update") { keyword = t; return true; }
			// Handle "insert into" multi-token: check previous token too
			if (t == "into")
			{
				// find previous non-whitespace token
				int pEnd = pos;
				while (pEnd >= 0 && char.IsWhiteSpace(sql[pEnd])) pEnd--;
				int pStart = pEnd;
				while (pStart >= 0 && !char.IsWhiteSpace(sql[pStart])) pStart--;
				var prev = sql.Substring(pStart + 1, Math.Max(0, pEnd - pStart)).Trim('[', ']').ToLowerInvariant();
				if (prev == "insert") { keyword = "insert into"; return true; }
			}
			return false;
		}

		private static List<string> CollectProcedureParameters(string sql)
		{
			var list = new List<string>();
			if (string.IsNullOrWhiteSpace(sql)) return list;
			try
			{
				// Normalize whitespace and lowercase for detection, but extract using original text
				var lower = sql.ToLowerInvariant();
				int idxCreate = lower.IndexOf("create procedure", StringComparison.Ordinal);
				if (idxCreate < 0)
					idxCreate = lower.IndexOf("alter procedure", StringComparison.Ordinal);
				if (idxCreate < 0) return list;
				// Find end of header: typically before AS keyword
				int idxAs = lower.IndexOf("\nas ", idxCreate, StringComparison.Ordinal);
				if (idxAs < 0)
					idxAs = lower.IndexOf("\r\nas ", idxCreate, StringComparison.Ordinal);
				if (idxAs < 0)
					idxAs = lower.IndexOf(" as ", idxCreate, StringComparison.Ordinal);
				int headerEnd = idxAs > idxCreate ? idxAs : Math.Min(lower.Length, idxCreate + 1000); // cap scan length
				string header = sql.Substring(idxCreate, Math.Max(0, headerEnd - idxCreate));
				// Extract tokens starting with @ up to separators (comma, whitespace, =, output)
				int pos = 0;
				while (pos < header.Length)
				{
					int at = header.IndexOf('@', pos);
					if (at < 0) break;
					int end = at + 1;
					while (end < header.Length)
					{
						char ch = header[end];
						if (char.IsLetterOrDigit(ch) || ch == '_') { end++; continue; }
						break;
					}
					var name = header.Substring(at, end - at);
					if (!string.IsNullOrWhiteSpace(name) && !list.Contains(name, StringComparer.OrdinalIgnoreCase))
						list.Add(name);
					pos = end + 1;
				}
			}
			catch
			{
				// ignored
			}
			return list;
		}

		private static string ExtractUpdateTableName(string segmentText, int caretInSeg)
		{
			return TableNameExtractor.ExtractUpdateTableName(segmentText);
		}

		private static string ExtractInsertTableName(string segmentText, int caretInSeg)
		{
			return TableNameExtractor.ExtractInsertTableName(segmentText);
		}

		private static bool IsAtQueryStart(string sql, int caret, SegmentInfo segment)
		{
			// SegmentInfo struct olduğu için null kontrolü gereksiz; sadece text'e bakmak yeterli
			if (string.IsNullOrEmpty(segment.Text)) return true;

			// Segment içindeki caret konumu
			int caretInSeg = Math.Max(0, caret - segment.Start);
			var text = segment.Text;
			if (caretInSeg > text.Length) caretInSeg = text.Length;

			// Caret'in bulunduğu satırı bul
			int lineStart = caretInSeg;
			while (lineStart > 0)
			{
				char ch = text[lineStart - 1];
				if (ch == '\r' || ch == '\n') break;
				lineStart--;
			}

			int lineEnd = caretInSeg;
			while (lineEnd < text.Length)
			{
				char ch = text[lineEnd];
				if (ch == '\r' || ch == '\n') break;
				lineEnd++;
			}

			var lineUpToCaret = text.Substring(lineStart, caretInSeg - lineStart);

			// Sadece bu satırın başındaki boşlukları at
			var trimmed = lineUpToCaret.TrimStart();
			if (string.IsNullOrEmpty(trimmed))
				return true; // tamamen boş satırda -> yeni cümlecik

			var lower = trimmed.ToLowerInvariant();

			// Eğer bu SATIRDA SELECT/INSERT/UPDATE/DELETE/WITH ile başlamışsak snippet önermeyelim
			if (lower.StartsWith("select") ||
				lower.StartsWith("insert") ||
				lower.StartsWith("update") ||
				lower.StartsWith("delete") ||
				lower.StartsWith("with "))
			{
				return false;
			}

			// Aynı satırda noktalı virgül sonrası yeni statement olabilir
			int lastSemi = lower.LastIndexOf(';');
			if (lastSemi >= 0 && lastSemi < trimmed.Length - 1)
			{
				var afterSemi = trimmed.Substring(lastSemi + 1).TrimStart().ToLowerInvariant();
				if (string.IsNullOrEmpty(afterSemi))
					return true; // satırda ; sonrası sadece boşluk → yeni cümle başı

				// Eğer ; sonrası zaten DML ile başlamışsa snippet'e gerek yok
				if (afterSemi.StartsWith("select") ||
					afterSemi.StartsWith("insert") ||
					afterSemi.StartsWith("update") ||
					afterSemi.StartsWith("delete") ||
					afterSemi.StartsWith("with "))
				{
					return false;
				}

				// ; sonrası henüz DML yazılmamışsa yeni cümle kabul et
				return true;
			}

			// Satır içinde henüz DML başlamamışsa bu satırı yeni cümlecik kabul et
			return true;
		}

		private static List<string> BuildSnippetSuggestionsForQueryStart(
			string sql,
			int caret,
			SegmentInfo segment)
		{
			if (!IsAtQueryStart(sql, caret, segment))
				return new List<string>();

			SnippetsBusiness snippetBusiness = new SnippetsBusiness();
			var allSnippets = snippetBusiness.GetAll(true);
			if (allSnippets.Count == 0) return new List<string>();

			// Görünen metin: "shortcut - description"
			// Eğer Description boşsa sadece shortcut göster
			var list = new List<string>();
			foreach (var sn in allSnippets)
			{
				if (string.IsNullOrWhiteSpace(sn.Shortcut)) continue;
				var display = sn.Shortcut;
				list.Add(display);
			}

			return list;
		}
	}
}

