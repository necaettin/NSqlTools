using System;

namespace NSqlTools.Types.FormDataContracts
{
	[Serializable]
	public class SqlViewerScreenDataContract : DBObjectSelectScreenDataContract
	{
		public SqlViewerScreenDataContract() { }
		
		public SqlViewerScreenDataContract(String name = null) : base(name) { }
	}
}
