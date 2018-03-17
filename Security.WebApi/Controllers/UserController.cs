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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string loginOrEmail)
        {
            var user = await _repo.GetByNameAsync(loginOrEmail);
            return Ok(user);
        }

        [HttpPost]
        [Route("setstatus")]
        public async Task<IHttpActionResult> SetStatus(string loginOrEmail, bool status)
        {
            await _repo.SetStatusAsync(loginOrEmail, status);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> CreateEmpty(string prefixForRequired)
        {
            var user = await _repo.CreateEmptyAsync(prefixForRequired);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await _repo.GetAsync(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var users = await _repo.GetAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(User entity)
        {
            var user = await _repo.CreateAsync(entity);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(User entity)
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
    }
}
