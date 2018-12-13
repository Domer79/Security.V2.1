using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Extensions;
using Security.Model;

namespace Security.Tests.Scenarios
{
    /// <summary>
    /// Этот сценарий наполняет базу данных всеми необходимыми для тестов данными
    /// </summary>
    public class FillDatabaseScenario: BaseScenario<FillDatabaseScenarioResult>
    {
        private readonly string _password;
        private FillDatabaseScenarioResult _scenarioResult;

        public FillDatabaseScenario(string password = FillDatabaseScenarioResult.DefaultPassword)
        {
            _password = password;
        }

        protected override async Task<FillDatabaseScenarioResult> _RunAsync(ISecurity security, Func<object, Task<object>> someTask)
        {
            var result = new FillDatabaseScenarioResult();
            for (int i = 100; i < 105; i++)
            {
                var user = CreateUser($"user{i}");
                user = await security.UserRepository.CreateAsync(user);
                result.Users.Add(user);
                await security.SetPasswordAsync(user.Login, _password);
            }

            for (int i = 100; i < 102; i++)
            {
                var group = new Group()
                {
                    Name = $"group{i}",
                    Description = $"Group{i} description"
                };

                group = await security.GroupRepository.CreateAsync(group);
                result.Groups.Add(group);
            }

            for (int i = 100; i < 102; i++)
            {
                var role = new Role()
                {
                    Name = $"role{i}",
                    Description = $"Role{i} description"
                };

                role = await security.RoleRepository.CreateAsync(role);
                result.Roles.Add(role);
            }

            for (int i = 100; i < 102; i++)
            {
                var policy = new SecObject()
                {
                    ObjectName = i.ToString()
                };

                policy = await security.SecObjectRepository.CreateAsync(policy);
                result.Policies.Add(policy);
            }

            var j = 0;
            foreach (var @group in result.Groups)
            {
                if (j == 0)
                {
                    await security.UserGroupRepository.AddUsersToGroupAsync(result.Users.Take(2).Select(_ => _.IdMember).ToArray(), @group.IdMember);
                    continue;
                }

                await security.UserGroupRepository.AddUsersToGroupAsync(result.Users.Skip(2).Select(_ => _.IdMember).ToArray(), @group.IdMember);

                j++;
            }

            foreach (var @group in result.Groups)
            {
                result.GroupUsers[@group] = await security.UserGroupRepository.GetUsersAsync(@group.IdMember).ToArrayAsync();
            }

            foreach (var user in result.Users)
            {
                result.UserGroups[user] = await security.UserGroupRepository.GetGroupsAsync(user.IdMember).ToArrayAsync();
            }

            j = 0;
            foreach (var role in result.Roles)
            {
                if (j == 0)
                {
                    await security.MemberRoleRepository.AddMembersToRoleAsync(result.Users.Take(2).Select(_ => _.IdMember).ToArray(), role.IdRole);
                    await security.MemberRoleRepository.AddMembersToRoleAsync(new[] { result.Groups[0].IdMember }, role.IdRole);
                    continue;
                }
                await security.MemberRoleRepository.AddMembersToRoleAsync(result.Users.Skip(2).Select(_ => _.IdMember).ToArray(), role.IdRole);
                await security.MemberRoleRepository.AddMembersToRoleAsync(new[] { result.Groups[1].IdMember }, role.IdRole);

                j++;
            }

            foreach (var group in result.Groups)
            {
                result.MemberRoles[new Member() { Id = group.Id, IdMember = group.IdMember, Name = group.Name }] =
                    await security.MemberRoleRepository.GetRolesAsync(group.IdMember).ToArrayAsync();
            }

            foreach (var user in result.Users)
            {
                result.MemberRoles[new Member() { Id = user.Id, IdMember = user.IdMember, Name = user.Name }] =
                    await security.MemberRoleRepository.GetRolesAsync(user.IdMember).ToArrayAsync();
            }

            foreach (var role in result.Roles)
            {
                result.RoleMembers[role] = await security.MemberRoleRepository.GetMembersAsync(role.IdRole).ToArrayAsync();
            }

            _scenarioResult = result;
            return result;
        }

        protected override async Task _RollbackAsync(ISecurity security)
        {
            foreach (var policy in _scenarioResult.Policies)
            {
                await security.SecObjectRepository.RemoveAsync(policy.IdSecObject);
            }

            foreach (var role in _scenarioResult.Roles)
            {
                await security.RoleRepository.RemoveAsync(role.IdRole);
            }

            foreach (var @group in _scenarioResult.Groups)
            {
                await security.GroupRepository.RemoveAsync(@group.IdMember);
            }

            foreach (var user in _scenarioResult.Users)
            {
                await security.UserRepository.RemoveAsync(user.IdMember);
            }
        }

