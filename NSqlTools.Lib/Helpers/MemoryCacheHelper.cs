using System;
using System.Runtime.Caching;


namespace NSqlTools.Lib.Helpers
{
	public class MemoryCacheHelper
	{
		#region Static Methods
		private static MemoryCache _cache = MemoryCache.Default;

		public static void Add(String key, Object value, TimeSpan duration)
		{
			_cache.Set(key, value, DateTimeOffset.Now.Add(duration));
		}

		public static T Get<T>(String key)
		{
			return (T)_cache.Get(key);
		}

		public static void Remove(String key)
		{
			_cache.Remove(key);
		}

		public static void Clear()
		{
			try
			{
				foreach (var item in _cache)
				{
					_cache.Remove(item.Key);
				}
			}
			catch(Exception ex) {

				throw new Exception("Error occured while clearing cache!", ex);
			}
		}
		#endregion
	}
}
