using Autofac;
using CommonContracts;
using Orm;
using Security.V2.Contracts;
using WpfApp_TestSecurity.AbonentBL;
using WpfApp_TestSecurity.AbonentBL.Interfaces;
using WpfApp_TestSecurity.AbonentBL.Repositories;
using WpfApp_TestSecurity.Pages;
using WpfApp_TestSecurity.Pages.Left;
using WpfApp_TestSecurity.Pages.Right;
using WpfApp_TestSecurity.ViewModelManagers;

namespace WpfApp_TestSecurity
{
    public class IocConfig
    {
        private static IContainer _container;

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Security.V2.Core.Security>().As<ISecurity>().WithParameter("appName", "IPPS.LightingSystem");
            builder.RegisterType<UserManager>().AsSelf().SingleInstance();
            builder.RegisterType<RoleManager>().AsSelf().SingleInstance();
            builder.RegisterType<UserRoleManager>().AsSelf().SingleInstance();
            builder.RegisterType<PolicyManager>().AsSelf().SingleInstance();
            builder.RegisterType<AbonentManager>().AsSelf();
            builder.RegisterType<AccessSetupPage>().AsSelf().SingleInstance();
            builder.RegisterType<UsersPage>().AsSelf();//.SingleInstance();
            builder.RegisterType<RolesPage>().AsSelf();//.SingleInstance();
            builder.RegisterType<UserEditPage>().AsSelf();
            builder.RegisterType<RoleEditPage>().AsSelf();
            builder.RegisterType<LoginPage>().AsSelf();
            builder.RegisterType<Abonents>().AsSelf();
            builder.RegisterType<AccessDeniedPage>().AsSelf();
            builder.RegisterType<GlobalSettings>().As<IGlobalSettings>();
            builder.RegisterType<CommonDb>().As<ICommonDb>();
            builder.RegisterType<SqlConnectionFactory>().As<CommonContracts.IConnectionFactory>();
            builder.RegisterType<AbonentRepository>().As<IAbonentRepository>();

            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
