using NSqlTools.Lib;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using static NSqlTools.Types.Enums;

namespace NSqlTools.BusinessLayer
{
	public class DBObjectBusiness
	{
		#region Methods
		public List<DBObjectContract> GetDBObjectListByDBSchemaAndObjectType(String connectionString, String dbName, Int32 schemaId, String objectType, String objectType2 = null)
		{
			List<DBObjectContract> list = new List<DBObjectContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	o.[name] AS Name, o.Type AS ObjectType, o.schema_id, " +
						$"	s.name as SchemaName, o.object_id AS ObjectId " +
						$"FROM " +
						$"	Sys.Objects o " +
						$"	JOIN sys.schemas s ON s.schema_id = o.schema_id " +
						$"WHERE " +
						$"	(" +
						$"		(@type1 IS NULL AND @type2 IS NULL) " +
						$"		OR (@type1 IS NOT NULL AND @type2 IS NULL AND O.type IN (@type1)) " +
						$"		OR (@type1 IS NULL AND @type2 IS NOT NULL AND O.type IN (@type2)) " +
						$"		OR (@type1 IS NOT NULL AND @type2 IS NOT NULL AND O.type IN (@type1, @type2)) " +
						$"	)" +
						$"	AND o.schema_id = @schema_id " +
						$"ORDER BY " +
						$"	o.name", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@schema_id", SqlDbType.Int) { Value = schemaId });
					cmd.Parameters.Add(objectType == null
						? new SqlParameter("@type1", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@type1", SqlDbType.VarChar) { Value = objectType });

                    cmd.Parameters.Add(objectType2 == null
                        ? new SqlParameter("@type2", SqlDbType.VarChar) { Value = DBNull.Value }
                        : new SqlParameter("@type2", SqlDbType.VarChar) { Value = objectType2 });

                    // Execute
                    SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						if (!Enum.TryParse(dr["ObjectType"].ToString(), out ObjectTypeEnum objectTypeEnumValue))
						{
							objectTypeEnumValue = ObjectTypeEnum.UNDEFINED;
						}
						String objectTypeName = EnumHelper.GetEnumDescription(objectTypeEnumValue);

						list.Add(new DBObjectContract(
							Convert.ToInt32(dr["ObjectId"]),
							objectTypeEnumValue,
							objectTypeName,
							dr["Name"].ToString(),
							dr["SchemaName"].ToString(), 
							dbName));
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingObjectList, ex);
			}

			return list;
		}

		public List<DBObjectContract> GetDBObjectListWithDefinitionByDBSchemaAndObjectType(String connectionString, String dbName, Int32 schemaId, String objectType, String nameFilter = null, CancellationToken cancellationToken = default)
		{
			List<DBObjectContract> list = new List<DBObjectContract>();
			try
			{
				String collation = Constants.CaseInsensitiveCollation;
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	o.object_id, o.schema_id, s.name as SchemaName, " +
						$"	o.name, m.definition " +
						$"FROM " +
						$"	sys.sql_modules m " +
						$"	JOIN sys.Objects o ON m.object_id = o.object_id " +
						$"	JOIN sys.schemas s on s.schema_id = o.schema_id " +
						$"WHERE " +
						$"	o.type = @object_type " +
						$"	AND o.schema_id = @schema_id " +
						$"	AND (@name_filter IS NULL OR o.name collate {collation} LIKE '%' + @name_filter collate {collation} + '%') " +
						$"ORDER BY " +
						$"	name", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@object_type", SqlDbType.VarChar) { Value = objectType });
					cmd.Parameters.Add(new SqlParameter("@schema_id", SqlDbType.Int) { Value = schemaId });
					cmd.Parameters.Add(String.IsNullOrWhiteSpace(nameFilter)
						? new SqlParameter("@name_filter", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@name_filter", SqlDbType.VarChar) { Value = nameFilter });

					// Execute with cancellation support
					using (cancellationToken.Register(() => cmd.Cancel()))
					{
						SqlDataReader dr = cmd.ExecuteReader();
						while (dr.Read())
						{
							list.Add(new DBObjectContract()
							{
								ObjectId = Convert.ToInt32(dr["object_id"]),
								SchemaId = Convert.ToInt32(dr["schema_id"]),
								SchemaName = dr["SchemaName"].ToString(),
								Name = dr["name"].ToString(),
								Definition = dr["definition"].ToString()
							});
						}
					}
				}
			}
			catch (SqlException ex) when (ex.Number == -2 || ex.Message.Contains("severe error") || ex.Message.Contains("current command"))
			{
				LogHelper.Info($"GetDBObjectListWithDefinitionByDBSchemaAndObjectType cancelled: {ex.Message}");
				return list;
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingObjectList, ex);
			}

			return list;
		}

