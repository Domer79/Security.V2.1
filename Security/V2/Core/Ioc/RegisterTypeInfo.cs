using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Security.V2.Core.Ioc
{
    public interface IDependency
    {
        Type ImplementType { get; }
        Type ServiceType { get; }
        Func<object> MethodImplement { get; }
        object Instance { get; }
    }

    public interface IScope
    {
        IDependency Dependency { get; }
        object GetObject();
        bool IsDisposed { get; }
    }

    public abstract class BaseScope : IScope
    {
        private IDependency _dependency;

        public BaseScope(IDependency dependency)
        {
            _dependency = dependency;
        }

        public IDependency Dependency => _dependency;

        public bool IsDisposed => false;

        public object GetObject()
        {
            return GetInstance()
                .GetInistanceByMethodImplement()
                .GetInstanceByImplementType();
        }

        protected abstract object GetInstanceByImplementType();

        protected abstract BaseScope GetInistanceByMethodImplement();

        protected abstract BaseScope GetInstance();
    }

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

    public class RegisterFactoryInfo
    {

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