using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
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
            var id = _commonDb.ExecuteScalar<int>("EXECUTE [AddApp] @appName ,@description", entity);
            entity.IdApplication = id;

            return entity;
        }

        public async Task<Application> CreateAsync(Application entity)
        {
            var id = await _commonDb.ExecuteScalarAsync<int>("EXECUTE [AddApp] @appName ,@description", entity);
            entity.IdApplication = id;

            return entity;
        }

        public Application Get(object id)
        {
            return _commonDb.QuerySingle<Application>("select * from Applications where idApplication = @id", new {id});
        }

        public Task<Application> GetAsync(object id)
        {
            return _commonDb.QuerySingleAsync<Application>("select * from Applications where idApplication = @id", new { id });
        }

        public IEnumerable<Application> Get()
        {
            return _commonDb.Query<Application>("select * from Applications");
        }

        public Task<IEnumerable<Application>> GetAsync()
        {
            return _commonDb.QueryAsync<Application>("select * from Applications");
        }

        public Application GetByName(string name)
        {
            return _commonDb.QuerySingleOrDefault<Application>("select * from Applications where appName = @appName", new { appName = name });
        }

        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonDb.QuerySingleOrDefaultAsync<Application>("select * from Applications where appName = @appName", new { appName = name });
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("execute DeleteApp @idApplication", new {idApplication = id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("execute DeleteApp @idApplication", new { idApplication = id });
        }

        public void Update(Application entity)
        {
            _commonDb.ExecuteNonQuery("execute UpdateApp @idApplication, @appName, @description", entity);
        }

        public Task UpdateAsync(Application entity)
        {
            return _commonDb.ExecuteNonQueryAsync("execute UpdateApp @idApplication, @appName, @description", entity);
        }
    }
}
