using System;
using System.Collections.Generic;

namespace NSqlTools.Types.IntellisenseContracts
{
	public class IntellisenseDatabaseContract
	{
        // DbName indicates the name of the database.
		public String DbName { get; set; }
		public List<IntellisenseTableContract> TableList { get; set; }
	}
}
