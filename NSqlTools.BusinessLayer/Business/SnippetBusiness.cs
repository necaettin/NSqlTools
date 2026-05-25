using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using NSqlTools.Types.IntellisenseContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NSqlTools.BusinessLayer
{
    public class SnippetsBusiness
	{
		public SnippetContract FindByShortcut(string shortcut)
		{
			if (string.IsNullOrWhiteSpace(shortcut)) 
				return null;
			List<SnippetContract> snippets = GetAll(true);

			return snippets.FirstOrDefault(
				s => string.Equals(s.Shortcut, shortcut, StringComparison.OrdinalIgnoreCase));
		}

		public List<SnippetContract> GetAll(Boolean fromCache = false)
        {
            if (!File.Exists(Constants.SnippetsFileName))
                return new List<SnippetContract>();

			List<SnippetContract> SnippetsContractList;
			if (fromCache)
			{
				SnippetsContractList = GetSnippetsFromCache();
				if(SnippetsContractList != null)
					return SnippetsContractList;	
			}

			SnippetsContractList = SerializeHelper.DeserializeFromXml<List<SnippetContract>>(Constants.SnippetsFileName);

			return SnippetsContractList;
        }

		public void SaveAll(List<SnippetContract> snippets)
        {
			SerializeHelper.SerializeToXml(snippets, Constants.SnippetsFileName);
			AddSnippetsToCache(snippets);
		}

        public void Add(SnippetContract snippetContract)
        {
            var all = GetAll();
            all.Add(snippetContract);
            SaveAll(all);
        }

        public void Update(SnippetContract snippetContract)
        {
            var all = GetAll();
            var idx = all.FindIndex(q => q.UniqueId == snippetContract.UniqueId);
            if (idx >= 0)
                all[idx] = snippetContract;
            SaveAll(all);
        }

        public void Delete(String uniqueId)
        {
            var all = GetAll();
            all.RemoveAll(q => q.UniqueId == uniqueId);
            SaveAll(all);
        }

		#region Cache Methods
		public List<SnippetContract> GetSnippetsFromCache()
		{
			List<SnippetContract> result = null;

			String key = "Snippets_Cache";
			List<SnippetContract> list = MemoryCacheHelper.Get<List<SnippetContract>>(key);
			if (list != null)
			{
				result = new List<SnippetContract>();
				foreach (var item in list)
					result.Add(new SnippetContract() { UniqueId = item.UniqueId, Description = item.Description, Shortcut = item.Shortcut, Expansion = item.Expansion });
			}

			return result;
		}

		public void AddSnippetsToCache(List<SnippetContract> SnippetContractList)
		{
			String key = "Snippets_Cache";

			MemoryCacheHelper.Add(key, SnippetContractList, TimeSpan.FromMinutes(Constants.CacheDuration));
		}
		#endregion
	}
}
