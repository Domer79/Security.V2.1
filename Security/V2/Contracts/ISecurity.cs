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

        /// <summary>
        /// Проверка доступа пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        bool CheckAccess(string loginOrEmail, string secObject);

        /// <summary>
        /// Установка нового пароля
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool SetPassword(string loginOrEmail, string password);

        /// <summary>
        /// Асинхронно. Производит идентификацию пользователя
        /// </summary>
        /// <param name="loginOrEmail">Логин пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>True или False</returns>
        Task<bool> UserValidateAsync(string loginOrEmail, string password);

        /// <summary>
        /// Асинхронно. Проверка доступа пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        Task<bool> CheckAccessAsync(string loginOrEmail, string secObject);

        /// <summary>
        /// Асинхронно. Установка нового пароля
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> SetPasswordAsync(string loginOrEmail, string password);

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