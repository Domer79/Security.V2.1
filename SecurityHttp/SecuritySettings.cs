using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using SecurityHttp.Interfaces;

namespace SecurityHttp
{
    public class SecuritySettings : ISecuritySettings
    {
        private readonly ICommonWeb _commonWeb;
        private readonly string _url;

        public SecuritySettings(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
            _url = "api/settings";
        }

        public T GetValue<T>(string key)
        {
            var value = GetValue(key, typeof(T));
            return (T)value;
        }

        public object GetValue(string key, Type type)
        {
            var stringValue = _commonWeb.Get<string>(_url, new {key, systemType = type.FullName});
            var value = Convert.ChangeType(stringValue, type);
            return value;
        }

        public void RemoveValue(string key)
        {
            _commonWeb.Delete(_url, new {key});
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            var value = await GetValueAsync(key, typeof(T));
            return (T)value;
        }

        public async Task<object> GetValueAsync(string key, Type type)
        {
            var stringValue = await _commonWeb.GetAsync<string>(_url, new { key, systemType = type.FullName });
            var value = Convert.ChangeType(stringValue, type);
            return value;
        }

        public bool IsDeprecated(string key)
        {
            return _commonWeb.Get<bool>($"{_url}/isdeprecated", new {key});
        }

        public Task<bool> IsDeprecatedAsync(string key)
        {
            return _commonWeb.GetAsync<bool>($"{_url}/isdeprecated", new { key });
        }

        public Task RemoveValueAsync(string key)
        {
            return _commonWeb.DeleteAsync(_url, new { key });
        }

        public void SetValue(string key, object value, TimeSpan? lifetime = null)
        {
            _commonWeb.Post(_url, null, new { key, value, lifetime = lifetime.HasValue ? lifetime.Value.Ticks : 0 });
        }

        public Task SetValueAsync(string key, object value, TimeSpan? lifetime = null)
        {
            return _commonWeb.PostAsync(_url, null, new { key, value, lifetime = lifetime.HasValue ? lifetime.Value.Ticks : 0 });
        }
    }
}
