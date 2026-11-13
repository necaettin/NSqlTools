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
            lock (_queryLock)
            {
                _cancellationTokenSource?.Cancel();
            }
        }
		#endregion

		#region Methods
		private CancellationTokenSource _cancellationTokenSource;
		private volatile bool _isQueryRunning = false;
		private readonly object _queryLock = new object();

		private bool canUpdateUi()
		{
			return !IsDisposed && !Disposing && IsHandleCreated && tcQueryResults != null && !tcQueryResults.IsDisposed;
		}

		private bool canUpdateQueryResults()
		{
			return canUpdateUi() && tcQueryResults.TabPages != null;
		}

		private void clearQueryResults()
		{
			if (!canUpdateQueryResults())
				return;

			try
			{
				var pages = tcQueryResults.TabPages.Cast<TabPage>().ToList();
				tcQueryResults.TabPages.Clear();
				foreach (TabPage page in pages)
					page.Dispose();
			}
			catch (ObjectDisposedException)
			{
			}
			catch (InvalidOperationException)
			{
			}
		}

		private void setQueryToolbarState(Boolean isRunning)
		{
			if (!canUpdateUi())
				return;

			if (this.InvokeRequired)
			{
				try
				{
					this.BeginInvoke(new Action(() =>
					{
						if (!canUpdateUi())
							return;

						if (tsbCancelQuery != null && !tsbCancelQuery.IsDisposed)
							tsbCancelQuery.Enabled = isRunning;
						if (tsbRunQuery != null && !tsbRunQuery.IsDisposed)
							tsbRunQuery.Enabled = !isRunning;
						if (tspParse != null && !tspParse.IsDisposed)
							tspParse.Enabled = !isRunning;
					}));
				}
				catch (ObjectDisposedException)
				{
				}
				catch (InvalidOperationException)
				{
				}
			}
			else
			{
				if (tsbCancelQuery != null && !tsbCancelQuery.IsDisposed)
					tsbCancelQuery.Enabled = isRunning;
				if (tsbRunQuery != null && !tsbRunQuery.IsDisposed)
					tsbRunQuery.Enabled = !isRunning;
				if (tspParse != null && !tspParse.IsDisposed)
					tspParse.Enabled = !isRunning;
			}
		}

		private async void runQuery(Boolean parse = false)
		{
			#region Validation
			if (ucDBObjectSelectControl.SelectedDB == null)
			{
				MessageBox.Show(CommonResource.SelectADatabase, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			lock (_queryLock)
			{
				if (_isQueryRunning)
				{
					MessageBox.Show("A query is already running. Please wait or cancel it first.", CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				_isQueryRunning = true;
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
			if (!canUpdateUi())
			{
				lock (_queryLock)
				{
					_isQueryRunning = false;
				}
				return;
			}

			scQueryAndResultPanel2Collapsed = false;
			clearQueryResults();

			CancellationTokenSource cancellationTokenSource;
			lock (_queryLock)
			{
				_cancellationTokenSource?.Dispose();
				cancellationTokenSource = new CancellationTokenSource();
				_cancellationTokenSource = cancellationTokenSource;
			}

			setQueryToolbarState(true);

			try
			{
				// Run query on background thread with cancellation support
				RunSqlResultContract runSqlResultContract = await Task.Run(() => 
					sqlScriptBusiness.RunSqlQuery(
						connectionString,
						databaseName,
						runQueryString,
						parse,
						cancellationTokenSource.Token),
					cancellationTokenSource.Token).ConfigureAwait(true);

				if (!canUpdateUi())
					return;

				// Display return tables
				if (runSqlResultContract.TableCollection.Count > 0)
				{
					if (!canUpdateQueryResults())
						return;

					addQueryResultGridTab(CommonResource.Result, runSqlResultContract.TableCollection.Cast<DataTable>().ToList());
				}

				// Display messages
				if (runSqlResultContract.AffectedRowsMessages != null && runSqlResultContract.AffectedRowsMessages.Length > 0)
				{
					if (canUpdateQueryResults())
						addQueryResultTextBoxTab(CommonResource.Messages, string.Join(Environment.NewLine, runSqlResultContract.AffectedRowsMessages));
				}
			}
			catch (TaskCanceledException)
			{
				if (canUpdateQueryResults())
					addQueryResultTextBoxTab(CommonResource.Messages, NSqlTools.Types.Properties.CommonResource.QueryCancelled);
			}
			catch (OperationCanceledException)
			{
				if (canUpdateQueryResults())
					addQueryResultTextBoxTab(CommonResource.Messages, NSqlTools.Types.Properties.CommonResource.QueryCancelled);
			}
			catch (SqlException ex)
			{
				// SqlCommand.Cancel() SqlException fırlatır (Error -2)
				if (ex.Number == -2 || ex.Message.Contains("cancelled") || ex.Message.Contains("iptal"))
				{
					if (canUpdateQueryResults())
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
					if (canUpdateQueryResults())
						addQueryResultTextBoxTab(CommonResource.Messages, sb.ToString());
				}
			}
			catch (OutOfMemoryException ex)
			{
				LogHelper.Error(ex);
				if (canUpdateQueryResults())
					addQueryResultTextBoxTab(CommonResource.Messages, NSqlTools.Types.Properties.CommonResource.QueryReturnedTooMuchDataTryAddingTOPClauseOrWHEREConditionsToLimitResultsNError + ex.Message);
			}
			catch (InvalidOperationException ex) when (ex.Message.Contains("connection") || ex.Message.Contains("bağlantı"))
			{
				LogHelper.Error(ex);
				if (canUpdateQueryResults())
					addQueryResultTextBoxTab(CommonResource.Messages, "Connection error: " + ex.Message);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				if (canUpdateQueryResults())
					addQueryResultTextBoxTab(CommonResource.Messages, "Unexpected error: " + ex.Message);
			}
			finally
			{
				lock (_queryLock)
				{
					_isQueryRunning = false;
					if (ReferenceEquals(_cancellationTokenSource, cancellationTokenSource))
					{
						_cancellationTokenSource = null;
					}
					cancellationTokenSource.Dispose();
				}

				setQueryToolbarState(false);
			}
			#endregion

			if (canUpdateQueryResults() && tcQueryResults.TabPages.Count > 0)
				tcQueryResults.SelectedTab = tcQueryResults.TabPages[0];
		}

		private const Int32 MaxDisplayRows = 10000;

		private static DataTable limitRows(DataTable source, Int32 maxRows)
		{
			if (source.Rows.Count <= maxRows)
				return source;

			DataTable limited = source.Clone();
			for (Int32 r = 0; r < maxRows; r++)
				limited.ImportRow(source.Rows[r]);
			return limited;
		}

		private const Int32 MaxResultGridHeight = 200;
		private const Int32 MinResultGridHeight = 100;
		private const Int32 ResultGridSpacing = 6;
		private const Int32 EstimatedRowHeight = 22;
		private const Int32 EstimatedHeaderAndScrollHeight = 23 + 17 + 4;

		private void addQueryResultGridTab(String caption, List<DataTable> dataSources)
		{
			if (!canUpdateQueryResults())
				return;

			try
			{
				TabPage tpQueryResult = new TabPage(caption);
				tpQueryResult.Padding = new Padding(2, 5, 2, 2);

				tpQueryResult.Controls.Add(buildResultLayout(dataSources, 0));

				if (canUpdateQueryResults())
					tcQueryResults.TabPages.Add(tpQueryResult);
				else
					tpQueryResult.Dispose();
			}
			catch (ObjectDisposedException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
			}
		}

		private ucQueryResult createQueryResultView(DataTable dataSource, DockStyle dock)
		{
			return new ucQueryResult
			{
				MainForm = this.MainForm,
				Dock = dock,
				ParentTabPage = this.ParentTabPage,
				DataSource = limitRows(dataSource, MaxDisplayRows)
			};
		}

		private Control buildResultLayout(List<DataTable> dataSources, Int32 index)
		{
			// Outer panel: single vertical scrollbar for all results (like SSMS)
			Panel pnlResults = new Panel
			{
				Dock = DockStyle.Fill,
				AutoScroll = true,
				BackColor = System.Drawing.SystemColors.ControlDark
			};

			pnlResults.SuspendLayout();
			try
			{
				// Dock = Top stacks the last added control on top, so add in reverse order
				for (Int32 i = dataSources.Count - 1; i >= index; i--)
				{
					DataTable table = dataSources[i];

					Panel pnlItem = new Panel
					{
						Dock = DockStyle.Top,
						Height = calculateResultHeight(table),
						BackColor = System.Drawing.SystemColors.Control
					};
					pnlItem.Controls.Add(createQueryResultView(table, DockStyle.Fill));

					// Drag handle resizes the result panel directly above it
					pnlResults.Controls.Add(createResultResizeHandle(pnlItem));
					pnlResults.Controls.Add(pnlItem);
				}
			}
			finally
			{
				pnlResults.ResumeLayout(true);
			}

			return pnlResults;
		}

		private static Control createResultResizeHandle(Control target)
		{
			Panel handle = new Panel
			{
				Dock = DockStyle.Top,
				Height = ResultGridSpacing,
				Cursor = Cursors.HSplit,
				BackColor = System.Drawing.SystemColors.ControlDark
			};

			Boolean dragging = false;
			Int32 startY = 0;
			Int32 startHeight = 0;

			handle.MouseDown += (s, e) =>
			{
				if (e.Button != MouseButtons.Left)
					return;
				dragging = true;
				startY = Control.MousePosition.Y;
				startHeight = target.Height;
			};
			handle.MouseMove += (s, e) =>
			{
				if (!dragging)
					return;
				Int32 newHeight = Math.Max(MinResultGridHeight, startHeight + (Control.MousePosition.Y - startY));
				if (newHeight != target.Height)
					target.Height = newHeight;
			};
			handle.MouseUp += (s, e) => dragging = false;

			return handle;
		}

		private static Int32 calculateResultHeight(DataTable table)
		{
			Int32 rowCount = Math.Min(table?.Rows.Count ?? 0, MaxDisplayRows);
			Int32 desired = EstimatedHeaderAndScrollHeight + (rowCount * EstimatedRowHeight);
			return Math.Max(MinResultGridHeight, Math.Min(MaxResultGridHeight, desired));
		}

		private void addQueryResultTextBoxTab(String caption, String message)
		{
			if (!canUpdateQueryResults())
				return;

			try
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

				if (canUpdateQueryResults())
					tcQueryResults.TabPages.Add(tpQueryResult);
				else
					tpQueryResult.Dispose();
			}
			catch (ObjectDisposedException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
			}
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
			this.tsbCancelQuery.Text = NSqlTools.Types.Properties.CommonResource.CancelQuery;
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
			if (MainForm is frmMain main)
			{
				main.Resize -= frmMain_Resize;
				main.Resize += frmMain_Resize;
			}
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

       protected override void OnHandleDestroyed(EventArgs e)
		{
			if (MainForm is frmMain main)
				main.Resize -= frmMain_Resize;

			lock (_queryLock)
			{
				try
				{
					_cancellationTokenSource?.Cancel();
					_cancellationTokenSource?.Dispose();
					_cancellationTokenSource = null;
				}
				catch { }
			}

			base.OnHandleDestroyed(e);
		}
		#endregion

		#endregion
	}
};

