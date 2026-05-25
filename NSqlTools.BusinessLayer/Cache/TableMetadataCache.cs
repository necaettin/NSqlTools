using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NSqlTools.Types.IntellisenseContracts;
using System.IO;
using Newtonsoft.Json;
using NSqlTools.Types;

namespace NSqlTools.BusinessLayer.Cache
{
    /// <summary>
    /// Caches table and column metadata for IntelliSense.
    /// Tables: 5 days TTL
    /// Columns: 5 days TTL (bulk loaded with single query per database).
    /// </summary>
    public static class TableMetadataCache
    {
        private const int TableCacheDays = 5; // table & column TTL in days
        private static readonly TimeSpan ColumnTtl = TimeSpan.FromDays(TableCacheDays);
        private static readonly string CacheRoot = GetCacheRoot();

        private class CacheEntry
        {
            public DateTime TableExpirationUtc { get; set; }
            public List<IntellisenseTableContract> Tables { get; set; }
            public List<string> FullNames { get; set; } // schema.table
            // Column caching
            public DateTime? ColumnExpirationUtc { get; set; }
            public bool ColumnsLoaded => ColumnExpirationUtc.HasValue && ColumnExpirationUtc.Value > DateTime.UtcNow;
            public Lazy<Task> ColumnLoadTask { get; set; }
        }

        private class CacheEntryDto
        {
            public DateTime TableExpirationUtc { get; set; }
            public List<IntellisenseTableContract> Tables { get; set; }
            public List<string> FullNames { get; set; }
            public DateTime? ColumnExpirationUtc { get; set; }
        }

        // Key format: server|database
        private static readonly ConcurrentDictionary<string, CacheEntry> _cache = new ConcurrentDictionary<string, CacheEntry>(StringComparer.OrdinalIgnoreCase);

        private static string BuildKey(string connectionString, string databaseName)
        {
            string serverName = ExtractServerName(connectionString);
            return serverName + "|" + databaseName;
        }

        /// <summary>
        /// Ensure table list (no columns) is cached.
        /// </summary>
        public static void EnsureCached(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(databaseName)) return;
            string key = BuildKey(connectionString, databaseName);
            // Try to load a valid cache from disk first
            if (!_cache.ContainsKey(key))
            {
                var loaded = TryLoadFromDisk(key);
                if (loaded != null && loaded.TableExpirationUtc > DateTime.UtcNow && loaded.Tables != null)
                {
                    _cache[key] = loaded;
                    return;
                }
            }
            if (_cache.TryGetValue(key, out var existing))
            {
                if (existing.TableExpirationUtc > DateTime.UtcNow && existing.Tables != null)
                    return;
            }
            var fresh = LoadTables(connectionString, databaseName);
            _cache[key] = fresh;
            SaveToDisk(key, fresh);
        }

