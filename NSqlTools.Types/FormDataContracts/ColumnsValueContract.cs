using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class ColumnsValueContract
	{
		public String ColumnName { get; set; }

		public Boolean IsSelected { get; set; }

		public String DefaultValue { get; set; }
	}
}