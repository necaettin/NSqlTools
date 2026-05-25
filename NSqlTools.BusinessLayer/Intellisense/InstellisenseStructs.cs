using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Generic;

namespace NSqlTools.BusinessLayer.Intellisense
{
	public static partial class SimpleSqlIntellisenseEngine
	{
		private struct SegmentInfo { public int Start; public int End; public string Text; }

		private struct ParsedEnvironment { public TSqlFragment Root; public List<TSqlStatement> Statements; public TSqlStatement ActiveStatement; }

		private struct TokenContextInfo { public bool HasFrom; public bool CaretBeforeFrom; public bool WhereAfterFrom; }

		private struct LogicalSegment { public int Start; public int End; }
	}
}
