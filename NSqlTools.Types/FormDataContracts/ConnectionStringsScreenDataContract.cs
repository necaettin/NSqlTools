using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class ConnectionStringsScreenDataContract : BaseScreenDataContract
	{
		public ConnectionStringsScreenDataContract() { }

		public ConnectionStringsScreenDataContract(String name) : base(name) { }
	}
}