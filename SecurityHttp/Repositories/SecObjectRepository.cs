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
    public class SecObjectRepository : ISecObjectRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private readonly string _url;

        public SecObjectRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
            _url = $"api/{context.Application.AppName}/policy";
        }

        public SecObject Create(SecObject entity)
        {
            return _commonWeb.PostAndGet<SecObject>($"{_url}", entity);
        }

        public Task<SecObject> CreateAsync(SecObject entity)
        {
            return _commonWeb.PostAndGetAsync<SecObject>($"{_url}", entity);
        }

        public SecObject CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<SecObject>($"{_url}", new {prefix = prefixForRequired});
        }

        public Task<SecObject> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<SecObject>($"{_url}", new { prefix = prefixForRequired });
        }

        public SecObject Get(object id)
        {
            return _commonWeb.Get<SecObject>($"{_url}", new {id});
        }

        public IEnumerable<SecObject> Get()
        {
            return _commonWeb.GetCollection<SecObject>($"{_url}");
        }

        public Task<SecObject> GetAsync(object id)
        {
            return _commonWeb.GetAsync<SecObject>($"{_url}", new { id });
        }

        public Task<IEnumerable<SecObject>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<SecObject>($"{_url}");
        }

        public SecObject GetByName(string name)
        {
            return _commonWeb.Get<SecObject>($"{_url}", new {name});
        }

        public Task<SecObject> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<SecObject>($"{_url}", new { name });
        }

        public void Remove(object id)
        {
            _commonWeb.Delete($"{_url}", new {id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync($"{_url}", new { id });
        }

        public void Update(SecObject entity)
        {
            _commonWeb.Put($"{_url}", entity);
        }

        public Task UpdateAsync(SecObject entity)
        {
            return _commonWeb.PutAsync($"{_url}", entity);
        }
    }
}
