using System.Collections.Generic;

namespace CommonContracts
{
    public interface IEntityCollectionInfo
    {
        int PageCount { get; set; }
        int Count { get; set; }
    }

    public interface IEntityCollectionInfo<TEntity> : IEntityCollectionInfo where TEntity: class
    {
        IEnumerable<TEntity> Entities { get; set; }
    }
}