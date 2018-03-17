using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        public RoleRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        public Role Create(Role entity)
        {
            entity.IdApplication = _context.Application.IdApplication;
            var id = _commonDb.ExecuteScalar<int>(@"
insert into sec.Roles(name, description, idApplication) values(@name, @description, @idApplication)
select SCOPE_IDENTITY()
", entity);

            entity.IdRole = id;
            return entity;
        }

        public async Task<Role> CreateAsync(Role entity)
        {
            entity.IdApplication = _context.Application.IdApplication;
            var id = _commonDb.ExecuteScalarAsync<int>(@"
insert into sec.Roles(name, description, idApplication) values(@name, @description, @idApplication)
select SCOPE_IDENTITY()
", entity);

            entity.IdRole = await id;
            return entity;
        }

        public Role CreateEmpty(string prefixForRequired)
        {
            var idRole = _commonDb.ExecuteScalar<int>(@"
declare @ident int = IDENT_CURRENT('sec.Roles')
declare @name nvarchar(200) = concat(@prefix, @ident)

insert into sec.Roles(name, idApplication) values(@name, @idApplication)
select SCOPE_IDENTITY()
", new {prefix = prefixForRequired, idApplication = _context.Application.IdApplication});

            return Get(idRole);
        }

        public async Task<Role> CreateEmptyAsync(string prefixForRequired)
        {
            var idRole = await _commonDb.ExecuteScalarAsync<int>(@"
declare @ident int = IDENT_CURRENT('sec.Roles')
declare @name nvarchar(200) = concat(@prefix, @ident)

insert into sec.Roles(name, idApplication) values(@name, @idApplication)
select SCOPE_IDENTITY()
", new { prefix = prefixForRequired, idApplication = _context.Application.IdApplication }).ConfigureAwait(false);

            return await GetAsync(idRole);
        }

        public Role Get(object id)
        {
            return _commonDb.QuerySingleOrDefault<Role>("select * from sec.Roles where idRole = @id and idApplication = @idApplication", new {id, _context.Application.IdApplication});
        }

        public IEnumerable<Role> Get()
        {
            return _commonDb.Query<Role>("select * from sec.Roles where idApplication = @idApplication", new {_context.Application.IdApplication});
        }

        public Task<Role> GetAsync(object id)
        {
            return _commonDb.QuerySingleOrDefaultAsync<Role>("select * from sec.Roles where idRole = @id and idApplication = @idApplication", new { id, _context.Application.IdApplication });
        }

        public Task<IEnumerable<Role>> GetAsync()
        {
            return _commonDb.QueryAsync<Role>("select * from sec.Roles where idApplication = @idApplication", new { _context.Application.IdApplication });
        }

        public Role GetByName(string name)
        {
            return _commonDb.QuerySingle<Role>("select * from sec.Roles where name = @name and idApplication = @idApplication", new {name, _context.Application.IdApplication});
        }

        public Task<Role> GetByNameAsync(string name)
        {
            return _commonDb.QuerySingleAsync<Role>("select * from sec.Roles where name = @name and idApplication = @idApplication", new { name, _context.Application.IdApplication });
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("delete from sec.Roles where idRole = @id and idApplication = @idApplication", new {id, _context.Application.IdApplication});
        }

        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.Roles where idRole = @id and idApplication = @idApplication", new { id, _context.Application.IdApplication });
        }

        public void Update(Role entity)
        {
            _commonDb.ExecuteNonQuery("update sec.Roles set name = @name, description = @description where idRole = @idRole", entity);
        }

        public Task UpdateAsync(Role entity)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.Roles set name = @name, description = @description where idRole = @idRole", entity);
        }
    }
}
