using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonContracts;

namespace Orm
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly IGlobalSettings _globalSettings;

        public SqlConnectionFactory(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        private string ConnectionString => GetConnectionString();

        private string GetConnectionString()
        {
            return _globalSettings.DefaultConnectionString;
        }

        public Func<IDbConnection> CreateConnection => GetConnection;

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
