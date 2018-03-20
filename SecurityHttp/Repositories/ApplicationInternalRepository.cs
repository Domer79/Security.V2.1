using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts.Repository;
using Security.Model;
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
            throw new NotSupportedException();
        }

        public Task<Application> CreateEmptyAsync(string prefixForRequired)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }

        public Application Get(object id)
        {
            return _commonWeb.Get<Application>($"api/applications", new{id});
        }

        public Task<Application> GetAsync(object id)
        {
            return _commonWeb.GetAsync<Application>($"api/applications", new { id });
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
            _commonWeb.Delete($"api/applications", new {id});
        }

        public async Task RemoveAsync(object id)
        {
            await _commonWeb.DeleteAsync($"api/applications", new { id }).ConfigureAwait(false);
        }

        public void Update(Application entity)
        {
            _commonWeb.Put("api/applications", entity);
        }

        public async Task UpdateAsync(Application entity)
        {
            await _commonWeb.PutAsync("api/applications", entity);
        }

        public void Remove(string appName)
        {
            _commonWeb.Delete($"api/applications", new {appName});
        }

        public Task RemoveAsync(string appName)
        {
            return _commonWeb.DeleteAsync($"api/applications", new { appName });
        }
    }
}
