using NSqlTools.Types.Properties;
using ScintillaNET;
using System;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	public partial class frmNotePadCompareFullScreen : Form
	{
		#region Constructor
		public frmNotePadCompareFullScreen(String title, String sourceNotePadText, String sourceSchemaName, String sourceDBObjectName, String targetNotePadText, String targetSchemaName, String targetDBObjectName, Lexer lexer = Lexer.Sql)
		{
			InitializeComponent();
			setTextFromResource();
			InitForm(title, sourceNotePadText, sourceSchemaName, sourceDBObjectName, targetNotePadText, targetSchemaName, targetDBObjectName, lexer);
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

		#region Methods
		public void InitForm(String title, String sourceNotePadText, String sourceSchemaName, String sourceDBObjectName, String targetNotePadText, String targetSchemaName, String targetDBObjectName, Lexer lexer = Lexer.Sql)
		{
			this.Text = title;

			ucNotePadCompareControl.SetCompareType(lexer);
			ucNotePadCompareControl.InitScintilla();
			ucNotePadCompareControl.PrepareBothNotePads(sourceNotePadText, sourceSchemaName, sourceDBObjectName, targetNotePadText, targetSchemaName, targetDBObjectName, lexer);
			ucNotePadCompareControl.NavigateToFirstDifference();
		}

		private void setTextFromResource()
		{
			this.Text = CommonResource.NotePadCompare;
		}
		#endregion
	}
}
