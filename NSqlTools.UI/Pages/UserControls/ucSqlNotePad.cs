using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.IntellisenseContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Pages;
using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using ScintillaNET_FindReplaceDialog.FindAllResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NSqlTools.BusinessLayer.Intellisense;

namespace NSqlTools.UI.UserControls
{
	public partial class ucSqlNotePad : BaseUserControl
	{
		#region Properties
		private FindReplace scintillaFindReplace;
		public FindReplace ScintillaFindReplace
		{
			get
			{
				if (scintillaFindReplace == null)
				{
					scintillaFindReplace = new FindReplace
					{
						Scintilla = scSqlQuery
					};
					scintillaFindReplace.FindAllResults += FindReplace_FindAllResults;
				}

				return scintillaFindReplace;
			}
		}

		public FindAllResultsPanel findAllResultsPanel { get; set; }

		public DBObjectContract DBObjectContract { get; set; }

		public String NotePadText
		{
			get
			{
				return scSqlQuery.Text;
			}
		}

		public String Title
		{
			get
			{
				return gbSqlNotePad.Text;
			}
			set
			{
				gbSqlNotePad.Text = value;
			}
		}

		public String SelectedNotePadText
		{
			get
			{
				return scSqlQuery.SelectedText;
			}
		}

		public String SearchKeyword
		{
			get
			{
				return txtFind.Text;
			}
			set
			{
				txtFind.Text = value;
			}
		}

		private Boolean displayFullScreen = true;
		public Boolean DisplayFullScreen
		{
			get
			{
				return displayFullScreen;
			}
			set
			{
				displayFullScreen
					= tsbFullScreen.Visible
					= value;
			}
		}

		public Boolean scoSqlNotepadPanel2Collapsed
		{
			get
			{
				return scoSqlNotepad.Panel2Collapsed;
			}
			set
			{
				scoSqlNotepad.Panel2Collapsed = value;
				tsbFindResultPanelOpenClose.Image
					= scoSqlNotepad.Panel2Collapsed
					? Properties.Resources.SearchResultOpen
					: Properties.Resources.SearchResultClose;
				tsbFindResultPanelOpenClose.Text =
					scoSqlNotepad.Panel2Collapsed
					? CommonResource.ExpandFindResultsPanel
					: CommonResource.CollapseFindResultsPanel;
			}
		}

		public Boolean DisplayStatus
		{
			get
			{
				return pnlStatus.Visible;
			}
			set
			{
				pnlStatus.Visible = value;
			}
		}

		public List<String> SchemaKeywordList { get; set; }

		public List<String> DBObjectKeywordList { get; set; }

		public int FontSize { get; set; } = 12;

		public Boolean CaseSensitive
		{
			get
			{
				return tsbCaseSensitive.CheckState == CheckState.Checked;
			}
			set
			{
				tsbCaseSensitive.CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
			}
		}

		public Boolean CompareTypeVisible
		{
			get
			{
				return cbCompareType.Visible;
			}
			set
			{
				cbCompareType.Visible = value;
			}
		}

		private List<CompareTypeContract> CompareTypeContractList { get; set; } = new List<CompareTypeContract>();

		public CompareTypeContract SelectedCompareTypeContract
		{
			get
			{
				if (cbCompareType.SelectedItem == null)
					return null;

				return cbCompareType.SelectedItem as CompareTypeContract;
			}
		}

		public Lexer? SelectedCompareType
		{
			get
			{
				if (SelectedCompareTypeContract == null)
					return null;

				return SelectedCompareTypeContract.CompareType;
			}
		}

		public Boolean IsWraped
		{
			get
			{
				return scSqlQuery.WrapMode == WrapMode.Word;
			}
			set
			{
				scSqlQuery.WrapMode = value ? WrapMode.Word : WrapMode.None;
				tsbWrap.Checked = value;
			}
		}
		#endregion

		#region Constructor
		public ucSqlNotePad()
		{
			InitializeComponent();
			setTextFromResource();

			initForm();
		}
		#endregion

