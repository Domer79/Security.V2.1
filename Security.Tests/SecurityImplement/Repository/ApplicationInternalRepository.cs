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

        public Application Get(object id)
        {
            return Database.Applications.First(_ => _.IdApplication == (int)id);
        }

        public IEnumerable<Application> Get()
        {
            return Database.Applications;
        }

        public Application GetByName(string name)
        {
            return Database.Applications.SingleOrDefault(_ => _.AppName == name);
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

        public void Update(Application entity)
        {
            Database.Applications.Update(entity);
        }
    }
}
