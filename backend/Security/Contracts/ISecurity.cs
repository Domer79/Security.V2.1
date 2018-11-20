using System;
using System.Threading.Tasks;
using Security.Contracts.Repository;

namespace Security.Contracts
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
        /// Проверка доступа пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        bool CheckAccessByToken(string token, string policy);

        /// <summary>
        /// Установка нового пароля
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool SetPassword(string loginOrEmail, string password);

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string CreateToken(string loginOrEmail, string password);

        /// <summary>
        /// Прекращение действия токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        void StopExpire(string tokenId, string reason = null);

        /// <summary>
        /// Прекращение действия всех токенов пользователя
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        void StopExpireForUser(string tokenId, string reason = null);

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
        /// Проверка доступа пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        Task<bool> CheckAccessByTokenAsync(string token, string policy);

        /// <summary>
        /// Асинхронно. Установка нового пароля
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> SetPasswordAsync(string loginOrEmail, string password);

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> CreateTokenAsync(string loginOrEmail, string password);

        /// <summary>
        /// Прекращение действия токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        Task StopExpireAsync(string tokenId, string reason = null);

        /// <summary>
        /// Прекращение действия всех токенов пользователя
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        Task StopExpireForUserAsync(string tokenId, string reason = null);

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