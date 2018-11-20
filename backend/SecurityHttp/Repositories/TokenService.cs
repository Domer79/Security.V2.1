using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts.Repository;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class TokenService: ITokenService
    {
        private readonly ICommonWeb _commonWeb;

        public TokenService(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        public string Create(int idUser)
        {
            return _commonWeb.PostAndGet<string>("api/token/create-by-id", null, new {idUser});
        }

        public string Create(string loginOrEmail)
        {
            return _commonWeb.PostAndGet<string>("api/token/create-by-login", null, new {loginOrEmail});
        }

        public void StopExpire(string tokenId, string reason = null)
        {
            _commonWeb.Delete("api/token/stop", new {tokenId, reason});
        }

        public void StopExpireForUser(string tokenId, string reason = null)
        {
            _commonWeb.Delete("api/token/stop-all", new { tokenId, reason });
        }

        public Task<string> CreateAsync(int idUser)
        {
            return _commonWeb.PostAndGetAsync<string>("api/token/create-by-id", null, new { idUser });
        }

        public Task<string> CreateAsync(string loginOrEmail)
        {
            return _commonWeb.PostAndGetAsync<string>("api/token/create-by-login", null, new { loginOrEmail });
        }

        public Task StopExpireAsync(string tokenId, string reason = null)
        {
            return _commonWeb.DeleteAsync("api/token/stop", new { tokenId, reason });
        }

        public Task StopExpireForUserAsync(string tokenId, string reason = null)
        {
            return _commonWeb.DeleteAsync("api/token/stop-all", new { tokenId, reason });
        }
    }
}
