using System;
using Security.V2.Contracts;
using Security.V2.Core;

namespace Security.Tests.SecurityInDatabaseTest.RepositoryTests
{
    public class BaseTest: IDisposable
    {
        public BaseTest()
        {
            ServiceLocator = IocConfig.GetLocator();
        }

        internal IServiceLocator ServiceLocator { get; }

        public void Dispose()
        {
            ServiceLocator.Dispose();
        }
    }
}