using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using ExceptionHandler = Security.WebApi.Infrastructure.DelegatingHandlers.ExceptionHandler;
using ExceptionLogger = Security.WebApi.Infrastructure.DelegatingHandlers.ExceptionLogger;

namespace Security.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Services.Replace(typeof(IExceptionHandler), new ExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new ExceptionLogger());

            var corsAttribute = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(corsAttribute);

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithApp",
                routeTemplate: "api/{app}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
