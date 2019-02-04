using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Управление разрешениями
    /// </summary>
    public class GrantRepository : IGrantRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        /// <summary>
        /// Управление разрешениями
        /// </summary>
        public GrantRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        /// <summary>
        /// Получение отсутствующих у роли политик безопасности
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<SecObject> GetExceptRoleGrant(string role)
        {
            return _commonDb.Query<SecObject>("select * from sec.SecObjects where idApplication = @idApplication and idSecObject not in (select g.idSecObject from sec.Grants g inner join sec.Roles r on g.idRole = r.idRole where r.name = @role)", new {role, _context.Application.IdApplication});
        }

        /// <summary>
        /// Получение отсутствующих у роли политик безопасности
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role)
        {
            return _commonDb.QueryAsync<SecObject>("select * from sec.SecObjects where idApplication = @idApplication and idSecObject not in (select g.idSecObject from sec.Grants g inner join sec.Roles r on g.idRole = r.idRole where r.name = @role)", new { role, _context.Application.IdApplication });
        }

        /// <summary>
        /// Получение политик безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<SecObject> GetRoleGrants(string role)
        {
            return _commonDb.Query<SecObject>(@"
select 
	s.* 
from 
	sec.SecObjects s left join sec.Grants g on s.idSecObject = g.idSecObject
	left join sec.Roles r on g.idRole = r.idRole
where
	r.name = @roleName and r.idApplication = @idApplication
", new {roleName = role, idApplication = _context.Application.IdApplication});
        }

        /// <summary>
        /// Получение политик безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<SecObject>> GetRoleGrantsAsync(string role)
        {
            return _commonDb.QueryAsync<SecObject>(@"
select 
	s.* 
from 
	sec.SecObjects s left join sec.Grants g on s.idSecObject = g.idSecObject
	left join sec.Roles r on g.idRole = r.idRole
where
	r.name = @roleName and r.idApplication = @idApplication
", new { roleName = role, idApplication = _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаление разрешения из роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        public void RemoveGrant(string role, string secObject)
        {
            _commonDb.ExecuteNonQuery(@"
delete from sec.Grants where idSecObject = (select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idApplication)
and idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
", new {objectName = secObject, roleName = role, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаление разрешения из роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public Task RemoveGrantAsync(string role, string secObject)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
delete from sec.Grants where idSecObject = (select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idApplication)
and idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
", new { objectName = secObject, roleName = role, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаление нескольких разрешений для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObjects">Политики безопасности</param>
        public void RemoveGrants(string role, string[] secObjects)
        {
            _commonDb.ExecuteNonQuery(@"
delete from sec.Grants where idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
and idSecObject in (select idSecObject from sec.SecObjects where idApplication = @idApplication and ObjectName in @objectNames)
", new {roleName = role, objectNames = secObjects, _context.Application.IdApplication});
        }

        /// <summary>
        /// Удаление нескольких разрешений для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        public Task RemoveGrantsAsync(string role, string[] secObjects)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
delete from sec.Grants where idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
and idSecObject in (select idSecObject from sec.SecObjects where idApplication = @idApplication and ObjectName in @objectNames)
", new { roleName = role, objectNames = secObjects, _context.Application.IdApplication });
        }

        /// <summary>
        /// Установка разрешения для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObject">Политика безопасности</param>
        public void SetGrant(string role, string secObject)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.Grants(idRole, idSecObject)
select
	(select idRole from sec.Roles where name = @roleName and idApplication = @idApplication) as idRole,
	(select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idapplication) as idSecObject
", new {objectName = secObject, roleName = role, idApplication = _context.Application.IdApplication});
        }

        /// <summary>
        /// Установка разрешения для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public Task SetGrantAsync(string role, string secObject)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into sec.Grants(idRole, idSecObject)
select
	(select idRole from sec.Roles where name = @roleName and idApplication = @idApplication) as idRole,
	(select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idapplication) as idSecObject
", new { objectName = secObject, roleName = role, idApplication = _context.Application.IdApplication });
        }

        /// <summary>
        /// Установка нескольких разрешений для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObjects">Политики безопасности</param>
        public void SetGrants(string role, string[] secObjects)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.Grants(idRole, idSecObject)
select
	r.idRole,
	s.idSecObject
from
	(select idSecObject, idApplication from sec.SecObjects where idApplication = @idApplication and ObjectName in @objectNames) s left join (select idRole, idApplication from sec.Roles where idApplication = @idApplication and name = @roleName)r on s.idApplication = r.idApplication
", new { objectNames = secObjects, roleName = role, idApplication = _context.Application.IdApplication });
        }

        /// <summary>
        /// Установка нескольких разрешений для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        public Task SetGrantsAsync(string role, string[] secObjects)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into sec.Grants(idRole, idSecObject)
select
	r.idRole,
	s.idSecObject
from
	(select idSecObject, idApplication from sec.SecObjects where idApplication = @idApplication and ObjectName in @objectNames) s left join (select idRole, idApplication from sec.Roles where idApplication = @idApplication and name = @roleName)r on s.idApplication = r.idApplication
", new { objectNames = secObjects, roleName = role, idApplication = _context.Application.IdApplication });
        }
    }
}