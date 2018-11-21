using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Core.Resolving;
using Autofac.Integration.WebApi;
using Security.CommonContracts;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Core;
using Security.Core.DataLayer;
using Security.Core.DataLayer.Repositories;
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

            if (bool.TryParse(ConfigurationManager.AppSettings["FullLog"], out var fullLog))
            {
                if (fullLog)
                    builder.RegisterType<LoggingActionFilter>().AsWebApiActionFilterFor<ApiController>().SingleInstance();
            }
            builder.RegisterType<ExceptionActionFilter>().AsWebApiExceptionFilterFor<ApiController>().InstancePerRequest();

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
            builder.RegisterType<GroupRepository>().As<IGroupRepository>().InstancePerRequest();
            builder.RegisterType<SecuritySettings>().As<ISecuritySettings>().InstancePerRequest();
            builder.RegisterType<Config>().As<IConfig>().InstancePerRequest();
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerRequest();
        }
    }
}