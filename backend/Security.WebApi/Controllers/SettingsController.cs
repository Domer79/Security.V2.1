using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Contracts;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// Управление настройками
    /// </summary>
    [RoutePrefix("api/settings")]
    public class SettingsController : ApiController
    {
        private readonly ISecuritySettings _settings;

        /// <summary>
        /// Управление настройками
        /// </summary>
        /// <param name="settings"></param>
        public SettingsController(ISecuritySettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Получение значения 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="systemType"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(string key, string systemType)
        {
            var value = await _settings.GetValueAsync(key, Type.GetType(systemType));
            return Ok(value);
        }

        /// <summary>
        /// Обновление значения
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post(string key, string value, long lifetime)
        {
            await _settings.SetValueAsync(key, value, lifetime == 0 ? (TimeSpan?) null : TimeSpan.FromTicks(lifetime));
            return Ok();
        }

        /// <summary>
        /// Проверка актуальности значения настройки
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("isdeprecated")]
        public async Task<IHttpActionResult> IsDeprecated(string key)
        {
            var isDeprecated = await _settings.IsDeprecatedAsync(key);
            return Ok(isDeprecated);
        }

        /// <summary>
        /// Удаление настройки
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(string key)
        {
            await _settings.RemoveValueAsync(key);
            return Ok();
        }
    }
}
