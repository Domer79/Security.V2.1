using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;

namespace Security.V2.Core.Http
{
    public class UserRepository : IUserRepository
    {
        private readonly IGlobalSettings _settings;

        public UserRepository(IGlobalSettings settings)
        {
            _settings = settings;
        }

        public User Create(User entity)
        {
            throw new NotImplementedException();
//            var requestUri = new Uri($"{_settings.SecurityServerAddress}/api");
//            var request = WebRequest.Create(requestUri)
        }

        public Task<User> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public User CreateEmpty(string prefixForRequired)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateEmptyAsync(string prefixForRequired)
        {
            throw new NotImplementedException();
        }

        public User Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public User GetByName(string loginOrEmail)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByNameAsync(string loginOrEmail)
        {
            throw new NotImplementedException();
        }

        public void Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(string loginOrEmail, bool status)
        {
            throw new NotImplementedException();
        }

        public Task SetStatusAsync(string loginOrEmail, bool status)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
