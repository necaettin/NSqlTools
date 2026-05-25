using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json.Linq;
using PoorMansTSqlFormatterLib;
using PoorMansTSqlFormatterLib.Formatters;
using ScintillaNET;
using System;
using System.Linq;
using System.Xml.Linq;

namespace NSqlTools.Lib.Helpers
{
	public static class ContentFormatHelper
	{
		public static string Format(Lexer lexer, string content)
		{
			if (string.IsNullOrWhiteSpace(content))
				return content;

			try
			{
				switch (lexer)
				{
					case Lexer.Sql:
						return FormatSql(content);
					case Lexer.Json:
						return FormatJson(content);
					case Lexer.Xml:
						return FormatXml(content);
					case Lexer.Html:
						return FormatHtml(content);
					case Lexer.Cpp:
						// Roslyn yerine basit formatter
						return FormatCodeLike(content); // veya FormatCSharpWithNArrange(content);
					case Lexer.R:
					case Lexer.PhpScript:
						return FormatCodeLike(content);
					case Lexer.Batch:
						return FormatBatch(content);
					default:
						return content;
				}
			}
			catch
			{
				return content;
			}
		}

		private static string FormatSql(string sql)
		{
			var formatter = new TSqlStandardFormatter
			{
				IndentString = "\t",
				ExpandCommaLists = true,
				ExpandBooleanExpressions = true,
				UppercaseKeywords = true,
				TrailingCommas = true
			};
			bool dummy = false;
			var manager = new SqlFormattingManager(formatter);
			return manager.Format(sql, ref dummy);
		}

		private static string FormatJson(string json)
		{
			var token = JToken.Parse(json);
			return token.ToString(Newtonsoft.Json.Formatting.Indented);
		}

		private static string FormatXml(string xml)
		{
			if (string.IsNullOrWhiteSpace(xml))
				return xml;

			try
			{
				// Whitespace'i normalize ederek yükle (indent için daha uygun)
				var doc = XDocument.Parse(xml, LoadOptions.None);

				// Default ToString zaten girintili yazar
				return doc.ToString(SaveOptions.None);
			}
			catch
			{
				// Geçersiz XML ise orijinali bozma
				return xml;
			}
		}

		private static string FormatHtml(string html)
		{
			try
			{
				var parser = new HtmlParser(new HtmlParserOptions
				{
					IsKeepingSourceReferences = false
				});
				var document = parser.ParseDocument(html ?? string.Empty);

				// Tüm kök düğümleri (doctype, html, yorum vs.) yaz
				return PrettyPrintHtml(document, 0);
			}
			catch
			{
				return html;
			}
		}

		private static string PrettyPrintHtml(INode node, int level)
		{
			var sb = new System.Text.StringBuilder();
			foreach (var child in node.ChildNodes)
			{
				AppendNode(sb, child, level);
			}
			return sb.ToString();
		}

		private static void AppendNode(System.Text.StringBuilder sb, INode node, int level)
		{
			string indent = new string('\t', level);
			switch (node)
			{
				case IDocumentType docType:
					// <!DOCTYPE html> veya gelen isim ne ise onu yaz
					sb.Append(indent);
					sb.Append("<!DOCTYPE ");
					sb.Append(string.IsNullOrEmpty(docType.Name) ? "html" : docType.Name);
					sb.AppendLine(">");
					break;

				case IText textNode:
					var text = textNode.Text.Trim();
					if (!string.IsNullOrEmpty(text))
					{
						sb.Append(indent);
						sb.AppendLine(text);
					}
					break;
				case IComment comment:
					sb.Append(indent);
					sb.Append("<!--");
					sb.Append(comment.Data.Trim());
					sb.AppendLine("-->");
					break;
				case IElement el:
					// Açılış etiket
					sb.Append(indent);
					sb.Append("<");
					sb.Append(el.TagName.ToLowerInvariant());
					foreach (var attr in el.Attributes)
					{
						sb.Append(" ");
						sb.Append(attr.Name);
						sb.Append("=\"");
						sb.Append(attr.Value);
						sb.Append("\"");
					}
					sb.AppendLine(">");

					// Çocuklar
					foreach (var child in el.ChildNodes)
					{
						AppendNode(sb, child, level + 1);
					}

					// Self-closing olmayan elementler için kapanış etiketi
					if (!IsVoidElement(el.TagName))
					{
						sb.Append(indent);
						sb.Append("</");
						sb.Append(el.TagName.ToLowerInvariant());
						sb.AppendLine(">");
					}
					break;
			}
		}

