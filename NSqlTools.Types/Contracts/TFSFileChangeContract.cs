namespace NSqlTools.Types.Contracts
{
	public class TFSFileChangeContract
	{
		public string FileName { get; set; }
		public string ServerPath { get; set; }
		public string ChangeType { get; set; }
		public int ItemChangesetId { get; set; }
		public bool IsAdd { get; set; }
		public bool IsDelete { get; set; }
		public string OldContent { get; set; }
		public string NewContent { get; set; }
	}
}
