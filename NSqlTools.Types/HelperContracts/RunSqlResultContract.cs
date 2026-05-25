using System.Data;
using System.Text;

namespace NSqlTools.Types.HelperContracts
{
	public class RunSqlResultContract
	{
		#region Constructors
		public RunSqlResultContract()
        {
            AffectedRowsMessages = new StringBuilder();
        }
		#endregion

		#region Properties
		public DataTableCollection TableCollection { get; set; }

        public StringBuilder AffectedRowsMessages { get; set; }
		#endregion
	}
}
