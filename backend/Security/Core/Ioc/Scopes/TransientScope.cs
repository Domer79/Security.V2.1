using System;
using Security.Contracts;
using Security.Core.Ioc.Interfaces;

namespace Security.Core.Ioc.Scopes
{
    internal class TransientScope: IScope
    {
        private readonly IRegistry _registry;

        public TransientScope(IRegistry registry)
        {
            _registry = registry;
            registry.AddInstanceEvent += Registry_AddInstanceEvent;
        }

        private void Registry_AddInstanceEvent(object sender, AddInstanceEventArgs args)
        {
            _registry.ServiceInstanceDismiss(args.ServiceType);
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

        public void Dispose()
        {
            _registry.AddInstanceEvent -= Registry_AddInstanceEvent;
        }
    }
}