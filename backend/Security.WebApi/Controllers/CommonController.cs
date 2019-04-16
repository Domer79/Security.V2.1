using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Contracts;
using Security.Contracts.Repository;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// Общие методы
    /// </summary>
    public class CommonController : ApiController
    {
        private readonly IUserInternalRepository _repo;
        private readonly IConfig _config;

        /// <summary>
        /// Общие методы
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="config"></param>
        public CommonController(IUserInternalRepository repo, IConfig config)
        {
            _repo = repo;
            _config = config;
        }

        /// <summary>
        /// Проверка доступа по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        [Route("api/{app}/common/checkaccess")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckAccess(string loginOrEmail, string policy)
        {
            var result = await _repo.CheckAccessAsync(loginOrEmail, policy);
            return Ok(result);
        }

        /// <summary>
        /// Проверка доступа по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        [Route("api/{app}/common/check-access-token")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckAccessByToken(string token, string policy)
        {
            var result = await _repo.CheckTokenAccessAsync(token, policy);
            return Ok(result);
        }

        /// <summary>
        /// Установка пароля для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("api/common/setpassword")]
        [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> SetPassword(string loginOrEmail, string password)
        {
            var result = await _repo.SetPasswordAsync(loginOrEmail, password);
            return Ok(result);
        }

        /// <summary>
        /// Проверка пароля пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("api/common/validate")]
        [HttpGet]
        public async Task<IHttpActionResult> UserValidate(string loginOrEmail, string password)
        {
            var result = await _repo.UserValidateAsync(loginOrEmail, password);
            return Ok(result);
        }

        /// <summary>
        /// Создание токена для пользователя по логину и паролю
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("api/common/create-token")]
        [ResponseType(typeof(string))]
        [HttpPost]
        public async Task<IHttpActionResult> CreateToken(string loginOrEmail, string password)
        {
            var token = await _repo.CreateTokenAsync(loginOrEmail, password);
            return Ok(token);
        }

        /// <summary>
        /// Регистрация нового приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Route("api/common/registerapp")]
        [HttpPut]
        public async Task<IHttpActionResult> RegisterApplication(string appName, string description)
        {
            await _config.RegisterApplicationAsync(appName, description);
            return Ok();
        }

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
        /// <returns></returns>
        [Route("api/common/registerpolicy")]
        [HttpPut]
        public async Task<IHttpActionResult> RegisterSecurityObjects(string appName, [FromBody] string[] securityObjects)
        {
            await _config.RegisterSecurityObjectsAsync(appName, securityObjects);
            return Ok();
        }
    }
}
