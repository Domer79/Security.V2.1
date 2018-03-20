using System;

namespace Security.Core.Ioc.Interfaces
{
    public interface IScope: IDisposable
    {
        object GetObject(IRequest request, Type serviceType);
    }
}