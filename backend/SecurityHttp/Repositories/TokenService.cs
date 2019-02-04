using System.Threading.Tasks;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление токенами
    /// </summary>
    public class TokenService: ITokenService
    {
        private readonly ICommonWeb _commonWeb;

        /// <summary>
        /// Управление токенами
        /// </summary>
        public TokenService(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public string Create(int idUser)
        {
            return _commonWeb.PostAndGet<string>("api/token/create-by-id", null, new {idUser});
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        public string Create(string loginOrEmail)
        {
            return _commonWeb.PostAndGet<string>("api/token/create-by-login", null, new {loginOrEmail});
        }

        /// <summary>
        /// Прекращение действия отдельного токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public void StopExpire(string tokenId, string reason = null)
        {
            _commonWeb.Delete("api/token/stop", new {token = tokenId, reason});
        }

        /// <summary>
        /// Прекращение действия всех токенов связанных с пользователем
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public void StopExpireForUser(string tokenId, string reason = null)
        {
            _commonWeb.Delete("api/token/stop-all", new { tokenId, reason });
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckExpire(string token)
        {
            return _commonWeb.Get<bool>("api/token/check-expire", new {token});
        }

        /// <summary>
        /// Возвращает пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User GetUser(string token)
        {
            return _commonWeb.Get<User>("api/token/get-user", new {token});
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public Task<string> CreateAsync(int idUser)
        {
            return _commonWeb.PostAndGetAsync<string>("api/token/create-by-id", null, new { idUser });
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        public Task<string> CreateAsync(string loginOrEmail)
        {
            return _commonWeb.PostAndGetAsync<string>("api/token/create-by-login", null, new { loginOrEmail });
        }

        /// <summary>
        /// Прекращение действия отдельного токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public Task StopExpireAsync(string tokenId, string reason = null)
        {
            return _commonWeb.DeleteAsync("api/token/stop", new { token = tokenId, reason });
        }

        /// <summary>
        /// Прекращение действия всех токенов связанных с пользователем
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason">Причина</param>
        public Task StopExpireForUserAsync(string tokenId, string reason = null)
        {
            return _commonWeb.DeleteAsync("api/token/stop-all", new { tokenId, reason });
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> CheckExpireAsync(string token)
        {
            return _commonWeb.GetAsync<bool>("api/token/check-expire", new { token });
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<User> GetUserAsync(string token)
        {
            return _commonWeb.GetAsync<User>("api/token/get-user", new { token });
        }
    }
}
