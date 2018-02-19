using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;
using Security.Dapper;
using Security.Model;
using Security.V2.Core;
using Group = Security.Model.Group;
using User = Security.Model.User;

namespace Security.Tests.SecurityInDatabaseTest.RepositoryTestsAsync
{
    [SetUpFixture]
    public class SetupAsync
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var currentDirectory = Environment.CurrentDirectory;

            CreateDatabase();
            ExecuteDatabaseScript(ConfigurationManager.AppSettings["scriptPath"]);
            FillDatabase();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DropDatabase();
        }

        private void FillDatabase()
        {
            using (var security = new MySecurity())
            {
                security.Config.RegisterSecurityObjectsAsync("HelloWorldApp1", "1", "2", "3");

                security.Config.RegisterApplicationAsync("HelloWorldApp2", "Hello World Application 2!");

                using (var security2 =
                    new V2.Core.Security("HelloWorldApp2", "", IocConfig.GetLocator("HelloWorldApp2")))
                {
                    security2.Config.RegisterSecurityObjectsAsync("HelloWorldApp2", "1", "4", "5", "6");
                }

                security.UserRepository.CreateAsync(new Model.User()
                {
                    Login = "user1",
                    Email = "user1@mail.ru",
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    MiddleName = "Ivanovich",
                    Status = true,
                    DateCreated = DateTime.Now
                });

                security.UserRepository.CreateAsync(new Model.User()
                {
                    Login = "user2",
                    Email = "user2@mail.ru",
                    FirstName = "Petr",
                    LastName = "Ivanov",
                    MiddleName = "Petrovich",
                    Status = true,
                    DateCreated = DateTime.Now
                });

                security.UserRepository.CreateAsync(new User()
                {
                    Login = "user3",
                    Email = "user3@mail.ru",
                    FirstName = "Petr",
                    LastName = "Ivanov",
                    MiddleName = "Petrovich",
                    Status = true,
                    DateCreated = DateTime.Now
                });

                security.GroupRepository.CreateAsync(new Group()
                {
                    Name = "group1",
                    Description = "Group1 Description"
                });

                security.GroupRepository.CreateAsync(new Group()
                {
                    Name = "group2",
                    Description = "Group2 Description"
                });

                security.UserGroupRepository.AddGroupsToUserAsync(new[] { "group1" }, "user1");
                security.UserGroupRepository.AddGroupsToUserAsync(new[] { "group1" }, "user2");

                security.UserGroupRepository.AddUsersToGroupAsync(new[] { "user2, user3" }, "group2");

                security.RoleRepository.CreateAsync(new Role()
                {
                    Name = "role1",
                    Description = "Role1 Description"
                });

                security.RoleRepository.CreateAsync(new Role()
                {
                    Name = "role2",
                    Description = "Role2 Description"
                });

                security.MemberRoleRepository.AddMembersToRoleAsync(new[] { "user1", "group1" }, "role1");
                security.MemberRoleRepository.AddMembersToRoleAsync(new[] { "user1" }, "role2");
                security.MemberRoleRepository.AddMembersToRoleAsync(new[] { "user3" }, "role2");

                security.GrantRepository.SetGrantAsync("role1", "1");
                security.GrantRepository.SetGrantAsync("role1", "2");
                security.GrantRepository.SetGrantAsync("role2", "2");
                security.GrantRepository.SetGrantAsync("role2", "3");
            }
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
