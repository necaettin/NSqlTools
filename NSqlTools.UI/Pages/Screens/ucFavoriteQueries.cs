using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Popups;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
    public partial class ucFavoriteQueries : BaseUserControl
    {
		#region Properties
		private List<FavoriteQueryContract> favoriteQueryList;
        private readonly FavoriteQueryBusiness repository = new FavoriteQueryBusiness();

		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					dgvFavoriteQueries
				};
			}
		}
		#endregion

		#region Constructor
		public ucFavoriteQueries()
        {
            InitializeComponent();
            setTextFromResource();
        }
		#endregion

		#region Events
		private void tsbAddFavoriteQuery_Click(object sender, EventArgs e)
        {
            frmFavoriteQueryPopup frm = new frmFavoriteQueryPopup(favoriteQueryList);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var newQuery = frm.GetFavoriteQuery();
                newQuery.CreatedDate = DateTime.Now;
                repository.Add(newQuery);
                LoadFavoriteQueries();
            }
        }

        private void tsbEditFavoriteQuery_Click(object sender, EventArgs e)
        {
            if (dgvFavoriteQueries.CurrentRow == null) return;
            var selected = (FavoriteQueryContract)dgvFavoriteQueries.CurrentRow.DataBoundItem;
            frmFavoriteQueryPopup frm = new frmFavoriteQueryPopup(selected, favoriteQueryList);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var updated = frm.GetFavoriteQuery();
                repository.Update(updated);
                LoadFavoriteQueries();
            }
        }

        private void tsbDeleteFavoriteQuery_Click(object sender, EventArgs e)
        {
            if (dgvFavoriteQueries.CurrentRow == null) return;
            var selected = (FavoriteQueryContract)dgvFavoriteQueries.CurrentRow.DataBoundItem;
            if (MessageBox.Show(CommonResource.AreYouSureYouWantToDeleteit, CommonResource.Confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                repository.Delete(selected.Name);
                LoadFavoriteQueries();
            }
        }

        private void dgvFavoriteQueries_SelectionChanged(object sender, EventArgs e)
        {
            bool rowSelected = dgvFavoriteQueries.CurrentRow != null && favoriteQueryList != null;
            tsbEditFavoriteQuery.Enabled = rowSelected;
            tsbDeleteFavoriteQuery.Enabled = rowSelected;
        }

        private void dgvFavoriteQueries_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hit = dgvFavoriteQueries.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0)
            {
                DataGridViewRow clickedRow = dgvFavoriteQueries.Rows[hit.RowIndex];
                var selected = (FavoriteQueryContract)clickedRow.DataBoundItem;
                frmFavoriteQueryPopup frm = new frmFavoriteQueryPopup(selected, favoriteQueryList);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var updated = frm.GetFavoriteQuery();
                    updated.CreatedDate = selected.CreatedDate;
                    repository.Update(updated);
                    LoadFavoriteQueries();
                }
            }
        }
		#endregion

		#region Methods
		private void BindGrid()
		{
			var list = favoriteQueryList ?? new List<FavoriteQueryContract>();
			dgvFavoriteQueries.BindList(list);
		}

		private void LoadFavoriteQueries()
        {
            favoriteQueryList = repository.GetAll();
			dgvFavoriteQueries.AutoGenerateColumns = false;

			BindGrid();
			//dgvFavoriteQueries.DataSource = null;
   //         dgvFavoriteQueries.DataSource = favoriteQueryList == null ? null : new SortableBindingList<FavoriteQueryContract>(favoriteQueryList);
            
			
			setStatusLabel();
        }

        private void setStatusLabel()
        {
            lblStatus.Text = favoriteQueryList == null ? null : string.Format(CommonResource._0FavoriteQueryies, favoriteQueryList.Count);
        }

        private void setTextFromResource()
        {
            this.tsbAddFavoriteQuery.Text = CommonResource.AddFavoriteQuery;
            this.tsbEditFavoriteQuery.Text = CommonResource.EditFavoriteQuery;
            this.tsbDeleteFavoriteQuery.Text = CommonResource.DeleteFavoriteQuery;
            this.NameColumn.HeaderText = CommonResource.Name;
            this.DescriptionColumn.HeaderText = CommonResource.Description;
            this.CreatedDateColumn.HeaderText = CommonResource.CreatedDate;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			LoadFavoriteQueries();
			dgvFavoriteQueries.AutoGenerateColumns = false;
			setStatusLabel();
		}

		public override BaseScreenDataContract GetFormData()
		{
			return new FavoriteQueryScreenDataContract() {
				Name = CommonResource.FavoriteQueries
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
		}
		#endregion
	}
}
