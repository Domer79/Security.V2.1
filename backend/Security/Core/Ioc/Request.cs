using System;
using System.Collections.Generic;
using Security.Contracts;
using Security.Core.Ioc.Interfaces;

namespace Security.Core.Ioc
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
            var instance = _registry.GetService(serviceType);
            if (instance != null)
                return instance;

            if (!_instanceRegistry.ContainsKey(serviceType))
                return null;

            return _instanceRegistry[serviceType];
        }

        public void SetService(Type serviceType, object service)
        {
            _instanceRegistry[serviceType] = service;
            _registry.SetService(serviceType, service);
        }
    }
}