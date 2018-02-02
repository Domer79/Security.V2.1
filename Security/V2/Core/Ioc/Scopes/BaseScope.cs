using System;
using Security.V2.Contracts;
using Security.V2.Core.Ioc.Interfaces;

namespace Security.V2.Core.Ioc.Scopes
{
    internal abstract class BaseScope : IScope
    {
        protected BaseScope(IRegistry registry)
        {
            Registry = registry;
        }

        private bool _isDisposed;

        public bool IsKilled => _isDisposed;

        protected IRegistry Registry { get; }

        public object GetObject(IRequest request, Type serviceType)
        {
            var dependency = Registry.GetFromRegistry(serviceType);
            if (dependency == null)
                throw new ObjectDisposedException($"Object {dependency.ServiceType} disposed");

            var instance = Registry.GetService(serviceType);
            if (instance != null)
                return instance;

            instance = dependency.MethodImplement();
            if (instance != null)
                return instance;

            return dependency.ResolveByType(request);
        }

        public void Kill()
        {
            _isDisposed = true;
        }
    }
}