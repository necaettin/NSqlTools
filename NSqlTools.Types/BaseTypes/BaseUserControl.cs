using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSqlTools.Types.BaseTypes
{
	public class BaseUserControl : UserControl
	{
		#region Properties
		public TabPage ParentTabPage { get; set; }

		public Form MainForm { get; set; }

		public virtual List<Object> TabProviders { get; }
		#endregion

		#region Virtual Methods
		public virtual void InitForm() { }

		public virtual BaseScreenDataContract GetFormData() { return null; }

		public virtual void SetFormData(BaseScreenDataContract formDataBaseContract) { }
		#endregion
	}
}
