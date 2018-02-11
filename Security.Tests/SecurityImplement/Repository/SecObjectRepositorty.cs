using System.Collections.Generic;
using System.Linq;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;
using System.Threading.Tasks;

namespace Security.Tests.SecurityImplement.Repository
{
    public class SecObjectRepositorty : ISecObjectRepository
    {
        private readonly IApplicationContext _context;

        public SecObjectRepositorty(IApplicationContext context)
        {
            _context = context;
        }

        private int IdApplication
        {
            get { return _context.Application.IdApplication; }
        }

        public SecObject Create(SecObject entity)
        {
            entity.IdApplication = IdApplication;
            Database.SecObjects.Add(entity);
            return entity;
        }

        public Task<SecObject> CreateAsync(SecObject entity)
        {
            throw new System.NotImplementedException();
        }

        public SecObject Get(object id)
        {
            return Get().SingleOrDefault(_ => _.IdSecObject == (int) id);
        }

        public IEnumerable<SecObject> Get()
        {
            return Database.SecObjects.Where(_ => _.IdApplication == IdApplication);
        }

        public Task<SecObject> GetAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SecObject>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public SecObject GetByName(string name)
        {
            return Get().SingleOrDefault(_ => _.ObjectName == name);
        }

        public Task<SecObject> GetByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEntityCollectionInfo<SecObject> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            var collection = new EntityCollectionInfo<SecObject>();
            var antities = Get().ToArray();
            collection.Entities = antities.Skip(pageNumber * pageSize - pageSize).Take(pageSize);
            collection.PageCount = antities.Length / pageSize + (antities.Length % pageSize > 0 ? 1 : 0);

            return collection;
        }

        public void Remove(object id)
        {
            var secObject = Get().First(_ => _.IdSecObject == (int)id);
            Database.SecObjects.Remove(secObject);
        }

        public Task RemoveAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(SecObject entity)
        {
            Database.SecObjects.Update(entity);
        }

        public Task UpdateAsync(SecObject entity)
        {
            throw new System.NotImplementedException();
        }
    }
}