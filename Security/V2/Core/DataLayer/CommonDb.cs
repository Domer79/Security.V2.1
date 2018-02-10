using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Security.V2.CommonContracts;
using Security.V2.Contracts;

namespace Security.V2.Core.DataLayer
{
    public class CommonDb : ICommonDb
    {
        private readonly IConnectionFactory _connectionFactory;

        public CommonDb(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<T> Query<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.Query<T>(query, parameters);
            }
        }

        public T ExecuteScalar<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.ExecuteScalar<T>(query, parameters);
            }
        }

        public int ExecuteNonQuery(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.Execute(query, parameters);
            }
        }

        public void GetPageCount(int pageSize, string query, IEntityCollectionInfo collectionInfo)
        {
            collectionInfo.Count = ExecuteScalar<int>(query);
            collectionInfo.PageCount = collectionInfo.Count / pageSize + (collectionInfo.Count % pageSize > 0 ? 1 : 0);
        }

        public IEnumerable<TReturn> Query<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.Query<T1, T2, TReturn>(query, p, parameters);
            }
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryAsync<T>(query, parameters);
            }
        }

        public Task<T> ExecuteScalarAsync<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.ExecuteScalarAsync<T>(query, parameters);
            }
        }

        public Task<int> ExecuteNonQueryAsync(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task GetPageCountAsync(int pageSize, string query, IEntityCollectionInfo collectionInfo)
        {
            collectionInfo.Count = await ExecuteScalarAsync<int>(query);
            collectionInfo.PageCount = collectionInfo.Count / pageSize + (collectionInfo.Count % pageSize > 0 ? 1 : 0);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryAsync<T1, T2, TReturn>(query, p, parameters);
            }
        }

        public T QueryFirst<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryFirst<T>(query, parameters);
            }
        }

        public T QueryFirstOrDefault<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryFirstOrDefault<T>(query, parameters);
            }
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QuerySingle<T>(query, parameters);
            }
        }

        public T QuerySingleOrDefault<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QuerySingleOrDefault<T>(query, parameters);
            }
        }

        public Task<T> QueryFirstAsync<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryFirstAsync<T>(query, parameters);
            }
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryFirstOrDefaultAsync<T>(query, parameters);
            }
        }

        public Task<T> QuerySingleAsync<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QuerySingleAsync<T>(query, parameters);
            }
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QuerySingleOrDefaultAsync<T>(query, parameters);
            }
        }
    }
}
