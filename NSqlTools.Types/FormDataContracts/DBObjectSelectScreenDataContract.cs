using NSqlTools.Types.BaseTypes;
using System;
using System.Collections.Generic;

namespace NSqlTools.Types.FormDataContracts
{
	[Serializable]
	public class DBObjectSelectScreenDataContract : BaseScreenDataContract
	{
		public DBObjectSelectScreenDataContract() { }
		
		public DBObjectSelectScreenDataContract(String name = null) : base(name) { }

		public String DataSourceName { get; set; }

		public List<Int32> DBIndexes { get; set; }

		public Int32? ObjectType { get; set; }

		public Int32? SchemaId { get; set; }

		public Int32? ObjectId { get; set; }
	}
}
