﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Security.WebApi.App_Start;

namespace Security.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(IocConfig.Register);
            GlobalConfiguration.Configure(DelegatingHandler.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AreaRegistration.RegisterAllAreas();
        }
    }
}
