using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class DBBatchCompareScreenDataContract : BaseScreenDataContract
	{
		public DBBatchCompareScreenDataContract() { }

		public DBBatchCompareScreenDataContract(String name = null) : base(name)
		{
			SourceDBObjectSelectFormDataContract = new DBObjectSelectScreenDataContract();
			TargetDBObjectSelectFormDataContract = new DBObjectSelectScreenDataContract();
		}

		public int? ObjectTypeOriginal{ get; set; }

		public String NameFilter { get; set; }

		public DBObjectSelectScreenDataContract SourceDBObjectSelectFormDataContract { get; set; }
	
		public DBObjectSelectScreenDataContract TargetDBObjectSelectFormDataContract { get; set; }
	}
}