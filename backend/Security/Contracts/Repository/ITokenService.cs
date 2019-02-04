using System.Threading.Tasks;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Управление токенами
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        string Create(int idUser);

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        string Create(string loginOrEmail);

        /// <summary>
        /// Прекращение действия отдельного токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        void StopExpire(string tokenId, string reason = null);

        /// <summary>
        /// Прекращение действия всех токенов связанных с пользователем
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        void StopExpireForUser(string tokenId, string reason = null);

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool CheckExpire(string token);

        /// <summary>
        /// Возвращает пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        User GetUser(string token);

        #region async

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<string> CreateAsync(int idUser);

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        Task<string> CreateAsync(string loginOrEmail);

        /// <summary>
        /// Прекращение действия отдельного токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        Task StopExpireAsync(string tokenId, string reason = null);

        /// <summary>
        /// Прекращение действия всех токенов связанных с пользователем
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        Task StopExpireForUserAsync(string tokenId, string reason = null);

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> CheckExpireAsync(string token);

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<User> GetUserAsync(string token);

        #endregion
    }
}