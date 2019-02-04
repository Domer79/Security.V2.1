using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление ролями
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private string _url;

        /// <summary>
        /// Управление ролями
        /// </summary>
        public RoleRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        private string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/roles"); }
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Role Create(Role entity)
        {
            return _commonWeb.PostAndGet<Role>(Url, entity);
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<Role> CreateAsync(Role entity)
        {
            return _commonWeb.PostAndGetAsync<Role>(Url, entity);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Role CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<Role>(Url, new {prefix = prefixForRequired});
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Task<Role> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<Role>(Url, new { prefix = prefixForRequired });
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role Get(object id)
        {
            return _commonWeb.Get<Role>(Url, new {id});
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> Get()
        {
            return _commonWeb.GetCollection<Role>(Url);
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Role> GetAsync(object id)
        {
            return _commonWeb.GetAsync<Role>(Url, new { id });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Role>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<Role>(Url);
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Role GetByName(string name)
        {
            return _commonWeb.Get<Role>(Url, new {name});
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Role> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<Role>(Url, new { name });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonWeb.Delete(Url, new {id});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync(Url, new { id });
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Role entity)
        {
            _commonWeb.Put(Url, entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(Role entity)
        {
            return _commonWeb.PutAsync(Url, entity);
        }
    }
}
