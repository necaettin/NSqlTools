using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;

namespace NSqlTools.UI.UserControls
{
	public partial class ucDBObjectSelect : BaseUserControl, ICustomTabSequenceProvider
	{
		#region Constructor
		public ucDBObjectSelect()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Event Handlers
		public event EventHandler<DBObjectChangedEventArgs> OnDBObjectChanged;

		public event EventHandler OnDBObjectClear;

		public event EventHandler OnDBChanged;

		public event EventHandler OnDBClear;

		public event EventHandler OnSchemaChanged;

		public event EventHandler OnSchemaClear;

		public event EventHandler OnObjectTypeChanged;
		#endregion

		#region Properties
		public ConnectionStringContract SelectedConnectionString
		{
			get
			{
				return cbConnectionStrings.SelectedItem as ConnectionStringContract;
			}

		}

		public DBContract SelectedDB
		{
			get
			{
				return SelectedDBList != null && SelectedDBList.Count > 0 ? SelectedDBList.First() : null;
			}
		}

		public List<DBContract> SelectedDBList
		{
			get
			{
				List<DBContract> selectedDBList = new List<DBContract>();
				foreach (var checkedItem in clbDB.CheckedItems)
				{
					selectedDBList.Add((DBContract)checkedItem);
				}
				
				return selectedDBList;
			}
		}

