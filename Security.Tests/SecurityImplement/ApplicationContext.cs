using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts;

namespace Security.Tests.SecurityImplement
{
    public class ApplicationContext : IApplicationContext
    {
        public Application Application => Database.Application;
    }
}
