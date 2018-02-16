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
            var id = _commonDb.ExecuteScalar<int>("EXECUTE [AddApp] @appName ,@description", entity);
            entity.IdApplication = id;

            return entity;
        }

        public async Task<Application> CreateAsync(Application entity)
        {
            var id = _commonDb.ExecuteScalarAsync<int>("EXECUTE [AddApp] @appName ,@description", entity);
            entity.IdApplication = await id;

            return entity;
        }

        public Application Get(object id)
        {
            return _commonDb.QuerySingle<Application>("select * from Applications where idApplication = @id", new { id });
        }

        public IEnumerable<Application> Get()
        {
            return _commonDb.Query<Application>("select * from Applications");
        }

        public Task<Application> GetAsync(object id)
        {
            return _commonDb.QuerySingleAsync<Application>("select * from Applications where idApplication = @id", new { id });
        }

        public Task<IEnumerable<Application>> GetAsync()
        {
            return _commonDb.QueryAsync<Application>("select * from Applications");
        }

        public Application GetByName(string name)
        {
            return _commonDb.QuerySingle<Application>("select * from Applications where appName = @name", new {name});
        }

        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonDb.QuerySingleAsync<Application>("select * from Applications where appName = @name", new { name });
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
