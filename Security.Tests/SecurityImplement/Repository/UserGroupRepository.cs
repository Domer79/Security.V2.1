using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}