		#region Private Methods
		public void initForm()
		{
			InitialiseScintilla();

			// Set default options
			scSqlQuery.WrapMode = tsbWrap.Checked ? WrapMode.Word : WrapMode.None;
			scSqlQuery.AutoCIgnoreCase = true; // Büyük/küçük harf duyarsız

			// Find result panel
			findAllResultsPanel = new FindAllResultsPanel
			{
				Scintilla = scSqlQuery,
				Dock = DockStyle.Fill
			};
			pnlFindResultBody.Controls.Add(findAllResultsPanel);
			scoSqlNotepadPanel2Collapsed = true;
			CompareTypeVisible = false;
			fillCompareTypes();
			cbCompareType.SelectedItem = CompareTypeContractList.First(l => l.CompareType == Lexer.Sql);

			scSqlQuery.AutoCSeparator = ' ';
			scSqlQuery.AutoCMaxHeight = 15;
		}

		private void setTextFromResource()
		{
			this.tsbWrap.Text = CommonResource.Wrap;
			this.tsbCopy.Text = CommonResource.Save;
			this.tsbSave.Text = CommonResource.Save;
			this.tsbOpenFindDialog.Text = CommonResource.OpenFindDialog;
			this.tsbFullScreen.Text = CommonResource.FullScreen;
			this.tsbBiggerChars.Text = CommonResource.BiggerChars;
			this.tsbSmallerChars.Text = CommonResource.SmallerChars;
			this.tsbCaseSensitive.Text = CommonResource.CaseSentitive;
			this.tsbFind.Text = CommonResource.Find;
			this.tsbFindResultPanelOpenClose.Text = CommonResource.OpenCloseFindResultsPanel;
			this.tsbDown.Text = CommonResource.FindNext;
			this.tsbUp.Text = CommonResource.FindPrevious;
			this.gbSqlNotePad.Text = CommonResource.SqlScript;
			this.saveFileDialog.Filter = CommonResource.SaveFileDialogFilter;
			this.tsbFormat.Text = CommonResource.FormatText;
		}

		public void InitialiseScintilla(Lexer lexer)
		{
			UIHelper.InitialiseScintilla(scSqlQuery, lexer);
		}

		public void InitialiseScintilla()
		{
			InitialiseScintilla(SelectedCompareType ?? Lexer.Sql);
		}

		private void fillCompareTypes()
		{
			CompareTypeContractList = Enum.GetValues(typeof(Lexer))
				.Cast<Lexer>()
				.Select(e => new CompareTypeContract(e))
				.Where(e => e.CompareType == Lexer.Sql || e.CompareType == Lexer.Xml || e.CompareType == Lexer.Html || e.CompareType == Lexer.Cpp || e.CompareType == Lexer.Css || e.CompareType == Lexer.Json || e.CompareType == Lexer.Vb || e.CompareType == Lexer.VbScript || e.CompareType == Lexer.PhpScript || e.CompareType == Lexer.PowerShell || e.CompareType == Lexer.Batch || e.CompareType == Lexer.R)
				.ToList();

			cbCompareType.Items.AddRange(CompareTypeContractList.ToArray());
		}

		private void gotoNextFind()
		{
			var characterRange = ScintillaFindReplace.FindNext(SearchKeyword);
			scSqlQuery.GotoPosition(characterRange.cpMax);
		}

		private void gotoPreviousFind()
		{
			var characterRange = ScintillaFindReplace.FindPrevious(SearchKeyword);
			scSqlQuery.GotoPosition(characterRange.cpMin);
		}

		private string formatSql(string sql)
		{
			var lexer = SelectedCompareType ?? Lexer.Sql;
			return ContentFormatHelper.Format(lexer, sql);
		}
		#endregion