		public DBObjectContract GetDBObjectByDBObject(String connectionString, String dbName, DBObjectContract dbObjectContract)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand($"" +
						$"USE {dbName}; " +
						$"SELECT " +
						$"	TOP 1 definition " +
						$"FROM " +
						$"	sys.sql_modules " +
						$"WHERE " +
						$"	object_id = @object_id", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@object_id", SqlDbType.Int) { Value = dbObjectContract.ObjectId });

					// Execute
					String objectContent = cmd.ExecuteScalar().ToString();
					dbObjectContract.Definition = objectContent;
					dbObjectContract.ConnectionString = connectionString;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingDbObject, ex);
			}

			return dbObjectContract;
		}

		public List<DBObjectContract> GetTableDBObjectListByDBSchemaId(
			String connectionString, 
			String dbName, 
			Int32? schemaId, 
			String searchKeyword = null, 
			String nameFilter = null, 
			Boolean caseSensitive = false,
			CancellationToken cancellationToken = default,
			Action<SqlCommand> commandCallback = null)
		{
			List<DBObjectContract> list = new List<DBObjectContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					String collation = caseSensitive ? Constants.CaseSensitiveCollation : Constants.CaseInsensitiveCollation;

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	t.object_id as TableObjectId, t.type AS ObjectType, t.name as TableName, " +
						$"	s.name as SchemaName, c.column_id, c.name as column_name, " +
						$"	c.user_type_id, c.system_type_id, c.max_length, " +
						$"	c.precision, c.is_identity, c.is_nullable, ty.name as ColumnType," +
						$"	(SELECT COUNT(1) FROM sys.columns csk WHERE csk.object_id = t.object_id AND csk.name COLLATE {collation} LIKE '%' + @search_keyword collate {collation} + '%') AS HitCount " +
						$"FROM " +
						$"	sys.tables t " +
						$"	JOIN sys.schemas s ON s.schema_id = t.schema_id " +
						$"	JOIN sys.columns c ON c.object_id = t.object_id " +
						$"	JOIN sys.types ty ON ty.system_type_id = c.system_type_id AND ty.user_type_id = c.user_type_id " +
						$"WHERE " +
						$"	(@schema_id IS NULL OR t.schema_id = @schema_id) " +
						$"	AND (@search_keyword IS NULL OR (EXISTS(SELECT TOP 1 1 FROM sys.columns csk WHERE csk.object_id = t.object_id AND csk.name COLLATE {collation} LIKE '%' + @search_keyword collate {collation} + '%'))) " +
						$"	AND (@name_filter IS NULL OR t.name COLLATE {collation} LIKE '%' + @name_filter collate {collation} + '%') " +
						$"ORDER BY " +
						$"	t.name", con);

					// Parameters
					cmd.Parameters.Add(schemaId == null
						? new SqlParameter("@schema_id", SqlDbType.Int) { Value = DBNull.Value }
						: new SqlParameter("@schema_id", SqlDbType.Int) { Value = schemaId.Value });

					cmd.Parameters.Add(String.IsNullOrWhiteSpace(searchKeyword)
						? new SqlParameter("@search_keyword", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@search_keyword", SqlDbType.VarChar) { Value = searchKeyword });

					cmd.Parameters.Add(String.IsNullOrWhiteSpace(nameFilter)
						? new SqlParameter("@name_filter", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@name_filter", SqlDbType.VarChar) { Value = nameFilter });

					// Register command for cancellation
					commandCallback?.Invoke(cmd);

					// Register cancellation
					using (cancellationToken.Register(() => cmd.Cancel()))
					{
						// Execute
						SqlDataReader dr = cmd.ExecuteReader();
						List<ColumnContract> columns = new List<ColumnContract>();
						while (dr.Read())
						{
							columns.Add(new ColumnContract()
							{
								TableObjectId = Convert.ToInt32(dr["TableObjectId"]),
								TableName = dr["TableName"].ToString(),
								SchemaName = dr["SchemaName"].ToString(),

								ColumnId = Convert.ToInt32(dr["column_id"]),
								Name = dr["column_name"].ToString(),
								TypeName = dr["ColumnType"].ToString(),
								UserTypeId = Convert.ToInt32(dr["user_type_id"]),
								SystemTypeId = Convert.ToInt32(dr["system_type_id"]),
								MaxLength = Convert.ToInt32(dr["max_length"]),
								Precision = Convert.ToInt32(dr["precision"]),
								IsIdentity = Convert.ToInt32(dr["is_identity"]) == 1,
								IsNullable = Convert.ToInt32(dr["is_nullable"]) == 1,
								HitCount = Convert.ToInt32(dr["HitCount"])
							});
						}

						if (columns.Count == 0)
							return list;

						String tableObjectTypeName = EnumHelper.GetEnumDescription(ObjectTypeEnum.U);
						columns.GroupBy(c => new { TableObjectId = c.TableObjectId, TableName = c.TableName, SchemaName = c.SchemaName, HitCount = c.HitCount }).ToList().ForEach(g =>
						{
							DBObjectContract dbTableObjectContract = new DBObjectContract(
								g.Key.TableObjectId,
								ObjectTypeEnum.U,
								tableObjectTypeName,
								g.Key.TableName,
								g.Key.SchemaName,
								dbName, 
								g.Key.HitCount);
							list.Add(dbTableObjectContract);
							dbTableObjectContract.ColumnList = new List<ColumnContract>(
								columns.Where(c => c.TableObjectId == g.Key.TableObjectId).ToList()
							);
						});
					}
				}
			}
			catch (SqlException ex)
			{
				// SqlCommand.Cancel() SqlException fırlatır - bunu ignore et
				if (ex.Number == -2 || ex.Message.Contains("severe error") || ex.Message.Contains("current command"))
				{
					// This is a cancellation, not a real error
					LogHelper.Info("Search cancelled by user.");
					return list; // Return partial results
				}

				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingObjectList, ex);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingObjectList, ex);
			}

			return list;
		}

		public List<DBObjectContract> SearchDBObject(
			String connectionString, 
			String dbName, 
			Int32? schemaId, 
			String objectType, 
			String searchKeyword, 
			String nameFilter, 
			Boolean caseSensitive,
			CancellationToken cancellationToken = default,
			Action<SqlCommand> commandCallback = null)
		{
			List<DBObjectContract> list = new List<DBObjectContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					String collation = caseSensitive ? Constants.CaseSensitiveCollation : Constants.CaseInsensitiveCollation;

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	o.object_id, o.schema_id, s.name as SchemaName, " +
						$"	o.name, o.type, m.definition " +
						$"FROM " +
						$"	sys.sql_modules m " +
						$"	JOIN sys.objects o ON m.object_id = o.object_id " +
						$"	JOIN sys.schemas s ON s.schema_id = o.schema_id " +
						$"WHERE " +
						$"	(@type IS NULL OR o.type=@type) " +
						$"	AND (@schema_id IS NULL OR o.schema_id = @schema_id) " +
						$"	AND (@search_keyword IS NULL OR m.definition collate {collation} LIKE '%' + @search_keyword collate {collation} + '%') " +
						$"	AND (@name IS NULL OR o.name collate {collation} LIKE '%' + @name collate {collation} + '%') " +
						$"ORDER BY " +
						$"	o.name", con);

					// Parameters
					cmd.Parameters.Add(objectType == null
						? new SqlParameter("@type", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@type", SqlDbType.VarChar) { Value = objectType });

					cmd.Parameters.Add(schemaId == null
						? new SqlParameter("@schema_id", SqlDbType.Int) { Value = DBNull.Value }
						: new SqlParameter("@schema_id", SqlDbType.Int) { Value = schemaId.Value });

					cmd.Parameters.Add(String.IsNullOrWhiteSpace(searchKeyword)
						? new SqlParameter("@search_keyword", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@search_keyword", SqlDbType.VarChar) { Value = searchKeyword });

					cmd.Parameters.Add(String.IsNullOrWhiteSpace(nameFilter)
						? new SqlParameter("@name", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@name", SqlDbType.VarChar) { Value = nameFilter });

					// Register command for cancellation
					commandCallback?.Invoke(cmd);

					// Register cancellation
					using (cancellationToken.Register(() => cmd.Cancel()))
					{
						// Execute
						SqlDataReader dr = cmd.ExecuteReader();
						while (dr.Read())
						{
							ObjectTypeEnum objectTypeValue = EnumHelper.StringToEnum(dr["type"].ToString());
							list.Add(new DBObjectContract()
							{
								DBName = dbName,
								ObjectId = Convert.ToInt32(dr["object_id"]),
								SchemaId = Convert.ToInt32(dr["schema_id"]),
								SchemaName = dr["SchemaName"].ToString(),
								Name = dr["name"].ToString(),
								Definition = dr["definition"].ToString(),
								HitCount = countOccurrences(dr["definition"].ToString(), searchKeyword, caseSensitive),
								ObjectType = objectTypeValue,
								ObjectTypeName = EnumHelper.GetEnumDescription(objectTypeValue)
							});
						}
					}
				}
			}
			catch (SqlException ex)
			{
				// SqlCommand.Cancel() SqlException fırlatır - bunu ignore et
				if (ex.Number == -2 || ex.Message.Contains("severe error") || ex.Message.Contains("current command"))
				{
					// This is a cancellation, not a real error
					LogHelper.Info("Search cancelled by user.");
					return list; // Return partial results
				}

				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileSearchingDB, ex);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileSearchingDB, ex);
			}

			return list;
		}
		#endregion

		#region Private Methods
		public Int32 countOccurrences(String input, String valueToFind, Boolean caseSensitive)
		{
			if (String.IsNullOrEmpty(input) || String.IsNullOrEmpty(valueToFind))
			{
				return 0;
			}

			Int32 count = 0;
			Int32 index = 0;
			StringComparison comparisonType = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

			while ((index = input.IndexOf(valueToFind, index, comparisonType)) != -1)
			{
				count++;
				index += valueToFind.Length;
			}

			return count;
		}
		#endregion
	}
}
