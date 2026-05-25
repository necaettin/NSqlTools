using NSqlTools.Lib.Helpers;
using NSqlTools.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using static NSqlTools.Types.Enums;

namespace NSqlTools.BusinessLayer
{
	public class ObjectTypeBusiness
	{
		#region Properties
		private List<ObjectTypeContract> objectTypes { get; set; }
		public List<ObjectTypeContract> ObjectTypes
		{
			get { return objectTypes ?? (objectTypes = GetSqlObjectTypes()); }
		}
		#endregion

		#region Static Methods
		public List<ObjectTypeContract> GetSqlObjectTypes()
		{
			return Enum.GetValues(typeof(ObjectTypeEnum))
				.Cast<ObjectTypeEnum>()
				.Select(e => new ObjectTypeContract(e, EnumHelper.GetEnumDescription(e)))
				.Where(e => e.Type.IsScriptable())
				.ToList();
		}
		#endregion
	}
}
