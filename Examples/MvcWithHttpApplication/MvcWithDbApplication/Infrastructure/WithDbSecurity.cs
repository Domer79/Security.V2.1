using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecurityHttp;

namespace MvcWithDbApplication.Infrastructure
{
    public class WithDbSecurity: SecurityWebClient
    {
        public WithDbSecurity() : base("MvcWithDbApplication", "Тестовое приложение MvcWithDbApplication")
        {
            Config.RegisterSecurityObjects("MvcWithDbApplication", "contact");            
        }
    }
}