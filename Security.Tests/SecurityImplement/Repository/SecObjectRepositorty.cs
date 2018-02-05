using System.Collections.Generic;
using System.Linq;
using CommonContracts;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class SecObjectRepositorty : ISecObjectRepository
    {
        private readonly IApplicationContext _context;

        public SecObjectRepositorty(IApplicationContext context)
        {
            _context = context;
        }

        public IApplicationContext ApplicationContext => _context;

        private int IdApplication
        {
            get { return _context.Application.IdApplication; }
        }

        public SecObject Create(SecObject entity)
        {
            entity.IdApplication = IdApplication;
            Database.SecObjects.Add(entity);
            return entity;
        }

        public SecObject Get(object id)
        {
            return Database.SecObjects.SingleOrDefault(_ => _.IdSecObject == (int) id);
        }

        public IEnumerable<SecObject> Get()
        {
            return Database.SecObjects.Where(_ => _.IdApplication == IdApplication);
        }

        public SecObject GetByName(string name)
        {
            return Database.SecObjects.SingleOrDefault(_ => _.ObjectName == name);
        }

        public IEntityCollectionInfo<SecObject> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            var collection = new EntityCollectionInfo<SecObject>();
            var antities = Database.SecObjects.ToArray();
            collection.Entities = antities.Skip(pageNumber * pageSize - pageSize).Take(pageSize);
            collection.PageCount = antities.Length / pageSize + (antities.Length % pageSize > 0 ? 1 : 0);

            return collection;
        }

        public void Remove(object id)
        {
            var secObject = Database.SecObjects.First(_ => _.IdSecObject == (int)id);
            Database.SecObjects.Remove(secObject);
        }

        public void Update(SecObject entity)
        {
            Database.SecObjects.Update(entity);
        }
    }
}