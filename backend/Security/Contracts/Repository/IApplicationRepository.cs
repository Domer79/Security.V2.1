using Security.Contracts.Repository.Base;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Ограниченное управление приложениями
    /// </summary>
    public interface IApplicationRepository : ISecurityBaseRepository<Application>
    {
        
    }
}