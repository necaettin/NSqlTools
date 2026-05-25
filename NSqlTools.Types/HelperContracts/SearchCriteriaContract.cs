using System;

namespace NSqlTools.Types.HelperContracts
{
	public class SearchCriteriaContract
	{
		#region Properties
		public String SearchKeyword { get; set; }

		public Boolean CaseSensitive { get; set; }
		#endregion
	}
}
