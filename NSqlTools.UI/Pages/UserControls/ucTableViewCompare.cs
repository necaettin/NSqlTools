using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.UI.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NSqlTools.Types.Properties;

namespace NSqlTools.UI.UserControls
{
	public partial class ucTableViewCompare : BaseUserControl
	{
		#region Constructor
		public ucTableViewCompare()
		{
			InitializeComponent();
			setTextFromResource();

			initForm();
		}
		#endregion

		#region Properties
		public List<ColumnCompareContract> dataSource;
		public List<ColumnCompareContract> DataSource
		{
			get
			{
				return dataSource;
			}
			set
			{
				dataSource = value;
				dgvColumns.BindList(value == null ? null : new List<ColumnCompareContract>(value));

				tsbEqual.CheckState = CheckState.Unchecked;
				tsbNotEqual.CheckState = CheckState.Unchecked;
				tsbSourceExists.CheckState = CheckState.Unchecked;
				tsbTargetExists.CheckState = CheckState.Unchecked;

				lblStatus.Text = String.Format(CommonResource._0DBObjects, value?.Count() ?? 0);
			}
		}
		#endregion

		#region Events
		private void filterColumnsGrid(Object sender, EventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;
			button.CheckState = button.CheckState 
				== CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;

			filterDataSource();
		}
		#endregion

		#region Private Methods
		private void initForm()
		{
			dgvColumns.AutoGenerateColumns = false;
		}

		private void setTextFromResource()
		{
			this.ColumnIdSourceColumn.HeaderText = CommonResource.IDSource;
			this.NameSourceColumn.HeaderText = CommonResource.NameSource;
			this.TypeNameCustomSourceColumn.HeaderText = CommonResource.TypeSource;
			this.IsNullableSourceColumn.HeaderText = CommonResource.NullableSource;
			this.IsIdentitySourceColumn.HeaderText = CommonResource.IdentitySource;
			this.Diff.HeaderText = CommonResource.Difference;
			this.ColumnIdTargetColumn.HeaderText = CommonResource.IDTarget;
			this.NameTargetColumn.HeaderText = CommonResource.NameTarget;
			this.TypeNameCustomTargetColumn.HeaderText = CommonResource.TypeTarget;
			this.IsNullableTargetColumn.HeaderText = CommonResource.NullableTarget;
			this.IsIdentityTargetColumn.HeaderText = CommonResource.IdentityTarget;
			this.tsbEqual.Text = CommonResource.Equal;
			this.tsbNotEqual.Text = CommonResource.NotEqual;
			this.tsbSourceExists.Text = CommonResource.ExistsInSource;
			this.tsbTargetExists.Text = CommonResource.ExistsInTarget;
		}

		private void filterDataSource()
		{
			if (DataSource == null)
			{
				DataSource = null;

				return;
			}

			dgvColumns.BindList(DataSource == null ? null : new SortableBindingList<ColumnCompareContract>(DataSource.Where(d =>
				(tsbEqual.CheckState == CheckState.Unchecked && tsbNotEqual.CheckState == CheckState.Unchecked && tsbSourceExists.CheckState == CheckState.Unchecked && tsbTargetExists.CheckState == CheckState.Unchecked)
				|| (tsbEqual.CheckState == CheckState.Checked && d.ColumnDifferenceType == Enums.ColumnDifferenceTypeEnum.Equal)
				|| (tsbNotEqual.CheckState == CheckState.Checked && d.ColumnDifferenceType == Enums.ColumnDifferenceTypeEnum.NotEqual)
				|| (tsbSourceExists.CheckState == CheckState.Checked && d.ColumnDifferenceType == Enums.ColumnDifferenceTypeEnum.SourceExists)
				|| (tsbTargetExists.CheckState == CheckState.Checked && d.ColumnDifferenceType == Enums.ColumnDifferenceTypeEnum.TargetExists)
			).ToList()));
		}
		#endregion

		#region Public Methods
		public void FillGrid(List<ColumnCompareContract> columnCompareResultList)
		{
			DataSource = columnCompareResultList;
		}

		public void FillGrid(DBObjectContract dBObjectSourceContract, DBObjectContract dBObjectTargetContract)
		{
			if (dBObjectSourceContract == null || dBObjectTargetContract == null)
			{
				DataSource = null;

				return;
			}

			// Compare columns
			var columListResult = dBObjectSourceContract.ColumnList.FullOuterJoin(
				dBObjectTargetContract.ColumnList, source => source.Name, target => target.Name,
				(source, target, Name) => new ColumnCompareContract
				{
					ColumnIdSource = source?.ColumnId,
					NameSource = source?.Name,
					SystemTypeIdSource = source?.SystemTypeId,
					UserTypeIdSource = source?.UserTypeId,
					MaxLengthSource = source?.MaxLength,
					PrecisionSource = source?.Precision,
					IsNullableSource = source?.IsNullable,
					IsIdentitySource = source?.IsIdentity,
					TypeNameSource = source?.TypeName,

					Difference = Resources.Equality_Equal,

					ColumnIdTarget = target?.ColumnId,
					NameTarget = target?.Name,
					SystemTypeIdTarget = target?.SystemTypeId,
					UserTypeIdTarget = target?.UserTypeId,
					MaxLengthTarget = target?.MaxLength,
					PrecisionTarget = target?.Precision,
					IsNullableTarget = target?.IsNullable,
					IsIdentityTarget = target?.IsIdentity,
					TypeNameTarget = target?.TypeName
				},
				null,
				null
			).ToList();

			// Set equality images
			columListResult.ForEach(c =>
			{
				if (c.ColumnIdSource == null)
				{
					c.Difference = Resources.Equality_TargetExists;
					c.ColumnDifferenceType = Enums.ColumnDifferenceTypeEnum.TargetExists;
				}
				else if (c.ColumnIdTarget == null)
				{
					c.Difference = Resources.Equality_SourceExists;
					c.ColumnDifferenceType = Enums.ColumnDifferenceTypeEnum.SourceExists;
				}
				else if (c.UserTypeIdSource != c.UserTypeIdTarget || c.NameSource != c.NameTarget || c.MaxLengthSource != c.MaxLengthTarget || c.PrecisionSource != c.PrecisionTarget || c.IsNullableSource != c.IsNullableTarget)
				{
					c.Difference = Resources.Equality_NotEqual;
					c.ColumnDifferenceType = Enums.ColumnDifferenceTypeEnum.NotEqual;
				}
				else
				{
					c.Difference = Resources.Equality_Equal;
					c.ColumnDifferenceType = Enums.ColumnDifferenceTypeEnum.Equal;
				}
			});

			// Set datasource
			DataSource = columListResult;
		}
		#endregion
	}
}