        /// <summary>
        /// Ensure columns are bulk loaded asynchronously. If already valid, returns completed task.
        /// </summary>
        public static Task EnsureColumnsCachedAsync(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(databaseName)) return Task.CompletedTask;
            EnsureCached(connectionString, databaseName); // make sure table list exists first
            string key = BuildKey(connectionString, databaseName);
            var entry = _cache.GetOrAdd(key, _ => LoadTables(connectionString, databaseName));
            if (entry.ColumnsLoaded && entry.ColumnExpirationUtc > DateTime.UtcNow) return Task.CompletedTask;
            // Create load task if missing or expired
            if (entry.ColumnLoadTask != null && entry.ColumnLoadTask.IsValueCreated)
            {
                return entry.ColumnLoadTask.Value;
            }
            var newTask = new Lazy<Task>(() => Task.Run(() => BulkLoadColumns(connectionString, databaseName, entry)));
            var task = (entry.ColumnLoadTask = newTask);
            return task.Value; // trigger
        }

        /// <summary>
        /// Return table contracts (columns may be null if column cache not yet loaded).
        /// </summary>
        public static List<IntellisenseTableContract> GetTables(string connectionString, string databaseName)
        {
            EnsureCached(connectionString, databaseName);
            string key = BuildKey(connectionString, databaseName);
            return _cache.TryGetValue(key, out var entry) && entry.Tables != null ? entry.Tables : new List<IntellisenseTableContract>();
        }

        private static CacheEntry LoadTables(string connectionString, string databaseName)
        {
            var contractList = new List<IntellisenseTableContract>();
            var fullNames = new List<string>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (!string.IsNullOrWhiteSpace(databaseName) && !conn.Database.Equals(databaseName, StringComparison.OrdinalIgnoreCase))
                        conn.ChangeDatabase(databaseName);
                    string sql = @"SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_SCHEMA, TABLE_NAME";
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string schema = rdr.GetString(0);
                            string table = rdr.GetString(1);
                            contractList.Add(new IntellisenseTableContract
                            {
                                SchemaName = schema,
                                TableName = table,
                                ColumnList = null
                            });
                            fullNames.Add(schema + "." + table);
                        }
                    }
                }
            }
            catch
            {
	            // ignored
            }

            return new CacheEntry
            {
                TableExpirationUtc = DateTime.UtcNow.AddDays(TableCacheDays),
                Tables = contractList,
                FullNames = fullNames,
                ColumnExpirationUtc = null,
                ColumnLoadTask = null
            };
        }

        /// <summary>
        /// Bulk load all columns for all tables in a database and attach to table contracts.
        /// </summary>
        private static void BulkLoadColumns(string connectionString, string databaseName, CacheEntry entry)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (!string.IsNullOrWhiteSpace(databaseName) && !conn.Database.Equals(databaseName, StringComparison.OrdinalIgnoreCase))
                        conn.ChangeDatabase(databaseName);
                    const string sql = @"SELECT s.name AS SchemaName, t.name AS TableName, c.name AS ColumnName
                                           FROM sys.tables t
                                           INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                                           INNER JOIN sys.columns c ON c.object_id = t.object_id
                                           WHERE t.is_ms_shipped = 0
                                           ORDER BY s.name, t.name, c.column_id";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
	                    cmd.CommandType = CommandType.Text;
	                    cmd.CommandTimeout = 60;
	                    using (var rdr = cmd.ExecuteReader())
	                    {
		                    var map = entry.Tables.ToDictionary(t => t.SchemaName + "." + t.TableName, t => t, StringComparer.OrdinalIgnoreCase);
		                    while (rdr.Read())
		                    {
			                    var schema = rdr.GetString(0);
			                    var table = rdr.GetString(1);
			                    var column = rdr.GetString(2);
			                    if (map.TryGetValue(schema + "." + table, out var tbl))
			                    {
				                    if (tbl.ColumnList == null) tbl.ColumnList = new List<IntellisenseColumnContract>();
				                    tbl.ColumnList.Add(new IntellisenseColumnContract { ColumnName = column, DataType = null });
			                    }
		                    }
	                    }
                    }
                }
                entry.ColumnExpirationUtc = DateTime.UtcNow.Add(ColumnTtl);
                // Persist updated columns TTL
                var key = _cache.FirstOrDefault(kvp => kvp.Value == entry).Key;
                if (!string.IsNullOrEmpty(key)) SaveToDisk(key, entry);
            }
            catch
            {
                // leave columns unloaded on failure
            }
        }

        private static string ExtractServerName(string connectionString)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                return builder.DataSource ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Returns all cached databases (across servers) as IntellisenseDatabaseContract entries.
        /// Column lists may be null if not yet bulk-loaded.
        /// </summary>
        public static List<IntellisenseDatabaseContract> GetAllCachedDatabases()
        {
            var list = new List<IntellisenseDatabaseContract>();
            foreach (var kvp in _cache)
            {
                var entry = kvp.Value;
                // extract db name from key: server|database
                var idx = kvp.Key.IndexOf('|');
                var db = idx >= 0 && kvp.Key.Length > idx + 1 ? kvp.Key.Substring(idx + 1) : kvp.Key;
                list.Add(new IntellisenseDatabaseContract
                {
                    DbName = db,
                    TableList = entry.Tables ?? new List<IntellisenseTableContract>()
                });
            }
            return list;
        }

        /// <summary>
        /// Clears all cached table/column metadata.
        /// </summary>
        public static void Clear()
        {
            _cache.Clear();
            try
            {
                if (Directory.Exists(CacheRoot))
                {
                    foreach (var file in Directory.EnumerateFiles(CacheRoot, "*.json", SearchOption.TopDirectoryOnly))
                    {
                        try { File.Delete(file); }
                        catch
                        {
	                        // ignored
                        }
                    }
                }
            }
            catch
            {
	            // ignored
            }
        }

        private static string GetCacheRoot()
        {
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var path = Path.Combine(baseDir, Constants.BaseFolder, "Cache");
                Directory.CreateDirectory(path);
                return path;
            }
            catch { return Path.GetTempPath(); }
        }

        private static string GetCachePath(string key)
        {
            // sanitize key for filename: server|db -> server_db.json
            var safe = string.Concat(key.Replace("|", "_"), ".json");
            return Path.Combine(CacheRoot, safe);
        }

        private static void SaveToDisk(string key, CacheEntry entry)
        {
            try
            {
                var dto = new CacheEntryDto
                {
                    TableExpirationUtc = entry.TableExpirationUtc,
                    Tables = entry.Tables,
                    FullNames = entry.FullNames,
                    ColumnExpirationUtc = entry.ColumnExpirationUtc
                };
                var json = JsonConvert.SerializeObject(dto);
                File.WriteAllText(GetCachePath(key), json);
            }
            catch
            {
	            // ignored
            }
        }

        private static CacheEntry TryLoadFromDisk(string key)
        {
            try
            {
                var path = GetCachePath(key);
                if (!File.Exists(path)) return null;
                var json = File.ReadAllText(path);
                var dto = JsonConvert.DeserializeObject<CacheEntryDto>(json);
                if (dto == null) return null;
                return new CacheEntry
                {
                    TableExpirationUtc = dto.TableExpirationUtc,
                    Tables = dto.Tables ?? new List<IntellisenseTableContract>(),
                    FullNames = dto.FullNames ?? new List<string>(),
                    ColumnExpirationUtc = dto.ColumnExpirationUtc,
                    ColumnLoadTask = null
                };
            }
            catch { return null; }
        }
    }
}
