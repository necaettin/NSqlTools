using NSqlTools.BusinessLayer;
using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages
{
	public partial class ucTableToCSV : BaseUserControl
	{
		#region Constructors
		public ucTableToCSV()
		{
			InitializeComponent();

			setTextFromResource();
		}
		#endregion

		#region Properties
		public List<TableContract> tableContractList;
        public List<TableContract> TableContractList 
		{ 
			get{ return tableContractList; }
			set
			{
				tableContractList = value;
				filterTableGrid();
			}
		}

        public List<DBContract> DBContractList 
		{
			get
			{
				return ucDBObjectSelectControl.SelectedDBList;
			}
			set{
				ucDBObjectSelectControl.SelectedDBList.ForEach(db => db.Progress = 0);

				dgvDBProgress.AutoGenerateColumns = false;
				dgvDBProgress.BindList(value ?? new List<DBContract>());
			} 
		}

        private CancellationTokenSource cancellationTokenSource;

        public SemaphoreSlim semaphore { get; set; }
        #endregion

        #region Events
        private async void tsbRun_Click(object sender, EventArgs e)
		{
			await run();
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			scoQuery.Size = new System.Drawing.Size(3000, 3000);
			UIHelper.SafeSetSplitterDistance(scoQuery, Constants.DefaultSplitterDistance);
		}

		private void btnPathSelect_Click(object sender, EventArgs e)
		{
			if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void tsbCompleted_Click(object sender, EventArgs e)
		{
			tsbNotCompleted.CheckState
				= tsbRunning.CheckState
				= CheckState.Unchecked;         
			filterTableGrid();
		}

		private void tsbNotCompleted_Click(object sender, EventArgs e)
		{
			tsbRunning.CheckState
				= tsbCompleted.CheckState
				= CheckState.Unchecked;             
			filterTableGrid();
		}

		private void tsbRunning_Click(object sender, EventArgs e)
		{
			tsbCompleted.CheckState
				= tsbNotCompleted.CheckState
				= CheckState.Unchecked;         
			filterTableGrid();
		}

		private void tsbStop_Click(object sender, EventArgs e)
		{
			cancellationTokenSource.Cancel();
			semaphore.Dispose();

			addLog(CommonResource.ProcessCancelled);
		}
		#endregion

		#region Methods
		private async Task run(Boolean parse = false)
		{
			TableBusiness dbBusiness = new TableBusiness();
			
			#region Validation
			if (ucDBObjectSelectControl.SelectedDB == null)
			{
				MessageBox.Show(CommonResource.SelectADatabase, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}

			if (String.IsNullOrWhiteSpace(txtPath.Text))
			{
				MessageBox.Show(CommonResource.SelectAPath, CommonResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}
			#endregion

			#region Clear & Get variables
			txtLogs.Clear();
			DBContractList = null;
			TableContractList = null;

			cancellationTokenSource = new CancellationTokenSource();

			String rootPath = txtPath.Text;
			Boolean createZip = cbCreateZip.Checked;
			Boolean addTableColumnsToCSV = cbAddTableColumnsToCSV.Checked;

			tsbRun.Enabled = false;
			tsbStop.Enabled = true;
			#endregion

			#region Run
			try
			{
				DBContractList = ucDBObjectSelectControl.SelectedDBList;

				foreach (DBContract selectedDB in ucDBObjectSelectControl.SelectedDBList)
                {
					if (cancellationTokenSource.IsCancellationRequested)
						return;

					#region Query Results
					TableContractList = dbBusiness.GetTableList(ucDBObjectSelectControl.SelectedConnectionString.ConnectionString, selectedDB.Name, cbOnlyNotEmptyTables.Checked);
					if (TableContractList.Count == 0)
					{
						addLog(String.Format(CommonResource.NoTableFoundForX, selectedDB.Name));

						continue;
					}

					addLog(String.Format(CommonResource.ExportToCsvStartedForX, selectedDB.Name));

					List<Task> tasks = new List<Task>();
					String dbName = selectedDB.Name;
					Decimal totalRowCount = TableContractList.Sum(t => t.RowCount);
					String dbPath = System.IO.Path.Combine(rootPath, dbName);
					String zipFileName = System.IO.Path.Combine(rootPath, dbName + ".zip");
					String connectionString = ucDBObjectSelectControl.SelectedConnectionString.ConnectionString;

					if (!Directory.Exists(dbPath))
						Directory.CreateDirectory(dbPath);
					Int32 commandTimeout = Convert.ToInt32(nudCommandTimeout.Value);
					Decimal rowCountCounter = 0;

					semaphore = new SemaphoreSlim((Int32)nudThreadCount.Value);
					TaskFactory factory = new TaskFactory(cancellationTokenSource.Token);
					foreach (TableContract tableContract in TableContractList)
					{
						if (cancellationTokenSource.IsCancellationRequested)
							return;

						tasks.Add(Task.Run(async () =>
						{
							cancellationTokenSource.Token.ThrowIfCancellationRequested();

							await semaphore.WaitAsync();
							try
							{
								dgvTables.InvokeIfRequired(d =>
								{
									tableContract.Status = Enums.RunningStatusEnum.Running;
									filterTableGrid();
								});

								if (cancellationTokenSource.IsCancellationRequested)
									return; 
								
								await exportTableDataToCsvAsync(tableContract.Name, connectionString, dbName, commandTimeout, dbPath, tableContract.Name + ".csv", addTableColumnsToCSV);
							}
							finally
							{
								dgvTables.InvokeIfRequired(d =>
								{
									tableContract.Status = Enums.RunningStatusEnum.Completed;
									filterTableGrid();
								});

								rowCountCounter += tableContract.RowCount;
								dgvDBProgress.InvokeIfRequired(d =>
								{
									selectedDB.Progress = Convert.ToInt16(Math.Round((rowCountCounter * 100) / totalRowCount));
									dgvDBProgress.Refresh();
								});

								semaphore.Release();
							}
						}, cancellationTokenSource.Token));
					}

					// Wait for all tasks to complete
					await factory.ContinueWhenAll(tasks.ToArray(), (results) => { }, cancellationTokenSource.Token);

					#region Zip the folder
					if (createZip)
					{
						addLog(String.Format(CommonResource.ZipFileCreateStartedForX, selectedDB.Name));
						if(File.Exists(zipFileName))
							File.Delete(zipFileName);
						ZipFile.CreateFromDirectory(dbPath, zipFileName);
						addLog(String.Format(CommonResource.ZipFileCreateEndedForX, selectedDB.Name));
					}
					addLog(String.Format(CommonResource.ExportToCsvCompletedForDB, selectedDB.Name));
					#endregion

					#endregion
				}

				addLog(String.Format(CommonResource.ExportToCSVCompleted));
			}
			catch(Exception ex)
			{
				addLog(String.Format(CommonResource.ErrorOccuredErrorDetail, ex.Message));
			}
			finally
			{
				tsbRun.Enabled = true;
				tsbStop.Enabled = false;

				if(semaphore != null)
					semaphore.Dispose();
			}
			#endregion
		}

		private async Task exportTableDataToCsvAsync(String tableName, String connectionString, String dbName, Int32 commandTimeout, String folderPath, String fileName, Boolean addTableColumnsToCSV)
		{
			String filePath = Path.Combine(folderPath, fileName);

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					await connection.OpenAsync();

					addLog(String.Format(CommonResource.FileExportedStartedTableNameX, tableName));
					using (SqlCommand command = new SqlCommand($"SELECT * FROM {dbName}.{tableName} WITH (NOLOCK)", connection))
					{
						command.CommandTimeout = commandTimeout;

						using (SqlDataReader reader = await command.ExecuteReaderAsync())
						{
							using (StreamWriter writer = new StreamWriter(filePath))
							{
								if (addTableColumnsToCSV)
								{
									// Write the header row
									for (int i = 0; i < reader.FieldCount; i++)
									{
										writer.Write(reader.GetName(i));
										if (i < reader.FieldCount - 1)
											writer.Write(",");
									}
									writer.WriteLine();
								}

								// Write the data rows
								while (await reader.ReadAsync())
								{
									for (int i = 0; i < reader.FieldCount; i++)
									{
										String value = reader[i].ToString();
										writer.Write("\"" + value + "\"");
										if (i < reader.FieldCount - 1)
											writer.Write(",");
									}
									writer.WriteLine();
								}
							}
						}
					}
					addLog(String.Format(CommonResource.FileExportedEndedTableNameX, tableName));
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				addLog(String.Format(CommonResource.ErrorOccuredTableNameErrorDetail, tableName, ex.Message));
			}
		}

		private void addLog(String message)
		{
			txtLogs.InvokeIfRequired(t => { txtLogs.AppendText(message + Environment.NewLine); });
		}

		private void filterTableGrid()
		{
			dgvTables.AutoGenerateColumns = false;
			dgvTables.BindList(TableContractList == null ? new List<TableContract>() : new List<TableContract>(TableContractList.Where(d =>
				(tsbCompleted.CheckState == CheckState.Unchecked && tsbNotCompleted.CheckState == CheckState.Unchecked && tsbRunning.CheckState == CheckState.Unchecked)
				|| (tsbCompleted.CheckState == CheckState.Checked && d.Status == Enums.RunningStatusEnum.Completed)
				|| (tsbNotCompleted.CheckState == CheckState.Checked && d.Status == Enums.RunningStatusEnum.NotCompleted)
				|| (tsbRunning.CheckState == CheckState.Checked && d.Status == Enums.RunningStatusEnum.Running)
			).ToList()));
		}

		private void setTextFromResource()
		{
			this.groupBox1.Text = CommonResource.Criteria;
			this.cbCreateZip.Text = CommonResource.CreateZip;
			this.label1.Text = CommonResource.CommandTimeout;
			this.cbAddTableColumnsToCSV.Text = CommonResource.AddTableColumnsToCSV;
			this.lblThreadCount.Text = CommonResource.ThreadCount;
			this.lblPath.Text = CommonResource.Path;
			this.cbOnlyNotEmptyTables.Text = CommonResource.OnlyNotEmptyTables;
			this.ucDBObjectSelectControl.Caption = CommonResource.DBSelect;
			this.groupBox4.Text = CommonResource.DBList;
			this.DBNameColumn.HeaderText = CommonResource.DBName;
			this.ProgressColumn.HeaderText = CommonResource.Progress;
			this.groupBox3.Text = CommonResource.Tables;
			this.NameColumn.HeaderText = CommonResource.TableName;
			this.StatusColumn.HeaderText = CommonResource.Status;
			this.tsbCompleted.Text = CommonResource.Completed;
			this.tsbNotCompleted.Text = CommonResource.NotCompleted;
			this.tsbRunning.Text = CommonResource.Running;
			this.groupBox2.Text = CommonResource.Logs;
			this.tsMenu.Text = CommonResource.ExpandQueryResultsPanel;
			this.tsbRun.Text = CommonResource.Run;
			this.tsbStop.Text = CommonResource.Stop;
		}
		#endregion

		#region Override Methods
		public override void InitForm()
		{
			ucDBObjectSelectControl.InitForm();
			((frmMain)MainForm).Resize += frmMain_Resize;
			frmMain_Resize(this, EventArgs.Empty);
		}
		#endregion
	}
}
