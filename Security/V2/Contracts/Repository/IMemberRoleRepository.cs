using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.V2.Contracts.Repository
{
    public interface IMemberRoleRepository
    {
        IEnumerable<Member> GetMembers(int idRole);
        IEnumerable<Member> GetMembers(string role);

        IEnumerable<Role> GetRoles(int idMember);
        IEnumerable<Role> GetRoles(string member);

        IEnumerable<Role> GetExceptRoles(int idMember);
        IEnumerable<Role> GetExceptRoles(string member);

        IEnumerable<Member> GetExceptMembers(int idRole);
        IEnumerable<Member> GetExceptMembers(string role);

        void AddMembersToRole(int[] idMembers, int idRole);
        void AddMembersToRole(string[] members, string role);

        void AddRolesToMember(int[] idRoles, int idMember);
        void AddRolesToMember(string[] roles, string member);

        void DeleteMembersFromRole(int[] idMembers, int idRole);
        void DeleteMembersFromRole(string[] members, string role);

        void DeleteRolesFromMember(int[] idRoles, int idMember);
        void DeleteRolesFromMember(string[] roles, string member);

        #region Async

        Task<IEnumerable<Member>> GetMembersAsync(int idRole);
        Task<IEnumerable<Member>> GetMembersAsync(string role);

        Task<IEnumerable<Role>> GetRolesAsync(int idMember);
        Task<IEnumerable<Role>> GetRolesAsync(string member);

        Task<IEnumerable<Role>> GetExceptRolesAsync(int idMember);
        Task<IEnumerable<Role>> GetExceptRolesAsync(string member);

        Task<IEnumerable<Member>> GetExceptMembersAsync(int idRole);
        Task<IEnumerable<Member>> GetExceptMembersAsync(string role);

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