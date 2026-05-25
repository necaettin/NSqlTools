using NSqlTools.Types;
using NSqlTools.Types.Properties;
using ScintillaNET;
using System;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	public partial class frmNotePadFullScreen : Form
	{
		#region Constructors
		public frmNotePadFullScreen(DBObjectContract dBObjectContract, String searchKeyword, Lexer lexer = Lexer.Sql, Boolean compareTypeVisible = false, Boolean caseSensitive = false)
		{
			InitializeComponent();
			setTextFromResource();

			initForm(dBObjectContract, searchKeyword, lexer, compareTypeVisible, caseSensitive);
		}
		#endregion

		#region Events
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				this.Close();

				return true; // Tuş işlenmiş olarak işaretlenir
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
		#endregion

		#region Events
		private void initForm(DBObjectContract dBObjectContract, String searchKeyword, Lexer lexer, Boolean compareTypeVisible = false, Boolean caseSensitive = false)
		{
			ucSqlNotePadControl.CompareTypeVisible = compareTypeVisible;
			if (compareTypeVisible)
				ucSqlNotePadControl.Title = null;
			ucSqlNotePadControl.SetCompareType(lexer);
			ucSqlNotePadControl.InitialiseScintilla();
			ucSqlNotePadControl.SetDBObject(dBObjectContract);

			if (!String.IsNullOrWhiteSpace(searchKeyword))
			{
				ucSqlNotePadControl.HighlightWordAndGotoNextFind(searchKeyword, caseSensitive);
				ucSqlNotePadControl.SearchKeyword = searchKeyword;
			}
		}

		private void setTextFromResource()
		{
			this.Text = CommonResource.NotePad;
		}
		#endregion
	}
}
