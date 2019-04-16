using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Управление группами
    /// </summary>
    public class GroupRepository : IGroupRepository
    {
        private readonly ICommonDb _commonDb;

        /// <summary>
        /// Управление группами
        /// </summary>
        public GroupRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Group Create(Group entity)
        {
            entity.Id = Guid.NewGuid();
            var id = _commonDb.ExecuteScalar<int>("execute sec.AddGroup @id, @name, @description", entity);
            entity.IdMember = id;
            return entity;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Group> CreateAsync(Group entity)
        {
            entity.Id = Guid.NewGuid();
            var id = _commonDb.ExecuteScalarAsync<int>("execute sec.AddGroup @id, @name, @description", entity);
            entity.IdMember = await id;
            return entity;
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Group CreateEmpty(string prefixForRequired)
        {
            var idMember = _commonDb.ExecuteScalar<int>("exec sec.AddEmptyGroup @id, @prefix", new {id = Guid.NewGuid(), prefix = prefixForRequired});

            return Get(idMember);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public async Task<Group> CreateEmptyAsync(string prefixForRequired)
        {
            var idMember = await _commonDb.ExecuteScalarAsync<int>("exec sec.AddEmptyGroup @id, @prefix", new { id = Guid.NewGuid(), prefix = prefixForRequired }).ConfigureAwait(false);

            return await GetAsync(idMember);
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Group Get(object id)
        {
            return _commonDb.QuerySingleOrDefault<Group>("select * from sec.GroupsView where idMember = @id", new {id});
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Group> Get()
        {
            return _commonDb.Query<Group>("select * from sec.GroupsView");
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Group> GetAsync(object id)
        {
            return _commonDb.QuerySingleOrDefaultAsync<Group>("select * from sec.GroupsView where idMember = @id", new { id });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Group>> GetAsync()
        {
            return _commonDb.QueryAsync<Group>("select * from sec.GroupsView");
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Group GetByName(string name)
        {
            return _commonDb.QuerySingle<Group>("select * from sec.GroupsView where name = @name", new { name });
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Group> GetByNameAsync(string name)
        {
            return _commonDb.QuerySingleAsync<Group>("select * from sec.GroupsView where name = @name", new { name });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("execute sec.DeleteGroup @idMember", new {idMember = id});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("execute sec.DeleteGroup @idMember", new { idMember = id });
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Group entity)
        {
            _commonDb.ExecuteNonQuery("execute sec.UpdateGroup @id, @idMember, @name, @description", entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(Group entity)
        {
            return _commonDb.ExecuteNonQueryAsync("execute sec.UpdateGroup @id, @idMember, @name, @description", entity);
        }
    }
}
