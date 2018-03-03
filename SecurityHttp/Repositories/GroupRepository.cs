using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ICommonWeb _commonWeb;

        public GroupRepository(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        public Group Create(Group entity)
        {
            return _commonWeb.PostAndGet<Group>("api/groups", entity);
        }

        public Task<Group> CreateAsync(Group entity)
        {
            return _commonWeb.PostAndGetAsync<Group>("api/groups", entity);
        }

        public Group CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<Group>("api/groups", new {prefix = prefixForRequired});
        }

        public Task<Group> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<Group>("api/groups", new { prefix = prefixForRequired });
        }

        public Group Get(object id)
        {
            return _commonWeb.Get<Group>("api/groups", new {id});
        }

        public IEnumerable<Group> Get()
        {
            return _commonWeb.GetCollection<Group>("api/groups");
        }

        public Task<Group> GetAsync(object id)
        {
            return _commonWeb.GetAsync<Group>("api/groups", new { id });
        }

        public Task<IEnumerable<Group>> GetAsync()
        {
            return _commonWeb.GetCollectionAsync<Group>("api/groups");
        }

        public Group GetByName(string name)
        {
            return _commonWeb.Get<Group>($"api/groups/getbyname/{name}");
        }

        public Task<Group> GetByNameAsync(string name)
        {
            return _commonWeb.GetAsync<Group>($"api/groups/getbyname/{name}");
        }

        public void Remove(object id)
        {
            _commonWeb.Delete("api/groups", new {id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync("api/groups", new { id });
        }

        public void Update(Group entity)
        {
            _commonWeb.Put("api/groups", entity);
        }

        public Task UpdateAsync(Group entity)
        {
            return _commonWeb.PutAsync("api/groups", entity);
        }
    }
}
