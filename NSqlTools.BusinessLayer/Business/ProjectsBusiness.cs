using NSqlTools.Types;
using NSqlTools.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NSqlTools.BusinessLayer
{
    public class ProjectsBusiness
	{
        public List<ProjectContract> GetAll()
        {
            if (!File.Exists(Constants.ProjectsFileName))
                return new List<ProjectContract>();
            
			List<ProjectContract> ProjectsContractList = SerializeHelper.DeserializeFromXml<List<ProjectContract>>(Constants.ProjectsFileName);

			return ProjectsContractList ?? new List<ProjectContract>();
        }

		public void SaveAll(List<ProjectContract> queries)
        {
			SerializeHelper.SerializeToXml(queries, Constants.ProjectsFileName);
        }

        public void Add(ProjectContract query)
        {
            var all = GetAll();
            all.Add(query);
            SaveAll(all);
        }

        public void Update(ProjectContract query)
        {
            var all = GetAll();
            var idx = all.FindIndex(q => q.UniqueId == query.UniqueId);
            if (idx >= 0)
                all[idx] = query;
            SaveAll(all);
        }

        public void Delete(String uniqueId)
        {
            var all = GetAll();
            all.RemoveAll(q => q.UniqueId == uniqueId);
            SaveAll(all);
        }
    }
}
