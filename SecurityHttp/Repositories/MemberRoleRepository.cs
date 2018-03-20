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

        public IEnumerable<Member> GetExceptMembers(string role)
        {
            return _commonWeb.GetCollection<Member>($"{Url}/except", new {role});
        }

        public void AddMembersToRole(int[] idMembers, int idRole)
        {
            _commonWeb.Put($"{Url}", idMembers, new {idRole});
        }

        public void AddMembersToRole(string[] members, string role)
        {
            _commonWeb.Put($"{Url}", members, new { role });
        }

        public Task<IEnumerable<Member>> GetExceptMembersAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}/except", new { role });
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
            _commonWeb.Put($"{Url}", idRoles, new {idMember});
        }

        public void AddRolesToMember(string[] roles, string member)
        {
            _commonWeb.Put($"{Url}", roles, new {member});
        }

        public Task AddRolesToMemberAsync(int[] idRoles, int idMember)
        {
            return _commonWeb.PutAsync($"{Url}", idRoles, new {idMember});
        }

        public Task AddRolesToMemberAsync(string[] roles, string member)
        {
            return _commonWeb.PutAsync($"{Url}", roles, new {member});
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

        public void DeleteMembersFromRole(string[] members, string role)
        {
            _commonWeb.Delete(Url, members, new {role});
        }

        public void DeleteRolesFromMember(string[] roles, string member)
        {
            _commonWeb.Delete(Url, roles, new {member});
        }

        public Task DeleteRolesFromMemberAsync(int[] idRoles, int idMember)
        {
            return _commonWeb.DeleteAsync($"{Url}", idRoles, new { idMember });
        }

        public Task DeleteMembersFromRoleAsync(string[] members, string role)
        {
            return _commonWeb.DeleteAsync(Url, members, new { role });
        }

        public Task DeleteRolesFromMemberAsync(string[] roles, string member)
        {
            return _commonWeb.DeleteAsync(Url, roles, new { member });
        }

        public IEnumerable<Role> GetExceptRoles(int idMember)
        {
            return _commonWeb.GetCollection<Role>($"{Url}/except", new {idMember});
        }

        public Task<IEnumerable<Role>> GetExceptRolesAsync(int idMember)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}/except", new { idMember });
        }

        public IEnumerable<Role> GetExceptRoles(string member)
        {
            return _commonWeb.GetCollection<Role>($"{Url}/except", new { member });
        }

        public IEnumerable<Member> GetExceptMembers(int idRole)
        {
            return _commonWeb.GetCollection<Member>($"{Url}/except", new { idRole });
        }

        public Task<IEnumerable<Role>> GetExceptRolesAsync(string member)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}/except", new { member });
        }

        public Task<IEnumerable<Member>> GetExceptMembersAsync(int idRole)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}/except", new { idRole });
        }

        public IEnumerable<Member> GetMembers(int idRole)
        {
            return _commonWeb.GetCollection<Member>($"{Url}", new {idRole});
        }

        public Task<IEnumerable<Member>> GetMembersAsync(int idRole)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}", new { idRole });
        }

        public IEnumerable<Member> GetMembers(string role)
        {
            return _commonWeb.GetCollection<Member>($"{Url}", new {role});
        }

        public Task<IEnumerable<Member>> GetMembersAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}", new { role });
        }

        public IEnumerable<Role> GetRoles(int idMember)
        {
            return _commonWeb.GetCollection<Role>($"{Url}", new { idMember });
        }

        public Task<IEnumerable<Role>> GetRolesAsync(int idMember)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}", new { idMember });
        }

        public IEnumerable<Role> GetRoles(string member)
        {
            return _commonWeb.GetCollection<Role>($"{Url}", new { member });
        }

        public Task<IEnumerable<Role>> GetRolesAsync(string member)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}", new { member });
        }
    }
}
