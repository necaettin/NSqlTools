namespace NSqlTools.BusinessLayer.Intellisense
{
	internal static class SqlKeywords
	{
		public const string Insert = "insert";
		public const string Into = "into";
		public const string Update = "update";
		public const string Set = "set";
		public const string Where = "where";
		public const string Join = " join ";
		public const string Order = " order";
		public const string Group = " group";
		public const string By = "by";
		public const string Values = " values ";

		public const char Dot = '.';
		public const char OpenParen = '(';
		public const char Space = ' ';
		public const char Newline = '\n';
		public const char CarriageReturn = '\r';
		public const char Tab = '\t';

		public static readonly char[] BracketChars = { '[', ']' };
		public static readonly char[] QuoteChars = { '[', ']', '`', '"' };
		public static readonly char[] AllQuoteChars = { '[', ']', '`', '"', '(', ')' };
		public static readonly char[] TokenDelimiters = { '\n', '\r', '\t', ' ' };
	}
}
