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
select @idUser idUser, idMember from (select idMember from sec.Groups where idMember in @idGroups) s1
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
        
        public IEnumerable<Group> GetGroups(Guid id)
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

        public Task<IEnumerable<Group>> GetGroupsAsync(Guid id)
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

        public IEnumerable<Group> GetGroups(int idUser)
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

        public Task<IEnumerable<Group>> GetGroupsAsync(int idUser)
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

        public IEnumerable<Group> GetGroups(string name)
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

        public Task<IEnumerable<Group>> GetGroupsAsync(string name)
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

        public IEnumerable<User> GetUsers(string name)
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

        public Task<IEnumerable<User>> GetUsersAsync(string name)
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

        public IEnumerable<User> GetUsers(Guid id)
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

        public Task<IEnumerable<User>> GetUsersAsync(Guid id)
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

        public IEnumerable<User> GetUsers(int idGroup)
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

        public Task<IEnumerable<User>> GetUsersAsync(int idGroup)
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

        public void RemoveUsersFromGroup(int[] idUsers, int idGroup)
        {
            _commonDb.ExecuteNonQuery("delete from sec.UserGroups where idGroup = @idGroup and idUser in @idUsers", new {idGroup, idUsers});
        }

        public void RemoveUsersFromGroup(Guid[] usersId, Guid groupId)
        {
            _commonDb.ExecuteNonQuery("delete from sec.UserGroups where idGroup = (select idMember from sec.GroupsView where id = @groupId) and idUser in (select idMember from sec.Members where id in @usersId)", new {groupId, usersId});
        }

        public void RemoveUsersFromGroup(string[] users, string group)
        {
            _commonDb.ExecuteNonQuery("delete from sec.UserGroups where idGroup = (select idMember from sec.GroupsView where name = @group) and idUser in (select idMember from sec.Members where name in @users)", new {users, group});
        }

        public void RemoveGroupsFromUser(int[] idGroups, int idUser)
        {
            _commonDb.ExecuteNonQuery("delete from sec.UserGroups where idUser = @idUser and idGroup in @idGroups", new {idUser, idGroups});
        }

        public void RemoveGroupsFromUser(Guid[] groupsId, Guid userId)
        {
            _commonDb.ExecuteNonQuery("delete from sec.UserGroups where idUser = (select idMember from sec.UsersView where id = @userId) and idGroup in (select idMember from sec.Members where id in @groupsId)");
        }

        public void RemoveGroupsFromUser(string[] groups, string user)
        {
            _commonDb.ExecuteNonQuery("delete from sec.UserGroups where idUser = (select idMember from sec.UsersView where login = @user) and idGroup in (select idMember from sec.Members where name in @groups)", new {user, groups});
        }

        public Task RemoveUsersFromGroupAsync(int[] idUsers, int idGroup)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.UserGroups where idGroup = @idGroup and idUser in @idUsers", new { idGroup, idUsers });
        }

        public Task RemoveUsersFromGroupAsync(Guid[] usersId, Guid groupId)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.UserGroups where idGroup = (select idMember from sec.GroupsView where id = @groupId) and idUser in (select idMember from sec.Members where id in @usersId)", new { groupId, usersId });
        }

        public Task RemoveUsersFromGroupAsync(string[] users, string group)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.UserGroups where idGroup = (select idMember from sec.GroupsView where name = @group) and idUser in (select idMember from sec.Members where name in @users)", new {users, group});
        }

        public Task RemoveGroupsFromUserAsync(int[] idGroups, int idUser)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.UserGroups where idUser = @idUser and idGroup in @idGroups", new { idUser, idGroups });
        }

        public Task RemoveGroupsFromUserAsync(Guid[] groupsId, Guid userId)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.UserGroups where idUser = (select idMember from sec.UsersView where id = @userId) and idGroup in (select idMember from sec.Members where id in @groupsId)", new{groupsId, userId});
        }

        public Task RemoveGroupsFromUserAsync(string[] groups, string user)
        {
            return _commonDb.ExecuteNonQueryAsync("delete from sec.UserGroups where idUser = (select idMember from sec.UsersView where login = @user) and idGroup in (select idMember from sec.Members where name in @groups)", new { user, groups });
        }

        public IEnumerable<User> GetNonIncludedUsers(string group)
        {
            return _commonDb.Query<User>("select * from sec.UsersView where idMember not in (select idUser from sec.UserGroups where idGroup = (select idMember from sec.Members where name = @group)) and 1 = (select 1 from sec.Members where name = @group)", new{group});
        }

        public IEnumerable<User> GetNonIncludedUsers(Guid groupId)
        {
            return _commonDb.Query<User>("select * from sec.UsersView where idMember not in (select idUser from sec.UserGroups where idGroup = (select idMember from sec.Members where id = @groupId)) and 1 = (select 1 from sec.Members where id = @groupdId)", new{groupId});
        }

        public IEnumerable<User> GetNonIncludedUsers(int idGroup)
        {
            return _commonDb.Query<User>("select * from sec.UsersView where idMember not in (select idUser from sec.UserGroups where idGroup = @idGroup) and 1 = (select 1 from sec.Members where idMember = @idGroup)", new {idGroup});
        }

        public IEnumerable<Group> GetNonIncludedGroups(string user)
        {
            return _commonDb.Query<Group>("select * from sec.GroupsView where idMember not in (select idGroup from sec.UserGroups where idUser = (select idMember from sec.Members where name = @user)) and 1 = (select 1 from sec.Members where name = @user)", new {user});
        }

        public IEnumerable<Group> GetNonIncludedGroups(Guid userId)
        {
            return _commonDb.Query<Group>("select * from sec.GroupsView where idMember not in (select idGroup from sec.UserGroups where idUser = (select idMember from sec.Members where id = @userId)) and 1 = (select 1 from sec.Members where id = @userId)", new {userId});
        }

        public IEnumerable<Group> GetNonIncludedGroups(int idUser)
        {
            return _commonDb.Query<Group>("select * from sec.GroupsView where idMember not in (select idGroup from sec.UserGroups where idUser = @idUser) and 1 = (select 1 from sec.Members where idMember = @idUser)", new {idUser});
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(string group)
        {
            return _commonDb.QueryAsync<User>("select * from sec.UsersView where idMember not in (select idUser from sec.UserGroups where idGroup = (select idMember from sec.Members where name = @group)) and 1 = (select 1 from sec.Members where name = @group)", new { group });
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(Guid groupId)
        {
            return _commonDb.QueryAsync<User>("select * from sec.UsersView where idMember not in (select idUser from sec.UserGroups where idGroup = (select idMember from sec.Members where id = @groupId)) and 1 = (select 1 from sec.Members where id = @groupId)", new { groupId });
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(int idGroup)
        {
            return _commonDb.QueryAsync<User>("select * from sec.UsersView where idMember not in (select idUser from sec.UserGroups where idGroup = @idGroup) and 1 = (select 1 from sec.Members where idMember = @idGroup)", new { idGroup });
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(string user)
        {
            return _commonDb.QueryAsync<Group>("select * from sec.GroupsView where idMember not in (select idGroup from sec.UserGroups where idUser = (select idMember from sec.Members where name = @user)) and 1 = (select 1 from sec.Members where name = @user)", new { user });
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(Guid userId)
        {
            return _commonDb.QueryAsync<Group>("select * from sec.GroupsView where idMember not in (select idGroup from sec.UserGroups where idUser = (select idMember from sec.Members where id = @userId)) and 1 = (select 1 from sec.Members where id = @userId)", new { userId });
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(int idUser)
        {
            return _commonDb.QueryAsync<Group>("select * from sec.GroupsView where idMember not in (select idGroup from sec.UserGroups where idUser = @idUser) and 1 = (select 1 from sec.Members where idMember = @idUser)", new { idUser });
        }
    }
}
