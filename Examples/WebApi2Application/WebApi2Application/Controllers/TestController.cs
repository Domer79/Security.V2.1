using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Contracts;
using WebApi2Application.Models;

namespace WebApi2Application.Controllers
{
    using Security.Web.Http;

    /// <summary>
    /// Тестовый контроллер
    /// </summary>
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly ISecurity _security;

        /// <summary>
        /// Тестовый контроллер
        /// </summary>
        public TestController(ISecurity security)
        {
            _security = security;
        }

        /// <summary>
        /// Получение токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("create-token")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> CreateToken(string loginOrEmail, string password)
        {
            return Ok(await _security.CreateTokenAsync(loginOrEmail, password));
        }

        /// <summary>
        /// Тест 1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test1")]
        [Authorize("test1")]
        public IHttpActionResult TestApi1()
        {
            return Ok("Test1 OK!");
        }

        /// <summary>
        /// Тест 2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test2")]
        [Authorize("test2")]
        public IHttpActionResult TestApi2()
        {
            return Ok("Test2 OK!");
        }

        /// <summary>
        /// Тест 3
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test3")]
        [Authorize("test3")]
        public IHttpActionResult TestApi3()
        {
            return Ok("Test3 OK!");
        }
    }
}
