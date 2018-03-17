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
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="context"></param>
        public ApplicationsController(IApplicationInternalRepository repository, IApplicationContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<IHttpActionResult> CreateEmpty(string prefix)
        {
            var application = await _repository.CreateEmptyAsync(prefix);
            return Ok(application);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repository.RemoveAsync(id);
            return Ok();
        }

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

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var app = await _repository.GetAsync(id);
            return Ok(app);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var app = await _repository.GetByNameAsync(name);
            return Ok(app);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Application entity)
        {
            var app = await _repository.CreateAsync(entity);
            return Ok(app);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Application entity)
        {
            await _repository.UpdateAsync(entity);
            return Ok();
        }
    }
}
