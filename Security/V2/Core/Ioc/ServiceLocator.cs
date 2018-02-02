﻿using System;
using System.Collections.Generic;
using Security.V2.Contracts;
using Security.V2.Core.Ioc.Dependencies;
using Security.V2.Core.Ioc.Interfaces;

namespace Security.V2.Core.Ioc
{
    internal class ServiceLocator : IServiceLocator
    {
        private Dictionary<Type, IDependency> _registry = new Dictionary<Type, IDependency>();
        private Dictionary<Type, object> _instanceRegistry = new Dictionary<Type, object>();

        public IEnumerable<IDependency> DependencyCollection => _registry.Values;

        public IDependency RegisterType(Type serviceType, Type implementType)
        {
            IDependency dep = new Dependency();
            dep.ServiceType = serviceType;
            dep.ImplementType = implementType;
            dep.Registry = this;

            _registry[serviceType] = dep;

            return dep;
        }

        public IDependency RegisterByMethod(Type serviceType, Func<object> methodImplement)
        {
            IDependency dep = new Dependency();
            dep.ServiceType = serviceType;
            dep.MethodImplement = methodImplement;
            dep.Registry = this;

            _registry[serviceType] = dep;

            return dep;
        }

        public IDependency RegisterType<TService, TImplement>()
        {
            return RegisterType(typeof(TService), typeof(TImplement));
        }

        public IDependency RegisterFactory(Type factoryService, Type factoryImplement)
        {
            IDependency dep = new FactoryDependency();
            dep.ServiceType = factoryService;
            dep.ImplementType = factoryImplement;
            dep.Registry = this;
            _registry[factoryService] = dep;

            return dep;
        }

        public object Resolve(Type serviceType)
        {
            var dep = _registry[serviceType];
            if (dep.Scope == null)
                dep.InTransientScope();

            var request = new Request(this);
            var instance = dep.Scope.GetObject(request, serviceType);
            SetService(serviceType, instance);
            return instance;
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public void Dispose()
        {
            foreach (var pair in _instanceRegistry)
            {
                var instance = pair.Value as IDisposable;
                if (instance != null)
                    instance.Dispose();

                _instanceRegistry[pair.Key] = null;
                _instanceRegistry.Remove(pair.Key);
            }
        }

        public IDependency GetFromRegistry(Type serviceType)
        {
            return _registry[serviceType];
        }

        public IDependency GetFromRegistry<TService>()
        {
            return GetFromRegistry(typeof(TService));
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _instanceRegistry[serviceType];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public event AddInstanceHandler AddInstanceEvent;

        protected virtual void OnAddInstanceEvent(Type serviceType)
        {
            AddInstanceEvent?.Invoke(this, new AddInstanceEventArgs(){ServiceType = serviceType});
        }

        public void ServiceInstanceDismiss(Type serviceType)
        {
            if (!_instanceRegistry.ContainsKey(serviceType))
                return;

            if (_instanceRegistry[serviceType] is IDisposable instance)
                instance.Dispose();

            _instanceRegistry[serviceType] = null;
            _instanceRegistry.Remove(serviceType);
        }

        public void SetService(Type serviceType, object service)
        {
            _instanceRegistry[serviceType] = service;
            OnAddInstanceEvent(serviceType);
        }
    }
}
