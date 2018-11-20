using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Contracts;
using Security.Contracts.Repository;

namespace Security.WebApi.Controllers
{
    public class CommonController : ApiController
    {
        private readonly IUserInternalRepository _repo;
        private readonly IConfig _config;

        public CommonController(IUserInternalRepository repo, IConfig config)
        {
            _repo = repo;
            _config = config;
        }

        [Route("api/{app}/common/checkaccess")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckAccess(string loginOrEmail, string policy)
        {
            var result = await _repo.CheckAccessAsync(loginOrEmail, policy);
            return Ok(result);
        }

        [Route("api/{app}/common/check-access-token")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckAccessByToken(string token, string policy)
        {
            var result = await _repo.CheckTokenAccessAsync(token, policy);
            return Ok(result);
        }

        [Route("api/common/setpassword")]
        [HttpGet]
        public async Task<IHttpActionResult> SetPassword(string loginOrEmail, string password)
        {
            var result = await _repo.SetPasswordAsync(loginOrEmail, password);
            return Ok(result);
        }

        [Route("api/common/validate")]
        [HttpGet]
        public async Task<IHttpActionResult> UserValidate(string loginOrEmail, string password)
        {
            var result = await _repo.UserValidateAsync(loginOrEmail, password);
            return Ok(result);
        }

        [Route("api/common/create-token")]
        [ResponseType(typeof(string))]
        [HttpPost]
        public async Task<IHttpActionResult> CreateToken(string loginOrEmail, string password)
        {
            var token = await _repo.CreateTokenAsync(loginOrEmail, password);
            return Ok(token);
        }

        [Route("api/common/registerapp")]
        [HttpPut]
        public async Task<IHttpActionResult> RegisterApplication(string appName, string description)
        {
            await _config.RegisterApplicationAsync(appName, description);
            return Ok();
        }

        [Route("api/common/registerpolicy")]
        [HttpPut]
        public async Task<IHttpActionResult> RegisterSecurityObjects(string appName, [FromBody] string[] securityObjects)
        {
            await _config.RegisterSecurityObjectsAsync(appName, securityObjects);
            return Ok();
        }
    }
}
