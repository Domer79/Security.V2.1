using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace SecurityHttp
{
    public class SecurityWebClient : ISecurity
    {
        private string _appName;

        public SecurityWebClient(string appName, string description = null)
            : this(appName, description, IocConfig.GetLocator(appName))
        {
        }

        internal SecurityWebClient(string appName, string description, IServiceLocator locator)
        {
            _appName = appName;
            Locator = locator;

            //Register application if not exists
            Config.RegisterApplication(appName, description);
        }

        public Task StopExpireForUserAsync(string tokenId, string reason = null)
        {
            return TokenService.StopExpireForUserAsync(tokenId, reason);
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

        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            return UserInternalRepository.CheckAccess(loginOrEmail, secObject);
        }

        public bool CheckAccessByToken(string token, string policy)
        {
            return UserInternalRepository.CheckTokenAccess(token, policy);
        }

        public User GetUserByToken(string token)
        {
            return TokenService.GetUser(token);
        }

        public bool SetPassword(string loginOrEmail, string password)
        {
            return UserInternalRepository.SetPassword(loginOrEmail, password);
        }

        public string CreateToken(string loginOrEmail, string password)
        {
            return UserInternalRepository.CreateToken(loginOrEmail, password);
        }

        public bool CheckTokenExpire(string token)
        {
            return TokenService.CheckExpire(token);
        }

        public void StopExpire(string tokenId, string reason = null)
        {
            TokenService.StopExpire(tokenId, reason);
        }

        public void StopExpireForUser(string tokenId, string reason = null)
        {
            TokenService.StopExpireForUser(tokenId, reason);
        }

        public bool UserValidate(string loginOrEmail, string password)
        {
            return UserInternalRepository.UserValidate(loginOrEmail, password);
        }

        public void Dispose()
        {
            Locator.Dispose();
        }

        public Task<bool> UserValidateAsync(string loginOrEmail, string password)
        {
            return UserInternalRepository.UserValidateAsync(loginOrEmail, password);
        }

        public Task<bool> CheckAccessAsync(string loginOrEmail, string secObject)
        {
            return UserInternalRepository.CheckAccessAsync(loginOrEmail, secObject);
        }

        public Task<bool> CheckAccessByTokenAsync(string token, string policy)
        {
            return UserInternalRepository.CheckTokenAccessAsync(token, policy);
        }

        public Task<User> GetUserByTokenAsync(string token)
        {
            return TokenService.GetUserAsync(token);
        }

        public Task<bool> SetPasswordAsync(string loginOrEmail, string password)
        {
            return UserInternalRepository.SetPasswordAsync(loginOrEmail, password);
        }

        public Task<string> CreateTokenAsync(string loginOrEmail, string password)
        {
            return UserInternalRepository.CreateTokenAsync(loginOrEmail, password);
        }

        public Task<bool> CheckTokenExpireAsync(string token)
        {
            return TokenService.CheckExpireAsync(token);
        }

        public Task StopExpireAsync(string tokenId, string reason = null)
        {
            return TokenService.StopExpireAsync(tokenId, reason);
        }

        private ITokenService TokenService => Factory.TokenService;
    }
}
