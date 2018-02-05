using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Tests.SecurityFake;
using Security.Tests.SecurityImplement.Repository;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core.Ioc;

namespace Security.Tests.SecurityImplement
{
    internal class IocConfig
    {
        public static IServiceLocator GetServiceLocator(string appName)
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterFactory<ISecurityFactory, SecurityFactory>().InSingletonScope();
            serviceLocator.RegisterType<IApplicationInternalRepository, ApplicationInternalRepository>().InSingletonScope();
            serviceLocator.RegisterType<IApplicationRepository, ApplicationRepository>().InSingletonScope();
            serviceLocator.RegisterType<IGrantRepository, GrantRepository>().InSingletonScope();
            serviceLocator.RegisterType<IGroupRepository, GroupRepository>().InSingletonScope();
            serviceLocator.RegisterType<IMemberRoleRepository, MemberRoleRepository>().InSingletonScope();
            serviceLocator.RegisterType<IRoleRepository, RoleRepository>().InSingletonScope();
            serviceLocator.RegisterType<ISecObjectRepository, SecObjectRepositorty>().InSingletonScope();
            serviceLocator.RegisterType<IUserGroupRepository, UserGroupRepository>().InSingletonScope();
            serviceLocator.RegisterType<IUserRepository, UserRepository>().InSingletonScope();
            serviceLocator.RegisterByMethod(typeof(IApplicationContext), () => new ApplicationContext(appName)).InSingletonScope();
            serviceLocator.RegisterType<IConfig, Config>().InSingletonScope();
            serviceLocator.RegisterType<IUserInternalRepository, UserInternalRepository>().InSingletonScope();
            serviceLocator.RegisterType<ISecuritySettings, SecuritySettings>().InSingletonScope();

            return serviceLocator;
        }
    }
}
