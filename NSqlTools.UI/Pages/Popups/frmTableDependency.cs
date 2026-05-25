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
	public partial class frmTableDependency : BasePopup
	{
        #region Properties
        public DBObjectContract DBObjectContractObj { get; set; }
        #endregion

        #region Constructors
        public frmTableDependency(DBObjectContract dBObjectContract)
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
			this.gbTableIndexes.Text = CommonResource.TableDependencies;
			this.ColumnNamesColumn.HeaderText = CommonResource.ColumnNames;
			this._ucSqlNotePad.Title = CommonResource.ObjectContent;
			this.IndexNameColumn.HeaderText = CommonResource.Type;
			this.IsUniqueColumn.HeaderText = CommonResource.SchemaName;
			this.IsPrimaryKeyColumn.HeaderText = CommonResource.ObjectName;
			this.ObjectId.HeaderText = CommonResource.ObjectId;
			this.Text = CommonResource.TableDependencies;
		}
		#endregion

		#region Events
		private void frmTableDependency_Load(object sender, EventArgs e)
		{
			Text = String.Format(CommonResource.TableDependencies + " - {0}.{1}.{2}", DBObjectContractObj.DBName, DBObjectContractObj.SchemaName, DBObjectContractObj.Name);
			try
			{
				TableBusiness tableBusiness = new TableBusiness();
				
				List<TableDependencyContract> tableDependencyList = tableBusiness.GetTableDependencyList(DBObjectContractObj.ConnectionString, DBObjectContractObj.DBName, DBObjectContractObj.ObjectId);
				dgvTableDependency.AutoGenerateColumns = false;
				BindGrid(tableDependencyList ?? new List<TableDependencyContract>());
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
		}

		private void dgvTableDependency_SelectionChanged(object sender, EventArgs e)
		{
			TableDependencyContract tableDependencyContract = (TableDependencyContract)dgvTableDependency?.CurrentRow?.DataBoundItem;
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
			dgvTableDependency.BindList(list);
		}
		#endregion
	}
}
