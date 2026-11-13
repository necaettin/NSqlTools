using NSqlTools.Lib;
using NSqlTools.Types.HelperContracts;
using NSqlTools.Types.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace NSqlTools.BusinessLayer
{
	public class SQLScriptBusiness
	{
		#region Methods
		public DataTable RunSqlQueryForOneTable(String connectionString, String dbName, String sqlQuery)
		{
			DataTable dataTable = new DataTable();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					SqlCommand cmd = new SqlCommand(String.Format(
						"USE [" + dbName + "]; \n" +
						"{0}", sqlQuery), con);
					cmd.CommandTimeout = 1000;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
					{
						da.Fill(dataTable);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileRunningSqlQuery, ex);
			}

			return dataTable;
		}

		public RunSqlResultContract RunSqlQuery(
			string connectionString, 
			string databaseName, 
			string query, 
			bool parse = false,
			CancellationToken cancellationToken = default)
		{
			RunSqlResultContract result = new RunSqlResultContract();
			DataSet dataSet = new DataSet();
			SqlConnection con = null;
			SqlCommand cmd = null;

			// Büyük veri setleri için DataSet ayarlarını optimize et
			dataSet.EnforceConstraints = false;

			try
			{
				// Connection timeout ayarla
				SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder(connectionString);
				if (connBuilder.ConnectTimeout < 30)
					connBuilder.ConnectTimeout = 30;

				con = new SqlConnection(connBuilder.ConnectionString);

				// PRINT, RAISERROR vb. mesajları yakala
				con.InfoMessage += (sender, e) =>
				{
					foreach (SqlError error in e.Errors)
					{
						result.AffectedRowsMessages.AppendLine(error.Message);
					}
				};

				// Cancellation kontrolü - connection açılmadan önce
				cancellationToken.ThrowIfCancellationRequested();

				con.Open();

				// Cancellation kontrolü - connection açıldıktan sonra
				cancellationToken.ThrowIfCancellationRequested();

				// Prepare Sql Query String
				StringBuilder sb = new StringBuilder();
				sb.AppendLine($"USE [{databaseName}]");
				//sb.AppendLine($"SET NOCOUNT OFF;");
				if(parse)
					sb.AppendLine("SET FMTONLY ON");
				sb.AppendLine(query);
				if (parse)
					sb.AppendLine("SET FMTONLY OFF");

				// Run Sql Query
				String sqlQueryRun = sb.ToString();
				cmd = new SqlCommand(sqlQueryRun, con);
				cmd.CommandTimeout = 1000;
                cmd.CommandType = CommandType.Text;
				// 0 = unlimited timeout (kullanıcı cancel edene kadar çalışır)
				cmd.CommandTimeout = 0;
				cmd.StatementCompleted += (sender, e) =>
				{
					result.AffectedRowsMessages.AppendLine(String.Format(CommonResource._0RowSAffected, e.RecordCount));
				};

				// CancellationToken ile SqlCommand.Cancel() çağrısını bağla
				using (cancellationToken.Register(() => 
				{
					try
					{
						if (cmd != null && con != null && con.State == ConnectionState.Open)
						{
							cmd.Cancel();
						}
					}
					catch
					{
						// Cancel sırasında oluşabilecek hataları yoksay
					}
				}))
				{
					using (SqlDataAdapter da = new SqlDataAdapter(cmd))
					{
						da.Fill(dataSet);
					}
				}
			}
			catch (OperationCanceledException)
			{
				// Cancellation bekleniyordu, yeniden fırlat
				throw;
			}
			catch(SqlException ex)
			{
				// SqlException'ı olduğu gibi fırlat (UI'da detaylı handle edilecek)
				throw;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileRunningSqlQuery, ex);
			}
			finally
			{
				// Kaynakları temizle
				if (cmd != null)
				{
					try { cmd.Dispose(); } catch { }
				}
				if (con != null)
				{
					try 
					{ 
						if (con.State == ConnectionState.Open)
							con.Close();
						con.Dispose(); 
					} 
					catch { }
				}
			}

			result.TableCollection = dataSet.Tables;

			// DEBUG: Log table count
			System.Diagnostics.Debug.WriteLine($"DataSet Table Count: {dataSet.Tables.Count}");
			for (int i = 0; i < dataSet.Tables.Count; i++)
			{
				System.Diagnostics.Debug.WriteLine($"Table {i}: {dataSet.Tables[i].Rows.Count} rows, {dataSet.Tables[i].Columns.Count} columns");
			}

			return result;
		}
		#endregion
	}
}
