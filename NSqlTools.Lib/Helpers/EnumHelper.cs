using System;
using System.ComponentModel;
using System.Reflection;
using static NSqlTools.Types.Enums;

namespace NSqlTools.Lib.Helpers
{
	public class EnumHelper
	{
		public static ObjectTypeEnum StringToEnum(String objectTypeStr)
		{
			if (Enum.TryParse(objectTypeStr, out ObjectTypeEnum result))
				return result;
			else
				return ObjectTypeEnum.UNDEFINED;
		}

		public static string GetEnumDescription(Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

			return attribute == null ? value.ToString() : attribute.Description;
		}
	}
}
