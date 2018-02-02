using System;

namespace Security.V2.Core.Ioc.Interfaces
{
    public interface IRequest
    {
        void SetService(Type serviceType, object service);
        object GetService(Type serviceType);
    }
}