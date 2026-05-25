using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.FormDataContracts;
using NSqlTools.Types.HelperContracts;
using NSqlTools.Types.Properties;
using NSqlTools.UI.Popups;
using NSqlTools.UI.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	public partial class ucRunQuery : BaseUserControl
	{
		#region Constructors
		public ucRunQuery()
		{
			InitializeComponent();
			setTextFromResource();
		}
		#endregion

		#region Properties
		public Boolean scQueryAndResultPanel2Collapsed
		{
			get
			{
				return scQueryAndResult.Panel2Collapsed;
			}
			set
			{
				scQueryAndResult.Panel2Collapsed = value;
				tsbQueryResultOpenClose.Image
					= scQueryAndResult.Panel2Collapsed
					? Properties.Resources.OpenDown
					: Properties.Resources.CloseDown;
				tsbQueryResultOpenClose.Text =
					scQueryAndResult.Panel2Collapsed
					? CommonResource.ExpandQueryResultsPanel
					: CommonResource.CollapseQueryResultsPanel;
			}
		}

		public Boolean scoQueryPanel1Collapsed
		{
			get
			{
				return scoQuery.Panel1Collapsed;
			}
			set
			{
				scoQuery.Panel1Collapsed = value;
				tsbCriteriaCollapse.Image
					= scoQuery.Panel1Collapsed
					? Properties.Resources.NotCollapse
					: Properties.Resources.Collapse;
				tsbCriteriaCollapse.Text =
					scoQuery.Panel1Collapsed
					? CommonResource.ExpandCriteriaPanel
					: CommonResource.CollapseCriteriaPanel;
			}
		}

		List<FavoriteQueryContract> favoriteQueryContractList = new List<FavoriteQueryContract>();

		public override List<Object> TabProviders
		{
			get {
				return new List<Object>
				{
					ucDBObjectSelectControl,
					ucSqlNotePadControl
				};
			}
		}

		private SQLScriptBusiness _sqlScriptBusiness;
		public SQLScriptBusiness sqlScriptBusiness
		{ 
			get { return _sqlScriptBusiness ?? (_sqlScriptBusiness = new SQLScriptBusiness()); }
		}

		private FavoriteQueryBusiness _favoriteQueryBusiness;
		public FavoriteQueryBusiness favoriteQueryBusiness
		{
			get { return _favoriteQueryBusiness ?? (_favoriteQueryBusiness = new FavoriteQueryBusiness()); }
		}
		#endregion

		#region Events
		private void tsbRunQuery_Click(object sender, EventArgs e)
		{
			runQuery();
		}

		private void tspParse_Click(object sender, EventArgs e)
		{
			runQuery(true);
		}

		private void tsbCriteriaCollapse_Click(object sender, EventArgs e)
		{
			scoQueryPanel1Collapsed = !scoQueryPanel1Collapsed;
		}

		private void tsbQueryResultOpenClose_Click(object sender, EventArgs e)
		{
			scQueryAndResultPanel2Collapsed = !scQueryAndResultPanel2Collapsed;
		}

		private void ucDBObjectSelectControl_OnDBClear(object sender, EventArgs e)
		{
			ucSqlNotePadControl.SchemaKeywordList = null;
			ucSqlNotePadControl.DBObjectKeywordList = null;
		}

		private void ucDBObjectSelectControl_OnDBChanged(object sender, EventArgs e)
		{
			ucSqlNotePadControl.SchemaKeywordList = ucDBObjectSelectControl.SchemaNameList;
			ucSqlNotePadControl.DBObjectKeywordList = ucDBObjectSelectControl.DBObjectNameList;
		}

		private void ucDBObjectSelectControl_OnDBObjectChanged(object sender, DBObjectChangedEventArgs e)
		{
			if (ucDBObjectSelectControl.SelectedObjectType.Type == Enums.ObjectTypeEnum.U)
				return;

			if (MessageBox.Show(CommonResource.TransferToNotePad,
				CommonResource.Confirmation,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ucSqlNotePadControl.SetDBObject(e.DBObjectContract);
			}
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			UIHelper.SafeSetSplitterDistance(scoQuery, Constants.DefaultSplitterDistance);
		}

		private void btnRefreshFavoriteQueries_Click(object sender, EventArgs e)
		{
			fillFavoriteQueries();
		}

		private void btnGetFromFavoriteQueries_Click(object sender, EventArgs e)
		{
			FavoriteQueryContract selectedFavoriteQueryContract = cbFavoriteQueries.SelectedItem as FavoriteQueryContract;
			if (selectedFavoriteQueryContract == null)
				return;

			FavoriteQueryContract favoriteQueryContract = favoriteQueryBusiness.GetByUniqueId(selectedFavoriteQueryContract.UniqueId);
			if (favoriteQueryContract == null)
				return;

			ucSqlNotePadControl.scSqlQuery.Text += favoriteQueryContract.QueryText;
		}

		private void btnSaveToFavoriteQueries_Click(object sender, EventArgs e)
		{
			FavoriteQueryContract selectedFavoriteQueryContract = cbFavoriteQueries.SelectedItem as FavoriteQueryContract;
			Boolean editMode = false;

			// Create Form
			frmFavoriteQueryPopup frm;
			String queryText =
					!String.IsNullOrWhiteSpace(ucSqlNotePadControl.SelectedNotePadText)
					? ucSqlNotePadControl.SelectedNotePadText
					: ucSqlNotePadControl.NotePadText;
			if (selectedFavoriteQueryContract == null)
			{
				frm = new frmFavoriteQueryPopup(favoriteQueryContractList, queryText);
			}
			else
			{
				editMode = true;
				selectedFavoriteQueryContract.QueryText =
					!String.IsNullOrWhiteSpace(ucSqlNotePadControl.SelectedNotePadText)
					? ucSqlNotePadControl.SelectedNotePadText
					: ucSqlNotePadControl.NotePadText;
				frm = new frmFavoriteQueryPopup(selectedFavoriteQueryContract, favoriteQueryContractList);
			}

			// Add or Update Favorite Query
			FavoriteQueryBusiness repository = new FavoriteQueryBusiness();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				var query = frm.GetFavoriteQuery();
				if (!editMode)
				{
					query.CreatedDate = DateTime.Now;
					repository.Add(query);
				}
				else
					repository.Update(query);
			}
		}

        // Event handler ekleyelim
        private void tsbCancelQuery_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }
		#endregion

		#region Methods
		private CancellationTokenSource _cancellationTokenSource;

		private async void runQuery(Boolean parse = false)
		{
			#region Validation
			if (ucDBObjectSelectControl.SelectedDB == null)
			{
				MessageBox.Show(CommonResource.SelectADatabase, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			#endregion

			#region Capture UI values
			String connectionString = ucDBObjectSelectControl.SelectedConnectionString.ConnectionString;
			String databaseName = ucDBObjectSelectControl.SelectedDB.Name;
			String runQueryString =
				!String.IsNullOrWhiteSpace(ucSqlNotePadControl.SelectedNotePadText)
				? ucSqlNotePadControl.SelectedNotePadText
				: ucSqlNotePadControl.NotePadText;
			#endregion

			#region Query Results
			scQueryAndResultPanel2Collapsed = false;
			tcQueryResults.TabPages.Clear();
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = new CancellationTokenSource();
			
			// Enable cancel button, disable run buttons
			tsbCancelQuery.Enabled = true;
			tsbRunQuery.Enabled = false;
			tspParse.Enabled = false;

            try
            {
				// Run query on background thread with cancellation support
				RunSqlResultContract runSqlResultContract = await Task.Run(() => 
					sqlScriptBusiness.RunSqlQuery(
						connectionString,
						databaseName,
						runQueryString,
						parse,
						_cancellationTokenSource.Token),
					_cancellationTokenSource.Token);

				// Display return tables
				for (Int32 i = 0; i < runSqlResultContract.TableCollection.Count; i++)
				{
					DataTable dt = runSqlResultContract.TableCollection[i];
					String tabName = String.Format(CommonResource.XQueryResult, i + 1);
					addQueryResultGridTab(tabName, dt);
				}

				// Display messages
				addQueryResultTextBoxTab(CommonResource.Messages, runSqlResultContract.AffectedRowsMessages.ToString());
			}
			catch (OperationCanceledException)
			{
				addQueryResultTextBoxTab(CommonResource.Messages, NSqlTools.Types.Properties.CommonResource.QueryCancelled);
			}
			catch (SqlException ex)
			{
				// SqlCommand.Cancel() SqlException fırlatır
				if (ex.Number == -2 || ex.Message.Contains("cancelled") || ex.Message.Contains("iptal"))
				{
					addQueryResultTextBoxTab(CommonResource.Messages, NSqlTools.Types.Properties.CommonResource.QueryCancelled);
				}
				else
				{
					// Normal SQL error handling
					LogHelper.Error(ex);
					StringBuilder sb = new StringBuilder();
					foreach (SqlError error in ex.Errors)
					{
						sb.AppendLine($"Message: {error.Message}");
						sb.AppendLine($"Line Number: {error.LineNumber}");
						sb.AppendLine($"Number: {error.Number}");
						sb.AppendLine($"Source: {error.Source}");
						sb.AppendLine($"State: {error.State}");
						sb.AppendLine($"Procedure: {error.Procedure}");
						sb.AppendLine("----------------------");
					}
					addQueryResultTextBoxTab(CommonResource.Messages, sb.ToString());
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				addQueryResultTextBoxTab(CommonResource.Messages, ex.Message);
			}
			finally
			{
				tsbCancelQuery.Enabled = false;
				tsbRunQuery.Enabled = true;
				tspParse.Enabled = true;
            }
            #endregion

            if (tcQueryResults.TabPages.Count > 0)
				tcQueryResults.SelectedTab = tcQueryResults.TabPages[0];
		}

		private void addQueryResultGridTab(String caption, DataTable dataSource)
		{
			TabPage tpQueryResult = new TabPage(caption);
			ucQueryResult tableView = new ucQueryResult
			{
				MainForm = this.MainForm,
				Dock = DockStyle.Fill,
				ParentTabPage = this.ParentTabPage,
				DataSource = dataSource
			};
			tableView.MainForm = this.MainForm;
			tpQueryResult.Padding = new Padding(2, 5, 2, 2);
			tpQueryResult.Controls.Add(tableView);
			tcQueryResults.TabPages.Add(tpQueryResult);
			//tcQueryResults.SelectedTab = tpQueryResult;
		}

		private void addQueryResultTextBoxTab(String caption, String message)
		{
			TabPage tpQueryResult = new TabPage(caption);
			TextBox txtQueryResult = new TextBox();
			txtQueryResult.Text = message;
			txtQueryResult.Multiline = true;
			txtQueryResult.ReadOnly = true;
			txtQueryResult.Dock = DockStyle.Fill;
			txtQueryResult.ScrollBars = ScrollBars.Both;
			tpQueryResult.Padding = new Padding(2, 5, 2, 2);
			tpQueryResult.AutoScroll = true;
			tpQueryResult.HorizontalScroll.Visible = true;
			tpQueryResult.Controls.Add(txtQueryResult);
			tcQueryResults.TabPages.Add(tpQueryResult);
			//tcQueryResults.SelectedTab = tpQueryResult;
		}

		private void fillFavoriteQueries()
		{
			// Listeyi yenilerken eski öğeleri temizle
			cbFavoriteQueries.Items.Clear();

			// Veri kaynağını al
			favoriteQueryContractList = new FavoriteQueryBusiness().GetAll() ?? new List<FavoriteQueryContract>();
			cbFavoriteQueries.Items.AddRange(
				favoriteQueryContractList
					.OrderBy(f => f.Name)
					.ToArray()
			);

			cbFavoriteQueries.Text = null;
		}

		private void setTextFromResource()
		{
			this.ucDBObjectSelectControl.Caption = CommonResource.DBSelect;
			this.tsMenu.Text = CommonResource.ExpandQueryResultsPanel;
			this.tsbRunQuery.Text = CommonResource.RunQuery;
			this.tspParse.Text = CommonResource.ParseSql;
			this.tsbCancelQuery.Text = "Cancel Query"; // Hardcoded until resource is added
			this.tsbCriteriaCollapse.Text = CommonResource.CollapseCriteriaPanel;
			this.tsbQueryResultOpenClose.Text = CommonResource.ExpandQueryResultsPanel;
			this.btnGetFromFavoriteQueries.ToolTipText = CommonResource.GetFromFavoriteQueries;
			this.btnSaveToFavoriteQueries.ToolTipText = CommonResource.SaveToFavoriteQueries;
			this.btnRefreshFavoriteQueries.ToolTipText = CommonResource.RefreshFavoriteQueries;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			fillFavoriteQueries();
			ucDBObjectSelectControl.InitForm();
			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);
		}

		public override BaseScreenDataContract GetFormData()
		{
			DBObjectSelectScreenDataContract dbObjectSelectFormDataContract = (DBObjectSelectScreenDataContract)ucDBObjectSelectControl.GetFormData();
			return new RunQueryScreenDataContract
			{
				Name = CommonResource.RunQuery,

				QueryText = ucSqlNotePadControl.NotePadText,
				DataSourceName = dbObjectSelectFormDataContract.DataSourceName,
				DBIndexes = dbObjectSelectFormDataContract.DBIndexes,
				ObjectType = dbObjectSelectFormDataContract.ObjectType,
				SchemaId = dbObjectSelectFormDataContract.SchemaId,
				ObjectId = dbObjectSelectFormDataContract.ObjectId
			};
		}

		public override void SetFormData(BaseScreenDataContract formDataBaseContract)
		{
			var data = formDataBaseContract as RunQueryScreenDataContract;
			if (data == null)
				return;

			ucDBObjectSelectControl.OnDBObjectChanged -= this.ucDBObjectSelectControl_OnDBObjectChanged;
			ucDBObjectSelectControl.SetFormData(data);
			ucDBObjectSelectControl.OnDBObjectChanged += this.ucDBObjectSelectControl_OnDBObjectChanged;

			ucSqlNotePadControl.SetDBObject(new DBObjectContract() { Definition = data.QueryText });
		}

		#region Override Methods
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.F5)
			{
				runQuery();

				return true;
			}
			else if (keyData == (Keys.Control | Keys.R))
			{
				tsbQueryResultOpenClose_Click(tsbQueryResultOpenClose, EventArgs.Empty);

				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		public void OnHandleDestroyed(object sender, EventArgs e)
		{
			if (MainForm is frmMain main)
				main.Resize -= frmMain_Resize;
		}
		#endregion

		#endregion
	}
};

