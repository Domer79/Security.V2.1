using Security.V2.Core.Ioc.Interfaces;

namespace Security.Tests.DI.Concrete
{
    public static class IocService
    {
        public static void InTestRequestScope(this IDependency dependency, ServiceLocatorTest serviceLocatorTest)
        {
            dependency.Scope = new TestRequestScope(dependency.Registry, serviceLocatorTest);
        }
    }
}