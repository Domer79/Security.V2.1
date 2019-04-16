using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ICommonDb _commonDb;

        /// <summary>
        /// Управление пользователями
        /// </summary>
        public UserRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public User Create(User entity)
        {
            if (entity.PasswordSalt == null)
                entity.PasswordSalt = Guid.NewGuid().ToString("N");

            var id = _commonDb.ExecuteScalar<int>("exec sec.AddUser @id, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated", entity);

            return Get(id);
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<User> CreateAsync(User entity)
        {
            if (entity.PasswordSalt == null)
                entity.PasswordSalt = Guid.NewGuid().ToString("N");

            var id = _commonDb.ExecuteScalarAsync<int>("exec sec.AddUser @id, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated", entity);
            
            return await GetAsync(await id).ConfigureAwait(false);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public User CreateEmpty(string prefixForRequired)
        {
            var idMember = _commonDb.ExecuteScalar<int>("exec sec.AddEmptyUser @id, @prefix", new { id = Guid.NewGuid(), prefix = prefixForRequired });

            return Get(idMember);
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        public async Task<User> CreateEmptyAsync(string prefixForRequired)
        {
            var idMember = await _commonDb.ExecuteScalarAsync<int>("exec sec.AddEmptyUser @id, @prefix", new { id = Guid.NewGuid(), prefix = prefixForRequired }).ConfigureAwait(false);

            return await GetAsync(idMember);
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(object id)
        {
            return _commonDb.QuerySingleOrDefault<User>("select * from sec.UsersView where idMember = @id", new {id});
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return _commonDb.Query<User>("select * from sec.UsersView");
        }

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<User> GetAsync(object id)
        {
            return _commonDb.QuerySingleOrDefaultAsync<User>("select * from sec.UsersView where idMember = @id", new { id });
        }

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetAsync()
        {
            return _commonDb.QueryAsync<User>("select * from sec.UsersView");
        }

        /// <summary>
        /// Возвращает пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        public User GetByName(string loginOrEmail)
        {
            return _commonDb.QuerySingleOrDefault<User>("select * from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail", new {loginOrEmail});
        }

        /// <summary>
        /// Возвращает пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        public Task<User> GetByNameAsync(string loginOrEmail)
        {
            return _commonDb.QuerySingleOrDefaultAsync<User>("select * from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail", new { loginOrEmail });
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("exec sec.DeleteUser @idMember", new {idMember = id});
        }

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("exec sec.DeleteUser @idMember", new { idMember = id });
        }

        /// <summary>
        /// Устанавливает статус пользователю
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="status"></param>
        public void SetStatus(string loginOrEmail, bool status)
        {
            _commonDb.ExecuteNonQuery("update sec.Users set Status = @status where idMember = (select idMember from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail)", new {loginOrEmail, status});
        }

        /// <summary>
        /// Устанавливает статус пользователю
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task SetStatusAsync(string loginOrEmail, bool status)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.Users set Status = @status where idMember = (select idMember from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail)", new { loginOrEmail, status });
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        public void Update(User entity)
        {
            _commonDb.ExecuteNonQuery("exec sec.UpdateUser @idMember, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated, @lastActivityDate", entity);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(User entity)
        {
            return _commonDb.ExecuteNonQueryAsync("exec sec.UpdateUser @idMember, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated, @lastActivityDate", entity);
        }
    }
}
