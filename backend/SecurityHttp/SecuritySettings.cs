using System;
using System.Threading.Tasks;
using Security.Contracts;
using SecurityHttp.Interfaces;

namespace SecurityHttp
{
    /// <summary>
    /// Управление параметрами для системы доступа
    /// </summary>
    public class SecuritySettings : ISecuritySettings
    {
        private readonly ICommonWeb _commonWeb;
        private readonly string _url;

        /// <summary>
        /// Управление параметрами для системы доступа
        /// </summary>
        public SecuritySettings(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
            _url = "api/settings";
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Значение типа <see cref="T"/></returns>
        public T GetValue<T>(string key)
        {
            var value = GetValue(key, typeof(T));
            return (T)value;
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="type">Тип значения</param>
        /// <returns>Значение типа <see cref="Object"/></returns>
        public object GetValue(string key, Type type)
        {
            var stringValue = _commonWeb.Get<string>(_url, new {key, systemType = type.FullName});
            var value = Convert.ChangeType(stringValue, type);
            return value;
        }

        /// <summary>
        /// Удаляет значение из кеша
        /// </summary>
        /// <param name="key"></param>
        public void RemoveValue(string key)
        {
            _commonWeb.Delete(_url, new {key});
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Значение типа <see cref="T"/></returns>
        public async Task<T> GetValueAsync<T>(string key)
        {
            var value = await GetValueAsync(key, typeof(T));
            return (T)value;
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="type">Тип значения</param>
        /// <returns>Значение типа <see cref="Task{TResult}"/></returns>
        public async Task<object> GetValueAsync(string key, Type type)
        {
            var stringValue = await _commonWeb.GetAsync<string>(_url, new { key, systemType = type.FullName });
            var value = Convert.ChangeType(stringValue, type);
            return value;
        }

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsDeprecated(string key)
        {
            return _commonWeb.Get<bool>($"{_url}/isdeprecated", new {key});
        }

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> IsDeprecatedAsync(string key)
        {
            return _commonWeb.GetAsync<bool>($"{_url}/isdeprecated", new { key });
        }

        /// <summary>
        /// Удаляет значение из кеша
        /// </summary>
        /// <param name="key"></param>
        public Task RemoveValueAsync(string key)
        {
            return _commonWeb.DeleteAsync(_url, new { key });
        }

        /// <summary>
        /// Устанавливает значения для ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <param name="lifetime"></param>
        public void SetValue(string key, object value, TimeSpan? lifetime = null)
        {
            _commonWeb.Post(_url, null, new { key, value, lifetime = lifetime.HasValue ? lifetime.Value.Ticks : 0 });
        }

        /// <summary>
        /// Устанавливает значения для ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <param name="lifetime"></param>
        public Task SetValueAsync(string key, object value, TimeSpan? lifetime = null)
        {
            return _commonWeb.PostAsync(_url, null, new { key, value, lifetime = lifetime.HasValue ? lifetime.Value.Ticks : 0 });
        }
    }
}
