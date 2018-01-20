using System;
using Security.V2.Core.Ioc;

namespace Security.V2.Contracts
{
    internal interface IServiceLocator: IDisposable
    {
        RegisterTypeInfo RegisterType(Type serviceType, Type implementType);
        RegisterTypeInfo RegisterType<TService>();

        T Resolve<T>();
        object Resolve(Type serviceType);
    }
}