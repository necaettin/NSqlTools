using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace NSqlTools.BusinessLayer.Intellisense
{
    public class SqlSchemaCache
    {
        public class TableInfo
        {
            public string DbName { get; set; }
            public string Schema { get; set; }
            public string Name { get; set; }
            public List<string> Columns { get; } = new List<string>();

            public string FullSchemaName => Schema + "." + Name;
            public string FullDbSchemaName => DbName + "." + Schema + "." + Name;
        }

        private readonly Dictionary<string, TableInfo> _index =
            new Dictionary<string, TableInfo>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _dbNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, HashSet<string>> _schemasPerDb =
            new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        public IReadOnlyCollection<TableInfo> Tables => _index.Values;

        public void LoadSingleDatabase(string connectionString)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var server = new Server(new ServerConnection(conn));
                LoadDatabase(server, conn.Database);
            }
        }

        public void LoadMultipleDatabases(string connectionString, IEnumerable<string> dbWhitelist = null)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var server = new Server(new ServerConnection(conn));

                var dbs = server.Databases.Cast<Database>()
                    .Where(d => !d.IsSystemObject)
                    .Select(d => d.Name);

                if (dbWhitelist != null)
                    dbs = dbs.Where(n => dbWhitelist.Contains(n, StringComparer.OrdinalIgnoreCase));

                foreach (var dbName in dbs)
                    LoadDatabase(server, dbName);
            }
        }

        private void LoadDatabase(Server server, string dbName)
        {
            var db = server.Databases[dbName];
            if (db == null) return;

            _dbNames.Add(dbName);
            if (!_schemasPerDb.ContainsKey(dbName))
                _schemasPerDb[dbName] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Table t in db.Tables)
            {
                if (t.IsSystemObject) continue;
                var info = new TableInfo
                {
                    DbName = dbName,
                    Schema = t.Schema,
                    Name = t.Name
                };
                foreach (Column c in t.Columns)
                    info.Columns.Add(c.Name);

                _index[info.Name] = info;
                _index[info.FullSchemaName] = info;
                _index[info.FullDbSchemaName] = info;

                _schemasPerDb[dbName].Add(t.Schema);
            }
        }

        public IEnumerable<string> GetDatabaseNames() => _dbNames;

        public IEnumerable<string> GetSchemas(string dbName)
        {
            if (dbName == null) return Enumerable.Empty<string>();
            HashSet<string> set;
            if (_schemasPerDb.TryGetValue(dbName, out set)) return set;
            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetTables(string dbName = null, string schema = null, bool includeAllForms = false)
        {
            IEnumerable<TableInfo> q = _index.Values;
            if (!string.IsNullOrEmpty(dbName))
                q = q.Where(t => t.DbName.Equals(dbName, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(schema))
                q = q.Where(t => t.Schema.Equals(schema, StringComparison.OrdinalIgnoreCase));

            foreach (var t in q)
            {
                if (includeAllForms)
                {
                    yield return t.FullDbSchemaName;
                    yield return t.FullSchemaName;
                    yield return t.Name;
                }
                else
                {
                    yield return t.FullSchemaName;
                }
            }
        }

        public IEnumerable<string> GetColumns(string anyKey)
        {
            TableInfo info;
            if (_index.TryGetValue(anyKey, out info))
                return info.Columns;
            return Enumerable.Empty<string>();
        }

        public bool HasTable(string anyKey) => _index.ContainsKey(anyKey);
    }
}