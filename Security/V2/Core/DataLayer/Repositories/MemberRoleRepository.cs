using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
{
    public class MemberRoleRepository : IMemberRoleRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        public MemberRoleRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        public void AddMembersToRole(int[] idMembers, int idRole)
        {
            _commonDb.ExecuteNonQuery(@"
insert into MemberRoles(idMember, idRole)
select idMember, @idRole idRole from (select idMember from Members where idMember in @idMembers) s1
", new {idMembers, idRole});
        }

        public void AddMembersToRole(string[] members, string role)
        {
            _commonDb.ExecuteNonQuery(@"
declare @idRole int = (select idRole from Roles where name = @roleName and idApplication = @idApplication)

insert into MemberRoles(idMember, idRole)
select 
	idMember,
	@idRole idRole
from 
	(select idMember from Members where name in @members) s1
", new {roleName = role, _context.Application.IdApplication, members});
        }

        public Task AddMembersToRoleAsync(int[] idMembers, int idRole)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into MemberRoles(idMember, idRole)
select idMember, @idRole idRole from (select idMember from Members where idMember in @idMembers) s1
", new { idMembers, idRole });
        }

        public Task AddMembersToRoleAsync(string[] members, string role)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idRole int = (select idRole from Roles where name = @roleName and idApplication = @idApplication)

insert into MemberRoles(idMember, idRole)
select 
	idMember,
	@idRole idRole
from 
	(select idMember from Members where name in @members) s1
", new { roleName = role, _context.Application.IdApplication, members });
        }

        public void AddRolesToMember(int[] idRoles, int idMember)
        {
            _commonDb.ExecuteNonQuery(@"
insert into MemberRoles(idMember, idRole)
select @idMember, idRole from (select idRole from Roles where idRole in @idRoles) s1
", new { idMember, idRoles });
        }

        public void AddRolesToMember(string[] roles, string member)
        {
            _commonDb.ExecuteNonQuery(@"
declare @idMember int = (select idMember from Members where name = @member)
insert into MemberRoles(idMember, idRole)
select 
	@idMember,
	idRole
from 
	(select idRole from Roles where name in @roles and idApplication = @idApplication) s1
", new { roles, _context.Application.IdApplication, member });
        }

        public Task AddRolesToMemberAsync(int[] idRoles, int idMember)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into MemberRoles(idMember, idRole)
select @idMember, idRole from (select idRole from Roles where idRole in @idRoles) s1
", new { idMember, idRoles });
        }

        public Task AddRolesToMemberAsync(string[] roles, string member)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idMember int = (select idMember from Members where name = @member)
insert into MemberRoles(idMember, idRole)
select 
	@idMember,
	idRole
from 
	(select idRole from Roles where name in @roles and idApplication = @idApplication) s1
", new { roles, _context.Application.IdApplication, member });
        }

        public IEnumerable<Member> GetMembersByIdRole(int idRole)
        {
            return _commonDb.Query<Member>(
                "select m.* from Members m inner join MemberRoles r on m.idMember = r.idMember where r.idRole = @idRole",
                new {idRole});
        }

        public Task<IEnumerable<Member>> GetMembersByIdRoleAsync(int idRole)
        {
            return _commonDb.QueryAsync<Member>(
                "select m.* from Members m inner join MemberRoles r on m.idMember = r.idMember where r.idRole = @idRole",
                new { idRole });
        }

        public IEnumerable<Member> GetMembersByRoleName(string role)
        {
            return _commonDb.Query<Member>(
                @"select * from Members where idMember in (select idMember from MemberRoles where idRole = (select idRole from Roles where name = @role and idApplication = @idApplication))",
                new {role, _context.Application.IdApplication});
        }

        public Task<IEnumerable<Member>> GetMembersByRoleNameAsync(string role)
        {
            return _commonDb.QueryAsync<Member>(
                @"select * from Members where idMember in (select idMember from MemberRoles where idRole = (select idRole from Roles where name = @role and idApplication = @idApplication))",
                new { role, _context.Application.IdApplication });
        }

        public IEnumerable<Role> GetRolesByIdMember(int idMember)
        {
            return _commonDb.Query<Role>(
                "select r.* from Roles r inner join MemberRoles mr on r.idRole = mr.idRole where mr.idMember = @idMember and r.idApplication = @idApplication", new {idMember, _context.Application.IdApplication});
        }

        public Task<IEnumerable<Role>> GetRolesByIdMemberAsync(int idMember)
        {
            return _commonDb.QueryAsync<Role>(
                "select r.* from Roles r inner join MemberRoles mr on r.idRole = mr.idRole where mr.idMember = @idMember and r.idApplication = @idApplication", new { idMember, _context.Application.IdApplication });
        }

        public IEnumerable<Role> GetRolesByMemberName(string member)
        {
            return _commonDb.Query<Role>(
                "select * from Roles where idRole in (select idRole from MemberRoles where idMember = (select idMember from Members where name = @member)) and idApplication = @idApplication",
                new {member, _context.Application.IdApplication});
        }

        public Task<IEnumerable<Role>> GetRolesByMemberNameAsync(string member)
        {
            return _commonDb.QueryAsync<Role>(
                "select * from Roles where idRole in (select idRole from MemberRoles where idMember = (select idMember from Members where name = @member)) and idApplication = @idApplication",
                new { member, _context.Application.IdApplication });
        }
    }
}
