﻿using Security.Contracts;
using Security.Contracts.Repository;

namespace Security.Core
{
    internal class SecurityFactory : ISecurityFactory
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

        public ITokenService TokenService { get; set; }

        public IConfig Config { get; set; }
    }
}