using System;
using System.Collections.Generic;
using Security.V2.Contracts;
using Security.V2.Core.Ioc.Interfaces;
using Security.V2.Core.Ioc.Scopes;

namespace Security.V2.Core.Ioc.Dependencies
{
    internal class Dependency : IDependency
    {
        private IScope _scope;
        public Type ImplementType { get; set; }

        public Type ServiceType { get; set; }

        public Func<object> MethodImplement { get; set; }

        public IScope Scope
        {
            get { return _scope ?? (_scope = new TransientScope(Registry)); }
            set { _scope = value; }
        }

        public IRegistry Registry { get; set; }

        public object ResolveByType(IRequest request)
        {
            var constructors = ImplementType.GetConstructors();

            var constructorInfo = constructors[0];
            var parameters = constructorInfo.GetParameters();
            var parameterValues = new List<object>();
            foreach (var parameterInfo in parameters)
            {
                var dependency = Registry.GetFromRegistry(parameterInfo.ParameterType);
                parameterValues.Add(dependency.Scope.GetObject(request, dependency.ServiceType));
            }

            return constructorInfo.Invoke(parameterValues.ToArray());
        }
    }
}