using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonContracts;
using Security.Model;
using Security.Tests.SecurityFake;
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

        public Group Get(object id)
        {
            return Database.Groups.SingleOrDefault(_ => _.IdMember == (int)id);
        }

        public IEnumerable<Group> Get()
        {
            return Database.Groups;
        }

        public Group GetByName(string name)
        {
            return Database.Groups.SingleOrDefault(_ => _.Name == name);
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

        public void Update(Group entity)
        {
            Database.Groups.Update(entity);
        }
    }
}
