using System.Threading.Tasks;
using Security.CommonContracts;

namespace Security.Contracts.Repository.Base
{
    public interface ISecurityBaseRepository<T> : IRepository<T> where T : class
    {
        T CreateEmpty(string prefixForRequired);
        T GetByName(string name);

        #region Async

        Task<T> CreateEmptyAsync(string prefixForRequired);
        Task<T> GetByNameAsync(string name);

        #endregion
    }
}