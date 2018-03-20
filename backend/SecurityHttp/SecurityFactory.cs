using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace SecurityHttp
{
    public class SecurityFactory : ISecurityFactory
    {
        public IUserRepository UserRepository { get; set; }

        public IUserInternalRepository UserInternalRepository { get; set; }

        public IGroupRepository GroupRepository { get; set; }

        public IApplicationRepository ApplicationRepository { get; set; }

        public IUserGroupRepository UserGroupRepository { get; set; }

        public IMemberRoleRepository MemberRoleRepository { get; set; }

        public IRoleRepository RoleRepository { get; set; }

        public ISecObjectRepository SecObjectRepository { get; set; }

        public IGrantRepository GrantRepository { get; set; }

        public ISecuritySettings SecuritySettings { get; set; }

        public IConfig Config { get; set; }
    }
}
