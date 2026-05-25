using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Popups;
using NSqlTools.Lib.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Zuby.ADGV;

namespace NSqlTools.UI.Pages
{
	public partial class ucConnectionStrings : BaseUserControl
	{
		#region Constructors
		public ucConnectionStrings()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Properties
		private List<ConnectionStringContract> connectionStringContractList;
		public List<ConnectionStringContract> ConnectionStringContractList
		{
			get
			{
				return connectionStringContractList;
			}
			set
			{
				connectionStringContractList = value;
			}
		}
		#endregion

		#region Events
		private void tsbAddDataSource_Click(object sender, EventArgs e)
		{
			frmConnectionStringDefinitionPopup frm = new frmConnectionStringDefinitionPopup(ConnectionStringContractList);
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (ConnectionStringContractList == null)
					ConnectionStringContractList = new List<ConnectionStringContract>();
				ConnectionStringContractList.Add(frm.GetConnectionStringContract());

				// grid'i yeniden doldur
				BindGrid();

				setStatusLabel();

				saveConnectionStringsToFile();
			}
		}

		private void tsbEditDataSource_Click(object sender, EventArgs e)
		{
			if (dgvConnectionStrings?.CurrentRow == null)
				return;

			// CurrentRow.DataBoundItem DataRowView olacak, Id ile orijinal liste elemanini bul
			var drv = dgvConnectionStrings.CurrentRow.DataBoundItem as ConnectionStringContract;
			if (drv == null)
				return;

			string id = drv.Id.ToString();
			ConnectionStringContract connectionStringContract = ConnectionStringContractList?.FirstOrDefault(c => c.Id == id);
			if (connectionStringContract == null)
				return;

			openDataSourceForEdit(connectionStringContract);
		}

		private void tsbDeleteDataSource_Click(object sender, EventArgs e)
		{
			if (dgvConnectionStrings?.CurrentRow == null)
				return;

			var drv = dgvConnectionStrings.CurrentRow.DataBoundItem as ConnectionStringContract;
			if (drv == null)
				return;

			string id = drv.Id.ToString();
			ConnectionStringContract connectionStringContract = ConnectionStringContractList?.FirstOrDefault(c => c.Id == id);
			if (connectionStringContract == null)
				return;

			if (MessageBox.Show(CommonResource.AreYouSureYouWantToDeleteit, CommonResource.Confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ConnectionStringContractList.Remove(ConnectionStringContractList.First(c => c.Id == connectionStringContract.Id));
				if (!ConnectionStringContractList.Any())
					ConnectionStringContractList = null;

				BindGrid();

				setStatusLabel();

				saveConnectionStringsToFile();
			}
		}

		private void dgvDataSource_SelectionChanged(object sender, EventArgs e)
		{
			bool rowNotSelected = dgvConnectionStrings.CurrentRow == null || ConnectionStringContractList == null;
			tsbEditConnectionString.Enabled = !rowNotSelected;
			tsbDeleteConnectionString.Enabled = !rowNotSelected;
		}

		private void dgvDataSource_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var hit = dgvConnectionStrings.HitTest(e.X, e.Y);

			if (hit.RowIndex >= 0)
			{
				DataGridViewRow clickedRow = dgvConnectionStrings.Rows[hit.RowIndex];
				var drv = clickedRow.DataBoundItem as ConnectionStringContract;
				if (drv == null)
					return;

				string id = drv.Id.ToString();
				ConnectionStringContract connectionStringContract = ConnectionStringContractList?.FirstOrDefault(c => c.Id == id);
				if (connectionStringContract == null)
					return;

				openDataSourceForEdit(connectionStringContract);
			}
		}
		#endregion

		#region Methods
		private void openDataSourceForEdit(ConnectionStringContract connectionStringContract)
		{
			frmConnectionStringDefinitionPopup frm = new frmConnectionStringDefinitionPopup(connectionStringContract, ConnectionStringContractList);
			if (frm.ShowDialog() == DialogResult.OK)
			{
				ConnectionStringContract editedConnectionStringContract = frm.GetConnectionStringContract();
				connectionStringContract.DataSource = editedConnectionStringContract.DataSource;
				connectionStringContract.Name = editedConnectionStringContract.Name;
				connectionStringContract.ConnectionString = editedConnectionStringContract.ConnectionString;
				connectionStringContract.IntegratedSecurity = editedConnectionStringContract.IntegratedSecurity;
				connectionStringContract.UserName = editedConnectionStringContract.UserName;
				connectionStringContract.Password = editedConnectionStringContract.Password;
				connectionStringContract.InitialCatalog = editedConnectionStringContract.InitialCatalog;

				BindGrid();
				setStatusLabel();

				saveConnectionStringsToFile();
			}
		}

		private void getConnectionStringsFromFile()
		{
			if (System.IO.File.Exists(Constants.ConnectionStringsFileName))
				ConnectionStringContractList = SerializeHelper.DeserializeFromXml<List<ConnectionStringContract>>(Constants.ConnectionStringsFileName);
		}

		private void saveConnectionStringsToFile()
		{
			SerializeHelper.SerializeToXml(ConnectionStringContractList, Constants.ConnectionStringsFileName);
		}

		private void setStatusLabel()
		{
			lblStatus.Text = ConnectionStringContractList == null ? null : string.Format(CommonResource._0ConnectionStringS, ConnectionStringContractList.Count);
		}

		private void setTextFromResource()
		{
			this.tsbAddConnectionString.Text = CommonResource.AddDataSource;
			this.tsMenu.Text = CommonResource.Navigation;
			this.tsbEditConnectionString.Text = CommonResource.EditDataSource;
			this.tsbDeleteConnectionString.Text = CommonResource.DeleteDataSource;
			this.NameColumn.HeaderText = CommonResource.Name;
			this.DataSourceColumn.HeaderText = CommonResource.DataSource;
			this.IntegratedSecurityColumn.HeaderText = CommonResource.IntegratedSecurity;
			this.UserNameColumn.HeaderText = CommonResource.UserName;
			this.InitialCatalogColumn.HeaderText = CommonResource.InitialCatalog;
		}

		private void BindGrid()
		{
			var list = ConnectionStringContractList ?? new List<ConnectionStringContract>();
			dgvConnectionStrings.BindList(list);
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			getConnectionStringsFromFile();

			dgvConnectionStrings.AutoGenerateColumns = false;
			dgvConnectionStrings.EnableHeadersVisualStyles = false;

			BindGrid();

			setStatusLabel();
		}

		public override BaseScreenDataContract GetFormData()
		{
			return new ConnectionStringsScreenDataContract()
			{
				Name = CommonResource.ConnectionString,
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
		}
		#endregion

	}
}
