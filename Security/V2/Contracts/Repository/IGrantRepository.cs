using System.Collections.Generic;
using Security.Model;

namespace Security.V2.Contracts.Repository
{
    public interface IGrantRepository
    {
        IApplicationContext ApplicationContext { get; }
        void SetGrant(string role, string secObject);
        void SetGrants(string role, string[] secObjects);
        IEnumerable<SecObject> GetRoleGrants(string role);
    }
}