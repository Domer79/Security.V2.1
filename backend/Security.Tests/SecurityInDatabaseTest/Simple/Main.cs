﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Security.CommonContracts;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Core;
using Security.Model;
using Security.Tests.Infrastructure;
using Security.Tests.Scenarios;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace Security.Tests.SecurityInDatabaseTest.Simple
{
    [TestFixture]
    public class Main : BaseTest
    {
        private ISecurity _security;
        private ICommonDb _commonDb;

        [SetUp]
        public void Setup()
        {
            _security = new MySecurity();
            _commonDb = ServiceLocator.Resolve<ICommonDb>();
        }

        [TearDown]
        public void TearDown()
        {
            _security.Dispose();
        }

        [TestCase("HelloWorldApp1", "Hello World Application 1!")]
        public void RegisterApplicationTest(string name, string description)
        {
            var application = _security.ApplicationRepository.GetByName(name);

            Assert.That(application, Has.Property("AppName").EqualTo(name));
            Assert.That(application, Has.Property("Description").EqualTo(description));
        }

        [TestCase("HelloWorldApp1", "1")]
        [TestCase("HelloWorldApp1", "2")]
        [TestCase("HelloWorldApp1", "3")]
        public void SecObjectExistenceTest(string appName, string objectName)
        {
            using (var security = new Core.Security(appName, "", IocConfig.GetLocator(appName)))
            {
                var secObject = security.SecObjectRepository.GetByName(objectName);
                Assert.That(secObject, Is.Not.Null);
            }
        }

        [Test]
        public void PasswordValidateTest()
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
            Assert.That(() => _security.SetPassword("testadmin", "testadmin"), Is.True);
            Assert.That(() => _security.UserValidate("testadmin", "testadmin"), Is.True);

            Assert.That(() => _security.UserValidate("testadmin@mail.ru", "testadmin"), Is.True);
        }

        [Test]
        public void User_MemberFields_ValidationTest()
        {
            using (var scenario = new FillDatabaseScenario())
            {
                scenario.Run(_security);
                var user = _security.UserRepository.GetByName(FillDatabaseScenarioResult.User1);

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
        public void Group_MemberFields_ValidationTest()
        {
            var group = _security.GroupRepository.GetByName("group1");

            Assert.That(group, Has.Property("Name").EqualTo("group1"));
            Assert.That(group, Has.Property("Description").EqualTo("Group1 Description"));
        }

        [Test]
        public void Member_MemberFields_ValidationTest()
        {
            var members = new List<string>();

            members.AddRange(_security.UserRepository.Get().Select(m => m.Name));
            members.AddRange(_security.GroupRepository.Get().Select(m => m.Name));

            var list = _commonDb.Query<string>("select name from sec.Members");

            CollectionAssert.IsNotEmpty(members);
            CollectionAssert.AreEqual(list.OrderBy(_ => _), members.OrderBy(_ => _));
        }

        [Test]
        public void Role_MemberFields_ValidationTest()
        {
            var role = _security.RoleRepository.GetByName("role1");

            Assert.That(role, Has.Property("Name").EqualTo("role1"));
            Assert.That(role, Has.Property("Description").EqualTo("Role1 Description"));
        }

        [TestCase("user1 user2 group1", "role1")]
        [TestCase("user1 user3", "role2")]
        public void Check_Existence_Members_in_Role(string member, string role)
        {
            var members = _security.MemberRoleRepository.GetMembers(role).Select(m => m.Name);
            CollectionAssert.AreEqual(member.Split(' '), members);
        }

        [TestCase("role1 role2", "user1")]
        [TestCase("role1", "group1")]
        public void CheckExistsence_Roles_in_User(string roleNames, string member)
        {
            var roles = _security.MemberRoleRepository.GetRoles(member).Select(_ => _.Name);
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
        public void CheckAccessToMemberTest(string loginOrEmail, string objectName)
        {
            Assert.That(() => _security.CheckAccess(loginOrEmail, objectName), Is.True);
        }

        [TestCase("user2", "3")]
        [TestCase("user3", "1")]
        public void CheckNotAccessToMemberTest(string loginOrEmail, string objectName)
        {
            Assert.That(() => _security.CheckAccess(loginOrEmail, objectName), Is.False);
        }

        #region UserGroupRepository testing

        [TestCase("user1", "group10,group11,group12,group13")]
        public void CheckGroupListInUser(string userName, string groupsByDelimiters)
        {
            var groups = _security.GroupRepository.Get().Where(_ => groupsByDelimiters.Split(',').Contains(_.Name));

            var member = _security.UserRepository.GetByName(userName);

            var membersByIdMember = _security.UserGroupRepository.GetGroups(member.IdMember);
            var membersByGuid = _security.UserGroupRepository.GetGroups(member.Id);
            var membersByName = _security.UserGroupRepository.GetGroups(member.Name);

            CollectionAssert.AreEqual(membersByIdMember, membersByGuid, new GroupComparer());
            CollectionAssert.AreEqual(membersByIdMember, membersByName, new GroupComparer());
        }

        [TestCase("group4", "user2, user4,user6,user7,user12")]
        public void CheckUserListInGroup(string groupName, string usersByDelimiters)
        {
            var users = _security.UserRepository.Get().Where(_ => usersByDelimiters.Split(',').Contains(_.Name));

            var member = _security.GroupRepository.GetByName(groupName);

            var membersByIdMember = _security.UserGroupRepository.GetUsers(member.IdMember);
            var membersByGuid = _security.UserGroupRepository.GetUsers(member.Id);
            var membersByName = _security.UserGroupRepository.GetUsers(member.Name);

            CollectionAssert.IsNotEmpty(membersByIdMember);
            CollectionAssert.IsNotEmpty(membersByGuid);
            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(membersByIdMember, membersByGuid, new UserComparer());
            CollectionAssert.AreEqual(membersByIdMember, membersByName, new UserComparer());
        }

        [TestCase("group4", "user2, user4,user6,user7,user12")]
        [TestCase("group1", "user0,user1,user2, user7")]
        public void CheckNonIncludedUsers(string groupName, string exceptUsersByDelimiters)
        {
            var users = _security.UserRepository.Get().Where(_ => !exceptUsersByDelimiters.Split(',').Contains(_.Name));

            var member = _security.GroupRepository.GetByName(groupName);

            var membersByIdMember = _security.UserGroupRepository.GetNonIncludedUsers(member.IdMember);
            var membersByGuid = _security.UserGroupRepository.GetNonIncludedUsers(member.Id);
            var membersByName = _security.UserGroupRepository.GetNonIncludedUsers(member.Name);

            CollectionAssert.IsNotEmpty(membersByIdMember);
            CollectionAssert.IsNotEmpty(membersByGuid);
            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(membersByIdMember, membersByGuid, new UserComparer());
            CollectionAssert.AreEqual(membersByIdMember, membersByName, new UserComparer());
        }

        [TestCase("user7", "group1,group4,group18,group2,group8")]
        [TestCase("user1", "group0,group1,group10,group11,group12,group13")]
        public void CheckNonIncludedGroups(string userName, string exceptGroupsByDelimiters)
        {
            var groups = _security.GroupRepository.Get().Where(_ => !exceptGroupsByDelimiters.Split(',').Contains(_.Name));

            var member = _security.UserRepository.GetByName(userName);

            var membersByIdMember = _security.UserGroupRepository.GetNonIncludedGroups(member.IdMember);
            var membersByGuid = _security.UserGroupRepository.GetNonIncludedGroups(member.Id);
            var membersByName = _security.UserGroupRepository.GetNonIncludedGroups(member.Name);

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
        public void TestCreateUser(string login)
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
            user = _security.UserRepository.Create(user);

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
        public void CheckRemoveUsersInGroup(int testCase)
        {
            using (var scenario = new FillDatabaseScenario())
            {
                var result = scenario.Run(_security);
                var group = result.Groups.Single(_ => _.Name == FillDatabaseScenarioResult.Group1);

                IEnumerable<User> groupUsers = null;
                if (testCase == 1)
                {
                    _security.UserGroupRepository.RemoveUsersFromGroup(result.GroupUsers[group].Select(_ => _.Name).ToArray(), group.Name);
                    groupUsers = _security.UserGroupRepository.GetUsers(group.Name);
                }

                if (testCase == 2)
                {
                    _security.UserGroupRepository.RemoveUsersFromGroup(result.GroupUsers[group].Select(_ => _.Id).ToArray(), group.Id);
                    groupUsers = _security.UserGroupRepository.GetUsers(group.Id);
                }

                if (testCase == 3)
                {
                    _security.UserGroupRepository.RemoveUsersFromGroup(result.GroupUsers[group].Select(_ => _.IdMember).ToArray(), group.IdMember);
                    groupUsers = _security.UserGroupRepository.GetUsers(group.IdMember);
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
        public void CheckRemoveGroupsInUser(int testCase)
        {
            using (var scenario = new FillDatabaseScenario())
            {
                var result = scenario.Run(_security);
                var user = result.Users.Single(_ => _.Login == FillDatabaseScenarioResult.User1);

                IEnumerable<Group> userGroups = null;
                if (testCase == 1)
                {
                    _security.UserGroupRepository.RemoveGroupsFromUser(result.UserGroups[user].Select(_ => _.Name).ToArray(), user.Login);
                    userGroups = _security.UserGroupRepository.GetGroups(user.Login);
                }

                if (testCase == 2)
                {
                    _security.UserGroupRepository.RemoveGroupsFromUser(result.UserGroups[user].Select(_ => _.Id).ToArray(), user.Id);
                    userGroups = _security.UserGroupRepository.GetGroups(user.Id);
                }

                if (testCase == 3)
                {
                    _security.UserGroupRepository.RemoveGroupsFromUser(result.UserGroups[user].Select(_ => _.IdMember).ToArray(), user.IdMember);
                    userGroups = _security.UserGroupRepository.GetGroups(user.IdMember);
                }

                Assert.IsNotNull(userGroups);
                CollectionAssert.IsEmpty(userGroups);
            }
        }

        #endregion

        #region UserRepository testing

        [Test]
        public void CreateUserTest()
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

            var readyUser = _security.UserRepository.Create(user);
            Assert.That(readyUser, Is.InstanceOf(typeof(User)));
            Assert.That(readyUser, Has.Property("Login").EqualTo("Domer3"));
            Assert.DoesNotThrow(() => { _security.UserRepository.Remove(readyUser.IdMember);});
        }

        [Test]
        public void CreateEmptyUserTest()
        {
            var user = _security.UserRepository.CreateEmpty("new_user");
            _security.UserRepository.Remove(user.IdMember);

            Assert.That(user, Is.InstanceOf(typeof(User)));
            Assert.That(user, Has.Property("Login").StartWith("new_user"));
            Assert.That(user, Has.Property("FirstName").StartWith("new_user"));
            Assert.That(user, Has.Property("LastName").StartWith("new_user"));
            Assert.That(user, Has.Property("Email").StartWith("new_user"));
        }

        [Test]
        public void CheckGetAllUsers()
        {
            var users = _security.UserRepository.Get();

            CollectionAssert.IsNotEmpty(users);
            Assert.That(users.Count(), Is.EqualTo(21));
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void UpdateUser_And_Check_UpdatedPropertiesTest(int idMember)
        {
            var user = _security.UserRepository.Get(idMember);

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

            _security.UserRepository.Update(user);

            user = _security.UserRepository.Get(idMember);

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
        public void SetUserStatusTest()
        {
            _security.UserRepository.SetStatus("user0", false);
            var user = _security.UserRepository.GetByName("user0");

            Assert.IsFalse(user.Status);
        }

        [TestCase(21)]
        public void RemoveUserTest(int idMember)
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
            user = _security.UserRepository.Create(user);

            var group = new Group
            {
                Name = $"Group{idMember}"
            };
            group = _security.GroupRepository.Create(group);

            var role = new Role()
            {
                Name = $"Role{idMember}"
            };
            role = _security.RoleRepository.Create(role);

            _security.UserGroupRepository.AddGroupsToUser(new []{group.Name}, user.Login);
            _security.MemberRoleRepository.AddRolesToMember(new []{role.Name}, user.Name);

            Assert.That(() => _security.UserRepository.Remove(user.IdMember), Throws.Nothing);
            _security.RoleRepository.Remove(role.IdRole);
        }

        #endregion

        #region GroupRepository testing

        [Test]
        public void CreateEmptyGroupTest()
        {
            var group = _security.GroupRepository.CreateEmpty("new_Group");
            _security.GroupRepository.Remove(group.IdMember);
            Assert.That(group, Has.Property("Name").StartWith("new_Group"));
        }

        [Test]
        public void GetAllGroupsTest()
        {
            var groups = _security.GroupRepository.Get();

            CollectionAssert.IsNotEmpty(groups);
            Assert.That(groups.Count(), Is.EqualTo(20));
        }

        [TestCase(22)]
        [TestCase(23)]
        [TestCase(24)]
        [TestCase(25)]
        [TestCase(26)]
        public void UpdateGroup_And_Check_UpdatedPropertiesTest(int idMember)
        {
            var group = _security.GroupRepository.Get(idMember);

            group.Name = $"new_Name{idMember}";
            group.Description = "new_Description";

            _security.GroupRepository.Update(group);

            group = _security.GroupRepository.Get(idMember);

            Assert.IsNotNull(group);

            Assert.That(group, Has.Property("Name").EqualTo($"new_Name{idMember}"));
            Assert.That(group, Has.Property("Description").EqualTo("new_Description"));
        }

        [TestCase(41)]
        public void RemoveGroupTest(int idMember)
        {
            var group = new Group()
            {
                Name = $"Group{idMember}",
            };
            group = _security.GroupRepository.Create(group);

            var user = _security.UserRepository.CreateEmpty("new_user");

            var role = _security.RoleRepository.CreateEmpty("new_role");

            _security.UserGroupRepository.AddUsersToGroup(new[] { user.Name }, group.Name);
            _security.MemberRoleRepository.AddRolesToMember(new[] { role.Name }, group.Name);

            Assert.That(() => _security.GroupRepository.Remove(group.IdMember), Throws.Nothing);
            _security.RoleRepository.Remove(role.IdRole);
        }

        #endregion

        #region SecObjectRepository testing

        [Test]
        public void SecObjectCreateEmptyTest()
        {
            var policy = _security.SecObjectRepository.CreateEmpty("new_Policy");
            _security.SecObjectRepository.Remove(policy.IdSecObject);
            Assert.That(policy, Has.Property("ObjectName").StartWith("new_Policy"));
        }

        [Test]
        public void SecObjectGetAllTest()
        {
            var policies = _security.SecObjectRepository.Get();

            CollectionAssert.IsNotEmpty(policies);
            Assert.That(policies.Count(), Is.EqualTo(20));
        }

        [TestCase(FillDatabaseScenarioResult.Policy1)]
        [TestCase(FillDatabaseScenarioResult.Policy2)]
        public void SecObjectUpdate_And_Check_UpdatedPropertiesTest(string policy)
        {
            using (var scenario = new FillDatabaseScenario())
            {
                var result = scenario.Run(_security);
                var secObject = _security.SecObjectRepository.GetByName(policy);

                secObject.ObjectName = $"new_Policy{secObject.IdSecObject}";

                _security.SecObjectRepository.Update(secObject);

                secObject = _security.SecObjectRepository.Get(secObject.IdSecObject);

                Assert.IsNotNull(secObject);
                Assert.That(secObject, Has.Property("ObjectName").EqualTo($"new_Policy{secObject.IdSecObject}"));
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void SecObject_GetById_AnotherApplication(int idSecObject)
        {
            var secObject = _security.SecObjectRepository.Get(idSecObject);
            Assert.That(secObject, Is.Null);
        }

        [TestCase(31)]
        public void SecObjectRemoveTest(int idSecObject)
        {
            var policy = new SecObject()
            {
                ObjectName = $"Policy{idSecObject}",
            };
            policy = _security.SecObjectRepository.Create(policy);

            var role = _security.RoleRepository.CreateEmpty("new_role");

            _security.GrantRepository.SetGrants(role.Name, new[] { policy.ObjectName });

            Assert.That(() => _security.SecObjectRepository.Remove(policy.IdSecObject), Throws.Nothing);

            _security.RoleRepository.Remove(role.IdRole);
        }

        [Test]
        public void RegisterExistingPolicy()
        {
            using (var scenario = new FillDatabaseScenario())
            {
                scenario.Run(_security);
                Assert.DoesNotThrow(() =>
                {
                    _security.Config.RegisterSecurityObjects(FillDatabaseScenarioResult.AppName, new[] { FillDatabaseScenarioResult.Policy1 });
                });
            }
        }

        #endregion

        #region RoleRepository testing

        [Test]
        public void RoleCreateEmptyTest()
        {
            var role = _security.RoleRepository.CreateEmpty("new_Role");
            _security.RoleRepository.Remove(role.IdRole);
            Assert.That(role, Has.Property("Name").StartWith("new_Role"));
        }

        [Test]
        public void RoleGetAllTest()
        {
            var roles = _security.RoleRepository.Get();

            CollectionAssert.IsNotEmpty(roles);
            Assert.That(roles.Count(), Is.EqualTo(20));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void RolesUpdate_And_Check_UpdatedPropertiesTest(int idRole)
        {
            var role = _security.RoleRepository.Get(idRole);

            role.Name = $"new_Role{idRole}";

            _security.RoleRepository.Update(role);

            role = _security.RoleRepository.Get(idRole);

            Assert.IsNotNull(role);

            Assert.That(role, Has.Property("Name").EqualTo($"new_Role{idRole}"));
        }

        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [TestCase(24)]
        public void Role_GetById_AnotherApplication(int idRole)
        {
            var role = _security.RoleRepository.Get(idRole);

            Assert.That(role, Is.Null);
        }

        [TestCase(31)]
        public void RoleRemoveTest(int idSecObject)
        {
            var role = new Role()
            {
                Name = $"Policy{idSecObject}",
            };
            role = _security.RoleRepository.Create(role);

            var secObject = _security.SecObjectRepository.CreateEmpty("new_Policy");

            _security.GrantRepository.SetGrants(role.Name, new[] { secObject.ObjectName });
            _security.MemberRoleRepository.AddMembersToRole(new[] {"user1"}, role.Name);

            Assert.That(() => _security.RoleRepository.Remove(role.IdRole), Throws.Nothing);
            _security.SecObjectRepository.Remove(secObject.IdSecObject);
        }

        #endregion

        #region ApplicationInternalRepository testing

        [Test]
        public void ApplicationInternal_Get_Update_And_Remove_ApplicationTest()
        {
            IServiceLocator locator = IocConfig.GetLocator("MyNewTestApp");
            using (var security = new Core.Security("MyNewTestApp", "MyNewTestApp Description", locator))
            {
                security.Config.RegisterSecurityObjects("MyNewTestApp", "1", "2", "3", "4", "5", "6", "7", "8");
                var user = security.UserRepository.GetByName("user1");

                var group = security.GroupRepository.GetByName("group1");

                var role = security.RoleRepository.Create(new Role(){Name = "Role1"});
                var policy = security.SecObjectRepository.Create(new SecObject() {ObjectName = "policy1"});

                security.MemberRoleRepository.AddMembersToRole(new[] {user.IdMember, group.IdMember},
                    role.IdRole);
            }

            var repo = locator.Resolve<IApplicationInternalRepository>();
            var apps = repo.Get();

            foreach (var app in apps)
            {
                var oldDescription = app.Description;
                app.Description = $"New {app.AppName} Description 2.0";
                repo.Update(app);
                var application = repo.Get(app.IdApplication);

                Assert.That(application, Has.Property("Description").EqualTo(app.Description));
                Assert.That(application, Is.Not.Null);

                app.Description = oldDescription;
                repo.Update(app);
            }

            Assert.DoesNotThrow(() =>
            {
                _security.Config.RemoveApplication("MyNewTestApp");
                repo.Remove(apps.First(_ => _.AppName == "HelloWorldApp2").IdApplication);
            });

            Assert.Throws<NotSupportedException>(() => repo.CreateEmpty(null));
        }

        #endregion

        #region GrantRepository testing

        [Test]
        public void GrantRemoveGrantTest()
        {
            _security.GrantRepository.SetGrant("role10", "5");

            var policies = _security.GrantRepository.GetRoleGrants("role10").Select(_ => _.ObjectName);

            CollectionAssert.AreEqual(new[] {"5"}, policies);
            Assert.DoesNotThrow(() => { _security.GrantRepository.RemoveGrant("role10", "5");});
        }

        [Test]
        public void GrantRemoveGrantsTest()
        {
            _security.GrantRepository.SetGrants("role10", new []{"5", "6", "7", "8"});

            var policies = _security.GrantRepository.GetRoleGrants("role10").Select(_ => _.ObjectName);

            CollectionAssert.AreEqual(new[] { "5", "6", "7", "8" }, policies);
            Assert.DoesNotThrow(() => { _security.GrantRepository.RemoveGrants("role10", new[] { "5", "6", "7", "8" }); });
        }

        [Test]
        public void GrantGetExceptRoleGrantsTest()
        {
            var secObjects = new []{1,2,3,4,5}.Select(_ => _.ToString()).ToArray();
            var exceptSecObjects = new[] {6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20}
                .Select(_ => _.ToString()).OrderBy(_ => _).ToArray();

            _security.GrantRepository.SetGrants("role10", secObjects);
            var policies = _security.GrantRepository.GetExceptRoleGrant("role10").OrderBy(_ => _.ObjectName).Select(_ => _.ObjectName);

            Assert.That(policies.Count(), Is.EqualTo(15));
            CollectionAssert.AreEqual(exceptSecObjects, policies);
            Assert.DoesNotThrow(() => { _security.GrantRepository.RemoveGrants("role10", secObjects);});
        }

        #endregion

        #region MemberRoleRepository testing

        [Test]
        public void MemberRoleAddMembersToRoleByName()
        {
            var memberStrings = new[] { "user10", "user11", "user12", "group10", "group11", "group12" };
            var exceptMemberStrings = new[] { "admin", "group0", "group1", "group13", "group14", "group15", "group16", "group17", "group18", "group19", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9", "user0", "user1", "user13", "user14", "user15", "user16", "user17", "user18", "user19", "user2", "user3", "user4", "user5", "user6", "user7", "user8", "user9" };

            _security.MemberRoleRepository.AddMembersToRole(memberStrings, "role10");

            var members = _security.MemberRoleRepository.GetMembers("role10").OrderBy(_ => _.Name).Select(_ => _.Name);
            var exceptMembers = _security.MemberRoleRepository.GetExceptMembers("role10").OrderBy(_ => _.Name).Select(_ => _.Name);

            Assert.That(members.Count(), Is.EqualTo(6));
            Assert.That(exceptMembers.Count(), Is.EqualTo(35));
            CollectionAssert.AreEqual(memberStrings.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptMemberStrings.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrow(() => { _security.MemberRoleRepository.DeleteMembersFromRole(memberStrings, "role10"); });
        }

        [Test]
        public void MemberRoleAddMembersToRoleById()
        {
            var idMembers = new[] { 11, 12, 13, 31, 32, 33 };
            var exceptIdMembers = new[] { 21, 22, 34, 35, 36, 37, 38, 39, 40, 41, 23, 24, 25, 26, 27, 28, 29, 30, 1, 2, 14, 15, 16, 17, 18, 19, 20, 3, 4, 5, 6, 7, 8, 9, 10 };

            _security.MemberRoleRepository.AddMembersToRole(idMembers, 10);

            var members = _security.MemberRoleRepository.GetMembers(10).OrderBy(_ => _.IdMember).Select(_ => _.IdMember);
            var exceptMembers = _security.MemberRoleRepository.GetExceptMembers(10).OrderBy(_ => _.IdMember).Select(_ => _.IdMember);

            Assert.That(members.Count(), Is.EqualTo(6));
            Assert.That(exceptMembers.Count(), Is.EqualTo(35));
            CollectionAssert.AreEqual(idMembers.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptIdMembers.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrow(() => { _security.MemberRoleRepository.DeleteMembersFromRole(idMembers, 10); });
        }

        [Test]
        public void MemberRoleAddRolesToMemberByName()
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

            _security.MemberRoleRepository.AddRolesToMember(roleStrings, "user15");

            var members = _security.MemberRoleRepository.GetRoles("user15").OrderBy(_ => _.Name).Select(_ => _.Name);
            var exceptMembers = _security.MemberRoleRepository.GetExceptRoles("user15").OrderBy(_ => _.Name).Select(_ => _.Name);

            Assert.That(members.Count(), Is.EqualTo(5));
            Assert.That(exceptMembers.Count(), Is.EqualTo(15));
            CollectionAssert.AreEqual(roleStrings.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptRoleStrings.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrow(() => { _security.MemberRoleRepository.DeleteRolesFromMember(roleStrings, "user15"); });
        }

        [Test]
        public void MemberRoleAddRolesToMemberById()
        {
            var idRoles = new[] {11, 12, 13, 14, 15};
            var exceptIdRoles = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 16, 17, 18, 19, 20};

            _security.MemberRoleRepository.AddRolesToMember(idRoles, 15);

            var members = _security.MemberRoleRepository.GetRoles(15).OrderBy(_ => _.IdRole).Select(_ => _.IdRole);
            var exceptMembers = _security.MemberRoleRepository.GetExceptRoles(15).OrderBy(_ => _.IdRole).Select(_ => _.IdRole);

            Assert.That(members.Count(), Is.EqualTo(5));
            Assert.That(exceptMembers.Count(), Is.EqualTo(15));
            CollectionAssert.AreEqual(idRoles.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptIdRoles.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrow(() => { _security.MemberRoleRepository.DeleteRolesFromMember(idRoles, 15); });
        }

        #endregion

        #region ApplicationRepository testing

        [Test]
        public void ApplicationRepositoryGetAll()
        {
            var applications = _security.ApplicationRepository.Get();
            CollectionAssert.AllItemsAreNotNull(applications);
            CollectionAssert.AllItemsAreInstancesOfType(applications, typeof(Application));
        }

        [Test]
        public void ApplicationRepositoryGetById()
        {
            var application = _security.ApplicationRepository.Get(1);
            Assert.IsNotNull(application);
            Assert.IsInstanceOf(typeof(Application), application);
        }

        [Test]
        public void ApplicationRepositoryNotSupportedMethodsTest()
        {
            Assert.Throws<NotSupportedException>(() => _security.ApplicationRepository.Create(null));
            Assert.Throws<NotSupportedException>(() => _security.ApplicationRepository.Update(null));
            Assert.Throws<NotSupportedException>(() => _security.ApplicationRepository.Remove(null));
            Assert.Throws<NotSupportedException>(() => _security.ApplicationRepository.CreateEmpty(null));
        }

        #endregion

        #region SecuritySettigns testing

        [Test]
        public void SecuritySettingsSetValue_ValidParameterOfOneSeconds_Test()
        {
            _security.SecuritySettings.SetValue("key1", "value1", TimeSpan.FromSeconds(1));
            Assert.IsFalse(_security.SecuritySettings.IsDeprecated("key1"));
            Assert.DoesNotThrow(() => { _security.SecuritySettings.RemoveValue("key1");});
        }

        [TestCase(25, typeof(int))]
        [TestCase(25.3, typeof(float))]
        [TestCase(true, typeof(bool))]
        [TestCase("qwerty", typeof(string))]
        public void SecuritySettingsGetValueTest(object value, Type type)
        {
            _security.SecuritySettings.SetValue("key2", value);
            Assert.That(_security.SecuritySettings.GetValue("key2", type), Is.EqualTo(value).Within(value));
            Assert.DoesNotThrow(() => { _security.SecuritySettings.RemoveValue("key2"); });
        }

        #endregion

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }

        #region Token

        [Test]
        public void CheckAccessByToken_Expected_True_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = scenario.Run(_security);

                var token = _security.CreateToken(result.Login, "test");
                var allow = _security.CheckAccessByToken(token, result.UserPolicies[0]);

                Assert.IsTrue(allow);
            }
        }

        [Test]
        public void CheckAccessByToken_Expected_False_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = scenario.Run(_security);

                var token = _security.CreateToken(result.Login, "test");
                _security.StopExpire(token);
                Thread.Sleep(10);
                var allow = _security.CheckAccessByToken(token, result.UserPolicies[0]);

                Assert.IsFalse(allow);
            }
        }

        [Test]
        public void CheckAccessBySeveralToken_Expected_True_And_Token2IsFalse_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = scenario.Run(_security);

                var token1 = result.UserTokens[0];
                var token2 = result.UserTokens[1];
                var token3 = result.UserTokens[2];
                var token4 = result.UserTokens[3];

                _security.StopExpireAsync(token2);

                var allow1 = _security.CheckAccessByToken(token1, result.UserPolicies[0]);
                var allow2 = _security.CheckAccessByToken(token2, result.UserPolicies[0]);
                var allow3 = _security.CheckAccessByToken(token3, result.UserPolicies[0]);
                var allow4 = _security.CheckAccessByToken(token4, result.UserPolicies[0]);

                Assert.IsTrue(allow1);
                Assert.IsFalse(allow2);
                Assert.IsTrue(allow3);
                Assert.IsTrue(allow4);
            }
        }

        [Test]
        public void CheckAccessBySeveralToken_Expected_AllFalse_Test()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = scenario.Run(_security);

                var token1 = result.UserTokens[0];
                var token2 = result.UserTokens[1];
                var token3 = result.UserTokens[2];
                var token4 = result.UserTokens[3];

                _security.StopExpireForUser(token2);

                var allow1 = _security.CheckAccessByToken(token1, result.UserPolicies[0]);
                var allow2 = _security.CheckAccessByToken(token2, result.UserPolicies[0]);
                var allow3 = _security.CheckAccessByToken(token3, result.UserPolicies[0]);
                var allow4 = _security.CheckAccessByToken(token4, result.UserPolicies[0]);

                Assert.IsFalse(allow1);
                Assert.IsFalse(allow2);
                Assert.IsFalse(allow3);
                Assert.IsFalse(allow4);
            }
        }

        [Test]
        public void CheckTokenExpire_ExpectedFalse()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = scenario.Run(_security);
                var token = result.UserTokens[0];

                _security.StopExpire(token);
                Thread.Sleep(10);
                var expired = _security.CheckTokenExpire(token);

                Assert.IsFalse(expired);
            }
        }

        [Test]
        public void CheckTokenExpire_ExpectedTrue()
        {
            using (var scenario = new CreateUserAndGrantAccessScenario())
            {
                var result = scenario.Run(_security);
                var token = result.UserTokens[0];

                var expired = _security.CheckTokenExpire(token);

                Assert.IsTrue(expired);
            }
        }


        [Test]
        public void GetUserByToken_Test()
        {
            using (var scenario = new FillDatabaseScenario())
            {
                scenario.Run(_security);
                var token = _security.CreateToken(FillDatabaseScenarioResult.User1, FillDatabaseScenarioResult.DefaultPassword);
                var user = _security.GetUserByToken(token);

                Assert.IsNotNull(user);
                Assert.AreEqual(FillDatabaseScenarioResult.User1, user.Login);
            }
        }

        #endregion
    }
}
