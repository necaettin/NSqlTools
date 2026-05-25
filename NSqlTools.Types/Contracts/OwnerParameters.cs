using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSqlTools.Types.Contracts
{
	public class OwnerParameters
	{
		public String TfsUrl { get; set; }
		
		public String BasePath { get; set; }
		
		public DateTime? StartDate { get; set; }
		
		public DateTime? EndDate { get; set; }
	}
}
