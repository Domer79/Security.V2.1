using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonContracts;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        public Application Create(Application entity)
        {
            Database.ApplicationCollection.Add(entity);
            return entity;
        }

        public Application Get(object id)
        {
            return Database.ApplicationCollection.First(_ => _.IdApplication == (int)id);
        }

        public IEnumerable<Application> Get()
        {
            return Database.ApplicationCollection;
        }

        public Application GetByName(string name)
        {
            return Database.ApplicationCollection.First(_ => _.AppName == name);
        }

        public IEntityCollectionInfo<Application> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            throw new NotSupportedException();
        }

        public void Remove(object id)
        {
            var application = Get(id);
            Database.ApplicationCollection.Remove(application);
        }

        public void Update(Application entity)
        {
            Database.ApplicationCollection.Update(entity);
        }
    }
}
