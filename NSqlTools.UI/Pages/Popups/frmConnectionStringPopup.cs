using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;

namespace NSqlTools.UI.Popups
{
	public partial class frmConnectionStringDefinitionPopup : BasePopup
	{
		#region Constructors
		public frmConnectionStringDefinitionPopup(List<ConnectionStringContract> connectionStringContractList, FormOpenModeEnum formOpenMode = FormOpenModeEnum.Add)
		{
			InitializeComponent();
			setTextFromResource();

			initForm(connectionStringContractList, formOpenMode);
		}

		public frmConnectionStringDefinitionPopup(ConnectionStringContract connectionStringContract, List<ConnectionStringContract> connectionStringContractList) : this(connectionStringContractList, FormOpenModeEnum.Edit)
		{
			ConnectionStringContract = connectionStringContract;
		}
		#endregion

		#region Properties
		private ConnectionStringContract connectionStringContract = new ConnectionStringContract();
		public ConnectionStringContract ConnectionStringContract
		{
			set
			{
				connectionStringContract = value;

				txtConnectionStringName.Text = value.Name;
				txtConnectionString.Text = value.ConnectionString;
				txtDataSource.Text = value.DataSource;
				txtUserName.Text = value.UserName;
				txtPassword.Text = value.Password;
				txtInitialCatalog.Text = value.InitialCatalog;
				cbIntegratedSecurity.Checked = value.IntegratedSecurity;

				lstDatabases.Items.Clear();
				if (value.DatabaseOrderList != null)
					foreach (String db in value.DatabaseOrderList)
						lstDatabases.Items.Add(db);
			}
		}

		private List<ConnectionStringContract> ConnectionStringContractList { get; set; }

		public FormOpenModeEnum FormOpenMode { get; set; }
		#endregion

