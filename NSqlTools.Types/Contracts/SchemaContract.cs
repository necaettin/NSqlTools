using System;

namespace NSqlTools.Types
{
	[Serializable]
	public class SchemaContract
	{
		#region Constructors
		public SchemaContract(String name, Int32 schemaId)
		{
			Name = name;
			SchemaId = schemaId;
		}
		#endregion

		#region Properties
		public String Name { get; set; }

		public Int32 SchemaId { get; set; }
		#endregion
	}
}
