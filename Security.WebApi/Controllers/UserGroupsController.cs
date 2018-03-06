using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Model;
using Security.V2.Contracts.Repository;
using Security.WebApi.Models;

namespace Security.WebApi.Controllers
{
    [RoutePrefix("api/usergroups")]
    public class UserGroupsController : ApiController
    {
        private readonly IUserGroupRepository _repo;

        public UserGroupsController(IUserGroupRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetUsersByGroupName(string groupName)
        {
            var users = await _repo.GetUsersByGroupNameAsync(groupName);
            return Ok(users);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUsersByGroupId(Guid groupId)
        {
            var users = await _repo.GetUsersByGroupIdAsync(groupId);
            return Ok(users);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUsersByIdGroup(int idGroup)
        {
            var users = await _repo.GetUsersByIdGroupAsync(idGroup);
            return Ok(users);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGroupsByUserName(string userName)
        {
            var groups = await _repo.GetGroupsByUserNameAsync(userName);
            return Ok(groups);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGroupsByUserId(Guid userId)
        {
            var groups = await _repo.GetGroupsByUserIdAsync(userId);
            return Ok(groups);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGroupsByIdUser(int idUser)
        {
            var groups = await _repo.GetGroupsByIdUserAsync(idUser);
            return Ok(groups);
        }

        [HttpGet]
        [Route("exceptfor")]
        public async Task<IHttpActionResult> GetNotIncludedUsers(string group)
        {
            var users = await _repo.GetNonIncludedUsersAsync(group);
            return Ok(users);
        }

        [HttpGet]
        [Route("exceptfor")]
        public async Task<IHttpActionResult> GetNotIncludedUsers(Guid groupId)
        {
            var users = await _repo.GetNonIncludedUsersAsync(groupId);
            return Ok(users);
        }

        [HttpGet]
        [Route("exceptfor")]
        public async Task<IHttpActionResult> GetNotIncludedUsers(int idGroup)
        {
            var users = await _repo.GetNonIncludedUsersAsync(idGroup);
            return Ok(users);
        }

        [HttpGet]
        [Route("exceptfor")]
        public async Task<IHttpActionResult> GetNotIncludedGroups(string user)
        {
            var groups = await _repo.GetNonIncludedGroupsAsync(user);
            return Ok(groups);
        }

        [HttpGet]
        [Route("exceptfor")]
        public async Task<IHttpActionResult> GetNotIncludedGroups(Guid userId)
        {
            var groups = await _repo.GetNonIncludedGroupsAsync(userId);
            return Ok(groups);
        }

        [HttpGet]
        [Route("exceptfor")]
        public async Task<IHttpActionResult> GetNotIncludedGroups(int idUser)
        {
            var groups = await _repo.GetNonIncludedGroupsAsync(idUser);
            return Ok(groups);
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddUsersToGroup(string group, [FromBody] string[] users)
        {
            await _repo.AddUsersToGroupAsync(users, group);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddUsersToGroup(Guid groupId, [FromBody] Guid[] users)
        {
            await _repo.AddUsersToGroupAsync(users, groupId);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddUsersToGroup(int idGroup, [FromBody] int[] users)
        {
            await _repo.AddUsersToGroupAsync(users, idGroup);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddGroupsToUser(string user, [FromBody] string[] groups)
        {
            await _repo.AddGroupsToUserAsync(groups, user);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddGroupsToUser(Guid userId, [FromBody] Guid[] groups)
        {
            await _repo.AddGroupsToUserAsync(groups, userId);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddGroupsToUser(int idUser, [FromBody] int[] groups)
        {
            await _repo.AddGroupsToUserAsync(groups, idUser);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveUsersFromGroup(int idGroup, [FromBody]int[] idUsers)
        {
            await _repo.RemoveUsersFromGroupAsync(idUsers, idGroup);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveUsersFromGroup(Guid groupId, [FromBody]Guid[] usersId)
        {
            await _repo.RemoveUsersFromGroupAsync(usersId, groupId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveUsersFromGroup(string group, [FromBody]string[] users)
        {
            await _repo.RemoveUsersFromGroupAsync(users, group);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGroupsFromUser(int idUser, [FromBody]int[] idGroups)
        {
            await _repo.RemoveGroupsFromUserAsync(idGroups, idUser);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGroupsFromUser(Guid userId, [FromBody]Guid[] groupsId)
        {
            await _repo.RemoveGroupsFromUserAsync(groupsId, userId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveGroupsFromUser(string user, [FromBody]string[] groups)
        {
            await _repo.RemoveGroupsFromUserAsync(groups, user);
            return Ok();
        }
    }
}
