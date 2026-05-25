using System;
using System.Collections.Generic;

namespace NSqlTools.Types.RepoContracts
{
	public class DepotResponse
	{
		public List<DepotValue> value { get; set; }
		public int count { get; set; }
	}

	public class DepotValue
	{
		public int version { get; set; }
		public DateTime changeDate { get; set; }
		public int encoding { get; set; }
		public string path { get; set; }
		public bool isFolder { get; set; }
		public string url { get; set; }
	}
}
