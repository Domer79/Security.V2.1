using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Security.Contracts;

namespace Security.Web.Http
{
    /// <summary>
    /// Абстрактный класс атрибута авторизации. Осуществляет проверку авторизации пользователя
    /// </summary>
    public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute, ISecurityObject
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));

            var token = actionContext.Request.Headers.TryGetValues("token", out var headerValues)
                ? headerValues.First()
                : null;

            if (token == null)
                return false;

            var security = DependencyResolver.Current.GetService<ISecurity>();
            return security.CheckAccessByToken(token, ObjectName);
        }

        /// <summary>
        /// Наименование объекта безопасности, для которого требуется запрашивать разрешение на доступ
        /// </summary>
        public string ObjectName { get; set; }
    }
}
