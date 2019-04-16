using System.Collections.Generic;
using System.Threading.Tasks;

namespace Security.CommonContracts
{
    public interface IRepository<TEntity> where TEntity: class
    {
        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(object id);

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        void Remove(object id);

        #region Async

        /// <summary>
        /// Возвращает объект по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(object id);

        /// <summary>
        /// Возвращает все объекты
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveAsync(object id);

        #endregion
    }
}