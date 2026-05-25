using System;
using static NSqlTools.Types.Enums;

namespace NSqlTools.Types
{
	[Serializable]
	public class ColumnContract
	{
		#region Constructors
		public ColumnContract(String name, Int32 columnId) : this()
		{
			Name = name;
			ColumnId = columnId;
		}

		public ColumnContract() { }
		#endregion

		#region Properties
		public Int32 TableObjectId { get; set; }
		public String TableName { get; set; }
		public String SchemaName { get; set; }

		public Int32? ColumnId { get; set; }
		public String Name { get; set; }
		public int? SystemTypeId { get; set; }
		public int? UserTypeId { get; set; }
		public int? MaxLength { get; set; }
		public int? Precision { get; set; }
		public bool? IsNullable { get; set; }
		public bool? IsIdentity { get; set; }
		public string TypeName { get; set; }
        public string DefaultValue { get; set; }
        public Boolean IsSelected { get; set; } = true;
        public Int32 HitCount { get; set; }
        public String TypeNameCustom => GetTypeNameCustom(TypeName, SystemTypeId, MaxLength, Precision);

        #endregion

		#region Public Methods
		public static String GetTypeNameCustom(String typeName, Int32? systemTypeId, Int32? maxLength, Int32? precision)
		{
			String _typeName = typeName;
			if (systemTypeId != null)
				switch ((SqlColumnDataTypeEnum)systemTypeId)
				{
					case SqlColumnDataTypeEnum.sql_char:
					case SqlColumnDataTypeEnum.sql_nvarchar:
					case SqlColumnDataTypeEnum.sql_nchar:
					case SqlColumnDataTypeEnum.sql_ntext:
					case SqlColumnDataTypeEnum.sql_varchar:
						_typeName = $"{typeName}({maxLength})";
						break;

					case SqlColumnDataTypeEnum.sql_float:
					case SqlColumnDataTypeEnum.sql_decimal:
					case SqlColumnDataTypeEnum.sql_numeric:
						_typeName = $"{typeName}({maxLength}, {precision})";

						break;

					case SqlColumnDataTypeEnum.sql_image:
					case SqlColumnDataTypeEnum.sql_text:
					case SqlColumnDataTypeEnum.sql_uniqueidentifier:
					case SqlColumnDataTypeEnum.sql_date:
					case SqlColumnDataTypeEnum.sql_time:
					case SqlColumnDataTypeEnum.sql_datetime2:
					case SqlColumnDataTypeEnum.sql_datetimeoffset:
					case SqlColumnDataTypeEnum.sql_tinyint:
					case SqlColumnDataTypeEnum.sql_smallint:
					case SqlColumnDataTypeEnum.sql_int:
					case SqlColumnDataTypeEnum.sql_smalldatetime:
					case SqlColumnDataTypeEnum.sql_real:
					case SqlColumnDataTypeEnum.sql_money:
					case SqlColumnDataTypeEnum.sql_datetime:
					case SqlColumnDataTypeEnum.sql_sql_variant:
					case SqlColumnDataTypeEnum.sql_bit:
					case SqlColumnDataTypeEnum.sql_smallmoney:
					case SqlColumnDataTypeEnum.sql_bigint:
					case SqlColumnDataTypeEnum.sql_hierarchyid:
					case SqlColumnDataTypeEnum.sql_geometry:
					case SqlColumnDataTypeEnum.sql_geography:
					case SqlColumnDataTypeEnum.sql_varbinary:
					case SqlColumnDataTypeEnum.sql_binary:
					case SqlColumnDataTypeEnum.sql_timestamp:
					case SqlColumnDataTypeEnum.sql_xml:
					case SqlColumnDataTypeEnum.sql_sysname:
						_typeName = typeName;
						break;
					default:
						break;
				}

			return _typeName;
		}
		#endregion
	}
}
