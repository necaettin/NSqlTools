using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class RunQueryScreenDataContract : DBObjectSelectScreenDataContract
	{
		public RunQueryScreenDataContract() { }

		public RunQueryScreenDataContract(String name = null) : base(name) { }

		public String QueryText { get; set; }
    }
}