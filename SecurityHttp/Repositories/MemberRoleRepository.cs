using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class MemberRoleRepository : IMemberRoleRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;

        public MemberRoleRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        public void AddMembersToRole(int[] idMembers, int idRole)
        {
            throw new NotImplementedException();
        }

        public void AddMembersToRole(string[] members, string role)
        {
            throw new NotImplementedException();
        }

        public Task AddMembersToRoleAsync(int[] idMembers, int idRole)
        {
            throw new NotImplementedException();
        }

        public Task AddMembersToRoleAsync(string[] members, string role)
        {
            throw new NotImplementedException();
        }

        public void AddRolesToMember(int[] idRoles, int idMember)
        {
            throw new NotImplementedException();
        }

        public void AddRolesToMember(string[] roles, string member)
        {
            throw new NotImplementedException();
        }

        public Task AddRolesToMemberAsync(int[] idRoles, int idMember)
        {
            throw new NotImplementedException();
        }

        public Task AddRolesToMemberAsync(string[] roles, string member)
        {
            throw new NotImplementedException();
        }

        public void DeleteMembersFromRole(int[] idMembers, int idRole)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMembersFromRoleAsync(int[] idMembers, int idRole)
        {
            throw new NotImplementedException();
        }

        public void DeleteRolesFromMember(int[] idRoles, int idMember)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRolesFromMemberAsync(int[] idRoles, int idMember)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetExceptRolesByIdMember(int idMember)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetExceptRolesByIdMemberAsync(int idMember)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetExceptRolesByMemberName(string member)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetExceptRolesByMemberNameAsync(string member)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetMembersByIdRole(int idRole)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Member>> GetMembersByIdRoleAsync(int idRole)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetMembersByRoleName(string role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Member>> GetMembersByRoleNameAsync(string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRolesByIdMember(int idMember)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesByIdMemberAsync(int idMember)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRolesByMemberName(string member)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesByMemberNameAsync(string member)
        {
            throw new NotImplementedException();
        }
    }
}
