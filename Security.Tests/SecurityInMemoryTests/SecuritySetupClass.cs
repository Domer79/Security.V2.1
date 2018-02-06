using System;
using NUnit.Framework;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.Tests.SecurityImplement;

namespace Security.Tests.SecurityInMemoryTests
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
                security.Config.RegisterSecurityObjects("HelloWorldApp1", "1", "2", "3");

                security.Config.RegisterApplication("HelloWorldApp2", "Hello World Application 2!");

                using (var security2 =
                    new V2.Core.Security("HelloWorldApp2", "", IocConfig.GetServiceLocator("HelloWorldApp2")))
                {
                    security2.Config.RegisterSecurityObjects("HelloWorldApp2", "1", "4", "5", "6");
                }

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

                security.UserRepository.Create(new User()
                {
                    Login = "user2",
                    Email = "user2@mail.ru",
                    FirstName = "Petr",
                    LastName = "Ivanov",
                    MiddleName = "Petrovich",
                    Status = true,
                    DateCreated = DateTime.Now
                });

                security.UserRepository.Create(new User()
                {
                    Login = "user3",
                    Email = "user3@mail.ru",
                    FirstName = "Petr",
                    LastName = "Ivanov",
                    MiddleName = "Petrovich",
                    Status = true,
                    DateCreated = DateTime.Now
                });

                security.GroupRepository.Create(new Group()
                {
                    Name = "group1",
                    Description = "Group1 Description"
                });

                security.UserGroupRepository.AddGroupsToUser(new[] { "group1" }, "user1");
                security.UserGroupRepository.AddGroupsToUser(new[] { "group1" }, "user2");

                security.RoleRepository.Create(new Role()
                {
                    Name = "role1",
                    Description = "Role1 Description"
                });

                security.RoleRepository.Create(new Role()
                {
                    Name = "role2",
                    Description = "Role2 Description"
                });

                security.MemberRoleRepository.AddMembersToRole(new []{"user1", "group1"}, "role1");
                security.MemberRoleRepository.AddMembersToRole(new []{"user1"}, "role2");
                security.MemberRoleRepository.AddMembersToRole(new []{"user3"}, "role2");

                security.GrantRepository.SetGrant("role1", "1");
                security.GrantRepository.SetGrant("role1", "2");
                security.GrantRepository.SetGrant("role2", "2");
                security.GrantRepository.SetGrant("role2", "3");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Database.Drop();
        }
    }
}