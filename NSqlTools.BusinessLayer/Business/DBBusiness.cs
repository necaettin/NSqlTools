using NSqlTools.Types;
using NSqlTools.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static NSqlTools.Types.Enums;
using NSqlTools.Types.Properties;
using System.Linq;
using NSqlTools.Lib;

namespace NSqlTools.BusinessLayer
{
	public class DBBusiness
	{
		#region Public Methods
		public List<DBContract> GetDBList(ConnectionStringContract connectionStringContract, ref String defaultDBName)
		{
			String connectionString = connectionStringContract.ConnectionString;
			String connectionStringName = connectionStringContract.Name; 
			List<DBContract> list = new List<DBContract>();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					try
					{
						con.Open();
					}
					catch (Exception ex) {
						LogHelper.Error(ex);
						throw new Exception(String.Format(CommonResource.ErrorOccuredWhileConnectingToX, connectionStringName), ex);
					}

					defaultDBName = con.Database;

					// Get from cache
					var listCache = GetDBCache(connectionStringName);
					if (listCache != null)
						return listCache;

					// Sql Command
					SqlCommand cmd = new SqlCommand("" +
						"SELECT " +
						"	name AS Name, database_id AS DatabaseId " +
						"FROM " +
						"	sys.databases " +
						"WHERE " +
						"	database_id > 5 ORDER BY name", con);

					// Execute
					SqlDataReader dr = cmd.ExecuteReader();
					Int32 orderNo = 1000;
					while (dr.Read())
					{
						list.Add(new DBContract(dr["Name"].ToString(), Convert.ToInt32(dr["DatabaseId"].ToString())));

						var databaseOrder = connectionStringContract.DatabaseOrderList?.IndexOfInvariant(dr["Name"].ToString(), true);
						if (databaseOrder.HasValue && databaseOrder > -1)
						{
							list[list.Count - 1].OrderNo = databaseOrder.Value;
						}
						else
						{
							list[list.Count - 1].OrderNo = orderNo++;
						}
					}

					//// Add to cache
					AddDBCache(list.OrderBy(l => l.OrderNo).ToList(), connectionStringName);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingDBList, ex);
			}

			return list.OrderBy(l => l.OrderNo).ToList();
		}
		#endregion

		#region DB Cache Methods
		public List<DBContract> GetDBCache(String connectionStringName)
		{
			List<DBContract> result = null;

			String key = $"{nameof(CacheTypeEnum.DB)}_{connectionStringName}";
			List<DBContract> list = MemoryCacheHelper.Get<List<DBContract>>(key);
			if(list != null)
			{
				result = new List<DBContract>();
                foreach (var item in list)
					result.Add(new DBContract(item.Name, item.DatabaseId));
            }

			return result;
		}

		public void AddDBCache(List<DBContract> dbContractList, String connectionStringName)
		{
			String key = $"{nameof(CacheTypeEnum.DB)}_{connectionStringName}";

			MemoryCacheHelper.Add(key, dbContractList, TimeSpan.FromMinutes(Constants.CacheDuration));
		}
		#endregion
	}
}
