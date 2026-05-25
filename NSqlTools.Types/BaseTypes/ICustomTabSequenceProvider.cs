using System.Collections.Generic;
using System.Windows.Forms;

namespace NSqlTools.Types.BaseTypes
{
	public interface ICustomTabSequenceProvider
    {
        IList<Control> GetCustomTabSequence();
    }
}