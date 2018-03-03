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

        public IServiceLocator Locator { get; set; }


        public IUserRepository UserRepository => Locator.Resolve<IUserRepository>();

        public IGroupRepository GroupRepository => throw new NotImplementedException();

        public IApplicationRepository ApplicationRepository => throw new NotImplementedException();

        public IUserGroupRepository UserGroupRepository => throw new NotImplementedException();

        public IMemberRoleRepository MemberRoleRepository => throw new NotImplementedException();

        public IRoleRepository RoleRepository => throw new NotImplementedException();

        public ISecObjectRepository SecObjectRepository => throw new NotImplementedException();

        public IGrantRepository GrantRepository => throw new NotImplementedException();

        public ISecuritySettings SecuritySettings => throw new NotImplementedException();

        public IConfig Config => throw new NotImplementedException();

        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckAccessAsync(string loginOrEmail, string secObject)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool SetPassword(string loginOrEmail, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetPasswordAsync(string loginOrEmail, string password)
        {
            throw new NotImplementedException();
        }

        public bool UserValidate(string loginOrEmail, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserValidateAsync(string loginOrEmail, string password)
        {
            throw new NotImplementedException();
        }
    }
}
