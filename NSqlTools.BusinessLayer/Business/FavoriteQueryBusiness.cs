using NSqlTools.Types;
using NSqlTools.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NSqlTools.BusinessLayer
{
    public class FavoriteQueryBusiness
    {
        public List<FavoriteQueryContract> GetAll()
        {
            if (!File.Exists(Constants.FavoriteQueriesFileName))
                return new List<FavoriteQueryContract>();
            
			List<FavoriteQueryContract> favoriteQueryContractList = SerializeHelper.DeserializeFromXml<List<FavoriteQueryContract>>(Constants.FavoriteQueriesFileName);

			return favoriteQueryContractList ?? new List<FavoriteQueryContract>();
        }

		public FavoriteQueryContract GetByUniqueId(String uniqueId)
		{
			List<FavoriteQueryContract> FavoriteQueryContractList = GetAll();
			
			return FavoriteQueryContractList.FirstOrDefault(f => f.UniqueId == uniqueId);
		}

		public void SaveAll(List<FavoriteQueryContract> queries)
        {
			SerializeHelper.SerializeToXml(queries, Constants.FavoriteQueriesFileName);
        }

        public void Add(FavoriteQueryContract query)
        {
            var all = GetAll();
            all.Add(query);
            SaveAll(all);
        }

        public void Update(FavoriteQueryContract query)
        {
            var all = GetAll();
            var idx = all.FindIndex(q => q.Name == query.Name);
            if (idx >= 0)
                all[idx] = query;
            SaveAll(all);
        }

        public void Delete(string name)
        {
            var all = GetAll();
            all.RemoveAll(q => q.Name == name);
            SaveAll(all);
        }
    }
}
