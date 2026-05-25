using System;
using System.Collections.Generic;
using System.Data;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class InsertScriptGeneratorScreenDataContract : DBObjectSelectScreenDataContract
	{
		public InsertScriptGeneratorScreenDataContract() { }
		
		public InsertScriptGeneratorScreenDataContract(String name = null) : base(name) { }
        
		public String InputSqlScript { get; set; }

        public String OutputSqlScript { get; set; }
        
		public Boolean UseNoSquareBrackets { get; set; }

		public Boolean SeperateInsertScripts { get; set; }
        
		public DataTable ScriptResultDataSource { get; set; }

		public List<ColumnContract> Columns { get; set; }
	}
}