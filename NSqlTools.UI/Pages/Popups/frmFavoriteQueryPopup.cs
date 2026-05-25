using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NSqlTools.UI.Popups
{
    public partial class frmFavoriteQueryPopup : BasePopup
	{
		#region Variables
        private List<FavoriteQueryContract> favoriteQueryList;
		
		private FavoriteQueryContract favoriteQuery;

		private bool isEditMode;
		#endregion

		#region Constructors
		public frmFavoriteQueryPopup(List<FavoriteQueryContract> favoriteQueryList, String queryText = null)
        {
			init(false, favoriteQueryList);

			favoriteQuery = new FavoriteQueryContract() { UniqueId = Guid.NewGuid().ToString(), QueryText = queryText };
			if(!String.IsNullOrWhiteSpace(queryText))
				ucSqlNotePad.SetDBObject(new DBObjectContract() { Name = favoriteQuery.Name, Definition = queryText });
		}

		public frmFavoriteQueryPopup(FavoriteQueryContract query, List<FavoriteQueryContract> favoriteQueryList) 
        {
			init(true, favoriteQueryList);

            if (query != null)
            {
                favoriteQuery = new FavoriteQueryContract
                {
                    Name = query.Name,
                    Description = query.Description,
                    QueryText = query.QueryText,
                    CreatedDate = query.CreatedDate,
					UniqueId = query.UniqueId
                };
                txtName.Text = favoriteQuery.Name;
                txtDescription.Text = favoriteQuery.Description;
                ucSqlNotePad.SetDBObject(new DBObjectContract() { Name = favoriteQuery.Name, Definition = favoriteQuery.QueryText });
            }
        }
		#endregion

		#region Events
		private void tsbOK_Click(object sender, EventArgs e)
        {
            if (!validateFields())
                return;

            favoriteQuery.Name = txtName.Text.Trim();
            favoriteQuery.Description = txtDescription.Text.Trim();
            favoriteQuery.QueryText = ucSqlNotePad.NotePadText;
            if (!isEditMode)
                favoriteQuery.CreatedDate = DateTime.Now;
            DialogResult = DialogResult.OK;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
		#endregion

		#region Methods
		private void init(Boolean _isEditMode, List<FavoriteQueryContract> _favoriteQueryList)
		{
			InitializeComponent();

			this.isEditMode = _isEditMode;
			this.favoriteQueryList = _favoriteQueryList;

			txtName.Enabled = !_isEditMode;

			setTextFromResource();
		}

		private bool validateFields()
        {
            bool isValid = true;

			// Name validation
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider.SetError(txtName, CommonResource.NameisRequired);
                isValid = false;
            }
            else
            {
				// Name uniqueness validation
				if (favoriteQueryList != null && favoriteQueryList.Any(f => f.UniqueId != favoriteQuery.UniqueId && f.Name == txtName.Text))
				{
					errorProvider.SetError(txtName, CommonResource.NameShouldBeUnique);
					isValid = false;
				}
				else
				{
					errorProvider.SetError(txtName, null);
				}
			}


			// Query validation
            if (string.IsNullOrWhiteSpace(ucSqlNotePad.NotePadText))
            {
                errorProvider.SetError(ucSqlNotePad, CommonResource.QueryTextisRequired);
                isValid = false;
            }
            else
            {
                errorProvider.SetError(ucSqlNotePad, null);
            }


            return isValid;
        }

        private void setTextFromResource()
        {
            this.lblName.Text = CommonResource.Name;
            this.lblDescription.Text = CommonResource.Description;
            this.tsbOK.Text = CommonResource.Ok;
            this.tsbClose.Text = CommonResource.Close;
            this.Text = isEditMode ? CommonResource.EditFavoriteQuery : CommonResource.AddFavoriteQuery;
			this.lblCreateDate.Text = CommonResource.CreatedDate;
		}

        public FavoriteQueryContract GetFavoriteQuery()
        {
            return favoriteQuery;
        }
		#endregion

		#region Override Methods
		#endregion
	}
}
