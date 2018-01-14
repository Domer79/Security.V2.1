using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using CommonContracts;
using Dapper;

namespace Orm
{
    public class CommonDb: ICommonDb
    {
        private readonly IGlobalSettings _settings;

        public CommonDb(IGlobalSettings settings)
        {
            _settings = settings;
        }

        public string GetConnectionString()
        {
            return _settings.DefaultConnectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public IEnumerable<T> Query<T>(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(query, parameters);
            }
        }

        public T ExecuteScalar<T>(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                return connection.ExecuteScalar<T>(query, parameters);
            }
        }

        public int ExecuteNonQuery(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(query, parameters);
            }
        }

        public void CreateDatabaseIfNotExists()
        {
            var connectionString = GetConnectionString();
            var dbName = GetDbName(connectionString);

            var builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "master";

            if (DatabaseExists(dbName, builder.ToString()))
                return;

            using (var connection = new SqlConnection(builder.ToString()))
            {
                connection.Execute($"create database {dbName}");
            }
        }

        public void GetPageCount(int pageSize, string query, IEntityCollectionInfo collectionInfo)
        {
            collectionInfo.Count = ExecuteScalar<int>(query);
            collectionInfo.PageCount = collectionInfo.Count / pageSize + (collectionInfo.Count % pageSize > 0 ? 1 : 0);
        }

        public IEnumerable<TReturn> Query<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null)
        {
            using (var connection = new SqlConnection(query))
            {
                return connection.Query<T1, T2, TReturn>(query, p, parameters);
            }
        }

        private string GetDbName(string connection)
        {
            var dbName = Regex.Match(connection, @"database=(?<dbName>[\w]+);|initial catalog=(?<dbName>[\w]+);", RegexOptions.IgnoreCase).Groups["dbName"].Value;
            return dbName;
        }

        private bool DatabaseExists(string dbName, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return connection.ExecuteScalar<bool>(
                        "select top 1 cast(isExist as bit) from (select 1 isExist from (select id from (select DB_ID(@dbName) id) s where id is not null)s1 union all select 0)s2",
                        new {dbName});
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
