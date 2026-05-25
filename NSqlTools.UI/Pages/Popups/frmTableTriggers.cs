using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSqlTools.UI.Popups
{
	public partial class frmTableTriggers : BasePopup
	{
        #region Properties
        public DBObjectContract DBObjectContractObj { get; set; }
        #endregion

        #region Constructors
        public frmTableTriggers(DBObjectContract dBObjectContract)
		{
			InitializeComponent();
			setTextFromResource();
			
			this.DBObjectContractObj = dBObjectContract;

			initForm();
		}

		private void initForm()
		{
			_ucSqlNotePad.SetCompareType(ScintillaNET.Lexer.Sql);
			_ucSqlNotePad.InitialiseScintilla();
		}

		private void setTextFromResource()
		{
			this.gbTableTriggers.Text = NSqlTools.Types.Properties.CommonResource.TableTriggers;
			this.Text = CommonResource.TableTriggers;
			this._ucSqlNotePad.Title = CommonResource.ObjectContent;
			this.coldObjectName.HeaderText = NSqlTools.Types.Properties.CommonResource.TriggerName;
		}
		#endregion

		#region Events
		private void frmTableTriggers_Load(object sender, EventArgs e)
		{
			Text = String.Format(CommonResource.TableTriggers + " - {0}.{1}.{2}", DBObjectContractObj.DBName, DBObjectContractObj.SchemaName, DBObjectContractObj.Name);
			try
			{
				TableBusiness tableBusiness = new TableBusiness();
				
				List<TableDependencyContract> tableDependencyList = tableBusiness.GetTableTriggerList(DBObjectContractObj.ConnectionString, DBObjectContractObj.DBName, DBObjectContractObj.ObjectId);
				dgvTableTriggers.AutoGenerateColumns = false;
				BindGrid(tableDependencyList ?? new List<TableDependencyContract>());
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
		}

		private void dgvTableTriggers_SelectionChanged(object sender, EventArgs e)
		{
			TableDependencyContract tableDependencyContract = (TableDependencyContract)dgvTableTriggers?.CurrentRow?.DataBoundItem;
			DBObjectContract dbObjectContract = null;
			if (tableDependencyContract != null)
			{
				DBObjectBusiness dBObjectBusiness = new DBObjectBusiness();
				dbObjectContract = dBObjectBusiness.GetDBObjectByDBObject(DBObjectContractObj.ConnectionString, DBObjectContractObj.DBName, new DBObjectContract() { ObjectId = tableDependencyContract.ObjectId });
			}

			_ucSqlNotePad.SetDBObject(dbObjectContract);
		}
		#endregion

		#region Methods
		private void BindGrid(List<TableDependencyContract> tableDependencyList)
		{
			var list = tableDependencyList ?? new List<TableDependencyContract>();
			dgvTableTriggers.BindList(list);
		}
		#endregion
	}
}
