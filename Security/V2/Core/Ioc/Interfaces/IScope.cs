using System;

namespace Security.V2.Core.Ioc.Interfaces
{
    public interface IScope: IDisposable
    {
        object GetObject(IRequest request, Type serviceType);
    }
}