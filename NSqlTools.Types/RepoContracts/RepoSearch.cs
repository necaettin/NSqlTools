using System.Collections.Generic;

namespace NSqlTools.Types.RepoContracts
{
	#region Request
	public class RepoSearchRequest
	{
		public string searchText { get; set; }
		public int skipResults { get; set; }
		public int takeResults { get; set; }
		public RepoSearchRequestFilter[] filters { get; set; }
		public RepoSearchRequestSearchFilters searchFilters { get; set; }
		public object[] sortOptions { get; set; }
		public bool summarizedHitCountsNeeded { get; set; }
		public bool includeSuggestions { get; set; }
		public bool isInstantSearch { get; set; }
	}

	public class RepoSearchRequestFilter
	{
		public string name { get; set; }
		public string[] values { get; set; }
	}

	public class RepoSearchRequestSearchFilters
	{
		public string[] ProjectFilters { get; set; }
		public string[] RepositoryFilters { get; set; }
		public string[] PathFilters { get; set; }
	}
	#endregion
	#region Response
	public class RepoSearchResponse
	{
		public RepoSearchResults results { get; set; }
		public List<object> errors { get; set; }
		public object suggestions { get; set; }
	}

	public class RepoSearchResults
	{
		public int count { get; set; }
		public List<RepoSearchResultValue> values { get; set; }
	}

	public class RepoSearchResultValue
	{
		public string fileName { get; set; }
		public string path { get; set; }
		public int hitCount { get; set; }
		public List<RepoSearchResultHit> hits { get; set; }
		public RepoSearchResultMatch matches { get; set; }
		public string collection { get; set; }
		public string project { get; set; }
		public object projectId { get; set; }
		public string repository { get; set; }
		public object repositoryId { get; set; }
		public string branch { get; set; }
		public List<RepoSearchVersion> versions { get; set; }
		public string changeId { get; set; }
		public string contentId { get; set; }
		public string vcType { get; set; }
	}

	public class RepoSearchVersion
	{
		public string branchName { get; set; }
		public string changeId { get; set; }
	}

	public class RepoSearchResultHit
	{
		public int charOffset { get; set; }
		public int length { get; set; }
		public int line { get; set; }
		public int column { get; set; }
		public object codeSnippet { get; set; }
		public string type { get; set; }
	}

	public class RepoSearchResultMatch
	{
		public List<RepoSearchResultHit> content { get; set; }
		public List<object> fileName { get; set; }
	}
	#endregion
}
