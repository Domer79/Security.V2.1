using System;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository;

namespace Security.V2.Contracts
{
    public interface ISecurity: IDisposable
    {
        /// <summary>
        /// Производит идентификацию пользователя
        /// </summary>
        /// <param name="loginOrEmail">Логин пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>True или False</returns>
        bool UserValidate(string loginOrEmail, string password);

        bool CheckAccess(string loginOrEmail, string secObject);

        bool SetPassword(string loginOrEmail, string password);

        IUserRepository UserRepository { get; }
        IGroupRepository GroupRepository { get; }
        IApplicationRepository ApplicationRepository { get; }
        IUserGroupRepository UserGroupRepository { get; }
        IMemberRoleRepository MemberRoleRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISecObjectRepository SecObjectRepository { get; }
        IGrantRepository GrantRepository { get; }
        ISecuritySettings SecuritySettings { get; }
        IConfig Config { get; }
    }
}