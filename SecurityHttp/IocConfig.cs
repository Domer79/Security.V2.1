using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core.Ioc;
using SecurityHttp.Repositories;

namespace SecurityHttp
{
    public class IocConfig
    {
        public static IServiceLocator GetLocator(string appName)
        {
            var locator = new ServiceLocator();
            locator.RegisterType<IUserRepository, UserRepository>().InSingletonScope();
            return locator;
        }
    }
}
