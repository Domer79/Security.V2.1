using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Contracts.Repository;
using Security.Model;
using Security.WebApi.Models;

namespace Security.WebApi.Controllers
{
    /// <summary>
    /// Управление группами пользователя
    /// </summary>
    [RoutePrefix("api/usergroups")]
    public class UserGroupsController : ApiController
    {
        private readonly IUserGroupRepository _repo;

        /// <summary>
        /// Управление группами пользователя
        /// </summary>
        /// <param name="repo"></param>
        public UserGroupsController(IUserGroupRepository repo)
        {
            _repo = repo;
        }
        
        /// <summary>
        /// Возвращает список пользователей группы по ее имени
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<User>))]
        [HttpGet]
        public async Task<IHttpActionResult> GetUsersByGroupName(string groupName)
        {
            var users = await _repo.GetUsersAsync(groupName);
            return Ok(users);
        }

        /// <summary>
        /// Возвращает список пользователей группы по ее идентификатору <see cref="Guid"/>
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<User>))]
        public async Task<IHttpActionResult> GetUsersByGroupId(Guid groupId)
        {
            var users = await _repo.GetUsersAsync(groupId);
            return Ok(users);
        }

        /// <summary>
        /// Возвращает список пользователей группы по ее идентификатору
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<User>))]
        public async Task<IHttpActionResult> GetUsersByIdGroup(int idGroup)
        {
            var users = await _repo.GetUsersAsync(idGroup);
            return Ok(users);
        }

        /// <summary>
        /// Возвращает список групп пользователя по его логину
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Group>))]
        public async Task<IHttpActionResult> GetGroupsByUserName(string userName)
        {
            var groups = await _repo.GetGroupsAsync(userName);
            return Ok(groups);
        }

        /// <summary>
        /// Возвращает список групп пользователя по его идентификатору <see cref="Guid"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Group>))]
        public async Task<IHttpActionResult> GetGroupsByUserId(Guid userId)
        {
            var groups = await _repo.GetGroupsAsync(userId);
            return Ok(groups);
        }

        /// <summary>
        /// Возвращает список групп пользователя по его идентификатору
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Group>))]
        public async Task<IHttpActionResult> GetGroupsByIdUser(int idUser)
        {
            var groups = await _repo.GetGroupsAsync(idUser);
            return Ok(groups);
        }

        /// <summary>
        /// Возвращает список пользователей, отсутствующих в группе
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("exceptfor")]
        [ResponseType(typeof(IEnumerable<User>))]
        public async Task<IHttpActionResult> GetNotIncludedUsers(string group)
        {
            var users = await _repo.GetNonIncludedUsersAsync(group);
            return Ok(users);
        }

        /// <summary>
        /// Возвращает список пользователей, отсутствующих в группе
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("exceptfor")]
        [ResponseType(typeof(IEnumerable<User>))]
        public async Task<IHttpActionResult> GetNotIncludedUsers(Guid groupId)
        {
            var users = await _repo.GetNonIncludedUsersAsync(groupId);
            return Ok(users);
        }

        /// <summary>
        /// Возвращает список пользователей, отсутствующих в группе
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("exceptfor")]
        [ResponseType(typeof(IEnumerable<User>))]
        public async Task<IHttpActionResult> GetNotIncludedUsers(int idGroup)
        {
            var users = await _repo.GetNonIncludedUsersAsync(idGroup);
            return Ok(users);
        }

        /// <summary>
        /// Возвращает список групп, отсутствующих у пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("exceptfor")]
        [ResponseType(typeof(IEnumerable<Group>))]
        public async Task<IHttpActionResult> GetNotIncludedGroups(string user)
        {
            var groups = await _repo.GetNonIncludedGroupsAsync(user);
            return Ok(groups);
        }

        /// <summary>
        /// Возвращает список групп, отсутствующих у пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("exceptfor")]
        [ResponseType(typeof(IEnumerable<Group>))]
        public async Task<IHttpActionResult> GetNotIncludedGroups(Guid userId)
        {
            var groups = await _repo.GetNonIncludedGroupsAsync(userId);
            return Ok(groups);
        }

        /// <summary>
        /// Возвращает список групп, отсутствующих у пользователя
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("exceptfor")]
        [ResponseType(typeof(IEnumerable<Group>))]
        public async Task<IHttpActionResult> GetNotIncludedGroups(int idUser)
        {
            var groups = await _repo.GetNonIncludedGroupsAsync(idUser);
            return Ok(groups);
        }

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="group"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddUsersToGroup(string group, [FromBody] string[] users)
        {
            await _repo.AddUsersToGroupAsync(users, group);
            return Ok();
        }

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddUsersToGroup(Guid groupId, [FromBody] Guid[] users)
        {
            await _repo.AddUsersToGroupAsync(users, groupId);
            return Ok();
        }

        /// <summary>
        /// Добавляет пользователей в группу
        /// </summary>
        /// <param name="idGroup"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddUsersToGroup(int idGroup, [FromBody] int[] users)
        {
            await _repo.AddUsersToGroupAsync(users, idGroup);
            return Ok();
        }

        /// <summary>
        /// Добавляет пользователя сразу в несколько групп
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddGroupsToUser(string user, [FromBody] string[] groups)
        {
            await _repo.AddGroupsToUserAsync(groups, user);
            return Ok();
        }

        /// <summary>
        /// Добавляет пользователя сразу в несколько групп
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddGroupsToUser(Guid userId, [FromBody] Guid[] groups)
        {
            await _repo.AddGroupsToUserAsync(groups, userId);
            return Ok();
        }

        /// <summary>
        /// Добавляет пользователя сразу в несколько групп
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> AddGroupsToUser(int idUser, [FromBody] int[] groups)
        {
            await _repo.AddGroupsToUserAsync(groups, idUser);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователей из группы
        /// </summary>
        /// <param name="idGroup"></param>
        /// <param name="idUsers"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveUsersFromGroup(int idGroup, [FromBody]int[] idUsers)
        {
            await _repo.RemoveUsersFromGroupAsync(idUsers, idGroup);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователей из группы
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="usersId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveUsersFromGroup(Guid groupId, [FromBody]Guid[] usersId)
        {
            await _repo.RemoveUsersFromGroupAsync(usersId, groupId);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователей из группы
        /// </summary>
        /// <param name="group"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveUsersFromGroup(string group, [FromBody]string[] users)
        {
            await _repo.RemoveUsersFromGroupAsync(users, group);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя сразу из несколько групп
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idGroups"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGroupsFromUser(int idUser, [FromBody]int[] idGroups)
        {
            await _repo.RemoveGroupsFromUserAsync(idGroups, idUser);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя сразу из несколько групп
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupsId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGroupsFromUser(Guid userId, [FromBody]Guid[] groupsId)
        {
            await _repo.RemoveGroupsFromUserAsync(groupsId, userId);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя сразу из несколько групп
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGroupsFromUser(string user, [FromBody]string[] groups)
        {
            await _repo.RemoveGroupsFromUserAsync(groups, user);
            return Ok();
        }
    }
}
