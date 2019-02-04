using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts.Repository;
using Security.Model;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Ограниченное управление приложениями
    /// </summary>
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IApplicationInternalRepository _repo;

        /// <summary>
        /// Ограниченное управление приложениями
        /// </summary>
        public ApplicationRepository(IApplicationInternalRepository repo)
        {
            _repo = repo;
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
            return _repo.Get(id);
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Application> Get()
        {
            return _repo.Get();
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Application> GetAsync(object id)
        {
            return _repo.GetAsync(id);
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Application>> GetAsync()
        {
            return _repo.GetAsync();
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Application GetByName(string name)
        {
            return _repo.GetByName(name);
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Application> GetByNameAsync(string name)
        {
            return _repo.GetByNameAsync(name);
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
