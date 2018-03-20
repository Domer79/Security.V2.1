using System;
using Security.Contracts;
using SecurityHttp;

namespace Security.Tests.SecurityHttpTest.SimpleAsync
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