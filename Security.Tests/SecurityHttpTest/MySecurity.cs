using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityHttp;

namespace Security.Tests.SecurityHttpTest
{
    public class MySecurity: SecurityWebClient
    {
        public MySecurity() 
            : base("HelloWorldApp1", "Hello World Application 1!")
        {
        }
    }
}
