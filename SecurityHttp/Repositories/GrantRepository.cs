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
        private string _url;

        public GrantRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        private string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/grants"); }
        }

        public IEnumerable<SecObject> GetExceptRoleGrant(string role)
        {
            return _commonWeb.GetCollection<SecObject>($"{Url}/except/{role}");
        }

        public Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<SecObject>($"{Url}/except/{role}");
        }

        public IEnumerable<SecObject> GetRoleGrants(string role)
        {
            return _commonWeb.GetCollection<SecObject>($"{Url}/{role}");
        }

        public Task<IEnumerable<SecObject>> GetRoleGrantsAsync(string role)
        {
            return _commonWeb.GetCollectionAsync<SecObject>($"{Url}/{role}");
        }

        public void RemoveGrant(string role, string secObject)
        {
            _commonWeb.Delete(Url, new string[] {secObject}, new {role});
        }

        public Task RemoveGrantAsync(string role, string secObject)
        {
            return _commonWeb.DeleteAsync(Url, new string[] { secObject }, new { role });
        }

        public void RemoveGrants(string role, string[] secObjects)
        {
            _commonWeb.Delete(Url, secObjects, new {role});
        }

        public Task RemoveGrantsAsync(string role, string[] secObjects)
        {
            return _commonWeb.DeleteAsync(Url, secObjects, new { role });
        }

        public void SetGrant(string role, string secObject)
        {
            _commonWeb.Put(Url, new string[] { secObject }, new { role });
        }

        public Task SetGrantAsync(string role, string secObject)
        {
            return _commonWeb.PutAsync(Url, new string[] { secObject }, new { role });
        }

        public void SetGrants(string role, string[] secObjects)
        {
            _commonWeb.Put(Url, secObjects, new { role });
        }

        public Task SetGrantsAsync(string role, string[] secObjects)
        {
            return _commonWeb.PutAsync(Url, secObjects, new { role });
        }
    }
}
