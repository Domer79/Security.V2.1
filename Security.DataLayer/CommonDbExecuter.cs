using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Security.DataLayer
{
    public class CommonDbExecuter
    {
        private readonly IConnectionFactory _connectionFactory;

        public CommonDbExecuter(IConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public int ExecuteNonQuery(string query, params QueryParameter[] parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var dynamicParameters = new DynamicParameters();
                foreach (var queryParameter in parameters)
                {
                    dynamicParameters.Add(queryParameter.Name, queryParameter.Value);
                }
                connection.Open();
                return connection.Execute(query, dynamicParameters);
            }
        }

        public IEnumerable<T> Query<T>(string query, params QueryParameter[] parameters) where T: class
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var dynamicParameters = new DynamicParameters();
                foreach (var queryParameter in parameters)
                {
                    dynamicParameters.Add(queryParameter.Name, queryParameter.Value);
                }
                connection.Open();
                return connection.Query<T>(query, dynamicParameters);
            }
        }

        public T ExecuteScalar<T>(string query, params QueryParameter[] parameters) where T : class
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var dynamicParameters = new DynamicParameters();
                foreach (var queryParameter in parameters)
                {
                    dynamicParameters.Add(queryParameter.Name, queryParameter.Value);
                }
                connection.Open();
                return connection.ExecuteScalar<T>(query, dynamicParameters);
            }
        }
    }

    public interface IConnectionFactory
    {
        Func<IDbConnection> CreateConnection { get; }
    }

    public class QueryParameter
    {
        public QueryParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
