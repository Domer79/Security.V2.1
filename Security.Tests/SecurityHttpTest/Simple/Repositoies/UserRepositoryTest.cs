using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Security.Model;
using SecurityHttp;
using SecurityHttp.Repositories;

namespace Security.Tests.SecurityHttpTest.Simple.Repositoies
{
    [TestFixture]
    public class UserRepositoryTest
    {
        [Test]
        public void CreateUserTest()
        {
            var repo = new UserRepository(new CommonWeb(new GlobalSettings()));
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

            var readyUser = repo.Create(user);
            Assert.That(readyUser, Is.InstanceOf(typeof(User)));
            Assert.That(readyUser, Has.Property("Login").EqualTo("Domer3"));
        }

        [Test]
        public void CreateEmptyUserTest()
        {
            var repo= new UserRepository(new CommonWeb(new GlobalSettings()));
            var readyUser = repo.CreateEmpty("new_user");

            Assert.That(readyUser, Is.InstanceOf(typeof(User)));
        }
    }
}
