using System;
using System.Collections.Generic;
using Security.V2.Contracts;
using Security.V2.Core.Ioc.Interfaces;

namespace Security.V2.Core.Ioc
{
    public class Request : IRequest
    {
        private readonly IRegistry _registry;
        private Dictionary<Type, object> _instanceRegistry = new Dictionary<Type, object>();

        public Request(IRegistry registry)
        {
            _registry = registry;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                var instance = _registry.GetService(serviceType);
                if (instance != null)
                    return instance;

                return _instanceRegistry[serviceType];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public void SetService(Type serviceType, object service)
        {
            _instanceRegistry[serviceType] = service;
            _registry.SetService(serviceType, service);
        }
    }
}