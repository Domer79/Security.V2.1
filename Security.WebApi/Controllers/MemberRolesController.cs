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
        public async Task<IHttpActionResult> GetRolesByIdMember(int idMember)
        {
            var roles = await _repo.GetRolesByIdMemberAsync(idMember);
            return Ok(roles);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMembersByRoleName(string role)
        {
            var members = await _repo.GetMembersByRoleNameAsync(role);
            return Ok(members);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMembersByIdRole(int idRole)
        {
            var members = await _repo.GetMembersByIdRoleAsync(idRole);
            return Ok(members);
        }

        [HttpGet]
        [Route("except")]
        public async Task<IHttpActionResult> GetExceptRolesByMemberName(string member)
        {
            var roles = await _repo.GetExceptRolesByMemberNameAsync(member);
            return Ok(roles);
        }

        [HttpGet]
        [Route("except")]
        public async Task<IHttpActionResult> GetExceptRolesByIdMember(int idMember)
        {
            var roles = await _repo.GetExceptRolesByIdMemberAsync(idMember);
            return Ok(roles);
        }

        [HttpPut]
        [Route("rolestomember")]
        public async Task<IHttpActionResult> AddRolesToMember(string member, [FromBody]string[] roles)
        {
            await _repo.AddRolesToMemberAsync(roles, member);
            return Ok();
        }

        [HttpPut]
        [Route("rolestomember")]
        public async Task<IHttpActionResult> AddRolesToMemberByIds(int idMember, [FromBody] int[] idRoles)
        {
            await _repo.AddRolesToMemberAsync(idRoles, idMember);
            return Ok();
        }

        [HttpPut]
        [Route("memberstorole")]
        public async Task<IHttpActionResult> AddMembersToRole(string role, [FromBody] string[] members)
        {
            await _repo.AddMembersToRoleAsync(members, role);
            return Ok();
        }

        [HttpPut]
        [Route("memberstorole")]
        public async Task<IHttpActionResult> AddMembersToRoleByIds(int idRole, [FromBody] int[] idMembers)
        {
            await _repo.AddMembersToRoleAsync(idMembers, idRole);
            return Ok();
        }

        [HttpDelete]
        [Route("RolesFromMember")]
        public async Task<IHttpActionResult> DeleteRolesFromMember(int idMember, [FromBody]int[] idRoles)
        {
            await _repo.DeleteRolesFromMemberAsync(idRoles, idMember);
            return Ok();
        }

        [HttpDelete]
        [Route("MembersFromRole")]
        public async Task<IHttpActionResult> DeleteMembersFromRole(int idRole, [FromBody]int[] idMembers)
        {
            await _repo.DeleteMembersFromRoleAsync(idMembers, idRole);
            return Ok();
        }
    }
}
