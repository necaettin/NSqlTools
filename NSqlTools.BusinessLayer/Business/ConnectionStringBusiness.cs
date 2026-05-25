using NSqlTools.Types;
using NSqlTools.Lib;
using System;
using System.Collections.Generic;
using NSqlTools.Lib.Helpers;
using NSqlTools.Types.Properties;

namespace NSqlTools.BusinessLayer
{
	public class ConnectionStringBusiness
	{
		#region Methods
		public List<ConnectionStringContract> GetConnectionString()
		{
			List<ConnectionStringContract> list;
			try
			{
				list = SerializeHelper.DeserializeFromXml<List<ConnectionStringContract>>(Constants.ConnectionStringsFileName);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
				throw new Exception(CommonResource.ErrorOccuredWhileGettingConnectionStringsDefineConnectionStrings, ex);
			}

			return list;
		}
		#endregion
	}
}
