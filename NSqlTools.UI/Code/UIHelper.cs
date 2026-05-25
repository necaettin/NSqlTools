using Microsoft.Win32;
using NSqlTools.BusinessLayer;
using NSqlTools.BusinessLayer.Cache;
using NSqlTools.BusinessLayer.Intellisense;
using NSqlTools.Types;
using NSqlTools.Types.Contracts;
using NSqlTools.Types.IntellisenseContracts;
using NSqlTools.Types.Properties;
using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSqlTools.UI
{
	public class UIHelper
	{
		#region Properties

		#region Intellisense Properties
		public static List<IntellisenseDatabaseContract> DbCacheInfo { get; set; }
		#endregion

		#endregion

		#region Methods
		#region Assembly Attribute Accessors
		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public static string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public static string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public static string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public static string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public static string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion

		#region Properties\Constants
		public static List<LocalizationContract> Localizations;
		#endregion

		#region Validation Methods
		public static Boolean ComponentIsValidString(ErrorProvider errorProvider, String value, Control control, String message)
		{
			return ComponentIsValid(errorProvider, String.IsNullOrWhiteSpace(value) ? null : value, control, message);
		}

		public static Boolean ComponentIsValid(ErrorProvider errorProvider, Object value, Control control, String message)
		{
			Boolean isValid;

			if (value == null)
			{
				errorProvider.SetError(control, message);
				isValid = false;
			}
			else
			{
				errorProvider.SetError(control, "");
				isValid = true;
			}

			return isValid;
		}

		public static Boolean ComponentIsValidBoolean(ErrorProvider errorProvider, Boolean isValid, String message, params Control[] controls)
		{
			Boolean result;
			if (!isValid)
			{
				controls.ToList().ForEach(c =>
					errorProvider.SetError(c, message)
				);
				result = false;
			}
			else
			{
				controls.ToList().ForEach(c =>
					errorProvider.SetError(c, "")
				); result = true;
			}

			return result;
		}
		#endregion

		#region Methods
		public static void SaveText(SaveFileDialog sfd, String text)
		{
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				using (FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write))
				{
					byte[] textBytes = new UTF8Encoding(true).GetBytes(text);
					fs.Write(textBytes, 0, textBytes.Length);
				}
			}
		}

		public static void InitialiseScintilla(Scintilla scSqlQuery, Lexer lexer = Lexer.Sql, Int32 fontSize = 12)
		{
			// Reset the styles
			scSqlQuery.StyleResetDefault();
			scSqlQuery.Styles[Style.Default].Font = "Courier New";
			scSqlQuery.Styles[Style.Default].Size = fontSize;
			scSqlQuery.StyleClearAll();

			// Set the SQL Lexer
			scSqlQuery.Lexer = lexer;
			scSqlQuery.HScrollBar = true;
			scSqlQuery.VScrollBar = true;

			// Set Styles
			switch (lexer)
			{
				case Lexer.Container:
					break;
				case Lexer.Null:
					break;
				case Lexer.Ada:
					break;
				case Lexer.Asm: // JS
					// JavaScript stilleri tanımla
					scSqlQuery.Styles[Style.Cpp.Default].ForeColor = Color.Black; // Varsayılan metin
					scSqlQuery.Styles[Style.Cpp.Comment].ForeColor = Color.Green; // Yorumlar
					scSqlQuery.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green; // Tek satır yorumlar
					scSqlQuery.Styles[Style.Cpp.CommentDoc].ForeColor = Color.Gray; // Doküman yorumları
					scSqlQuery.Styles[Style.Cpp.Number].ForeColor = Color.Blue; // Sayılar
					scSqlQuery.Styles[Style.Cpp.String].ForeColor = Color.Brown; // Metinler
					scSqlQuery.Styles[Style.Cpp.Character].ForeColor = Color.Orange; // Karakterler
					scSqlQuery.Styles[Style.Cpp.Word].ForeColor = Color.Blue; // Anahtar kelimeler
					scSqlQuery.Styles[Style.Cpp.Word2].ForeColor = Color.Purple; // Operatörler ve ek anahtar kelimeler
					scSqlQuery.Styles[Style.Cpp.Operator].ForeColor = Color.DarkCyan; // Operatörler
					scSqlQuery.Styles[Style.Cpp.Identifier].ForeColor = Color.Black; // Değişkenler ve fonksiyon isimleri

					// Anahtar kelimeleri tanımla
					scSqlQuery.SetKeywords(0, "break case catch class const continue debugger default delete do else enum export extends false finally for function if import in instanceof let new null return super switch this throw true try typeof var void while with yield async await");

					// Ek anahtar kelimeler (DOM ve BOM ile ilgili)
					scSqlQuery.SetKeywords(1, "window document console setTimeout setInterval clearTimeout clearInterval fetch XMLHttpRequest alert prompt confirm");

					break;
				case Lexer.Batch:
					// Batch sözdizimi için stiller
					scSqlQuery.Styles[Style.Properties.Default].ForeColor = Color.Black; // Varsayılan metin
					scSqlQuery.Styles[Style.Properties.Comment].ForeColor = Color.Green; // Yorumlar (REM veya ::)
					scSqlQuery.Styles[Style.Properties.Section].ForeColor = Color.Blue; // Anahtar kelimeler ([section])
					scSqlQuery.Styles[Style.Properties.Assignment].ForeColor = Color.Purple; // Atamalar (=)
					scSqlQuery.Styles[Style.Properties.DefVal].ForeColor = Color.DarkRed; // Değişken tanımları
					scSqlQuery.Styles[Style.Properties.Comment].Italic = true; // Yorumları italik yap

					// Anahtar kelimeleri tanımla
					scSqlQuery.SetKeywords(0, "echo set if exist not else for in do goto call pause cls exit rem :: shift chcp pushd popd setlocal endlocal title color find findstr type copy move del mkdir rmdir");

					break;
				case Lexer.Cpp: // CS
					// C# stilleri tanımla
					scSqlQuery.Styles[Style.Cpp.Default].ForeColor = Color.Black;
					scSqlQuery.Styles[Style.Cpp.Comment].ForeColor = Color.Green; // Yorumlar
					scSqlQuery.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green; // Satır yorumları
					scSqlQuery.Styles[Style.Cpp.CommentDoc].ForeColor = Color.Gray; // Dokümantasyon yorumları
					scSqlQuery.Styles[Style.Cpp.Number].ForeColor = Color.Blue; // Sayılar
					scSqlQuery.Styles[Style.Cpp.Word].ForeColor = Color.Blue; // Anahtar kelimeler
					scSqlQuery.Styles[Style.Cpp.Word2].ForeColor = Color.DarkCyan; // Ekstra anahtar kelimeler
					scSqlQuery.Styles[Style.Cpp.String].ForeColor = Color.DarkRed; // Metinler
					scSqlQuery.Styles[Style.Cpp.Character].ForeColor = Color.DarkOrange; // Karakterler
					scSqlQuery.Styles[Style.Cpp.Operator].ForeColor = Color.Purple; // Operatörler
					scSqlQuery.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Gray; // Ön işleme direktifleri

					// Anahtar kelimeleri tanımla
					scSqlQuery.SetKeywords(0, "abstract as base bool break byte case catch char checked class const continue decimal " +
											   "default delegate do double else enum event explicit extern false finally fixed float for foreach goto if implicit in int interface internal is lock long namespace new null object operator out override params private protected public readonly ref return sbyte sealed short sizeof stackalloc static string struct switch this throw true try typeof uint ulong unchecked unsafe ushort using virtual void volatile while");

					scSqlQuery.SetKeywords(1, "get set value add remove alias ascending descending dynamic from group into join let nameof on orderby partial select where yield");

					break;
				case Lexer.Css:
					// CSS stilleri tanımla
					scSqlQuery.Styles[Style.Css.Default].ForeColor = Color.Black;
					scSqlQuery.Styles[Style.Css.Tag].ForeColor = Color.Blue; // HTML etiket isimleri
					scSqlQuery.Styles[Style.Css.Class].ForeColor = Color.DarkCyan; // Sınıf isimleri
					scSqlQuery.Styles[Style.Css.PseudoClass].ForeColor = Color.Teal; // Pseudo-sınıflar
					scSqlQuery.Styles[Style.Css.Identifier].ForeColor = Color.DarkBlue; // ID'ler
					scSqlQuery.Styles[Style.Css.UnknownPseudoClass].ForeColor = Color.Red; // Bilinmeyen pseudo-sınıflar
					scSqlQuery.Styles[Style.Css.Operator].ForeColor = Color.Purple; // Operatörler
					//scSqlQuery.Styles[Style.Css].ForeColor = Color.DarkGreen; // Özellik isimleri
					scSqlQuery.Styles[Style.Css.UnknownIdentifier].ForeColor = Color.Red; // Bilinmeyen özellikler
					scSqlQuery.Styles[Style.Css.Value].ForeColor = Color.DarkMagenta; // Değerler
					scSqlQuery.Styles[Style.Css.Comment].ForeColor = Color.Green; // Yorumlar

					break;
				case Lexer.Fortran:
					break;
				case Lexer.FreeBasic:
					break;
				case Lexer.Html:
					// HTML stilleri tanımla
					scSqlQuery.Styles[Style.Html.Default].ForeColor = Color.Black;
					scSqlQuery.Styles[Style.Html.Tag].ForeColor = Color.Blue;
					scSqlQuery.Styles[Style.Html.Attribute].ForeColor = Color.Red;
					scSqlQuery.Styles[Style.Html.AttributeUnknown].ForeColor = Color.Green;
					scSqlQuery.Styles[Style.Html.Comment].ForeColor = Color.Gray;
					scSqlQuery.Styles[Style.Html.DoubleString].ForeColor = Color.DarkGreen;
					scSqlQuery.Styles[Style.Html.SingleString].ForeColor = Color.DarkGreen;
					scSqlQuery.Styles[Style.Html.Number].ForeColor = Color.Purple;
					scSqlQuery.Styles[Style.Html.Entity].ForeColor = Color.Orange;

					// HTML lexer için ek özellikler
					scSqlQuery.SetProperty("lexer.html.simple.tags", "1");
					scSqlQuery.SetProperty("lexer.html.script.comments", "1");

					break;
				case Lexer.Json:
					// JSON stilleri tanımla
					scSqlQuery.Styles[Style.Json.Default].ForeColor = Color.Black;
					scSqlQuery.Styles[Style.Json.Number].ForeColor = Color.Blue;
					scSqlQuery.Styles[Style.Json.String].ForeColor = Color.DarkGreen;
					scSqlQuery.Styles[Style.Json.StringEol].ForeColor = Color.Brown;
					scSqlQuery.Styles[Style.Json.PropertyName].ForeColor = Color.Red;
					scSqlQuery.Styles[Style.Json.LineComment].ForeColor = Color.Gray;
					scSqlQuery.Styles[Style.Json.BlockComment].ForeColor = Color.Gray;
					scSqlQuery.Styles[Style.Json.Operator].ForeColor = Color.Purple;

					break;
				case Lexer.Lisp:
					break;
				case Lexer.Lua:
					break;
				case Lexer.Pascal:
					break;
				case Lexer.Perl:
					break;
				case Lexer.PhpScript:
					// PHP için özel stiller
					scSqlQuery.Styles[Style.PhpScript.Default].ForeColor = Color.Black; // Varsayılan metin
					scSqlQuery.Styles[Style.PhpScript.Comment].ForeColor = Color.Green; // Yorumlar
					scSqlQuery.Styles[Style.PhpScript.CommentLine].ForeColor = Color.Green; // Tek satır yorumlar
					//scSqlQuery.Styles[Style.PhpScript.CommentDoc].ForeColor = Color.Gray; // Doküman yorumları
					//scSqlQuery.Styles[Style.PhpScript.String].ForeColor = Color.Brown; // Stringler
					scSqlQuery.Styles[Style.PhpScript.Number].ForeColor = Color.Blue; // Sayılar
					//scSqlQuery.Styles[Style.PhpScript.Keyword].ForeColor = Color.DarkBlue; // Anahtar kelimeler (if, else, function, etc.)
					//scSqlQuery.Styles[Style.PhpScript.Identifier].ForeColor = Color.DarkRed; // Değişkenler
					scSqlQuery.Styles[Style.PhpScript.Operator].ForeColor = Color.Cyan; // Operatörler (==, +, - vb.)

					// PHP anahtar kelimeleri
					scSqlQuery.SetKeywords(0, "if else elseif while for foreach function return echo print include require class interface try catch");

					break;
				case Lexer.PowerShell:
					// PowerShell için özel stiller
					scSqlQuery.Styles[Style.PowerShell.Default].ForeColor = Color.Black; // Varsayılan metin
					scSqlQuery.Styles[Style.PowerShell.Comment].ForeColor = Color.Green; // Yorumlar
					//scSqlQuery.Styles[Style.PowerShell.CommentLine].ForeColor = Color.Green; // Tek satır yorumlar
					//scSqlQuery.Styles[Style.PowerShell.Section].ForeColor = Color.Blue; // Cmdlet anahtar kelimeleri (Get-Command, Set-Item, vb.)
					//scSqlQuery.Styles[Style.PowerShell.Assignment].ForeColor = Color.Purple; // Değişken atamaları
					scSqlQuery.Styles[Style.PowerShell.Keyword].ForeColor = Color.DarkBlue; // Anahtar kelimeler (if, else, function)
					scSqlQuery.Styles[Style.PowerShell.Identifier].ForeColor = Color.DarkRed; // Değişkenler
					scSqlQuery.Styles[Style.PowerShell.String].ForeColor = Color.Brown; // Stringler

					// PowerShell cmdlet'leri için anahtar kelimeler
					scSqlQuery.SetKeywords(0, "Get-Command Get-Item Set-Item Remove-Item Set-Content Get-Content Out-File ForEach-Object Where-Object If Else Function");

					break;
				case Lexer.Properties:
					break;
				case Lexer.PureBasic:
					break;
				case Lexer.Python:
					// Python stilleri tanımla
					scSqlQuery.Styles[Style.Python.Default].ForeColor = Color.Black; // Varsayılan metin
					scSqlQuery.Styles[Style.Python.CommentLine].ForeColor = Color.Green; // Yorumlar
					scSqlQuery.Styles[Style.Python.Number].ForeColor = Color.Blue; // Sayılar
					scSqlQuery.Styles[Style.Python.String].ForeColor = Color.DarkRed; // Metinler
					scSqlQuery.Styles[Style.Python.Character].ForeColor = Color.Orange; // Karakterler
					scSqlQuery.Styles[Style.Python.Word].ForeColor = Color.Blue; // Anahtar kelimeler
					scSqlQuery.Styles[Style.Python.Triple].ForeColor = Color.DarkGreen; // Üçlü tırnaklı metin
					scSqlQuery.Styles[Style.Python.TripleDouble].ForeColor = Color.DarkGreen; // Üçlü çift tırnaklı metin
					scSqlQuery.Styles[Style.Python.ClassName].ForeColor = Color.DarkMagenta; // Sınıf isimleri
					scSqlQuery.Styles[Style.Python.DefName].ForeColor = Color.DarkBlue; // Fonksiyon isimleri
					scSqlQuery.Styles[Style.Python.Operator].ForeColor = Color.Purple; // Operatörler
					scSqlQuery.Styles[Style.Python.Identifier].ForeColor = Color.Black; // Değişkenler
					scSqlQuery.Styles[Style.Python.CommentBlock].ForeColor = Color.Green; // Çok satırlı yorumlar
					scSqlQuery.Styles[Style.Python.StringEol].BackColor = Color.LightPink; // Satır sonu metin hatası

					break;
				case Lexer.Ruby:
					break;
				case Lexer.Smalltalk:
					break;
				case Lexer.Sql:
					// Set the Styles
					scSqlQuery.Styles[Style.LineNumber].ForeColor = Color.FromArgb(255, 128, 128, 128);  //Dark Gray
					scSqlQuery.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 228, 228, 228);  //Light Gray
					scSqlQuery.Styles[Style.Sql.Comment].ForeColor = Color.Green;
					scSqlQuery.Styles[Style.Sql.CommentLine].ForeColor = Color.Green;
					scSqlQuery.Styles[Style.Sql.CommentLineDoc].ForeColor = Color.Green;
					scSqlQuery.Styles[Style.Sql.Number].ForeColor = Color.Maroon;
					scSqlQuery.Styles[Style.Sql.Word].ForeColor = Color.Blue;
					scSqlQuery.Styles[Style.Sql.Word2].ForeColor = Color.Fuchsia;
					scSqlQuery.Styles[Style.Sql.User1].ForeColor = Color.Gray;
					scSqlQuery.Styles[Style.Sql.User2].ForeColor = Color.FromArgb(255, 00, 128, 192);    //Medium Blue-Green
					scSqlQuery.Styles[Style.Sql.String].ForeColor = Color.Red;
					scSqlQuery.Styles[Style.Sql.Character].ForeColor = Color.Red;
					scSqlQuery.Styles[Style.Sql.Operator].ForeColor = Color.Black;

					// Set keyword lists
					scSqlQuery.SetKeywords(0, @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
					scSqlQuery.SetKeywords(1, @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
					scSqlQuery.SetKeywords(4, @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");
					scSqlQuery.SetKeywords(5, @"sys objects sysobjects ");

					break;
				case Lexer.Vb:
					// VB stilleri tanımla
					scSqlQuery.Styles[Style.Vb.Default].ForeColor = Color.Black;
					scSqlQuery.Styles[Style.Vb.Comment].ForeColor = Color.Green; // Yorumlar
					scSqlQuery.Styles[Style.Vb.Number].ForeColor = Color.Blue; // Sayılar
					scSqlQuery.Styles[Style.Vb.String].ForeColor = Color.DarkRed; // Metinler
					scSqlQuery.Styles[Style.Vb.Keyword].ForeColor = Color.Blue; // Anahtar kelimeler
					scSqlQuery.Styles[Style.Vb.Identifier].ForeColor = Color.Black; // Değişkenler
					scSqlQuery.Styles[Style.Vb.Operator].ForeColor = Color.Purple; // Operatörler
					scSqlQuery.Styles[Style.Vb.Preprocessor].ForeColor = Color.Gray; // Preprocessor direktifleri

					break;
				case Lexer.VbScript:
					// VBScript stilleri tanımla
					scSqlQuery.Styles[Style.VbScript.Default].ForeColor = Color.Black; // Varsayılan metin
					scSqlQuery.Styles[Style.VbScript.Comment].ForeColor = Color.Green; // Yorumlar (' veya Rem)
					scSqlQuery.Styles[Style.VbScript.Number].ForeColor = Color.Blue; // Sayılar
					scSqlQuery.Styles[Style.VbScript.String].ForeColor = Color.Brown; // Metinler
					scSqlQuery.Styles[Style.VbScript.Keyword].ForeColor = Color.Blue; // Anahtar kelimeler
					scSqlQuery.Styles[Style.VbScript.Identifier].ForeColor = Color.Black; // Değişkenler ve metotlar
					scSqlQuery.Styles[Style.VbScript.Operator].ForeColor = Color.Purple; // Operatörler
					scSqlQuery.Styles[Style.VbScript.Date].ForeColor = Color.DarkMagenta; // Tarihler
					//scSqlQuery.Styles[Style.Vb.UnclosedString].BackColor = Color.LightPink; // Eksik metin hataları

					break;
				case Lexer.Verilog:
					break;
				case Lexer.Xml:
					// Set the Styles
					scSqlQuery.Styles[Style.Xml.Default].ForeColor = Color.Black;
					scSqlQuery.Styles[Style.Xml.Tag].ForeColor = Color.Blue;
					scSqlQuery.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
					scSqlQuery.Styles[Style.Xml.DoubleString].ForeColor = Color.Green;
					scSqlQuery.Styles[Style.Xml.SingleString].ForeColor = Color.Green;
					scSqlQuery.Styles[Style.Xml.Number].ForeColor = Color.Purple;

					scSqlQuery.SetProperty("lexer.xml.allow.scripts", "1");
					scSqlQuery.SetProperty("lexer.xml.cache.document", "1");

					break;
				case Lexer.BlitzBasic:
					break;
				case Lexer.Markdown:
					break;
				case Lexer.R: // React
					// JavaScript ve JSX için stiller
					scSqlQuery.Styles[Style.Cpp.Default].ForeColor = Color.Black; // Varsayılan metin
					scSqlQuery.Styles[Style.Cpp.Comment].ForeColor = Color.Green; // Yorumlar
					scSqlQuery.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green; // Tek satır yorumlar
					scSqlQuery.Styles[Style.Cpp.CommentDoc].ForeColor = Color.Gray; // Doküman yorumları
					scSqlQuery.Styles[Style.Cpp.Number].ForeColor = Color.Blue; // Sayılar
					scSqlQuery.Styles[Style.Cpp.String].ForeColor = Color.Brown; // Stringler (metinler)
					scSqlQuery.Styles[Style.Cpp.Character].ForeColor = Color.Orange; // Karakterler
					//scSqlQuery.Styles[Style.Cpp.Keyword].ForeColor = Color.Blue; // Anahtar kelimeler (const, let, etc.)
					scSqlQuery.Styles[Style.Cpp.Identifier].ForeColor = Color.Black; // Değişkenler, fonksiyon isimleri
					scSqlQuery.Styles[Style.Cpp.Operator].ForeColor = Color.DarkCyan; // Operatörler (==, +, -, etc.)

					// JSX etiketi ve bileşenleri için özel stiller
					//scSqlQuery.Styles[Style.Cpp.Keyword].BackColor = Color.LightYellow; // JSX etiketleri ve bileşenleri

					// JavaScript ve JSX anahtar kelimeleri
					scSqlQuery.SetKeywords(0, "const let var function return class extends super import export default");
					scSqlQuery.SetKeywords(1, "React useState useEffect useContext useReducer");

					// JSX etiketleri için anahtar kelimeler
					scSqlQuery.SetKeywords(2, "div span h1 h2 p button input form img section article header footer");

					break;
			}

			// Girinti ve rehber çizgileri
			scSqlQuery.IndentWidth = 4; // Girinti genişliği
			scSqlQuery.IndentationGuides = IndentView.LookBoth; // Girinti rehberi çizgileri

			// Satır numaraları
			scSqlQuery.Margins[0].Width = 30; // Sol margin genişliği
			scSqlQuery.Margins[0].Type = MarginType.Number;
		}

		public static void ShowException(Exception ex)
		{
			if (ex.InnerException != null)
			{
				MessageBox.Show(
					String.Format(CommonResource.ErrorDetail,
						ex.Message,
						ex.InnerException.Message),
					CommonResource.Error,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			else
			{
				MessageBox.Show(
					ex.Message,
					CommonResource.Error,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		public static void HighlightWord(Scintilla sc, FindReplace findReplace, String text, Boolean caseSensitive)
		{
			if (String.IsNullOrEmpty(text))
				return;

			// Indicators 0-7 could be in use by a lexer
			// so we'll use indicator 8 to highlight words.
			const int NUM = 8;

			// Remove all uses of our indicator
			sc.IndicatorCurrent = NUM;
			sc.IndicatorClearRange(0, sc.TextLength);

			// Update indicator appearance
			sc.Indicators[NUM].Style = IndicatorStyle.StraightBox;
			sc.Indicators[NUM].Under = true;
			sc.Indicators[NUM].ForeColor = Color.Yellow;
			sc.Indicators[NUM].OutlineAlpha = 175;
			sc.Indicators[NUM].Alpha = 175;

			// Search the document
			sc.TargetStart = 0;
			sc.TargetEnd = sc.TextLength;
			sc.SearchFlags = SearchFlags.None;
			while (sc.SearchInTarget(text) != -1)
			{
				// Mark the search results with the current indicator
				sc.IndicatorFillRange(sc.TargetStart, sc.TargetEnd - sc.TargetStart);

				// Search the remainder of the document
				sc.TargetStart = sc.TargetEnd;
				sc.TargetEnd = sc.TextLength;
			}

			findReplace.FindAll(text, caseSensitive ? SearchFlags.MatchCase : SearchFlags.None, true, true);
		}

		public static void SafeSetSplitterDistance(SplitContainer split, int desired)
		{
			// SplitContainer'ın toplam genişliği
			int totalWidth = split.Orientation == Orientation.Horizontal ? split.Height : split.Width;
			int min = split.Panel1MinSize;
			int max = totalWidth - split.Panel2MinSize;

			// Eğer toplam genişlik, iki panelin min boyutundan küçükse, ayarlama yapma
			if (max < min)
				return;

			// desired değeri aralık dışında ise, aralığa çek
			int safeValue = Math.Max(min, Math.Min(desired, max));
			split.SplitterDistance = safeValue;
		}
		#endregion

		#region Localization Methods
		public static void SaveCultureToRegistry(String culture)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.RegistryRootKey);
			key?.SetValue("Culture", culture);
		}

		public static String GetCultureFromRegistry()
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(Constants.RegistryRootKey);
			String culture = key?.GetValue("Culture")?.ToString();

			return culture ?? "en-US";
		}

		public static void SaveToolbarVisibilityToRegistry(String toolbarName, Boolean visibility)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.RegistryRootKey);
			key?.SetValue(toolbarName, visibility ? "1" : "0", RegistryValueKind.String);
		}

		public static Boolean GetToolbarVisibilityFromRegistry(String toolbarName)
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(Constants.RegistryRootKey);
			Boolean? toolbarVisibility = key?.GetValue(toolbarName)?.ToString() != "0";

			return toolbarVisibility ?? true;
		}

		public static String GetLocalization(String key, String defaultValue = null)
		{
			String culture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
			LocalizationContract localizationContract = Localizations?.FirstOrDefault(t => t.Key == key);

			return
				localizationContract != null 
				? (
					culture == "tr"
					? localizationContract.TrValue
					: localizationContract.EnValue)
				: (String.IsNullOrWhiteSpace(defaultValue) ? key : defaultValue);
		}
		#endregion

		#region Intellisense Methods
		public static bool ScintillaComponent_CharAdded(Scintilla scintillaComponent, List<IntellisenseDatabaseContract> dbCacheInfo, CharAddedEventArgs e)
		{
			if (scintillaComponent.Lexer != Lexer.Sql)
				return false;

			if (e.Char == '\n' || e.Char == '\r' || e.Char == '\t') return false;

			var pos = scintillaComponent.CurrentPosition;
			int start = scintillaComponent.WordStartPosition(pos, true);
			string fragment = scintillaComponent.GetTextRange(start, pos - start);
			#if DEBUG
			System.Diagnostics.Debug.WriteLine($"[UI] CharAdded pos={pos} start={start} fragment='{fragment}' char='{(char)e.Char}'");
			#endif

			// Allow triggers on dot, space, comma, paren, equals, and '@'
			char prev = pos > 0 ? (char)scintillaComponent.GetCharAt(pos - 1) : '\0';
			char prev2 = pos > 1 ? (char)scintillaComponent.GetCharAt(pos - 2) : '\0';
			bool isDot = prev == '.';
			bool isSpace = prev == ' ';
			bool isComma = prev == ',';
			bool isOpenParen = prev == '(';
			bool isEquals = prev == '=';
			bool isAt = prev == '@';
			bool isAfterDot = prev2 == '.'; // 2 karakter önce nokta var mı?

			// Fragment boşsa ama nokta/boşluk/özel karakter sonrası yazıyorsak intellisense tetikle
			if (fragment.Length < 1 && !isAfterDot && !(isDot || isSpace || isComma || isOpenParen || isEquals || isAt)) return false;

			// Çok kısa prefix'ler için (örn. "s", "se") ve tabloların dolu olduğu durumlarda
			// sadece keyword/snippet gösterilmişse tekrar ağır GetSuggestions çağrısı yapmaya gerek yok.
			// Bu, özellikle uzun script + hızlı yazımda gecikmeyi azaltır.
			if (!string.IsNullOrEmpty(fragment) && fragment.Length < 2 && (dbCacheInfo == null || dbCacheInfo.Count == 0))
			{
				// Henüz tablo metadatası yüklenmemişken tek harf ile ağır motoru tetikleme
				return false;
			}

			// Nokta sonrası yazarken metni değiştirme
			int replaceLen = (isSpace || isComma || isOpenParen || isEquals || isAt || isAfterDot) ? 0 : (pos - start);
			var suggestions = SimpleSqlIntellisenseEngine.GetSuggestions(scintillaComponent.Text, pos, dbCacheInfo, dbCacheInfo?.FirstOrDefault());
			if (!suggestions.Any()) return false;

			// For space/comma/paren/equals/@, show list without replacing any text
			scintillaComponent.AutoCShow(replaceLen, string.Join(" ", suggestions));

			System.Diagnostics.Debug.WriteLine(
				$"[UI] AutoCShow replaceLen={replaceLen} count={suggestions.Count()} first={suggestions.FirstOrDefault()}");

			return true;
		}

		public static bool ScintillaComponent_KeyUp(Scintilla scintillaComponent, KeyEventArgs e)
		{
			if (scintillaComponent.Lexer != Lexer.Sql)
				return false;

			// Kelime tamamlanma tetikleyicileri (boşluk, enter, tab vs.)
			if (e.KeyCode != Keys.Space && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Tab)
				return false;

			var editor = scintillaComponent;
			int caret = editor.CurrentPosition;
			if (caret == 0) return false;

			string text = editor.Text;

			// caret'ten sola doğru son kelimeyi bul
			int pos = caret - 1;
			while (pos >= 0 && char.IsWhiteSpace(text[pos])) pos--;
			int end = pos;
			while (pos >= 0 && !char.IsWhiteSpace(text[pos])) pos--;
			int start = pos + 1;
			if (end < start) return false;

			string token = text.Substring(start, end - start + 1);
			if (string.IsNullOrEmpty(token)) return false;

			// 1) Snippet kontrolü
			SnippetsBusiness snippetsBusiness = new SnippetsBusiness();
			var snippet = snippetsBusiness.FindByShortcut(token);
			if (snippet != null)
			{
				// token'ı snippet ile değiştir
				editor.SetSelection(start, end + 1);
				editor.ReplaceSelection(snippet.Expansion);

				// caret'i snippet sonuna al
				editor.GotoPosition(start + snippet.Expansion.Length);
				return false; // snippet uygulandı, geri kalanı çalışmasın
			}

			// reserved keyword listesi: zaten NSqlTools.Types.Constants içinde var
			var reserved = Constants.SqlKeywords;
			if (reserved == null) return false;

			// token reserved ise uppercase'e çevir
			bool isKeyword = reserved.Any(k =>
				string.Equals(k, token, StringComparison.OrdinalIgnoreCase));

			if (!isKeyword) return false;

			string upper = token.ToUpperInvariant();

			// Metni sadece bu kelime için değiştir
			editor.SetSelection(start, end + 1);
			editor.ReplaceSelection(upper);

			// caret'i eski konuma getir (Scintilla CurrentPosition kullanır)
			editor.GotoPosition(caret);

			return true;
		}

		public static async Task<List<IntellisenseDatabaseContract>> EnsureIntellisenseDbCacheAsync(
			ConnectionStringContract selectedConnectionString,
			DBContract selectedDb,
			List<IntellisenseDatabaseContract> currentCache)
		{
			System.Diagnostics.Debug.WriteLine(
				$"[EnsureIntellisenseDbCacheAsync] connNull={selectedConnectionString == null}, dbNull={selectedDb == null}, dbName={selectedDb?.Name}");

			if (selectedConnectionString == null || selectedDb == null)
			{
				return new List<IntellisenseDatabaseContract>();
			}

			var connStr = selectedConnectionString.ConnectionString;
			var dbName = selectedDb.Name;
			TableMetadataCache.EnsureCached(connStr, dbName);
			await TableMetadataCache.EnsureColumnsCachedAsync(connStr, dbName).ConfigureAwait(false);
			var tables = TableMetadataCache.GetTables(connStr, dbName) ?? new List<IntellisenseTableContract>();
			var newCache = TableMetadataCache.GetAllCachedDatabases() ?? new List<IntellisenseDatabaseContract>();
			newCache = newCache.Where(d => d != null && !string.Equals(d.DbName, dbName, StringComparison.OrdinalIgnoreCase)).ToList();
			newCache.Insert(0, new IntellisenseDatabaseContract { DbName = dbName, TableList = tables });

			return newCache;
		}
		#endregion
		#endregion
	}
}
