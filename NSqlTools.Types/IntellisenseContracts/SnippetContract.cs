using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NSqlTools.Types.IntellisenseContracts
{
	public class SnippetContract
	{
		public String UniqueId { get; set; }

		public string Shortcut { get; set; }    // örn: "saf"

		public string Expansion { get; set; }   // örn: "SELECT * FROM "

		public string Description { get; set; } // opsiyonel, UI'de gösterirsin

		[XmlIgnore]
		public List<SnippetContract> AllSnippetContractList { get; set; }
	}
}
