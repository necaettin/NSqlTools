using System;

namespace NSqlTools.Types
{
	[Serializable]
	public class TableDependencyContract
	{
		#region Constructors
		public TableDependencyContract(String typeDescription, String schemaName, String objectName) : this()
		{
			TypeDescription = typeDescription;
			SchemaName = schemaName;
			ObjectName = objectName;
		}

		public TableDependencyContract() { }
		#endregion

		#region Properties
		public String TypeDescription { get; set; }
		public String SchemaName { get; set; }
		public String ObjectName { get; set; }
		public Int32 ObjectId { get; set; }
		#endregion

		#region Public Methods
		#endregion
	}
}
