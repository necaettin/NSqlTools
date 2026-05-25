using NSqlTools.Types;
using NSqlTools.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static NSqlTools.Types.Enums;
using NSqlTools.Types.Properties;
using NSqlTools.Lib;

namespace NSqlTools.BusinessLayer
{
	public class SchemaBusiness
	{
		#region Public Methods
		public List<SchemaContract> GetSchemaListByDBAndObjectType(String connectionString, String connectionStringName, String dbName, String objectType)
		{
			// Get from cache
			var listCache = GetSchemaCache(connectionStringName, dbName, objectType);
			if (listCache != null)
				return listCache;

			List<SchemaContract> list = new List<SchemaContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					// Sql Command
					SqlCommand cmd = new SqlCommand(
						$"USE {dbName}; " +
						$"SELECT " +
						$"	DISTINCT S.schema_id AS SchemaId, S.name AS Name " +
						$"FROM " +
						$"	SYS.OBJECTS O " +
						$"	JOIN SYS.SCHEMAS S ON S.schema_id = O.schema_id " +
						$"WHERE " +
						$"	(@type IS NULL OR O.type = @type) " +
						$"ORDER BY " +
						$"	name", con);

					// Parameters
					cmd.Parameters.Add(objectType == null
						? new SqlParameter("@type", SqlDbType.VarChar) { Value = DBNull.Value }
						: new SqlParameter("@type", SqlDbType.VarChar) { Value = objectType });

					// Execute
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						list.Add(new SchemaContract(dr["Name"].ToString(), Convert.ToInt32(dr["SchemaId"].ToString())));
					}

					// Add to cache
					AddSchemaCache(list, connectionStringName, dbName, objectType);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingSchemaList, ex);
			}

			return list;
		}
		#endregion

		#region Schema Cache Methods
		public List<SchemaContract> GetSchemaCache(String connectionStringName, String dbName, String objectType)
		{
			if (objectType == null)
				return null;

			List<SchemaContract> result = null;

			String key = $"{nameof(CacheTypeEnum.Schema)}_{connectionStringName}_{dbName}_{objectType}";

			List<SchemaContract> list = MemoryCacheHelper.Get<List<SchemaContract>>(key);

			if (list != null)
			{
				result = new List<SchemaContract>();
				foreach (var item in list)
					result.Add(new SchemaContract(item.Name, item.SchemaId));
			}

			return result;
		}

		public void AddSchemaCache(List<SchemaContract> schemaContractList, String connectionStringName, String dbName, String objectType)
		{
			if (objectType == null)
				return;

			String key = $"{nameof(CacheTypeEnum.Schema)}_{connectionStringName}_{dbName}_{objectType}";

			MemoryCacheHelper.Add(key, schemaContractList, TimeSpan.FromMinutes(Constants.CacheDuration));
		}
		#endregion
	}
}
