using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.DataLayer.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ICommonDb _commonDb;

        public ApplicationRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public Application Create(Application entity)
        {
            var id = _commonDb.Query<int>("EXECUTE [sec].[AddApp] @appName ,@description", entity).Single();
            entity.IdApplication = id;

            return entity;
        }

        public Application Get(object id)
        {
            return _commonDb.Query<Application>("select * from sec.Applications where idApplication = @id", new { id }).Single();
        }

        public IEnumerable<Application> Get()
        {
            return _commonDb.Query<Application>("select * from sec.Applications");
        }

        public Application GetByName(string name)
        {
            throw new NotImplementedException();
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
