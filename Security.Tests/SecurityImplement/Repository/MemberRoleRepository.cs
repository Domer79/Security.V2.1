using System.Collections.Generic;
using System.Linq;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class MemberRoleRepository : IMemberRoleRepository
    {
        public void AddMembersToRole(int[] idMembers, int idRole)
        {
            foreach (var idMember in idMembers)
            {
                var member = Database.Members.First(_ => _.IdMember == idMember);
                var role = Database.Roles.First(_ => _.IdRole == idRole);
                Database.MemberRoles.Add(member, role);
            }
        }

        public void AddMembersToRole(string[] members, string role)
        {
            foreach (var memberName in members)
            {
                var member = Database.Members.First(_ => _.Name == memberName);
                var roleEntity = Database.Roles.First(_ => _.Name == role);
                Database.MemberRoles.Add(member, roleEntity);
            }
        }

        public void AddRolesToMember(int[] idRoles, int idMember)
        {
            foreach (var idRole in idRoles)
            {
                var role = Database.Roles.First(_ => _.IdRole == idRole);
                var member = Database.Members.First(_ => _.IdMember == idMember);
                Database.MemberRoles.Add(member, role);
            }
        }

        public void AddRolesToMember(string[] roles, string member)
        {
            foreach (var roleName in roles)
            {
                var role = Database.Roles.First(_ => _.Name == roleName);
                var memberEntity = Database.Members.First(_ => _.Name == member);
                Database.MemberRoles.Add(memberEntity, role);
            }
        }

        public IEnumerable<Member> GetMembersByIdRole(int idRole)
        {
            var role = Database.Roles.First(_ => _.IdRole == idRole);
            return Database.MemberRoles.GetRoleMembers(role);
        }

        public IEnumerable<Member> GetMembersByRoleName(string role)
        {
            var roleEntity = Database.Roles.First(_ => _.Name == role);
            return Database.MemberRoles.GetRoleMembers(roleEntity);
        }

        public IEnumerable<Role> GetRolesByIdMember(int idMember)
        {
            var member = Database.Members.First(_ => _.IdMember == idMember);
            return Database.MemberRoles.GetMemberRoles(member);
        }

        public IEnumerable<Role> GetRolesByMemberName(string member)
        {
            var memberEntity = Database.Members.First(_ => _.Name == member);
            return Database.MemberRoles.GetMemberRoles(memberEntity);
        }
    }
}