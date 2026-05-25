using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class DBObjectCompareScreenDataContract : BaseScreenDataContract
	{
		public DBObjectCompareScreenDataContract() { }
	
		public DBObjectCompareScreenDataContract(String name = null) : base(name)
		{
			SourceDBObjectSelectFormDataContract = new DBObjectSelectScreenDataContract();
			TargetDBObjectSelectFormDataContract = new DBObjectSelectScreenDataContract();
		}

		public int? ObjectTypeOriginal{ get; set; }

		public DBObjectSelectScreenDataContract SourceDBObjectSelectFormDataContract { get; set; }
	
		public DBObjectSelectScreenDataContract TargetDBObjectSelectFormDataContract { get; set; }
	}
}