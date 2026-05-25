using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	public partial class ucSqlViewer : BaseUserControl
	{
		#region Constructor
		public ucSqlViewer()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Properties
		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					ucDBObjectSelect
				};
			}
		}
		#endregion

		#region Methods
		private void setTextFromResource()
		{
			this.ucDBObjectSelect.Caption = CommonResource.DBObject;
			this.tsbCriteriaCollapse.Text = CommonResource.CollapseCriteriaPanel;
		}
		#endregion

		#region Events
		private void ucDBObjectSelect_OnDBObjectChanged(object sender, DBObjectChangedEventArgs e)
		{
			switch (ucDBObjectSelect.SelectedObjectType.Type)
			{
				case Enums.ObjectTypeEnum.U:
					ucTableViewControl.SetDBObject(e.DBObjectContract);

					break;
				default:
					ucSqlNotePadControl.SetDBObject(e.DBObjectContract);

					break;
			}

			ParentTabPage.Text = $"{ucDBObjectSelect.SelectedSchema.Name}.{ucDBObjectSelect.SelectedDBObject.Name}";
		}

		private void ucDBObjectSelect_OnDBObjectClear(object sender, EventArgs e)
		{
			if (ucDBObjectSelect.SelectedObjectType == null)
			{
				ucTableViewControl.Visible = false;
				ucSqlNotePadControl.Visible = false;

				return;
			}

			switch (ucDBObjectSelect.SelectedObjectType.Type)
			{
				case Enums.ObjectTypeEnum.U:
					ucTableViewControl.SetDBObject(null);

					break;
				default:
					ucSqlNotePadControl.SetDBObject(null);

					break;
			}
		}

		private void ucDBObjectSelect_OnObjectTypeChanged(object sender, EventArgs e)
		{
			if(ucDBObjectSelect.SelectedObjectType?.Type == null)
			{
				ucSqlNotePadControl.Visible = false;
				ucTableViewControl.Visible = false;

				return;
			}

			switch (ucDBObjectSelect.SelectedObjectType.Type)
			{
				case Enums.ObjectTypeEnum.U:
					if (ucTableViewControl.Visible)
						break;

					ucSqlNotePadControl.Visible = false;
					ucSqlNotePadControl.Dock = DockStyle.None;

					ucTableViewControl.Visible = true;
					ucTableViewControl.Dock = DockStyle.Fill;

					break;
				default:
					if (ucSqlNotePadControl.Visible)
						break;

					ucTableViewControl.Visible = false;
					ucTableViewControl.Dock = DockStyle.None;

					ucSqlNotePadControl.Visible = true;
					ucSqlNotePadControl.Dock = DockStyle.Fill;

					break;
			}
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			UIHelper.SafeSetSplitterDistance(scSqlViewer, Constants.DefaultSplitterDistance);
		}

		private void tsbCriteriaCollapse_Click(object sender, EventArgs e)
		{
			scSqlViewer.Panel1Collapsed = !scSqlViewer.Panel1Collapsed;
			tsbCriteriaCollapse.Image
				= scSqlViewer.Panel1Collapsed
				? Properties.Resources.NotCollapse
				: Properties.Resources.Collapse;
			tsbCriteriaCollapse.Text
				= scSqlViewer.Panel1Collapsed
				? CommonResource.ExpandCriteriaPanel
				: CommonResource.CollapseCriteriaPanel;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			ucSqlNotePadControl.MainForm
				= ucTableViewControl.MainForm
				= MainForm;

			ucDBObjectSelect.InitForm();
			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);
		}

		public override BaseScreenDataContract GetFormData()
		{
			DBObjectSelectScreenDataContract dbObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelect.GetFormData();
			SqlViewerScreenDataContract sqlViewerFormDataContract = new SqlViewerScreenDataContract()
			{
				Name = CommonResource.SqlViewer,
				Description = ParentTabPage.Text,

				DataSourceName = dbObjectSelectFormDataContract.DataSourceName,
				DBIndexes = dbObjectSelectFormDataContract.DBIndexes,
				ObjectType = dbObjectSelectFormDataContract.ObjectType,
				SchemaId = dbObjectSelectFormDataContract.SchemaId,
				ObjectId = dbObjectSelectFormDataContract.ObjectId
			};

			return sqlViewerFormDataContract;
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as SqlViewerScreenDataContract;
			if (data == null)
				return;

			ucDBObjectSelect.SetFormData(data);
		}
		#endregion
	}
}
