using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository;
using SecurityHttp.Helpers;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class ApplicationInternalRepository : IApplicationInternalRepository
    {
        private readonly ICommonWeb _commonWeb;

        public ApplicationInternalRepository(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        public Application Create(Application entity)
        {
            var app = _commonWeb.PostAndGet<Application>("api/applications", entity);
            return app;
        }

        public async Task<Application> CreateAsync(Application entity)
        {
            var app = await _commonWeb.PostAndGetAsync<Application>("api/applications", entity).ConfigureAwait(false);
            return app;
        }

        public Application CreateEmpty(string prefixForRequired)
        {
            var app = _commonWeb.Get<Application>($"api/applications", new {prefix = prefixForRequired});
            return app;
        }

        public async Task<Application> CreateEmptyAsync(string prefixForRequired)
        {
            var app = await _commonWeb.GetAsync<Application>($"api/applications", new { prefix = prefixForRequired });
            return app;
        }

        public Application Get(object id)
        {
            return _commonWeb.Get<Application>($"api/applications/{id}");
        }

        public async Task<Application> GetAsync(object id)
        {
            return await _commonWeb.GetAsync<Application>($"api/applications/{id}").ConfigureAwait(false);
        }

        public IEnumerable<Application> Get()
        {
            return _commonWeb.GetCollection<Application>($"api/applications");
        }

        public async Task<IEnumerable<Application>> GetAsync()
        {
            return await _commonWeb.GetCollectionAsync<Application>($"api/applications").ConfigureAwait(false);
        }

        public Application GetByName(string name)
        {
            return _commonWeb.Get<Application>($"api/applications", new {name});
        }

        public async Task<Application> GetByNameAsync(string name)
        {
            return await _commonWeb.GetAsync<Application>($"api/applications", new { name });
        }

        public void Remove(object id)
        {
            _commonWeb.Delete($"api/applications/{id}");
        }

        public async Task RemoveAsync(object id)
        {
            await _commonWeb.DeleteAsync($"api/applications/{id}").ConfigureAwait(false);
        }

        public void Update(Application entity)
        {
            _commonWeb.Put("api/applications", entity);
        }

        public async Task UpdateAsync(Application entity)
        {
            await _commonWeb.PutAsync("api/applications", entity);
        }
    }
}
