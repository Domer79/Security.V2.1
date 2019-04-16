using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// API для работы с приложениями
    /// </summary>
    [RoutePrefix("api/applications")]
    public class ApplicationsController : ApiController, ISecurityController<Application>
    {
        private readonly IApplicationInternalRepository _repository;
        private readonly IApplicationContext _context;

        /// <summary>
        /// API для работы с приложениями
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="context"></param>
        public ApplicationsController(IApplicationInternalRepository repository, IApplicationContext context)
        {
            _repository = repository;
            _context = context;
        }

        /// <summary>
        /// Создание нового приложения с заданным префиксом
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> CreateEmpty(string prefix)
        {
            var application = await _repository.CreateEmptyAsync(prefix);
            return Ok(application);
        }

        /// <summary>
        /// Удаление приложения по его идентификатору
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
        /// Удаление приложения по имени
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string appName)
        {
            await _repository.RemoveAsync(appName);
            return Ok();
        }

        /// <summary>
        /// Выдает список зарегистрированных приложений
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var applications = await _repository.GetAsync();
            return Ok(applications);
        }

        /// <summary>
        /// Возвращает приложение по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var app = await _repository.GetAsync(id);
            return Ok(app);
        }

        /// <summary>
        /// Возвращает приложение по его имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var app = await _repository.GetByNameAsync(name);
            return Ok(app);
        }

        /// <summary>
        /// Создание приложения
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Application entity)
        {
            var app = await _repository.CreateAsync(entity);
            return Ok(app);
        }

        /// <summary>
        /// Обновление приложения
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Application entity)
        {
            await _repository.UpdateAsync(entity);
            return Ok();
        }
    }
}
