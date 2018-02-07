using System.Collections.Generic;
using System.Threading.Tasks;

namespace Security.V2.CommonContracts
{
    public interface IRepository<TEntity> where TEntity: class
    {
        TEntity Get(object id);
        IEnumerable<TEntity> Get();
//        IEntityCollectionInfo<TEntity> GetEntityCollectionInfo(int pageNumber, int pageSize);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Remove(object id);

        #region Async

        Task<TEntity> GetAsync(object id);
        Task<IEnumerable<TEntity>> GetAsync();
        //        IEntityCollectionInfo<TEntity> GetEntityCollectionInfo(int pageNumber, int pageSize);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(object id);

        #endregion
    }
}