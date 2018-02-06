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
            return Database.Applications.First(_ => _.AppName == name);
        }

        public IEntityCollectionInfo<Application> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            throw new NotSupportedException();
        }

        public void Remove(object id)
        {
            throw new NotSupportedException();
        }

        public void Update(Application entity)
        {
            throw new NotSupportedException();
        }
    }
}
