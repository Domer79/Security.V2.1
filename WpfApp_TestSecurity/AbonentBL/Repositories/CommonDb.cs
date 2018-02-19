using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonContracts;
using Dapper;
using WpfApp_TestSecurity.Interfaces;

namespace WpfApp_TestSecurity.AbonentBL.Repositories
{
    public class CommonDb : ICommonDb
    {
        private readonly IConnectionFactory _connectionFactory;

        public CommonDb(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void CreateDatabaseIfNotExists()
        {
            throw new NotSupportedException();
        }

        public int ExecuteNonQuery(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.Execute(query, parameters);
            }
        }

        public T ExecuteScalar<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.ExecuteScalar<T>(query, parameters);
            }
        }

        public IDbConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public void GetPageCount(int pageSize, string query, IEntityCollectionInfo collectionInfo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(string query, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TReturn> Query<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
