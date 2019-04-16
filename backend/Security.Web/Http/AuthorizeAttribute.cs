using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Security.Contracts;

namespace Security.Web.Http
{
    /// <summary>
    /// Абстрактный класс атрибута авторизации. Осуществляет проверку авторизации пользователя
    /// </summary>
    public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public AuthorizeAttribute(string policy)
        {
            Policy = policy;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));

            var token = actionContext.Request.Headers.TryGetValues("token", out var headerValues)
                ? headerValues.First()
                : null;

            if (token == null)
                return false;

            var dependencyScope = actionContext.Request.GetDependencyScope();
            var security = (ISecurity)dependencyScope.GetService(typeof(ISecurity));
            return security.CheckAccessByToken(token, Policy);
        }

        /// <summary>
        /// Наименование объекта безопасности, для которого требуется запрашивать разрешение на доступ
        /// </summary>
        public string Policy { get; set; }
    }
}
