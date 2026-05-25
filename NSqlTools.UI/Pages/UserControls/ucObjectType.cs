using NSqlTools.BusinessLayer;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static NSqlTools.Types.Enums;

namespace NSqlTools.UI.UserControls
{
	public partial class ucObjectType : BaseUserControl
	{
		#region Event Handlers
		public event EventHandler OnObjectTypeChanged;
		#endregion

		#region Properties
		public Boolean IsNullable { get; set; } = false;

		private List<ObjectTypeContract> objectTypes;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public List<ObjectTypeContract> ObjectTypes
		{
			get
			{
				if (objectTypes == null)
				{
					ObjectTypeBusiness objectTypeHelper = new ObjectTypeBusiness();
					objectTypes = objectTypeHelper.ObjectTypes;
				}

				return objectTypes;
			}
			set
			{
				objectTypes = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ObjectTypeContract SelectedObjectType
		{
			get
			{
				if (tsbObjectType_P.Checked)
					return ObjectTypes.First(o => o.Type == ObjectTypeEnum.P);
				else if (tsbObjectType_U.Checked)
					return ObjectTypes.First(o => o.Type == ObjectTypeEnum.U);
				else if (tsbObjectType_FN.Checked)
					return ObjectTypes.First(o => o.Type == ObjectTypeEnum.FN);
				else if (tsbObjectType_IF.Checked)
					return ObjectTypes.First(o => o.Type == ObjectTypeEnum.IF);
				else if (tsbObjectType_V.Checked)
					return ObjectTypes.First(o => o.Type == ObjectTypeEnum.V);
				else if (tsbObjectType_TR.Checked)
					return ObjectTypes.First(o => o.Type == ObjectTypeEnum.TR);
				else
					return null;
			}
			set
			{
				//if (value?.Type == null)
				//	return;

				switch (value?.Type)
				{
					case ObjectTypeEnum.P:
						tsbObjectType_Click(tsbObjectType_P, EventArgs.Empty);

						break;
					case ObjectTypeEnum.U:
						tsbObjectType_Click(tsbObjectType_U, EventArgs.Empty);

						break;
					case ObjectTypeEnum.FN:
						tsbObjectType_Click(tsbObjectType_FN, EventArgs.Empty);

						break;
					case ObjectTypeEnum.IF:
						tsbObjectType_Click(tsbObjectType_IF, EventArgs.Empty);

						break;
					case ObjectTypeEnum.V:
						tsbObjectType_Click(tsbObjectType_V, EventArgs.Empty);

						break;
					case ObjectTypeEnum.TR:
						tsbObjectType_Click(tsbObjectType_TR, EventArgs.Empty);

						break;
					default:
						tsbObjectType_Click(null, EventArgs.Empty);
						break;
				}
			}
		}
		#endregion

		#region Constructor
		public ucObjectType()
		{
			InitializeComponent();
		}
		#endregion

		#region Events
		private void ucObjectType_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void tsbObjectType_Click(object sender, EventArgs e)
		{
			foreach (ToolStripButton item in tsObjectType.Items)
			{
				//if (item == null) 
				//	continue;

				Boolean isChecked = (!IsNullable && item == sender) || (IsNullable && item == sender && !item.Checked);
				item.Checked = isChecked;
				item.CheckState = isChecked ? CheckState.Checked : CheckState.Unchecked;
				item.BackColor = isChecked ? SystemColors.ScrollBar : SystemColors.Control;
			}

			callObjectTypeChangedEventHandler();
		}
		#endregion

		#region Methods
		private void fillObjectTypeList()
		{
			ObjectTypeBusiness objectTypeHelper = new ObjectTypeBusiness();
			ObjectTypes = objectTypeHelper.ObjectTypes;
			
			if(!IsNullable)
				SelectedObjectType = ObjectTypes.First(o => o.Type == ObjectTypeEnum.P);
			
			if (SelectedObjectType != null)
				callObjectTypeChangedEventHandler();
		}

		private void callObjectTypeChangedEventHandler()
		{
			OnObjectTypeChanged?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region Virtual Methods
		public override void InitForm()
		{
			fillObjectTypeList();
		}
		#endregion
	}
}
