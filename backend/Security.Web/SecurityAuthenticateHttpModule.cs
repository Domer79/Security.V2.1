using System;
using System.Web;
using System.Web.Mvc;
using Security.Contracts;

namespace Security.Web
{
    /// <summary>
    /// Модуль аутентификации
    /// </summary>
    public class SecurityAuthenticateHttpModule : IHttpModule
    {
        /// <summary>
        /// Инициализация модуля
        /// </summary>
        /// <param name="context">Элемент <see cref="T:System.Web.HttpApplication"/>, что обеспечивает доступ к методам, свойствам и событиям, общим для всех объектов приложений в ASP.NET приложение</param>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += Context_AuthenticateRequest;
        }

        /// <summary>
        /// Обрабатывает событие аутентификации приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Context_AuthenticateRequest(object sender, EventArgs e)
        {
            var httpApplication = ((HttpApplication) sender);
            
            var httpContext = httpApplication.Context;

            var user = httpContext.User;

            if (user?.Identity == null)
                return;

            if (!user.Identity.IsAuthenticated)
                return;

            httpContext.User = new UserPrincipal(user.Identity.Name);
        }

        public void Dispose()
        {
            
        }
    }
}
