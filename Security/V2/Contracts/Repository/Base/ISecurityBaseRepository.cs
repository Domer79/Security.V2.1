using Security.V2.CommonContracts;

namespace Security.V2.Contracts.Repository.Base
{
    public interface ISecurityBaseRepository<T> : IRepository<T> where T : class
    {
        T GetByName(string name);
    }
}