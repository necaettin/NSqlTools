using NSqlTools.Types.FormDataContracts;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NSqlTools.Types
{
    [Serializable]
    public class ProjectContract
	{
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

		public DateTime? UpdateDate { get; set; }

		public String UniqueId { get; set; }

		[XmlIgnore]
		public ScreenDataListContract ScreenDataListContract { get; set; }

		[XmlIgnore]
		public List<ProjectContract> AllProjectContractList { get; set; }
	}
}
