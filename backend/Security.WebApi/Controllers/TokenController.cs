﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// API поддержки токена
    /// </summary>
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        private readonly ITokenService _tokenService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenService"></param>
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("create-by-id")]
        public async Task<IHttpActionResult> Create(int idUser)
        {
            var token = await _tokenService.CreateAsync(idUser);
            return Ok(token);
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("create-by-login")]
        public async Task<IHttpActionResult> Create(string loginOrEmail)
        {
            var token = await _tokenService.CreateAsync(loginOrEmail);
            return Ok(token);
        }

        /// <summary>
        /// Прекращение действия отдельного токена
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("stop")]
        public async Task<IHttpActionResult> StopExpire(string token, string reason = null)
        {
            await _tokenService.StopExpireAsync(token, reason);
            return Ok();
        }

        /// <summary>
        /// Прекращение действия всех токенов связанных с пользователем
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("stop-all")]
        public async Task<IHttpActionResult> StopExpireForUser(string tokenId, string reason = null)
        {
            await _tokenService.StopExpireForUserAsync(tokenId, reason);
            return Ok();
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("check-expire")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> CheckExpire(string token)
        {
            var expired = await _tokenService.CheckExpireAsync(token);
            return Ok(expired);
        }

        /// <summary>
        /// Возвращает пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string token)
        {
            var user = await _tokenService.GetUserAsync(token);
            return Ok(user);
        }
    }
}
