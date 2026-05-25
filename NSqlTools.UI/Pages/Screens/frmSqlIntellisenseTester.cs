using NSqlTools.BusinessLayer.Intellisense;
using NSqlTools.Types.IntellisenseContracts;
using ScintillaNET;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
    public partial class frmSqlIntellisenseTester : Form
    {
		private List<IntellisenseDatabaseContract> _databases;

		public frmSqlIntellisenseTester()
        {
            InitializeComponent();
            InitIntellisense();
        }

        private void InitIntellisense()
        {
            UIHelper.InitialiseScintilla(scSqlQuery);

			// Sample tables for demo
			List<IntellisenseTableContract>  _tablesSales = new List<IntellisenseTableContract>
            {
                new IntellisenseTableContract
                {
                    SchemaName = "cus",
                    TableName = "Customer",
                    ColumnList = new List<IntellisenseColumnContract>
                    {
                        new IntellisenseColumnContract{ ColumnName = "CustomerId"},
                        new IntellisenseColumnContract{ ColumnName = "Name"},
                        new IntellisenseColumnContract{ ColumnName = "City"},
                        new IntellisenseColumnContract{ ColumnName = "CreatedDate"}
                    }
                },
                new IntellisenseTableContract
                {
                    SchemaName = "acc",
                    TableName = "Account",
                    ColumnList = new List<IntellisenseColumnContract>
                    {
                        new IntellisenseColumnContract{ ColumnName = "AccountId"},
                        new IntellisenseColumnContract{ ColumnName = "CustomerId"},
                        new IntellisenseColumnContract{ ColumnName = "Balance"},
                        new IntellisenseColumnContract{ ColumnName = "Suffix"}
                    }
                }
            };

			List<IntellisenseTableContract> _tablesOrder = new List<IntellisenseTableContract>
			{
				new IntellisenseTableContract
				{
					SchemaName = "lor",
					TableName = "Lord",
					ColumnList = new List<IntellisenseColumnContract>
					{
						new IntellisenseColumnContract{ ColumnName = "LordId"},
						new IntellisenseColumnContract{ ColumnName = "LordName"}
					}
				}
			};

			_databases = new List<IntellisenseDatabaseContract>()
			{
				new IntellisenseDatabaseContract()
				{
					DbName = "SalesDB",
					TableList = _tablesSales
				},

				new IntellisenseDatabaseContract()
				{
					DbName = "LordDB",
					TableList = _tablesOrder
				}
			};

			scSqlQuery.CharAdded += ScSqlQuery_CharAdded;
            scSqlQuery.AutoCIgnoreCase = true;
            // Autocomplete settings for better keyword suggestions
            scSqlQuery.AutoCMaxHeight = 10;
            scSqlQuery.AutoCAutoHide = false; // Don't auto-hide when typing exact match
            scSqlQuery.AutoCChooseSingle = false; // Don't auto-complete when there's only one match
        }

        private void ScSqlQuery_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (e.Char == '\n' || e.Char == '\r' || e.Char == '\t') return;

            var pos = scSqlQuery.CurrentPosition;
            int start = scSqlQuery.WordStartPosition(pos, true);
            string fragment = scSqlQuery.GetTextRange(start, pos - start);

            // Allow triggers on dot, space, comma, paren, equals, and '@'
            char prev = pos > 0 ? (char)scSqlQuery.GetCharAt(pos - 1) : '\0';
            bool isDot = prev == '.';
            bool isSpace = prev == ' ';
            bool isComma = prev == ',';
            bool isOpenParen = prev == '(';
            bool isEquals = prev == '=';
            bool isAt = prev == '@';

            // Always trigger when there's a fragment OR when after special chars (dot, space, comma, paren, equals, @)
            // Remove the "fragment.Length < 1" restriction to enable keyword suggestions while typing
            if (fragment.Length < 1 && !(isDot || isSpace || isComma || isOpenParen || isEquals || isAt)) return;

            var suggestions = SimpleSqlIntellisenseEngine.GetSuggestions(scSqlQuery.Text, pos, _databases, _databases.FirstOrDefault());
            if (!suggestions.Any()) return;

            // Scintilla AutoCShow parameter meanings:
            // - lengthEntered: number of characters already typed that will be replaced
            // - For special triggers (dot, space, comma), use 0 to show list without replacing
            // - For normal typing, always use fragment length so selection replaces the typed text (e.g., 'sel' -> 'SELECT')
            var suggList = suggestions.ToList();
            int replaceLen = (isSpace || isComma || isOpenParen || isEquals || isAt) ? 0 : fragment.Length;

            scSqlQuery.AutoCShow(replaceLen, string.Join(" ", suggList));
        }
    }
}
