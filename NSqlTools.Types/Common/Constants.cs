using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace NSqlTools.Types
{
	public class Constants
	{
		public const string BaseFolder = "DataFiles";

		public static String connectionStringsFileName;
		public static String ConnectionStringsFileName
		{
			get
			{
				if (String.IsNullOrWhiteSpace(connectionStringsFileName))
				{
					String path = AppDomain.CurrentDomain.BaseDirectory;
					connectionStringsFileName = Path.Combine(path, BaseFolder, "DataSources.xml");
				}

				return connectionStringsFileName;
			}
		}

		public static String favoriteQueriesFileName;
		public static String FavoriteQueriesFileName
		{
			get
			{
				if (String.IsNullOrWhiteSpace(favoriteQueriesFileName))
				{
					String path = AppDomain.CurrentDomain.BaseDirectory;
					favoriteQueriesFileName = Path.Combine(path, BaseFolder, "FavoriteQueries.xml");
				}

				return favoriteQueriesFileName;
			}
		}

		public static String projectsFileName;
		public static String ProjectsFileName
		{
			get
			{
				if (String.IsNullOrWhiteSpace(projectsFileName))
				{
					String path = AppDomain.CurrentDomain.BaseDirectory;
					projectsFileName = Path.Combine(path, BaseFolder, "Projects.xml");
				}

				return projectsFileName;
			}
		}

		public static String snippetsFileName;
		public static String SnippetsFileName
		{
			get
			{
				if (String.IsNullOrWhiteSpace(snippetsFileName))
				{
					String path = AppDomain.CurrentDomain.BaseDirectory;
					snippetsFileName = Path.Combine(path, BaseFolder, "Snippets.xml");
				}

				return snippetsFileName;
			}
		}

		public static String lastFormDataFileName;
		public static String LastFormDataFileName
		{
			get
			{
				if (String.IsNullOrWhiteSpace(lastFormDataFileName))
				{
					String path = AppDomain.CurrentDomain.BaseDirectory;
					lastFormDataFileName = Path.Combine(path, BaseFolder, "LastFormData.xml");
				}

				return lastFormDataFileName;
			}
		}

		public static String logsFolder;
		public static String LogsFolder
		{
			get
			{
				if (String.IsNullOrWhiteSpace(logsFolder))
				{
					String path = AppDomain.CurrentDomain.BaseDirectory;
					logsFolder = Path.Combine(path, BaseFolder, "Logs");
				}

				return logsFolder;
			}
		}

		public static String changeLogFileName;
		public static String ChangeLogFileName
		{
			get
			{
				if (String.IsNullOrWhiteSpace(changeLogFileName))
				{
					String path = AppDomain.CurrentDomain.BaseDirectory;
					changeLogFileName = Path.Combine(path, BaseFolder, "ChangeLog.txt");
				}

				return changeLogFileName;
			}
		}

		public static String GetProjectFile(String uniqueId)
		{
			String path = AppDomain.CurrentDomain.BaseDirectory;
			
			return Path.Combine(path, BaseFolder, "Projects", uniqueId + ".xml");
		}

		public static int DefaultSplitterDistance = 250;

		public static Color ComponentRequiredColor = Color.Linen;

		public static String CaseInsensitiveCollation = "Latin1_General_CI_AS";

		public static String CaseSensitiveCollation = "Latin1_General_CS_AS";

		public static String[] SqlKeywords =
			("ADD,ALL,ALTER,AND,ANY,AS,ASC,AUTHORIZATION,BACKUP,BEGIN,BETWEEN,BREAK,BROWSE,BULK,BY,CASCADE,CASE,CHECK," +
			"CLOSE,CLUSTERED,COALESCE,COLLATE,COLUMN,COMMIT,COMPUTE,CONSTRAINT,CONTAINS,CONTAINSTABLE,CONTINUE,CONVERT," +
			"CREATE,CROSS,CURRENT,CURRENT_DATE,CURRENT_TIME,CURRENT_TIMESTAMP,CURRENT_USER,CURSOR,DATABASE,DBCC,DEALLOCATE," +
			"DECLARE,DEFAULT,DELETE,DENY,DESC,DISK,DISTINCT,DISTRIBUTED,DOUBLE,DROP,DUMP,ELSE,END,ERRLVL,ESCAPE,EXCEPT,EXEC," +
			"EXECUTE,EXISTS,EXIT,EXTERNAL,FETCH,FILE,FILLFACTOR,FOR,FOREIGN,FREETEXT,FREETEXTTABLE,FROM,FULL,FUNCTION,GOTO,GRANT," +
			"GROUP,HAVING,HOLDLOCK,IDENTITY,IDENTITY_INSERT,IDENTITYCOL,IF,IN,INDEX,INNER,INSERT,INTERSECT,INTO,IS,JOIN,KEY,KILL," +
			"LEFT,LIKE,LINENO,LOAD,MERGE,NATIONAL,NOCHECK,NONCLUSTERED,NOT,NULL,NULLIF,OF,OFF,OFFSETS,ON,OPEN,OPENDATASOURCE,OPENQUERY," +
			"OPENROWSET,OPENXML,OPTION,OR,ORDER,OUTER,OVER,PERCENT,PIVOT,PLAN,PRECISION,PRIMARY,PRINT,PROC,PROCEDURE,PUBLIC,RAISERROR," +
			"READ,READTEXT,RECONFIGURE,REFERENCES,REPLICATION,RESTORE,RESTRICT,RETURN,REVERT,REVOKE,RIGHT,ROLLBACK,ROWCOUNT,ROWGUIDCOL," +
			"RULE,SAVE,SCHEMA,SECURITYAUDIT,SELECT,SEMANTICKEYPHRASETABLE,SEMANTICSIMILARITYDETAILSTABLE,SEMANTICSIMILARITYTABLE,SESSION_USER," +
			"SET,SETUSER,SHUTDOWN,SOME,STATISTICS,SYSTEM_USER,TABLE,TABLESAMPLE,TEXTSIZE,THEN,TO,TOP,TRAN,TRANSACTION,TRIGGER,TRUNCATE," +
			"TRY_CONVERT,TSEQUAL,UNION,UNIQUE,UNPIVOT,UPDATE,UPDATETEXT,USE,USER,VALUES,VARYING,VIEW,WAITFOR,WHEN,WHERE,WHILE,WITH,WITHIN GROUP," +
			"WRITETEXT").Split(',').Distinct().OrderBy(o => o).ToArray();
	
		public static Int32 CacheDuration = 60;

		public static String RegistryRootKey = @"Software\NSqlTools";
	}
}
