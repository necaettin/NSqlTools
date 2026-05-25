using NSqlTools.Types.BaseTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class DataCompareScreenDataContract : BaseScreenDataContract
	{
		public DataCompareScreenDataContract() { }

		public DataCompareScreenDataContract(String name = null) : base(name)
		{
			SourceDBObjectSelectFormDataContract = new DBObjectSelectScreenDataContract();
			TargetDBObjectSelectFormDataContract = new DBObjectSelectScreenDataContract();
		}

		public List<ColumnContract> ComparisonColumns { get; set; }

		public DataTable CompareResult { get; set; }

		public String InputSqlScriptSource { get; set; }

		public String InputSqlScriptTarget { get; set; }

		public DBObjectSelectScreenDataContract SourceDBObjectSelectFormDataContract { get; set; }
	
		public DBObjectSelectScreenDataContract TargetDBObjectSelectFormDataContract { get; set; }

		public Boolean IsDiffSql { get; set; }
	}
}