		#region Public Methods
		public void SetCompareType(Lexer lexer)
		{
			cbCompareType.SelectedIndexChanged -= cbCompareType_SelectedIndexChanged;
			Int32 index = -1, i = 0;
			foreach (CompareTypeContract compareType in cbCompareType.Items.OfType<CompareTypeContract>().ToList())
			{
				if (((CompareTypeContract)cbCompareType.Items[i]).CompareType == lexer)
				{
					index = i;
					break;
				}

				i++;
			}
			cbCompareType.SelectedIndex = index;
			cbCompareType.SelectedIndexChanged += cbCompareType_SelectedIndexChanged;
		}

		public void SetDBObject(DBObjectContract dBObjectContract)
		{
			DBObjectContract = dBObjectContract;
			scSqlQuery.Text = DBObjectContract?.Definition;
			lblStatus.Text =
				DBObjectContract == null
				? null
				: dBObjectContract.Description;
			scoSqlNotepadPanel2Collapsed = true;
		}


		List<IntellisenseDatabaseContract> databases = new List<IntellisenseDatabaseContract>
			{
				new IntellisenseDatabaseContract
				{
					DbName = "TestDb",
					TableList = new List<IntellisenseTableContract>
					{
						new IntellisenseTableContract
						{
							SchemaName = "dbo",
							TableName = "tbl1",
							ColumnList = new List<IntellisenseColumnContract>
							{
								new IntellisenseColumnContract { ColumnName = "tbl1id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "col11", DataType = "nvarchar" },
								new IntellisenseColumnContract { ColumnName = "col12", DataType = "nvarchar" }
							}
						},
						new IntellisenseTableContract
						{
							SchemaName = "dbo",
							TableName = "tbl2",
							ColumnList = new List<IntellisenseColumnContract>
							{
								new IntellisenseColumnContract { ColumnName = "tbl2id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "tbl1id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "col21", DataType = "nvarchar" },
								new IntellisenseColumnContract { ColumnName = "col22", DataType = "nvarchar" }
							}
						},
						new IntellisenseTableContract
						{
							SchemaName = "dbo",
							TableName = "tbl3",
							ColumnList = new List<IntellisenseColumnContract>
							{
								new IntellisenseColumnContract { ColumnName = "tbl3id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "col31", DataType = "nvarchar" },
								new IntellisenseColumnContract { ColumnName = "col32", DataType = "nvarchar" }
							}
						},
						new IntellisenseTableContract
						{
							SchemaName = "cor",
							TableName = "tbl4",
							ColumnList = new List<IntellisenseColumnContract>
							{
								new IntellisenseColumnContract { ColumnName = "tbl4id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "col41", DataType = "nvarchar" },
								new IntellisenseColumnContract { ColumnName = "col42", DataType = "nvarchar" }
							}
						}
					}
				},
				new IntellisenseDatabaseContract
				{
					DbName = "MestDb",
					TableList = new List<IntellisenseTableContract>
					{
						new IntellisenseTableContract
						{
							SchemaName = "abc",
							TableName = "tbl1",
							ColumnList = new List<IntellisenseColumnContract>
							{
								new IntellisenseColumnContract { ColumnName = "tbl1id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "col11", DataType = "nvarchar" },
								new IntellisenseColumnContract { ColumnName = "col12", DataType = "nvarchar" }
							}
						},
						new IntellisenseTableContract
						{
							SchemaName = "def",
							TableName = "tbl2",
							ColumnList = new List<IntellisenseColumnContract>
							{
								new IntellisenseColumnContract { ColumnName = "tbl2id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "tbl1id", DataType = "int" },
								new IntellisenseColumnContract { ColumnName = "col21", DataType = "nvarchar" },
								new IntellisenseColumnContract { ColumnName = "col22", DataType = "nvarchar" }
							}
						}
					}
				}
			};

		public void HighlightWordAndGotoNextFind(String text, Boolean caseSensitive)
		{
			CaseSensitive = caseSensitive;
			UIHelper.HighlightWord(scSqlQuery, ScintillaFindReplace, text, CaseSensitive);

			gotoNextFind();
		}

		public void HighlightWordAndGotoNextFind(String text)
		{
			UIHelper.HighlightWord(scSqlQuery, ScintillaFindReplace, text, CaseSensitive);

			gotoNextFind();
		}
		#endregion

		#region Events
		private void scSqlQuery_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.F)
			{
				ScintillaFindReplace.ShowFind();
				e.SuppressKeyPress = true;
			}
			else if (e.Shift && e.KeyCode == Keys.F3)
			{
				ScintillaFindReplace.Window.FindPrevious();
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.F3)
			{
				ScintillaFindReplace.Window.FindNext();
				e.SuppressKeyPress = true;
			}
			else if (e.Control && e.KeyCode == Keys.H)
			{
				ScintillaFindReplace.ShowReplace();
				e.SuppressKeyPress = true;
			}
			else if (e.Control && e.KeyCode == Keys.I)
			{
				ScintillaFindReplace.ShowIncrementalSearch();
				e.SuppressKeyPress = true;
			}
			else if (e.Control && e.KeyCode == Keys.G)
			{
				GoTo MyGoTo = new GoTo((Scintilla)sender);
				MyGoTo.ShowGoToDialog();
				e.SuppressKeyPress = true;
			}
		}

		private void FindReplace_FindAllResults(object sender, FindResultsEventArgs FindAllResults)
		{
			findAllResultsPanel.UpdateFindAllResults(FindAllResults.FindReplace, FindAllResults.FindAllResults);
			scoSqlNotepadPanel2Collapsed = false;
		}

		private void tsbFullScreen_Click(object sender, EventArgs e)
		{
			DBObjectContract dBObjectContract = DBObjectContract ?? new DBObjectContract() { Definition = NotePadText };
			frmNotePadFullScreen frm = new frmNotePadFullScreen(
				dBObjectContract,
				txtFind.Text,
				SelectedCompareType ?? Lexer.Sql,
				CompareTypeVisible,
				CaseSensitive)
			{
				WindowState = FormWindowState.Maximized
			};
			frm.ShowDialog();
		}

		private void tsbFindResultPanelOpenClose_Click(object sender, EventArgs e)
		{
			scoSqlNotepadPanel2Collapsed = !scoSqlNotepadPanel2Collapsed;
		}

		private void tsbBiggerChars_Click(object sender, EventArgs e)
		{
			FontSize += 2;
			UIHelper.InitialiseScintilla(scSqlQuery, Lexer.Sql, FontSize);
		}

		private void tsbSmallerChars_Click(object sender, EventArgs e)
		{
			if (FontSize <= 6)
				return;

			FontSize -= 2;
			UIHelper.InitialiseScintilla(scSqlQuery, Lexer.Sql, FontSize);
		}

		private void cbCompareType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedCompareTypeContract == null)
				return;

			UIHelper.InitialiseScintilla(scSqlQuery, SelectedCompareType ?? Lexer.Sql, FontSize);
		}

		private void AutoUppercaseJustCompletedKeyword(int currentPos, char justTyped)
		{
			if (currentPos <= 0) return;
			// Only act when last char is identifier char
			if (!IsIdentifierChar(justTyped)) return;
			int scan = currentPos - 1; // last identifier char
			while (scan >= 0 && IsIdentifierChar((char)scSqlQuery.GetCharAt(scan))) scan--;
			int wordStart = scan + 1;
			int wordEndExclusive = currentPos; // include last typed char
			if (wordEndExclusive <= wordStart) return;
			string word = scSqlQuery.GetTextRange(wordStart, wordEndExclusive - wordStart);
			if (string.IsNullOrWhiteSpace(word)) return;
			// Ensure preceding char (if any) is a delimiter and following is end-of-doc OR delimiter soon (current end)
			char prevChar = scan >= 0 ? (char)scSqlQuery.GetCharAt(scan) : '\0';
			if (scan >= 0 && IsIdentifierChar(prevChar)) return; // inside longer identifier
			string upper = word.ToUpperInvariant();
			if (upper != word && TSqlReservedKeywordsProvider.GetAll().Contains(upper))
			{
				scSqlQuery.TargetStart = wordStart;
				scSqlQuery.TargetEnd = wordEndExclusive;
				scSqlQuery.ReplaceTarget(upper);
			}
		}

		private void AutoUppercaseLastKeyword(int currentPos)
		{
			if (currentPos <= 0) return;
			char justTyped = (char)scSqlQuery.GetCharAt(currentPos - 1);
			if (!IsDelimiter(justTyped)) return; // only act when word finished
			int scan = currentPos - 2; // char before delimiter
			while (scan >= 0)
			{
				char c = (char)scSqlQuery.GetCharAt(scan);
				if (!IsIdentifierChar(c)) break;
				scan--;
			}
			int wordStart = scan + 1;
			int wordEndExclusive = currentPos - 1; // delimiter not part of word
			if (wordEndExclusive <= wordStart) return;
			string word = scSqlQuery.GetTextRange(wordStart, wordEndExclusive - wordStart);
			if (string.IsNullOrWhiteSpace(word)) return;
			string upper = word.ToUpperInvariant();
			// Use reserved keywords provider
			if (upper != word && TSqlReservedKeywordsProvider.GetAll().Contains(upper))
			{
				// Replace word with uppercase variant
				scSqlQuery.TargetStart = wordStart;
				scSqlQuery.TargetEnd = wordEndExclusive;
				scSqlQuery.ReplaceTarget(upper);
			}
		}

		private bool IsIdentifierChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_' || c == '@';
		}

