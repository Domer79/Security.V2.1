using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<Role> CreateAsync(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public Role Get(object id)
        {
            return Get().SingleOrDefault(_ => _.IdRole == (int)id);
        }

        public IEnumerable<Role> Get()
        {
            return Database.Roles.Where(_ => _.IdApplication == IdApplication);
        }

        public Task<Role> GetAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Role GetByName(string name)
        {
            return Get().SingleOrDefault(_ => _.Name == name);
        }

        public Task<Role> GetByNameAsync(string name)
        {
            throw new System.NotImplementedException();
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

        public Task RemoveAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Role entity)
        {
            Database.Roles.Update(entity);
        }

        public Task UpdateAsync(Role entity)
        {
            throw new System.NotImplementedException();
        }
    }
}