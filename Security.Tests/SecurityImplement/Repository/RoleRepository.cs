using System.Collections.Generic;
using System.Linq;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private IApplicationContext _applicationContext;

        public RoleRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        private int IdApplication => _applicationContext.Application.IdApplication;

        public Role Create(Role entity)
        {
            entity.IdApplication = IdApplication;
            Database.Roles.Add(entity);
            return entity;
        }

        public Role Get(object id)
        {
            return Get().SingleOrDefault(_ => _.IdRole == (int)id);
        }

        public IEnumerable<Role> Get()
        {
            return Database.Roles.Where(_ => _.IdApplication == IdApplication);
        }

        public Role GetByName(string name)
        {
            return Get().SingleOrDefault(_ => _.Name == name);
        }

        public IEntityCollectionInfo<Role> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            var collection = new EntityCollectionInfo<Role>();
            var antities = Get().ToArray();
            collection.Entities = antities.Skip(pageNumber * pageSize - pageSize).Take(pageSize);
            collection.PageCount = antities.Length / pageSize + (antities.Length % pageSize > 0 ? 1 : 0);

            return collection;
        }

        public void Remove(object id)
        {
            var role = Get().First(_ => _.IdRole == (int) id);
            Database.Roles.Remove(role);
        }

        public void Update(Role entity)
        {
            Database.Roles.Update(entity);
        }
    }
}