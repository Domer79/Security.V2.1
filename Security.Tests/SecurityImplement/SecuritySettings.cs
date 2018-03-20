using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts;

namespace Security.Tests.SecurityImplement
{
    internal class SecuritySettings : ISecuritySettings
    {
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<Setting> GetSystemSettings()
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>(string key)
        {
            throw new NotImplementedException();
        }

        public object GetValue(string key, Type type)
        {
            throw new NotImplementedException();
        }

        public void RemoveValue(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetValueAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetValueAsync(string key, Type type)
        {
            throw new NotImplementedException();
        }

        public bool IsDeprecated(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDeprecatedAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveValueAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void SetValue<T>(string key, T value, TimeSpan? lifetime = null)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string key, object value, TimeSpan? lifetime = null)
        {
            throw new NotImplementedException();
        }

        public Task SetValueAsync(string key, object value, TimeSpan? lifetime = null)
        {
            throw new NotImplementedException();
        }
    }
}
