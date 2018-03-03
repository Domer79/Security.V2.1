using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Model;
using Security.V2.Contracts.Repository;
using Security.WebApi.Models;

namespace Security.WebApi.Controllers
{
    [RoutePrefix("api/{app}/grants")]
    public class GrantsController : ApiController
    {
        private readonly IGrantRepository _repo;

        public GrantsController(IGrantRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("except/{role}")]
        public async Task<IHttpActionResult> GetExceptRoleGrant(string role)
        {
            var secObjects = await _repo.GetExceptRoleGrantAsync(role);
            return Ok(secObjects);
        }

        [HttpGet]
        [Route("{role}")]
        public async Task<IHttpActionResult> GetRoleGrants(string role)
        {
            var secObject = await _repo.GetExceptRoleGrantAsync(role);
            return Ok(secObject);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGrants(GrantsModel model)
        {
            await _repo.RemoveGrantsAsync(model.Role, model.SecObjects);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> SetGrants([FromBody]GrantsModel model)
        {
            await _repo.SetGrantsAsync(model.Role, model.SecObjects);
            return Ok();
        }
    }
}