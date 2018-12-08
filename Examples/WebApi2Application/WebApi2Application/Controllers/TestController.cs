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

    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly ISecurity _security;

        public TestController(ISecurity security)
        {
            _security = security;
        }

        [HttpGet]
        [Route("create-token")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> CreateToken(string loginOrEmail, string password)
        {
            return Ok(await _security.CreateTokenAsync(loginOrEmail, password));
        }

        [HttpGet]
        [Route("test_api1")]
        [Authorize("test1")]
        public IHttpActionResult TestApi1()
        {
            return Ok("Test1 OK!");
        }

        [HttpGet]
        [Route("test_api2")]
        [Authorize("test2")]
        public IHttpActionResult TestApi2()
        {
            return Ok("Test2 OK!");
        }

        [HttpGet]
        [Route("test_api3")]
        [Authorize("test3")]
        public IHttpActionResult TestApi3()
        {
            return Ok("Test3 OK!");
        }
    }
}
