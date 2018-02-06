using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;

namespace Security.Tests.SecurityInDatabaseTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var currentDirectory = Environment.CurrentDirectory;

            CreateDatabase();
            ExecuteDatabaseScript(ConfigurationManager.AppSettings["scriptPath"]);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DropDatabase();
        }

        private void DropDatabase()
        {
            using (var connection = new SqlConnection(GetMasterConnectionString()))
            {
                var dbName = GetDbName(GetConnectionString());
                connection.Execute($@"
alter database {dbName} set single_user with rollback immediate;
drop database {dbName};
");
            }
        }

        private void CreateDatabase()
        {
            var connectionString = GetConnectionString();
            var dbName = GetDbName(connectionString);

            using (var connection = new SqlConnection(GetMasterConnectionString()))
            {
                connection.Execute($"create database {dbName}");
            }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["securitydb"].ConnectionString;
        }

        private string GetMasterConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(GetConnectionString());
            builder.InitialCatalog = "master";
            return builder.ToString();
        }

        private string GetDbName(string connectionString)
        {
            var dbName = Regex.Match(connectionString, @"database=(?<dbName>[\w]+);|initial catalog=(?<dbName>[\w]+);", RegexOptions.IgnoreCase).Groups["dbName"].Value;
            return dbName;
        }

        private void ExecuteDatabaseScript(string scriptPath)
        {
            string sqlConnectionString = GetConnectionString();
            string script = File.ReadAllText(scriptPath);

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                Server server = new Server(new ServerConnection(conn));
                server.ConnectionContext.ExecuteNonQuery(script);
            }
        }
    }
}
