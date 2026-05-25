namespace NSqlTools.BusinessLayer.Intellisense
{
	public static partial class SimpleSqlIntellisenseEngine
	{
		private static readonly string[] TopLevelStatementKeywords = { SqlTokens.Select, SqlTokens.Update, SqlTokens.Delete, SqlTokens.Insert, SqlTokens.Merge };
		private static readonly string[] AggregateFunctions = { "COUNT", "SUM", "MIN", "MAX", "AVG" };
		private const string InternalTablePrefix = "__TABLE__:";
		private enum SqlContext { SelectList, From, Where, JoinOn, GroupBy, Having, OrderBy, InsertInto, InsertColumns, InsertValues, Declare, Other }

		private static class SqlTokens
		{
			public const string Select = "SELECT";
			public const string Update = "UPDATE";
			public const string Delete = "DELETE";
			public const string Insert = "INSERT";
			public const string Merge = "MERGE";
			public const string From = "FROM";
			public const string Where = "WHERE";
			public const string Group = "GROUP";
			public const string Order = "ORDER";
			public const string By = "BY";
			public const string Join = "JOIN";
			public const string Inner = "INNER";
			public const string Left = "LEFT";
			public const string Right = "RIGHT";
			public const string Full = "FULL";
			public const string Outer = "OUTER";
			public const string Cross = "CROSS";
			public const string Apply = "APPLY";
			public const string On = "ON";
			public const string Into = "INTO";
			public const string Values = "VALUES";
			public const string Declare = "DECLARE";
			public const string Set = "SET";
		}

	}
}
