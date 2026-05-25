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
    public partial class ucProjects : BaseUserControl
    {
		#region Properties
		private List<ProjectContract> projectsContractList;
        
		private readonly ProjectsBusiness repository = new ProjectsBusiness();

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
					dgvProjects
				};
			}
		}
		#endregion

		#region Constructor
		public ucProjects()
        {
            InitializeComponent();
            setTextFromResource();
        }
		#endregion

		#region Events
		private void tsbAddProject_Click(object sender, EventArgs e)
        {
			FormDataBusiness formDataBusiness = new FormDataBusiness();

			ProjectContract projectContract = new ProjectContract()
			{
				ScreenDataListContract = MainFormRef.GetOpenedFormsInfo(),
				AllProjectContractList = projectsContractList,
				CreatedDate = DateTime.Now,
				UpdateDate = DateTime.MinValue,
				UniqueId = Guid.NewGuid().ToString()
			};

			frmProjectPopup frm = new frmProjectPopup(MainFormRef, projectContract, false);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                repository.Add(frm.ProjectContract);
				formDataBusiness.SaveAll(projectContract.ScreenDataListContract, Constants.GetProjectFile(frm.ProjectContract.UniqueId));
				
				LoadProjects();
            }
        }

        private void tsbEditProject_Click(object sender, EventArgs e)
        {
			editProject(dgvProjects.CurrentRow);
        }

		private void editProject(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
				return;

			FormDataBusiness formDataBusiness = new FormDataBusiness();

			ProjectContract projectContract = (ProjectContract)dataGridViewRow.DataBoundItem;
			projectContract.ScreenDataListContract = MainFormRef.GetFormsInfo(Constants.GetProjectFile(projectContract.UniqueId));
			projectContract.AllProjectContractList = projectsContractList;

			frmProjectPopup frm = new frmProjectPopup(MainFormRef, projectContract, true);
			if (frm.ShowDialog() == DialogResult.OK)
			{
				frm.ProjectContract.UpdateDate = DateTime.Now;
				repository.Update(frm.ProjectContract);

				formDataBusiness.SaveAll(projectContract.ScreenDataListContract, Constants.GetProjectFile(frm.ProjectContract.UniqueId));

				LoadProjects();
			}
		}

        private void tsbDeleteProject_Click(object sender, EventArgs e)
        {
            if (dgvProjects.CurrentRow == null) return;
            var selected = (ProjectContract)dgvProjects.CurrentRow.DataBoundItem;
            if (MessageBox.Show(CommonResource.AreYouSureYouWantToDeleteit, CommonResource.Confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                repository.Delete(selected.UniqueId);

				String fileName = Constants.GetProjectFile(selected.UniqueId);
				if (System.IO.File.Exists(fileName))
					System.IO.File.Delete(fileName);

				LoadProjects();
            }
        }

        private void dgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            bool rowSelected = dgvProjects.CurrentRow != null && projectsContractList != null;
            tsbEditProject.Enabled = rowSelected;
            tsbProjectOpen.Enabled = rowSelected;
			tsbDeleteProject.Enabled = rowSelected;
        }

        private void dgvProjects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hit = dgvProjects.HitTest(e.X, e.Y);
			if (hit.RowIndex < 0)
				return;

			editProject(dgvProjects.Rows[hit.RowIndex]);
        }

		private void tsbProjectOpen_Click(object sender, EventArgs e)
		{
			ProjectContract projectsContract = (ProjectContract)dgvProjects?.CurrentRow?.DataBoundItem;
			
			MainFormRef.LoadFormsInfo(Constants.GetProjectFile(projectsContract?.UniqueId));
		}
		#endregion

		#region Methods
		private void BindGrid()
		{
			var list = projectsContractList ?? new List<ProjectContract>();
			dgvProjects.BindList(list);
		}

		private void LoadProjects()
        {
            projectsContractList = repository.GetAll();
			dgvProjects.AutoGenerateColumns = false;
			BindGrid();
			setStatusLabel();
        }

        private void setStatusLabel()
        {
            lblStatus.Text = projectsContractList == null ? null : string.Format(CommonResource.XScreenPackages, projectsContractList.Count);
        }

        private void setTextFromResource()
        {
            this.tsbAddProject.Text = CommonResource.AddProject;
            this.tsbEditProject.Text = CommonResource.EditProject;
            this.tsbDeleteProject.Text = CommonResource.DeleteProject;
            this.NameColumn.HeaderText = CommonResource.Name;
            this.DescriptionColumn.HeaderText = CommonResource.Description;
            this.CreatedDateColumn.HeaderText = CommonResource.CreatedDate;
            this.UpdateDateColumn.HeaderText = CommonResource.UpdateDate;
			this.tsbProjectOpen.Text = CommonResource.OpenProject;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			LoadProjects();
			dgvProjects.AutoGenerateColumns = false;
			setStatusLabel();
		}

		public override BaseScreenDataContract GetFormData()
		{
			return new ProjectsScreenDataContract()
			{
				Name = CommonResource.Projects
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
		}
		#endregion
	}
}
