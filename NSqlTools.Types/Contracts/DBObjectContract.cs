using System;
using System.Collections.Generic;
using static NSqlTools.Types.Enums;

namespace NSqlTools.Types
{
	[Serializable]
	public class DBObjectContract
	{
		#region Constructors
		public DBObjectContract(Int32 objectId, ObjectTypeEnum objectType, String objectTypeName, String name, String schemaName, String dbName, Int32 hitCount = 0) : this()
		{
			Name = name;
			ObjectId = objectId;
			SchemaName = schemaName;
			ObjectType = objectType;
			ObjectTypeName = objectTypeName;
			DBName = dbName;
			HitCount = hitCount;
		}

		public DBObjectContract()
		{

		}
        #endregion

        #region Properties
        public String ConnectionString { get; set; }

        public String DBName { get; set; }

        public ObjectTypeEnum ObjectType { get; set; }

		public String ObjectTypeName { get; set; }

		public Int32 SchemaId { get; set; }

		public String SchemaName { get; set; }

		public String Name { get; set; }

		public String Path { get; set; }

		public Int32 ObjectId { get; set; }

		public String Definition { get; set; }

		public List<ColumnContract> ColumnList { get; set; }

		public Int32 HitCount { get; set; }
        #endregion

        #region Helper Properties
        public String Description 
		{ 
			get
			{
				String description;
				switch (ObjectType)
				{
					case ObjectTypeEnum.REPO_FILE:
						description = $"{ObjectTypeName}: {Path}";

						break;
					default:
						description = $"{ObjectTypeName}: {DBName}.{SchemaName}.{Name}";

						break;
				}

				return description;
			}
		}
        #endregion
    }
}
