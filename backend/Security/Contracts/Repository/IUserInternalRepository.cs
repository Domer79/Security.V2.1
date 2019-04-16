using System.Threading.Tasks;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Дополнительное управление пользователями
    /// </summary>
    public interface IUserInternalRepository
    {
        /// <summary>
        /// Проверка доступа по логину
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        bool CheckAccess(string loginOrEmail, string secObject);

        /// <summary>
        /// Установка пароля для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool SetPassword(string loginOrEmail, string password);

        /// <summary>
        /// Проверка аутентификации
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool UserValidate(string loginOrEmail, string password);

        /// <summary>
        /// Создание токена для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string CreateToken(string loginOrEmail, string password);

        /// <summary>
        /// Проверка доступа по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        bool CheckTokenAccess(string token, string policy);

        #region Async

        /// <summary>
        /// Проверка доступа по логину
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        Task<bool> CheckAccessAsync(string loginOrEmail, string secObject);

        /// <summary>
        /// Установка пароля для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> SetPasswordAsync(string loginOrEmail, string password);

        /// <summary>
        /// Проверка аутентификации
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> UserValidateAsync(string loginOrEmail, string password);

        /// <summary>
        /// Создание токена для пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> CreateTokenAsync(string loginOrEmail, string password);

        /// <summary>
        /// Проверка доступа по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        Task<bool> CheckTokenAccessAsync(string token, string policy);

        #endregion
    }
}