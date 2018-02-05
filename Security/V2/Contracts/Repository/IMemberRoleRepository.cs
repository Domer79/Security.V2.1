using System.Collections.Generic;
using Security.Model;

namespace Security.V2.Contracts.Repository
{
    public interface IMemberRoleRepository
    {
        IEnumerable<Member> GetMembersByIdRole(int idRole);
        IEnumerable<Role> GetRolesByIdMember(int idMember, string appName);

        IEnumerable<Member> GetMembersByRoleName(string role);
        IEnumerable<Role> GetRolesByMemberName(string member, string appName);

        void AddMembersToRole(int[] idMembers, int idRole);
        void AddRolesToMember(int[] idRoles, int idMember);

        void AddMembersToRole(string[] members, string role);
        void AddRolesToMember(string[] roles, string member);
    }
}