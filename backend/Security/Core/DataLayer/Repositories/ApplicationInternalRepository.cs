using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Управление приложениями
    /// </summary>
    public class ApplicationInternalRepository : IApplicationInternalRepository
    {
        private readonly ICommonDb _commonDb;

        /// <summary>
        /// Управление приложениями
        /// </summary>
        /// <param name="commonDb"></param>
        public ApplicationInternalRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Application Create(Application entity)
        {
            var id = _commonDb.ExecuteScalar<int>("EXECUTE [sec].[AddApp] @appName ,@description", entity);
            entity.IdApplication = id;

            return entity;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Application> CreateAsync(Application entity)
        {
            var id = await _commonDb.ExecuteScalarAsync<int>("EXECUTE [sec].[AddApp] @appName ,@description", entity);
            entity.IdApplication = id;

            return entity;
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Application Get(object id)
        {
            return _commonDb.QuerySingleOrDefault<Application>("select * from sec.Applications where idApplication = @id", new {id});
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Application> GetAsync(object id)
        {
            return _commonDb.QuerySingleOrDefaultAsync<Application>("select * from sec.Applications where idApplication = @id", new { id });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Application> Get()
        {
            return _commonDb.Query<Application>("select * from sec.Applications");
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Application>> GetAsync()
        {
            return _commonDb.QueryAsync<Application>("select * from sec.Applications");
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Application GetByName(string name)
        {
            return _commonDb.QuerySingleOrDefault<Application>("select * from sec.Applications where appName = @appName", new { appName = name });
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonDb.QuerySingleOrDefaultAsync<Application>("select * from sec.Applications where appName = @appName", new { appName = name });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("execute sec.DeleteApp @idApplication", new {idApplication = id});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("execute sec.DeleteApp @idApplication", new { idApplication = id });
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Application entity)
        {
            _commonDb.ExecuteNonQuery("execute sec.UpdateApp @idApplication, @description", new {entity.IdApplication, entity.Description});
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(Application entity)
        {
            return _commonDb.ExecuteNonQueryAsync("execute sec.UpdateApp @idApplication, @description", new { entity.IdApplication, entity.Description });
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Application CreateEmpty(string prefixForRequired)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Task<Application> CreateEmptyAsync(string prefixForRequired)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        public void Remove(string appName)
        {
            _commonDb.ExecuteNonQuery(@"exec sec.DeleteAppByName @appName", new {appName});
        }

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public Task RemoveAsync(string appName)
        {
            return _commonDb.ExecuteNonQueryAsync(@"exec sec.DeleteAppByName @appName", new { appName });
        }
    }
}
