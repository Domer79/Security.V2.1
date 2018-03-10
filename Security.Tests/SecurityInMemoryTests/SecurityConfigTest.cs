using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Security.Tests.SecurityFake;
using Security.Tests.SecurityImplement;
using Security.V2.Contracts;

namespace Security.Tests.SecurityInMemoryTests
{
    [TestFixture]
    public class SecurityConfigTest
    {
        private ISecurity _security;

        [SetUp]
        public void Setup()
        {
            _security = new MySecurity();
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
            using(var security = new V2.Core.Security(appName, "", IocConfig.GetServiceLocator(appName)))
            {
                var secObject = security.SecObjectRepository.GetByName(objectName);
                Assert.That(secObject, Is.Not.Null);
            }
        }

        [TestCase("HelloWorldApp1", "1 2 3")]
        [TestCase("HelloWorldApp2", "1 4 5 6")]
        public void SecObjectCollectionCompareTest(string appName, string secObjects)
        {
            using (var security = new V2.Core.Security(appName, "", IocConfig.GetServiceLocator(appName)))
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
            Assert.That(user, Has.Property("FirstName").EqualTo("Ivan"));
            Assert.That(user, Has.Property("LastName").EqualTo("Petrov"));
            Assert.That(user, Has.Property("MiddleName").EqualTo("Ivanovich"));
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

            CollectionAssert.AreEqual(Database.Members.Select(m => m.Name), members);
        }

        [Test]
        public void Role_MemberFields_ValidationTest()
        {
            var role = _security.RoleRepository.GetByName("role1");

            Assert.That(role, Has.Property("Name").EqualTo("role1"));
            Assert.That(role, Has.Property("Description").EqualTo("Role1 Description"));
        }

        [Test]
        public void Check_Existence_Group_in_User()
        {
            var group = _security.UserGroupRepository.GetGroups("user1").FirstOrDefault();

            Assert.That(group, Is.Not.Null);
            Assert.That(group.Name, Is.EqualTo("group1"));
        }

        [Test]
        public void Check_Existence_User_in_Group()
        {
            var user = _security.UserGroupRepository.GetUsers("group1").FirstOrDefault();

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Login, Is.EqualTo("user1"));
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

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }
}
