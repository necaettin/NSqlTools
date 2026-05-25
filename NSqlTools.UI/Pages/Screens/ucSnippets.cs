using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.IntellisenseContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Popups;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
    public partial class ucSnippet: BaseUserControl
    {
		#region Properties
		private List<SnippetContract> snippetContractList = null;
        
		private readonly SnippetsBusiness repository = new SnippetsBusiness();

		public frmMain MainFormRef 
		{ 
			get{
				return ((frmMain)MainForm);
			} 
		}

		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					dgvSnippets
				};
			}
		}
		#endregion

		#region Constructor
		public ucSnippet()
        {
            InitializeComponent();
            setTextFromResource();
        }
		#endregion

		#region Events
		private void tsbAddSnippet_Click(object sender, EventArgs e)
        {
			SnippetContract snippetContract = new SnippetContract()
			{
				AllSnippetContractList = snippetContractList,
				UniqueId = Guid.NewGuid().ToString()
			};

			frmSnippetPopup frm = new frmSnippetPopup(MainFormRef, snippetContract, false);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                repository.Add(frm.SnippetContract);
				
				LoadSnippets();
            }
        }

        private void tsbEditSnippet_Click(object sender, EventArgs e)
        {
			editSnippet(dgvSnippets.CurrentRow);
        }

		private void editSnippet(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
				return;

			SnippetContract snippetContract = (SnippetContract)dataGridViewRow.DataBoundItem;
			snippetContract.AllSnippetContractList = snippetContractList;

			frmSnippetPopup frm = new frmSnippetPopup(MainFormRef, snippetContract, true);
			if (frm.ShowDialog() == DialogResult.OK)
			{
				repository.Update(frm.SnippetContract);

				LoadSnippets();
			}
		}

        private void tsbDeleteSnippet_Click(object sender, EventArgs e)
        {
            if (dgvSnippets.CurrentRow == null) return;
            var selected = (SnippetContract)dgvSnippets.CurrentRow.DataBoundItem;
            if (MessageBox.Show(CommonResource.AreYouSureYouWantToDeleteit, CommonResource.Confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                repository.Delete(selected.UniqueId);

				LoadSnippets();
            }
        }

        private void dgvSnippet_SelectionChanged(object sender, EventArgs e)
        {
            bool rowSelected = dgvSnippets.CurrentRow != null && snippetContractList != null;
            tsbEditSnippet.Enabled = rowSelected;
			tsbDeleteSnippet.Enabled = rowSelected;
        }

        private void dgvSnippet_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hit = dgvSnippets.HitTest(e.X, e.Y);
			if (hit.RowIndex < 0)
				return;

			editSnippet(dgvSnippets.Rows[hit.RowIndex]);
        }
		#endregion

		#region Methods
		private void BindGrid()
		{
			var list = snippetContractList ?? new List<SnippetContract>();
			dgvSnippets.BindList(list);
		}

		private void LoadSnippets()
        {
            snippetContractList = repository.GetAll();
			dgvSnippets.AutoGenerateColumns = false;
			BindGrid();
			setStatusLabel();
        }

        private void setStatusLabel()
        {
            lblStatus.Text = snippetContractList == null ? null : string.Format(CommonResource.XScreenPackages, snippetContractList.Count);
        }

        private void setTextFromResource()
        {
			this.tsbAddSnippet.Text = CommonResource.AddSnippet;
            this.tsbEditSnippet.Text = CommonResource.UpdateSnippet;
			this.tsbDeleteSnippet.Text = CommonResource.DeleteSnippet;
			this.NameColumn.HeaderText = CommonResource.Shortcut;
            this.DescriptionColumn.HeaderText = CommonResource.Description;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			LoadSnippets();
			dgvSnippets.AutoGenerateColumns = false;
			setStatusLabel();
		}

		public override BaseScreenDataContract GetFormData()
		{
			return new SnippetScreenDataContract()
			{
				Name = CommonResource.Snippets
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
		}
		#endregion
	}
}
