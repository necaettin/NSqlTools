using System;

namespace NSqlTools.Types.RepoContracts
{
	#region Request
	public class ContentRequest
	{
		public String path { get; set; }

		public int recursionLevel { get; set; }

		public Boolean includeContent { get; set; }

		public ContentVersionDescriptor versionDescriptor { get; set; }
	}

	public class ContentVersionDescriptor
	{
		public int versionOption { get; set; }

		public string version { get; set; }

		public int versionType { get; set; }

	}
	#endregion

	#region Response
	public class ContentResponse
	{
		public int version { get; set; }
		public DateTime changeDate { get; set; }
		public int size { get; set; }
		public string hashValue { get; set; }
		public int encoding { get; set; }
		public string path { get; set; }
		public string content { get; set; }
		public ContentMetadata contentMetadata { get; set; }
		public string url { get; set; }
		public ContentLinks _links { get; set; }
	}

	public class ContentMetadata
	{
		public int encoding { get; set; }
		public string contentType { get; set; }
		public string fileName { get; set; }
		public string extension { get; set; }
		public string vsLink { get; set; }
	}

	public class ContentLinks
	{
		public ContentLinkSelf self { get; set; }
	}

	public class ContentLinkSelf
	{
		public string href { get; set; }
	}
	#endregion
}
