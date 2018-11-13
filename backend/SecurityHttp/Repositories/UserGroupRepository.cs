using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly string _url;

        public UserGroupRepository(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
            _url = "api/usergroups";
        }

        public void AddGroupsToUser(int[] idGroups, int idUser)
        {
            _commonWeb.Put($"{_url}", idGroups, new {idUser});
        }

        public void AddGroupsToUser(Guid[] groupsId, Guid userId)
        {
            _commonWeb.Put($"{_url}", groupsId, new { userId });
        }

        public void AddGroupsToUser(string[] groups, string user)
        {
            _commonWeb.Put($"{_url}", groups, new { user });
        }

        public Task AddGroupsToUserAsync(int[] idGroups, int idUser)
        {
            return _commonWeb.PutAsync($"{_url}", idGroups, new { idUser });
        }

        public Task AddGroupsToUserAsync(Guid[] groupsId, Guid userId)
        {
            return _commonWeb.PutAsync($"{_url}", groupsId, new { userId });
        }

        public Task AddGroupsToUserAsync(string[] groups, string user)
        {
            return _commonWeb.PutAsync($"{_url}", groups, new { user });
        }

        public void AddUsersToGroup(int[] idUsers, int idGroup)
        {
            _commonWeb.Put($"{_url}", idUsers, new { idGroup });
        }

        public void AddUsersToGroup(Guid[] usersId, Guid groupId)
        {
            _commonWeb.Put($"{_url}", usersId, new { groupId });
        }

        public void AddUsersToGroup(string[] users, string group)
        {
            _commonWeb.Put($"{_url}", users, new { group });
        }

        public Task AddUsersToGroupAsync(int[] idUsers, int idGroup)
        {
            return _commonWeb.PutAsync($"{_url}", idUsers, new { idGroup });
        }

        public Task AddUsersToGroupAsync(Guid[] usersId, Guid groupId)
        {
            return _commonWeb.PutAsync($"{_url}", usersId, new { groupId });
        }

        public Task AddUsersToGroupAsync(string[] users, string group)
        {
            return _commonWeb.PutAsync($"{_url}", users, new { group });
        }

        public IEnumerable<Group> GetGroups(int idUser)
        {
            return _commonWeb.GetCollection<Group>($"{_url}", new {idUser});
        }

        public Task<IEnumerable<Group>> GetGroupsAsync(int idUser)
        {
            return _commonWeb.GetCollectionAsync<Group>($"{_url}", new { idUser });
        }

        public IEnumerable<Group> GetGroups(Guid id)
        {
            return _commonWeb.GetCollection<Group>($"{_url}", new { userId = id });
        }

        public Task<IEnumerable<Group>> GetGroupsAsync(Guid id)
        {
            return _commonWeb.GetCollectionAsync<Group>($"{_url}", new { userId = id });
        }

        public IEnumerable<Group> GetGroups(string name)
        {
            return _commonWeb.GetCollection<Group>($"{_url}", new { userName = name });
        }

        public Task<IEnumerable<Group>> GetGroupsAsync(string name)
        {
            return _commonWeb.GetCollectionAsync<Group>($"{_url}", new { userName = name });
        }

        public IEnumerable<Group> GetNonIncludedGroups(string user)
        {
            return _commonWeb.GetCollection<Group>($"{_url}/exceptfor", new{user});
        }

        public IEnumerable<Group> GetNonIncludedGroups(Guid userId)
        {
            return _commonWeb.GetCollection<Group>($"{_url}/exceptfor", new { userId });
        }

        public IEnumerable<Group> GetNonIncludedGroups(int idUser)
        {
            return _commonWeb.GetCollection<Group>($"{_url}/exceptfor", new { idUser });
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(string user)
        {
            return _commonWeb.GetCollectionAsync<Group>($"{_url}/exceptfor", new { user });
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(Guid userId)
        {
            return _commonWeb.GetCollectionAsync<Group>($"{_url}/exceptfor", new { userId });
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(int idUser)
        {
            return _commonWeb.GetCollectionAsync<Group>($"{_url}/exceptfor", new { idUser });
        }

        public IEnumerable<User> GetNonIncludedUsers(string group)
        {
            return _commonWeb.GetCollection<User>($"{_url}/exceptfor", new { group });
        }

        public IEnumerable<User> GetNonIncludedUsers(Guid groupId)
        {
            return _commonWeb.GetCollection<User>($"{_url}/exceptfor", new { groupId });
        }

        public IEnumerable<User> GetNonIncludedUsers(int idGroup)
        {
            return _commonWeb.GetCollection<User>($"{_url}/exceptfor", new { idGroup });
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(string group)
        {
            return _commonWeb.GetCollectionAsync<User>($"{_url}/exceptfor", new { group });
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(Guid groupId)
        {
            return _commonWeb.GetCollectionAsync<User>($"{_url}/exceptfor", new { groupId });
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(int idGroup)
        {
            return _commonWeb.GetCollectionAsync<User>($"{_url}/exceptfor", new { idGroup });
        }

        public IEnumerable<User> GetUsers(Guid id)
        {
            return _commonWeb.GetCollection<User>($"{_url}", new {groupId = id});
        }

        public Task<IEnumerable<User>> GetUsersAsync(Guid id)
        {
            return _commonWeb.GetCollectionAsync<User>($"{_url}", new { groupId = id });
        }

        public IEnumerable<User> GetUsers(string name)
        {
            return _commonWeb.GetCollection<User>($"{_url}", new { groupName = name });
        }

        public Task<IEnumerable<User>> GetUsersAsync(string name)
        {
            return _commonWeb.GetCollectionAsync<User>($"{_url}", new { groupName = name });
        }

        public IEnumerable<User> GetUsers(int idGroup)
        {
            return _commonWeb.GetCollection<User>($"{_url}", new { idGroup });
        }

        public Task<IEnumerable<User>> GetUsersAsync(int idGroup)
        {
            return _commonWeb.GetCollectionAsync<User>($"{_url}", new { idGroup });
        }

        public void RemoveGroupsFromUser(int[] idGroups, int idUser)
        {
            _commonWeb.Delete($"{_url}", idGroups, new {idUser});
        }

        public void RemoveGroupsFromUser(Guid[] groupsId, Guid userId)
        {
            _commonWeb.Delete($"{_url}", groupsId, new { userId });
        }

        public void RemoveGroupsFromUser(string[] groups, string user)
        {
            _commonWeb.Delete($"{_url}", groups, new { user });
        }

        public Task RemoveGroupsFromUserAsync(int[] idGroups, int idUser)
        {
            return _commonWeb.DeleteAsync($"{_url}", idGroups, new { idUser });
        }

        public Task RemoveGroupsFromUserAsync(Guid[] groupsId, Guid userId)
        {
            return _commonWeb.DeleteAsync($"{_url}", groupsId, new { userId });
        }

        public Task RemoveGroupsFromUserAsync(string[] groups, string user)
        {
            return _commonWeb.DeleteAsync($"{_url}", groups, new { user });
        }

        public void RemoveUsersFromGroup(int[] idUsers, int idGroup)
        {
            _commonWeb.Delete($"{_url}", idUsers, new {idGroup});
        }

        public void RemoveUsersFromGroup(Guid[] usersId, Guid groupId)
        {
            _commonWeb.Delete($"{_url}", usersId, new { groupId });
        }

        public void RemoveUsersFromGroup(string[] users, string group)
        {
            _commonWeb.Delete($"{_url}", users, new { group });
        }

        public Task RemoveUsersFromGroupAsync(int[] idUsers, int idGroup)
        {
            return _commonWeb.DeleteAsync($"{_url}", idUsers, new { idGroup });
        }

        public Task RemoveUsersFromGroupAsync(Guid[] usersId, Guid groupId)
        {
            return _commonWeb.DeleteAsync($"{_url}", usersId, new { groupId });
        }

        public Task RemoveUsersFromGroupAsync(string[] users, string group)
        {
            return _commonWeb.DeleteAsync($"{_url}", users, new { group });
        }
    }
}
