using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core
{
    public class SecurityWebClient : ISecurity
    {
        public IUserRepository UserRepository => throw new NotImplementedException();

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
