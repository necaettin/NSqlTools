using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class SnippetScreenDataContract : BaseScreenDataContract
	{
		public SnippetScreenDataContract() { }
		
		public SnippetScreenDataContract(String name = null) : base(name) { }
	}
}