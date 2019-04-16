using System.Threading.Tasks;
using Security.Contracts.Repository.Base;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Управление приложениями
    /// </summary>
    public interface IApplicationInternalRepository : ISecurityBaseRepository<Application>
    {
        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        void Remove(string appName);

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        Task RemoveAsync(string appName);
    }
}