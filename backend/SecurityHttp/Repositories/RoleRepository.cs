using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private string _url;

        public RoleRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        private string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/roles"); }
        }

        public Role Create(Role entity)
        {
            return _commonWeb.PostAndGet<Role>($"{Url}", entity);
        }

        public Task<Role> CreateAsync(Role entity)
        {
            return _commonWeb.PostAndGetAsync<Role>($"{Url}", entity);
        }

        public Role CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<Role>($"{Url}", new {prefix = prefixForRequired});
        }

        public Task<Role> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<Role>($"{Url}", new { prefix = prefixForRequired });
        }

        public Role Get(object id)
        {
            return _commonWeb.Get<Role>($"{Url}", new {id});
        }

        public IEnumerable<Role> Get()
        {
            return _commonWeb.GetCollection<Role>($"{Url}");
        }

        public Task<Role> GetAsync(object id)
        {
            return _commonWeb.GetAsync<Role>($"{Url}", new { id });
        }

        public Task<IEnumerable<Role>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<Role>($"{Url}");
        }

        public Role GetByName(string name)
        {
            return _commonWeb.Get<Role>($"{Url}", new {name});
        }

        public Task<Role> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<Role>($"{Url}", new { name });
        }

        public void Remove(object id)
        {
            _commonWeb.Delete($"{Url}", new {id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync($"{Url}", new { id });
        }

        public void Update(Role entity)
        {
            _commonWeb.Put($"{Url}", entity);
        }

        public Task UpdateAsync(Role entity)
        {
            return _commonWeb.PutAsync($"{Url}", entity);
        }
    }
}