		public SchemaContract SelectedSchema
		{
			get
			{
				return cbSchema.SelectedItem as SchemaContract;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ObjectTypeContract SelectedObjectType
		{
			get
			{
				return _ucObjectType.SelectedObjectType;
			}
			set
			{
				_ucObjectType.SelectedObjectType = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DBObjectContract SelectedDBObject
		{
			get
			{
				return lbDBObject.SelectedItem as DBObjectContract;
			}
		}

		public String Caption
		{
			get { return gbDBObject.Text; }
			set
			{
				gbDBObject.Text = value;
			}
		}


		private Boolean objectTypeVisibility = true;
		public Boolean ObjectTypeVisibility
		{
			get
			{
				return objectTypeVisibility;
			}
			set
			{
				objectTypeVisibility
					= pnlObjectType.Visible
					= value;
			}
		}


		private Boolean dbObjectVisibility = true;
		public Boolean DBObjectVisibility
		{
			get
			{
				return dbObjectVisibility;
			}
			set
			{
				dbObjectVisibility
					= pnlDBObject.Visible
					= value;
			}
		}

		private Boolean schemaVisibility = true;
		public Boolean SchemaVisibility
		{
			get
			{
				return schemaVisibility;
			}
			set
			{
				schemaVisibility
					= pnlSchema.Visible
					= value;
			}
		}

		public String TitleVisibility
		{
			get;
			set;
		}


		private List<DBObjectContract> dbObjectContractList;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public List<DBObjectContract> DBObjectContractList
		{
			get
			{
				return dbObjectContractList;
			}
			set
			{
				lbDBObject.DataSource
					= dbObjectContractList
					= value;

				if (value == null)
					txtDBObjectFilter.Text = null;
			}
		}

		private List<DBContract> dbContractList;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public List<DBContract> DBContractList
		{
			get
			{
				return dbContractList;
			}
			set
			{
				dbContractList = value;

				clbDB.DataSource = null;
				clbDB.DataSource
					= new BindingSource() { DataSource = value };

				if (value == null)
					txtDBFilter.Text = null;
			}
		}

		private List<ObjectTypeContract> objectTypes;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public List<ObjectTypeContract> ObjectTypes
		{
			get
			{
				return _ucObjectType.ObjectTypes;
			}
			set
			{
				_ucObjectType.ObjectTypes = value;
			}
		}


		public Boolean isRequiredConnectionString;
		public Boolean IsRequiredConnectionString
		{
			get { return isRequiredConnectionString; }
			set
			{
				isRequiredConnectionString = value;

				if (value)
					cbConnectionStrings.BackColor = Constants.ComponentRequiredColor;
			}
		}


		public Boolean isRequiredObjectType;
		public Boolean IsRequiredObjectType
		{
			get { return isRequiredObjectType; }
			set
			{
				isRequiredObjectType = value;

				if (value)
					_ucObjectType.BackColor = Constants.ComponentRequiredColor;
			}
		}


		public Boolean isRequiredDB;
		public Boolean IsRequiredDB
		{
			get { return isRequiredDB; }
			set
			{
				isRequiredDB = value;

				if (value)
					clbDB.BackColor = Constants.ComponentRequiredColor;
			}
		}


		public Boolean isRequiredSchema;
		public Boolean IsRequiredSchema
		{
			get { return isRequiredSchema; }
			set
			{
				isRequiredSchema = value;

				if (value)
					cbSchema.BackColor = Constants.ComponentRequiredColor;
			}
		}


		public Boolean isRequiredDBObject;
		public Boolean IsRequiredDBObject
		{
			get { return isRequiredDBObject; }
			set
			{
				isRequiredDBObject = value;

				if (value)
					lbDBObject.BackColor = Constants.ComponentRequiredColor;
			}
		}

		public List<String> SchemaNameList;
		public List<String> DBObjectNameList;

        public Boolean AllowOnlyOneDBSelection { get; set; } = true;
		#endregion

		#region Selected Value Properties
		public String SelectedConnectionNameValue
		{
			get
			{
				return cbConnectionStrings.SelectedValue?.ToString();
			}
			set
			{
				cbConnectionStrings.SelectedValue = value;
			}
		}

		public List<Int32> SelectedDBIndexes
		{
			get
			{
				if (clbDB.CheckedItems.Count > 0)
					return clbDB.CheckedIndices.Cast<int>().ToList();
				else
					return null;
			}
			set
			{
				if (value != null)
				{
					clbDB.ItemCheck -= clbDB_ItemCheck;
					for (int i = 0; i < clbDB.Items.Count; i++)
					{
						clbDB.SetItemChecked(i, value.Contains(i));
					}
					clbDB.ItemCheck += clbDB_ItemCheck;

					dbChanged();
				}
			}
		}

		public Int32? SelectedObjectType2
		{
			get
			{
				return (Int32?)SelectedObjectType?.Type;
			}
			set
			{
				SelectedObjectType = value == null ? null : ObjectTypes.First(x => x.Type == (ObjectTypeEnum?)value);
			}
		}

		public Int32? SelectedSchemaId
		{
			get
			{
				return (cbSchema.SelectedItem as SchemaContract)?.SchemaId;
			}
			set
			{
				cbSchema.SelectedValue = value;
			}
		}

		public Int32? SelectedDBObjectObjectId
		{
			get
			{
				return SelectedDBObject?.ObjectId;
			}
			set
			{
				lbDBObject.SelectedValue = value;
			}
		}
		#endregion

		#region TabIndex Properties
		public Int32 TabIndexConnectionString
		{
			get 
			{
				return cbConnectionStrings.TabIndex;
			}
			set 
			{
				cbConnectionStrings.TabIndex = value;
			}
		}

		public Int32 TabIndexDB
		{
			get
			{
				return clbDB.TabIndex;
			}
			set
			{
				clbDB.TabIndex = value;
			}
		}

		public Int32 TabIndexObjectType
		{
			get
			{
				return _ucObjectType.TabIndex;
			}
			set
			{
				_ucObjectType.TabIndex = value;
			}
		}

		public Int32 TabIndexSchema
		{
			get
			{
				return cbSchema.TabIndex;
			}
			set
			{
				cbSchema.TabIndex = value;
			}
		}

		public Int32 TabIndexDBObjectFilter
		{
			get
			{
				return txtDBObjectFilter.TabIndex;
			}
			set
			{
				txtDBObjectFilter.TabIndex = value;
			}
		}

		public Int32 TabIndexDBObject
		{
			get
			{
				return lbDBObject.TabIndex;
			}
			set
			{
				lbDBObject.TabIndex = value;
			}
		}
		#endregion

		#region Methods
		#region Form Methods
		private void setTextFromResource()
		{
			this.lblDBObject.Text = CommonResource.DBObject;
			this.lblSchema.Text = CommonResource.Schema;
			this.lblObjectType.Text = CommonResource.ObjectType;
			this.lblDB.Text = CommonResource.DB;
			this.lblConnectionString.Text = CommonResource.ConnectionString;
			this.lblDBObject.Text = CommonResource.DBObject;
		}
		#endregion

		#region Connection String Methods
		private void connectionStringChanged()
		{
			fillDbList();
		}

		private void fillconnectionStrings()
		{
			try
			{
				callDBClearEventHandler();
				clearDBList();

				ConnectionStringBusiness dataSourceBusiness = new ConnectionStringBusiness();
				List<ConnectionStringContract> list = dataSourceBusiness.GetConnectionString();

				cbConnectionStrings.SuspendLayout();
				cbConnectionStrings.SelectedIndexChanged -= cbConnectionString_SelectedIndexChanged;
				cbConnectionStrings.DataSource = list;
				cbConnectionStrings.SelectedItem = null;
				cbConnectionStrings.SelectedIndexChanged += cbConnectionString_SelectedIndexChanged;
				cbConnectionStrings.ResumeLayout();
			}
			catch(Exception ex)
			{
				UIHelper.ShowException(ex);
			}
		}
		#endregion

		#region DB Methods
		private void fillDbList()
		{
			DBBusiness dbBusiness = new DBBusiness();
			try
			{
				clbDB.SuspendLayout();

				if (SelectedConnectionString == null)
				{
					clearDBList();

					return;
				}

				ClearSchemaList();

				String defaultDBName = null;
				List<DBContract> _dbContractList = dbBusiness.GetDBList(SelectedConnectionString, ref defaultDBName);
				clbDB.ItemCheck -= clbDB_ItemCheck;
				DBContractList = _dbContractList;
				clbDB.DisplayMember = "Name";
				clbDB.ValueMember = "DatabaseId";
				clbDB.ItemCheck += clbDB_ItemCheck;

				if (!String.IsNullOrWhiteSpace(SelectedConnectionString.InitialCatalog))
				{
					Int32 i = dbContractList.FindIndex(l => l.Name.ToUpper() == SelectedConnectionString.InitialCatalog.ToUpper());

					if(i >= 0)
						clbDB.SetItemChecked(i, true);
				}
				else if (!String.IsNullOrWhiteSpace(defaultDBName))
				{
					Int32 i = dbContractList.FindIndex(l => l.Name.ToUpper() == defaultDBName.ToUpper());
					if(i >= 0)
						clbDB.SetItemChecked(i, true);
				}
				else
				{
					for (int i = 0; i < clbDB.Items.Count; i++)
					{
						clbDB.SetItemChecked(i, false);
					}
				}

				fillSchemaList();
				callDBChangedEventHandler();
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
			finally
			{
				clbDB.ResumeLayout();
			}
		}

		private void dbChanged()
		{
			fillSchemaList();

			callDBChangedEventHandler();
		}

		private void clearDBList()
		{
			clbDB.DataSource = null;

			ClearSchemaList();
		}
		#endregion

		#region Object Type Methods
		private void callObjectTypeChangedEventHandler()
		{
			OnObjectTypeChanged?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region DB Object Methods
		private void dbObjectChanged()
		{
			if (SelectedDBObject == null)
			{
				callDBObjectClearEventHandler();

				return;
			}

			DBObjectBusiness dbObjectBusiness = new DBObjectBusiness();
			ColumnBusiness columnBusiness = new ColumnBusiness();
			try
			{
				DBObjectContract dbObjectContract = SelectedDBObject;
				switch (SelectedDBObject.ObjectType)
				{
					case ObjectTypeEnum.U:
						dbObjectContract = columnBusiness.GetColumnListByTableId(SelectedConnectionString.ConnectionString, SelectedDB.Name, SelectedDBObject);
			
						break;
					default:
					 dbObjectContract = dbObjectBusiness.GetDBObjectByDBObject(SelectedConnectionString.ConnectionString, SelectedDB.Name, SelectedDBObject);

						break;
				}

				callDBObjectChangedEventHandler(dbObjectContract);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
		}

		private void callDBObjectChangedEventHandler(DBObjectContract dbObjectContract)
		{
			OnDBObjectChanged?.Invoke(this, new DBObjectChangedEventArgs() { DBObjectContract = dbObjectContract });
		}

		private void callDBObjectClearEventHandler()
		{
			OnDBObjectClear?.Invoke(this, EventArgs.Empty);
		}

		private void callSchemaChangedEventHandler()
		{
			OnSchemaChanged?.Invoke(this, EventArgs.Empty);
		}

		private void callSchemaClearEventHandler()
		{
			OnSchemaClear?.Invoke(this, EventArgs.Empty);
		}

		private async void callDBChangedEventHandler()
		{
			var cache = await UIHelper.EnsureIntellisenseDbCacheAsync(
				SelectedConnectionString,
				SelectedDB,
				UIHelper.DbCacheInfo);

			UIHelper.DbCacheInfo = cache;

			System.Diagnostics.Debug.WriteLine(
				$"[DBUIHelperChanged] DbCacheInfo tables={UIHelper.DbCacheInfo?.FirstOrDefault()?.TableList?.Count ?? 0}");

			OnDBChanged?.Invoke(this, EventArgs.Empty);
		}

		private void callDBClearEventHandler()
		{
			OnDBClear?.Invoke(this, EventArgs.Empty);
		}

		private void fillDBObjectList()
		{
			if (SelectedSchema == null)
				return;

			DBObjectBusiness dbObjectBusiness = new DBObjectBusiness();
			try
			{
				lbDBObject.SuspendLayout();

				callDBObjectClearEventHandler();

				String objectType = SelectedObjectType?.Type.ToString();
                List<DBObjectContract> list = dbObjectBusiness.GetDBObjectListByDBSchemaAndObjectType(SelectedConnectionString.ConnectionString, SelectedDB.Name, SelectedSchema.SchemaId, objectType, objectType == "IF" ? "TF" : null);

				lbDBObject.SelectedIndexChanged -= lbDBObject_SelectedIndexChanged;
				DBObjectContractList = list;
				lbDBObject.DisplayMember = "Name";
				lbDBObject.ValueMember = "ObjectId";
				lbDBObject.SelectedIndexChanged += lbDBObject_SelectedIndexChanged;
				lbDBObject.SelectedItem = null;

				txtDBObjectFilter.Text = null;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
			finally
			{
				lbDBObject.ResumeLayout();
			}
		}

		private void clearDBObjectList()
		{
			DBObjectContractList = null;

			callDBObjectClearEventHandler();
		}
		#endregion

		#region Schema Methods
		private void schemaChanged()
		{
			fillDBObjectList();

			callSchemaChangedEventHandler();
		}

		public void ClearSchemaList()
		{
			cbSchema.DataSource = null;
			callSchemaClearEventHandler();
			clearDBObjectList();
		}

		private void fillSchemaList()
		{
			SchemaBusiness schemaBusiness = new SchemaBusiness();
			try
			{
				cbSchema.SuspendLayout();

				if (SelectedDB == null || SelectedDBList.Count == 0 || SelectedDBList.Count > 1)
				{
					ClearSchemaList();
					
					return;
				}

				callSchemaClearEventHandler();
				clearDBObjectList();

				using (SqlConnection con = new SqlConnection(SelectedConnectionString.ConnectionString))
				{
					List<SchemaContract> list = schemaBusiness.GetSchemaListByDBAndObjectType(SelectedConnectionString.ConnectionString, SelectedConnectionString.Name, SelectedDB.Name, SelectedObjectType?.Type.ToString());
					cbSchema.SelectedIndexChanged -= cbSchema_SelectedIndexChanged;
					cbSchema.DataSource = list;
					cbSchema.DisplayMember = "Name";
					cbSchema.ValueMember = "SchemaId";
					cbSchema.SelectedIndexChanged += cbSchema_SelectedIndexChanged;
					cbSchema.SelectedItem = null;

					SchemaNameList = list.Select(l => l.Name).ToList();

					cbSchema.AutoCompleteCustomSource = new AutoCompleteStringCollection();
					cbSchema.AutoCompleteCustomSource.AddRange(SchemaNameList.ToArray());
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				UIHelper.ShowException(ex);
			}
			finally 
			{ 
				cbSchema.ResumeLayout();
			}
		}
		#endregion
		#endregion

		#region Override Methods	
		public override void InitForm()
		{
			fillconnectionStrings();
		}

		public override BaseScreenDataContract GetFormData()
		{
			DBObjectSelectScreenDataContract dbObjectSelectDataContract = new DBObjectSelectScreenDataContract
			{
				DataSourceName = SelectedConnectionString?.Name,
				DBIndexes = clbDB.CheckedItems.Count > 0 ? clbDB.CheckedIndices.Cast<int>().ToList() : null,
				ObjectType = (Int32?)SelectedObjectType?.Type,
				SchemaId = SelectedSchema?.SchemaId,
				ObjectId = SelectedDBObject?.ObjectId
			};

			return dbObjectSelectDataContract;
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			DBObjectSelectScreenDataContract data = formDataBaseContract as DBObjectSelectScreenDataContract;
			if (data == null)
				return;

			// Connection String
			if(data.DataSourceName != null)
				cbConnectionStrings.SelectedValue = data.DataSourceName;

			// DB Indexes
			if (data.DBIndexes != null)
			{
				clbDB.ItemCheck -= clbDB_ItemCheck;
				for (int i = 0; i < clbDB.Items.Count; i++)
				{
					clbDB.SetItemChecked(i, data.DBIndexes.Contains(i));
				}
				clbDB.ItemCheck += clbDB_ItemCheck;
				dbChanged();
			}

			// Object Type
			if(data.ObjectType != null)
				SelectedObjectType = ObjectTypes.FirstOrDefault(o => o.Type == (ObjectTypeEnum?)data.ObjectType);

			// Schema
			if(data?.SchemaId != null)
				cbSchema.SelectedValue = data?.SchemaId;

			// DB Object
			if(data?.ObjectId != null)
				lbDBObject.SelectedValue = data?.ObjectId;
		}
		#endregion

		#region Events
		private void cbConnectionString_SelectedIndexChanged(object sender, EventArgs e)
		{
			connectionStringChanged();
		}

		private void lbDBObject_SelectedIndexChanged(object sender, EventArgs e)
		{
			dbObjectChanged();
		}

		private void cbSchema_SelectedIndexChanged(object sender, EventArgs e)
		{
			schemaChanged();
		}

		private void txtDBObjectFilter_TextChanged(object sender, EventArgs e)
		{
			lbDBObject.DataSource =
    string.IsNullOrWhiteSpace(txtDBObjectFilter.Text)
        ? DBObjectContractList
        : DBObjectContractList
            .Where(d => d.Name != null &&
                        d.Name.IndexOf(txtDBObjectFilter.Text,
                            StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();
		}

		private void txtDBFilter_TextChanged(object sender, EventArgs e)
		{
			// 1) Şu an seçili DB'leri key ile topla (örneğin Name veya DatabaseId)
			var selectedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (DBContractList != null)
			{
				foreach (DBContract db in clbDB.CheckedItems)
				{
					// İstersen Name yerine DatabaseId'yi kullan
					selectedKeys.Add(db.Name);
				}
			}

			// 2) Filtreli listeyi üret
			List<DBContract> source;
			if (String.IsNullOrWhiteSpace(txtDBFilter.Text))
				source = DBContractList;
			else
				source = DBContractList
					.Where(d => d.Name != null &&
								d.Name.IndexOf(txtDBFilter.Text,
									StringComparison.OrdinalIgnoreCase) >= 0)
					.ToList();

			// 3) DataSource'u güncelle, sonra eski seçimleri geri uygala
			clbDB.ItemCheck -= clbDB_ItemCheck;
			clbDB.DataSource = null;
			clbDB.DataSource = source;
			clbDB.DisplayMember = "Name";
			clbDB.ValueMember = "DatabaseId";

			// Eski seçili olanlardan, filtreli listede kalanları yeniden işaretle
			for (int i = 0; i < clbDB.Items.Count; i++)
			{
				var db = (DBContract)clbDB.Items[i];
				if (selectedKeys.Contains(db.Name))
					clbDB.SetItemChecked(i, true);
			}
			clbDB.ItemCheck += clbDB_ItemCheck;
		}

		private void cbConnectionStrings_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				cbConnectionStrings.SelectedItem = null;
				connectionStringChanged();
			}
		}

		private void cbSchema_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				cbSchema.SelectedItem = null;
				schemaChanged();
			}
		}

		private void clbDB_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				clbDB.SelectedItem = null;
				dbChanged();
			}
		}

		private void lbDBObject_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				lbDBObject.SelectedItem = null;
				dbObjectChanged();
			}
		}

		private void clbDB_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (AllowOnlyOneDBSelection 
				&& e.NewValue == CheckState.Checked 
				&& clbDB.CheckedItems.Count > 0)
			{
				clbDB.ItemCheck -= clbDB_ItemCheck;
				clbDB.SetItemChecked(clbDB.CheckedIndices[0], false);
				clbDB.ItemCheck += clbDB_ItemCheck;
			}

			clbDB.ItemCheck -= clbDB_ItemCheck;
			clbDB.SetItemChecked(e.Index, e.NewValue == CheckState.Checked);
			clbDB.ItemCheck += clbDB_ItemCheck;

			dbChanged();
		}

		private void _ucObjectType_OnObjectTypeChanged(object sender, EventArgs e)
		{
			fillSchemaList();

			callObjectTypeChangedEventHandler();
		}
		#endregion

		#region Interface Methods	
		public IList<Control> GetCustomTabSequence()
		{
			return new List<Control>
			{
				cbConnectionStrings,
				txtDBFilter,
				clbDB,
				_ucObjectType,
				cbSchema,
				txtDBObjectFilter,
				lbDBObject
			};
		}
		#endregion
	}
}
