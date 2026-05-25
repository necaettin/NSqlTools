using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Popups;
using System;
using System.Collections.Generic;

namespace NSqlTools.UI.UserControls
{
	public partial class ucTableView : BaseUserControl
	{
		#region Properties
		public DBObjectContract dbObjectContract; 
		public DBObjectContract DBObjectContract
		{
			get
			{
				return dbObjectContract;
			}
			set{
				dbObjectContract = value;

				dgvColumns.BindList(value?.ColumnList);
				
				lblStatus.Text =
					value != null 
					? String.Format(CommonResource.Table0123Columns, value.DBName, value.SchemaName, value.Name, value.ColumnList.Count)
					: null;
			}
		}
		#endregion

		#region Constructor
		public ucTableView()
		{
			InitializeComponent();
			setTextFromResource();

			initForm();
		}

		#endregion

		#region Private Methods
		private void initForm()
		{
			dgvColumns.AutoGenerateColumns = false;
		}

		private void setTextFromResource()
		{
			this.ColumnIdColumn.HeaderText = CommonResource.ID;
			this.NameColumn.HeaderText = CommonResource.Name;
			this.TypeNameCustomColumn.HeaderText = CommonResource.Type;
			this.IsNullableColumn.HeaderText = CommonResource.Nullable;
			this.IsIdentityColumn.HeaderText = CommonResource.Identity;
			this.gbTableView.Text = CommonResource.TableColumns;
			this.tsbTableInfo.Text = CommonResource.TableInfo;
			this.tsbTableTriggers.Text = CommonResource.TableTriggers;
        }
        #endregion

        #region Private Methods
        public void SetDBObject(DBObjectContract dBObjectContract)
		{
			DBObjectContract = dBObjectContract;
		}
		#endregion

		#region Events
		private void tsbTableInfo_Click(object sender, EventArgs e)
		{
			frmTableInfo frm = new frmTableInfo(DBObjectContract);
			frm.ShowDialog();
		}

		private void tsbTableDependencies_Click(object sender, EventArgs e)
		{
			frmTableDependency frm = new frmTableDependency(DBObjectContract);
			frm.ShowDialog();
		}

        private void tsbTableTriggers_Click(object sender, EventArgs e)
        {
            frmTableTriggers frm = new frmTableTriggers(DBObjectContract);
            frm.ShowDialog();
        }
        #endregion
    }
}
