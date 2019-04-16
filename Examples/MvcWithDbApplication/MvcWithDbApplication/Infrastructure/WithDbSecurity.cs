using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWithDbApplication.Infrastructure
{
    public class WithDbSecurity: Security.Core.Security
    {
        public WithDbSecurity() : base("MvcWithDbApplication", "Тестовое приложение MvcWithDbApplication")
        {
            Config.RegisterSecurityObjects("MvcWithDbApplication", "contact");            
        }
    }
}