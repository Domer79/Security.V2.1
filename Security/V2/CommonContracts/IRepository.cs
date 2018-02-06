using System.Collections.Generic;

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
    }
}