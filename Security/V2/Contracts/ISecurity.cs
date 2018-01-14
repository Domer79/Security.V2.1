using Security.V2.Contracts.Repository;

namespace Security.V2.Contracts
{
    public interface ISecurity
    {
        /// <summary>
        /// Производит идентификацию пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>True или False</returns>
        bool UserValidate(string login, string password);

        bool CheckAccess(string login, string secObject);

        IApplicationContext ApplicationContext { get; }
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