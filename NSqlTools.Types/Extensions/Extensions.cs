using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NSqlTools.Types.Enums;

namespace NSqlTools.Types
{
	public static class ObjectTypeEnumExtensions
	{
		public static bool IsScriptable(this ObjectTypeEnum type)
		{
			switch (type)
			{
				case ObjectTypeEnum.P:
				case ObjectTypeEnum.U:
				case ObjectTypeEnum.FN:
				case ObjectTypeEnum.IF:
				case ObjectTypeEnum.V:
				case ObjectTypeEnum.TR:
				case ObjectTypeEnum.TT:
				case ObjectTypeEnum.X:
					return true;
				default:
					return false;
			}
		}
	}
}
