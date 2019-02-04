using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Security.Contracts;

namespace Security.Core.DataLayer
{
    /// <summary>
    /// Управление соединением с БД
    /// </summary>
    public class SqlConnectionFactory: IConnectionFactory
    {
        private string ConnectionString => GetConnectionString();

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["securitydb"].ConnectionString;
        }

        /// <summary>
        /// Создание соединения с БД
        /// </summary>
        public Func<IDbConnection> CreateConnection => GetConnection;

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
