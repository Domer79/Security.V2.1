using Security.Tests.SecurityImplement;

namespace Security.Tests.SecurityInMemoryTests
{
    public class MySecurity: V2.Core.Security
    {
        internal MySecurity() 
            : base("HelloWorldApp1", "Hello World Application 1!", IocConfig.GetServiceLocator("HelloWorldApp1"))
        {

        }
    }
}
