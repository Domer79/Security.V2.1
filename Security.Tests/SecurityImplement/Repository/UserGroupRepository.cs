using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    public class UserGroupRepository : IUserGroupRepository
    {
        public void AddGroupsToUser(int[] idGroups, int idUser)
        {
            var user = Database.Users.First(_ => _.IdMember == idUser);
            foreach (var idGroup in idGroups)
            {
                var group = Database.Groups.Single(_ => _.IdMember == idGroup);

                Database.UserGroups.Add(user, group);
            }
        }

        public void AddGroupsToUser(Guid[] groupsId, Guid userId)
        {
            var user = Database.Users.First(_ => _.Id == userId);
            foreach (var groupId in groupsId)
            {
                var group = Database.Groups.Single(_ => _.Id == groupId);

                Database.UserGroups.Add(user, group);
            }
        }

        public void AddGroupsToUser(string[] groups, string user)
        {
            var userEntity = Database.Users.First(_ => _.Name == user);
            foreach (var groupName in groups)
            {
                var group = Database.Groups.Single(_ => _.Name == groupName);
                Database.UserGroups.Add(userEntity, group);
            }
        }

        public void AddUsersToGroup(int[] idUsers, int idGroup)
        {
            var group = Database.Groups.First(_ => _.IdMember == idGroup);
            foreach (var idUser in idUsers)
            {
                var user = Database.Users.Single(_ => _.IdMember == idUser);
                Database.UserGroups.Add(user, group);
            }
        }

        public void AddUsersToGroup(Guid[] usersId, Guid groupId)
        {
            var group = Database.Groups.First(_ => _.Id == groupId);
            foreach (var userId in usersId)
            {
                var user = Database.Users.Single(_ => _.Id == userId);
                Database.UserGroups.Add(user, group);
            }
        }

        public void AddUsersToGroup(string[] users, string group)
        {
            var groupEntity = Database.Groups.First(_ => _.Name == group);
            foreach (var userName in users)
            {
                var user = Database.Users.Single(_ => _.Name == userName);
                Database.UserGroups.Add(user, groupEntity);
            }
        }

        public IEnumerable<User> GetUsersByIdGroup(int idGroup)
        {
            var group = Database.Groups.Single(_ => _.IdMember == idGroup);
            return Database.UserGroups.GetGroupUsers(group);
        }

        public IEnumerable<User> GetUsersByGroupName(string name)
        {
            var group = Database.Groups.Single(_ => _.Name == name);
            return Database.UserGroups.GetGroupUsers(group);
        }

        public IEnumerable<User> GetUsersByGroupId(Guid id)
        {
            var group = Database.Groups.Single(_ => _.Id == id);
            return Database.UserGroups.GetGroupUsers(group);
        }

        public IEnumerable<Group> GetGroupsByIdUser(int idUser)
        {
            var user = Database.Users.Single(_ => _.IdMember == idUser);
            return Database.UserGroups.GetUserGroups(user);
        }

        public IEnumerable<Group> GetGroupsByUserName(string name)
        {
            var user = Database.Users.Single(_ => _.Name == name);
            return Database.UserGroups.GetUserGroups(user);
        }

        public IEnumerable<Group> GetGroupsByIdUser(Guid id)
        {
            var user = Database.Users.Single(_ => _.Id == id);
            return Database.UserGroups.GetUserGroups(user);
        }

        public IEnumerable<Group> GetGroupsByUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersByIdGroupAsync(int idGroup)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupsByIdUserAsync(int idUser)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersByGroupIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupsByUserIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersByGroupNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupsByUserNameAsync(string name)
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

        public void RemoveUsersFromGroup(int[] idUsers, int idGroup)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersFromGroup(Guid[] usersId, Guid groupId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersFromGroup(string[] users, string group)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroupsFromUser(int[] idGroups, int idUser)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroupsFromUser(Guid[] groupsId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroupsFromUser(string[] groups, string user)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersFromGroupAsync(int[] idUsers, int idGroup)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersFromGroupAsync(Guid[] usersId, Guid groupId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersFromGroupAsync(string[] users, string group)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroupsFromUserAsync(int[] idGroups, int idUser)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroupsFromUserAsync(Guid[] groupsId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroupsFromUserAsync(string[] groups, string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetNonIncludedUsers(string group)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetNonIncludedUsers(Guid groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetNonIncludedUsers(int idGroup)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetNotIncludedGroups(string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetNotIncludedGroups(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetNotIncludedGroups(int idUser)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(string group)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(Guid groupId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetNonIncludedUsersAsync(int idGroup)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetNotIncludedGroupsAsync(string user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetNotIncludedGroupsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetNotIncludedGroupsAsync(int idUser)
        {
            throw new NotImplementedException();
        }

        Task IUserGroupRepository.RemoveUsersFromGroupAsync(int[] idUsers, int idGroup)
        {
            throw new NotImplementedException();
        }

        Task IUserGroupRepository.RemoveUsersFromGroupAsync(Guid[] usersId, Guid groupId)
        {
            throw new NotImplementedException();
        }

        Task IUserGroupRepository.RemoveUsersFromGroupAsync(string[] users, string group)
        {
            throw new NotImplementedException();
        }

        Task IUserGroupRepository.RemoveGroupsFromUserAsync(int[] idGroups, int idUser)
        {
            throw new NotImplementedException();
        }

        Task IUserGroupRepository.RemoveGroupsFromUserAsync(Guid[] groupsId, Guid userId)
        {
            throw new NotImplementedException();
        }

        Task IUserGroupRepository.RemoveGroupsFromUserAsync(string[] groups, string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetNonIncludedGroups(string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetNonIncludedGroups(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetNonIncludedGroups(int idUser)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(string user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(int idUser)
        {
            throw new NotImplementedException();
        }
    }
}