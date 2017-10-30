using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace Security.V2.Ioc
{
    public class CommonModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<>()
        }
    }

    public class AutofacContainer
    {
        private Container _container;

        public AutofacContainer()
        {
            var builder = new ContainerBuilder();
            IContainer _container = builder.Build();
            _container.
        }
    }
}
