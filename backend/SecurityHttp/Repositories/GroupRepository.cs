using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление группами
    /// </summary>
    public class GroupRepository : IGroupRepository
    {
        private readonly ICommonWeb _commonWeb;

        /// <summary>
        /// Управление группами
        /// </summary>
        public GroupRepository(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Group Create(Group entity)
        {
            return _commonWeb.PostAndGet<Group>("api/groups", entity);
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<Group> CreateAsync(Group entity)
        {
            return _commonWeb.PostAndGetAsync<Group>("api/groups", entity);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Group CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<Group>("api/groups", new {prefix = prefixForRequired});
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Task<Group> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<Group>("api/groups", new { prefix = prefixForRequired });
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Group Get(object id)
        {
            return _commonWeb.Get<Group>("api/groups", new {id});
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Group> Get()
        {
            return _commonWeb.GetCollection<Group>("api/groups");
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Group> GetAsync(object id)
        {
            return _commonWeb.GetAsync<Group>("api/groups", new { id });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Group>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<Group>("api/groups");
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Group GetByName(string name)
        {
            return _commonWeb.Get<Group>($"api/groups", new { name });
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Group> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<Group>($"api/groups", new { name });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonWeb.Delete("api/groups", new {id});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync("api/groups", new { id });
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Group entity)
        {
            _commonWeb.Put("api/groups", entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(Group entity)
        {
            return _commonWeb.PutAsync("api/groups", entity);
        }
    }
}
