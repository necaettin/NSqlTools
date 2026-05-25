using NSqlTools.Types.BaseTypes;
using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types.FormDataContracts
{
	[Serializable]
	public class TfsChangesetSearchScreenDataContract : BaseScreenDataContract
	{
		public TfsChangesetSearchScreenDataContract() { }
		public TfsChangesetSearchScreenDataContract(String name = null) : base(name) { }

		public String TFSUrl { get; set; }
		public String TFSPath { get; set; }
		public String CommentFilter { get; set; }
		public String OwnerFilter { get; set; }
		public Int32? ChangesetId { get; set; }
		public Boolean ShowOnlyUnmergedToTest { get; set; }
		public Boolean ShowOnlyUnmergedToMain { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
