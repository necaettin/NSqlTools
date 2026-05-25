using System;

namespace NSqlTools.Types.Contracts
{
	public class TFSChangesetContract
	{
		public int ChangesetId { get; set; }
		public int? TestChangesetId { get; set; }
		public int? MainChangesetId { get; set; }
		public string Comment { get; set; }
		public string Owner { get; set; }
		public String TestMergeUser { get; set; }
		public String MainMergeUser { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? TestMergeDate { get; set; }
		public DateTime? MainMergeDate { get; set; }
		public bool MergedToTest { get; set; }
		public bool MergedToMain { get; set; }
		public string Branch { get; set; }
		public string Solutions { get; set; }
	}
}
