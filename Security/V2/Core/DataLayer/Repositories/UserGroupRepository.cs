using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.DataLayer.Repositories
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
            _commonDb.ExecuteNonQuery(@"
declare @idUser int = (select idMember from sec.Members where name = @user)

insert into sec.UserGroups(idUser, idGroup)
select @idUser idUser, idMember idGroup from sec.Members where name in @groups
", new {user, groups});
        }

        public Task AddGroupsToUserAsync(int[] idGroups, int idUser)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into sec.UserGroups(idUser, idGroup)
select @idUser idUser, idMember from (select idMember from sec.Groups where idGroup in @idGroups) s1
", new { idUser, idGroups });
        }

        public Task AddGroupsToUserAsync(Guid[] groupsId, Guid userId)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idUser int = (select idMember from sec.Members where id = @userId)

insert into sec.UserGroups(idUser, idGroup)
select @idUser idUser, idMember idGroup from sec.Members where id in @groupsId
", new { userId, groupsId });
        }

        public Task AddGroupsToUserAsync(string[] groups, string user)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idUser int = (select idMember from sec.Members where name = @user)

insert into sec.UserGroups(idUser, idGroup)
select @idUser idUser, idMember idGroup from sec.Members where name in @groups
", new { user, groups });
        }

        public void AddUsersToGroup(int[] idUsers, int idGroup)
        {
            _commonDb.ExecuteNonQuery(@"
insert into sec.UserGroups(idUser, idGroup)
select idMember idUser, @idGroup idGroup from (select idMember from sec.Members where idMember in @idUsers) s1
", new {idUsers, idGroup});
        }

        public void AddUsersToGroup(Guid[] usersId, Guid groupId)
        {
            _commonDb.ExecuteNonQuery(@"
declare @idGroup int = (select idMember from sec.Members where id = @groupId)

insert into sec.UserGroups(idUser, idGroup)
select idMember idUser, @idGroup idGroup from sec.Members where id in @usersId
", new {usersId, groupId});
        }

        public void AddUsersToGroup(string[] users, string group)
        {
            _commonDb.ExecuteNonQuery(@"
declare @idGroup int = (select idMember from sec.Members where name = @group)

insert into sec.UserGroups(idUser, idGroup)
select idMember idUser, @idGroup idGroup from sec.Members where name in @users
", new {users, group});
        }

        public Task AddUsersToGroupAsync(int[] idUsers, int idGroup)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
insert into sec.UserGroups(idUser, idGroup)
select idMember idUser, @idGroup idGroup from (select idMember from sec.Members where idMember in @idUsers) s1
", new { idUsers, idGroup });
        }

        public Task AddUsersToGroupAsync(Guid[] usersId, Guid groupId)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idGroup int = (select idMember from sec.Members where id = @groupId)

insert into sec.UserGroups(idUser, idGroup)
select idMember idUser, @idGroup idGroup from sec.Members where id in @usersId
", new { usersId, groupId });
        }

        public Task AddUsersToGroupAsync(string[] users, string group)
        {
            return _commonDb.ExecuteNonQueryAsync(@"
declare @idGroup int = (select idMember from sec.Members where name = @group)

insert into sec.UserGroups(idUser, idGroup)
select idMember idUser, @idGroup idGroup from sec.Members where name in @users
", new { users, group });
        }
        
        public IEnumerable<Group> GetGroupsByUserId(Guid id)
        {
            return _commonDb.Query<Group>(@"
select
	gv.*
from
	sec.GroupsView gv inner join sec.UserGroups ug on gv.idMember = ug.idGroup
	inner join sec.Members m on ug.idUser = m.idMember
where
	m.id = @id
", new {id});
        }

        public Task<IEnumerable<Group>> GetGroupsByUserIdAsync(Guid id)
        {
            return _commonDb.QueryAsync<Group>(@"
select
	gv.*
from
	sec.GroupsView gv inner join sec.UserGroups ug on gv.idMember = ug.idGroup
	inner join sec.Members m on ug.idUser = m.idMember
where
	m.id = @id
", new { id });
        }

        public IEnumerable<Group> GetGroupsByIdUser(int idUser)
        {
            return _commonDb.Query<Group>(@"
select
	gv.*
from
	sec.GroupsView gv inner join sec.UserGroups ug on gv.idMember = ug.idGroup
	inner join sec.Members m on ug.idUser = m.idMember
where
	m.idMember = @idUser
", new { idUser });
        }

        public Task<IEnumerable<Group>> GetGroupsByIdUserAsync(int idUser)
        {
            return _commonDb.QueryAsync<Group>(@"
select
	gv.*
from
	sec.GroupsView gv inner join sec.UserGroups ug on gv.idMember = ug.idGroup
	inner join sec.Members m on ug.idUser = m.idMember
where
	m.idMember = @idUser
", new { idUser });
        }

        public IEnumerable<Group> GetGroupsByUserName(string name)
        {
            return _commonDb.Query<Group>(@"
select
	gv.*
from
	sec.GroupsView gv inner join sec.UserGroups ug on gv.idMember = ug.idGroup
	inner join sec.Members m on ug.idUser = m.idMember
where
	m.name = @name
", new { name });
        }

        public Task<IEnumerable<Group>> GetGroupsByUserNameAsync(string name)
        {
            return _commonDb.QueryAsync<Group>(@"
select
	gv.*
from
	sec.GroupsView gv inner join sec.UserGroups ug on gv.idMember = ug.idGroup
	inner join sec.Members m on ug.idUser = m.idMember
where
	m.name = @name
", new { name });
        }

        public IEnumerable<User> GetUsersByGroupName(string name)
        {
            return _commonDb.Query<User>(@"
select
	uv.*
from
	sec.UsersView uv inner join sec.UserGroups ug on uv.idMember = ug.idUser
	inner join sec.Members m on ug.idGroup = m.idMember
where
	m.name = @name", new {name});
        }

        public Task<IEnumerable<User>> GetUsersByGroupNameAsync(string name)
        {
            return _commonDb.QueryAsync<User>(@"
select
	uv.*
from
	sec.UsersView uv inner join sec.UserGroups ug on uv.idMember = ug.idUser
	inner join sec.Members m on ug.idGroup = m.idMember
where
	m.name = @name", new { name });
        }

        public IEnumerable<User> GetUsersByGroupId(Guid id)
        {
            return _commonDb.Query<User>(@"
select
	uv.*
from
	sec.UsersView uv inner join sec.UserGroups ug on uv.idMember = ug.idUser
	inner join sec.Members m on ug.idGroup = m.idMember
where
	m.id = @id", new { id });
        }

        public Task<IEnumerable<User>> GetUsersByGroupIdAsync(Guid id)
        {
            return _commonDb.QueryAsync<User>(@"
select
	uv.*
from
	sec.UsersView uv inner join sec.UserGroups ug on uv.idMember = ug.idUser
	inner join sec.Members m on ug.idGroup = m.idMember
where
	m.id = @id", new { id });
        }

        public IEnumerable<User> GetUsersByIdGroup(int idGroup)
        {
            return _commonDb.Query<User>(@"
select
	uv.*
from
	sec.UsersView uv inner join sec.UserGroups ug on uv.idMember = ug.idUser
	inner join sec.Members m on ug.idGroup = m.idMember
where
	m.idMember = @idGroup", new { idGroup });
        }

        public Task<IEnumerable<User>> GetUsersByIdGroupAsync(int idGroup)
        {
            return _commonDb.QueryAsync<User>(@"
select
	uv.*
from
	sec.UsersView uv inner join sec.UserGroups ug on uv.idMember = ug.idUser
	inner join sec.Members m on ug.idGroup = m.idMember
where
	m.idMember = @idGroup", new { idGroup });
        }
    }
}
