using System.ComponentModel;

namespace NSqlTools.Types
{
	public class Enums
	{
		public enum ObjectTypeEnum
		{
			[Description("Aggregate function (CLR)")]
			A,
			[Description("CHECK constraint")]
			C,
			[Description("DEFAULT (constraint or stand-alone)")]
			D,
			[Description("FOREIGN KEY constraint")]
			F,
			[Description("SQL scalar function")]
			FN,
			[Description("Assembly (CLR) scalar-function")]
			FS,
			[Description("Assembly (CLR) table-valued function")]
			FT,
			[Description("SQL table-valued function")]
			IF,
			[Description("Internal table")]
			IT,
			[Description("Stored Procedure")]
			P,
			[Description("Assembly (CLR) stored-procedure")]
			PC,
			[Description("Plan guide")]
			PG,
			[Description("PRIMARY KEY constraint")]
			PK,
			[Description("Rule (old-style, stand-alone)")]
			R,
			[Description("Replication-filter-procedure")]
			RF,
			[Description("System base table")]
			S,
			[Description("Synonym")]
			SN,
			[Description("Sequence object")]
			SO,
			[Description("Table")]
			U,
			[Description("View")]
			V,
			[Description("Trigger")]
			TR,
			[Description("Table type")]
			TT,
			[Description("UNIQUE constraint")]
			UQ,
			[Description("Extended stored procedure")]
			X,
			[Description("Undefined")]
			UNDEFINED,
			[Description("Repo")]
			REPO_FILE
		};

		public enum SqlColumnDataTypeEnum
		{
			sql_image = 34,
			sql_text = 35,
			sql_uniqueidentifier = 36,
			sql_date = 40,
			sql_time = 41,
			sql_datetime2 = 42,
			sql_datetimeoffset = 43,
			sql_tinyint = 48,
			sql_smallint = 52,
			sql_int = 56,
			sql_smalldatetime = 58,
			sql_real = 59,
			sql_money = 60,
			sql_datetime = 61,
			sql_float = 62,
			sql_sql_variant = 98,
			sql_ntext = 99,
			sql_bit = 104,
			sql_decimal = 106,
			sql_numeric = 108,
			sql_smallmoney = 122,
			sql_bigint = 127,
			sql_hierarchyid = 128,
			sql_geometry = 129,
			sql_geography = 130,
			sql_varbinary = 165,
			sql_varchar = 167,
			sql_binary = 173,
			sql_char = 175,
			sql_timestamp = 189,
			sql_nvarchar = 231,
			sql_nchar = 239,
			sql_xml = 241,
			sql_sysname = 256
		}

		public enum ColumnDifferenceTypeEnum
		{
			Equal = 1,
			NotEqual = 2,
			SourceExists = 3,
			TargetExists = 4
		}

		public enum FormOpenModeEnum
		{
			Add = 1,
			Edit = 2,
			Delete = 3
		}

		public enum CacheTypeEnum 
		{ 
			DB,
			Schema,
			Depot
		}

		public enum RunningStatusEnum
		{
			Completed = 1,
			NotCompleted = 2,
			Running = 3
		}
	}
}
