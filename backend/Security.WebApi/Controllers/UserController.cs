using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _repo;

        /// <summary>
        /// Управление пользователями
        /// </summary>
        /// <param name="repo"></param>
        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Возвращает пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetByName(string loginOrEmail)
        {
            var user = await _repo.GetByNameAsync(loginOrEmail);
            return Ok(user);
        }

        /// <summary>
        /// Установка статуса пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("setstatus")]
        public async Task<IHttpActionResult> SetStatus(string loginOrEmail, bool status)
        {
            await _repo.SetStatusAsync(loginOrEmail, status);
            return Ok();
        }

        /// <summary>
        /// Создание пустого пользователя с заданным префиксом
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> CreateEmpty(string prefixForRequired)
        {
            var user = await _repo.CreateEmptyAsync(prefixForRequired);
            return Ok(user);
        }

        /// <summary>
        /// Возвращает пользователя по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await _repo.GetAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Возвращает список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<User>))]
        public async Task<IHttpActionResult> Get()
        {
            var users = await _repo.GetAsync();
            return Ok(users);
        }

        /// <summary>
        /// Создание пустого пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post(User entity)
        {
            var user = await _repo.CreateAsync(entity);
            return Ok(user);
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(User entity)
        {
            await _repo.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.RemoveAsync(id);
            return Ok();
        }
    }
}
