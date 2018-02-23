using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Security.Model;
using Security.V2.Contracts.Repository;

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
        
        [Route("get/users/{groupName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUsersByGroupNameAsync(string groupName)
        {
            var users = await _repo.GetUsersByGroupNameAsync(groupName);
            return Ok(users);
        }

        [Route("get/groups/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGroupsByUserNameAsync(string userName)
        {
            var groups = await _repo.GetGroupsByUserNameAsync(userName);
            return Ok(groups);
        }

        [Route("set/users")]
        [HttpPost]
        public async Task<IHttpActionResult> AddUsersToGroupAsync(GroupUsersModel model)
        {
            if (model.Group == null)
                throw new ArgumentNullException(nameof(model.Group));

            if (model.Users == null)
                throw new ArgumentNullException(nameof(model.Users));

            await _repo.AddUsersToGroupAsync(model.Users, model.Group);
            return Ok();
        }

        [Route("set/groups")]
        [HttpPost]
        public async Task<IHttpActionResult> AddGroupsToUserAsync(UserGroupsModel model)
        {
            if (model.User == null)
                throw new ArgumentNullException(nameof(model.User));

            if (model.Groups == null)
                throw new ArgumentNullException(nameof(model.Groups));

            await _repo.AddGroupsToUserAsync(model.Groups, model.User);
            return Ok();
        }
    }

    public class GroupUsersModel
    {
        public string Group { get; set; }
        public string[] Users { get; set; }
    }

    public class UserGroupsModel
    {
        public string User { get; set; }
        public string[] Groups { get; set; }
    }
}
