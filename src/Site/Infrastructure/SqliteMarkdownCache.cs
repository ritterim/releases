using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RimDev.Releases.Infrastructure
{
    public interface IMarkdownCache
    {
        Task<string> Get(string markdown, string context, string mode);
        Task Add(string markdown, string context, string mode, string contents);
    }

    /// <remarks>
    /// This includes somewhat manual usage of ADO.NET, rather than using Dapper
    /// due to https://github.com/StackExchange/dapper-dot-net/issues/375 which leads to
    /// https://github.com/aspnet/Microsoft.Data.Sqlite/issues/80 and should be fixed in
    /// https://github.com/aspnet/Microsoft.Data.Sqlite/pull/172
    /// </remarks>
    public class SqliteMarkdownCache : IMarkdownCache
    {
        private const string CacheTableName = "MarkdownCache";

        private readonly string file;
        private bool isInitialized;

        public SqliteMarkdownCache(string dbFile)
        {
            if (dbFile == null) throw new ArgumentNullException(nameof(dbFile));

            file = dbFile;
        }

        public async Task Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            if (!System.IO.File.Exists(file))
            {
                throw new ApplicationException($"SQLite database at path '{file}' was not found.");
            }

            var tableExists = await TableExistsAsync(CacheTableName);
            if (!tableExists)
            {
                using (var conn = await GetOpenConnectionAsync())
                {
                    await conn.ExecuteAsync(
                        $@"create table {CacheTableName}
                           (
                               Key text primary key,
                               Contents blob
                           )");
                }
            }

            isInitialized = true;
        }

        /// <returns>Returns null if no matching item is in the cache.</returns>
        public async Task<string> Get(string markdown, string context, string mode)
        {
            if (markdown == null) throw new ArgumentNullException(nameof(markdown));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (mode == null) throw new ArgumentNullException(nameof(mode));

            await Initialize();

            var key = ComputeHash(markdown, context, mode);

            using (var conn = await GetOpenConnectionAsync())
            {
                // After https://github.com/aspnet/Microsoft.Data.Sqlite/pull/172 is published to the
                // Microsoft.Data.SQLite package we can update the package and use this code instead:
                // var result = await conn.ExecuteScalarAsync<string>(
                //     $"select Contents from {CacheTableName} where Key = @Key",
                //     new { Key = key });

                // Using a cast since this code should be removable in the future:
                var cmd = new SqliteCommand($"select Contents from {CacheTableName} where Key = @Key", (SqliteConnection)conn);
                cmd.Parameters.AddWithValue("@Key", key);

                var result = (string)(await cmd.ExecuteScalarAsync());

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return null;
        }

        public async Task Add(string markdown, string context, string mode, string contents)
        {
            if (markdown == null) throw new ArgumentNullException(nameof(markdown));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (mode == null) throw new ArgumentNullException(nameof(mode));

            await Initialize();

            var key = ComputeHash(markdown, context, mode);

            using (var conn = await GetOpenConnectionAsync())
            {
                // After https://github.com/aspnet/Microsoft.Data.Sqlite/pull/172 is published to the
                // Microsoft.Data.SQLite package we can update the package and use this code instead:
                // await conn.ExecuteAsync(
                //     $"insert or replace into {CacheTableName} (Key, Contents) values (@Key, @Contents)",
                //     new { Key = key, Contents = contents });

                // Using a cast since this code should be removable in the future:
                var cmd = new SqliteCommand($"insert or replace into {CacheTableName} (Key, Contents) values (@Key, @Contents)", (SqliteConnection)conn);
                cmd.Parameters.AddWithValue("@Key", key);
                cmd.Parameters.AddWithValue("@Contents", contents);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task<bool> TableExistsAsync(string tableName)
        {
            if (tableName == null) throw new ArgumentNullException(nameof(tableName));

            using (var conn = await GetOpenConnectionAsync())
            {
                // Adapted from http://stackoverflow.com/a/1604121/941536
                var result = await conn.ExecuteScalarAsync<long>(
                    $"select count(*) from sqlite_master where type='table' and name='{tableName}'");

                return result > 0;
            }
        }

        private async Task<IDbConnection> GetOpenConnectionAsync()
        {
            var conn = new SqliteConnection($"Data Source={file}");
            await conn.OpenAsync();

            return conn;
        }

        private static string ComputeHash(string markdown, string context, string mode)
        {
            return Hash(markdown + context + mode);
        }

        // Source: http://stackoverflow.com/a/26558102
        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}