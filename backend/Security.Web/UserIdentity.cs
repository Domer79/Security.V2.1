using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using Security.Contracts;
using Security.Model;

namespace Security.Web
{
    /// <summary>
    /// Удостоверение пользователя. Определяет основные возможности объекта удостоверения.
    /// </summary>
    public class UserIdentity : IIdentity
    {
        public UserIdentity(string login)
        {
            User = GetUser(login);
        }

        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public User User { get; }

        private static User GetUser(string token)
        {
            var security = DependencyResolver.Current.GetService<ISecurity>();
            var user = security.GetByToken(token);
            return user;
        }

        /// <summary>
        /// Получает имя текущего пользователя.
        /// </summary>
        /// <returns>
        /// Имя пользователя, от имени которого выполняется код.
        /// </returns>
        public string Name => User == null ? "" : $"{User.LastName} {User.FirstName}";

        /// <summary>
        /// Возвращает тип проверки подлинности.
        /// </summary>
        /// <returns>
        /// Тип проверки подлинности, используемый для идентификации пользователя.
        /// </returns>
        public string AuthenticationType => FormsAuthentication.IsEnabled ? "Forms" : "Unknown";

        /// <summary>
        /// Возвращает значение, указывающее, прошел ли пользователь проверку подлинности.
        /// </summary>
        /// <returns>
        /// true, если пользователь был аутентифицирован; в противном случае false.
        /// </returns>
        public bool IsAuthenticated => User != null;

        /// <summary>
        /// Возвращает имя текущего пользователя
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}