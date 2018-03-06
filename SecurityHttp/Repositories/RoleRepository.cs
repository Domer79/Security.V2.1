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
    public class RoleRepository : IRoleRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private readonly string _url;

        public RoleRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
            _url = $"api/{context.Application.AppName}/roles";
        }

        public Role Create(Role entity)
        {
            return _commonWeb.PostAndGet<Role>($"{_url}", entity);
        }

        public Task<Role> CreateAsync(Role entity)
        {
            return _commonWeb.PostAndGetAsync<Role>($"{_url}", entity);
        }

        public Role CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<Role>($"{_url}", new {prefix = prefixForRequired});
        }

        public Task<Role> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<Role>($"{_url}", new { prefix = prefixForRequired });
        }

        public Role Get(object id)
        {
            return _commonWeb.Get<Role>($"{_url}", new {id});
        }

        public IEnumerable<Role> Get()
        {
            return _commonWeb.GetCollection<Role>($"{_url}");
        }

        public Task<Role> GetAsync(object id)
        {
            return _commonWeb.GetAsync<Role>($"{_url}", new { id });
        }

        public Task<IEnumerable<Role>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<Role>($"{_url}");
        }

        public Role GetByName(string name)
        {
            return _commonWeb.Get<Role>($"{_url}", new {name});
        }

        public Task<Role> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<Role>($"{_url}", new { name });
        }

        public void Remove(object id)
        {
            _commonWeb.Delete($"{_url}", new {id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync($"{_url}", new { id });
        }

        public void Update(Role entity)
        {
            _commonWeb.Put($"{_url}", entity);
        }

        public Task UpdateAsync(Role entity)
        {
            return _commonWeb.PutAsync($"{_url}", entity);
        }
    }
}
