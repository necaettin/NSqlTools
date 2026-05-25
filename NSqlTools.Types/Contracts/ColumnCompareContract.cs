using NSqlTools.Types.BaseTypes;
using System;

namespace NSqlTools.Types
{
	[Serializable]
	public class ColumnCompareContract : BaseCompareContract
	{
		#region Properties
		public Int32? ColumnIdSource { get; set; }
		public String NameSource { get; set; }
		public int? SystemTypeIdSource { get; set; }
		public int? UserTypeIdSource { get; set; }
		public int? MaxLengthSource { get; set; }
		public int? PrecisionSource { get; set; }
		public bool? IsNullableSource { get; set; }
		public bool? IsIdentitySource { get; set; }
		public string TypeNameSource { get; set; }
		public String TypeNameCustomSource
		{
			get
			{
				if (ColumnIdSource == null)
					return null;
				else
					return ColumnContract.GetTypeNameCustom(TypeNameSource, SystemTypeIdSource, MaxLengthSource, PrecisionSource);
			}
		}

		public Int32? ColumnIdTarget { get; set; }
		public String NameTarget { get; set; }
		public int? SystemTypeIdTarget { get; set; }
		public int? UserTypeIdTarget { get; set; }
		public int? MaxLengthTarget { get; set; }
		public int? PrecisionTarget { get; set; }
		public bool? IsNullableTarget { get; set; }
		public bool? IsIdentityTarget { get; set; }
		public string TypeNameTarget { get; set; }
		public String TypeNameCustomTarget
		{
			get
			{
				if (ColumnIdTarget == null)
					return null;
				else
					return ColumnContract.GetTypeNameCustom(TypeNameTarget, SystemTypeIdTarget, MaxLengthTarget, PrecisionTarget);
			}
		}
		#endregion
	}
}
