using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Security.Tests.SecurityFake;
using Security.Tests.SecurityImplement;
using Security.V2.Contracts;
using Security.V2.Core.Ioc;

namespace Security.Tests.SecurityTests
{
    [TestFixture]
    public class SecurityConfigTest
    {
        private ISecurity _security;

        [SetUp]
        public void Setup()
        {
            //var serviceLocator = IocConfig.GetServiceLocator("HelloWorldApp");
            _security = new MySecurity();
        }

        [TearDown]
        public void TearDown()
        {
            _security.Dispose();
        }

        [Test]
        public void RegisterApplicationTest()
        {
            var application = _security.ApplicationRepository.GetByName("HelloWorldApp");

            Debug.WriteLine(application.IdApplication);
            Console.WriteLine(application.IdApplication);

            Assert.That(application, Has.Property("AppName").EqualTo("HelloWorldApp"));
            Assert.That(application, Has.Property("Description").EqualTo("Hello World Application!"));
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public void SecObjectExistenceTest(string objectName)
        {
            var secObject = _security.SecObjectRepository.GetByName(objectName);
            Assert.That(secObject, Is.Not.Null);
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
            var user = _security.UserRepository.Get("user1");

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
            var group = _security.UserGroupRepository.GetGroupsByUserName("user1").FirstOrDefault();

            Assert.That(group, Is.Not.Null);
            Assert.That(group.Name, Is.EqualTo("group1"));
        }

        [Test]
        public void Check_Existence_User_in_Group()
        {
            var user = _security.UserGroupRepository.GetUsersByGroupName("group1").FirstOrDefault();

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Login, Is.EqualTo("user1"));
        }

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }
}
