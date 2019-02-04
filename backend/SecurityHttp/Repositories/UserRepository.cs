using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ICommonWeb _commonWeb;

        /// <summary>
        /// Управление пользователями
        /// </summary>
        public UserRepository(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public User Create(User entity)
        {
            return _commonWeb.PostAndGet<User>("api/user", entity);
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<User> CreateAsync(User entity)
        {
            return _commonWeb.PostAndGetAsync<User>("api/user", entity);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public User CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<User>("api/user", new {prefixForRequired});
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public Task<User> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<User>("api/user", new { prefixForRequired });
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(object id)
        {
            var user = _commonWeb.Get<User>($"api/user", new {id});
            return user;
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetAsync(object id)
        {
            var user = await _commonWeb.GetAsync<User>($"api/user", new { id });
            return user;
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            var users = _commonWeb.Get<IEnumerable<User>>("api/user");
            return users;
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetAsync()
        {
            var users = _commonWeb.GetAsync<IEnumerable<User>>("api/user");
            return users;
        }

        /// <summary>
        /// Возвращает пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        public User GetByName(string loginOrEmail)
        {
            var user = _commonWeb.Get<User>($"api/user", new {loginOrEmail});
            return user;
        }

        /// <summary>
        /// Возвращает пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        public Task<User> GetByNameAsync(string loginOrEmail)
        {
            var user = _commonWeb.GetAsync<User>($"api/user", new { loginOrEmail });
            return user;
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonWeb.Delete("api/user", new {id});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync("api/user", new {id});
        }

        /// <summary>
        /// Устанавливает статус пользователю
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="status"></param>
        public void SetStatus(string loginOrEmail, bool status)
        {
            _commonWeb.Post("api/user/setstatus", null, new {loginOrEmail, status});
        }

        /// <summary>
        /// Устанавливает статус пользователю
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task SetStatusAsync(string loginOrEmail, bool status)
        {
            return _commonWeb.PostAsync("api/user/setstatus", null, new {loginOrEmail, status});
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(User entity)
        {
            _commonWeb.Put("api/user", entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(User entity)
        {
            return _commonWeb.PutAsync("api/user", entity);
        }
    }
}
