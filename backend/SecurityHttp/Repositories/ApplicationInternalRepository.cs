using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление приложениями
    /// </summary>
    public class ApplicationInternalRepository : IApplicationInternalRepository
    {
        private readonly ICommonWeb _commonWeb;

        /// <summary>
        /// Управление приложениями
        /// </summary>
        /// <param name="commonWeb"></param>
        public ApplicationInternalRepository(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Application Create(Application entity)
        {
            var app = _commonWeb.PostAndGet<Application>("api/applications", entity);
            return app;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Application> CreateAsync(Application entity)
        {
            var app = await _commonWeb.PostAndGetAsync<Application>("api/applications", entity).ConfigureAwait(false);
            return app;
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
            return _commonWeb.Get<Application>($"api/applications", new{id});
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Application> GetAsync(object id)
        {
            return _commonWeb.GetAsync<Application>($"api/applications", new { id });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Application> Get()
        {
            return _commonWeb.GetCollection<Application>($"api/applications");
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Application>> GetAsync()
        {
            return await _commonWeb.GetCollectionAsync<Application>($"api/applications").ConfigureAwait(false);
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Application GetByName(string name)
        {
            return _commonWeb.Get<Application>($"api/applications", new {name});
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonWeb.GetAsync<Application>($"api/applications", new { name });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonWeb.Delete($"api/applications", new {id});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(object id)
        {
            await _commonWeb.DeleteAsync($"api/applications", new { id }).ConfigureAwait(false);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Application entity)
        {
            _commonWeb.Put("api/applications", entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Application entity)
        {
            await _commonWeb.PutAsync("api/applications", entity);
        }

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        public void Remove(string appName)
        {
            _commonWeb.Delete($"api/applications", new {appName});
        }

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public Task RemoveAsync(string appName)
        {
            return _commonWeb.DeleteAsync($"api/applications", new { appName });
        }
    }
}
