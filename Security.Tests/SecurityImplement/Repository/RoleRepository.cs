using System.Collections.Generic;
using System.Linq;
using CommonContracts;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private IApplicationContext _applicationContext;
        private readonly int _idApplication;

        public RoleRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _idApplication = applicationContext.Application.IdApplication;
        }

        public IApplicationContext ApplicationContext => _applicationContext;

        public Role Create(Role entity)
        {
            Database.Roles.Add(entity);
            return entity;
        }

        public Role Get(object id)
        {
            return Database.Roles.SingleOrDefault(_ => _.IdRole == (int)id);
        }

        public IEnumerable<Role> Get()
        {
            return Database.Roles.Where(_ => _.IdApplication == _idApplication);
        }

        public Role GetByName(string name)
        {
            return Database.Roles.SingleOrDefault(_ => _.Name == name && _.IdApplication == _idApplication);
        }

        public IEntityCollectionInfo<Role> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            var collection = new EntityCollectionInfo<Role>();
            var antities = Database.Roles.ToArray();
            collection.Entities = antities.Skip(pageNumber * pageSize - pageSize).Take(pageSize);
            collection.PageCount = antities.Length / pageSize + (antities.Length % pageSize > 0 ? 1 : 0);

            return collection;
        }

        public void Remove(object id)
        {
            var role = Database.Roles.First(_ => _.IdRole == (int) id);
            Database.Roles.Remove(role);
        }

        public void Update(Role entity)
        {
            Database.Roles.Update(entity);
        }
    }
}