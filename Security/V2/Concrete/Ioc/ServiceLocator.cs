﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Tools.Extensions;

namespace Security.V2.Concrete.Ioc
{
    internal class ServiceLocator : IServiceLocator
    {
        private List<RegisterTypeInfo> _registerTypes = new List<RegisterTypeInfo>();

        public RegisterTypeInfo RegisterType(Type serviceType, Type implementType)
        {
            var info = new RegisterTypeInfo(serviceType);
            info.AsSingle(implementType);
            info.RegisterTypes = _registerTypes;
            _registerTypes.Add(info);
            return info;
        }

        public RegisterTypeInfo RegisterType<TService>()
        {
            return RegisterType(typeof(TService));
        }

        public object Resolve(Type serviceType)
        {
            var info = _registerTypes.First(rt => rt.ServiceType == serviceType);
            return info.Resolve();
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        private RegisterTypeInfo RegisterType(Type serviceType)
        {
            return RegisterType(serviceType, null);
        }
    }
}
