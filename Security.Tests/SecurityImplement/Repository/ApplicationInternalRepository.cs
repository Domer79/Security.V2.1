using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class ApplicationInternalRepository : IApplicationInternalRepository
    {
        public Application Create(Application entity)
        {
            Database.Applications.Add(entity);
            return entity;
        }

        public Task<Application> CreateAsync(Application entity)
        {
            throw new NotImplementedException();
        }

        public Application CreateEmpty(string prefixForRequired)
        {
            throw new NotImplementedException();
        }

        public Task<Application> CreateEmptyAsync(string prefixForRequired)
        {
            throw new NotImplementedException();
        }

        public Application Get(object id)
        {
            return Database.Applications.First(_ => _.IdApplication == (int)id);
        }

        public IEnumerable<Application> Get()
        {
            return Database.Applications;
        }

        public Task<Application> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Application>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Application GetByName(string name)
        {
            return Database.Applications.SingleOrDefault(_ => _.AppName == name);
        }

        public Task<Application> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IEntityCollectionInfo<Application> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            throw new NotSupportedException();
        }

        public void Remove(object id)
        {
            var application = Get(id);
            Database.Applications.Remove(application);
        }

        public Task RemoveAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(Application entity)
        {
            Database.Applications.Update(entity);
        }

        public Task UpdateAsync(Application entity)
        {
            throw new NotImplementedException();
        }
    }
}
