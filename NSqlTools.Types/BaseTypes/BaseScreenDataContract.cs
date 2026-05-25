using System;

namespace NSqlTools.Types.BaseTypes
{
	public class BaseScreenDataContract
	{
		public String Name { get; set; }

		public String Description { get; set; }

		public BaseScreenDataContract() {  }
		
		public BaseScreenDataContract(String name = null)
		{
			if(name != null)
				this.Name = name;
		}
	}
}
