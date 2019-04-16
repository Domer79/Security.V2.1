using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление участниками безопасности (пользователи и группы) и ролей
    /// </summary>
    public class MemberRoleRepository : IMemberRoleRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private string _url;

        /// <summary>
        /// Управление участниками безопасности (пользователи и группы) и ролей
        /// </summary>
        public MemberRoleRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        protected string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/memberroles"); }
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetExceptMembers(string role)
        {
            return _commonWeb.GetCollection<Member>($"{Url}/except", new {role});
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        public void AddMembersToRole(int[] idMembers, int idRole)
        {
            _commonWeb.Put(Url, idMembers, new {idRole});
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        public void AddMembersToRole(string[] members, string role)
        {
            _commonWeb.Put(Url, members, new { role });
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetExceptMembersAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}/except", new { role });
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task AddMembersToRoleAsync(int[] idMembers, int idRole)
        {
            return _commonWeb.PutAsync(Url, idMembers, new { idRole });
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task AddMembersToRoleAsync(string[] members, string role)
        {
            return _commonWeb.PutAsync(Url, members, new { role });
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        public void AddRolesToMember(int[] idRoles, int idMember)
        {
            _commonWeb.Put(Url, idRoles, new {idMember});
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        public void AddRolesToMember(string[] roles, string member)
        {
            _commonWeb.Put(Url, roles, new {member});
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task AddRolesToMemberAsync(int[] idRoles, int idMember)
        {
            return _commonWeb.PutAsync(Url, idRoles, new {idMember});
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task AddRolesToMemberAsync(string[] roles, string member)
        {
            return _commonWeb.PutAsync(Url, roles, new {member});
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        public void DeleteMembersFromRole(int[] idMembers, int idRole)
        {
            _commonWeb.Delete(Url, idMembers, new {idRole});
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task DeleteMembersFromRoleAsync(int[] idMembers, int idRole)
        {
            return _commonWeb.DeleteAsync(Url, idMembers, new { idRole });
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        public void DeleteRolesFromMember(int[] idRoles, int idMember)
        {
            _commonWeb.Delete(Url, idRoles, new { idMember });
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        public void DeleteMembersFromRole(string[] members, string role)
        {
            _commonWeb.Delete(Url, members, new {role});
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        public void DeleteRolesFromMember(string[] roles, string member)
        {
            _commonWeb.Delete(Url, roles, new {member});
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task DeleteRolesFromMemberAsync(int[] idRoles, int idMember)
        {
            return _commonWeb.DeleteAsync(Url, idRoles, new { idMember });
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task DeleteMembersFromRoleAsync(string[] members, string role)
        {
            return _commonWeb.DeleteAsync(Url, members, new { role });
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task DeleteRolesFromMemberAsync(string[] roles, string member)
        {
            return _commonWeb.DeleteAsync(Url, roles, new { member });
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetExceptRoles(int idMember)
        {
            return _commonWeb.GetCollection<Role>($"{Url}/except", new {idMember});
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetExceptRolesAsync(int idMember)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}/except", new { idMember });
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetExceptRoles(string member)
        {
            return _commonWeb.GetCollection<Role>($"{Url}/except", new { member });
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetExceptMembers(int idRole)
        {
            return _commonWeb.GetCollection<Member>($"{Url}/except", new { idRole });
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetExceptRolesAsync(string member)
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}/except", new { member });
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetExceptMembersAsync(int idRole)
        {
            return _commonWeb.GetCollectionAsync<Member>($"{Url}/except", new { idRole });
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetMembers(int idRole)
        {
            return _commonWeb.GetCollection<Member>(Url, new {idRole});
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetMembersAsync(int idRole)
        {
            return _commonWeb.GetCollectionAsync<Member>(Url, new { idRole });
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetMembers(string role)
        {
            return _commonWeb.GetCollection<Member>(Url, new {role});
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetMembersAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<Member>(Url, new { role });
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles(int idMember)
        {
            return _commonWeb.GetCollection<Role>(Url, new { idMember });
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetRolesAsync(int idMember)
        {
            return _commonWeb.GetCollectionAsync<Role>(Url, new { idMember });
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles(string member)
        {
            return _commonWeb.GetCollection<Role>(Url, new { member });
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetRolesAsync(string member)
        {
            return _commonWeb.GetCollectionAsync<Role>(Url, new { member });
        }
    }
}
