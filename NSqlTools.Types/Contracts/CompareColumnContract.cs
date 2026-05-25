using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types
{
	[Serializable]
	public class CompareColumnContract : BaseCompareContract
	{
		#region Properties
		public String Name { get; set; }
		#endregion
	}
}
