using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление разрешениями
    /// </summary>
    public class GrantRepository : IGrantRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private string _url;

        /// <summary>
        /// Управление разрешениями
        /// </summary>
        public GrantRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        private string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/grants"); }
        }

        /// <summary>
        /// Получение отсутствующих у роли политик безопасности
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<SecObject> GetExceptRoleGrant(string role)
        {
            return _commonWeb.GetCollection<SecObject>($"{Url}/except", new {role});
        }

        /// <summary>
        /// Получение отсутствующих у роли политик безопасности
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<SecObject>($"{Url}/except", new { role });
        }

        /// <summary>
        /// Получение политик безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<SecObject> GetRoleGrants(string role)
        {
            return _commonWeb.GetCollection<SecObject>(Url, new {role});
        }

        /// <summary>
        /// Получение политик безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<SecObject>> GetRoleGrantsAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<SecObject>(Url, new { role });
        }

        /// <summary>
        /// Удаление разрешения из роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        public void RemoveGrant(string role, string secObject)
        {
            _commonWeb.Delete(Url, new [] {secObject}, new {role});
        }

        /// <summary>
        /// Удаление разрешения из роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public Task RemoveGrantAsync(string role, string secObject)
        {
            return _commonWeb.DeleteAsync(Url, new [] { secObject }, new { role });
        }

        /// <summary>
        /// Удаление нескольких разрешений для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObjects">Политики безопасности</param>
        public void RemoveGrants(string role, string[] secObjects)
        {
            _commonWeb.Delete(Url, secObjects, new {role});
        }

        /// <summary>
        /// Удаление нескольких разрешений для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        public Task RemoveGrantsAsync(string role, string[] secObjects)
        {
            return _commonWeb.DeleteAsync(Url, secObjects, new { role });
        }

        /// <summary>
        /// Установка разрешения для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObject">Политика безопасности</param>
        public void SetGrant(string role, string secObject)
        {
            _commonWeb.Put(Url, new [] { secObject }, new { role });
        }

        /// <summary>
        /// Установка разрешения для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        public Task SetGrantAsync(string role, string secObject)
        {
            return _commonWeb.PutAsync(Url, new [] { secObject }, new { role });
        }

        /// <summary>
        /// Установка нескольких разрешений для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObjects">Политики безопасности</param>
        public void SetGrants(string role, string[] secObjects)
        {
            _commonWeb.Put(Url, secObjects, new { role });
        }

        /// <summary>
        /// Установка нескольких разрешений для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        public Task SetGrantsAsync(string role, string[] secObjects)
        {
            return _commonWeb.PutAsync(Url, secObjects, new { role });
        }
    }
}
