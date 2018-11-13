using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.Contracts.Repository
{
    public interface IGrantRepository
    {
        void SetGrant(string role, string secObject);
        void SetGrants(string role, string[] secObjects);
        void RemoveGrant(string role, string secObject);
        void RemoveGrants(string role, string[] secObjects);
        IEnumerable<SecObject> GetRoleGrants(string role);
        IEnumerable<SecObject> GetExceptRoleGrant(string role);

        #region Async

        Task SetGrantAsync(string role, string secObject);
        Task SetGrantsAsync(string role, string[] secObjects);
        Task RemoveGrantAsync(string role, string secObject);
        Task RemoveGrantsAsync(string role, string[] secObjects);
        Task<IEnumerable<SecObject>> GetRoleGrantsAsync(string role);
        Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role);

        #endregion
    }
}