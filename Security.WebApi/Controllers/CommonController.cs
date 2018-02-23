using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.V2.Contracts.Repository;

namespace Security.WebApi.Controllers
{
    public class CommonController : ApiController
    {
        private readonly IUserInternalRepository _repo;

        public CommonController(IUserInternalRepository repo)
        {
            _repo = repo;
        }

        [Route("api/{app}/common/checkaccess")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckAccessAsync(string loginOrEmail, string policy)
        {
            var result = await _repo.CheckAccessAsync(loginOrEmail, policy);
            return Ok(result);
        }

        [Route("api/common/setpassword")]
        [HttpGet]
        public async Task<IHttpActionResult> SetPasswordAsync(string loginOrEmail, string password)
        {
            var result = await _repo.SetPasswordAsync(loginOrEmail, password);
            return Ok(result);
        }

        [Route("api/common/validate")]
        [HttpGet]
        public async Task<IHttpActionResult> UserValidateAsync(string loginOrEmail, string password)
        {
            var result = await _repo.UserValidateAsync(loginOrEmail, password);
            return Ok(result);
        }
    }
}
