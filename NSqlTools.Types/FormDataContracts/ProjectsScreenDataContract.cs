using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class ProjectsScreenDataContract : BaseScreenDataContract
	{
		public ProjectsScreenDataContract() { }
		
		public ProjectsScreenDataContract(String name = null) : base(name) { }
	}
}