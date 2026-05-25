using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class FavoriteQueryScreenDataContract : BaseScreenDataContract
	{
		public FavoriteQueryScreenDataContract() { }

		public FavoriteQueryScreenDataContract(String name = null) : base(name) { }
	}
}