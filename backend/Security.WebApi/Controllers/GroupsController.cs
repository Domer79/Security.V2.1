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
    /// <summary>
    /// API для работы с группами
    /// </summary>
    [RoutePrefix("api/groups")]
    public class GroupsController : ApiController, ISecurityController<Group>
    {
        private readonly IGroupRepository _repo;

        /// <summary>
        /// API для работы с группами
        /// </summary>
        public GroupsController(IGroupRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Создает пустую группу с заданным префиксом
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> CreateEmpty(string prefix)
        {
            var group = await _repo.CreateEmptyAsync(prefix);
            return Ok(group);
        }

        /// <summary>
        /// Удаляет группу по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.RemoveAsync(id);
            return Ok();
        }

        /// <summary>
        /// Возвращает список всех групп
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var groups = await _repo.GetAsync();
            return Ok(groups);
        }

        /// <summary>
        /// Возвращает группу по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var group = await _repo.GetAsync(id);
            return Ok(group);
        }

        /// <summary>
        /// Возвращает группу по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var group = await _repo.GetByNameAsync(name);
            return Ok(group);
        }

        /// <summary>
        /// Создает новую группу с указанными свойствами
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Group entity)
        {
            var group = await _repo.CreateAsync(entity);
            return Ok(group);
        }

        /// <summary>
        /// Обновляет информацию о группе
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Group entity)
        {
            await _repo.UpdateAsync(entity);
            return Ok();
        }
    }
}
