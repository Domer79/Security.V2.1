using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace SecurityHttp
{
    public class SecurityFactory : ISecurityFactory
    {
        private readonly IServiceLocator _locator;

        public SecurityFactory(IServiceLocator locator)
        {
            _locator = locator;
        }

        public IUserRepository UserRepository { get; set; }

        public IUserInternalRepository UserInternalRepository { get; set; }

        public IGroupRepository GroupRepository { get; set; }

        public IApplicationRepository ApplicationRepository { get; set; }

        public IApplicationInternalRepository ApplicationInternalRepository { get; set; }

        public IUserGroupRepository UserGroupRepository { get; set; }

        public IMemberRoleRepository MemberRoleRepository { get; set; }

        public IRoleRepository RoleRepository { get; set; }

        public ISecObjectRepository SecObjectRepository { get; set; }

        public IGrantRepository GrantRepository { get; set; }

        public ISecuritySettings SecuritySettings { get; set; }

        public IConfig Config { get; set; }
    }
}
