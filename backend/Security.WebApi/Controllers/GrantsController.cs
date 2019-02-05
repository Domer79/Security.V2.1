using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Contracts.Repository;
using Security.WebApi.Models;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// Управление разрешениями
    /// </summary>
    [RoutePrefix("api/{app}/grants")]
    public class GrantsController : ApiController
    {
        private readonly IGrantRepository _repo;

        /// <summary>
        /// Управление разрешениями
        /// </summary>
        /// <param name="repo"></param>
        public GrantsController(IGrantRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Возвращает политики безопасности, не установленные для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("except")]
        public async Task<IHttpActionResult> GetExceptRoleGrant(string role)
        {
            var secObjects = await _repo.GetExceptRoleGrantAsync(role);
            return Ok(secObjects);
        }

        /// <summary>
        /// Возвращает политики безопасности, установленные для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRoleGrants(string role)
        {
            var secObject = await _repo.GetRoleGrantsAsync(role);
            return Ok(secObject);
        }

        /// <summary>
        /// Удаляет указанные политики безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGrants(string role, [FromBody] string[] secObjects)
        {
            await _repo.RemoveGrantsAsync(role, secObjects);
            return Ok();
        }

        /// <summary>
        /// Устанавливает указанные политики безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> SetGrants(string role, [FromBody] string[] secObjects)
        {
            await _repo.SetGrantsAsync(role, secObjects);
            return Ok();
        }
    }
}