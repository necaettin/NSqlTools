using System;

namespace NSqlTools.Types
{
	[Serializable]
	public class DBContract
	{
		#region Constructors
		public DBContract(string name, int databaseId)
		{
			Name = name;
			DatabaseId = databaseId;
		}
		#endregion

		#region Properties
		public String Name { get; set; }

		public Int32 DatabaseId { get; set; }

        public Int32 Progress { get; set; }

		public Int32 OrderNo { get; set; }
		#endregion

		#region Override Methods
		public override string ToString()
		{
			return Name;
		}
		#endregion
	}
}
