using System;

namespace Security.Core.Ioc.Interfaces
{
    public interface IRequest
    {
        void SetService(Type serviceType, object service);
        object GetService(Type serviceType);
    }
}