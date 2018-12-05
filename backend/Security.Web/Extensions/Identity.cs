using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Security.Contracts;
using Security.Exceptions;

namespace Security.Web.Extensions
{
    public static class Identity
    {
        /// <summary>
        /// Возвращает логин, вошедшего в систему пользователя
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetLogin(this IIdentity identity)
        {
            var security = DependencyResolver.Current.GetService<ISecurity>();
            return security.GetUserByToken(identity.Name).Login;
        }

        /// <summary>
        /// Производит аутентификацию пользователя по логину и паролю и возвращает уникальный токен
        /// </summary>
        /// <param name="security"></param>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        /// <exception cref="InvalidLoginPasswordException"></exception>
        /// <exception cref="WebException"></exception>
        public static void LoginAndSetCookie(this ISecurity security, string loginOrEmail, string password, bool isPersistent)
        {
            var token = security.CreateToken(loginOrEmail, password);
            FormsAuthentication.SetAuthCookie(token, isPersistent);
        }

        /// <summary>
        /// Производит аутентификацию пользователя по логину и паролю и возвращает уникальный токен
        /// </summary>
        /// <param name="security"></param>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        /// <exception cref="InvalidLoginPasswordException"></exception>
        /// <exception cref="WebException"></exception>
        public static async Task LoginAndSetCookieAsync(this ISecurity security, string loginOrEmail, string password, bool isPersistent)
        {
            var token = await security.CreateTokenAsync(loginOrEmail, password);
            FormsAuthentication.SetAuthCookie(token, isPersistent);
        }
    }
}
