using System.Drawing;
using static NSqlTools.Types.Enums;

namespace NSqlTools.Types.BaseTypes
{
	public class BaseCompareContract
	{
		public Bitmap Difference { get; set; }

		public ColumnDifferenceTypeEnum ColumnDifferenceType { get; set; }
	}
}
