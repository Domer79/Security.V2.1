using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Security.Tests.SecurityInDatabaseTest.RepositoryTests;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Core;

namespace Security.Tests.SecurityInDatabaseTest.RepositoryTestsAsync
{
    [TestFixture]
    public class MainAsync : BaseTest
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
        public async Task RegisterApplicationTest(string name, string description)
        {
            var application = await _security.ApplicationRepository.GetByNameAsync(name);

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
        public async Task SecObjectExistenceTest(string appName, string objectName)
        {
            using (var security = new V2.Core.Security(appName, "", IocConfig.GetLocator(appName)))
            {
                var secObject = await security.SecObjectRepository.GetByNameAsync(objectName);
                Assert.That(secObject, Is.Not.Null);
            }
        }

        [TestCase("HelloWorldApp1", "1 2 3")]
        [TestCase("HelloWorldApp2", "1 4 5 6")]
        public async Task SecObjectCollectionCompareTest(string appName, string secObjects)
        {
            using (var security = new V2.Core.Security(appName, "", IocConfig.GetLocator(appName)))
            {
                var secObjectNames = (await security.SecObjectRepository.GetAsync()).Select(_ => _.ObjectName);
                var secObjectIds = (await security.SecObjectRepository.GetAsync()).Select(_ => _.IdSecObject);

                CollectionAssert.AreEqual(secObjects.Split(' '), secObjectNames);
                CollectionAssert.AreEqual(secObjects.Split(' ').Select(_ => int.Parse(_)), secObjectNames.Select(_ => int.Parse(_)));
            }
        }

        [Test]
        public void PasswordValidateTest()
        {
            Assert.That(async () => await _security.SetPasswordAsync("user1", "123456"), Is.True);
            Assert.That(async () => await _security.UserValidateAsync("user1", "123456"), Is.True);

            Assert.That(() => _security.UserValidate("user1@mail.ru", "123456"));
        }

        [Test]
        public async Task User_MemberFields_ValidationTest()
        {
            var user = await _security.UserRepository.GetByNameAsync("user1");

            Assert.That(user, Has.Property("Login").EqualTo("user1"));
            Assert.That(user, Has.Property("Name").EqualTo("user1"));
            Assert.That(user, Has.Property("Email").EqualTo("user1@mail.ru"));
            Assert.That(user, Has.Property("FirstName").EqualTo("Ivan"));
            Assert.That(user, Has.Property("LastName").EqualTo("Petrov"));
            Assert.That(user, Has.Property("MiddleName").EqualTo("Ivanovich"));
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

            var expectedMembers = await _commonDb.QueryAsync<string>("select name from sec.Members");

            CollectionAssert.IsNotEmpty(members);
            CollectionAssert.AreEqual(expectedMembers, members);
        }

        [Test]
        public async Task Role_MemberFields_ValidationTest()
        {
            var role = await _security.RoleRepository.GetByNameAsync("role1");

            Assert.That(role, Has.Property("Name").EqualTo("role1"));
            Assert.That(role, Has.Property("Description").EqualTo("Role1 Description"));
        }

        [Test]
        public async Task Check_Existence_Group_in_User()
        {
            var group = (await _security.UserGroupRepository.GetGroupsByUserNameAsync("user1")).FirstOrDefault();

            Assert.That(group, Is.Not.Null);
            Assert.That(group.Name, Is.EqualTo("group1"));
        }

        [Test]
        public async Task Check_Existence_User_in_Group()
        {
            var user = (await _security.UserGroupRepository.GetUsersByGroupNameAsync("group1")).FirstOrDefault();

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Login, Is.EqualTo("user1"));
        }

        [TestCase("user1 group1", "role1")]
        [TestCase("user1 user3", "role2")]
        public async Task Check_Existence_Members_in_Role(string member, string role)
        {
            var members = (await _security.MemberRoleRepository.GetMembersByRoleNameAsync(role)).Select(m => m.Name);
            CollectionAssert.AreEqual(member.Split(' '), members);
        }

        [TestCase("role1 role2", "user1")]
        [TestCase("role1", "group1")]
        public async Task CheckExistsence_Roles_in_User(string roleNames, string member)
        {
            var roles = (await _security.MemberRoleRepository.GetRolesByMemberNameAsync(member)).Select(_ => _.Name);
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
            Assert.That(async () => await _security.CheckAccessAsync(loginOrEmail, objectName), Is.True);
        }

        [TestCase("user2", "3")]
        [TestCase("user3", "1")]
        public void CheckNotAccessToMemberTest(string loginOrEmail, string objectName)
        {
            Assert.That(async () => await _security.CheckAccessAsync(loginOrEmail, objectName), Is.False);
        }

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }
}
