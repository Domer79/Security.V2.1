using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Core.Ioc;
using SecurityHttp.Interfaces;
using SecurityHttp.Repositories;

namespace SecurityHttp
{
    public class IocConfig
    {
        public static IServiceLocator GetLocator(string appName)
        {
            var locator = new ServiceLocator();
            locator.RegisterType<ICommonWeb, CommonWeb>().InSingletonScope();
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
            locator.RegisterByMethod(typeof(IApplicationContext), () => new ApplicationContext(locator.Resolve<IApplicationRepository>(), appName));
            locator.RegisterType<IGlobalSettings, GlobalSettings>().InSingletonScope();

            return locator;
        }
    }
}
