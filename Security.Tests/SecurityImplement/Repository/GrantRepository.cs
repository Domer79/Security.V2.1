using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class GrantRepository : IGrantRepository
    {
        private readonly IApplicationContext _applicationContext;

        public GrantRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IEnumerable<SecObject> GetExceptRoleGrant(string role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SecObject> GetRoleGrants(string role)
        {
            var entity = Database.Roles.First(_ => _.Name == role);
            var grants = Database.Grants.Where(_ => _.IdRole == entity.IdRole).Select(_ => _.IdSecObject);
            return Database.SecObjects.Where(_ => grants.Contains(_.IdSecObject));
        }

        public Task<IEnumerable<SecObject>> GetRoleGrantsAsync(string role)
        {
            throw new NotImplementedException();
        }

        public void RemoveGrant(string role, string secObject)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGrantAsync(string role, string secObject)
        {
            throw new NotImplementedException();
        }

        public void RemoveGrants(string role, string[] secObjects)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGrantsAsync(string role, string[] secObjects)
        {
            throw new NotImplementedException();
        }

        public void SetGrant(string role, string secObject)
        {
            var roleEntity = Database.Roles.First(_ => _.Name == role);
            var secObjectEntity = Database.SecObjects.First(_ => _.ObjectName == secObject);
            Database.Grants.Add(new Grant(){IdRole = roleEntity.IdRole, IdSecObject = secObjectEntity.IdSecObject});
        }

        public Task SetGrantAsync(string role, string secObject)
        {
            throw new NotImplementedException();
        }

        public void SetGrants(string role, string[] secObjects)
        {
            var roleEntity = Database.Roles.First(_ => _.Name == role);
            var secObjectEntities = Database.SecObjects.Where(_ => secObjects.Contains(_.ObjectName));

            foreach (var secObject in secObjectEntities)
            {
                Database.Grants.Add(new Grant() { IdRole = roleEntity.IdRole, IdSecObject = secObject.IdSecObject });
            }
        }

        public Task SetGrantsAsync(string role, string[] secObjects)
        {
            throw new NotImplementedException();
        }
    }
}
