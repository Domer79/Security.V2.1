﻿using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core.DataLayer;
using Security.V2.Core.DataLayer.Repositories;
using Security.V2.Core.Ioc;

namespace Security.V2.Core
{
    internal class IocConfig 
    {
        public static IServiceLocator GetLocator(string appName)
        {
            var locator = new ServiceLocator();
            locator.RegisterType<IConnectionFactory, SqlConnectionFactory>().InSingletonScope();
            locator.RegisterType<ICommonDb, CommonDb>().InSingletonScope();
            locator.RegisterType<IApplicationInternalRepository, ApplicationInternalRepository>().InSingletonScope();
            locator.RegisterType<IApplicationRepository, ApplicationRepository>().InSingletonScope();
            locator.RegisterType<IGrantRepository, GrantRepository>().InSingletonScope();
            locator.RegisterType<IGroupRepository, GroupRepository>().InSingletonScope();
            locator.RegisterType<IMemberRoleRepository, MemberRoleRepository>().InSingletonScope();
            locator.RegisterType<IRoleRepository, RoleRepository>().InSingletonScope();
            locator.RegisterType<ISecObjectRepository, SecObjectRepository>().InSingletonScope();
            locator.RegisterType<IUserGroupRepository, UserGroupRepository>().InSingletonScope();
            locator.RegisterType<IUserInternalRepository, UserInternalRepository>().InSingletonScope();
            locator.RegisterType<IUserRepository, UserRepository>().InSingletonScope();
            locator.RegisterType<IConfig, Config>().InSingletonScope();
            locator.RegisterType<ISecuritySettings, SecuritySettings>().InSingletonScope();
            locator.RegisterFactory(typeof(ISecurityFactory), typeof(SecurityFactory)).InSingletonScope();
            locator.RegisterByMethod(typeof(IApplicationContext), () => new ApplicationContext(locator.Resolve<ICommonDb>(), appName));

            return locator;
        }
    }
}