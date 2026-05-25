using System;
using System.Collections.Generic;
using System.Globalization;

namespace NSqlTools.Lib.Helpers
{
	public static class StringListExtensions
	{
		// InvariantCulture compare; optionally ignore case
		public static int IndexOfInvariant(this IList<string> list, string value, bool ignoreCase = false)
		{
			if (list == null) throw new ArgumentNullException(nameof(list));
			if (value == null) return -1;

			var comparison = ignoreCase
				? StringComparison.InvariantCultureIgnoreCase
				: StringComparison.InvariantCulture;

			for (int i = 0; i < list.Count; i++)
			{
				if (string.Equals(list[i], value, comparison))
					return i;
			}
			return -1;
		}
	}
}
