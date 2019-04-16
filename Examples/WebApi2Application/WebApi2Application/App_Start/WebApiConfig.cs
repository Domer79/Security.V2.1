using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Security.Contracts;
using WebApi2Application.Infrastructure;

namespace WebApi2Application
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiConfig).Assembly);
            builder.RegisterType<WebApiSecurity>().As<ISecurity>();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
