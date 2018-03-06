using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.V2.Contracts.Repository
{
    public interface IUserGroupRepository
    {
        IEnumerable<User> GetUsersByIdGroup(int idGroup);
        IEnumerable<Group> GetGroupsByIdUser(int idUser);

        IEnumerable<User> GetUsersByGroupId(Guid id);
        IEnumerable<Group> GetGroupsByUserId(Guid id);

        IEnumerable<User> GetUsersByGroupName(string name);
        IEnumerable<Group> GetGroupsByUserName(string name);

        IEnumerable<User> GetNonIncludedUsers(string group);
        IEnumerable<User> GetNonIncludedUsers(Guid groupId);
        IEnumerable<User> GetNonIncludedUsers(int idGroup);

        IEnumerable<Group> GetNonIncludedGroups(string user);
        IEnumerable<Group> GetNonIncludedGroups(Guid userId);
        IEnumerable<Group> GetNonIncludedGroups(int idUser);

        void AddUsersToGroup(int[] idUsers, int idGroup);
        void AddUsersToGroup(Guid[] usersId, Guid groupId);
        void AddUsersToGroup(string[] users, string group);

        void AddGroupsToUser(int[] idGroups, int idUser);
        void AddGroupsToUser(Guid[] groupsId, Guid userId);
        void AddGroupsToUser(string[] groups, string user);

        void RemoveUsersFromGroup(int[] idUsers, int idGroup);
        void RemoveUsersFromGroup(Guid[] usersId, Guid groupId);
        void RemoveUsersFromGroup(string[] users, string group);
             
        void RemoveGroupsFromUser(int[] idGroups, int idUser);
        void RemoveGroupsFromUser(Guid[] groupsId, Guid userId);
        void RemoveGroupsFromUser(string[] groups, string user);

        #region Async

        Task<IEnumerable<User>> GetUsersByIdGroupAsync(int idGroup);
        Task<IEnumerable<Group>> GetGroupsByIdUserAsync(int idUser);

        Task<IEnumerable<User>> GetUsersByGroupIdAsync(Guid id);
        Task<IEnumerable<Group>> GetGroupsByUserIdAsync(Guid id);
        
        Task<IEnumerable<User>> GetUsersByGroupNameAsync(string name);
        Task<IEnumerable<Group>> GetGroupsByUserNameAsync(string name);

        Task<IEnumerable<User>> GetNonIncludedUsersAsync(string group);
        Task<IEnumerable<User>> GetNonIncludedUsersAsync(Guid groupId);
        Task<IEnumerable<User>> GetNonIncludedUsersAsync(int idGroup);
        
        Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(string user);
        Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(Guid userId);
        Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(int idUser);

        Task AddUsersToGroupAsync(int[] idUsers, int idGroup);
        Task AddUsersToGroupAsync(Guid[] usersId, Guid groupId);
        Task AddUsersToGroupAsync(string[] users, string group);
        
        Task AddGroupsToUserAsync(int[] idGroups, int idUser);
        Task AddGroupsToUserAsync(Guid[] groupsId, Guid userId);
        Task AddGroupsToUserAsync(string[] groups, string user);

        Task RemoveUsersFromGroupAsync(int[] idUsers, int idGroup);
        Task RemoveUsersFromGroupAsync(Guid[] usersId, Guid groupId);
        Task RemoveUsersFromGroupAsync(string[] users, string group);
                             
        Task RemoveGroupsFromUserAsync(int[] idGroups, int idUser);
        Task RemoveGroupsFromUserAsync(Guid[] groupsId, Guid userId);
        Task RemoveGroupsFromUserAsync(string[] groups, string user);

        #endregion
    }
}