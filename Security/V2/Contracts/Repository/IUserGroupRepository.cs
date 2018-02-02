using System;
using System.Collections.Generic;
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
    }
}