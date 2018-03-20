﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;

namespace Security.Tests.SecurityInDatabaseTest.SimpleAsync
{
    [TestFixture]
    public class Main : BaseTest
    {
        private ISecurity _security;
        private ICommonDb _commonDb;

        [SetUp]
        public Task Setup()
        {
            _security = new MySecurity();
            _commonDb = ServiceLocator.Resolve<ICommonDb>();
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
            using (var security = new V2.Core.Security(appName, "", IocConfig.GetLocator(appName)))
            {
                var secObject = await security.SecObjectRepository.GetByNameAsync(objectName);
                Assert.That(secObject, Is.Not.Null);
            }
        }

        [Test]
        public Task PasswordValidateTest()
        {
            Assert.That(() => _security.SetPasswordAsync("user1", "123456"), Is.True);
            Assert.That(() => _security.UserValidateAsync("user1", "123456"), Is.True);

            Assert.That(() => _security.UserValidateAsync("user1@mail.ru", "123456"), Is.True);

            return Task.Delay(0);
        }

        [Test]
        public async Task User_MemberFields_ValidationTest()
        {
            var user = await _security.UserRepository.GetByNameAsync("user1");

            Assert.That(user, Has.Property("Login").EqualTo("user1"));
            Assert.That(user, Has.Property("Name").EqualTo("user1"));
            Assert.That(user, Has.Property("Email").EqualTo("user1@mail.ru"));
            Assert.That(user, Has.Property("FirstName").EqualTo("User1First"));
            Assert.That(user, Has.Property("LastName").EqualTo("User1Last"));
            Assert.That(user, Has.Property("MiddleName").EqualTo("User1Middle"));
            Assert.That(user, Has.Property("Status").EqualTo(true));
            Assert.That(user, Has.Property("DateCreated").EqualTo(DateTime.Now).Within(TimeSpan.FromMinutes(1)));
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

            var list = await _commonDb.QueryAsync<string>("select name from sec.Members");

            CollectionAssert.IsNotEmpty(members);
            CollectionAssert.AreEqual(list.OrderBy(_ => _), members.OrderBy(_ => _));
        }

        [Test]
        public async Task Role_MemberFields_ValidationTest()
        {
            var role = await _security.RoleRepository.GetByNameAsync("role1");

            Assert.That(role, Has.Property("Name").EqualTo("role1"));
            Assert.That(role, Has.Property("Description").EqualTo("Role1 Description"));
        }

        [TestCase("user1 group1", "role1")]
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
        /// <param name="testCase"></param>
        /// <param name="groupName"></param>
        /// <param name="expectedNameUsers"></param>
        [TestCase(1, "group1", "user0,user1,user2,user7", 13)]
        [TestCase(2, "group1", "user0,user1,user2,user7", 10)]
        [TestCase(3, "group1", "user0,user1,user2,user7", 30)]
        [TestCase(1, "group4", "user2,user4,user6,user7,user12", 5)]
        [TestCase(2, "group4", "user2,user4,user6,user7,user12", 8)]
        [TestCase(3, "group4", "user2,user4,user6,user7,user12", 9)]
        [TestCase(1, "group0", "user0,user1", 12)]
        [TestCase(2, "group0", "user0,user1", 21)]
        [TestCase(3, "group0", "user0,user1", 18)]
        public async Task CheckRemoveUsersInGroup(int testCase, string groupName, string expectedNameUsers, int userCount)
        {
            var member = await _security.GroupRepository.GetByNameAsync(groupName);
            var userList = new List<User>();

            for (int i = 20; i < (20 + userCount); i++)
            {
                userList.Add(await _security.UserRepository.CreateAsync(new User()
                {
                    Login = $"user{i}",
                    Email = $"user{i}@mail.ru",
                    FirstName = $"User{i}First",
                    LastName = $"User{i}Last",
                    MiddleName = $"User{i}Middle",
                    Status = true,
                    DateCreated = DateTime.Now
                }));
            }

            await _security.UserGroupRepository.AddUsersToGroupAsync(userList.Select(_ => _.Name).ToArray(), member.Name);

            if (testCase == 1)
                await _security.UserGroupRepository.RemoveUsersFromGroupAsync(userList.Select(_ => _.Name).ToArray(), groupName);
            if (testCase == 2)
                await _security.UserGroupRepository.RemoveUsersFromGroupAsync(userList.Select(_ => _.Id).ToArray(), member.Id);
            if (testCase == 3)
                await _security.UserGroupRepository.RemoveUsersFromGroupAsync(userList.Select(_ => _.IdMember).ToArray(), member.IdMember);

            foreach (var user in userList)
            {
                await _security.UserRepository.RemoveAsync(user.IdMember);
            }

            var expectedUsers = (await _security.UserRepository.GetAsync()).Where(_ => expectedNameUsers.Split(',').Contains(_.Name)).OrderBy(_ => _.IdMember);

            var membersByName = (await _security.UserGroupRepository.GetUsersAsync(member.Name)).OrderBy(_ => _.IdMember);

            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(expectedUsers, membersByName, new UserComparer());
        }

