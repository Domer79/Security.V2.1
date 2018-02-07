using System.Collections.Generic;
using System.Linq;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.DataLayer.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ICommonDb _commonDb;

        public GroupRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public Group Create(Group entity)
        {
            var id = _commonDb.ExecuteScalar<int>("execute sec.AddGroup @id, @name, @description", entity);
            entity.IdMember = id;
            return entity;
        }

        public Group Get(object id)
        {
            return _commonDb.Query<Group>("select * from sec.GroupsView where idMember = @id", new {id}).Single();
        }

        public IEnumerable<Group> Get()
        {
            return _commonDb.Query<Group>("select * from sec.GroupsView");
        }

        public Group GetByName(string name)
        {
            return _commonDb.Query<Group>("select * from sec.GroupsView where name = @name", new { name }).Single();
        }

        public void Remove(object id)
        {
            _commonDb.ExecuteNonQuery("execute sec.DeleteGroup @idMember", new {idMember = id});
        }

        public void Update(Group entity)
        {
            _commonDb.ExecuteNonQuery("execute sec.UpdateGroup @id, @idMember, @name, @description", entity);
        }
    }
}
