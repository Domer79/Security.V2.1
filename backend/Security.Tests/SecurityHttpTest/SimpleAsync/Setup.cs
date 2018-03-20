using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;
using Security.Contracts;
using Security.Dapper;
using Security.Model;
using SecurityHttp;
using Group = Security.Model.Group;
using User = Security.Model.User;

namespace Security.Tests.SecurityHttpTest.SimpleAsync
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            await CreateDatabase();
            await ExecuteDatabaseScript(ConfigurationManager.AppSettings["scriptPath"]);
            await FillDatabase();
        }

        [OneTimeTearDown]
        public Task OneTimeTearDown()
        {
            return DropDatabase();
        }

        private async Task FillDatabase()
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
                await security.Config.RegisterSecurityObjectsAsync("HelloWorldApp1", secObjects());

                await security.Config.RegisterApplicationAsync("HelloWorldApp2", "Hello World Application 2!");

                using (var security2 = new SecurityWebClient("HelloWorldApp2", "", IocConfig.GetLocator("HelloWorldApp2")))
                {
                    await security2.Config.RegisterSecurityObjectsAsync("HelloWorldApp2", "1", "4", "5", "6");
                }

                await CreateUsers(security);
                await CreateGroups(security);
                await CreateUserGroups(security);
                await CreateRoles(security);

                await security.MemberRoleRepository.AddMembersToRoleAsync(new[] { "user1", "group1" }, "role1");
                await security.MemberRoleRepository.AddMembersToRoleAsync(new[] { "user1" }, "role2");
                await security.MemberRoleRepository.AddMembersToRoleAsync(new[] { "user3" }, "role2");
                
                await security.GrantRepository.SetGrantAsync("role1", "1");
                await security.GrantRepository.SetGrantAsync("role1", "2");
                await security.GrantRepository.SetGrantAsync("role2", "2");
                await security.GrantRepository.SetGrantAsync("role2", "3");
            }
        }

        private async Task CreateUsers(ISecurity security)
        {
            for (int i = 0; i < 20; i++)
            {
                await security.UserRepository.CreateAsync(new User()
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

        private async Task CreateGroups(ISecurity security)
        {
            for (int i = 0; i < 20; i++)
            {
                await security.GroupRepository.CreateAsync(new Group()
                {
                    Name = $"group{i}",
                    Description = $"Group{i} Description"
                });
            }
        }

        private async Task CreateRoles(ISecurity security)
        {
            for (int i = 1; i <= 20; i++)
            {
                await security.RoleRepository.CreateAsync(new Role()
                {
                    Name = $"role{i}",
                    Description = $"Role{i} Description"
                });
            }
        }

        private async Task CreateUserGroups(ISecurity security)
        {
            var users = (await security.UserRepository.GetAsync()).ToList();
            var groups = (await security.GroupRepository.GetAsync()).ToList();

            await security.UserGroupRepository.AddUsersToGroupAsync(new []{users[0].Id, users[1].Id}, groups[0].Id);
            await security.UserGroupRepository.AddUsersToGroupAsync(new[] {users[0].IdMember, users[1].IdMember, users[2].IdMember}, groups[1].IdMember);
            await security.UserGroupRepository.AddUsersToGroupAsync(new[] {users[4].Login, users[6].Login, users[7].Login, users[12].Login}, groups[4].Name);

            await security.UserGroupRepository.AddGroupsToUserAsync(new []{groups[10].Id, groups[11].Id, groups[12].Id, groups[13].Id, }, users[1].Id);
            await security.UserGroupRepository.AddGroupsToUserAsync(new []{groups[12].IdMember, groups[15].IdMember, groups[18].IdMember, groups[4].IdMember, }, users[2].IdMember);
            await security.UserGroupRepository.AddGroupsToUserAsync(new []{groups[1].Name, groups[18].Name, groups[2].Name, groups[8].Name, }, users[7].Name);
        }

        private async Task DropDatabase()
        {
            using (var connection = new SqlConnection(GetMasterConnectionString()))
            {
                var dbName = GetDbName(GetConnectionString());
                await connection.ExecuteAsync($@"
alter database {dbName} set single_user with rollback immediate;
drop database {dbName};
");
            }
        }

        private async Task CreateDatabase()
        {
            var connectionString = GetConnectionString();
            var dbName = GetDbName(connectionString);

            using (var connection = new SqlConnection(GetMasterConnectionString()))
            {
                await connection.ExecuteAsync($"create database {dbName}");
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

        private async Task ExecuteDatabaseScript(string scriptPath)
        {
            string sqlConnectionString = GetConnectionString();
            string script = File.ReadAllText(scriptPath);

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                Server server = new Server(new ServerConnection(conn));
                server.ConnectionContext.ExecuteNonQuery(script);
                await Task.Delay(0);
            }
        }
    }
}
