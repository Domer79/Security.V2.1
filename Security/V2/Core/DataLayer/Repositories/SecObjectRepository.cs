using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
{
    public class SecObjectRepository : ISecObjectRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;

        public SecObjectRepository(ICommonDb commonDb, IApplicationContext context)
        {
            _commonDb = commonDb;
            _context = context;
        }

        public SecObject Create(SecObject entity)
        {
            entity.IdApplication = _context.Application.IdApplication;
            var id = _commonDb.ExecuteScalar<int>(@"
insert into sec.SecObjects(idSecObject, ObjectName, idApplication) values(@idSecObject, @objectName, @idApplication)
select SCOPE_IDENTITY()
");
            entity.IdSecObject = id;
            return entity;
        }

        public async Task<SecObject> CreateAsync(SecObject entity)
        {
            entity.IdApplication = _context.Application.IdApplication;
            var id = _commonDb.ExecuteScalarAsync<int>(@"
insert into sec.SecObjects(idSecObject, ObjectName, idApplication) values(@idSecObject, @objectName, @idApplication)
select SCOPE_IDENTITY()
");
            entity.IdSecObject = await id;
            return entity;
        }

        public SecObject Get(object id)
        {
            return _commonDb.QuerySingle<SecObject>("select * from sec.SecObjects where idSecObject = @id", new {id});
        }

        public IEnumerable<SecObject> Get()
        {
            return _commonDb.Query<SecObject>("select * from sec.SecObjects where idApplication = @idApplication", new {_context.Application.IdApplication});
        }

        public Task<SecObject> GetAsync(object id)
        {
            return _commonDb.QuerySingleAsync<SecObject>("select * from sec.SecObjects where idSecObject = @id", new { id });
        }

        public Task<IEnumerable<SecObject>> GetAsync()
        {
            return _commonDb.QueryAsync<SecObject>("select * from sec.SecObjects where idApplication = @idApplication", new { _context.Application.IdApplication });
        }

        public SecObject GetByName(string name)
        {
            return _commonDb.QuerySingle<SecObject>("select * from sec.SecObject where objectName = @name and idApplication = @idApplication", new { name, _context.Application.IdApplication });
        }

        public Task<SecObject> GetByNameAsync(string name)
        {
            return _commonDb.QuerySingleAsync<SecObject>("select * from sec.SecObject where objectName = @name and idApplication = @idApplication", new { name, _context.Application.IdApplication });
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("delete from sec.SecObjects where idSecObject = @id", new { id });
        }

        public Task RemoveAsync(object id)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.SecObjects where idSecObject = @id", new { id });
        }

        public void Update(SecObject entity)
        {
            _commonDb.ExecuteNonQuery("update sec.SecObjects set objectName = @objectName where idSecObject = @idSecObject", new { entity });
        }

        public Task UpdateAsync(SecObject entity)
        {
            return _commonDb.ExecuteNonQueryAsync("update sec.SecObjects set objectName = @objectName where idSecObject = @idSecObject", new { entity });
        }
    }
}
