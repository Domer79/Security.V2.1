using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Security.V2.Concrete.Ioc
{
    internal class RegisterTypeInfo
    {
        private Type _implementType;

        public RegisterTypeInfo(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public Type ImplementType
        {
            get => _implementType;
        }

        public Type ServiceType { get; }

        public List<RegisterTypeInfo> RegisterTypes { get; set; }

        public void AsSingle(Type implementType)
        {
            _implementType = implementType;
        }

        public void AsSingle<TImplement>() where TImplement : class
        {
            _implementType = typeof(TImplement);
        }
    }

    internal static class RegisterTypeHelper
    {
        public static object Resolve(this RegisterTypeInfo info)
        {
            var constructors = info.ImplementType.GetConstructors();
            if (constructors.Length == 0)
                return Activator.CreateInstance(info.ImplementType);

            var constructorInfo = constructors[0];
            var parameters = constructorInfo.GetParameters();
            var parameterValues = new List<object>();
            foreach (var parameterInfo in parameters)
            {
                var registerTypeInfo = info.RegisterTypes.First(rt => rt.ServiceType == parameterInfo.ParameterType);
                parameterValues.Add(registerTypeInfo.Resolve());
            }

            return constructorInfo.Invoke(parameterValues.ToArray());
        }
    }
}