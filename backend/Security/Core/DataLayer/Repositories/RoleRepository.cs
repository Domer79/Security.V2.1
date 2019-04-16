using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Управление ролями
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        /// <summary>
        /// Управление ролями
        /// </summary>
        public RoleRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role Get(object id)
        {
            return _commonDb.QuerySingleOrDefault<Role>("select * from sec.Roles where idRole = @id and idApplication = @idApplication", new {id, _context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> Get()
        {
            return _commonDb.Query<Role>("select * from sec.Roles where idApplication = @idApplication", new {_context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Role> GetAsync(object id)
        {
            return _commonDb.QuerySingleOrDefaultAsync<Role>("select * from sec.Roles where idRole = @id and idApplication = @idApplication", new { id, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetAsync()
        {
            return _commonDb.QueryAsync<Role>("select * from sec.Roles where idApplication = @idApplication", new { _context.Application.IdApplication });
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Role GetByName(string name)
        {
            return _commonDb.QuerySingle<Role>("select * from sec.Roles where name = @name and idApplication = @idApplication", new {name, _context.Application.IdApplication});
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Role> GetByNameAsync(string name)
        {
            return _commonDb.QuerySingleAsync<Role>("select * from sec.Roles where name = @name and idApplication = @idApplication", new { name, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("delete from sec.Roles where idRole = @id and idApplication = @idApplication", new {id, _context.Application.IdApplication});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.Roles where idRole = @id and idApplication = @idApplication", new { id, _context.Application.IdApplication });
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Role entity)
        {
            _commonDb.ExecuteNonQuery("update sec.Roles set name = @name, description = @description where idRole = @idRole", entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(Role entity)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.Roles set name = @name, description = @description where idRole = @idRole", entity);
        }
    }
}
