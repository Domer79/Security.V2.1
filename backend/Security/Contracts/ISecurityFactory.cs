﻿using Security.Contracts.Repository;

namespace Security.Contracts
{
    public interface ISecurityFactory
    {
        IUserRepository UserRepository { get; set; }
        IUserInternalRepository UserInternalRepository { get; set; }
        IGroupRepository GroupRepository { get; set; }
        IApplicationRepository ApplicationRepository { get; set; }
        IUserGroupRepository UserGroupRepository { get; set; }
        IMemberRoleRepository MemberRoleRepository { get; set; }
        IRoleRepository RoleRepository { get; set; }
        ISecObjectRepository SecObjectRepository { get; set; }
        IGrantRepository GrantRepository { get; set; }
        ISecuritySettings SecuritySettings { get; set; }
        ITokenService TokenService { get; set; }
        IConfig Config { get; set; }
    }
}