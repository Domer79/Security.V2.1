using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
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

        public Task<Application> CreateAsync(Application entity)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }

        public Application CreateEmpty(string prefixForRequired)
        {
            throw new NotSupportedException();
        }

        public Task<Application> CreateEmptyAsync(string prefixForRequired)
        {
            return Task.FromException<Application>(new NotSupportedException());
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
            return Task.FromException<Application>(new NotSupportedException());
        }

        public void Update(Application entity)
        {
            throw new NotSupportedException();
        }

        public Task UpdateAsync(Application entity)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }
    }
}
