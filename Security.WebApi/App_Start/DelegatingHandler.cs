using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Security.WebApi.Infrastructure.DelegatingHandlers;

namespace Security.WebApi.App_Start
{
    public class DelegatingHandler
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new ApplicationContextRequestHandler());
        }
    }
}