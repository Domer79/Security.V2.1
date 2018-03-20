using System;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

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

        public bool SetPassword(string loginOrEmail, string password)
        {
            return UserInternalRepository.SetPassword(loginOrEmail, password);
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

        public Task<bool> SetPasswordAsync(string loginOrEmail, string password)
        {
            return UserInternalRepository.SetPasswordAsync(loginOrEmail, password);
        }
    }
}
