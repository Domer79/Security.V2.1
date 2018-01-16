using System;
using Security.V2.Concrete.Ioc;

namespace Security.V2.Contracts
{
    internal interface IServiceLocator
    {
        RegisterTypeInfo RegisterType(Type serviceType, Type implementType);
        object Resolve(Type serviceType);

        RegisterTypeInfo RegisterType<TService>();
        T Resolve<T>();
    }
}