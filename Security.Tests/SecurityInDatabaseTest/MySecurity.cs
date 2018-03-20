using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Tests.SecurityInDatabaseTest
{
    using Security.V2.Contracts;
    using Security.V2.Core;

    public class MySecurity: Security
    {
        internal MySecurity() 
            : base("HelloWorldApp1", "Hello World Application 1!")
        {
        }
    }
}
