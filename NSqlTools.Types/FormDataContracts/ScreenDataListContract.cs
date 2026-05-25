using NSqlTools.Types.BaseTypes;
using System;
using System.Collections.Generic;

namespace NSqlTools.Types.FormDataContracts
{
	[Serializable]
	public class ScreenDataListContract
	{
		public List<BaseScreenDataContract> BaseScreenDataContractList { get; set; }

		public ScreenDataListContract()
		{
			BaseScreenDataContractList = new List<BaseScreenDataContract>();
		}
	}
}
