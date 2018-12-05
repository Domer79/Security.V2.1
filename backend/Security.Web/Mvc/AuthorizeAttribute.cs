using System;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Security.Contracts;
using Security.Web.Exceptions;
using Tools.Extensions;

namespace Security.Web.Mvc
{
    /// <summary>
    /// Абстрактный класс атрибута авторизации. Осуществляет проверку авторизации пользователя
    /// </summary>
    public abstract class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private ActionResult _unAuthorizedResult;

        protected AuthorizeAttribute(string policy)
        {
            Policy = policy;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            _unAuthorizedResult = null;
            try
            {
                var security = DependencyResolver.Current.GetService<ISecurity>();
                if (!ConfigHelper.GetAppSettings<bool>("remotemode"))
                    if (httpContext.Request.Params["REMOTE_ADDR"] == "127.0.0.1" ||
                        httpContext.Request.Params["REMOTE_ADDR"] == "::1")
                        return true;
#if DEBUG
                Debugger.Log(0, "Security", $"httpContext.User = {httpContext.User}\r\n");
#endif
                if (!httpContext.User.Identity.IsAuthenticated)
                    return false;

#if DEBUG
                Debugger.Log(0, "Security", $"httpContext.User.Identity = {httpContext.User.Identity}\r\n");
#endif

                var allow = security.CheckAccessByToken(httpContext.User.Identity.Name, Policy);

                if (!allow)
                    if (new HttpRequestWrapper(HttpContext.Current.Request).IsAjaxRequest())
                    {
                        _unAuthorizedResult = GetNotAllowAjaxResult();
                        return true;
                    }
                    else
                    {
                        _unAuthorizedResult = GetNotAllowViewResult();
                    }

                return allow;
            }
            catch (AuthorizeException)
            {
                return base.AuthorizeCore(httpContext);
            }
        }

        /// <summary>
        /// Возвращает страницу с сообщением об ошибке на обычный http запрос
        /// </summary>
        /// <returns>ActionResult</returns>
        protected abstract ActionResult GetNotAllowViewResult();

        /// <summary>
        /// Возвращает сообщение об ошибки на AJAX запрос, задействуется только если в заголовке запроса присутствует атрибут XmlHttpRequest
        /// </summary>
        /// <returns>Сообщение в формате JSON, XML или в любых других форматах</returns>
        protected abstract ActionResult GetNotAllowAjaxResult();

        protected virtual int GetHttpStatusCode()
        {
            return (int) HttpStatusCode.Forbidden;
        }

        /// <summary>
        /// Вызывается, когда для запроса требуется авторизация.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>.</param><exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (_unAuthorizedResult != null)
            {
                filterContext.Result = _unAuthorizedResult;
                filterContext.HttpContext.Response.StatusCode = (int) GetHttpStatusCode();
            }
        }

        public string Policy { get; set; }
    }
}