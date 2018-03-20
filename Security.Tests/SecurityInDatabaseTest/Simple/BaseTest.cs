using System;
using Security.Contracts;
using Security.Core;

namespace Security.Tests.SecurityInDatabaseTest.Simple
{
    public class BaseTest: IDisposable
    {
        public BaseTest()
        {
            ServiceLocator = IocConfig.GetLocator("HelloWorldApp1");
        }

        internal IServiceLocator ServiceLocator { get; }

        public void Dispose()
        {
            ServiceLocator.Dispose();
        }
    }
}