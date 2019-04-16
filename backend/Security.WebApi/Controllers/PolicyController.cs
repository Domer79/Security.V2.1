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
    /// Управление политиками текущего приложения
    /// </summary>
    [RoutePrefix("api/{app}/policy")]
    public class PolicyController : ApiController
    {
        private readonly ISecObjectRepository _repo;

        /// <summary>
        /// Управление политиками текущего приложения
        /// </summary>
        /// <param name="repo"></param>
        public PolicyController(ISecObjectRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Возвращает список все политик для текущего приложения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var secObjects = await _repo.GetAsync();
            return Ok(secObjects);
        }

        /// <summary>
        /// Возвращает политику по ее идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var secObject = await _repo.GetAsync(id);
            return Ok(secObject);
        }

        /// <summary>
        /// Создает новую политику
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]SecObject entity)
        {
            var secObject = await _repo.CreateAsync(entity);
            return Ok(secObject);
        }

        /// <summary>
        /// Обновляет политику
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]SecObject entity)
        {
            await _repo.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// Удаление политики
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает политику по ее имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var secObject = await _repo.GetByNameAsync(name);
            return Ok(secObject);
        }
    }
}
