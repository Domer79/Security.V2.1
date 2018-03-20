using System;
using System.Linq;
using Security.Contracts;
using Security.Core.Ioc.Interfaces;

namespace Security.Tests.DI.Concrete
{
    public class TestRequestScope : IScope
    {
        private readonly IRegistry _registry;
        private readonly ServiceLocatorTest _serviceLocatorTest;

        public TestRequestScope(IRegistry registry, ServiceLocatorTest serviceLocatorTest)
        {
            _registry = registry;
            _serviceLocatorTest = serviceLocatorTest;
            _serviceLocatorTest.BeginScope += ServiceLocatorTest_BeginScope;
            _serviceLocatorTest.EndScope += ServiceLocatorTest_EndScope;
        }

        private void ServiceLocatorTest_EndScope(object sender, EventArgs e)
        {
            var dependencies = _registry.DependencyCollection.Where(_ => _.Scope is TestRequestScope);
            foreach (var dependency in dependencies)
            {
                _registry.ServiceInstanceDismiss(dependency.ServiceType);
            }
        }

        private void ServiceLocatorTest_BeginScope(object sender, EventArgs e)
        {
            
        }

        public object GetObject(IRequest request, Type serviceType)
        {
            var instance = request.GetService(serviceType);
            if (instance != null)
                return instance;

            var dependency = _registry.GetFromRegistry(serviceType);
            if (dependency == null)
                throw new ObjectDisposedException($"Object {dependency.ServiceType} disposed");

            if (dependency.MethodImplement != null)
            {
                instance = dependency.MethodImplement();
                if (instance != null)
                    return instance;
            }

            instance = dependency.ResolveByType(request);
            request.SetService(serviceType, instance);
            return instance;
        }

        public void Dispose()
        {
            _serviceLocatorTest.BeginScope += ServiceLocatorTest_BeginScope;
            _serviceLocatorTest.EndScope += ServiceLocatorTest_EndScope;
        }
    }
}