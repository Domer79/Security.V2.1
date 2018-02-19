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
    public class ApplicationRepository : IApplicationRepository
    {
        public Application Create(Application entity)
        {
            throw new NotSupportedException();
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
            return Database.Applications.First(_ => _.AppName == name);
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
            throw new NotSupportedException();
        }

        public Task RemoveAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(Application entity)
        {
            throw new NotSupportedException();
        }

        public Task UpdateAsync(Application entity)
        {
            throw new NotImplementedException();
        }
    }
}
