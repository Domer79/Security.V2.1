using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.V2.Contracts.Repository
{
    public interface IMemberRoleRepository
    {
        IEnumerable<Member> GetMembersByIdRole(int idRole);
        IEnumerable<Role> GetRolesByIdMember(int idMember);

        IEnumerable<Member> GetMembersByRoleName(string role);
        IEnumerable<Role> GetRolesByMemberName(string member);

        IEnumerable<Role> GetExceptRolesByIdMember(int idMember);
        IEnumerable<Role> GetExceptRolesByMemberName(string member);

        IEnumerable<Member> GetExceptMembersByIdRole(int idRole);
        IEnumerable<Member> GetExceptMembersByRoleName(string role);

        void AddMembersToRole(int[] idMembers, int idRole);
        void AddRolesToMember(int[] idRoles, int idMember);

        void AddMembersToRole(string[] members, string role);
        void AddRolesToMember(string[] roles, string member);

        void DeleteMembersFromRole(int[] idMembers, int idRole);
        void DeleteRolesFromMember(int[] idRoles, int idMember);

        void DeleteMembersFromRole(string[] members, string role);
        void DeleteRolesFromMember(string[] roles, string member);

        #region Async

        Task<IEnumerable<Member>> GetMembersByIdRoleAsync(int idRole);
        Task<IEnumerable<Role>> GetRolesByIdMemberAsync(int idMember);

        Task<IEnumerable<Member>> GetMembersByRoleNameAsync(string role);
        Task<IEnumerable<Role>> GetRolesByMemberNameAsync(string member);

        Task<IEnumerable<Role>> GetExceptRolesByIdMemberAsync(int idMember);
        Task<IEnumerable<Role>> GetExceptRolesByMemberNameAsync(string member);

        Task<IEnumerable<Member>> GetExceptMembersByIdRoleAsync(int idRole);
        Task<IEnumerable<Member>> GetExceptMembersByRoleNameAsync(string role);

        Task AddMembersToRoleAsync(int[] idMembers, int idRole);
        Task AddRolesToMemberAsync(int[] idRoles, int idMember);

        Task AddMembersToRoleAsync(string[] members, string role);
        Task AddRolesToMemberAsync(string[] roles, string member);

        Task DeleteMembersFromRoleAsync(int[] idMembers, int idRole);
        Task DeleteRolesFromMemberAsync(int[] idRoles, int idMember);

        Task DeleteMembersFromRoleAsync(string[] members, string role);
        Task DeleteRolesFromMemberAsync(string[] roles, string member);

        #endregion
    }
}