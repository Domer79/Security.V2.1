using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Contracts.Repository;
using Security.Model;
using Security.WebApi.Models;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// Управление ролями участников безопасности
    /// </summary>
    [RoutePrefix("api/{app}/memberroles")]
    public class MemberRolesController : ApiController
    {
        private readonly IMemberRoleRepository _repo;

        /// <summary>
        /// Управление ролями
        /// </summary>
        /// <param name="repo"></param>
        public MemberRolesController(IMemberRoleRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Возвращает список ролей для участника по его имени
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRolesByMemberName(string member)
        {
            var roles = await _repo.GetRolesAsync(member);
            return Ok(roles);
        }

        /// <summary>
        /// Возвращает список ролей для участника по его идентификатору
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRolesByIdMember(int idMember)
        {
            var roles = await _repo.GetRolesAsync(idMember);
            return Ok(roles);
        }

        /// <summary>
        /// Возвращает список участников роли по ее имени
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetMembersByRoleName(string role)
        {
            var members = await _repo.GetMembersAsync(role);
            return Ok(members);
        }

        /// <summary>
        /// Возвращает список участников роли по ее идентификатору
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetMembersByIdRole(int idRole)
        {
            var members = await _repo.GetMembersAsync(idRole);
            return Ok(members);
        }

        /// <summary>
        /// Возвращает список ролей не установленных для участника по его имени
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("except")]
        public async Task<IHttpActionResult> GetExceptRolesByMemberName(string member)
        {
            var roles = await _repo.GetExceptRolesAsync(member);
            return Ok(roles);
        }

        /// <summary>
        /// Возвращает список ролей не установленных для участника по его идентификатору
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("except")]
        public async Task<IHttpActionResult> GetExceptRolesByIdMember(int idMember)
        {
            var roles = await _repo.GetExceptRolesAsync(idMember);
            return Ok(roles);
        }

        /// <summary>
        /// Возвращает список участников не установленных для роли по ее имени
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("except")]
        public async Task<IHttpActionResult> GetExceptMembersByRole(string role)
        {
            var members = await _repo.GetExceptMembersAsync(role);
            return Ok(members);
        }

        /// <summary>
        /// Возвращает список участников не установленных для роли по ее идентификатору
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("except")]
        public async Task<IHttpActionResult> GetExceptMembersByRole(int idRole)
        {
            var members = await _repo.GetExceptMembersAsync(idRole);
            return Ok(members);
        }

        /// <summary>
        /// Добавляет роли участнику
        /// </summary>
        /// <param name="member"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddRolesToMember(string member, [FromBody]string[] roles)
        {
            await _repo.AddRolesToMemberAsync(roles, member);
            return Ok();
        }

        /// <summary>
        /// Добавляет роли участнику
        /// </summary>
        /// <param name="idMember"></param>
        /// <param name="idRoles"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddRolesToMemberByIds(int idMember, [FromBody] int[] idRoles)
        {
            await _repo.AddRolesToMemberAsync(idRoles, idMember);
            return Ok();
        }

        /// <summary>
        /// Добавляет участников для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddMembersToRole(string role, [FromBody] string[] members)
        {
            await _repo.AddMembersToRoleAsync(members, role);
            return Ok();
        }

        /// <summary>
        /// Добавляет участников для роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <param name="idMembers"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddMembersToRoleByIds(int idRole, [FromBody] int[] idMembers)
        {
            await _repo.AddMembersToRoleAsync(idMembers, idRole);
            return Ok();
        }

        /// <summary>
        /// Удаляет роли у участника
        /// </summary>
        /// <param name="idMember"></param>
        /// <param name="idRoles"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRolesFromMember(int idMember, [FromBody]int[] idRoles)
        {
            await _repo.DeleteRolesFromMemberAsync(idRoles, idMember);
            return Ok();
        }

        /// <summary>
        /// Удаляет участников у роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <param name="idMembers"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMembersFromRole(int idRole, [FromBody]int[] idMembers)
        {
            await _repo.DeleteMembersFromRoleAsync(idMembers, idRole);
            return Ok();
        }

        /// <summary>
        /// Удаляет роли у участника
        /// </summary>
        /// <param name="member"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRolesFromMember(string member, [FromBody] string[] roles)
        {
            await _repo.DeleteRolesFromMemberAsync(roles, member);
            return Ok();
        }

        /// <summary>
        /// Удаляет участников у роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMembersFromRole(string role, [FromBody] string[] members)
        {
            await _repo.DeleteMembersFromRoleAsync(members, role);
            return Ok();
        }
    }
}
