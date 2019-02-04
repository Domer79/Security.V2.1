using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Ограниченное управление приложениями
    /// </summary>
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ICommonDb _commonDb;

        /// <summary>
        /// Ограниченное управление приложениями
        /// </summary>
        public ApplicationRepository(ICommonDb commonDb)
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
            throw new NotSupportedException();
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<Application> CreateAsync(Application entity)
        {
            return Task.FromException<Application>(new NotSupportedException());
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
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Application Get(object id)
        {
            return _commonDb.QuerySingle<Application>("select * from sec.Applications where idApplication = @id", new { id });
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
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Application> GetAsync(object id)
        {
            return _commonDb.QuerySingleAsync<Application>("select * from sec.Applications where idApplication = @id", new { id });
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
            return _commonDb.QuerySingle<Application>("select * from sec.Applications where appName = @name", new {name});
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonDb.QuerySingleAsync<Application>("select * from sec.Applications where appName = @name", new { name });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Application entity)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(Application entity)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }
    }
}
