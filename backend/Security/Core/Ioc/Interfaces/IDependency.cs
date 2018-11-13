using System;
using Security.Contracts;

namespace Security.Core.Ioc.Interfaces
{
    public interface IDependency
    {
        Type ImplementType { get; set; }
        Type ServiceType { get; set; }
        Func<object> MethodImplement { get; set; }
        IScope Scope { get; set; }
        object ResolveByType(IRequest request);
        IRegistry Registry { get; set; }
    }
}