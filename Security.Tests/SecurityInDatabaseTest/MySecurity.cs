using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Security.V2.Core;

namespace Security.Tests.SecurityInDatabaseTest
{
    public class MySecurity: V2.Core.Security
    {
        internal MySecurity() 
            : base("HelloWorldApp1", "Hello World Application 1!", IocConfig.GetLocator("HelloWorldApp1"))
        {
        }
    }
}
