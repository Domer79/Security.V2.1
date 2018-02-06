using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.DataLayer
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
            var id = _commonDb.Query<int>("EXECUTE [sec].[AddApp] @appName ,@description", entity).Single();
            entity.IdApplication = id;

            return entity;
        }

        public Application Get(object id)
        {
            return _commonDb.Query<Application>("select * from sec.Applications where idApplication = @id", new {id}).Single();
        }

        public IEnumerable<Application> Get()
        {
            return _commonDb.Query<Application>("select * from sec.Applications");
        }

        public Application GetByName(string name)
        {
            return _commonDb.Query<Application>("select * from sec.Applications where appName = @appName", new { appName = name }).Single();
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("execute sec.DeleteApp @idApplication", new {idApplication = id});
        }

        public void Update(Application entity)
        {
            _commonDb.ExecuteNonQuery("execute sec.UpdateApp @idApplication, @appName, @description", entity);
        }
    }
}
