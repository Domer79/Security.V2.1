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

        IEnumerable<User> GetUsersById(Guid id);
        IEnumerable<Group> GetGroupsById(Guid id);

        IEnumerable<User> GetUsersByGroupName(string name);
        IEnumerable<Group> GetGroupsByUserName(string name);

        void AddUsersToGroup(int[] idUsers, int idGroup);
        void AddUsersToGroup(Guid[] usersId, Guid groupId);
        void AddUsersToGroup(string[] users, string group);

        void AddGroupsToUser(int[] idGroups, int idUser);
        void AddGroupsToUser(Guid[] groupsId, Guid userId);
        void AddGroupsToUser(string[] groups, string user);

        #region Async

        Task<IEnumerable<User>> GetUsersByIdGroupAsync(int idGroup);
        Task<IEnumerable<Group>> GetGroupsByIdUserAsync(int idUser);

        Task<IEnumerable<User>> GetUsersByIdAsync(Guid id);
        Task<IEnumerable<Group>> GetGroupsByIdAsync(Guid id);
        
        Task<IEnumerable<User>> GetUsersByGroupNameAsync(string name);
        Task<IEnumerable<Group>> GetGroupsByUserNameAsync(string name);
        
        Task AddUsersToGroupAsync(int[] idUsers, int idGroup);
        Task AddUsersToGroupAsync(Guid[] usersId, Guid groupId);
        Task AddUsersToGroupAsync(string[] users, string group);
        
        Task AddGroupsToUserAsync(int[] idGroups, int idUser);
        Task AddGroupsToUserAsync(Guid[] groupsId, Guid userId);
        Task AddGroupsToUserAsync(string[] groups, string user);
        

        #endregion    }
}