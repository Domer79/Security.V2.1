using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.DataLayer.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ICommonDb _commonDb;

        public UserGroupRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public void AddGroupsToUser(int[] idGroups, int idUser)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.UserGroups(idUser, idGroup)
select @idUser idUser, idMember from (select idMember from sec.Groups where idGroup in @idGroups) s1
", new {idUser, idGroups});
        }

        public void AddGroupsToUser(Guid[] groupsId, Guid userId)
        {
            _commonDb.ExecuteNonQuery(@"
declare @idUser int = (select idMember from sec.Members where id = @userId)

insert into sec.UserGroups(idUser, idGroup)
select @idUser idUser, idMember idGroup from sec.Members where id in @groupsId
", new {userId, groupsId});
        }

        public void AddGroupsToUser(string[] groups, string user)
        {
            throw new NotImplementedException();
        }

        public Task AddGroupsToUserAsync(int[] idGroups, int idUser)
        {
            throw new NotImplementedException();
        }

        public Task AddGroupsToUserAsync(Guid[] groupsId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task AddGroupsToUserAsync(string[] groups, string user)
        {
            throw new NotImplementedException();
        }

        public void AddUsersToGroup(int[] idUsers, int idGroup)
        {
            throw new NotImplementedException();
        }

        public void AddUsersToGroup(Guid[] usersId, Guid groupId)
        {
            throw new NotImplementedException();
        }

        public void AddUsersToGroup(string[] users, string group)
        {
            throw new NotImplementedException();
        }

        public Task AddUsersToGroupAsync(int[] idUsers, int idGroup)
        {
            throw new NotImplementedException();
        }

        public Task AddUsersToGroupAsync(Guid[] usersId, Guid groupId)
        {
            throw new NotImplementedException();
        }

        public Task AddUsersToGroupAsync(string[] users, string group)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroupsById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroupsByIdUser(int idUser)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupsByIdUserAsync(int idUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroupsByUserName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupsByUserNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersByGroupName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersByGroupNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersByIdGroup(int idGroup)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersByIdGroupAsync(int idGroup)
        {
            throw new NotImplementedException();
        }
    }
}
