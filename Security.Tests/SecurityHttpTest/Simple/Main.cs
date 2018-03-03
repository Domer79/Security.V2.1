using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Security.Model;
using SecurityHttp;

namespace Security.Tests.SecurityHttpTest.Simple
{
    [TestFixture]
    public class Main
    {
        [Test]
        public void UserCreateTest()
        {
            var security = new MySecurity();
            var user = new User();
            user.Id = Guid.NewGuid();
            user.Login = "Domer";
            user.FirstName = "Damir";
            user.LastName = "Garipov";
            user.MiddleName = "Sagdievich";
            user.Email = "garipovd@mail.ru";
            user.PasswordSalt = Guid.NewGuid().ToString("N");
            security.UserRepository.Create(user);

        }
    }
}
