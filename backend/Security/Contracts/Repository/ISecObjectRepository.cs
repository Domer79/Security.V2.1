using Security.Contracts.Repository.Base;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Управление политиками безопасности
    /// </summary>
    public interface ISecObjectRepository : ISecurityBaseRepository<SecObject>
    {
        
    }
}