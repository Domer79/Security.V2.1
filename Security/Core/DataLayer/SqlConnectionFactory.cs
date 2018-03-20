﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Security.Contracts;

namespace Security.Core.DataLayer
{
    public class SqlConnectionFactory: IConnectionFactory
    {
        private string ConnectionString => GetConnectionString();

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["securitydb"].ConnectionString;
        }

        public Func<IDbConnection> CreateConnection => GetConnection;

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}