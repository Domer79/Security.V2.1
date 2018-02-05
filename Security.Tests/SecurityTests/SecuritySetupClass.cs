using System;
using NUnit.Framework;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.Tests.SecurityImplement;

namespace Security.Tests.SecurityTests
{
    [SetUpFixture]
    public class SecuritySetupClass
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var serviceLocator = IocConfig.GetServiceLocator("");
//            using (var security = new V2.Core.Security("HelloWorldApp", serviceLocator))
            using (var security = new MySecurity())
            {
                security.Config.RegisterApplication("HelloWorldApp", "Hello World Application!");
                security.Config.RegisterSecurityObjects("HelloWorldApp", "1", "2", "3");

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

                security.GroupRepository.Create(new Group()
                {
                    Name = "group1",
                    Description = "Group1 Description"
                });

                security.UserGroupRepository.AddGroupsToUser(new[] { "group1" }, "user1");
            }

            using (var security = new MySecurity())
            {
                security.RoleRepository.Create(new Role()
                {
                    Name = "role1",
                    Description = "Role1 Description"
                });
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Database.Drop();
        }
    }
}