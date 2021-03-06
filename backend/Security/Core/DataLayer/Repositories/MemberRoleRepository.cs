﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Управление участниками безопасности (пользователи и группы) и ролей
    /// </summary>
    public class MemberRoleRepository : IMemberRoleRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        /// <summary>
        /// Управление участниками безопасности (пользователи и группы) и ролей
        /// </summary>
        public MemberRoleRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetExceptMembers(string role)
        {
            return _commonDb.Query<Member>("select * from sec.Members where idMember not in (select idMember from sec.MemberRoles where idRole = (select idRole from sec.Roles where name = @role and idApplication = @idApplication)) and 1 = (select 1 from sec.Roles where name = @role)", new{role, _context.Application.IdApplication});
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        public void AddMembersToRole(int[] idMembers, int idRole)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.MemberRoles(idMember, idRole)
select idMember, @idRole idRole from (select idMember from sec.Members where idMember in @idMembers) s1
", new {idMembers, idRole});
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        public void AddMembersToRole(string[] members, string role)
        {
            _commonDb.ExecuteNonQuery(@"
declare @idRole int = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)

insert into sec.MemberRoles(idMember, idRole)
select 
	idMember,
	@idRole idRole
from 
	(select idMember from sec.Members where name in @members) s1
", new {roleName = role, _context.Application.IdApplication, members});
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetExceptMembersAsync(string role)
        {
            return _commonDb.QueryAsync<Member>("select * from sec.Members where idMember not in (select idMember from sec.MemberRoles where idRole = (select idRole from sec.Roles where name = @role and idApplication = @idApplication)) and 1 = (select 1 from sec.Roles where name = @role)", new{role, _context.Application.IdApplication });
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task AddMembersToRoleAsync(int[] idMembers, int idRole)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into sec.MemberRoles(idMember, idRole)
select idMember, @idRole idRole from (select idMember from sec.Members where idMember in @idMembers) s1
", new { idMembers, idRole });
        }

        /// <summary>
        /// Добавляет роль нескольким участникам
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task AddMembersToRoleAsync(string[] members, string role)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idRole int = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)

insert into sec.MemberRoles(idMember, idRole)
select 
	idMember,
	@idRole idRole
from 
	(select idMember from sec.Members where name in @members) s1
", new { roleName = role, _context.Application.IdApplication, members });
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        public void AddRolesToMember(int[] idRoles, int idMember)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.MemberRoles(idMember, idRole)
select @idMember, idRole from (select idRole from sec.Roles where idRole in @idRoles) s1
", new { idMember, idRoles });
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        public void AddRolesToMember(string[] roles, string member)
        {
            _commonDb.ExecuteNonQuery(@"
declare @idMember int = (select idMember from sec.Members where name = @member)
insert into sec.MemberRoles(idMember, idRole)
select 
	@idMember,
	idRole
from 
	(select idRole from sec.Roles where name in @roles and idApplication = @idApplication) s1
", new { roles, _context.Application.IdApplication, member });
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task AddRolesToMemberAsync(int[] idRoles, int idMember)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into sec.MemberRoles(idMember, idRole)
select @idMember, idRole from (select idRole from sec.Roles where idRole in @idRoles) s1
", new { idMember, idRoles });
        }

        /// <summary>
        /// Добавляет несколько ролей участнику
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task AddRolesToMemberAsync(string[] roles, string member)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idMember int = (select idMember from sec.Members where name = @member)
insert into sec.MemberRoles(idMember, idRole)
select 
	@idMember,
	idRole
from 
	(select idRole from sec.Roles where name in @roles and idApplication = @idApplication) s1
", new { roles, _context.Application.IdApplication, member });
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        public void DeleteMembersFromRole(int[] idMembers, int idRole)
        {
            _commonDb.ExecuteNonQuery("delete from sec.MemberRoles where idRole = @idRole and idMember in @idMembers", new {idMembers, idRole});
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="idMembers"></param>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task DeleteMembersFromRoleAsync(int[] idMembers, int idRole)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.MemberRoles where idRole = @idRole and idMember in @idMembers", new { idMembers, idRole });
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        public void DeleteRolesFromMember(int[] idRoles, int idMember)
        {
            _commonDb.ExecuteNonQuery("delete from sec.MemberRoles where idMember = @idMember and idRole in @idRoles", new { idMember, idRoles });
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        public void DeleteMembersFromRole(string[] members, string role)
        {
            _commonDb.ExecuteNonQuery("delete from sec.MemberRoles where idRole = (select idRole from sec.Roles where name = @role and idApplication = @idApplication) and idMember in (select idMember from sec.Members where name in @members)", new { members, role, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        public void DeleteRolesFromMember(string[] roles, string member)
        {
            _commonDb.ExecuteNonQuery("delete from sec.MemberRoles where idMember = (select idMember from sec.Members where name = @member) and idRole in (select idRole from sec.Roles where name in @roles and idApplication = @idApplication)", new { roles, member, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="idRoles"></param>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task DeleteRolesFromMemberAsync(int[] idRoles, int idMember)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.MemberRoles where idMember = @idMember and idRole in @idRoles", new { idMember, idRoles });
        }

        /// <summary>
        /// Удаляет роль у участников
        /// </summary>
        /// <param name="members"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task DeleteMembersFromRoleAsync(string[] members, string role)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.MemberRoles where idRole = (select idRole from sec.Roles where name = @role and idApplication = @idApplication) and idMember in (select idMember from sec.Members where name in @members)", new { members, role, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаляет несколько ролей у участника
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task DeleteRolesFromMemberAsync(string[] roles, string member)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.MemberRoles where idMember = (select idMember from sec.Members where name = @member) and idRole in (select idRole from sec.Roles where name in @roles and idApplication = @idApplication)", new { roles, member, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetExceptRoles(int idMember)
        {
            return _commonDb.Query<Role>("select * from sec.Roles where idRole not in (select idRole from sec.MemberRoles where idMember = @idMember) and idApplication = @idApplication and 1 = (select 1 from sec.Members where idMember = @idMember)", new {idMember, _context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetExceptRolesAsync(int idMember)
        {
            return _commonDb.QueryAsync<Role>("select * from sec.Roles where idRole not in (select idRole from sec.MemberRoles where idMember = @idMember) and idApplication = @idApplication and 1 = (select 1 from sec.Members where idMember = @idMember)", new { idMember, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetExceptRoles(string member)
        {
            return _commonDb.Query<Role>("select * from sec.Roles where idRole not in (select idRole from sec.MemberRoles where idMember = (select idMember from sec.Members where name = @member)) and idApplication = @idApplication and 1 = (select 1 from sec.Members where name = @member)", new {member, _context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetExceptMembers(int idRole)
        {
            return _commonDb.Query<Member>("select * from sec.Members where idMember not in (select idMember from sec.MemberRoles where idRole = @idRole) and 1 = (select 1 from sec.Roles where idRole = @idRole and idApplication = @idApplication)", new {idRole, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает отсутствующие у участника роли
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetExceptRolesAsync(string member)
        {
            return _commonDb.QueryAsync<Role>("select * from sec.Roles where idRole not in (select idRole from sec.MemberRoles where idMember = (select idMember from sec.Members where name = @member)) and idApplication = @idApplication and 1 = (select 1 from sec.Members where name = @member)", new { member, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает отсутствующих у роли участников
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetExceptMembersAsync(int idRole)
        {
            return _commonDb.QueryAsync<Member>("select * from sec.Members where idMember not in (select idMember from sec.MemberRoles where idRole = @idRole) and 1 = (select 1 from sec.Roles where idRole = @idRole and idApplication = @idApplication)", new { idRole, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetMembers(int idRole)
        {
            return _commonDb.Query<Member>(
                "select m.* from sec.Members m inner join sec.MemberRoles r on m.idMember = r.idMember where r.idRole = @idRole",
                new {idRole});
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetMembersAsync(int idRole)
        {
            return _commonDb.QueryAsync<Member>(
                "select m.* from sec.Members m inner join sec.MemberRoles r on m.idMember = r.idMember where r.idRole = @idRole",
                new { idRole });
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetMembers(string role)
        {
            return _commonDb.Query<Member>(
                @"select * from sec.Members where idMember in (select idMember from sec.MemberRoles where idRole = (select idRole from sec.Roles where name = @role and idApplication = @idApplication))",
                new {role, _context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает участников роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<Member>> GetMembersAsync(string role)
        {
            return _commonDb.QueryAsync<Member>(
                @"select * from sec.Members where idMember in (select idMember from sec.MemberRoles where idRole = (select idRole from sec.Roles where name = @role and idApplication = @idApplication))",
                new { role, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles(int idMember)
        {
            return _commonDb.Query<Role>(
                "select r.* from sec.Roles r inner join sec.MemberRoles mr on r.idRole = mr.idRole where mr.idMember = @idMember and r.idApplication = @idApplication", new {idMember, _context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetRolesAsync(int idMember)
        {
            return _commonDb.QueryAsync<Role>(
                "select r.* from sec.Roles r inner join sec.MemberRoles mr on r.idRole = mr.idRole where mr.idMember = @idMember and r.idApplication = @idApplication", new { idMember, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles(string member)
        {
            return _commonDb.Query<Role>(
                "select * from sec.Roles where idRole in (select idRole from sec.MemberRoles where idMember = (select idMember from sec.Members where name = @member)) and idApplication = @idApplication",
                new {member, _context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает роли участника безопасности
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetRolesAsync(string member)
        {
            return _commonDb.QueryAsync<Role>(
                "select * from sec.Roles where idRole in (select idRole from sec.MemberRoles where idMember = (select idMember from sec.Members where name = @member)) and idApplication = @idApplication",
                new { member, _context.Application.IdApplication });
        }
    }
}
