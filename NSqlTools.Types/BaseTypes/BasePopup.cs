using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSqlTools.Types.BaseTypes
{
	public class BasePopup : Form
	{
		#region Override Methods
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				this.Close();

				return true; // Tuş işlenmiş olarak işaretlenir
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
		#endregion
	}
}
