using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Tests.DI;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core.Ioc;
using Security.V2.DataLayer;

namespace Security.Tests.SecurityInDatabaseTest
{
    internal class IocConfig
    {
        public static IServiceLocator GetServiceLocator()
        {
            var locator = new ServiceLocator();
            locator.RegisterType<IConnectionFactory, SqlConnectionFactory>().InSingletonScope();
            locator.RegisterType<ICommonDb, CommonDb>().InSingletonScope();
            locator.RegisterType<IApplicationInternalRepository, ApplicationInternalRepository>().InSingletonScope();

            return locator;
        }
    }
}
