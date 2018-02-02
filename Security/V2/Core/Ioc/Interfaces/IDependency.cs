using System;
using Security.V2.Contracts;

namespace Security.V2.Core.Ioc.Interfaces
{
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
}