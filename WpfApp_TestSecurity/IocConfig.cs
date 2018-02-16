using Autofac;
using Security.V2.Contracts;
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
            builder.RegisterType<AccessSetupPage>().AsSelf().SingleInstance();
            builder.RegisterType<UsersPage>().AsSelf().SingleInstance();
            builder.RegisterType<RolesPage>().AsSelf().SingleInstance();
            builder.RegisterType<UserEditPage>().AsSelf();

            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
