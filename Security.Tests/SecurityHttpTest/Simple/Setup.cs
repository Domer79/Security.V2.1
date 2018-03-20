using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;
using Security.Dapper;
using Security.Model;
using Security.V2.Contracts;
using SecurityHttp;
using Group = Security.Model.Group;
using User = Security.Model.User;

namespace Security.Tests.SecurityHttpTest.Simple
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
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
                string[] secObjects()
                {
                    var list = new List<string>();
                    for (int i = 1; i < 21; i++)
                    {
                        list.Add(i.ToString());
                    }

                    return list.ToArray();
                }
                security.Config.RegisterSecurityObjects("HelloWorldApp1", secObjects());

                security.Config.RegisterApplication("HelloWorldApp2", "Hello World Application 2!");

                using (var security2 = new SecurityWebClient("HelloWorldApp2", "", IocConfig.GetLocator("HelloWorldApp2")))
                {
                    security2.Config.RegisterSecurityObjects("HelloWorldApp2", "1", "4", "5", "6");
                }

                CreateUsers(security);
                CreateGroups(security);
                CreateUserGroups(security);
                CreateRoles(security);

                security.MemberRoleRepository.AddMembersToRole(new[] { "user1", "group1" }, "role1");
                security.MemberRoleRepository.AddMembersToRole(new[] { "user1" }, "role2");
                security.MemberRoleRepository.AddMembersToRole(new[] { "user3" }, "role2");

                security.GrantRepository.SetGrant("role1", "1");
                security.GrantRepository.SetGrant("role1", "2");
                security.GrantRepository.SetGrant("role2", "2");
                security.GrantRepository.SetGrant("role2", "3");
            }
        }

        private void CreateUsers(ISecurity security)
        {
            for (int i = 0; i < 20; i++)
            {
                security.UserRepository.Create(new User()
                {
                    Login = $"user{i}",
                    Email = $"user{i}@mail.ru",
                    FirstName = $"User{i}First",
                    LastName = $"User{i}Last",
                    MiddleName = $"User{i}Middle",
                    Status = true,
                    DateCreated = DateTime.Now
                });
            }
        }

        private void CreateGroups(ISecurity security)
        {
            for (int i = 0; i < 20; i++)
            {
                security.GroupRepository.Create(new Group()
                {
                    Name = $"group{i}",
                    Description = $"Group{i} Description"
                });
            }
        }

        private void CreateRoles(ISecurity security)
        {
            for (int i = 1; i <= 20; i++)
            {
                security.RoleRepository.Create(new Role()
                {
                    Name = $"role{i}",
                    Description = $"Role{i} Description"
                });
            }
        }

        private void CreateUserGroups(ISecurity security)
        {
            var users = security.UserRepository.Get().ToList();
            var groups = security.GroupRepository.Get().ToList();

            security.UserGroupRepository.AddUsersToGroup(new []{users[0].Id, users[1].Id}, groups[0].Id);
            security.UserGroupRepository.AddUsersToGroup(new[] {users[0].IdMember, users[1].IdMember, users[2].IdMember}, groups[1].IdMember);
            security.UserGroupRepository.AddUsersToGroup(new[] {users[4].Login, users[6].Login, users[7].Login, users[12].Login}, groups[4].Name);

            security.UserGroupRepository.AddGroupsToUser(new []{groups[10].Id, groups[11].Id, groups[12].Id, groups[13].Id, }, users[1].Id);
            security.UserGroupRepository.AddGroupsToUser(new []{groups[12].IdMember, groups[15].IdMember, groups[18].IdMember, groups[4].IdMember, }, users[2].IdMember);
            security.UserGroupRepository.AddGroupsToUser(new []{groups[1].Name, groups[18].Name, groups[2].Name, groups[8].Name, }, users[7].Name);
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
