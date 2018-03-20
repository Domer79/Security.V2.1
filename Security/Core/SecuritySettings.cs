﻿using System;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts;
using Security.Exceptions;
using Security.Model;

namespace Security.Core
{
    public class SecuritySettings : ISecuritySettings
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        public SecuritySettings(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        public T GetValue<T>(string key)
        {
            return (T) GetValue(key, typeof(T));
        }

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

        public void RemoveValue(string key)
        {
            _commonDb.ExecuteNonQuery("delete from sec.Settings where name = @key", new {key = GetKey(key)});
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            return (T) await GetValueAsync(key, typeof(T));
        }

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

        public bool IsDeprecated(string key)
        {
            var setting = _commonDb.QuerySingle<Setting>("select * from sec.Settings where name = @key",
                new {key = GetKey(key)});

            return setting.Deprecated;
        }

        public async Task<bool> IsDeprecatedAsync(string key)
        {
            var setting = await _commonDb.QuerySingleAsync<Setting>("select * from sec.Settings where name = @key",
                new { key = GetKey(key) });

            return setting.Deprecated;
        }

        public Task RemoveValueAsync(string key)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.Settings where name = @key", new { key = GetKey(key)});
        }

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