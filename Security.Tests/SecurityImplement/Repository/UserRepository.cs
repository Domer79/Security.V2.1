using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Create(User entity)
        {
            entity.PasswordSalt = Guid.NewGuid().ToString("N");
            Database.Users.Add(entity);
            return entity;
        }

        public User Get(object id)
        {
            return Database.Users.Single(_ => _.IdMember == (int) id);
        }

        public IEnumerable<User> Get()
        {
            return Database.Users;
        }

        public User GetByName(string name)
        {
            return Database.Users.Single(_ => _.Login == name);
        }

        public User GetByEmail(string email)
        {
            return Database.Users.Single(_ => _.Email == email);
        }

        public IEntityCollectionInfo<User> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            var collection = new EntityCollectionInfo<User>();
            var antities = Database.Users.ToArray();
            collection.Entities = antities.Skip(pageNumber * pageSize - pageSize).Take(pageSize);
            collection.PageCount = antities.Length / pageSize + (antities.Length % pageSize > 0 ? 1 : 0);

            return collection;

        }

        public void Remove(object id)
        {
            var user = Database.Users.First(_ => _.IdMember == (int)id);
            Database.Users.Remove(user);
        }

        public void Update(User entity)
        {
            Database.Users.Update(entity);
        }

        public User Get(string loginOrEmail)
        {
            return Database.Users.Single(_ => _.Login == loginOrEmail || _.Email == loginOrEmail);
        }

        public Task<User> GetByNameAsync(string loginOrEmail)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(string loginOrEmail, bool status)
        {
            throw new NotImplementedException();
        }

        public Task SetStatusAsync(string loginOrEmail, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}