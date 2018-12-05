using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Security.Contracts;

namespace Security.Web
{
    /// <summary>
    /// Представляет контекст безопасности пользователя, от лица которого выполняется код, 
    /// включая удостоверение пользователя (IIdentity) и любые принадлежащие ему роли.
    /// </summary>
    public class UserPrincipal : IPrincipal
    {
        private readonly string _token;

        public UserPrincipal(string token)
        {
            _token = token;
        }

        /// <summary>
        /// Проверяет наличие у пользователя роли <see cref="role"/>
        /// </summary>
        /// <param name="role">Проверяемая роль</param>
        /// <returns>True, если пользователь имеет данную роль, иначе False</returns>
        public bool IsInRole(string role)
        {
            var security = DependencyResolver.Current.GetService<ISecurity>();
            var user = security.GetUserByToken(_token);
            var roles = security.MemberRoleRepository.GetRoles(user.Login);
            return roles.Any(_ => _.Name == role);
        }

        /// <summary>
        /// Удостоверение пользователя <see cref="UserIdentity"/>
        /// </summary>
        public IIdentity Identity => new UserIdentity(_token);

        public override string ToString()
        {
            return Identity.ToString();
        }
    }
}