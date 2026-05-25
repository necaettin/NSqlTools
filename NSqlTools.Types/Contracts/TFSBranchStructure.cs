using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSqlTools.Types.Contracts
{
	public class TFSBranchStructure
	{
		public string CompanyName { get; set; }
		public string DevPath { get; set; }
		public string TestPath { get; set; }
		public string MainPath { get; set; }
	}
}
