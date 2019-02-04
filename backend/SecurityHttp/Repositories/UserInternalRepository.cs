using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Дополнительное управление пользователями
    /// </summary>
    public class UserInternalRepository : IUserInternalRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;

        /// <summary>
        /// Дополнительное управление пользователями
        /// </summary>
        public UserInternalRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        /// <summary>
        /// Проверка доступа по логину
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            return _commonWeb.Get<bool>($"api/{_context.Application.AppName}/common/checkaccess", new {loginOrEmail, policy = secObject});
        }

        /// <summary>
        /// Проверка доступа по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public bool CheckTokenAccess(string token, string policy)
        {
            return _commonWeb.Get<bool>($"api/{_context.Application.AppName}/common/check-access-token", new {token, policy});
        }

        /// <summary>
        /// Проверка доступа по логину
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public Task<bool> CheckAccessAsync(string loginOrEmail, string secObject)
        {
            return _commonWeb.GetAsync<bool>($"api/{_context.Application.AppName}/common/checkaccess", new { loginOrEmail, policy = secObject });
        }

        /// <summary>
        /// Установка пароля для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SetPassword(string loginOrEmail, string password)
        {
            return _commonWeb.PostAndGet<bool>("api/common/setpassword", null, new {loginOrEmail, password});
        }

        /// <summary>
        /// Установка пароля для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<bool> SetPasswordAsync(string loginOrEmail, string password)
        {
            return _commonWeb.PostAndGetAsync<bool>("api/common/setpassword", null, new { loginOrEmail, password });
        }

        /// <summary>
        /// Проверка аутентификации
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserValidate(string loginOrEmail, string password)
        {
            return _commonWeb.Get<bool>("api/common/validate", new {loginOrEmail, password});
        }

        /// <summary>
        /// Создание токена для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CreateToken(string loginOrEmail, string password)
        {
            return _commonWeb.PostAndGet<string>("api/common/create-token", null, new {loginOrEmail, password});
        }

        /// <summary>
        /// Проверка аутентификации
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<bool> UserValidateAsync(string loginOrEmail, string password)
        {
            return _commonWeb.GetAsync<bool>("api/common/validate", new { loginOrEmail, password });
        }

        /// <summary>
        /// Создание токена для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<string> CreateTokenAsync(string loginOrEmail, string password)
        {
            return _commonWeb.PostAndGetAsync<string>("api/common/create-token", null, new { loginOrEmail, password });
        }

        /// <summary>
        /// Проверка доступа по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Task<bool> CheckTokenAccessAsync(string token, string policy)
        {
            return _commonWeb.GetAsync<bool>($"api/{_context.Application.AppName}/common/check-access-token", new { token, policy });
        }
    }
}