        /// <summary>
        /// testCase = 1 - удаление по имени
        /// testCase = 2 - удаление по Guid
        /// testCase = 3 - удаление по IdMember
        /// </summary>
        /// <param name="testCase"></param>
        /// <param name="groupName"></param>
        /// <param name="expectedNameUsers"></param>
        [TestCase(1, "user1", "group0,group1,group10,group11,group12,group13", 13)]
        [TestCase(2, "user1", "group0,group1,group10,group11,group12,group13", 10)]
        [TestCase(3, "user1", "group0,group1,group10,group11,group12,group13", 30)]
        [TestCase(1, "user2", "group1,group4,group12,group15,group18", 5)]
        [TestCase(2, "user2", "group1,group4,group12,group15,group18", 8)]
        [TestCase(3, "user2", "group1,group4,group12,group15,group18", 9)]
        [TestCase(1, "user7", "group1,group2,group4,group8,group18", 12)]
        [TestCase(2, "user7", "group1,group2,group4,group8,group18", 21)]
        [TestCase(3, "user7", "group1,group2,group4,group8,group18", 18)]
        public async Task CheckRemoveGroupsInUser(int testCase, string userName, string expectedNameGroups, int groupCount)
        {
            var member = await _security.UserRepository.GetByNameAsync(userName);
            var groupList = new List<Group>();

            for (int i = 20; i < (20 + groupCount); i++)
            {
                groupList.Add(await _security.GroupRepository.CreateAsync(new Group()
                {
                    Name = $"group{i}",
                    Description = $"Group{i} Description",
                }));
            }

            await _security.UserGroupRepository.AddGroupsToUserAsync(groupList.Select(_ => _.Name).ToArray(), member.Name);

            if (testCase == 1)
                await _security.UserGroupRepository.RemoveGroupsFromUserAsync(groupList.Select(_ => _.Name).ToArray(), userName);
            if (testCase == 2)
                await _security.UserGroupRepository.RemoveGroupsFromUserAsync(groupList.Select(_ => _.Id).ToArray(), member.Id);
            if (testCase == 3)
                await _security.UserGroupRepository.RemoveGroupsFromUserAsync(groupList.Select(_ => _.IdMember).ToArray(), member.IdMember);

            foreach (var @group in groupList)
            {
                await _security.GroupRepository.RemoveAsync(@group.IdMember);
            }

            var expectedGroups = (await _security.GroupRepository.GetAsync()).Where(_ => expectedNameGroups.Split(',').Contains(_.Name)).OrderBy(_ => _.IdMember);

            var membersByName = (await _security.UserGroupRepository.GetGroupsAsync(member.Name)).OrderBy(_ => _.IdMember);

            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(expectedGroups, membersByName, new GroupComparer());
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
            Assert.That(users.Count(), Is.EqualTo(20));
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

        [TestCase(21)]
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

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public async Task SecObjectUpdate_And_Check_UpdatedPropertiesTest(int idSecObject)
        {
            var secObject = await _security.SecObjectRepository.GetAsync(idSecObject);

            secObject.ObjectName= $"new_Policy{idSecObject}";

            await _security.SecObjectRepository.UpdateAsync(secObject);

            secObject = await _security.SecObjectRepository.GetAsync(idSecObject);

            Assert.IsNotNull(secObject);

            Assert.That(secObject, Has.Property("ObjectName").EqualTo($"new_Policy{idSecObject}"));
        }

        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [TestCase(24)]
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
            using (var security = new V2.Core.Security("MyNewTestApp", "MyNewTestApp Description", locator))
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
            var memberStrings = new []{"user10", "user11", "user12", "group10","group11","group12"};
            var exceptMemberStrings = new[] { "group0", "group1", "group13", "group14", "group15", "group16", "group17", "group18", "group19", "group2", "group3", "group4", "group5", "group6", "group7", "group8", "group9", "user0", "user1", "user13", "user14", "user15", "user16", "user17", "user18", "user19", "user2", "user3", "user4", "user5", "user6", "user7", "user8", "user9" };

            await _security.MemberRoleRepository.AddMembersToRoleAsync(memberStrings, "role10");

            var members = (await _security.MemberRoleRepository.GetMembersAsync("role10")).OrderBy(_ => _.Name).Select(_ => _.Name);
            var exceptMembers = (await _security.MemberRoleRepository.GetExceptMembersAsync("role10")).OrderBy(_ => _.Name).Select(_ => _.Name);

            Assert.That(members.Count(), Is.EqualTo(6));
            Assert.That(exceptMembers.Count(), Is.EqualTo(34));
            CollectionAssert.AreEqual(memberStrings.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptMemberStrings.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrowAsync(() => _security.MemberRoleRepository.DeleteMembersFromRoleAsync(memberStrings, "role10"));
        }

        [Test]
        public async Task MemberRoleAddMembersToRoleById()
        {
            var idMembers = new[] {11, 12, 13, 31, 32, 33};
            var exceptIdMembers = new[] { 21, 22, 34, 35, 36, 37, 38, 39, 40, 23, 24, 25, 26, 27, 28, 29, 30, 1, 2, 14, 15, 16, 17, 18, 19, 20, 3, 4, 5, 6, 7, 8, 9, 10 };

            await _security.MemberRoleRepository.AddMembersToRoleAsync(idMembers, 10);

            var members = (await _security.MemberRoleRepository.GetMembersAsync(10)).OrderBy(_ => _.IdMember).Select(_ => _.IdMember);
            var exceptMembers = (await _security.MemberRoleRepository.GetExceptMembersAsync(10)).OrderBy(_ => _.IdMember).Select(_ => _.IdMember);

            Assert.That(members.Count(), Is.EqualTo(6));
            Assert.That(exceptMembers.Count(), Is.EqualTo(34));
            CollectionAssert.AreEqual(idMembers.OrderBy(_ => _), members);
            CollectionAssert.AreEqual(exceptIdMembers.OrderBy(_ => _), exceptMembers);

            Assert.DoesNotThrowAsync(() => _security.MemberRoleRepository.DeleteMembersFromRoleAsync(idMembers, 10));
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

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }

    public class GroupComparer : Comparer<Group>
    {
        public override int Compare(Group x, Group y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
                return x.Id.CompareTo(y.Id);
            if (x.IdMember.CompareTo(y.IdMember) != 0)
                return x.IdMember.CompareTo(y.IdMember);
            if (x.Name.CompareTo(y.Name) != 0)
                return x.Name.CompareTo(y.Name);
            if (x.Description.CompareTo(y.Description) != 0)
                return x.Description.CompareTo(y.Description);

            return 0;
        }
    }

    public class UserComparer : Comparer<User>
    {
        public override int Compare(User x, User y)
        {
            if (x.Status.CompareTo(y.Status) != 0)
                return x.Status.CompareTo(y.Status);
            if (x.Id.CompareTo(y.Id) != 0)
                return x.Id.CompareTo(y.Id);
            if (x.IdMember.CompareTo(y.IdMember) != 0)
                return x.IdMember.CompareTo(y.IdMember);
            if (x.Login.CompareTo(y.Login) != 0)
                return x.Login.CompareTo(y.Login);
            if (x.Name.CompareTo(y.Name) != 0)
                return x.Name.CompareTo(y.Name);
            if (x.DateCreated.CompareTo(y.DateCreated) != 0)
                return x.DateCreated.CompareTo(y.DateCreated);
            if (x.Email.CompareTo(y.Email) != 0)
                return x.Email.CompareTo(y.Email);
            if (x.FirstName.CompareTo(y.FirstName) != 0)
                return x.FirstName.CompareTo(y.FirstName);
            if (x.LastName.CompareTo(y.LastName) != 0)
                return x.LastName.CompareTo(y.LastName);
            if (x.MiddleName.CompareTo(y.MiddleName) != 0)
                return x.MiddleName.CompareTo(y.MiddleName);
            if (Nullable.Compare(x.LastActivityDate, y.LastActivityDate) != 0)
                return Nullable.Compare(x.LastActivityDate, y.LastActivityDate);
            if (x.PasswordSalt.CompareTo(y.PasswordSalt) != 0)
                return x.PasswordSalt.CompareTo(y.PasswordSalt);

            return 0;
        }
    }
}
