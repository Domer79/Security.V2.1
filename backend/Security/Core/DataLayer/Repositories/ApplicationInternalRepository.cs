using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.CommonContracts;
using Security.Contracts.Repository;
using Security.Model;

namespace Security.Core.DataLayer.Repositories
{
    public class ApplicationInternalRepository : IApplicationInternalRepository
    {
        private readonly ICommonDb _commonDb;

        public ApplicationInternalRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public Application Create(Application entity)
        {
            var id = _commonDb.ExecuteScalar<int>("EXECUTE [sec].[AddApp] @appName ,@description", entity);
            entity.IdApplication = id;

            return entity;
        }

        public async Task<Application> CreateAsync(Application entity)
        {
            var id = await _commonDb.ExecuteScalarAsync<int>("EXECUTE [sec].[AddApp] @appName ,@description", entity);
            entity.IdApplication = id;

            return entity;
        }

        public Application Get(object id)
        {
            return _commonDb.QuerySingleOrDefault<Application>("select * from sec.Applications where idApplication = @id", new {id});
        }

        public Task<Application> GetAsync(object id)
        {
            return _commonDb.QuerySingleOrDefaultAsync<Application>("select * from sec.Applications where idApplication = @id", new { id });
        }

        public IEnumerable<Application> Get()
        {
            return _commonDb.Query<Application>("select * from sec.Applications");
        }

        public Task<IEnumerable<Application>> GetAsync()
        {
            return _commonDb.QueryAsync<Application>("select * from sec.Applications");
        }

        public Application GetByName(string name)
        {
            return _commonDb.QuerySingleOrDefault<Application>("select * from sec.Applications where appName = @appName", new { appName = name });
        }

        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonDb.QuerySingleOrDefaultAsync<Application>("select * from sec.Applications where appName = @appName", new { appName = name });
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("execute sec.DeleteApp @idApplication", new {idApplication = id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("execute sec.DeleteApp @idApplication", new { idApplication = id });
        }

        public void Update(Application entity)
        {
            _commonDb.ExecuteNonQuery("execute sec.UpdateApp @idApplication, @description", new {entity.IdApplication, entity.Description});
        }

        public Task UpdateAsync(Application entity)
        {
            return _commonDb.ExecuteNonQueryAsync("execute sec.UpdateApp @idApplication, @description", new { entity.IdApplication, entity.Description });
        }

        public Application CreateEmpty(string prefixForRequired)
        {
            throw new NotSupportedException();
        }

        public Task<Application> CreateEmptyAsync(string prefixForRequired)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }

        public void Remove(string appName)
        {
            _commonDb.ExecuteNonQuery(@"exec sec.DeleteAppByName @appName", new {appName});
        }

        public Task RemoveAsync(string appName)
        {
            return _commonDb.ExecuteNonQueryAsync(@"exec sec.DeleteAppByName @appName", new { appName });
        }
    }
}
