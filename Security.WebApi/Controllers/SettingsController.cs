using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.V2.Contracts;

namespace Security.WebApi.Controllers
{
    [RoutePrefix("api/settings")]
    public class SettingsController : ApiController
    {
        private readonly ISecuritySettings _settings;

        public SettingsController(ISecuritySettings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(string key, string systemType)
        {
            var value = await _settings.GetValueAsync(key, Type.GetType(systemType));
            return Ok(value);
        }

//        [HttpPost]
        public async Task<IHttpActionResult> Post(string key, string value, long lifetime)
        {
            await _settings.SetValueAsync(key, value, lifetime == 0 ? (TimeSpan?) null : TimeSpan.FromTicks(lifetime));
            return Ok();
        }

        [HttpGet]
        [Route("isdeprecated")]
        public async Task<IHttpActionResult> IsDeprecated(string key)
        {
            var isDeprecated = await _settings.IsDeprecatedAsync(key);
            return Ok(isDeprecated);
        }

        public async Task<IHttpActionResult> Delete(string key)
        {
            await _settings.RemoveValueAsync(key);
            return Ok();
        }
    }
}
