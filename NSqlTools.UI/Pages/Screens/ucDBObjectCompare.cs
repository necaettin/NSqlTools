using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;

namespace NSqlTools.UI.Pages
{
	public partial class ucDBObjectCompare : BaseUserControl
	{
		#region Properties
		public override List<Object> TabProviders
		{
			get
			{
				return new List<Object>
				{
					_ucObjectType,
					ucDBObjectSelectSource,
					ucDBObjectSelectTarget
				};
			}
		}
		#endregion

		#region Constructors
		public ucDBObjectCompare()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Events
		private void ucDBObjectSelectSource_OnDBObjectChanged(object sender, DBObjectChangedEventArgs e)
		{
			if (ucDBObjectSelectSource.SelectedObjectType == null)
				return;

			ObjectTypeEnum type = ucDBObjectSelectSource.SelectedObjectType.Type;
			switch (type)
			{
				case ObjectTypeEnum.U:
					ucTableViewCompareControl.FillGrid(ucDBObjectSelectSource.SelectedDBObject, ucDBObjectSelectTarget.SelectedDBObject);

					break;
				default:
					ucNotePadCompareControl.PrepareLeftNotePad(e.DBObjectContract.Definition, e.DBObjectContract.SchemaName, e.DBObjectContract.Name);

					break;
			}
		}

		private void ucDBObjectSelectSource_OnDBObjectClear(object sender, EventArgs e)
		{
			if (ucDBObjectSelectSource.SelectedObjectType == null)
				return;

			ObjectTypeEnum type = ucDBObjectSelectSource.SelectedObjectType.Type;
			switch (type)
			{
				case ObjectTypeEnum.U:
					ucTableViewCompareControl.FillGrid(null, null);

					break;
				default:
					ucNotePadCompareControl.PrepareLeftNotePad(null);

					break;
			}
		}

		private void ucDBObjectSelectTarget_OnDBObjectChanged(object sender, DBObjectChangedEventArgs e)
		{
			if (ucDBObjectSelectSource.SelectedObjectType == null)
				return;

			ObjectTypeEnum type = ucDBObjectSelectSource.SelectedObjectType.Type;
			switch (type)
			{
				case ObjectTypeEnum.U:
					ucTableViewCompareControl.FillGrid(ucDBObjectSelectSource.SelectedDBObject, ucDBObjectSelectTarget.SelectedDBObject);

					break;
				default:
					ucNotePadCompareControl.PrepareRightNotePad(e.DBObjectContract.Definition, e.DBObjectContract.SchemaName, e.DBObjectContract.Name);

					break;
			}
		}

		private void ucDBObjectSelectTarget_OnDBObjectClear(object sender, EventArgs e)
		{
			if (ucDBObjectSelectSource.SelectedObjectType == null)
				return;

			ObjectTypeEnum type = ucDBObjectSelectSource.SelectedObjectType.Type;
			switch (type)
			{
				case ObjectTypeEnum.U:
					ucTableViewCompareControl.FillGrid(null, null);

					break;
				default:
					ucNotePadCompareControl.PrepareRightNotePad(null);

					break;
			}
		}

		private void ucDBObjectSelectSource_OnObjectTypeChanged(object sender, EventArgs e)
		{
			ucDBObjectSelectTarget.SelectedObjectType = ucDBObjectSelectSource.SelectedObjectType;
			if (ucDBObjectSelectSource.SelectedObjectType == null)
			{
				ucNotePadCompareControl.Dock = DockStyle.None;
				ucNotePadCompareControl.Visible = false;
				ucTableViewCompareControl.Dock = DockStyle.None;
				ucTableViewCompareControl.Visible = false;
			}
			else if (ucDBObjectSelectSource.SelectedObjectType.Type == ObjectTypeEnum.U)
			{
				ucNotePadCompareControl.Dock = DockStyle.None;
				ucNotePadCompareControl.Visible = false;
				ucTableViewCompareControl.Dock = DockStyle.Fill;
				ucTableViewCompareControl.Visible = true;
			}
			else
			{
				ucTableViewCompareControl.Dock = DockStyle.None;
				ucTableViewCompareControl.Visible = false;
				ucNotePadCompareControl.Dock = DockStyle.Fill;
				ucNotePadCompareControl.Visible = true;
			}
		}

		private void _ucObjectType_OnObjectTypeChanged(object sender, EventArgs e)
		{
			ucDBObjectSelectSource.SelectedObjectType = _ucObjectType.SelectedObjectType;
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			UIHelper.SafeSetSplitterDistance(scDBObjectCompare, Constants.DefaultSplitterDistance);
		}
		#endregion

		#region Methods
		private void fillObjectTypeList()
		{
			ucDBObjectSelectSource.ClearSchemaList();
		}

		private void setTextFromResource()
		{
			this.gbObjectType.Text = CommonResource.ObjectType;
			this.ucDBObjectSelectSource.Caption = CommonResource.SourceDBObject;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			fillObjectTypeList();

			ucDBObjectSelectSource.MainForm
				= ucDBObjectSelectTarget.MainForm
				= ucNotePadCompareControl.MainForm
				= ucTableViewCompareControl.MainForm
				= MainForm;

			_ucObjectType.BackColor = Constants.ComponentRequiredColor;

			ucDBObjectSelectSource.InitForm();
			ucDBObjectSelectTarget.InitForm();

			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);
		}

		public override BaseScreenDataContract GetFormData()
		{
			Int32? objectType = (Int32?)_ucObjectType.SelectedObjectType?.Type;
			DBObjectSelectScreenDataContract sourceDBObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectSource.GetFormData();
			DBObjectSelectScreenDataContract targetDBObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectTarget.GetFormData();

			return new DBObjectCompareScreenDataContract
			{
				Name = CommonResource.SqlCompare,
				SourceDBObjectSelectFormDataContract = sourceDBObjectSelectFormDataContract,
				TargetDBObjectSelectFormDataContract = targetDBObjectSelectFormDataContract,
				ObjectTypeOriginal = objectType
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as DBObjectCompareScreenDataContract;
			if (data == null)
				return;

			ucDBObjectSelectSource.SetFormData(data.SourceDBObjectSelectFormDataContract);
			ucDBObjectSelectTarget.SetFormData(data.TargetDBObjectSelectFormDataContract);

			if (data.ObjectTypeOriginal.HasValue)
			{
				_ucObjectType.OnObjectTypeChanged -= _ucObjectType_OnObjectTypeChanged;
				_ucObjectType.SelectedObjectType = _ucObjectType.ObjectTypes.FirstOrDefault(o => o.Type == (ObjectTypeEnum?)data.ObjectTypeOriginal.Value);
				_ucObjectType.OnObjectTypeChanged -= _ucObjectType_OnObjectTypeChanged;
			}
		}
		#endregion
	}
}
