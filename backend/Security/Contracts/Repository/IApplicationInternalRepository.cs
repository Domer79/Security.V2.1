using System.Threading.Tasks;
using Security.Contracts.Repository.Base;
using Security.Model;

namespace Security.Contracts.Repository
{
    public interface IApplicationInternalRepository : ISecurityBaseRepository<Application>
    {
        void Remove(string appName);

        Task RemoveAsync(string appName);
    }
}