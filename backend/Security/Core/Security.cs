using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core
{
    /// <summary>
    /// Контекст безопасности
    /// </summary>
    public class Security : ISecurity
    {
        /// <summary>
        /// Контекст безопасности
        /// </summary>
        public Security(string appName, string description = null)
            :this(appName, description, IocConfig.GetLocator(appName))
        {
        }

        internal Security(string appName, string description, IServiceLocator locator)
        {
            Locator = locator;

            //Register application if not exists
            Config.RegisterApplication(appName, description);
        }

        public IUserRepository UserRepository => Factory.UserRepository;

        public IGroupRepository GroupRepository => Factory.GroupRepository;

        public IApplicationRepository ApplicationRepository => Factory.ApplicationRepository;

        public IUserGroupRepository UserGroupRepository => Factory.UserGroupRepository;

        public IMemberRoleRepository MemberRoleRepository => Factory.MemberRoleRepository;

        public IRoleRepository RoleRepository => Factory.RoleRepository;

        public ISecObjectRepository SecObjectRepository => Factory.SecObjectRepository;

        public IGrantRepository GrantRepository => Factory.GrantRepository;

        public ISecuritySettings SecuritySettings => Factory.SecuritySettings;

        public IConfig Config => Factory.Config;

        internal IServiceLocator Locator { get; set; }

        internal IUserInternalRepository UserInternalRepository => Factory.UserInternalRepository;

        private ISecurityFactory Factory => Locator.Resolve<ISecurityFactory>();

        /// <summary>
        /// Проверка доступа пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            return UserInternalRepository.CheckAccess(loginOrEmail, secObject);
        }

        /// <summary>
        /// Проверка доступа пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public bool CheckAccessByToken(string token, string policy)
        {
            return UserInternalRepository.CheckTokenAccess(token, policy);
        }

        /// <summary>
        /// Возвращает пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User GetUserByToken(string token)
        {
            return TokenService.GetUser(token);
        }

        /// <summary>
        /// Установка нового пароля
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SetPassword(string loginOrEmail, string password)
        {
            return UserInternalRepository.SetPassword(loginOrEmail, password);
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CreateToken(string loginOrEmail, string password)
        {
            return UserInternalRepository.CreateToken(loginOrEmail, password);
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckTokenExpire(string token)
        {
            return TokenService.CheckExpire(token);
        }

        /// <summary>
        /// Прекращение действия токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        public void StopExpire(string tokenId, string reason = null)
        {
            TokenService.StopExpire(tokenId, reason);
        }

        /// <summary>
        /// Прекращение действия всех токенов пользователя
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        public void StopExpireForUser(string tokenId, string reason = null)
        {
            TokenService.StopExpireForUser(tokenId, reason);
        }

        /// <summary>
        /// Производит идентификацию пользователя
        /// </summary>
        /// <param name="loginOrEmail">Логин пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>True или False</returns>
        public bool UserValidate(string loginOrEmail, string password)
        {
            return UserInternalRepository.UserValidate(loginOrEmail, password);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Locator.Dispose();
        }

        /// <summary>
        /// Асинхронно. Производит идентификацию пользователя
        /// </summary>
        /// <param name="loginOrEmail">Логин пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>True или False</returns>
        public Task<bool> UserValidateAsync(string loginOrEmail, string password)
        {
            return UserInternalRepository.UserValidateAsync(loginOrEmail, password);
        }

        /// <summary>
        /// Асинхронно. Проверка доступа пользователя
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public Task<bool> CheckAccessAsync(string loginOrEmail, string secObject)
        {
            return UserInternalRepository.CheckAccessAsync(loginOrEmail, secObject);
        }

        /// <summary>
        /// Проверка доступа пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Task<bool> CheckAccessByTokenAsync(string token, string policy)
        {
            return UserInternalRepository.CheckTokenAccessAsync(token, policy);
        }

        /// <summary>
        /// Возвращает пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<User> GetUserByTokenAsync(string token)
        {
            return TokenService.GetUserAsync(token);
        }

        /// <summary>
        /// Асинхронно. Установка нового пароля
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<bool> SetPasswordAsync(string loginOrEmail, string password)
        {
            return UserInternalRepository.SetPasswordAsync(loginOrEmail, password);
        }

        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<string> CreateTokenAsync(string loginOrEmail, string password)
        {
            return UserInternalRepository.CreateTokenAsync(loginOrEmail, password);
        }

        /// <summary>
        /// Проверка срока действия токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> CheckTokenExpireAsync(string token)
        {
            return TokenService.CheckExpireAsync(token);
        }

        /// <summary>
        /// Прекращение действия токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        public Task StopExpireAsync(string tokenId, string reason = null)
        {
            return TokenService.StopExpireAsync(tokenId, reason);
        }

        /// <summary>
        /// Прекращение действия всех токенов пользователя
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="reason"></param>
        public Task StopExpireForUserAsync(string tokenId, string reason = null)
        {
            return TokenService.StopExpireForUserAsync(tokenId, reason);
        }

        private ITokenService TokenService => Factory.TokenService;
    }
}
