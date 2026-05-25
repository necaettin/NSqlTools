using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Pages;
using NSqlTools.UI.UserControls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSqlTools.UI.Popups
{
    public partial class frmProjectPopup : BasePopup
	{
		#region Variables
		public frmMain mainFormRef { get; set; }

		public ProjectContract projectContract;
		public ProjectContract ProjectContract 
		{ 
			get{
				return projectContract;
			} 
			set{
				projectContract = value;
				
				projectContractToScreen();
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
				
				this.Text = isUpdateMode ? CommonResource.EditProject : CommonResource.AddNewProject;
			}
		}

        private ScreenCardControl draggedCard;
        private Point dragStartPoint;

		private int dragSourceIndex = -1;
		#endregion

		#region Constructors
		public frmProjectPopup(frmMain mainFormRef, ProjectContract projectsContract, Boolean isUpdateMode)
        {
			InitializeComponent();
			setTextFromResource();

			this.IsUpdateMode = isUpdateMode;
			this.ProjectContract = projectsContract;
			this.mainFormRef = mainFormRef;
		}
		#endregion

		#region Events
		private void tsbOK_Click(object sender, EventArgs e)
        {
            if (!validateFields())
                return;

			screenToProjectContract();

			DialogResult = DialogResult.OK;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			ScreenDataListContract formDataContract = this.mainFormRef.GetOpenedFormsInfo();
			ProjectContract.ScreenDataListContract.BaseScreenDataContractList = formDataContract.BaseScreenDataContractList;
			RefreshListView();
		}
		#endregion

		#region Methods
		private void projectContractToScreen()
		{
			txtName.Text = ProjectContract.Name;
			txtDescription.Text = ProjectContract.Description;
			dtpCreateDate.Value = ProjectContract.CreatedDate;
			if(!ProjectContract.UpdateDate.HasValue || ProjectContract.UpdateDate == DateTime.MinValue)
			{
				dtpUpdateDate.Checked = false;
			}
			else
			{
				dtpUpdateDate.Checked = true;
				dtpUpdateDate.Value = ProjectContract.UpdateDate.Value;
			}
			RefreshListView();
		}

		private void screenToProjectContract()
		{
			ProjectContract.Name = txtName.Text;
			ProjectContract.Description = txtDescription.Text;
			ProjectContract.CreatedDate = dtpCreateDate.Value;
			ProjectContract.UpdateDate = dtpUpdateDate.Checked ? (DateTime?)dtpUpdateDate.Value : null;
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
				if (ProjectContract.AllProjectContractList != null && ProjectContract.AllProjectContractList.Any(f => f.UniqueId != ProjectContract.UniqueId && f.Name == txtName.Text))
				{
					errorProvider.SetError(txtName, CommonResource.NameShouldBeUnique);
					isValid = false;
				}
				else
				{
					errorProvider.SetError(txtName, null);
				}
			}

            return isValid;
        }

        private void setTextFromResource()
        {
            this.lblName.Text = CommonResource.Name;
            this.lblDescription.Text = CommonResource.Description;
            this.tsbOK.Text = CommonResource.Ok;
            this.tsbClose.Text = CommonResource.Close;
            this.Text = isUpdateMode ? CommonResource.EditProject : CommonResource.AddNewProject;
			this.lblCreateDate.Text = CommonResource.CreatedDate;
			this.lblUpdateDate.Text = CommonResource.UpdateDate;
			this.tsbRefresh.Text = CommonResource.FillWithCurrentOpenedDocuments;
			this.gbScreens.Text = CommonResource.Screens;
			this.gbScreenPackageInfo.Text = CommonResource.ScreenPackageInfo;
		}

		private void RefreshListView()
		{
			flpScreens.Controls.Clear();
			var list = ProjectContract.ScreenDataListContract.BaseScreenDataContractList;
			foreach ( var item in list ) {
				var card = new ScreenCardControl(item);
				card.Width = flpScreens.ClientSize.Width - 10;
				card.UpClicked += (s, e) => MoveCardUp(item);
				card.DownClicked += (s, e) => MoveCardDown(item);
				card.DeleteClicked += (s, e) => DeleteCard(item);
				flpScreens.Controls.Add(card);
			}
			flpScreens.Resize += (s, e) =>
			{
				foreach (ScreenCardControl card in flpScreens.Controls)
				{
					card.Width = flpScreens.ClientSize.Width - 10;
				}
			};
			updateAllUpDownButtons();
			EnableDragDropOnCards(); // Drag & drop desteði eklendi
		}

        private void EnableDragDropOnCards()
        {
            foreach (ScreenCardControl card in flpScreens.Controls)
            {
                card.MouseDown -= Card_MouseDown;
                card.MouseMove -= Card_MouseMove;
                card.MouseUp -= Card_MouseUp;
                card.AllowDrop = false;
                card.MouseDown += Card_MouseDown;
                card.MouseMove += Card_MouseMove;
                card.MouseUp += Card_MouseUp;
                card.AllowDrop = true;
                card.DragEnter -= Card_DragEnter;
                card.DragEnter += Card_DragEnter;
                card.DragDrop -= Card_DragDrop;
                card.DragDrop += Card_DragDrop;
            }
            flpScreens.AllowDrop = true;
            flpScreens.DragOver -= FlpScreens_DragOver;
            flpScreens.DragOver += FlpScreens_DragOver;
            flpScreens.DragDrop -= FlpScreens_DragDrop;
            flpScreens.DragDrop += FlpScreens_DragDrop;
        }

        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggedCard = sender as ScreenCardControl;
                dragStartPoint = e.Location;
                dragSourceIndex = flpScreens.Controls.GetChildIndex(draggedCard);
            }
        }

        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedCard != null && e.Button == MouseButtons.Left)
            {
                // Sürükleme mesafesi küçükse baþlatma
                if (Math.Abs(e.X - dragStartPoint.X) > SystemInformation.DragSize.Width ||
                    Math.Abs(e.Y - dragStartPoint.Y) > SystemInformation.DragSize.Height)
                {
                    draggedCard.DoDragDrop(draggedCard, DragDropEffects.Move);
                }
            }
        }

        private void Card_MouseUp(object sender, MouseEventArgs e)
        {
            draggedCard = null;
            dragSourceIndex = -1;
        }

        private void Card_DragEnter(object sender, DragEventArgs e)
        {
	        e.Effect = e.Data.GetDataPresent(typeof(ScreenCardControl)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void Card_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ScreenCardControl))) return;
            var targetCard = sender as ScreenCardControl;
            var sourceCard = e.Data.GetData(typeof(ScreenCardControl)) as ScreenCardControl;
            if (targetCard == null || sourceCard == null || targetCard == sourceCard) return;

            int targetIndex = flpScreens.Controls.GetChildIndex(targetCard);
            int sourceIndex = flpScreens.Controls.GetChildIndex(sourceCard);
            if (targetIndex == sourceIndex) return;

            // Listeyi güncelle
            var list = ProjectContract.ScreenDataListContract.BaseScreenDataContractList;
            var item = list[sourceIndex];
            list.RemoveAt(sourceIndex);
            list.Insert(targetIndex, item);
            RefreshListView();
        }

        private void FlpScreens_DragOver(object sender, DragEventArgs e)
        {
	        e.Effect = e.Data.GetDataPresent(typeof(ScreenCardControl)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void FlpScreens_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ScreenCardControl))) return;
            var sourceCard = e.Data.GetData(typeof(ScreenCardControl)) as ScreenCardControl;
            if (sourceCard == null) return;
            int sourceIndex = flpScreens.Controls.GetChildIndex(sourceCard);
            int targetIndex = flpScreens.Controls.Count - 1;
            // Mouse konumuna göre hedef index bul
            Point p = flpScreens.PointToClient(new Point(e.X, e.Y));
            for (int i = 0; i < flpScreens.Controls.Count; i++)
            {
                var c = flpScreens.Controls[i];
                if (c.Bounds.Contains(p))
                {
                    targetIndex = i;
                    break;
                }
            }
            if (targetIndex == sourceIndex) return;
            var list = ProjectContract.ScreenDataListContract.BaseScreenDataContractList;
            var item = list[sourceIndex];
            list.RemoveAt(sourceIndex);
            list.Insert(targetIndex, item);
            RefreshListView();
        }

		private void updateAllUpDownButtons()
		{
			for (int i = 0; i < flpScreens.Controls.Count; i++)
			{
				if (flpScreens.Controls[i] is ScreenCardControl card)
				{
					card.UpdateUpDownButtons(i, flpScreens.Controls.Count);
				}
			}
		}

		private void MoveCardUp(BaseScreenDataContract item)
		{
			var list = ProjectContract.ScreenDataListContract.BaseScreenDataContractList;
			int idx = list.IndexOf(item);
			if (idx > 0)
			{
				(list[idx - 1], list[idx]) = (list[idx], list[idx - 1]);
				RefreshListView();
			}
		}

		private void MoveCardDown(BaseScreenDataContract item)
		{
			var list = ProjectContract.ScreenDataListContract.BaseScreenDataContractList;
			int idx = list.IndexOf(item);
			if (idx < list.Count - 1)
			{
				(list[idx + 1], list[idx]) = (list[idx], list[idx + 1]);
				RefreshListView();
			}
		}

		private void DeleteCard(BaseScreenDataContract item)
		{
			var list = ProjectContract.ScreenDataListContract.BaseScreenDataContractList;
			list.Remove(item);
			RefreshListView();
		}
		#endregion

		#region Override Methods
		#endregion
	}
}