		#region Events
		private void tsbConnectionTest_Click(object sender, EventArgs e)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(txtConnectionString.Text))
				{
					con.Open();
				}
				MessageBox.Show(String.Format(CommonResource.ConnectionSuccessfull), CommonResource.ConnectionTestResult, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				MessageBox.Show(String.Format(CommonResource.ConnectionUnsuccessfullErrorDetail, ex.Message), CommonResource.ConnectionTestResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void tsbClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void tsbOK_Click(object sender, EventArgs e)
		{
			if (!validateFields())
				return;

			DialogResult = DialogResult.OK;
		}

		private void TxtConnectionString_LostFocus(object sender, EventArgs e)
		{
			fillFieldsFromConnectionString();
		}

		private void cbIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
		{
			if (cbIntegratedSecurity.Checked)
			{
				txtUserName.Text = txtPassword.Text = null;
				txtUserName.Enabled = txtPassword.Enabled = false;
				txtUserName.BackColor = txtPassword.BackColor = System.Drawing.SystemColors.ScrollBar;
			}
			else
			{
				txtUserName.Enabled = txtPassword.Enabled = true;
				txtUserName.BackColor = txtPassword.BackColor = Constants.ComponentRequiredColor;
			}

			createConnectionStringFromFields(cbIntegratedSecurity, EventArgs.Empty);
		}

		private void btnAddDb_Click(object sender, EventArgs e)
		{
			String db = String.IsNullOrWhiteSpace(txtAddDatabase.Text) ? null : txtAddDatabase.Text.Trim();
			if (String.IsNullOrEmpty(db))
				return;

			// Duplicate kontrol
			foreach (String item in lstDatabases.Items)
			{
				if (String.Equals(item, db, StringComparison.InvariantCultureIgnoreCase))
				{
					MessageBox.Show(CommonResource.DatabaseAlreadyAdded, CommonResource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}

			lstDatabases.Items.Add(db);
			txtAddDatabase.Text = null;
		}

		private void btnDbUp_Click(object sender, EventArgs e)
		{
			if (lstDatabases.SelectedIndex <= 0)
				return;

			int index = lstDatabases.SelectedIndex;
			Object current = lstDatabases.Items[index];
			lstDatabases.Items.RemoveAt(index);
			lstDatabases.Items.Insert(index - 1, current);
			lstDatabases.SelectedIndex = index - 1;
		}

		private void btnDbDown_Click(object sender, EventArgs e)
		{
			if (lstDatabases.SelectedIndex < 0 || lstDatabases.SelectedIndex >= lstDatabases.Items.Count - 1)
				return;

			int index = lstDatabases.SelectedIndex;
			Object current = lstDatabases.Items[index];
			lstDatabases.Items.RemoveAt(index);
			lstDatabases.Items.Insert(index + 1, current);
			lstDatabases.SelectedIndex = index + 1;
		}

		private void btnDbRemove_Click(object sender, EventArgs e)
		{
			if (lstDatabases.SelectedIndex < 0)
				return;

			lstDatabases.Items.RemoveAt(lstDatabases.SelectedIndex);
		}
		#endregion

		#region Private Methods
		private void initForm(List<ConnectionStringContract> connectionStringContractList, FormOpenModeEnum formOpenMode)
		{
			txtDataSource.LostFocus += createConnectionStringFromFields;
			txtUserName.LostFocus += createConnectionStringFromFields;
			txtPassword.LostFocus += createConnectionStringFromFields;
			txtInitialCatalog.LostFocus += createConnectionStringFromFields;
			txtConnectionString.LostFocus += TxtConnectionString_LostFocus;

			ConnectionStringContractList = connectionStringContractList;
			FormOpenMode = formOpenMode;
		}

		private void setTextFromResource()
		{
			this.lblConnectionString.Text = CommonResource.ConnectionString;
			this.lblUserName.Text = CommonResource.UserName;
			this.lblPassword.Text = CommonResource.Password;
			this.tsMenu.Text = CommonResource.Navigation;
			this.tsbOK.Text = CommonResource.Ok;
			this.tsbClose.Text = CommonResource.Close;
			this.tsbConnectionTest.Text = CommonResource.TestTheConnectionString;
			this.lblInitialCatalog.Text = CommonResource.DefaultDatabase;
			this.lblDataSource.Text = CommonResource.DataSource;
			this.lblConnectionStringName.Text = CommonResource.Name;
			this.cbIntegratedSecurity.Text = CommonResource.IntegratedSecurity;
			this.lblDatabaseOrder.Text = CommonResource.DatabaseOrder;
			this.Text = CommonResource.ConnectionStringDefinition;
		}

		private bool validateFields()
		{
			bool isValid = true;

			if (!UIHelper.ComponentIsValidString(errorProvider, txtConnectionStringName.Text, txtConnectionStringName, CommonResource.ConnectionStringNameFieldIsRequired))
				isValid = false;

			if (!UIHelper.ComponentIsValidString(errorProvider, txtConnectionString.Text, txtConnectionString, CommonResource.ConnectionStringFieldIsRequired))
				isValid = false;

			if (!UIHelper.ComponentIsValidString(errorProvider, txtDataSource.Text, txtDataSource, CommonResource.ServerNameFieldIsRequired))
				isValid = false;

			if (!cbIntegratedSecurity.Checked)
			{
				if (!UIHelper.ComponentIsValidString(errorProvider, txtUserName.Text, txtUserName, CommonResource.UserNameFieldIsRequired))
					isValid = false;

				if (!UIHelper.ComponentIsValidString(errorProvider, txtPassword.Text, txtPassword, CommonResource.PasswordFieldIsRequired))
				 isValid = false;
			}

			if (isValid && ConnectionStringContractList != null)
			{
				if (ConnectionStringContractList.Any(c => c.Name == txtConnectionStringName.Text && c.Id != connectionStringContract.Id))
				{
					errorProvider.SetError(txtConnectionStringName, CommonResource.ThisNameHasAlreadyBeednUsed);
					isValid = false;
				}
			}

			return isValid;
		}

		private void createConnectionStringFromFields(object sender, EventArgs e)
		{
			SqlConnectionStringBuilder scsb;
			if (cbIntegratedSecurity.Checked)
			{
				scsb = new SqlConnectionStringBuilder()
				{
					DataSource = txtDataSource.Text,
					IntegratedSecurity = true,
					Authentication = SqlAuthenticationMethod.NotSpecified,
					InitialCatalog = txtInitialCatalog.Text
				};
			}
			else
			{
				scsb = new SqlConnectionStringBuilder()
				{
					DataSource = txtDataSource.Text,
					IntegratedSecurity = false,
					Authentication = SqlAuthenticationMethod.NotSpecified,
					InitialCatalog = txtInitialCatalog.Text,
					UserID = txtUserName.Text,
					Password = txtPassword.Text
				};
			}
			txtConnectionString.Text = scsb.ConnectionString;
		}

		private void fillFieldsFromConnectionString()
		{
			try
			{
				SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(txtConnectionString.Text);
				txtDataSource.Text = scsb.DataSource;
				txtUserName.Text = scsb.UserID;
				txtPassword.Text = scsb.Password;
				txtInitialCatalog.Text = scsb.InitialCatalog;
				cbIntegratedSecurity.Checked = scsb.IntegratedSecurity;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				MessageBox.Show(String.Format(CommonResource.ErrorDetail, CommonResource.SqlConnectionStringBuilderError, ex.Message), CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtConnectionString.Text = null;
			}
		}
		#endregion

		#region Public Methods
		public ConnectionStringContract GetConnectionStringContract()
		{
			connectionStringContract.IntegratedSecurity = cbIntegratedSecurity.Checked;
			connectionStringContract.Name = txtConnectionStringName.Text;
			connectionStringContract.DataSource = txtDataSource.Text;
			connectionStringContract.ConnectionString = txtConnectionString.Text;
			connectionStringContract.InitialCatalog = txtInitialCatalog.Text;
			connectionStringContract.UserName = txtUserName.Text;
			connectionStringContract.Password = txtPassword.Text;
			connectionStringContract.DatabaseOrderList = lstDatabases.Items.Cast<String>().ToList();

			return connectionStringContract;
		}
		#endregion

		#region Override Methods
		#endregion
	}
}
