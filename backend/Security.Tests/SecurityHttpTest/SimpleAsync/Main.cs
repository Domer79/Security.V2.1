using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;
using Security.Tests.Infrastructure;
using Security.Tests.Scenarios;
using SecurityHttp;
using SecurityHttp.Interfaces;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace Security.Tests.SecurityHttpTest.SimpleAsync
{
    [TestFixture]
    public class Main : BaseTest
    {
        private ISecurity _security;
        private ICommonWeb _commonDb;

        [SetUp]
        public Task Setup()
        {
            _security = new MySecurity();
            _commonDb = ServiceLocator.Resolve<ICommonWeb>();
            return Task.Delay(0);
        }

        [TearDown]
        public Task TearDown()
        {
            _security.Dispose();
            return Task.Delay(0);
        }

        [TestCase("HelloWorldApp1", "Hello World Application 1!")]
        public async Task RegisterApplicationTest(string name, string description)
        {
            var application = await _security.ApplicationRepository.GetByNameAsync(name);

            Assert.That(application, Has.Property("AppName").EqualTo(name));
            Assert.That(application, Has.Property("Description").EqualTo(description));
        }

        [TestCase("HelloWorldApp1", "1")]
        [TestCase("HelloWorldApp1", "2")]
        [TestCase("HelloWorldApp1", "3")]
        public async Task SecObjectExistenceTest(string appName, string objectName)
        {
            using (var security = new Core.Security(appName, "", IocConfig.GetLocator(appName)))
            {
                var secObject = await security.SecObjectRepository.GetByNameAsync(objectName);
                Assert.That(secObject, Is.Not.Null);
            }
        }

        [Test]
        public async Task PasswordValidateTest()
        {
            var user = _security.UserRepository.Create(new User()
            {
                Login = "testadmin",
                Email = "testadmin@mail.ru",
                FirstName = "testadmin",
                LastName = "testadmin",
                DateCreated = DateTime.UtcNow,
                Status = true
            });
            Assert.That(async () => await _security.SetPasswordAsync("testadmin", "testadmin"), Is.True);
            Assert.That(async () => await _security.UserValidateAsync("testadmin", "testadmin"), Is.True);
            Assert.That(async () => await _security.UserValidateAsync("testadmin@mail.ru", "testadmin"), Is.True);
        }

        [Test]
        public async Task User_MemberFields_ValidationTest()
        {
            using (var scenario = new FillDatabaseScenario())
            {
                await scenario.RunAsync(_security);
                var user = await _security.UserRepository.GetByNameAsync(FillDatabaseScenarioResult.User1);

                Assert.That(user, Has.Property("Login").EqualTo("user100"));
                Assert.That(user, Has.Property("Name").EqualTo("user100"));
                Assert.That(user, Has.Property("Email").EqualTo("user100@mail.ru"));
                Assert.That(user, Has.Property("FirstName").EqualTo("user100"));
                Assert.That(user, Has.Property("LastName").EqualTo("user100"));
                Assert.That(user, Has.Property("Status").EqualTo(true));
                Assert.That(user, Has.Property("DateCreated").EqualTo(DateTime.UtcNow).Within(TimeSpan.FromMinutes(1)));
            }
        }

        [Test]
        public async Task Group_MemberFields_ValidationTest()
        {
            var group = await _security.GroupRepository.GetByNameAsync("group1");

            Assert.That(group, Has.Property("Name").EqualTo("group1"));
            Assert.That(group, Has.Property("Description").EqualTo("Group1 Description"));
        }

        [Test]
        public async Task Member_MemberFields_ValidationTest()
        {
            var members = new List<string>();

            members.AddRange((await _security.UserRepository.GetAsync()).Select(m => m.Name));
            members.AddRange((await _security.GroupRepository.GetAsync()).Select(m => m.Name));

            var list = new List<string>();
            list.AddRange((await _commonDb.GetCollectionAsync<User>("api/user")).Select(_ => _.Name ));
            list.AddRange((await _commonDb.GetCollectionAsync<Group>("api/groups")).Select(_ => _.Name ));

            CollectionAssert.IsNotEmpty(members);
            CollectionAssert.AreEqual(list, members);
        }

        [Test]
        public async Task Role_MemberFields_ValidationTest()
        {
            var role = await _security.RoleRepository.GetByNameAsync("role1");

            Assert.That(role, Has.Property("Name").EqualTo("role1"));
            Assert.That(role, Has.Property("Description").EqualTo("Role1 Description"));
        }

        [TestCase("user1 user2 group1", "role1")]
        [TestCase("user1 user3", "role2")]
        public async Task Check_Existence_Members_in_Role(string member, string role)
        {
            var members = (await _security.MemberRoleRepository.GetMembersAsync(role)).Select(m => m.Name);
            CollectionAssert.AreEqual(member.Split(' '), members);
        }

        [TestCase("role1 role2", "user1")]
        [TestCase("role1", "group1")]
        public async Task CheckExistsence_Roles_in_User(string roleNames, string member)
        {
            var roles = (await _security.MemberRoleRepository.GetRolesAsync(member)).Select(_ => _.Name);
            CollectionAssert.AreEqual(roleNames.Split(' '), roles);
        }

        [TestCase("user1", "1")]
        [TestCase("user1", "2")]
        [TestCase("user1", "3")]
        [TestCase("user1@mail.ru", "1")]
        [TestCase("user1@mail.ru", "2")]
        [TestCase("user1@mail.ru", "3")]
        [TestCase("user2", "1")]
        [TestCase("user2", "2")]
        [TestCase("user3", "2")]
        [TestCase("user3", "3")]
        public Task CheckAccessToMemberTest(string loginOrEmail, string objectName)
        {
            Assert.That(() => _security.CheckAccessAsync(loginOrEmail, objectName), Is.True);
            return Task.Delay(0);
        }

        [TestCase("user2", "3")]
        [TestCase("user3", "1")]
        public Task CheckNotAccessToMemberTest(string loginOrEmail, string objectName)
        {
            Assert.That(() => _security.CheckAccessAsync(loginOrEmail, objectName), Is.False);
            return Task.Delay(0);
        }

        #region UserGroupRepository testing

        [TestCase("user1", "group10,group11,group12,group13")]
        public async Task CheckGroupListInUser(string userName, string groupsByDelimiters)
        {
            var groups = (await _security.GroupRepository.GetAsync()).Where(_ => groupsByDelimiters.Split(',').Contains(_.Name));

            var member = await _security.UserRepository.GetByNameAsync(userName);

            var membersByIdMember = await _security.UserGroupRepository.GetGroupsAsync(member.IdMember);
            var membersByGuid = await _security.UserGroupRepository.GetGroupsAsync(member.Id);
            var membersByName = await _security.UserGroupRepository.GetGroupsAsync(member.Name);

            CollectionAssert.AreEqual(membersByIdMember, membersByGuid, new GroupComparer());
            CollectionAssert.AreEqual(membersByIdMember, membersByName, new GroupComparer());
        }

        [TestCase("group4", "user2, user4,user6,user7,user12")]
        public async Task CheckUserListInGroup(string groupName, string usersByDelimiters)
        {
            var users = (await _security.UserRepository.GetAsync()).Where(_ => usersByDelimiters.Split(',').Contains(_.Name));

            var member = await _security.GroupRepository.GetByNameAsync(groupName);

            var membersByIdMember = await _security.UserGroupRepository.GetUsersAsync(member.IdMember);
            var membersByGuid = await _security.UserGroupRepository.GetUsersAsync(member.Id);
            var membersByName = await _security.UserGroupRepository.GetUsersAsync(member.Name);

            CollectionAssert.IsNotEmpty(membersByIdMember);
            CollectionAssert.IsNotEmpty(membersByGuid);
            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(membersByIdMember, membersByGuid, new UserComparer());
            CollectionAssert.AreEqual(membersByIdMember, membersByName, new UserComparer());
        }

        [TestCase("group4", "user2, user4,user6,user7,user12")]
        [TestCase("group1", "user0,user1,user2, user7")]
        public async Task CheckNonIncludedUsers(string groupName, string exceptUsersByDelimiters)
        {
            var users = (await _security.UserRepository.GetAsync()).Where(_ => !exceptUsersByDelimiters.Split(',').Contains(_.Name));

            var member = await _security.GroupRepository.GetByNameAsync(groupName);

            var membersByIdMember = await _security.UserGroupRepository.GetNonIncludedUsersAsync(member.IdMember);
            var membersByGuid = await _security.UserGroupRepository.GetNonIncludedUsersAsync(member.Id);
            var membersByName = await _security.UserGroupRepository.GetNonIncludedUsersAsync(member.Name);

            CollectionAssert.IsNotEmpty(membersByIdMember);
            CollectionAssert.IsNotEmpty(membersByGuid);
            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(membersByIdMember, membersByGuid, new UserComparer());
            CollectionAssert.AreEqual(membersByIdMember, membersByName, new UserComparer());
        }

        [TestCase("user7", "group1,group4,group18,group2,group8")]
        [TestCase("user1", "group0,group1,group10,group11,group12,group13")]
        public async Task CheckNonIncludedGroups(string userName, string exceptGroupsByDelimiters)
        {
            var groups = (await _security.GroupRepository.GetAsync()).Where(_ => !exceptGroupsByDelimiters.Split(',').Contains(_.Name));

            var member = await _security.UserRepository.GetByNameAsync(userName);

            var membersByIdMember = await _security.UserGroupRepository.GetNonIncludedGroupsAsync(member.IdMember);
            var membersByGuid = await _security.UserGroupRepository.GetNonIncludedGroupsAsync(member.Id);
            var membersByName = await _security.UserGroupRepository.GetNonIncludedGroupsAsync(member.Name);

            CollectionAssert.IsNotEmpty(membersByIdMember);
            CollectionAssert.IsNotEmpty(membersByGuid);
            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(membersByIdMember, membersByGuid, new GroupComparer());
            CollectionAssert.AreEqual(membersByIdMember, membersByName, new GroupComparer());
        }

        /// <summary>
        /// По логину формируются значения всех остальных обязательных полей
        /// </summary>
        /// <param name="login"></param>
        [TestCase("user21")]
        public async Task TestCreateUser(string login)
        {
            var user = new User
            {
                Login = $"{login}",
                Email = $"{login}@mail.ru",
                FirstName = $"{login}First",
                LastName = $"{login}Last",
                MiddleName = $"{login}Middle",
                Status = true,
                DateCreated = DateTime.Now
            };
            user = await _security.UserRepository.CreateAsync(user);

            Assert.AreNotEqual(Guid.Empty, user.Id);
        }

        /// <summary>
        /// testCase = 1 - удаление по имени
        /// testCase = 2 - удаление по Guid
        /// testCase = 3 - удаление по IdMember
        /// </summary>
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CheckRemoveUsersInGroup(int testCase)
        {
            using (var scenario = new FillDatabaseScenario())
            {
                var result = await scenario.RunAsync(_security);
                var group = result.Groups.Single(_ => _.Name == FillDatabaseScenarioResult.Group1);

                IEnumerable<User> groupUsers = null;
                if (testCase == 1)
                {
                    await _security.UserGroupRepository.RemoveUsersFromGroupAsync(result.GroupUsers[group].Select(_ => _.Name).ToArray(), group.Name);
                    groupUsers = await _security.UserGroupRepository.GetUsersAsync(group.Name);
                }

                if (testCase == 2)
                {
                    await _security.UserGroupRepository.RemoveUsersFromGroupAsync(result.GroupUsers[group].Select(_ => _.Id).ToArray(), group.Id);
                    groupUsers = await _security.UserGroupRepository.GetUsersAsync(group.Id);
                }

                if (testCase == 3)
                {
                    await _security.UserGroupRepository.RemoveUsersFromGroupAsync(result.GroupUsers[group].Select(_ => _.IdMember).ToArray(), group.IdMember);
                    groupUsers = await _security.UserGroupRepository.GetUsersAsync(group.IdMember);
                }

                Assert.IsNotNull(groupUsers);
                CollectionAssert.IsEmpty(groupUsers);
            }
        }

        /// <summary>
        /// testCase = 1 - удаление по имени
        /// testCase = 2 - удаление по Guid
        /// testCase = 3 - удаление по IdMember
        /// </summary>
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CheckRemoveGroupsInUser(int testCase)
        {
            using (var scenario = new FillDatabaseScenario())
            {
                var result = await scenario.RunAsync(_security);
                var user = result.Users.Single(_ => _.Login == FillDatabaseScenarioResult.User1);

                IEnumerable<Group> userGroups = null;
                if (testCase == 1)
                {
                    await _security.UserGroupRepository.RemoveGroupsFromUserAsync(result.UserGroups[user].Select(_ => _.Name).ToArray(), user.Login);
                    userGroups = await _security.UserGroupRepository.GetGroupsAsync(user.Login);
                }

                if (testCase == 2)
                {
                    await _security.UserGroupRepository.RemoveGroupsFromUserAsync(result.UserGroups[user].Select(_ => _.Id).ToArray(), user.Id);
                    userGroups = await _security.UserGroupRepository.GetGroupsAsync(user.Id);
                }

                if (testCase == 3)
                {
                    await _security.UserGroupRepository.RemoveGroupsFromUserAsync(result.UserGroups[user].Select(_ => _.IdMember).ToArray(), user.IdMember);
                    userGroups = await _security.UserGroupRepository.GetGroupsAsync(user.IdMember);
                }

                Assert.IsNotNull(userGroups);
                CollectionAssert.IsEmpty(userGroups);
            }
        }

        #endregion

        #region UserRepository testing

        [Test]
        public async Task CreateUserTest()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = "Domer3",
                FirstName = "Damir3",
                LastName = "Garipov2",
                MiddleName = "Sagdievich",
                Email = "garipov3@mail.ru",
                PasswordSalt = Guid.NewGuid().ToString("N"),
                DateCreated = DateTime.Now
            };

            var readyUser = await _security.UserRepository.CreateAsync(user);
            Assert.That(readyUser, Is.InstanceOf(typeof(User)));
            Assert.That(readyUser, Has.Property("Login").EqualTo("Domer3"));
            Assert.DoesNotThrowAsync(() =>
            {
                return _security.UserRepository.RemoveAsync(readyUser.IdMember);
            });
        }

        [Test]
        public async Task CreateEmptyUserTest()
        {
            var user = await _security.UserRepository.CreateEmptyAsync("new_user");
            await _security.UserRepository.RemoveAsync(user.IdMember);

            Assert.That(user, Is.InstanceOf(typeof(User)));
            Assert.That(user, Has.Property("Login").StartWith("new_user"));
            Assert.That(user, Has.Property("FirstName").StartWith("new_user"));
            Assert.That(user, Has.Property("LastName").StartWith("new_user"));
            Assert.That(user, Has.Property("Email").StartWith("new_user"));
        }

        [Test]
        public async Task CheckGetAllUsers()
        {
            var users = await _security.UserRepository.GetAsync();

            CollectionAssert.IsNotEmpty(users);
            Assert.That(users.Count(), Is.EqualTo(21));
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public async Task UpdateUser_And_Check_UpdatedPropertiesTest(int idMember)
        {
            var user = await _security.UserRepository.GetAsync(idMember);

            user.Login = $"new_login{idMember}";
            user.FirstName = "new_FirstName";
            user.LastName = "new_LastName";
            user.MiddleName = "new_MiddleName";
            user.Email = $"new_email{idMember}@mail.ru";
            user.DateCreated = DateTime.Now;
            user.LastActivityDate = DateTime.Now;
            var userPasswordSalt = Guid.NewGuid().ToString("N");
            user.PasswordSalt = userPasswordSalt;
            var userStatus = !user.Status;
            user.Status = userStatus;

            await _security.UserRepository.UpdateAsync(user);

            user = await _security.UserRepository.GetAsync(idMember);

            Assert.IsNotNull(user);

            Assert.That(user, Has.Property("Login").EqualTo($"new_login{idMember}"));
            Assert.That(user, Has.Property("FirstName").EqualTo("new_FirstName"));
            Assert.That(user, Has.Property("LastName").EqualTo("new_LastName"));
            Assert.That(user, Has.Property("MiddleName").EqualTo("new_MiddleName"));
            Assert.That(user, Has.Property("Email").EqualTo($"new_email{idMember}@mail.ru"));
            Assert.That(user, Has.Property("DateCreated").EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
            Assert.That(user, Has.Property("LastActivityDate").EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
            Assert.That(user, Has.Property("PasswordSalt").EqualTo(userPasswordSalt));
            Assert.That(user, Has.Property("Status").EqualTo(userStatus));
        }

        [Test]
        public async Task SetUserStatusTest()
        {
            await _security.UserRepository.SetStatusAsync("user0", false);
            var user = await _security.UserRepository.GetByNameAsync("user0");

            Assert.IsFalse(user.Status);
        }

        [TestCase(21)]
        public async Task RemoveUserTest(int idMember)
        {
            var user = new User()
            {
                Login = $"user{idMember}",
                Email = $"user{idMember}@mail.ru",
                FirstName = $"User{idMember}First",
                LastName = $"User{idMember}Last",
                MiddleName = $"User{idMember}Middle",
                Status = true,
                DateCreated = DateTime.Now
            };
            user = await _security.UserRepository.CreateAsync(user);

            var group = new Group
            {
                Name = $"Group{idMember}"
            };
            group = await _security.GroupRepository.CreateAsync(group);

            var role = new Role()
            {
                Name = $"Role{idMember}"
            };
            role = await _security.RoleRepository.CreateAsync(role);

            await _security.UserGroupRepository.AddGroupsToUserAsync(new []{group.Name}, user.Login);
            await _security.MemberRoleRepository.AddRolesToMemberAsync(new []{role.Name}, user.Name);

            Assert.DoesNotThrowAsync(() => _security.UserRepository.RemoveAsync(user.IdMember));
            Assert.DoesNotThrowAsync(() => _security.RoleRepository.RemoveAsync(role.IdRole));
        }

        #endregion

        #region GroupRepository testing

        [Test]
        public async Task CreateEmptyGroupTest()
        {
            var group = await _security.GroupRepository.CreateEmptyAsync("new_Group");
            await _security.GroupRepository.RemoveAsync(group.IdMember);
            Assert.That(group, Has.Property("Name").StartWith("new_Group"));
        }

        [Test]
        public async Task GetAllGroupsTest()
        {
            var groups = await _security.GroupRepository.GetAsync();

            CollectionAssert.IsNotEmpty(groups);
            Assert.That(groups.Count(), Is.EqualTo(20));
        }

        [TestCase(22)]
        [TestCase(23)]
        [TestCase(24)]
        [TestCase(25)]
        [TestCase(26)]
        public async Task UpdateGroup_And_Check_UpdatedPropertiesTest(int idMember)
        {
            var group = await _security.GroupRepository.GetAsync(idMember);

            group.Name = $"new_Name{idMember}";
            group.Description = "new_Description";

            await _security.GroupRepository.UpdateAsync(group);

            group = await _security.GroupRepository.GetAsync(idMember);

            Assert.IsNotNull(group);

            Assert.That(group, Has.Property("Name").EqualTo($"new_Name{idMember}"));
            Assert.That(group, Has.Property("Description").EqualTo("new_Description"));
        }

        [TestCase(41)]
        public async Task RemoveGroupTest(int idMember)
        {
            var group = new Group()
            {
                Name = $"Group{idMember}",
            };
            group = await _security.GroupRepository.CreateAsync(group);

            var user = await _security.UserRepository.CreateEmptyAsync("new_user");

            var role = await _security.RoleRepository.CreateEmptyAsync("new_role");

            await _security.UserGroupRepository.AddUsersToGroupAsync(new[] { user.Name }, group.Name);
            await _security.MemberRoleRepository.AddRolesToMemberAsync(new[] { role.Name }, group.Name);

            Assert.DoesNotThrowAsync(() => _security.GroupRepository.RemoveAsync(group.IdMember));
            Assert.DoesNotThrowAsync(() => _security.RoleRepository.RemoveAsync(role.IdRole));
        }

        #endregion

        #region SecObjectRepository testing

        [Test]
        public async Task SecObjectCreateEmptyTest()
        {
            var policy = await _security.SecObjectRepository.CreateEmptyAsync("new_Policy");
            await _security.SecObjectRepository.RemoveAsync(policy.IdSecObject);

            Assert.That(policy, Has.Property("ObjectName").StartWith("new_Policy"));
        }

        [Test]
        public async Task SecObjectGetAllTest()
        {
            var policies = await _security.SecObjectRepository.GetAsync();

            CollectionAssert.IsNotEmpty(policies);
            Assert.That(policies.Count(), Is.EqualTo(20));
        }

        [TestCase(FillDatabaseScenarioResult.Policy1)]
        [TestCase(FillDatabaseScenarioResult.Policy2)]
        public async Task SecObjectUpdate_And_Check_UpdatedPropertiesTest(string policy)
        {
            using (var scenario = new FillDatabaseScenario())
            {
                var result = await scenario.RunAsync(_security);
                var secObject = await _security.SecObjectRepository.GetByNameAsync(policy);

                secObject.ObjectName = $"new_Policy{secObject.IdSecObject}";

                await _security.SecObjectRepository.UpdateAsync(secObject);

                secObject = await _security.SecObjectRepository.GetAsync(secObject.IdSecObject);

                Assert.IsNotNull(secObject);
                Assert.That(secObject, Has.Property("ObjectName").EqualTo($"new_Policy{secObject.IdSecObject}"));
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task SecObject_GetById_AnotherApplication(int idSecObject)
        {
            var secObject = await _security.SecObjectRepository.GetAsync(idSecObject);
            Assert.That(secObject, Is.Null);
        }

        [TestCase(31)]
        public async Task SecObjectRemoveTest(int idSecObject)
        {
            var policy = new SecObject()
            {
                ObjectName = $"Policy{idSecObject}",
            };
            policy = await _security.SecObjectRepository.CreateAsync(policy);

            var role = await _security.RoleRepository.CreateEmptyAsync("new_role");

            await _security.GrantRepository.SetGrantsAsync(role.Name, new[] { policy.ObjectName });

            Assert.DoesNotThrowAsync(() => _security.SecObjectRepository.RemoveAsync(policy.IdSecObject));
            Assert.DoesNotThrowAsync(() => _security.RoleRepository.RemoveAsync(role.IdRole));
        }

        [Test]
        public async Task RegisterExistingPolicy()
        {
            using (var scenario = new FillDatabaseScenario())
            {
                await scenario.RunAsync(_security);
                Assert.DoesNotThrowAsync(async () =>
                {
                    await _security.Config.RegisterSecurityObjectsAsync(FillDatabaseScenarioResult.AppName, new[] { FillDatabaseScenarioResult.Policy1 });
                });
            }
        }

        #endregion

        #region RoleRepository testing

        [Test]
        public async Task RoleCreateEmptyTest()
        {
            var role = await _security.RoleRepository.CreateEmptyAsync("new_Role");
            await _security.RoleRepository.RemoveAsync(role.IdRole);

            Assert.That(role, Has.Property("Name").StartWith("new_Role"));
        }

        [Test]
        public async Task RoleGetAllTest()
        {
            var roles = await _security.RoleRepository.GetAsync();

            CollectionAssert.IsNotEmpty(roles);
            Assert.That(roles.Count(), Is.EqualTo(20));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task RolesUpdate_And_Check_UpdatedPropertiesTest(int idRole)
        {
            var role = await _security.RoleRepository.GetAsync(idRole);

            role.Name = $"new_Role{idRole}";

            await _security.RoleRepository.UpdateAsync(role);

            role = await _security.RoleRepository.GetAsync(idRole);

            Assert.IsNotNull(role);

            Assert.That(role, Has.Property("Name").EqualTo($"new_Role{idRole}"));
        }

        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [TestCase(24)]
        public async Task Role_GetById_AnotherApplication(int idRole)
        {
            var role = await _security.RoleRepository.GetAsync(idRole);

            Assert.That(role, Is.Null);
        }

        [TestCase(31)]
        public async Task RoleRemoveTest(int idSecObject)
        {
            var role = new Role()
            {
                Name = $"Policy{idSecObject}",
            };
            role = await _security.RoleRepository.CreateAsync(role);

            var secObject = await _security.SecObjectRepository.CreateEmptyAsync("new_Policy");

            await _security.GrantRepository.SetGrantsAsync(role.Name, new[] { secObject.ObjectName });
            await _security.MemberRoleRepository.AddMembersToRoleAsync(new[] {"user1"}, role.Name);

            Assert.DoesNotThrowAsync(() => _security.RoleRepository.RemoveAsync(role.IdRole));
            Assert.DoesNotThrowAsync(() => _security.SecObjectRepository.RemoveAsync(secObject.IdSecObject));
        }

        #endregion

        #region ApplicationInternalRepository testing

        [Test]
        public async Task ApplicationInternal_Get_Update_And_Remove_ApplicationTest()
        {
            IServiceLocator locator = IocConfig.GetLocator("MyNewTestApp");
            using (var security = new SecurityWebClient("MyNewTestApp", "MyNewTestApp Description", locator))
            {
                await security.Config.RegisterSecurityObjectsAsync("MyNewTestApp", "1", "2", "3", "4", "5", "6", "7", "8");
                var user = await security.UserRepository.GetByNameAsync("user1");

                var group = await security.GroupRepository.GetByNameAsync("group1");

                var role = await security.RoleRepository.CreateAsync(new Role(){Name = "Role1"});
                var policy = await security.SecObjectRepository.CreateAsync(new SecObject() {ObjectName = "policy1"});

                await security.MemberRoleRepository.AddMembersToRoleAsync(new[] {user.IdMember, group.IdMember}, role.IdRole);
            }

            var repo = locator.Resolve<IApplicationInternalRepository>();
            var apps = await repo.GetAsync();

            foreach (var app in apps)
            {
                var oldDescription = app.Description;
                app.Description = $"New {app.AppName} Description 2.0";
                await repo.UpdateAsync(app);
                var application = await repo.GetAsync(app.IdApplication);

                Assert.That(application, Has.Property("Description").EqualTo(app.Description));
                Assert.That(application, Is.Not.Null);

                app.Description = oldDescription;
                await repo.UpdateAsync(app);
            }

            Assert.DoesNotThrowAsync(async() =>
            {
                await _security.Config.RemoveApplicationAsync("MyNewTestApp");
                await repo.RemoveAsync(apps.First(_ => _.AppName == "HelloWorldApp2").IdApplication);
            });

            Assert.ThrowsAsync<NotSupportedException>(() => repo.CreateEmptyAsync(null));
        }

        #endregion

        #region GrantRepository testing

        [Test]
        public async Task GrantRemoveGrantTest()
        {
            await _security.GrantRepository.SetGrantAsync("role10", "5");

            var policies = (await _security.GrantRepository.GetRoleGrantsAsync("role10")).Select(_ => _.ObjectName);

            CollectionAssert.AreEqual(new[] {"5"}, policies);
            Assert.DoesNotThrowAsync(() => _security.GrantRepository.RemoveGrantAsync("role10", "5"));
        }

        [Test]
        public async Task GrantRemoveGrantsTest()
        {
            await _security.GrantRepository.SetGrantsAsync("role10", new []{"5", "6", "7", "8"});

            var policies = (await _security.GrantRepository.GetRoleGrantsAsync("role10")).Select(_ => _.ObjectName);

            CollectionAssert.AreEqual(new[] { "5", "6", "7", "8" }, policies);
            Assert.DoesNotThrowAsync(() => _security.GrantRepository.RemoveGrantsAsync("role10", new[] { "5", "6", "7", "8" }));
        }

        [Test]
        public async Task GrantGetExceptRoleGrantsTest()
        {
            var secObjects = new []{1,2,3,4,5}.Select(_ => _.ToString()).ToArray();
            var exceptSecObjects = new[] {6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20}
                .Select(_ => _.ToString()).OrderBy(_ => _).ToArray();

            await _security.GrantRepository.SetGrantsAsync("role10", secObjects);
            var policies = (await _security.GrantRepository.GetExceptRoleGrantAsync("role10")).OrderBy(_ => _.ObjectName).Select(_ => _.ObjectName);

            Assert.That(policies.Count(), Is.EqualTo(15));
            CollectionAssert.AreEqual(exceptSecObjects, policies);
            Assert.DoesNotThrowAsync(() => _security.GrantRepository.RemoveGrantsAsync("role10", secObjects));
        }

        #endregion

        #region MemberRoleRepository testing

        [Test]
        public async Task MemberRoleAddMembersToRoleByName()
        {
            var memberStrings = new[] { "user10", "user11", "user12", "group10", "group11", "group12" };
            var exceptMemberStrings = new[] { "admin", "group0", "group1", "group13", "group14", "group15", "group16", "group17", "group18", "group19", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9", "user0", "user1", "user13", "user14", "user15", "user16", "user17", "user18", "user19", "user2", "user3", "user4", "user5", "user6", "user7", "user8", "user9" };

            await _security.MemberRoleRepository.AddMembersToRoleAsync(memberStrings, "role10");

            var members = (await _security.MemberRoleRepository.GetMembersAsync("role10")).OrderBy(_ => _.Name).Select(_ => _.Name);
            var exceptMembers = (await _security.MemberRoleRepository.GetExceptMembersAsync("role10")).OrderBy(_ => _.Name).Select(_ => _.Name);

            Assert.That(members.Count(), Is.EqualTo(6));
            Assert.That(exceptMembers.Count(), Is.EqualTo(35));
            CollectionAssert.AreEqual(memberStrings.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptMemberStrings.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrowAsync(async () => { await _security.MemberRoleRepository.DeleteMembersFromRoleAsync(memberStrings, "role10"); });
        }

        [Test]
        public async Task MemberRoleAddMembersToRoleById()
        {
            var idMembers = new[] { 11, 12, 13, 31, 32, 33 };
            var exceptIdMembers = new[] { 21, 22, 34, 35, 36, 37, 38, 39, 40, 41, 23, 24, 25, 26, 27, 28, 29, 30, 1, 2, 14, 15, 16, 17, 18, 19, 20, 3, 4, 5, 6, 7, 8, 9, 10 };

            await _security.MemberRoleRepository.AddMembersToRoleAsync(idMembers, 10);

            var members = (await _security.MemberRoleRepository.GetMembersAsync(10)).OrderBy(_ => _.IdMember).Select(_ => _.IdMember);
            var exceptMembers = (await _security.MemberRoleRepository.GetExceptMembersAsync(10)).OrderBy(_ => _.IdMember).Select(_ => _.IdMember);

            Assert.That(members.Count(), Is.EqualTo(6));
            Assert.That(exceptMembers.Count(), Is.EqualTo(35));
            CollectionAssert.AreEqual(idMembers.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptIdMembers.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrowAsync(async () => { await _security.MemberRoleRepository.DeleteMembersFromRoleAsync(idMembers, 10); });
        }

        [Test]
        public async Task MemberRoleAddRolesToMemberByName()
        {
            var roleStrings = new[] {"role11", "role12", "role13", "role14", "role15", };
            var exceptRoleStrings = new[]
            {
                "role1",
                "role2",
                "role3",
                "role4",
                "role5",
                "role6",
                "role7",
                "role8",
                "role9",
                "role10",
                "role16",
                "role17",
                "role18",
                "role19",
                "role20",
            };

            await _security.MemberRoleRepository.AddRolesToMemberAsync(roleStrings, "user15");

            var members = (await _security.MemberRoleRepository.GetRolesAsync("user15")).OrderBy(_ => _.Name).Select(_ => _.Name);
            var exceptMembers = (await _security.MemberRoleRepository.GetExceptRolesAsync("user15")).OrderBy(_ => _.Name).Select(_ => _.Name);

            Assert.That(members.Count(), Is.EqualTo(5));
            Assert.That(exceptMembers.Count(), Is.EqualTo(15));
            CollectionAssert.AreEqual(roleStrings.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptRoleStrings.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrowAsync(() => _security.MemberRoleRepository.DeleteRolesFromMemberAsync(roleStrings, "user15"));
        }

        [Test]
        public async Task MemberRoleAddRolesToMemberById()
        {
            var idRoles = new[] {11, 12, 13, 14, 15};
            var exceptIdRoles = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 16, 17, 18, 19, 20};

            await _security.MemberRoleRepository.AddRolesToMemberAsync(idRoles, 15);

            var members = (await _security.MemberRoleRepository.GetRolesAsync(15)).OrderBy(_ => _.IdRole).Select(_ => _.IdRole);
            var exceptMembers = (await _security.MemberRoleRepository.GetExceptRolesAsync(15)).OrderBy(_ => _.IdRole).Select(_ => _.IdRole);

            Assert.That(members.Count(), Is.EqualTo(5));
            Assert.That(exceptMembers.Count(), Is.EqualTo(15));
            CollectionAssert.AreEqual(idRoles.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptIdRoles.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrow(() => _security.MemberRoleRepository.DeleteRolesFromMemberAsync(idRoles, 15));
        }

        #endregion

        #region ApplicationRepository testing

        [Test]
        public async Task ApplicationRepositoryGetAll()
        {
            var applications = await _security.ApplicationRepository.GetAsync();

            CollectionAssert.AllItemsAreNotNull(applications);
            CollectionAssert.AllItemsAreInstancesOfType(applications, typeof(Application));
        }

        [Test]
        public async Task ApplicationRepositoryGetById()
        {
            var application = await _security.ApplicationRepository.GetAsync(1);
            Assert.IsNotNull(application);
            Assert.IsInstanceOf(typeof(Application), application);
        }

        [Test]
        public Task ApplicationRepositoryNotSupportedMethodsTest()
        {
            Assert.ThrowsAsync<NotSupportedException>(() => _security.ApplicationRepository.CreateAsync(null));
            Assert.ThrowsAsync<NotSupportedException>(() => _security.ApplicationRepository.UpdateAsync(null));
            Assert.ThrowsAsync<NotSupportedException>(() => _security.ApplicationRepository.RemoveAsync(null));
            Assert.ThrowsAsync<NotSupportedException>(() => _security.ApplicationRepository.CreateEmptyAsync(null));
            return Task.Delay(0);
        }

        #endregion

        #region SecuritySettigns testing

        [Test]
        public async Task SecuritySettingsSetValue_ValidParameterOfOneSeconds_Test()
        {
            await _security.SecuritySettings.SetValueAsync("key1", "value1", TimeSpan.FromSeconds(1));
            Assert.IsFalse(await _security.SecuritySettings.IsDeprecatedAsync("key1"));
            Assert.DoesNotThrowAsync(() => _security.SecuritySettings.RemoveValueAsync("key1"));
        }

        [Test]
        public async Task SecuritySettingsGetValueTest()
        {
            await _security.SecuritySettings.SetValueAsync("key2", 25);
            Assert.That(await _security.SecuritySettings.GetValueAsync<int>("key2"), Is.EqualTo(25));
            Assert.DoesNotThrowAsync(() => _security.SecuritySettings.RemoveValueAsync("key2"));
        }

        #endregion

        #region Token

        [Test]
        public async Task CheckAccessByToken_Expected_True_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = await scenario.RunAsync(_security);

                var token = await _security.CreateTokenAsync(result.Login, "test");
                var allow = await _security.CheckAccessByTokenAsync(token, result.UserPolicies[0]);

                Assert.IsTrue(allow);
            }
        }

        [Test]
        public async Task CheckAccessByToken_Expected_False_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = await scenario.RunAsync(_security);

                var token = await _security.CreateTokenAsync(result.Login, "test");
                await _security.StopExpireAsync(token);
                Thread.Sleep(10);
                var allow = await _security.CheckAccessByTokenAsync(token, result.UserPolicies[0]);

                Assert.IsFalse(allow);
            }
        }

        [Test]
        public async Task CheckAccessBySeveralToken_Expected_True_And_Token2IsFalse_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = await scenario.RunAsync(_security);

                var token1 = result.UserTokens[0];
                var token2 = result.UserTokens[1];
                var token3 = result.UserTokens[2];
                var token4 = result.UserTokens[3];

                await _security.StopExpireAsync(token2);

                var allow1 = await _security.CheckAccessByTokenAsync(token1, result.UserPolicies[0]);
                var allow2 = await _security.CheckAccessByTokenAsync(token2, result.UserPolicies[0]);
                var allow3 = await _security.CheckAccessByTokenAsync(token3, result.UserPolicies[0]);
                var allow4 = await _security.CheckAccessByTokenAsync(token4, result.UserPolicies[0]);

                Assert.IsTrue(allow1);
                Assert.IsFalse(allow2);
                Assert.IsTrue(allow3);
                Assert.IsTrue(allow4);
            }
        }

        [Test]
        public async Task CheckAccessBySeveralToken_Expected_AllFalse_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = await scenario.RunAsync(_security);

                var token1 = result.UserTokens[0];
                var token2 = result.UserTokens[1];
                var token3 = result.UserTokens[2];
                var token4 = result.UserTokens[3];

                await _security.StopExpireForUserAsync(token2);

                var allow1 = await _security.CheckAccessByTokenAsync(token1, result.UserPolicies[0]);
                var allow2 = await _security.CheckAccessByTokenAsync(token2, result.UserPolicies[0]);
                var allow3 = await _security.CheckAccessByTokenAsync(token3, result.UserPolicies[0]);
                var allow4 = await _security.CheckAccessByTokenAsync(token4, result.UserPolicies[0]);

                Assert.IsFalse(allow1);
                Assert.IsFalse(allow2);
                Assert.IsFalse(allow3);
                Assert.IsFalse(allow4);
            }
        }

        [Test]
        public async Task CheckTokenExpire_ExpectedFalse()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = await scenario.RunAsync(_security);
                var token = result.UserTokens[0];

                await _security.StopExpireAsync(token);
                var expired = await _security.CheckTokenExpireAsync(token);

                Assert.IsFalse(expired);
            }
        }

        [Test]
        public async Task CheckTokenExpire_ExpectedTrue()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = await scenario.RunAsync(_security);
                var token = result.UserTokens[0];

                var expired = await _security.CheckTokenExpireAsync(token);

                Assert.IsTrue(expired);
            }
        }

        [Test]
        public async Task GetUserByToken_Test()
        {
            using (var scenario = new FillDatabaseScenario())
            {
                await scenario.RunAsync(_security);
                var token = await _security.CreateTokenAsync(FillDatabaseScenarioResult.User1, FillDatabaseScenarioResult.DefaultPassword);
                var user = await _security.GetUserByTokenAsync(token);

                Assert.IsNotNull(user);
                Assert.AreEqual(FillDatabaseScenarioResult.User1, user.Login);
            }
        }

        #endregion

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }
}
