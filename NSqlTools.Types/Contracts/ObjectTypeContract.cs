using System;
using static NSqlTools.Types.Enums;

namespace NSqlTools.Types
{
	[Serializable]
	public class ObjectTypeContract
	{
		#region Constructor
		public ObjectTypeContract(ObjectTypeEnum type, String typeDescription)
		{
			Type = type;
			TypeDescription = typeDescription;
		}
		#endregion

		#region Properties
		public ObjectTypeEnum Type { get; set; }

		public String TypeDescription { get; set; }
		#endregion
	}
}
