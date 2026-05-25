using System;
using System.Collections.Generic;

namespace NSqlTools.Types.IntellisenseContracts
{
	public class IntellisenseTableContract
	{
		public String SchemaName { get; set; }
		public String TableName { get; set; }

		public String DBName { get; set; }
		public List<IntellisenseColumnContract> ColumnList { get; set; }
	}
}
