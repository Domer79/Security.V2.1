using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление политиками безопасности
    /// </summary>
    public class SecObjectRepository : ISecObjectRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private string _url;

        /// <summary>
        /// Управление политиками безопасности
        /// </summary>
        public SecObjectRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        private string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/policy"); }
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SecObject Create(SecObject entity)
        {
            return _commonWeb.PostAndGet<SecObject>(Url, entity);
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<SecObject> CreateAsync(SecObject entity)
        {
            return _commonWeb.PostAndGetAsync<SecObject>(Url, entity);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public SecObject CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<SecObject>(Url, new {prefix = prefixForRequired});
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Task<SecObject> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<SecObject>(Url, new { prefix = prefixForRequired });
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SecObject Get(object id)
        {
            return _commonWeb.Get<SecObject>(Url, new {id});
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SecObject> Get()
        {
            return _commonWeb.GetCollection<SecObject>(Url);
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SecObject> GetAsync(object id)
        {
            return _commonWeb.GetAsync<SecObject>(Url, new { id });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SecObject>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<SecObject>(Url);
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SecObject GetByName(string name)
        {
            return _commonWeb.Get<SecObject>(Url, new {name});
        }

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<SecObject> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<SecObject>(Url, new { name });
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
        public void Update(SecObject entity)
        {
            _commonWeb.Put(Url, entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(SecObject entity)
        {
            return _commonWeb.PutAsync(Url, entity);
        }
    }
}
