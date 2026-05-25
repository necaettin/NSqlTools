using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.IntellisenseContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Pages;
using NSqlTools.UI.UserControls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSqlTools.UI.Popups
{
    public partial class frmSnippetPopup : BasePopup
	{
		#region Variables
		public frmMain mainFormRef { get; set; }

		public SnippetContract snippetContract;
		public SnippetContract SnippetContract 
		{ 
			get{
				return snippetContract;
			} 
			set{
				snippetContract = value;
				
				snippetContractToScreen();
			}
		}

		private bool isUpdateMode;
		private bool IsUpdateMode
		{ 
			get
			{
				return isUpdateMode;
			}
			set
			{
				isUpdateMode = value;

				this.Text = isUpdateMode ? CommonResource.UpdateSnippet: CommonResource.AddSnippet;
			}
		}

        private ScreenCardControl draggedCard = null;
        private Point dragStartPoint;
        private int dragSourceIndex = -1;
		#endregion

		#region Constructors
		public frmSnippetPopup(frmMain mainFormRef, SnippetContract snippetContract, Boolean isUpdateMode)
        {
			InitializeComponent();
			setTextFromResource();

			this.IsUpdateMode = isUpdateMode;
			this.SnippetContract = snippetContract;
			this.mainFormRef = mainFormRef;
		}
		#endregion

		#region Events
		private void tsbOK_Click(object sender, EventArgs e)
        {
            if (!validateFields())
                return;

			screenToSnippetContract();

			DialogResult = DialogResult.OK;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
		#endregion

		#region Methods
		private void snippetContractToScreen()
		{
			txtShortcut.Text = SnippetContract.Shortcut;
			txtDescription.Text = SnippetContract.Description;
			if (!String.IsNullOrWhiteSpace(SnippetContract.Expansion))
				ucSqlNotePad.SetDBObject(new DBObjectContract() { Definition = SnippetContract.Expansion });
		}

		private void screenToSnippetContract()
		{
			SnippetContract.Shortcut = txtShortcut.Text;
			SnippetContract.Description = txtDescription.Text;
			SnippetContract.Expansion = ucSqlNotePad.NotePadText;
		}

		private bool validateFields()
        {
            bool isValid = true;

			// Name validation
            if (string.IsNullOrWhiteSpace(txtShortcut.Text))
            {
                errorProvider.SetError(txtShortcut, CommonResource.ShortcutIsRequired);
                isValid = false;
            }
			//else if (!txtShortcut.Text.StartsWith("#"))
			//{
			//	errorProvider.SetError(txtShortcut, CommonResource.ShortcutMustStartWith);
			//	isValid = false;
			//}
			else if (string.IsNullOrWhiteSpace(txtDescription.Text))
			{
				errorProvider.SetError(txtDescription, CommonResource.DescriptionIsRequired);
				isValid = false;
			}
			else if (string.IsNullOrWhiteSpace(ucSqlNotePad.NotePadText))
			{
				errorProvider.SetError(ucSqlNotePad, CommonResource.ExpansionIsRequired);
				isValid = false;
			}
			else
			{
				// Name uniqueness validation
				if (SnippetContract.AllSnippetContractList != null && SnippetContract.AllSnippetContractList.Any(f => f.UniqueId != SnippetContract.UniqueId && f.Shortcut == txtShortcut.Text))
				{
					errorProvider.SetError(txtShortcut, CommonResource.ShortcutShouldBeUnique);
					isValid = false;
				}
				else
				{
					errorProvider.SetError(txtShortcut, null);
				}
			}

            return isValid;
        }

        private void setTextFromResource()
        {
			this.lblExpansion.Text = CommonResource.SqlScript;
			this.lblShortcut.Text = CommonResource.Shortcut;
            this.lblDescription.Text = CommonResource.Description;
            this.tsbOK.Text = CommonResource.Ok;
            this.tsbClose.Text = CommonResource.Close;
            this.Text = isUpdateMode ? CommonResource.UpdateSnippet : CommonResource.AddSnippet;
		}
		#endregion

		#region Override Methods
		#endregion
	}
}
