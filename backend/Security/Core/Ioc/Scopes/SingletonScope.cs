using System;
using Security.Contracts;
using Security.Core.Ioc.Interfaces;

namespace Security.Core.Ioc.Scopes
{
    public class SingletonScope : IScope
    {
        private readonly IRegistry _registry;

        public SingletonScope(IRegistry registry)
        {
            _registry = registry;
        }

        public void Dispose()
        {
            
        }

        public object GetObject(IRequest request, Type serviceType)
        {
            var instance = request.GetService(serviceType);
            if (instance != null)
                return instance;

            var dependency = _registry.GetFromRegistry(serviceType);
            if (dependency == null)
                throw new ObjectDisposedException($"Object {dependency.ServiceType} disposed");

            if (dependency.MethodImplement != null)
            {
                instance = dependency.MethodImplement();
                if (instance != null)
                    return instance;
            }

            instance = dependency.ResolveByType(request);
            request.SetService(serviceType, instance);
            return instance;
        }
    }
}
