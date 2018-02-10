using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICommonDb _commonDb;

        public UserRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public User Create(User entity)
        {
            var id = _commonDb.ExecuteScalar<int>("exec sec.AddUser @id, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated, @lastActivityDate", entity);
            entity.IdMember = id;
            return entity;
        }

        public async Task<User> CreateAsync(User entity)
        {
            var id = _commonDb.ExecuteScalarAsync<int>("exec sec.AddUser @id, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated, @lastActivityDate", entity);
            entity.IdMember = await id;
            return entity;
        }

        public User Get(object id)
        {
            return _commonDb.QuerySingle<User>("select * from sec.UsersView where idMember = @id", new {id});
        }

        public IEnumerable<User> Get()
        {
            return _commonDb.Query<User>("select * from sec.UsersView");
        }

        public Task<User> GetAsync(object id)
        {
            return _commonDb.QuerySingleAsync<User>("select * from sec.UsersView where idMember = @id", new { id });
        }

        public Task<IEnumerable<User>> GetAsync()
        {
            return _commonDb.QueryAsync<User>("select * from sec.UsersView");
        }

        public User GetByName(string loginOrEmail)
        {
            return _commonDb.QuerySingle<User>("select * from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail", new {loginOrEmail});
        }

        public Task<User> GetByNameAsync(string loginOrEmail)
        {
            return _commonDb.QuerySingleAsync<User>("select * from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail", new { loginOrEmail });
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("exec sec.DeleteUser @idMember", new {idMember = id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("exec sec.DeleteUser @idMember", new { idMember = id });
        }

        public void SetStatus(string loginOrEmail, bool status)
        {
            _commonDb.ExecuteNonQuery("update sec.Users set Status = @status where idMember = (select idMember from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail)", new {loginOrEmail, status});
        }

        public Task SetStatusAsync(string loginOrEmail, bool status)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.Users set Status = @status where idMember = (select idMember from sec.UsersView where login = @loginOrEmail or email = @loginOrEmail)", new { loginOrEmail, status });
        }

        public void Update(User entity)
        {
            _commonDb.ExecuteNonQuery("exec sec.UpdateUser @idMember, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated, @lastActivityDate", entity);
        }

        public Task UpdateAsync(User entity)
        {
            return _commonDb.ExecuteNonQueryAsync("exec sec.UpdateUser @idMember, @login, @firstName, @lastName, @middleName, @email, @status, @passwordSalt, @dateCreated, @lastActivityDate", entity);
        }
    }
}
