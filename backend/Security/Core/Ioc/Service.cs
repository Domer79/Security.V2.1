using Security.Core.Ioc.Interfaces;
using Security.Core.Ioc.Scopes;

namespace Security.Core.Ioc
{
    public static class Service
    {
        public static void InSingletonScope(this IDependency dependency)
        {
            if (dependency.Scope != null)
                dependency.Scope.Dispose();

            dependency.Scope = new SingletonScope(dependency.Registry);
        }
    }
}
