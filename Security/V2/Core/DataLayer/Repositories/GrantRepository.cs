using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
{
    public class GrantRepository : IGrantRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        public GrantRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        public IEnumerable<SecObject> GetExceptRoleGrant(string role)
        {
            return _commonDb.Query<SecObject>("select * from sec.SecObjects where idApplication = @idApplication and idSecObject not in (select g.idSecObject from sec.Grants g inner join sec.Roles r on g.idRole = r.idRole where r.name = @role)", new {role, _context.Application.IdApplication});
        }

        public Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role)
        {
            return _commonDb.QueryAsync<SecObject>("select * from sec.SecObjects where idApplication = @idApplication and idSecObject not in (select g.idSecObject from sec.Grants g inner join sec.Roles r on g.idRole = r.idRole where r.name = @role)", new { role, _context.Application.IdApplication });
        }

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

        public void RemoveGrant(string role, string secObject)
        {
            _commonDb.ExecuteNonQuery(@"
delete from sec.Grants where idSecObject = (select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idApplication)
and idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
", new {objectName = secObject, roleName = role, _context.Application.IdApplication });
        }

        public Task RemoveGrantAsync(string role, string secObject)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
delete from sec.Grants where idSecObject = (select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idApplication)
and idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
", new { objectName = secObject, roleName = role, _context.Application.IdApplication });
        }

        public void RemoveGrants(string role, string[] secObjects)
        {
            _commonDb.ExecuteNonQuery(@"
delete from sec.Grants where idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
and idSecObject in (select idSecObject from sec.SecObjects where idApplication = @idApplication and ObjectName in @objectNames)
", new {roleName = role, objectNames = secObjects, _context.Application.IdApplication});
        }

        public Task RemoveGrantsAsync(string role, string[] secObjects)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
delete from sec.Grants where idRole = (select idRole from sec.Roles where name = @roleName and idApplication = @idApplication)
and idSecObject in (select idSecObject from sec.SecObjects where idApplication = @idApplication and ObjectName in @objectNames)
", new { roleName = role, objectNames = secObjects, _context.Application.IdApplication });
        }

        public void SetGrant(string role, string secObject)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.Grants(idRole, idSecObject)
select
	(select idRole from sec.Roles where name = @roleName and idApplication = @idApplication) as idRole,
	(select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idapplication) as idSecObject
", new {objectName = secObject, roleName = role, idApplication = _context.Application.IdApplication});
        }

        public Task SetGrantAsync(string role, string secObject)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into sec.Grants(idRole, idSecObject)
select
	(select idRole from sec.Roles where name = @roleName and idApplication = @idApplication) as idRole,
	(select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idapplication) as idSecObject
", new { objectName = secObject, roleName = role, idApplication = _context.Application.IdApplication });
        }

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