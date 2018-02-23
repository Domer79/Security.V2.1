using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Model;
using Security.V2.Contracts.Repository;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// API для работы с группами
    /// </summary>
    [RoutePrefix("api/groups")]
    public class GroupsController : ApiController, ISecurityController<Group>
    {
        private readonly IGroupRepository _repo;

        public GroupsController(IGroupRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> CreateEmpty(string prefix)
        {
            var group = await _repo.CreateEmptyAsync(prefix);
            return Ok(group);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.RemoveAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var groups = await _repo.GetAsync();
            return Ok(groups);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var group = await _repo.GetAsync(id);
            return Ok(group);
        }

        [HttpGet]
        [Route("getbyname/{name}")]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var group = await _repo.GetByNameAsync(name);
            return Ok(group);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Group entity)
        {
            var group = await _repo.CreateAsync(entity);
            return Ok(group);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Group entity)
        {
            await _repo.UpdateAsync(entity);
            return Ok();
        }
    }
}
