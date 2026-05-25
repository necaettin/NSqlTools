using NSqlTools.Lib;
using NSqlTools.Types;
using NSqlTools.Types.Contracts;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSqlTools.BusinessLayer
{
	public class TableBusiness
	{
		#region Public Methods
		public List<TableIndexContract> GetTableIndexList(String connectionString, String dbName, Int32 objectId)
		{
			List<TableIndexContract> list = new List<TableIndexContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	ind.index_id AS IndexId, " +
						$"	ind.name AS IndexName, " +
						$"	indcols.ColumnNames, " + 
						$"	ind.is_unique AS IsUnique, " +
						$"	ind.is_primary_key AS IsPrimaryKey, " + 
						$"	ind.type_desc AS IndexTypeName " + 
						$"FROM " +
						$"	sys.indexes ind " +
						$"	INNER JOIN sys.tables t ON ind.object_id = t.object_id " +
						$"	CROSS APPLY( " +
						$"		SELECT " + 
						$"			STRING_AGG(col.Name, ', ') AS ColumnNames " + 
						$"		FROM " +
						$"			sys.index_columns ic " +
						$"			JOIN sys.columns col ON ic.object_id = col.object_id AND ic.column_id = col.column_id " +
						$"		WHERE " +
						$"			ind.object_id = ic.object_id " +
						$"			AND ind.index_id = ic.index_id " +
						$"	) indcols " +
						$"WHERE " +
						$"	t.object_id = @objectId", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@objectId", SqlDbType.Int) { Value = objectId });

					// Execute
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						list.Add(new TableIndexContract() { 
							IndexName = dr["IndexName"].ToString(), 
							ColumnNames = dr["ColumnNames"].ToString(), 
							IndexId = Convert.ToInt32(dr["IndexId"]), 
							IndexTypeName = dr["IndexTypeName"].ToString(), 
							IsPrimaryKey = Convert.ToBoolean(dr["IsPrimaryKey"]), 
							IsUnique = Convert.ToBoolean(dr["IsUnique"])
						});
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingTableIndexList, ex);
			}

			return list;
		}

		public List<TableRelationshipContract> GetTableRelationshipList(String connectionString, String dbName, Int32 objectId)
		{
			List<TableRelationshipContract> list = new List<TableRelationshipContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"select " +
						$"	fk.name FKName, " +
						$"	tr.name ReferencedTable, " +
						$"	fkr.ColumnNames, " + 
						$"	fk.type_desc AS RelationshipName " +
						$"FROM " + 
						$"	sys.foreign_keys fk " +
						$"	JOIN sys.tables tp ON fk.parent_object_id = tp.object_id " +
						$"	JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id " +
						$"	CROSS APPLY( " +
						$"		SELECT " +
						$"			STRING_AGG(cp.Name + ' - ' + cr.Name, ',') AS ColumnNames " +
						$"		FROM " +
						$"			sys.foreign_key_columns fkc " +
						$"			JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id " +
						$"			JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id " +
						$"		WHERE " +
						$"			fkc.constraint_object_id = fk.object_id " +
						$"	) fkr " +
						$"WHERE " +
						$"	tp.object_id = @objectId", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@objectId", SqlDbType.Int) { Value = objectId });

					// Execute
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						list.Add(new TableRelationshipContract()
						{
							FKName = dr["FKName"].ToString(),
							ReferencedTable = dr["ReferencedTable"].ToString(),
							RelationshipName = dr["RelationshipName"].ToString(),
							ColumnNames = dr["ColumnNames"].ToString()
						});
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingTableRelationshipList, ex);
			}

			return list;
		}

		public List<TableDependencyContract> GetTableDependencyList(String connectionString, String dbName, Int32 objectId)
		{
			List<TableDependencyContract> list = new List<TableDependencyContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +"" +
						$"SELECT " +
						$"	DISTINCT " +
						$"	o.type_desc AS TypeDescription, " +
						$"	OBJECT_SCHEMA_NAME(d.referencing_id) AS SchemaName, " +
						$"	OBJECT_NAME(d.referencing_id) AS ObjectName, " +
						$"	o.object_id AS ObjectId " +
						$"FROM " +
						$"	sys.sql_expression_dependencies d " +
						$"	JOIN sys.objects o ON d.referencing_id = o.object_id " +
						$"WHERE " +
						$"	d.referenced_id = @objectId", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@objectId", SqlDbType.Int) { Value = objectId });

					// Execute
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						list.Add(new TableDependencyContract()
						{
							TypeDescription = dr["TypeDescription"].ToString(),
							SchemaName = dr["SchemaName"].ToString(),
							ObjectName = dr["ObjectName"].ToString(),
							ObjectId = Convert.ToInt32(dr["ObjectId"].ToString())
						});
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingTableRelationshipList, ex);
			}

			return list;
		}

		public List<TableDependencyContract> GetTableTriggerList(String connectionString, String dbName, Int32 objectId)
		{
			List<TableDependencyContract> list = new List<TableDependencyContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	trg.name AS ObjectName, " +
						$"	trg.object_id AS ObjectId " +
						$"FROM " +
						$"	sys.triggers trg " +
						$"WHERE " +
						$"	trg.parent_id = @objectId", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@objectId", SqlDbType.Int) { Value = objectId });

					// Execute
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						list.Add(new TableDependencyContract()
						{
							ObjectName = dr["ObjectName"].ToString(),
							ObjectId = Convert.ToInt32(dr["ObjectId"]),
						});
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(NSqlTools.Types.Properties.CommonResource.ErrorOccuredWhileGettingTableTriggerList, ex);
			}

			return list;
		}

        public List<TableContract> GetTableList(String connectionString, String dbName, Boolean onlyNotEmptyTables)
		{
			List<TableContract> list = new List<TableContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	tbl.table_name, " +
						$"	tbl.object_id," +
						$"	tbl.row_count " +
						$"FROM " +
						$"	( " +
						$"	SELECT " +
						$"		sOBJ.object_id, " +	
						$"		QUOTENAME(SCHEMA_NAME(sOBJ.schema_id)) + '.' + QUOTENAME(sOBJ.name) AS table_name, " +
						$"		SUM(sPTN.Rows) AS row_count " +
						$"	FROM " +
						$"		sys.objects AS sOBJ " +
						$"		INNER JOIN sys.partitions AS sPTN ON sOBJ.object_id = sPTN.object_id " +
						$"	WHERE " +
						$"		sOBJ.type = 'U' " +
						$"		AND sOBJ.is_ms_shipped = 0x0 " +
						$"		AND index_id < 2 /*0:Heap, 1:Clustered*/ " +
						$"	GROUP BY " +
						$"		sOBJ.object_id, " +
						$"		sOBJ.schema_id, " +
						$"		sOBJ.name " +
						$"	) tbl " +
						$"WHERE " +
						$"	(@only_not_empty_tables = 1 AND row_count > 0) " +
						$"	OR (@only_not_empty_tables = 0) " +
						$"ORDER BY " +
						$"	row_count ASC ", con);

					// Parameters
					cmd.Parameters.Add(new SqlParameter("@only_not_empty_tables", SqlDbType.Int) { Value = onlyNotEmptyTables ? 1: 0 });

					// Execute
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						list.Add(new TableContract(dr["table_name"].ToString(), Convert.ToInt32(dr["object_id"])) { RowCount = Convert.ToInt32(dr["row_count"]) });
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingTableList, ex);
			}

			return list;
		}

		#endregion
	}
}
