using System;
using System.Collections.Generic;

namespace NSqlTools.Types
{
	public class DBObjectChangedEventArgs : EventArgs
	{
		public DBObjectContract DBObjectContract { get; set; }
	}
}
