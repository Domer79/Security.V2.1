using Security.V2.Contracts.Repository;

namespace Security.V2.Contracts
{
    public interface ISecurityFactory
    {
        IUserRepository UserRepository { get; }
        IGroupRepository GroupRepository { get; }
        IApplicationRepository ApplicationRepository { get; }
        IUserGroupRepository UserGroupRepository { get; }
        IMemberRoleRepository MemberRoleRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISecObjectRepository SecObjectRepository { get; }
        IGrantRepository GrantRepository { get; }
        ISecuritySettings SecuritySettings { get; }
    }
}