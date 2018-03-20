using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository;

namespace SecurityHttp.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IApplicationInternalRepository _repo;

        public ApplicationRepository(IApplicationInternalRepository repo)
        {
            _repo = repo;
        }

        public Application Create(Application entity)
        {
            throw new NotSupportedException();
        }

        public Task<Application> CreateAsync(Application entity)
        {
            return Task.FromException<Application>(new NotSupportedException());
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
            return _repo.Get(id);
        }

        public IEnumerable<Application> Get()
        {
            return _repo.Get();
        }

        public Task<Application> GetAsync(object id)
        {
            return _repo.GetAsync(id);
        }

        public Task<IEnumerable<Application>> GetAsync()
        {
            return _repo.GetAsync();
        }

        public Application GetByName(string name)
        {
            return _repo.GetByName(name);
        }

        public Task<Application> GetByNameAsync(string name)
        {
            return _repo.GetByNameAsync(name);
        }

        public void Remove(object id)
        {
            throw new NotSupportedException();
        }

        public Task RemoveAsync(object id)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }

        public void Update(Application entity)
        {
            throw new NotSupportedException();
        }

        public Task UpdateAsync(Application entity)
        {
            return Task.FromException<Application>(new NotSupportedException());
        }
    }
}
