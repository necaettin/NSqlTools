using System;

namespace NSqlTools.Types.Contracts
{
	public class TableIndexContract
	{
        #region Constructors
        #endregion

        #region Properties
        public int IndexId { get; set; }

        public String IndexName { get; set; }

        public String ColumnNames { get; set; }

        public Boolean IsUnique { get; set; }

        public Boolean IsPrimaryKey { get; set; }

        public String IndexTypeName { get; set; }
        #endregion
    }
}
