using System.Threading.Tasks;
using Security.V2.CommonContracts;

namespace Security.V2.Contracts.Repository.Base
{
    public interface ISecurityBaseRepository<T> : IRepository<T> where T : class
    {
        T CreateEmpty(string prefixForRequired);
        Task<T> CreateEmptyAsync(string prefixForRequired);
        T GetByName(string name);
        Task<T> GetByNameAsync(string name);
    }
}