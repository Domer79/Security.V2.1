using System;
using System.Collections.Generic;
using Security.Core.Ioc;
using Security.Core.Ioc.Interfaces;

namespace Security.Contracts
{
    public interface IServiceLocator: IDisposable, IRegistry
    {
        IDependency RegisterType(Type serviceType, Type implementType);
        IDependency RegisterByMethod(Type serviceType, Func<object> methodImplement);
        IDependency RegisterType<TService, TImplement>();
        IDependency RegisterFactory<TService, TImplement>();

        T Resolve<T>();
        object Resolve(Type serviceType);
    }

    public interface IRegistry
    {
        event AddInstanceHandler AddInstanceEvent;
        object GetService(Type serviceType);
        void SetService(Type serviceType, object service);
        void ServiceInstanceDismiss(Type serviceType);
        IDependency GetFromRegistry(Type serviceType);
        IEnumerable<IDependency> DependencyCollection { get; }
    }
}