		private static bool IsVoidElement(string tagName)
		{
			if (string.IsNullOrEmpty(tagName))
				return false;

			switch (tagName.ToLowerInvariant())
			{
				case "area":
				case "base":
				case "br":
				case "col":
				case "embed":
				case "hr":
				case "img":
				case "input":
				case "link":
				case "meta":
				case "param":
				case "source":
				case "track":
				case "wbr":
					return true;
				default:
					return false;
			}
		}

		private static string FormatCodeLike(string code)
		{
			if (string.IsNullOrWhiteSpace(code))
				return code;

			var sb = new System.Text.StringBuilder();
			int indent = 0;
			bool inString = false;
			char stringChar = '\0';

			// Satırları kaba şekilde ; ve newline'a göre ayır
			var tokens = code
				.Replace("{", "{\n")
				.Replace("}", "}\n")
				.Replace(";", ";\n")
				.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var raw in tokens)
			{
				var line = raw.Trim();
				if (line.Length == 0)
					continue;

				// Kapanış parantezi ile başlayan satırda önce indent düşür
				if (line.StartsWith("}"))
					indent = Math.Max(0, indent - 1);

				sb.Append(new string('\t', indent));
				sb.AppendLine(line);

				// Satır içindeki { ve } sayısına göre indent ayarla
				foreach (var c in line)
				{
					// String içi { } dikkate alma (çok basit kaçış yönetimi)
					if (c == '"' || c == '\'')
					{
						if (!inString)
						{
							inString = true;
							stringChar = c;
						}
						else if (stringChar == c)
						{
							inString = false;
							stringChar = '\0';
						}
						continue;
					}

					if (inString)
						continue;

					if (c == '{')
					{
						indent++;
					}
					else if (c == '}')
					{
						indent = Math.Max(0, indent - 1);
					}
				}
			}

			return sb.ToString();
		}

		private static string FormatBatch(string code)
		{
			if (string.IsNullOrWhiteSpace(code))
				return code;

			var sb = new System.Text.StringBuilder();
			int indent = 0;

			// Satır satır dolaş
			var lines = code.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var rawLine in lines)
			{
				var line = rawLine.Trim();
				if (line.Length == 0)
					continue;

				// Yorum satırlarını (REM / ::) olduğu gibi bırak
				var lower = line.ToLowerInvariant();
				bool isComment = lower.StartsWith("rem ") || lower.StartsWith("::");

				// Kapanış parantezi ile başlayan satırda önce indent düşür
				if (!isComment && line.StartsWith(")"))
					indent = Math.Max(0, indent - 1);

				sb.Append(new string('\t', indent));
				sb.AppendLine(line);

				if (isComment)
					continue;

				// Satır içindeki '(' ve ')' sayısına göre indent ayarla
				int openCount = line.Count(c => c == '(');
				int closeCount = line.Count(c => c == ')');

				// İlk karakter ')' ise yukarıda zaten düşürdük, gerisini say
				if (line.StartsWith(")"))
					closeCount--;

				int diff = openCount - closeCount;
				if (diff > 0)
					indent += diff;
				else if (diff < 0)
					indent = Math.Max(0, indent + diff);
			}

			return sb.ToString();
		}
	}
}
