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

        void AddMembersToRole(int[] idMembers, int idRole);
        void AddRolesToMember(int[] idRoles, int idMember);

        void AddMembersToRole(string[] members, string role);
        void AddRolesToMember(string[] roles, string member);

        #region Async

        Task<IEnumerable<Member>> GetMembersByIdRoleAsync(int idRole);
        Task<IEnumerable<Role>> GetRolesByIdMemberAsync(int idMember);

        Task<IEnumerable<Member>> GetMembersByRoleNameAsync(string role);
        Task<IEnumerable<Role>> GetRolesByMemberNameAsync(string member);

        Task AddMembersToRoleAsync(int[] idMembers, int idRole);
        Task AddRolesToMemberAsync(int[] idRoles, int idMember);

        Task AddMembersToRoleAsync(string[] members, string role);
        Task AddRolesToMemberAsync(string[] roles, string member);

        #endregion
    }
}