using System;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts;
using Security.Exceptions;
using Security.Model;

namespace Security.Core
{
    /// <summary>
    /// Управление параметрами для системы доступа
    /// </summary>
    public class SecuritySettings : ISecuritySettings
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        /// <summary>
        /// Управление параметрами для системы доступа
        /// </summary>
        public SecuritySettings(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Значение типа <see cref="T"/></returns>
        public T GetValue<T>(string key)
        {
            return (T) GetValue(key, typeof(T));
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="type">Тип значения</param>
        /// <returns>Значение типа <see cref="Object"/></returns>
        public object GetValue(string key, Type type)
        {
            var value = _commonDb.ExecuteScalar<string>("select value from sec.Settings where name = @key", new { key = GetKey(key) });

            try
            {
                return Convert.ChangeType(value, type);
            }
            catch (Exception e)
            {
                throw new ConvertTypeException(e);
            }
        }

        /// <summary>
        /// Удаляет значение из кеша
        /// </summary>
        /// <param name="key"></param>
        public void RemoveValue(string key)
        {
            _commonDb.ExecuteNonQuery("delete from sec.Settings where name = @key", new {key = GetKey(key)});
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Значение типа <see cref="T"/></returns>
        public async Task<T> GetValueAsync<T>(string key)
        {
            return (T) await GetValueAsync(key, typeof(T));
        }

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="type">Тип значения</param>
        /// <returns>Значение типа <see cref="Task{Object}"/></returns>
        public async Task<object> GetValueAsync(string key, Type type)
        {
            var value = await _commonDb.ExecuteScalarAsync<string>("select value from sec.Settings where name = @key", new { key = GetKey(key) });

            try
            {
                return Convert.ChangeType(value, type);
            }
            catch (Exception e)
            {
                throw new ConvertTypeException(e);
            }
        }

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsDeprecated(string key)
        {
            var setting = _commonDb.QuerySingle<Setting>("select * from sec.Settings where name = @key",
                new {key = GetKey(key)});

            return setting.Deprecated;
        }

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> IsDeprecatedAsync(string key)
        {
            var setting = await _commonDb.QuerySingleAsync<Setting>("select * from sec.Settings where name = @key",
                new { key = GetKey(key) });

            return setting.Deprecated;
        }

        /// <summary>
        /// Удаляет значение из кеша
        /// </summary>
        /// <param name="key"></param>
        public Task RemoveValueAsync(string key)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.Settings where name = @key", new { key = GetKey(key)});
        }

        /// <summary>
        /// Устанавливает значения для ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <param name="lifetime"></param>
        public void SetValue(string key, object value, TimeSpan? lifetime = null)
        {
            var exists = _commonDb.ExecuteScalar<bool>(@"
select
	top 1
	cast(isAny as bit) isAny
from
(
	select 1 as isAny from sec.Settings where name = @key union all select 0
)s
", new { key = GetKey(key) });

            if (exists)
            {
                _commonDb.ExecuteNonQuery("update sec.Settings set value = @value where name = @key",
                    new { key = GetKey(key), value = value.ToString() });
                return;
            }

            long? lt = lifetime.HasValue ? lifetime.Value.Ticks : (long?)null;
            _commonDb.ExecuteNonQuery("insert into sec.Settings(name, value, inDbLifetime, changedDate) values(@key, @value, @lifetime, @changedDate)", new { key = GetKey(key), value = value.ToString(), lifetime = lt, changedDate = DateTime.Now });
        }

        /// <summary>
        /// Устанавливает значения для ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <param name="lifetime"></param>
        public async Task SetValueAsync(string key, object value, TimeSpan? lifetime = null)
        {
            var exists = _commonDb.ExecuteScalarAsync<bool>(@"
select
	top 1
	cast(isAny as bit) isAny
from
(
	select 1 as isAny from sec.Settings where name = @key union all select 0
)s
", new { key = GetKey(key) });

            if (await exists)
            {
                await _commonDb.ExecuteNonQueryAsync("update sec.Settings set value = @value where name = @key",
                    new { key = GetKey(key), value = value.ToString() });
                return;
            }

            long? lt = lifetime.HasValue ? lifetime.Value.Ticks : (long?)null;
            await _commonDb.ExecuteNonQueryAsync("insert into sec.Settings(name, value, inDbLifetime, changedDate) values(@key, @value, @lifetime, @changedDate)", new { key = GetKey(key), value = value.ToString(), lifetime = lt, changedDate = DateTime.Now });
        }

        string GetKey(string key)
        {
            return $"{_context.Application.AppName}_{key}";
        }
    }
}
