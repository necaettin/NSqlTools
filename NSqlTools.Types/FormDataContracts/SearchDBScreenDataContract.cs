using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class SearchDBScreenDataContract : DBObjectSelectScreenDataContract
	{
		public SearchDBScreenDataContract() : base(null) { } 
		
		public SearchDBScreenDataContract(String name = null) : base(name) { } 

		public string SearchKeyword { get; set; }

        public bool CaseSensitive { get; set; }

        public bool DBSearch { get; set; }

        public bool RepoSearch { get; set; }
 
		public string RepoPath { get; set; }
        
		public string RepoExtraSearchKeyword { get; set; }
        
		public int? ObjectTypeOriginal{ get; set; }
        
		public string NameFilter { get; set; }
    }
}	