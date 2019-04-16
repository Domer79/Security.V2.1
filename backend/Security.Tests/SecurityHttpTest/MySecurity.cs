using System.Linq;
using System.Text;
using SecurityHttp;

namespace Security.Tests.SecurityHttpTest
{
    public class MySecurity: SecurityWebClient
    {
        public MySecurity() 
            : base("HelloWorldApp1", "Hello World Application 1!", IocConfig.GetLocator("HelloWorldApp1"))
        {
        }
    }
}