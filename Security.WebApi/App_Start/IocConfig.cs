using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Core.Resolving;
using Autofac.Integration.WebApi;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core;
using Security.V2.Core.DataLayer;
using Security.V2.Core.DataLayer.Repositories;
using Security.WebApi.Controllers;
using Security.WebApi.Infrastructure.DelegatingHandlers;
using Security.WebApi.Infrastructure.Filters;

namespace Security.WebApi.App_Start
{
    public class IocConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterWebApiFilterProvider(config);

            Configure(builder);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }

        private static void Configure(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
//            builder.RegisterType<LoggingActionFilter>().AsWebApiActionFilterFor<ApiController>().InstancePerRequest();
//            builder.RegisterType<ExceptionActionFilter>().AsWebApiExceptionFilterFor<ApiController>().InstancePerRequest();

            builder.RegisterType<ApplicationInternalRepository>().As<IApplicationInternalRepository>().InstancePerRequest();
            builder.RegisterType<CommonDb>().As<ICommonDb>().SingleInstance();
            builder.RegisterType<SqlConnectionFactory>().As<IConnectionFactory>().SingleInstance();
            builder.Register(c => new ApplicationContext(c.Resolve<ICommonDb>(), ApplicationContextRequestHandler.ApplicationName)).As<IApplicationContext>().InstancePerRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();
            builder.RegisterType<GrantRepository>().As<IGrantRepository>().InstancePerRequest();
            builder.RegisterType<MemberRoleRepository>().As<IMemberRoleRepository>().InstancePerRequest();
            builder.RegisterType<SecObjectRepository>().As<ISecObjectRepository>().InstancePerRequest();
            builder.RegisterType<UserGroupRepository>().As<IUserGroupRepository>().InstancePerRequest();
            builder.RegisterType<UserInternalRepository>().As<IUserInternalRepository>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
        }
    }
}