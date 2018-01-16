using System;

namespace Security.V2.Concrete.Ioc
{
    internal class RegisterTypeInfo
    {
        private readonly Type _registerType;
        private Type _implementType;

        public RegisterTypeInfo(Type registerType)
        {
            _registerType = registerType;
        }

        public Type ImplementType
        {
            get => _implementType;
        }

        public Type RegisterType
        {
            get { return _registerType; }
        }

        public void AsSingle(Type implementType)
        {
            _implementType = implementType;
        }

        public void AsSingle<TImplement>() where TImplement : class
        {
            _implementType = typeof(TImplement);
        }
    }
}