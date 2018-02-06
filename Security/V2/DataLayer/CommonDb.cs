using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Security.V2.CommonContracts;
using Security.V2.Contracts;

namespace Security.V2.DataLayer
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
    }
}