        protected override FillDatabaseScenarioResult _Run(ISecurity security, Func<object, Task<object>> someTask)
        {
            var result = new FillDatabaseScenarioResult();
            for (int i = 100; i < 105; i++)
            {
                var user = CreateUser($"user{i}");
                user = security.UserRepository.Create(user);
                result.Users.Add(user);
                security.SetPassword(user.Login, _password);
            }

            for (int i = 100; i < 102; i++)
            {
                var group = new Group()
                {
                    Name = $"group{i}",
                    Description = $"Group{i} description"
                };

                group = security.GroupRepository.Create(group);
                result.Groups.Add(group);
            }

            for (int i = 100; i < 102; i++)
            {
                var role = new Role()
                {
                    Name = $"role{i}",
                    Description = $"Role{i} description"
                };

                role = security.RoleRepository.Create(role);
                result.Roles.Add(role);
            }

            for (int i = 100; i < 102; i++)
            {
                var policy = new SecObject()
                {
                    ObjectName = i.ToString()
                };

                policy = security.SecObjectRepository.Create(policy);
                result.Policies.Add(policy);
            }

            var j = 0;
            foreach (var @group in result.Groups)
            {
                if (j == 0)
                {
                    security.UserGroupRepository.AddUsersToGroup(result.Users.Take(2).Select(_ => _.IdMember).ToArray(), @group.IdMember);
                    continue;
                }

                security.UserGroupRepository.AddUsersToGroup(result.Users.Skip(2).Select(_ => _.IdMember).ToArray(), @group.IdMember);

                j++;
            }

            foreach (var @group in result.Groups)
            {
                result.GroupUsers[@group] = security.UserGroupRepository.GetUsers(@group.IdMember).ToArray();
            }

            foreach (var user in result.Users)
            {
                result.UserGroups[user] = security.UserGroupRepository.GetGroups(user.IdMember).ToArray();
            }

            j = 0;
            foreach (var role in result.Roles)
            {
                if (j == 0)
                {
                    security.MemberRoleRepository.AddMembersToRole(result.Users.Take(2).Select(_ => _.IdMember).ToArray(), role.IdRole);
                    security.MemberRoleRepository.AddMembersToRole(new[] {result.Groups[0].IdMember}, role.IdRole);
                    continue;
                }
                security.MemberRoleRepository.AddMembersToRole(result.Users.Skip(2).Select(_ => _.IdMember).ToArray(), role.IdRole);
                security.MemberRoleRepository.AddMembersToRole(new[] { result.Groups[1].IdMember }, role.IdRole);

                j++;
            }

            foreach (var group in result.Groups)
            {
                result.MemberRoles[new Member() {Id = group.Id, IdMember = group.IdMember, Name = group.Name}] =
                    security.MemberRoleRepository.GetRoles(group.IdMember).ToArray();
            }

            foreach (var user in result.Users)
            {
                result.MemberRoles[new Member() {Id = user.Id, IdMember = user.IdMember, Name = user.Name}] =
                    security.MemberRoleRepository.GetRoles(user.IdMember).ToArray();
            }

            foreach (var role in result.Roles)
            {
                result.RoleMembers[role] = security.MemberRoleRepository.GetMembers(role.IdRole).ToArray();
            }

            _scenarioResult = result;
            return result;
        }

        protected override void _Rollback(ISecurity security)
        {
            foreach (var policy in _scenarioResult.Policies)
            {
                security.SecObjectRepository.Remove(policy.IdSecObject);
            }

            foreach (var role in _scenarioResult.Roles)
            {
                security.RoleRepository.Remove(role.IdRole);
            }

            foreach (var @group in _scenarioResult.Groups)
            {
                security.GroupRepository.Remove(@group.IdMember);
            }

            foreach (var user in _scenarioResult.Users)
            {
                security.UserRepository.Remove(user.IdMember);
            }
        }

        User CreateUser(string login)
        {
            var user = new User()
            {
                Login = login,
                Email = $"{login}@mail.ru",
                FirstName = login,
                LastName = login,
                DateCreated = DateTime.UtcNow,
                Status = true
            };

            return user;
        }
    }

    public class FillDatabaseScenarioResult
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<Group> Groups { get; set; } = new List<Group>();
        public Dictionary<User, Group[]> UserGroups { get; set; } = new Dictionary<User, Group[]>();
        public Dictionary<Group, User[]> GroupUsers { get; set; } = new Dictionary<Group, User[]>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<SecObject> Policies { get; set; } = new List<SecObject>();
        public Dictionary<Member, Role[]> MemberRoles { get; set; } = new Dictionary<Member, Role[]>();
        public Dictionary<Role, Member[]> RoleMembers { get; set; } = new Dictionary<Role, Member[]>();

        public const string User1 = "user100";
        public const string User2 = "user101";
        public const string User3 = "user102";
        public const string User4 = "user103";
        public const string User5 = "user104";
        public const string Group1 = "group100";
        public const string Group2 = "group101";
        public const string Role1 = "role100";
        public const string Role2 = "role101";
        public const string Policy1 = "100";
        public const string Policy2 = "101";
        public const string DefaultPassword = "password";
        public const string AppName = "HelloWorldApp1";
    }
}
