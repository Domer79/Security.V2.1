﻿using System.Collections.Generic;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.DataLayer.Repositories
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

        public void RemoveGrant(string role, string secObject)
        {
            _commonDb.ExecuteNonQuery(@"
delete from sec.Grants where idSecObject = (select idSecObject from sec.SecObjects where ObjectName = @objectName)
and idRole = (select idRole from sec.Roles where name = @roleName)
", new {objectName = secObject, roleName = role});
        }

        public void RemoveGrants(string role, string[] secObjects)
        {
            _commonDb.ExecuteNonQuery(@"
delete from sec.Grants where idRole = (select idRole from sec.Roles where name = @roleName)
and idSecObject in (select idSecObject from sec.SecObjects where ObjectName in @objectNames)
", new {roleName = role, objectNames = secObjects});
        }

        public void SetGrant(string role, string secObject)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.Grants(idRole, idSecObject)
select
	(select idSecObject from sec.SecObjects where ObjectName = @objectName and idApplication = @idapplication) as idSecObject,
	(select idRole from sec.Roles where name = @roleName and idApplication = @idApplication) as idRole
", new {objectName = secObject, roleName = role, idApplication = _context.Application.IdApplication});
        }

        public void SetGrants(string role, string[] secObjects)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.Grants(idRole, idSecObject)
select
	s.idSecObject,
	r.idRole
from
	(select idSecObject, idApplication from sec.SecObjects where idApplication = @idApplication and ObjectName in @objectNames) s left join (select idRole, idApplication from sec.Roles where idApplication = @idApplication and name = @roleName)r on s.idApplication = r.idApplication
", new { objectNames = secObjects, roleName = role, idApplication = _context.Application.IdApplication });
        }
    }
}