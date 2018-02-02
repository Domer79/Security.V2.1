using Security.V2.Core.Ioc.Scopes;

namespace Security.V2.Core.Ioc.Interfaces
{
    public static class IocService
    {
        public static void InTransientScope(this IDependency dependency)
        {
            dependency.Scope = new TransientScope(dependency.Registry);
        }
    }
}