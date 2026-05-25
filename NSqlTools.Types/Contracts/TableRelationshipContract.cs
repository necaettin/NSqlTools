using System;

namespace NSqlTools.Types.Contracts
{
	public class TableRelationshipContract
	{
        #region Constructors
        #endregion

        #region Properties
        public String FKName { get; set; }

        public String ReferencedTable { get; set;}

        public String ColumnNames { get; set; }

        public String RelationshipName { get; set; }
        #endregion
    }
}
