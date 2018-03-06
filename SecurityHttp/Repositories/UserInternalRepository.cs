using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class UserInternalRepository : IUserInternalRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;

        public UserInternalRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            return _commonWeb.Get<bool>($"api/{_context.Application.AppName}/common/checkaccess", new {loginOrEmail, policy = secObject});
        }

        public Task<bool> CheckAccessAsync(string loginOrEmail, string secObject)
        {
            return _commonWeb.GetAsync<bool>($"api/{_context.Application.AppName}/common/checkaccess", new { loginOrEmail, policy = secObject });
        }

        public bool SetPassword(string loginOrEmail, string password)
        {
            return _commonWeb.Get<bool>("api/common/setpassword", new {loginOrEmail, password});
        }

        public Task<bool> SetPasswordAsync(string loginOrEmail, string password)
        {
            return _commonWeb.GetAsync<bool>("api/common/setpassword", new { loginOrEmail, password });
        }

        public bool UserValidate(string loginOrEmail, string password)
        {
            return _commonWeb.Get<bool>("api/common/validate", new {loginOrEmail, password});
        }

        public Task<bool> UserValidateAsync(string loginOrEmail, string password)
        {
            return _commonWeb.GetAsync<bool>("api/common/validate", new { loginOrEmail, password });
        }
    }
}
