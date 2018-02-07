using System.Linq;
using NUnit.Framework;
using Security.Model;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityInDatabaseTest.RepositoryTests
{
    [TestFixture]
    public class ApplicationInternalRepositoryTest: BaseTest
    {
        private IApplicationInternalRepository _repo;

        public ApplicationInternalRepositoryTest()
        {
            _repo = ServiceLocator.Resolve<IApplicationInternalRepository>();
        }

        [Test, Order(0)]
        public void CreateTest()
        {
            var app = _repo.Create(new Application
            {
                AppName = "TestApp1",
                Description = "First Test Application!"
            });

            Assert.That(app.IdApplication, Is.EqualTo(1));
        }

        [Test, Order(1)]
        public void GetByNameTest()
        {
            var app = _repo.GetByName("TestApp1");

            Assert.That(app, Is.Not.Null);
            Assert.That(app, Has.Property("AppName").EqualTo("TestApp1"));
            Assert.That(app, Has.Property("Description").EqualTo("First Test Application!"));
        }

        [Test, Order(2)]
        public void GetAllTest()
        {
            var apps = _repo.Get();
            Assert.AreEqual(1, apps.Count());
        }

        [Test, Order(3)]
        public void GetByIdTest()
        {
            var app = _repo.Get(1);
            Assert.That(app, Is.InstanceOf(typeof(Application)));
        }

        [Test, Order(4)]
        public void UpdateTest()
        {
            var app = new Application()
            {
                IdApplication = 1,
                AppName = "qwerty",
                Description = "asdfgh"
            };

            _repo.Update(app);
            var updatedApp = _repo.Get(1);
            Assert.That(updatedApp, Has.Property("AppName").EqualTo("qwerty"));
            Assert.That(updatedApp, Has.Property("Description").EqualTo("asdfgh"));
        }

        [Test, Order(5)]
        public void RemoveApp()
        {
            _repo.Remove(1);
            var apps = _repo.Get();

            CollectionAssert.IsEmpty(apps);
        }
    }
}
