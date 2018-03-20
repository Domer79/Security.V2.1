using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.WebApi.Controllers
{
    [RoutePrefix("api/{app}/policy")]
    public class PolicyController : ApiController
    {
        public ISecObjectRepository _repo { get; }

        public PolicyController(ISecObjectRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var secObjects = await _repo.GetAsync();
            return Ok(secObjects);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var secObject = await _repo.GetAsync(id);
            return Ok(secObject);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]SecObject entity)
        {
            var secObject = await _repo.CreateAsync(entity);
            return Ok(secObject);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]SecObject entity)
        {
            await _repo.UpdateAsync(entity);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.RemoveAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> CreateEmpty(string prefix)
        {
            var secObject = await _repo.CreateEmptyAsync(prefix);
            return Ok(secObject);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var secObject = await _repo.GetByNameAsync(name);
            return Ok(secObject);
        }
    }
}
