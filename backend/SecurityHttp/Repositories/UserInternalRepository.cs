using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
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

        public bool CheckTokenAccess(string token, string policy)
        {
            return _commonWeb.Get<bool>($"api/{_context.Application.AppName}/common/check-access-token", new {token, policy});
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

        public string CreateToken(string loginOrEmail, string password)
        {
            return _commonWeb.PostAndGet<string>("api/common/create-token", null, new {loginOrEmail, password});
        }

        public Task<bool> UserValidateAsync(string loginOrEmail, string password)
        {
            return _commonWeb.GetAsync<bool>("api/common/validate", new { loginOrEmail, password });
        }

        public Task<string> CreateTokenAsync(string loginOrEmail, string password)
        {
            return _commonWeb.PostAndGetAsync<string>("api/common/create-token", null, new { loginOrEmail, password });
        }

        public Task<bool> CheckTokenAccessAsync(string token, string policy)
        {
            return _commonWeb.GetAsync<bool>($"api/{_context.Application.AppName}/common/check-access-token", new { token, policy });
        }
    }
}
