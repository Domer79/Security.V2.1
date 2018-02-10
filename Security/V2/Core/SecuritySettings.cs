using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Exceptions;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts;

namespace Security.V2.Core
{
    internal class SecuritySettings : ISecuritySettings
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
            var value = _commonDb.ExecuteScalar<string>("select value from sec.Settings where name = @key", new {key});

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception e)
            {
                throw new ConvertTypeException(e);
            }
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            var value = await _commonDb.ExecuteScalarAsync<string>("select value from sec.Settings where name = @key", new { key = GetKey(key) });

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
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

        public void SetValue<T>(string key, T value, TimeSpan? lifetime = null)
        {
            var exists = _commonDb.ExecuteScalar<bool>(@"
select
	top 1
	cast(isAny as bit) isAny
from
(
	select 1 as isAny from sec.Settings where name = @key union all select 0
)s
", new {key = GetKey(key)});

            if (exists)
            {
                _commonDb.ExecuteNonQuery("update sec.Settings set value = @value where name = @key",
                    new {key = GetKey(key), value = value.ToString()});
                return;
            }

            long? lt = lifetime.HasValue ? lifetime.Value.Ticks : (long?)null;
            _commonDb.ExecuteNonQuery("insert into sec.Settings(name, value, inDbLifetime, changedDate) values(@key, @value, @lifetime, @changedDate)", new {key = GetKey(key), value = value.ToString(), lifetime = lt, changedDate = DateTime.Now});
        }

        public async Task SetValueAsync<T>(string key, T value, TimeSpan? lifetime = null)
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
