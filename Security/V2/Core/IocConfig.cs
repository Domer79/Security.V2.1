﻿using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core.Ioc;
using Security.V2.DataLayer;
using Security.V2.DataLayer.Repositories;

namespace Security.V2.Core
{
    internal class IocConfig 
    {
        public static IServiceLocator GetLocator()
        {
            var locator = new ServiceLocator();
            locator.RegisterType<IConnectionFactory, SqlConnectionFactory>().InSingletonScope();
            locator.RegisterType<ICommonDb, CommonDb>().InSingletonScope();
            locator.RegisterType<IApplicationInternalRepository, ApplicationInternalRepository>().InSingletonScope();
            locator.RegisterType<IGrantRepository, GrantRepository>().InSingletonScope();

            return locator;
        }
    }
}