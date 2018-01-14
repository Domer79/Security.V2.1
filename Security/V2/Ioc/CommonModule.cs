using Autofac;
using Security.V2.Concrete;
using Security.V2.Contracts;
using Security.V2.DataLayer;

namespace Security.V2.Ioc
{
    public class CommonModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ISecurityDataService>().As<SecurityDataService>();
            builder.RegisterType<IConnectionFactory>().As<SqlConnectionFactory>();
        }
    }

    public class AutofacContainer
    {
        public AutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.Build();
        }


    }
}
