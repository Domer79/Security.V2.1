using System.Collections.Generic;
using Security.V2.CommonContracts;

namespace Security.Tests.SecurityImplement.Repository
{
    public class EntityCollectionInfo<T> : IEntityCollectionInfo<T> where T : class
    {
        public IEnumerable<T> Entities { get; set; }
        public int PageCount { get; set; }
        public int Count { get; set; }
    }
}