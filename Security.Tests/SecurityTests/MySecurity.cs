using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Tests.SecurityImplement;
using Security.V2.Contracts;

namespace Security.Tests.SecurityTests
{
    public class MySecurity: V2.Core.Security
    {
        internal MySecurity() 
            : base("HelloWorldApp1", "Hello World Application 1!", IocConfig.GetServiceLocator("HelloWorldApp1"))
        {

        }
    }
}
