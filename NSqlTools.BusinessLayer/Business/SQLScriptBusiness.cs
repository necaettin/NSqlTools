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
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

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
					using (SqlCommand cmd = new SqlCommand(sqlQueryRun, con))
					{
						cmd.CommandType = CommandType.Text;
						cmd.CommandTimeout = 300; // 300 saniye timeout
						cmd.StatementCompleted += (sender, e) =>
						{
							result.AffectedRowsMessages.AppendLine(String.Format(CommonResource._0RowSAffected, e.RecordCount));
						};

						// CancellationToken ile SqlCommand.Cancel() çağrısını bağla
						using (cancellationToken.Register(() => cmd.Cancel()))
						{
							using (SqlDataAdapter da = new SqlDataAdapter(cmd))
							{
								da.Fill(dataSet);
							}
						}
					}
				}
			}
			catch(SqlException ex)
			{
				throw;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileRunningSqlQuery, ex);
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
