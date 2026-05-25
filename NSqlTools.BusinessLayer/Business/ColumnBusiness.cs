using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NSqlTools.BusinessLayer
{
	public class ColumnBusiness
	{
		#region Methods
		public DBObjectContract GetColumnListByTableId(String connectionString, String dbName, DBObjectContract dbObjectContract)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	c.column_id, c.name, c.system_type_id, c.user_type_id, c.max_length, " +
						$"	c.precision, c.is_nullable, c.is_identity, t.name as type_name " +
						$"FROM " +
						$"	sys.columns c " +
						$"	join sys.types t on c.user_type_id=t.user_type_id " +
						$"WHERE " +
						$"	object_id = @object_id", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@object_id", SqlDbType.Int) { Value = dbObjectContract.ObjectId });

					// Execute
					var reader = cmd.ExecuteReader();
					dbObjectContract.ColumnList = new List<ColumnContract>();
					dbObjectContract.ConnectionString = connectionString;
					while (reader.Read())
					{
						ColumnContract columnContract = new ColumnContract()
						{
							ColumnId = Convert.ToInt32(reader["column_id"]),
							Name = reader["name"].ToString(),
							SystemTypeId = Convert.ToInt32(reader["system_type_id"]),
							UserTypeId = Convert.ToInt32(reader["user_type_id"]),
							MaxLength = Convert.ToInt32(reader["max_length"]),
							Precision = Convert.ToInt32(reader["precision"]),
							IsNullable = Convert.ToInt32(reader["is_nullable"]) == 1,
							IsIdentity = Convert.ToInt32(reader["is_identity"]) == 1,
							TypeName = reader["type_name"].ToString()
						};
						dbObjectContract.ColumnList.Add(columnContract);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingColumns, ex);
			}

			return dbObjectContract;
		}

		// NEW: Get columns by schema-qualified table name (e.g. [dbo].[Table] or dbo.Table)
		public List<ColumnContract> GetColumnListByTableName(string connectionString, string dbName, string tableNameOrSchemaQualified)
		{
			try
			{
				(string schema, string table) = ParseSchemaAndTable(tableNameOrSchemaQualified);
				List<ColumnContract> list = new List<ColumnContract>();
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; SELECT c.column_id, c.name, c.system_type_id, c.user_type_id, c.max_length, c.precision, c.is_nullable, c.is_identity, t.name AS type_name FROM sys.columns c JOIN sys.types t ON c.user_type_id = t.user_type_id JOIN sys.objects o ON c.object_id = o.object_id JOIN sys.schemas s ON o.schema_id = s.schema_id WHERE o.type='U' AND s.name=@schema AND o.name=@table ORDER BY c.column_id", con);
					cmd.Parameters.Add(new SqlParameter("@schema", SqlDbType.NVarChar, 128) { Value = schema });
					cmd.Parameters.Add(new SqlParameter("@table", SqlDbType.NVarChar, 128) { Value = table });
					var rdr = cmd.ExecuteReader();
					while (rdr.Read())
					{
						list.Add(new ColumnContract
						{
							ColumnId = Convert.ToInt32(rdr["column_id"]),
							Name = rdr["name"].ToString(),
							SystemTypeId = Convert.ToInt32(rdr["system_type_id"]),
							UserTypeId = Convert.ToInt32(rdr["user_type_id"]),
							MaxLength = Convert.ToInt32(rdr["max_length"]),
							Precision = Convert.ToInt32(rdr["precision"]),
							IsNullable = Convert.ToInt32(rdr["is_nullable"]) == 1,
							IsIdentity = Convert.ToInt32(rdr["is_identity"]) == 1,
							TypeName = rdr["type_name"].ToString(),
							TableName = table,
							SchemaName = schema
						});
					}
				}
				return list;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingColumns, ex);
			}
		}

		private (string schema, string table) ParseSchemaAndTable(string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return ("dbo", string.Empty);
			string cleaned = input.Trim();
			// Remove brackets
			cleaned = cleaned.Replace("[", string.Empty).Replace("]", string.Empty);
			var parts = cleaned.Split('.');
			if (parts.Length == 2)
			{
				return (parts[0], parts[1]);
			}
			// If no schema provided assume dbo
			return ("dbo", parts[0]);
		}
		#endregion
	}
}
