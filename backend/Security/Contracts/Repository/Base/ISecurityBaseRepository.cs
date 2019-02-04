using System.Threading.Tasks;
using Security.CommonContracts;

namespace Security.Contracts.Repository.Base
{
    public interface ISecurityBaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        T CreateEmpty(string prefixForRequired);

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T GetByName(string name);

        #region Async

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        /// <param name="prefixForRequired"></param>
        /// <returns></returns>
        Task<T> CreateEmptyAsync(string prefixForRequired);

        /// <summary>
        /// Получение объекта по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<T> GetByNameAsync(string name);

        #endregion
    }
}