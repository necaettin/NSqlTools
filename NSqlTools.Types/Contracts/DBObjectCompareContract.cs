using NSqlTools.Types.BaseTypes;
using System;
using System.Collections.Generic;

namespace NSqlTools.Types.Contracts
{
	public class DBObjectCompareContract : BaseCompareContract
	{
		#region Methods
		public Int32? ObjectIdSource { get; set; }
		public Int32? ObjectIdTarget { get; set; }
		public String DefinitionSource { get; set; }
		public String DefinitionTarget { get; set; }
		public Int32? SchemaIdSource { get; set; }
		public Int32? SchemaIdTarget { get; set; }
		public String SchemaNameSource { get; set; }
		public String SchemaNameTarget { get; set; }
		public String NameSource { get; set; }
		public String NameTarget { get; set; }

		public List<ColumnCompareContract> ColumnCompareSourceList { get; set; }
		public List<ColumnCompareContract> ColumnCompareTargetList { get; set; }
		public List<ColumnContract> ColumnSourceList { get; set; }
		public List<ColumnContract> ColumnTargetList { get; set; }

		public List<ColumnCompareContract> ColumnCompareResultList { get; set; }
		#endregion
	}
}
