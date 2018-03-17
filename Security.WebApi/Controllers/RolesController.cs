using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Model;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.WebApi.Infrastructure;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// API для работы с ролями
    /// </summary>
    [RoutePrefix("api/{app}/roles")]
    public class RolesController : ApiController, ISecurityController<Role>
    {
        private readonly IRoleRepository _repository;

        public RolesController(IRoleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Возвращает список всех ролей приложения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var roles = await _repository.GetAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Возвращает роль приложения по ее идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var role = await _repository.GetAsync(id);
            return Ok(role);
        }

        /// <summary>
        /// Создает новую роль
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Role entity)
        {
            var newRole = await _repository.CreateAsync(entity);
            return Ok(newRole);
        }

        /// <summary>
        /// Обновляет имя или описание роли
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Role entity)
        {
            await _repository.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// Удаляет роль по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repository.RemoveAsync(id);
            return Ok();
        }

        /// <summary>
        /// Создает новую роль в базе данных со сначениями по умолчанию
        /// </summary>
        /// <param name="prefix">Префикс для значений по умолчанию</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> CreateEmpty(string prefix)
        {
            var role = await _repository.CreateEmptyAsync(prefix);
            return Ok(role);
        }

        /// <summary>
        /// Возвращает роль приложения по ее имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var role = await _repository.GetByNameAsync(name);
            return Ok(role);
        }
    }
}