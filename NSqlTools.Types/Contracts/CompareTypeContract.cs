using ScintillaNET;
using System;

namespace NSqlTools.Types
{
	[Serializable]
	public class CompareTypeContract
	{
		#region Constructor
		public CompareTypeContract(Lexer compareType)
		{
			this.CompareType = compareType;
		}
		#endregion

		#region Properties
		public Lexer CompareType { get; set; }

		public String Description { get; set; }

		public override string ToString()
		{
			String result = CompareType.ToString();
			switch (CompareType)
			{
				case Lexer.Sql:
					result = "Sql";
					break;
				case Lexer.Xml:
					result = "Xml";
					break;
				case Lexer.Html:
					result = "Html";
					break;
				case Lexer.Cpp:
					result = "Cpp";
					break;
				case Lexer.Css:
					result = "Css";
					break;
				case Lexer.Json:
					result = "Json";
					break;
				case Lexer.Vb:
					result = "Vb";
					break;
				case Lexer.VbScript:
					result = "Vb Script";
					break;
				case Lexer.PhpScript:
					result = "Php Script";
					break;
				case Lexer.PowerShell:
					result = "PowerShell";
					break;
				case Lexer.Batch:
					result = "Batch";
					break;
				case Lexer.R:
					result = "React & Javascript";
					break;
			}

			return result;
		}
		#endregion
	}
}