		private bool IsDelimiter(char c)
		{
			switch (c)
			{
				case ' ':
				case '\t':
				case '\r':
				case '\n':
				case ',':
				case ';':
				case ')':
				case '(':
				case '*':
				case '+':
				case '-':
				case '/':
				case '=':
				case '.':
				case '%':
					return true;
				default: return false;
			}
		}

		private void tsbFormat_Click(object sender, EventArgs e)
		{
			// Seçili metin varsa onu, yoksa tüm metni biçimlendir
			if (!string.IsNullOrWhiteSpace(scSqlQuery.SelectedText))
			{
				int selStart = scSqlQuery.SelectionStart;
				string formatted = formatSql(scSqlQuery.SelectedText);
				scSqlQuery.ReplaceSelection(formatted);
				scSqlQuery.SelectionStart = selStart;
				scSqlQuery.SelectionEnd = selStart + formatted.Length;
			}
			else
			{
				string formatted = formatSql(scSqlQuery.Text);
				scSqlQuery.Text = formatted;
			}
		}

		private void scSqlQuery_CharAdded(object sender, CharAddedEventArgs e)
		{
			UIHelper.ScintillaComponent_CharAdded(scSqlQuery, UIHelper.DbCacheInfo, e);

		}

		private void scSqlQuery_KeyUp(object sender, KeyEventArgs e)
		{
			UIHelper.ScintillaComponent_KeyUp(scSqlQuery, e);
		}
		#endregion

		#region ToolStrip Events
		private void tsbWrap_Click(object sender, EventArgs e)
		{
			tsbWrap.CheckState = tsbWrap.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
			scSqlQuery.WrapMode = tsbWrap.Checked ? WrapMode.Word : WrapMode.None;
		}

		private void tsbFind_Click(object sender, EventArgs e)
		{
			HighlightWordAndGotoNextFind(txtFind.Text);
		}

		private void tsbOpenFindDialog_Click(object sender, EventArgs e)
		{
			ScintillaFindReplace.ShowFind();
		}

		private void tsbDown_Click(object sender, EventArgs e)
		{
			gotoNextFind();
		}

		private void tsbUp_Click(object sender, EventArgs e)
		{
			gotoPreviousFind();
		}

		private void tsbSave_Click(object sender, EventArgs e)
		{
			UIHelper.SaveText(saveFileDialog, scSqlQuery.Text);
		}

		private void tsbCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(scSqlQuery.Text);
		}

		private void txtFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == (Int32)Keys.Enter)
				HighlightWordAndGotoNextFind(txtFind.Text);
		}

		private void tsbCaseSensitive_Click(object sender, EventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;
			button.CheckState = button.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
		}
		#endregion
	}
}
