using System;
using Security.Contracts;
using Security.Core.Ioc.Interfaces;

namespace Security.Core.Ioc.Dependencies
{
    internal class FactoryDependency : IDependency
    {
        public Type ImplementType { get; set; }
        public Type ServiceType { get; set; }
        public Func<object> MethodImplement { get; set; }
        public IScope Scope { get; set; }
        public IRegistry Registry { get; set; }

        public object ResolveByType(IRequest request)
        {
            var properties = ImplementType.GetProperties();
            var factoryInstance = Activator.CreateInstance(ImplementType);

            foreach (var pi in properties)
            {
                var dependency = Registry.GetFromRegistry(pi.PropertyType);
                var propertyInstance = dependency.Scope.GetObject(request, dependency.ServiceType);
                pi.SetValue(factoryInstance, propertyInstance);
            }

            return factoryInstance;
        }
    }
}