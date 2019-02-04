using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Управление политиками безопасности
    /// </summary>
    public class SecObjectRepository : ISecObjectRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        /// <summary>
        /// Управление политиками безопасности
        /// </summary>
        public SecObjectRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SecObject Create(SecObject entity)
        {
            entity.IdApplication = _context.Application.IdApplication;
            var id = _commonDb.ExecuteScalar<int>(@"
insert into sec.SecObjects(ObjectName, idApplication) values(@objectName, @idApplication)
select SCOPE_IDENTITY()
", entity);
            entity.IdSecObject = id;
            return entity;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<SecObject> CreateAsync(SecObject entity)
        {
            entity.IdApplication = _context.Application.IdApplication;
            var id = _commonDb.ExecuteScalarAsync<int>(@"
insert into sec.SecObjects(ObjectName, idApplication) values(@objectName, @idApplication)
select SCOPE_IDENTITY()
", entity);
            entity.IdSecObject = await id;
            return entity;
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public SecObject CreateEmpty(string prefixForRequired)
        {
            var idRole = _commonDb.ExecuteScalar<int>(@"
declare @ident int = IDENT_CURRENT('sec.SecObject')
declare @name nvarchar(200) = concat(@prefix, @ident)

insert into sec.SecObjects(objectName, idApplication) values(@name, @idApplication)
select SCOPE_IDENTITY()
", new { prefix = prefixForRequired, idApplication = _context.Application.IdApplication });

            return Get(idRole);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public async Task<SecObject> CreateEmptyAsync(string prefixForRequired)
        {
            var idRole = await _commonDb.ExecuteScalarAsync<int>(@"
declare @ident int = IDENT_CURRENT('sec.SecObjects')
declare @name nvarchar(200) = concat(@prefix, @ident)

insert into sec.SecObjects(objectName, idApplication) values(@name, @idApplication)
select SCOPE_IDENTITY()
", new { prefix = prefixForRequired, idApplication = _context.Application.IdApplication }).ConfigureAwait(false);

            return await GetAsync(idRole);
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SecObject Get(object id)
        {
            return _commonDb.QuerySingleOrDefault<SecObject>("select * from sec.SecObjects where idApplication = @idApplication and idSecObject = @id", new {id, _context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SecObject> Get()
        {
            return _commonDb.Query<SecObject>("select * from sec.SecObjects where idApplication = @idApplication", new {_context.Application.IdApplication});
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SecObject> GetAsync(object id)
        {
            return _commonDb.QuerySingleOrDefaultAsync<SecObject>("select * from sec.SecObjects where idApplication = @idApplication and idSecObject = @id", new { id, _context.Application.IdApplication });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SecObject>> GetAsync()
        {
            return _commonDb.QueryAsync<SecObject>("select * from sec.SecObjects where idApplication = @idApplication", new { _context.Application.IdApplication });
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SecObject GetByName(string name)
        {
            return _commonDb.QuerySingleOrDefault<SecObject>("select * from sec.SecObjects where objectName = @name and idApplication = @idApplication", new { name, _context.Application.IdApplication });
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<SecObject> GetByNameAsync(string name)
        {
            return _commonDb.QuerySingleOrDefaultAsync<SecObject>("select * from sec.SecObjects where objectName = @name and idApplication = @idApplication", new { name, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("delete from sec.SecObjects where idApplication = @idApplication and idSecObject = @id", new { id, _context.Application.IdApplication });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.SecObjects where idApplication = @idApplication and idSecObject = @id", new { id, _context.Application.IdApplication });
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(SecObject entity)
        {
            _commonDb.ExecuteNonQuery("update sec.SecObjects set objectName = @objectName where idSecObject = @idSecObject", entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(SecObject entity)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.SecObjects set objectName = @objectName where idSecObject = @idSecObject", entity);
        }
    }
}
