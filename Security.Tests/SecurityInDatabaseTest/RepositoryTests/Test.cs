using NUnit.Framework;
using Security.Model;
using Security.V2.DataLayer;

namespace Security.Tests.SecurityInDatabaseTest.RepositoryTests
{
    [TestFixture]
    public class Test
    {
        private CommonDb _commonDb;

        public Test()
        {
            _commonDb = new CommonDb(new SqlConnectionFactory());
        }

        [Test]
        public void ApplicationInternalRepository_CreateTest()
        {
            var repo = new ApplicationInternalRepository(_commonDb);
            var app = repo.Create(new Application
            {
                AppName = "TestApp1",
                Description = "First Test Application!"
            });

            Assert.That(app.IdApplication, Is.EqualTo(1));
        }
    }
}
