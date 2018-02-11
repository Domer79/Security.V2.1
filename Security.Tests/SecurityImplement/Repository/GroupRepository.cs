using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class GroupRepository : IGroupRepository
    {
        public Group Create(Group entity)
        {
            Database.Groups.Add(entity);
            return entity;
        }

        public Task<Group> CreateAsync(Group entity)
        {
            throw new System.NotImplementedException();
        }

        public Group Get(object id)
        {
            return Database.Groups.SingleOrDefault(_ => _.IdMember == (int)id);
        }

        public IEnumerable<Group> Get()
        {
            return Database.Groups;
        }

        public Task<Group> GetAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Group GetByName(string name)
        {
            return Database.Groups.SingleOrDefault(_ => _.Name == name);
        }

        public Task<Group> GetByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEntityCollectionInfo<Group> GetEntityCollectionInfo(int pageNumber, int pageSize)
        {
            var collection = new EntityCollectionInfo<Group>();
            var groups = Database.Groups.ToArray();
            collection.Entities = groups.Skip(pageNumber * pageSize - pageSize).Take(pageSize);
            collection.PageCount = groups.Length / pageSize + (groups.Length % pageSize > 0 ? 1 : 0);

            return collection;
        }

        public void Remove(object id)
        {
            var group = Database.Groups.First(_ => _.IdMember == (int) id);
            Database.Groups.Remove(group);
        }

        public Task RemoveAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Group entity)
        {
            Database.Groups.Update(entity);
        }

        public Task UpdateAsync(Group entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
