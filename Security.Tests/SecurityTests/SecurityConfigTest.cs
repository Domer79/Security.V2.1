using System;
using System.Diagnostics;
using NUnit.Framework;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.Tests.SecurityImplement;
using Security.V2.Contracts;
using Security.V2.Core.Ioc;

namespace Security.Tests.SecurityTests
{
    [TestFixture]
    public class SecurityConfigTest
    {
        [SetUp]
        public void Setup()
        {
            var serviceLocator = IocConfig.GetServiceLocator("");
            using (var security = new V2.Core.Security("", serviceLocator))
            {
                security.Config.RegisterApplication("HelloWorldApp", "Hello World Application!");
                security.Config.RegisterSecurityObjects("1", "2", "3");

                security.UserRepository.Create(new User()
                {
                    Login = "user1",
                    Email = "user1@mail.ru",
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    MiddleName = "Ivanovich",
                    Status = true,
                    DateCreated = DateTime.Now
                });
            }
        }

        [TearDown]
        public void TearDown()
        {
            Database.Drop();
        }

        [Test]
        public void RegisterApplicationTest()
        {
            var serviceLocator = IocConfig.GetServiceLocator("");
            using (var security = new V2.Core.Security("HelloWorldApp", serviceLocator))
            {
                var application = security.ApplicationRepository.GetByName("HelloWorldApp");

                Debug.WriteLine(application.IdApplication);
                Console.WriteLine(application.IdApplication);

                Assert.That(application, Has.Property("AppName").EqualTo("HelloWorldApp"));
                Assert.That(application, Has.Property("Description").EqualTo("Hello World Application!"));
            }
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public void SecObjectExistenceTest(string objectName)
        {
            var serviceLocator = IocConfig.GetServiceLocator("");
            using (var security = new V2.Core.Security("HelloWorldApp", serviceLocator))
            {
                var secObject = security.SecObjectRepository.GetByName(objectName);
                Assert.That(secObject, Is.Not.Null);
            }
        }

        [Test]
        public void PasswordValidateTest()
        {
            var serviceLocator = IocConfig.GetServiceLocator("");
            using (var security = new V2.Core.Security("HelloWorldApp", serviceLocator))
            {
                Assert.That(() => security.SetPassword("user1", "123456"), Is.True);
                Assert.That(() => security.UserValidate("user1", "123456"));

                Assert.That(() => security.SetPassword("user1@mail.ru", "123456"), Is.True);
                Assert.That(() => security.UserValidate("user1@mail.ru", "123456"));
            }
        }

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }
}
