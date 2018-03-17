using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository.Base;

namespace Security.V2.Contracts.Repository
{
    public interface IApplicationInternalRepository : ISecurityBaseRepository<Application>
    {
        void Remove(string appName);

        Task RemoveAsync(string appName);
    }
}