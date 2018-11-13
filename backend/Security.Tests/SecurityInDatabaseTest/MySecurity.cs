using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Tests.SecurityInDatabaseTest
{
    public class MySecurity: Core.Security
    {
        internal MySecurity() 
            : base("HelloWorldApp1", "Hello World Application 1!")
        {
        }
    }
}
