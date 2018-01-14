namespace Security.V2.Contracts.Repository.Base
{
    public interface IApplicationBaseRepository<T> : ISecurityBaseRepository<T> where T : class
    {
        IApplicationContext ApplicationContext { get; }
    }
}