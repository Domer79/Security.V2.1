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
        private string _url;

        public MemberRoleRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        protected string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/memberroles"); }
        }

        public void AddMembersToRole(int[] idMembers, int idRole)
        {
            _commonWeb.Put($"{Url}", idMembers, new {idRole});
        }

        public void AddMembersToRole(string[] members, string role)
        {
            _commonWeb.Put($"{Url}", members, new { role });
        }

        public Task AddMembersToRoleAsync(int[] idMembers, int idRole)
        {
            return _commonWeb.PutAsync($"{Url}", idMembers, new { idRole });
        }

        public Task AddMembersToRoleAsync(string[] members, string role)
        {
            return _commonWeb.PutAsync($"{Url}", members, new { role });
        }

        public void AddRolesToMember(int[] idRoles, int idMember)
        {
            _commonWeb.Put($"{Url}", idRoles, idMember);
        }

        public void AddRolesToMember(string[] roles, string member)
        {
            _commonWeb.Put($"{Url}", roles, member);
        }

        public Task AddRolesToMemberAsync(int[] idRoles, int idMember)
        {
            return _commonWeb.PutAsync($"{Url}", idRoles, idMember);
        }

        public Task AddRolesToMemberAsync(string[] roles, string member)
        {
            return _commonWeb.PutAsync($"{Url}", roles, member);
        }

        public void DeleteMembersFromRole(int[] idMembers, int idRole)
        {
            _commonWeb.Delete($"{Url}", idMembers, new {idRole});
        }

        public Task DeleteMembersFromRoleAsync(int[] idMembers, int idRole)
        {
            return _commonWeb.DeleteAsync($"{Url}", idMembers, new { idRole });
        }

        public void DeleteRolesFromMember(int[] idRoles, int idMember)
        {
            _commonWeb.Delete($"{Url}", idRoles, new { idMember });
        }

        public Task DeleteRolesFromMemberAsync(int[] idRoles, int idMember)
        {
            return _commonWeb.DeleteAsync($"{Url}", idRoles, new { idMember });
        }

        public IEnumerable<Role> GetExceptRolesByIdMember(int idMember)
        {
            return _commonWeb.GetCollection<Role>($"{Url}/except", new {idMember});
        }

        public Task<IEnumerable<Role>> GetExceptRolesByIdMemberAsync(int idMember)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}/except", new { idMember });
        }

        public IEnumerable<Role> GetExceptRolesByMemberName(string member)
        {
            return _commonWeb.GetCollection<Role>($"{Url}/except", new { member });
        }

        public Task<IEnumerable<Role>> GetExceptRolesByMemberNameAsync(string member)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}/except", new { member });
        }

        public IEnumerable<Member> GetMembersByIdRole(int idRole)
        {
            return _commonWeb.GetCollection<Member>($"{Url}", new {idRole});
        }

        public Task<IEnumerable<Member>> GetMembersByIdRoleAsync(int idRole)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}", new { idRole });
        }

        public IEnumerable<Member> GetMembersByRoleName(string role)
        {
            return _commonWeb.GetCollection<Member>($"{Url}", new {role});
        }

        public Task<IEnumerable<Member>> GetMembersByRoleNameAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}", new { role });
        }

        public IEnumerable<Role> GetRolesByIdMember(int idMember)
        {
            return _commonWeb.GetCollection<Role>($"{Url}", new { idMember });
        }

        public Task<IEnumerable<Role>> GetRolesByIdMemberAsync(int idMember)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}", new { idMember });
        }

        public IEnumerable<Role> GetRolesByMemberName(string member)
        {
            return _commonWeb.GetCollection<Role>($"{Url}", new { member });
        }

        public Task<IEnumerable<Role>> GetRolesByMemberNameAsync(string member)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}", new { member });
        }
    }
}
