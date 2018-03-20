using System;
using System.Web.Http.Controllers;
using Security.V2.Contracts;

namespace Security.Web.Http
{
    /// <summary>
    /// Абстрактный класс атрибута авторизации. Осуществляет проверку авторизации пользователя
    /// </summary>
    public abstract class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute, ISecurityObject
    {
        private string _controller;
        private string _action;
        private readonly string _applicationName;

        protected AuthorizeAttribute(string applicationName)
        {
            _applicationName = applicationName;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));

            var principal = actionContext.ControllerContext.RequestContext.Principal;
            if (!principal.Identity.IsAuthenticated)
                return false;

            _action = actionContext.ActionDescriptor.ActionName;
            _controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;

            using (var security = new V2.Core.Security(_applicationName))
            {
                var login = ((UserIdentity)principal.Identity).User.Login;
                return security.CheckAccess(login, ((ISecurityObject)this).ObjectName ?? Mvc.AuthorizeAttribute.GetObjectName(_controller, _action));
            }
        }

        /// <summary>
        /// Наименование объекта безопасности, для которого требуется запрашивать разрешение на доступ
        /// </summary>
        public string ObjectName { get; set; }
    }
}
