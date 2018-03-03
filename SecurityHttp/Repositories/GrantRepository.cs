using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class GrantRepository : IGrantRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private readonly string _url;

        public GrantRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
            _url = $"api/{_context.Application.AppName}/grants";
        }

        public IEnumerable<SecObject> GetExceptRoleGrant(string role)
        {
            return _commonWeb.GetCollection<SecObject>($"{_url}/except/{role}");
        }

        public Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<SecObject>($"{_url}/except/{role}");
        }

        public IEnumerable<SecObject> GetRoleGrants(string role)
        {
            return _commonWeb.GetCollection<SecObject>($"{_url}/{role}");
        }

        public Task<IEnumerable<SecObject>> GetRoleGrantsAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<SecObject>($"{_url}/{role}");
        }

        public void RemoveGrant(string role, string secObject)
        {
            _commonWeb.Delete(_url, new {Role = role, SecObjects = new string[] {secObject}});
        }

        public Task RemoveGrantAsync(string role, string secObject)
        {
            return _commonWeb.DeleteAsync(_url, new { Role = role, SecObjects = new string[] { secObject } });
        }

        public void RemoveGrants(string role, string[] secObjects)
        {
            _commonWeb.Delete(_url, new { Role = role, SecObjects = secObjects });
        }

        public Task RemoveGrantsAsync(string role, string[] secObjects)
        {
            return _commonWeb.DeleteAsync(_url, new { Role = role, SecObjects = secObjects });
        }

        public void SetGrant(string role, string secObject)
        {
            _commonWeb.Put(_url, new {Role = role, SecObjects = new string[] {secObject}});
        }

        public Task SetGrantAsync(string role, string secObject)
        {
            return _commonWeb.PutAsync(_url, new { Role = role, SecObjects = new string[] { secObject } });
        }

        public void SetGrants(string role, string[] secObjects)
        {
            _commonWeb.Put(_url, new { Role = role, secObjects});
        }

        public Task SetGrantsAsync(string role, string[] secObjects)
        {
            return _commonWeb.PutAsync(_url, new { Role = role, secObjects });
        }
    }
}
