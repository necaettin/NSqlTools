using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Pages;
using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using ScintillaNET_FindReplaceDialog.FindAllResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static ScintillaDiff.ScintillaDiffStyles;

namespace NSqlTools.UI.UserControls
{
	public partial class ucNotePadCompare : BaseUserControl, ICustomTabSequenceProvider
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
						Scintilla = sdcCompare.LeftScintilla
					};
					scintillaFindReplace.FindAllResults += FindReplace_FindAllResults;
				}

				return scintillaFindReplace;
			}
		}

		private FindAllResultsPanel findAllResultsPanel { get; set; }

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

		public String SourceSchemaName { get; set; }
		public String SourceDBObjectName { get; set; }
		public String TargetSchemaName { get; set; }
		public String TargetDBObjectName { get; set; }

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

		public int FontSize { get; set; } = 12;

		public Boolean FindResultCollapsed
		{
			get
			{
				return scNotePadCompare.Panel2Collapsed;
			}
			set
			{
				scNotePadCompare.Panel2Collapsed = value;
				tsbFindResultPanelOpenClose.Image
					= scNotePadCompare.Panel2Collapsed
					? Properties.Resources.SearchResultOpen
					: Properties.Resources.SearchResultClose;
				tsbFindResultPanelOpenClose.Text =
					scNotePadCompare.Panel2Collapsed
					? "Expand Find Results Panel"
					: "Collapse Find Results Panel";
			}
		}

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

		public Scintilla FindReplaceScintilla
		{
			set
			{
				ScintillaFindReplace.Scintilla = value;
				findAllResultsPanel.Scintilla = value;
			}
		}

		public Boolean StatusBarPanelIsVisible
		{
			get
			{
				return pnlStatusBar.Visible;
			}
			set
			{
				pnlStatusBar.Visible = value;
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
        #endregion

        #region Event Handlers
        public event EventHandler<UpdateUIEventArgs> LeftScintilla_UpdateUI;
		public event EventHandler<UpdateUIEventArgs> RightScintilla_UpdateUI;
		#endregion

		#region Constructor
		public ucNotePadCompare()
		{
			InitializeComponent();
			setTextFromResource();

			initForm();
		}
		#endregion

		#region Events
		private void scintillaDiffControl_BindingContextChanged(object sender, EventArgs e)
		{
			SetNavigationButtonsAccessibilities();
		}

		private void FindReplace_FindAllResults(object sender, FindResultsEventArgs FindAllResults)
		{
			FindResultCollapsed = false;
			findAllResultsPanel.UpdateFindAllResults(FindAllResults.FindReplace, FindAllResults.FindAllResults);
		}

		private void scSqlQueryLeft_KeyDown(object sender, KeyEventArgs e)
		{
			FindReplaceScintilla = sdcCompare.LeftScintilla;

			scSqlQuery_KeyDown(sender, e);
		}

		private void scSqlQueryRight_KeyDown(object sender, KeyEventArgs e)
		{
			FindReplaceScintilla = sdcCompare.RightScintilla;

			scSqlQuery_KeyDown(sender, e);
		}

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

		private void rightScintilla_UpdateUI(object sender, UpdateUIEventArgs e)
		{
			RightScintilla_UpdateUI?.Invoke(sender, e);

			SetNavigationButtonsAccessibilities();
		}

		private void leftScintilla_UpdateUI(object sender, UpdateUIEventArgs e)
		{
			LeftScintilla_UpdateUI?.Invoke(sender, e);

			SetNavigationButtonsAccessibilities();
		}

		private void cbCompareType_SelectedIndexChanged(object sender, EventArgs e)
		{
			InitScintilla();
		}

		private void tsbOpenLeft_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = CommonResource.AllFilesAll;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					String fileContent = System.IO.File.ReadAllText(openFileDialog.FileName);
					sdcCompare.TextLeft = sdcCompare.LeftScintilla.Text = fileContent;
				}
				catch (Exception ex)
				{
					LogHelper.Error(ex);
					MessageBox.Show(ex.Message, CommonResource.ImportFromFileFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void tsbOpenRight_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = CommonResource.AllFilesAll;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					String fileContent = System.IO.File.ReadAllText(openFileDialog.FileName);
					sdcCompare.TextRight = sdcCompare.RightScintilla.Text = fileContent;
				}
				catch (Exception ex)
				{
					LogHelper.Error(ex);
					MessageBox.Show(ex.Message, CommonResource.ImportFromFileFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void tsbBiggerChars_Click(object sender, EventArgs e)
		{
			FontSize += 2;
			InitScintilla();
		}

		private void tsbSmallerChars_Click(object sender, EventArgs e)
		{
			if (FontSize <= 6)
				return;

			FontSize -= 2;
			InitScintilla();
		}

		private void tsbFindResultPanelOpenClose_Click(object sender, EventArgs e)
		{
			FindResultCollapsed = !FindResultCollapsed;
		}

		private void tsbFindLeft_Click(object sender, EventArgs e)
		{
			FindReplaceScintilla = sdcCompare.LeftScintilla;

			UIHelper.HighlightWord(sdcCompare.LeftScintilla, ScintillaFindReplace, txtFind.Text, tsbCaseSensitive.CheckState == CheckState.Checked);
		}

		private void tsbFindRight_Click(object sender, EventArgs e)
		{
			FindReplaceScintilla = sdcCompare.RightScintilla;

			UIHelper.HighlightWord(sdcCompare.RightScintilla, ScintillaFindReplace, txtFind.Text, tsbCaseSensitive.CheckState == CheckState.Checked);
		}

		private void txtFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == (Int32)Keys.Enter)
			{
				FindReplaceScintilla = sdcCompare.LeftScintilla;

				UIHelper.HighlightWord(sdcCompare.LeftScintilla, ScintillaFindReplace, txtFind.Text, tsbCaseSensitive.CheckState == CheckState.Checked);
				gotoNextFind();
			}
		}
		#endregion

		#region Toolbar Events
		private void tsbFirst_Click(object sender, EventArgs e)
		{
			if (sdcCompare.DiffLocations.Any())
				jumpToLine(sdcCompare.DiffLocations.First(), true);
			SetNavigationButtonsAccessibilities();
		}

		private void tsbPrevious_Click(object sender, EventArgs e)
		{
			if (sdcCompare.DiffLocations.Any())
				jumpToLine(sdcCompare.DiffLocations.Where(d => d < sdcCompare.LeftScintilla.CurrentLine).Max(), true);
			SetNavigationButtonsAccessibilities();
		}

		private void tsbNext_Click(object sender, EventArgs e)
		{
			if (sdcCompare.DiffLocations.Any())
				jumpToLine(sdcCompare.DiffLocations.Where(d => d > sdcCompare.LeftScintilla.CurrentLine).Min(), true);
			SetNavigationButtonsAccessibilities();
		}

		private void tsbLast_Click(object sender, EventArgs e)
		{
			if (sdcCompare.DiffLocations.Any())
				jumpToLine(sdcCompare.DiffLocations.Last(), true);
			SetNavigationButtonsAccessibilities();
		}

		private void tsbRefresh_Click(object sender, EventArgs e)
		{

			sdcCompare.textRight = sdcCompare.RightScintilla.Text;
			sdcCompare.textLeft = sdcCompare.LeftScintilla.Text;
			sdcCompare.DiffTexts();
		}

		private void tsbSwap_Click(object sender, EventArgs e)
		{
			sdcCompare.SwapDiff();
		}

		private void tsbWrap_Click(object sender, EventArgs e)
		{
			tsbWrap.CheckState = tsbWrap.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
			sdcCompare.LeftScintilla.WrapMode
				= sdcCompare.RightScintilla.WrapMode
				= tsbWrap.Checked ? WrapMode.Word : WrapMode.None;
		}

		private void tsbSaveLeft_Click(object sender, EventArgs e)
		{
			UIHelper.SaveText(saveFileDialog, sdcCompare.LeftScintilla.Text);
		}

		private void tsbSaveRight_Click(object sender, EventArgs e)
		{
			UIHelper.SaveText(saveFileDialog, sdcCompare.RightScintilla.Text);
		}

		private void tsbFullScreen_Click(object sender, EventArgs e)
		{
			frmNotePadCompareFullScreen frm = new frmNotePadCompareFullScreen(
				NSqlTools.Types.Properties.CommonResource.NotPadCompare,
				sdcCompare.TextLeft,
				SourceSchemaName,
				SourceDBObjectName,
				sdcCompare.TextRight,
				TargetSchemaName,
				TargetDBObjectName,
				SelectedCompareType.Value)
			{
				WindowState = FormWindowState.Maximized
			};
			frm.ShowDialog();
		}

		private void tsbDown_Click(object sender, EventArgs e)
		{
			gotoNextFind();
		}

		private void tsbUp_Click(object sender, EventArgs e)
		{
			gotoPreviousFind();
		}

		private void tsbCaseSensitive_Click(object sender, EventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;
			button.CheckState = button.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
		}

		private void tsbSingleView_Click(object sender, EventArgs e)
		{
			ToolStripButton button = sender as ToolStripButton;
			if (button is null)
				return;

			sdcCompare.DiffStyle = button.CheckState == CheckState.Checked ? DiffStyle.DiffList : DiffStyle.DiffSideBySide;
		}
		#endregion

		#region Private Methods
		private void initForm()
		{
			sdcCompare.textLeft = String.Empty;
			sdcCompare.textRight = String.Empty;

			sdcCompare.LeftScintilla.WrapMode
				= sdcCompare.RightScintilla.WrapMode
				= tsbWrap.Checked ? WrapMode.Word : WrapMode.None;

			findAllResultsPanel = new FindAllResultsPanel
			{
				Scintilla = sdcCompare.LeftScintilla,
				Dock = DockStyle.Fill
			};
			scNotePadCompare.Panel2.Controls.Add(findAllResultsPanel);
			sdcCompare.LeftScintilla.KeyDown += scSqlQueryLeft_KeyDown;
			sdcCompare.RightScintilla.KeyDown += scSqlQueryRight_KeyDown;
			sdcCompare.LeftScintilla.UpdateUI += leftScintilla_UpdateUI;
			sdcCompare.RightScintilla.UpdateUI += rightScintilla_UpdateUI;

			fillCompareTypes();
			
			cbCompareType.SelectedItem =  CompareTypeContractList.First(l => l.CompareType == Lexer.Sql);
			scNotePadCompare.Panel2Collapsed = true;
		}

		private void setTextFromResource()
		{
			this.tsMenu.Text = CommonResource.Navigation;
			this.tsbSaveLeft.Text = CommonResource.SaveLeftNotePad;
			this.tsbSaveRight.Text = CommonResource.SaveRightNotePad;
			this.tsbOpenLeft.Text = CommonResource.OpenLeft;
			this.tsbFirst.Text = CommonResource.First;
			this.tsbPrevious.Text = CommonResource.Previous;
			this.tsbNext.Text = CommonResource.Next;
			this.tsbLast.Text = CommonResource.Last;
			this.tsbBiggerChars.Text = CommonResource.BiggerChars;
			this.tsbSmallerChars.Text = CommonResource.SmallerChars;
			this.tsbCaseSensitive.Text = CommonResource.CaseSensistive;
			this.tsbFindLeft.Text = CommonResource.FindLeft;
			this.tsbFindRight.Text = CommonResource.Find;
			this.tsbFindResultPanelOpenClose.Text = CommonResource.OpenCloseFindResultsPanel;
			this.tsbDown.Text = CommonResource.FindNext;
			this.tsbUp.Text = CommonResource.FindPrevious;
			this.tsbWrap.Text = CommonResource.Wrap;
			this.tsbFullScreen.Text = CommonResource.FullScreen;
			this.tsbRefresh.Text = CommonResource.Refresh;
			this.tsbSingleView.Text = CommonResource.SingleView;
			this.tsbSwap.Text = CommonResource.Swap;
			this.saveFileDialog.Filter = CommonResource.SaveFileDialogFilter;
			this.openFileDialog.Filter = CommonResource.AllFiles;
		}

		public void InitScintilla()
		{
			if (SelectedCompareTypeContract == null)
				return;

			UIHelper.InitialiseScintilla(sdcCompare.LeftScintilla, SelectedCompareType.Value, FontSize);
			UIHelper.InitialiseScintilla(sdcCompare.RightScintilla, SelectedCompareType.Value, FontSize);
		}

		private void setStatusLabel()
		{
			lblStatus.Text =
				SourceSchemaName == null || SourceDBObjectName == null || SourceSchemaName == null || SourceDBObjectName == null
				? null : String.Format(CommonResource.Source0Target12DifferencesWereFoundBetweenTheScripts,
					SourceSchemaName + "." + SourceDBObjectName,
					TargetDBObjectName == null ? "-" : TargetSchemaName + "." + TargetDBObjectName,
					sdcCompare.DiffLocations?.Count.ToString());
		}

		private bool jumpToLine(int lineNumber, bool backwards)
		{
			if (lineNumber < 0 || sdcCompare.DiffLocations == null || !sdcCompare.DiffLocations.Exists(d => d == lineNumber))
			{
				return false;
			}

			if (sdcCompare.DiffStyle == DiffStyle.DiffList)
			{
				int position = sdcCompare.LeftScintilla.Lines[lineNumber].Position;
				sdcCompare.LeftScintilla.GotoPosition(position);
				sdcCompare.LeftScintilla.ScrollCaret();
			}
			else if (sdcCompare.DiffStyle == DiffStyle.DiffSideBySide)
			{
				int position2 = sdcCompare.LeftScintilla.Lines[lineNumber].Position;
				sdcCompare.LeftScintilla.GotoPosition(position2);
				int position3 = sdcCompare.RightScintilla.Lines[lineNumber].Position;
				sdcCompare.RightScintilla.GotoPosition(position3);
				sdcCompare.LeftScintilla.ScrollCaret();
				sdcCompare.RightScintilla.ScrollCaret();
			}

			if (!backwards && lineNumber + 1 >= sdcCompare.DiffLocations.Count)
			{
				return false;
			}

			return true;
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
			CharacterRange characterRange = ScintillaFindReplace.FindNext(txtFind.Text);
			if (sdcCompare.RightScintilla.Focused)
			{
				FindReplaceScintilla = sdcCompare.RightScintilla;
				sdcCompare.RightScintilla.GotoPosition(characterRange.cpMax);
			}
			else
			{
				FindReplaceScintilla = sdcCompare.LeftScintilla;
				sdcCompare.LeftScintilla.GotoPosition(characterRange.cpMax);
			}
		}

		private void gotoPreviousFind()
		{
			CharacterRange characterRange = ScintillaFindReplace.FindPrevious(txtFind.Text);
			if (sdcCompare.RightScintilla.Focused)
			{
				FindReplaceScintilla = sdcCompare.RightScintilla;
				sdcCompare.RightScintilla.GotoPosition(characterRange.cpMin);
			}
			else
			{
				FindReplaceScintilla = sdcCompare.LeftScintilla;
				sdcCompare.LeftScintilla.GotoPosition(characterRange.cpMin);
			}
		}
		#endregion

		#region Public Methods
		public void SetNavigationButtonsAccessibilities()
		{
			Int32 currentLine = sdcCompare.LeftScintilla.CurrentLine;
			List<Int32> diffLocations = sdcCompare.DiffLocations;
			Boolean hasDifference = sdcCompare.DiffLocations != null && sdcCompare.DiffLocations.Any();

			tsbFirst.Enabled = hasDifference && diffLocations.Exists(d => d < currentLine);
			tsbPrevious.Enabled = hasDifference && diffLocations.Exists(d => d < currentLine);
			tsbNext.Enabled = hasDifference && diffLocations.Exists(d => d > currentLine);
			tsbLast.Enabled = hasDifference && diffLocations.Exists(d => d > currentLine);
		}

		public void PrepareLeftNotePad(String text, String schemaName = null, String dbObjectName = null, Lexer lexer = Lexer.Sql)
		{
			sdcCompare.LeftScintilla.Lexer = lexer; 
			sdcCompare.textLeft = text ?? String.Empty;
			sdcCompare.DiffTexts();
			SetNavigationButtonsAccessibilities();

			// Set Status Label
			SourceSchemaName = text == null ? null : schemaName;
			SourceDBObjectName = text == null ? null : dbObjectName;
			setStatusLabel();
		}

		public void PrepareRightNotePad(String text, String schemaName = null, String dbObjectName = null, Lexer lexer = Lexer.Sql)
		{
			sdcCompare.RightScintilla.Lexer = lexer; 
			sdcCompare.textRight = text ?? String.Empty;
			sdcCompare.DiffTexts();
			SetNavigationButtonsAccessibilities();

			// Set Status Label
			TargetSchemaName = text == null ? null : schemaName;
			TargetDBObjectName = text == null ? null : dbObjectName;
			setStatusLabel();
		}

		public void PrepareBothNotePads(String leftText, String leftSchemaName, String leftDBObjectName, String rightText, String rightSchemaName, String rightDBObjectName, Lexer lexer = Lexer.Sql)
		{
			sdcCompare.LeftScintilla.Lexer = lexer;
			sdcCompare.RightScintilla.Lexer = lexer;
			sdcCompare.textLeft = leftText ?? String.Empty;
			sdcCompare.textRight = rightText ?? String.Empty;
			sdcCompare.DiffTexts();
			SetNavigationButtonsAccessibilities();

			SourceSchemaName = leftText == null ? null : leftSchemaName;
			SourceDBObjectName = leftText == null ? null : leftDBObjectName;
			TargetSchemaName = rightText == null ? null : rightSchemaName;
			TargetDBObjectName = rightText == null ? null : rightDBObjectName;
			setStatusLabel();
		}

		public void NavigateToFirstDifference()
		{
			if (sdcCompare.DiffLocations != null && sdcCompare.DiffLocations.Any())
			{
				jumpToLine(sdcCompare.DiffLocations.First(), true);
				SetNavigationButtonsAccessibilities();
			}
		}

		public void SetCompareType(Lexer lexer)
		{
			cbCompareType.SelectedIndexChanged -= cbCompareType_SelectedIndexChanged;
			cbCompareType.SelectedItem = CompareTypeContractList.First(l => l.CompareType == lexer);
			cbCompareType.SelectedIndexChanged += cbCompareType_SelectedIndexChanged;
		}
		#endregion

		#region Interface Methods	
		public IList<Control> GetCustomTabSequence()
		{
			return new List<Control>
			{
				//tsbSaveLeft,
				//tsbSaveRight,
				//tsbOpenLeft,
				//tsbOpenRight,
				//tsbFirst,
				//tsbPrevious,
				//tsbNext,
				//tsbLast,
				//tsbBiggerChars,
				//tsbSmallerChars,
				//tsbCaseSensitive,
				//txtFind,
				//tsbFindLeft,
				//tsbFindRight,
				//tsbFindResultPanelOpenClose,
				//tsbDown,
				//tsbUp,
				//tsbWrap,
				//tsbFullScreen,
				//tsbRefresh,
				//tsbSingleView,
				//tsbSwap,
				sdcCompare
			};
		}
		#endregion
	}
}
