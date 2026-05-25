using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSqlTools.Types.Contracts
{
	public class SearchParameters
	{
		public string TfsUrl { get; set; }
		public string BasePath { get; set; }
		public string CommentFilter { get; set; }
		public string OwnerFilter { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int? ChangesetId { get; set; }
		public bool ShowOnlyUnmergedToTest { get; set; }
		public bool ShowOnlyUnmergedToMain { get; set; }
	}
}
