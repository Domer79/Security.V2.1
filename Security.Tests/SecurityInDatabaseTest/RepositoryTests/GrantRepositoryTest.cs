using NUnit.Framework;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityInDatabaseTest.RepositoryTests
{
    [TestFixture]
    public class GrantRepositoryTest : BaseTest
    {
        private IGrantRepository _repo;

        public GrantRepositoryTest()
        {
            _repo = ServiceLocator.Resolve<IGrantRepository>();
        }

    }
}