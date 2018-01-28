using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Security.V2.Contracts;

namespace Security.V2.Core.Ioc
{
    public class RegisterTypeInfo{}

    public interface IDependency
    {
        Type ImplementType { get; set; }
        Type ServiceType { get; set; }
        Func<object> MethodImplement { get; set; }
        object Instance { get; set; }
        IScope Scope { get; set; }
        object ResolveByType(IRequest request);
        IRegistry Registry { get; set; }
    }

    public interface IScope
    {
        object GetObject(IRequest request, Type serviceType);
    }

    internal class Dependency : IDependency
    {
        private IScope _scope;
        public Type ImplementType { get; set; }

        public Type ServiceType { get; set; }

        public Func<object> MethodImplement { get; set; }

        public object Instance { get; set; }

        public IScope Scope
        {
            get { return _scope ?? (_scope = new TransientScope(Registry)); }
            set { _scope = value; }
        }

        public IRegistry Registry { get; set; }

        public object ResolveByType(IRequest request)
        {
            var constructors = ImplementType.GetConstructors();

            var constructorInfo = constructors[0];
            var parameters = constructorInfo.GetParameters();
            var parameterValues = new List<object>();
            foreach (var parameterInfo in parameters)
            {
                var dependency = Registry.GetFromRegistry(parameterInfo.ParameterType);
                parameterValues.Add(dependency.Scope.GetObject(request, dependency.ServiceType));
            }

            return constructorInfo.Invoke(parameterValues.ToArray());
        }
    }

    internal class FactoryDependency : IDependency
    {
        public Type ImplementType { get; set; }
        public Type ServiceType { get; set; }
        public Func<object> MethodImplement { get; set; }
        public object Instance { get; set; }
        public IScope Scope { get; set; }
        public IRegistry Registry { get; set; }

        public object ResolveByType(IRequest request)
        {
            throw new NotImplementedException();
        }
    }

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
    }

    public interface IRequest
    {
        void SetService(Type serviceType, object service);
        object GetService(Type serviceType);
    }

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

    public static class IocService
    {
        public static void InTransientScope(this IDependency dependency)
        {
            dependency.Scope = new TransientScope(dependency.Registry);
        }
    }
}