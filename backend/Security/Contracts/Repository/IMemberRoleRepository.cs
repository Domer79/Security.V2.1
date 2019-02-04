using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Управление участниками безопасности (пользователи и группы) и ролей
    /// </summary>
    public interface IMemberRoleRepository
    {
        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        IEnumerable<Member> GetMembers(int idRole);

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IEnumerable<Member> GetMembers(string role);

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        IEnumerable<Role> GetRoles(int idMember);

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IEnumerable<Role> GetRoles(string member);

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        IEnumerable<Role> GetExceptRoles(int idMember);

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IEnumerable<Role> GetExceptRoles(string member);

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        IEnumerable<Member> GetExceptMembers(int idRole);

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IEnumerable<Member> GetExceptMembers(string role);

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        void AddMembersToRole(int[] idMembers, int idRole);

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        void AddMembersToRole(string[] members, string role);

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        void AddRolesToMember(int[] idRoles, int idMember);

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        void AddRolesToMember(string[] roles, string member);

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        void DeleteMembersFromRole(int[] idMembers, int idRole);

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        void DeleteMembersFromRole(string[] members, string role);

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        void DeleteRolesFromMember(int[] idRoles, int idMember);

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        void DeleteRolesFromMember(string[] roles, string member);

        #region Async

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        Task<IEnumerable<Member>> GetMembersAsync(int idRole);

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<Member>> GetMembersAsync(string role);

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetRolesAsync(int idMember);

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetRolesAsync(string member);

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetExceptRolesAsync(int idMember);

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetExceptRolesAsync(string member);

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        Task<IEnumerable<Member>> GetExceptMembersAsync(int idRole);

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<Member>> GetExceptMembersAsync(string role);

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        /// <returns></returns>
        Task AddMembersToRoleAsync(int[] idMembers, int idRole);

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task AddMembersToRoleAsync(string[] members, string role);

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        /// <returns></returns>
        Task AddRolesToMemberAsync(int[] idRoles, int idMember);

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task AddRolesToMemberAsync(string[] roles, string member);

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        /// <returns></returns>
        Task DeleteMembersFromRoleAsync(int[] idMembers, int idRole);

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task DeleteMembersFromRoleAsync(string[] members, string role);

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task DeleteRolesFromMemberAsync(string[] roles, string member);

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        /// <returns></returns>
        Task DeleteRolesFromMemberAsync(int[] idRoles, int idMember);

        #endregion
    }
}