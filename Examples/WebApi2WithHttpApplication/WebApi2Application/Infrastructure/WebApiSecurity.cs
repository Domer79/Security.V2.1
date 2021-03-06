﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecurityHttp;

namespace WebApi2Application.Infrastructure
{
    public class WebApiSecurity: SecurityWebClient
    {
        public WebApiSecurity() : base("WebApiTestApplication", "Тестовое приложение для Asp.Net WebApi2")
        {
            Config.RegisterSecurityObjects("WebApiTestApplication", "test1", "test2", "test3");
        }
    }
}