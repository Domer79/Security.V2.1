using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Управление пользователями и группами
    /// </summary>
    public interface IUserGroupRepository
    {
        /// <summary>
        /// Возвращает пользователей группы
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        IEnumerable<User> GetUsers(int idGroup);

        /// <summary>
        /// Возвращает пользователей группы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<User> GetUsers(Guid id);

        /// <summary>
        /// Возвращает пользователей группы
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<User> GetUsers(string name);

        /// <summary>
        /// Возвращает группы пользователя
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        IEnumerable<Group> GetGroups(int idUser);

        /// <summary>
        /// Возвращает группы пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Group> GetGroups(Guid id);

        /// <summary>
        /// Возвращает группы пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<Group> GetGroups(string name);

        /// <summary>
        /// Возвращает, не включенных в группу, пользователей
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        IEnumerable<User> GetNonIncludedUsers(string group);

        /// <summary>
        /// Возвращает, не включенных в группу, пользователей
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IEnumerable<User> GetNonIncludedUsers(Guid groupId);

        /// <summary>
        /// Возвращает, не включенных в группу, пользователей
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        IEnumerable<User> GetNonIncludedUsers(int idGroup);

        /// <summary>
        /// Возвращает, отсутствующие у пользователя, группы
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Group> GetNonIncludedGroups(string user);

        /// <summary>
        /// Возвращает, отсутствующие у пользователя, группы
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Group> GetNonIncludedGroups(Guid userId);

        /// <summary>
        /// Возвращает, отсутствующие у пользователя, группы
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        IEnumerable<Group> GetNonIncludedGroups(int idUser);

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="idUsers"></param>
        /// <param name="idGroup"></param>
        void AddUsersToGroup(int[] idUsers, int idGroup);

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="usersId"></param>
        /// <param name="groupId"></param>
        void AddUsersToGroup(Guid[] usersId, Guid groupId);

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="users"></param>
        /// <param name="group"></param>
        void AddUsersToGroup(string[] users, string group);

        /// <summary>
        /// Добавляет пользователя в несколько групп сразу
        /// </summary>
        /// <param name="idGroups"></param>
        /// <param name="idUser"></param>
        void AddGroupsToUser(int[] idGroups, int idUser);

        /// <summary>
        /// Добавляет пользователя в несколько групп сразу
        /// </summary>
        /// <param name="groupsId"></param>
        /// <param name="userId"></param>
        void AddGroupsToUser(Guid[] groupsId, Guid userId);

        /// <summary>
        /// Добавляет пользователя в несколько групп сразу
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        void AddGroupsToUser(string[] groups, string user);

        /// <summary>
        /// Производит удаление пользователей из группы
        /// </summary>
        /// <param name="idUsers"></param>
        /// <param name="idGroup"></param>
        void RemoveUsersFromGroup(int[] idUsers, int idGroup);

        /// <summary>
        /// Производит удаление пользователей из группы
        /// </summary>
        /// <param name="usersId"></param>
        /// <param name="groupId"></param>
        void RemoveUsersFromGroup(Guid[] usersId, Guid groupId);

        /// <summary>
        /// Производит удаление пользователей из группы
        /// </summary>
        /// <param name="users"></param>
        /// <param name="group"></param>
        void RemoveUsersFromGroup(string[] users, string group);
             
        /// <summary>
        /// Производит удаление пользователей из нескольких групп сразу
        /// </summary>
        /// <param name="idGroups"></param>
        /// <param name="idUser"></param>
        void RemoveGroupsFromUser(int[] idGroups, int idUser);

        /// <summary>
        /// Производит удаление пользователей из нескольких групп сразу
        /// </summary>
        /// <param name="groupsId"></param>
        /// <param name="userId"></param>
        void RemoveGroupsFromUser(Guid[] groupsId, Guid userId);

        /// <summary>
        /// Производит удаление пользователей из нескольких групп сразу
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        void RemoveGroupsFromUser(string[] groups, string user);

        #region Async

        /// <summary>
        /// Возвращает пользователей группы
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersAsync(int idGroup);

        /// <summary>
        /// Возвращает пользователей группы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersAsync(Guid id);

        /// <summary>
        /// Возвращает пользователей группы
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsersAsync(string name);

        /// <summary>
        /// Возвращает группы пользователя
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetGroupsAsync(int idUser);

        /// <summary>
        /// Возвращает группы пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetGroupsAsync(Guid id);

        /// <summary>
        /// Возвращает группы пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetGroupsAsync(string name);

        /// <summary>
        /// Возвращает, не включенных в группу, пользователей
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetNonIncludedUsersAsync(string group);

        /// <summary>
        /// Возвращает, не включенных в группу, пользователей
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetNonIncludedUsersAsync(Guid groupId);

        /// <summary>
        /// Возвращает, не включенных в группу, пользователей
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetNonIncludedUsersAsync(int idGroup);

        /// <summary>
        /// Возвращает, отсутствующие у пользователя, группы
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(string user);

        /// <summary>
        /// Возвращает, отсутствующие у пользователя, группы
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(Guid userId);

        /// <summary>
        /// Возвращает, отсутствующие у пользователя, группы
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetNonIncludedGroupsAsync(int idUser);

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="idUsers"></param>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        Task AddUsersToGroupAsync(int[] idUsers, int idGroup);

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="usersId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task AddUsersToGroupAsync(Guid[] usersId, Guid groupId);

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="users"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        Task AddUsersToGroupAsync(string[] users, string group);

        /// <summary>
        /// Добавляет пользователя в несколько групп сразу
        /// </summary>
        /// <param name="idGroups"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task AddGroupsToUserAsync(int[] idGroups, int idUser);

        /// <summary>
        /// Добавляет пользователя в несколько групп сразу
        /// </summary>
        /// <param name="groupsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AddGroupsToUserAsync(Guid[] groupsId, Guid userId);

        /// <summary>
        /// Добавляет пользователя в несколько групп сразу
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddGroupsToUserAsync(string[] groups, string user);

        /// <summary>
        /// Производит удаление пользователей из группы
        /// </summary>
        /// <param name="idUsers"></param>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        Task RemoveUsersFromGroupAsync(int[] idUsers, int idGroup);

        /// <summary>
        /// Производит удаление пользователей из группы
        /// </summary>
        /// <param name="usersId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task RemoveUsersFromGroupAsync(Guid[] usersId, Guid groupId);

        /// <summary>
        /// Производит удаление пользователей из группы
        /// </summary>
        /// <param name="users"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        Task RemoveUsersFromGroupAsync(string[] users, string group);

        /// <summary>
        /// Производит удаление пользователей из нескольких групп сразу
        /// </summary>
        /// <param name="idGroups"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task RemoveGroupsFromUserAsync(int[] idGroups, int idUser);

        /// <summary>
        /// Производит удаление пользователей из нескольких групп сразу
        /// </summary>
        /// <param name="groupsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RemoveGroupsFromUserAsync(Guid[] groupsId, Guid userId);

        /// <summary>
        /// Производит удаление пользователей из нескольких групп сразу
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task RemoveGroupsFromUserAsync(string[] groups, string user);

        #endregion
    }
}