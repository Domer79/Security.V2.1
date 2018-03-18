using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
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
            throw new NotSupportedException();
        }

        public async Task<Application> CreateAsync(Application entity)
        {
            throw new NotSupportedException();
        }

        public Application CreateEmpty(string prefixForRequired)
        {
            throw new NotSupportedException();
        }

        public Task<Application> CreateEmptyAsync(string prefixForRequired)
        {
            throw new NotSupportedException();
        }

        public Application Get(object id)
        {
            return _commonDb.QuerySingle<Application>("select * from sec.Applications where idApplication = @id", new { id });
        }

        public IEnumerable<Application> Get()
        {
            return _commonDb.Query<Application>("select * from sec.Applications");
        }

        public Task<Application> GetAsync(object id)
        {
            return _commonDb.QuerySingleAsync<Application>("select * from sec.Applications where idApplication = @id", new { id });
        }

        public Task<IEnumerable<Application>> GetAsync()
        {
            return _commonDb.QueryAsync<Application>("select * from sec.Applications");
        }

        public Application GetByName(string name)
        {
            return _commonDb.QuerySingle<Application>("select * from sec.Applications where appName = @name", new {name});
        }

        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonDb.QuerySingleAsync<Application>("select * from sec.Applications where appName = @name", new { name });
        }

        public void Remove(object id)
        {
            throw new NotSupportedException();
        }

        public Task RemoveAsync(object id)
        {
            throw new NotSupportedException();
        }

        public void Update(Application entity)
        {
            throw new NotSupportedException();
        }

        public Task UpdateAsync(Application entity)
        {
            throw new NotSupportedException();
        }
    }
}
