using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Security.V2.Contracts;

namespace Security.Manager.Infrastructure
{
    public class MySecurity: CoreSecurity
    {
        public MySecurity(string appName) : base(appName)
        {
            
        }

        public MySecurity() : base("Security.Manager")
        {
        }
    }
}