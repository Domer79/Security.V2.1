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
    [RoutePrefix("api/{app}/memberroles")]
    public class MemberRolesController : ApiController
    {
        private readonly IMemberRoleRepository _repo;

        public MemberRolesController(IMemberRoleRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRolesByMemberName(string member)
        {
            var roles = await _repo.GetRolesByMemberNameAsync(member);
            return Ok(roles);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMembersByRoleName(string role)
        {
            var members = await _repo.GetMembersByRoleNameAsync(role);
            return Ok(members);
        }

        [HttpGet]
        [Route("except/{member}")]
        public async Task<IHttpActionResult> GetExceptRolesByMemberName(string member)
        {
            var roles = await _repo.GetExceptRolesByMemberNameAsync(member);
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddRolesToMember([FromBody]MemberRolesModel model)
        {
            await _repo.AddRolesToMemberAsync(model.Roles, model.Member);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRolesFromMember(int idMember, string idRoles)
        {
            await _repo.DeleteRolesFromMemberAsync(idRoles.Split(',').Select(_ => Convert.ToInt32(_)).ToArray(), idMember);
            return Ok();
        }
    }
}
