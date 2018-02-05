using System;
using System.Collections.Generic;
using System.Linq;
using CommonContracts;
using Security.Model;
using Security.Tests.SecurityFake;
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
    }
}