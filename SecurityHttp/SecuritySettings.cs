using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
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
            var value = _commonWeb.Get<T>(_url, new {key});
            return value;
        }

        public object GetValue(string key, Type type)
        {
            var stringValue = _commonWeb.Get<string>(_url, new {key, systemType = type.FullName});
            var value = Convert.ChangeType(stringValue, type);
            return value;
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            var value = await _commonWeb.GetAsync<T>(_url, new { key });
            return value;
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

        public void SetValue<T>(string key, T value, TimeSpan? lifetime = null)
        {
            SetValue(key, (object)value, lifetime);
        }

        public void SetValue(string key, object value, TimeSpan? lifetime = null)
        {
            _commonWeb.Post(_url, new { key, value, lifetime = lifetime.HasValue ? lifetime.Value.Ticks : 0 });
        }

        public Task SetValueAsync<T>(string key, T value, TimeSpan? lifetime = null)
        {
            return SetValueAsync(key, (object) value, lifetime);
        }

        public Task SetValueAsync(string key, object value, TimeSpan? lifetime = null)
        {
            return _commonWeb.PostAsync(_url, new { key, value, lifetime = lifetime.HasValue ? lifetime.Value.Ticks : 0 });
        }
    }
}
