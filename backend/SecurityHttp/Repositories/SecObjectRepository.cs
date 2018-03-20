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
    public class SecObjectRepository : ISecObjectRepository
    {
        private readonly ICommonWeb _commonWeb;
        private readonly IApplicationContext _context;
        private string _url;

        public SecObjectRepository(ICommonWeb commonWeb, IApplicationContext context)
        {
            _commonWeb = commonWeb;
            _context = context;
        }

        private string Url
        {
            get { return _url ?? (_url = $"api/{_context.Application.AppName}/policy"); }
        }

        public SecObject Create(SecObject entity)
        {
            return _commonWeb.PostAndGet<SecObject>($"{Url}", entity);
        }

        public Task<SecObject> CreateAsync(SecObject entity)
        {
            return _commonWeb.PostAndGetAsync<SecObject>($"{Url}", entity);
        }

        public SecObject CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<SecObject>($"{Url}", new {prefix = prefixForRequired});
        }

        public Task<SecObject> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<SecObject>($"{Url}", new { prefix = prefixForRequired });
        }

        public SecObject Get(object id)
        {
            return _commonWeb.Get<SecObject>($"{Url}", new {id});
        }

        public IEnumerable<SecObject> Get()
        {
            return _commonWeb.GetCollection<SecObject>($"{Url}");
        }

        public Task<SecObject> GetAsync(object id)
        {
            return _commonWeb.GetAsync<SecObject>($"{Url}", new { id });
        }

        public Task<IEnumerable<SecObject>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<SecObject>($"{Url}");
        }

        public SecObject GetByName(string name)
        {
            return _commonWeb.Get<SecObject>($"{Url}", new {name});
        }

        public Task<SecObject> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<SecObject>($"{Url}", new { name });
        }

        public void Remove(object id)
        {
            _commonWeb.Delete($"{Url}", new {id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync($"{Url}", new { id });
        }

        public void Update(SecObject entity)
        {
            _commonWeb.Put($"{Url}", entity);
        }

        public Task UpdateAsync(SecObject entity)
        {
            return _commonWeb.PutAsync($"{Url}", entity);
        }
    }
}
