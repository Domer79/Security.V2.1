using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Security.Model;
using Security.V2.Contracts;
using SecurityHttp;
using SecurityHttp.Interfaces;

namespace Security.Tests.SecurityHttpTest.Simple
{
    [TestFixture]
    public class Main : BaseTest
    {
        private ISecurity _security;
        private ICommonWeb _commonDb;

        [SetUp]
        public void Setup()
        {
            _security = new MySecurity();
            _commonDb = ServiceLocator.Resolve<ICommonWeb>();
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
        [TestCase("HelloWorldApp2", "1")]
        [TestCase("HelloWorldApp2", "4")]
        [TestCase("HelloWorldApp2", "5")]
        [TestCase("HelloWorldApp2", "6")]
        public void SecObjectExistenceTest(string appName, string objectName)
        {
            using (var security = new V2.Core.Security(appName, "", IocConfig.GetLocator(appName)))
            {
                var secObject = security.SecObjectRepository.GetByName(objectName);
                Assert.That(secObject, Is.Not.Null);
            }
        }

        [TestCase("HelloWorldApp1", "1 2 3")]
        [TestCase("HelloWorldApp2", "1 4 5 6")]
        public void SecObjectCollectionCompareTest(string appName, string secObjects)
        {
            using (var security = new V2.Core.Security(appName, "", IocConfig.GetLocator(appName)))
            {
                var secObjectNames = security.SecObjectRepository.Get().Select(_ => _.ObjectName);
                var secObjectIds = security.SecObjectRepository.Get().Select(_ => _.IdSecObject);

                CollectionAssert.AreEqual(secObjects.Split(' '), secObjectNames);
                CollectionAssert.AreEqual(secObjects.Split(' ').Select(_ => int.Parse(_)), secObjectNames.Select(_ => int.Parse(_)));
            }
        }

        [Test]
        public void PasswordValidateTest()
        {
            Assert.That(() => _security.SetPassword("user1", "123456"), Is.True);
            Assert.That(() => _security.UserValidate("user1", "123456"));

            Assert.That(() => _security.UserValidate("user1@mail.ru", "123456"));
        }

        [Test]
        public void User_MemberFields_ValidationTest()
        {
            var user = _security.UserRepository.GetByName("user1");

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

            var list = new List<string>();
            list.AddRange(_commonDb.GetCollection<User>("api/user").Select(_ => _.Name ));
            list.AddRange(_commonDb.GetCollection<Group>("api/groups").Select(_ => _.Name ));

            CollectionAssert.IsNotEmpty(members);
            CollectionAssert.AreEqual(list, members);
        }

        [Test]
        public void Role_MemberFields_ValidationTest()
        {
            var role = _security.RoleRepository.GetByName("role1");

            Assert.That(role, Has.Property("Name").EqualTo("role1"));
            Assert.That(role, Has.Property("Description").EqualTo("Role1 Description"));
        }

        [TestCase("user1 group1", "role1")]
        [TestCase("user1 user3", "role2")]
        public void Check_Existence_Members_in_Role(string member, string role)
        {
            var members = _security.MemberRoleRepository.GetMembersByRoleName(role).Select(m => m.Name);
            CollectionAssert.AreEqual(member.Split(' '), members);
        }

        [TestCase("role1 role2", "user1")]
        [TestCase("role1", "group1")]
        public void CheckExistsence_Roles_in_User(string roleNames, string member)
        {
            var roles = _security.MemberRoleRepository.GetRolesByMemberName(member).Select(_ => _.Name);
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
        /// <param name="testCase"></param>
        /// <param name="groupName"></param>
        /// <param name="expectedUsers"></param>
        [TestCase(1, "group1", "user0,user1,user2,user7")]
        [TestCase(2, "group1", "user0,user1,user2,user7")]
        [TestCase(3, "group1", "user0,user1,user2,user7")]
        [TestCase(1, "group4", "user2,user4,user6,user7,user12")]
        [TestCase(2, "group4", "user2,user4,user6,user7,user12")]
        [TestCase(3, "group4", "user2,user4,user6,user7,user12")]
        [TestCase(1, "group0", "user0,user1")]
        [TestCase(2, "group0", "user0,user1")]
        [TestCase(3, "group0", "user0,user1")]
        public void CheckRemoveUsersInGroup(int testCase, string groupName, string expectedUsers)
        {
            var member = _security.GroupRepository.GetByName(groupName);
            var userList = new List<User>();

            var userCount = 20;
            for (int i = 20; i < (20 + userCount); i++)
            {
                userList.Add(_security.UserRepository.Create(new User()
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

            var random = new Random(0);
            var count = random.Next(userCount);
            var skipCount = random.Next(userCount - count);
            var users = userList.Skip(skipCount).Take(count);
            _security.UserGroupRepository.AddUsersToGroup(users.Select(_ => _.Name).ToArray(), member.Name);

            if (testCase == 1)
                _security.UserGroupRepository.RemoveUsersFromGroup(users.Select(_ => _.Name).ToArray(), groupName);
            if (testCase == 2)
                _security.UserGroupRepository.RemoveUsersFromGroup(users.Select(_ => _.Id).ToArray(), member.Id);
            if (testCase == 3)
                _security.UserGroupRepository.RemoveUsersFromGroup(users.Select(_ => _.IdMember).ToArray(), member.IdMember);

            users = _security.UserRepository.Get().Where(_ => expectedUsers.Split(',').Contains(_.Name)).OrderBy(_ => _.IdMember);

            var membersByName = _security.UserGroupRepository.GetUsers(member.Name).OrderBy(_ => _.IdMember);

            CollectionAssert.IsNotEmpty(membersByName);
            CollectionAssert.AreEqual(users, membersByName, new UserComparer());
        }

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
