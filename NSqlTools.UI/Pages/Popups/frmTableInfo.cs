using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Contracts;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSqlTools.UI.Popups
{
	public partial class frmTableInfo : BasePopup
	{
        #region Properties
        public DBObjectContract DBObjectContractObj { get; set; }
        #endregion

        #region Constructors
        public frmTableInfo(DBObjectContract dBObjectContract)
		{
			InitializeComponent();
			setTextFromResource();
			
			this.DBObjectContractObj = dBObjectContract;

			initForm();
		}

		private void initForm()
		{
		}

		private void setTextFromResource()
		{
			this.gbTableRelationships.Text = CommonResource.TableRelationships;
			this.gbTableIndexes.Text = CommonResource.TableIndexes;
			this.FKNameColumn.HeaderText = CommonResource.FKName;
			this.ReferencedTableColumn.HeaderText = CommonResource.ReferencedTable;
			this.ColumnNamesColumnRelationship.HeaderText = CommonResource.ColumnNames;
			this.RelationshipNameColumn.HeaderText = CommonResource.RelationshipName;
			this.IndexNameColumn.HeaderText = CommonResource.IndexName;
			this.ColumnNamesColumn.HeaderText = CommonResource.ColumnNames;
			this.IsUniqueColumn.HeaderText = CommonResource.IsUnique;
			this.IsPrimaryKeyColumn.HeaderText = CommonResource.IsPrimaryKey;
			this.IndexTypeNameColumn.HeaderText = CommonResource.IndexType;
			this.Text = CommonResource.TableInfo;
		}
		#endregion

		#region
		private void frmTableInfo_Load(object sender, EventArgs e)
		{
			Text = String.Format(CommonResource.TableInfo + " - {0}.{1}.{2}", DBObjectContractObj.DBName, DBObjectContractObj.SchemaName, DBObjectContractObj.Name);
			try
			{
				TableBusiness tableBusiness = new TableBusiness();
				
				List<TableIndexContract> tableIndexList = tableBusiness.GetTableIndexList(DBObjectContractObj.ConnectionString, DBObjectContractObj.DBName, DBObjectContractObj.ObjectId);
				dgvTableIndex.AutoGenerateColumns = false;
				dgvTableIndex.BindList(tableIndexList ?? new List<TableIndexContract>());

				List<TableRelationshipContract> tableRelationshipList = tableBusiness.GetTableRelationshipList(DBObjectContractObj.ConnectionString, DBObjectContractObj.DBName, DBObjectContractObj.ObjectId);
				dgvTableRelationship.AutoGenerateColumns = false;
				dgvTableRelationship.BindList(tableRelationshipList ?? new List<TableRelationshipContract>());
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
		}
		#endregion

		#region Override Methods
		#endregion
	}
}
