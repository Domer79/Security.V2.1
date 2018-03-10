using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Security.Model;
using Security.V2.CommonContracts;
using Security.V2.Contracts.Repository;
using SecurityHttp.Helpers;
using SecurityHttp.Interfaces;

namespace SecurityHttp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICommonWeb _commonWeb;

        public UserRepository(ICommonWeb commonWeb)
        {
            _commonWeb = commonWeb;
        }

        public User Create(User entity)
        {
            return _commonWeb.PostAndGet<User>("api/user", entity);
        }

        public Task<User> CreateAsync(User entity)
        {
            return _commonWeb.PostAndGetAsync<User>("api/user", entity);
        }

        public User CreateEmpty(string prefixForRequired)
        {
            return _commonWeb.Get<User>("api/user", new {prefixForRequired});
        }

        public Task<User> CreateEmptyAsync(string prefixForRequired)
        {
            return _commonWeb.GetAsync<User>("api/user", new { prefixForRequired });
        }

        public User Get(object id)
        {
            var user = _commonWeb.Get<User>($"api/user/{id}");
            return user;
        }

        public async Task<User> GetAsync(object id)
        {
            var user = await _commonWeb.GetAsync<User>($"api/user/{id}");
            return user;
        }

        public IEnumerable<User> Get()
        {
            var users = _commonWeb.Get<IEnumerable<User>>("api/user");
            return users;
        }

        public Task<IEnumerable<User>> GetAsync()
        {
            var users = _commonWeb.GetAsync<IEnumerable<User>>("api/user");
            return users;
        }

        public User GetByName(string loginOrEmail)
        {
            var user = _commonWeb.Get<User>($"api/user", new {loginOrEmail});
            return user;
        }

        public Task<User> GetByNameAsync(string loginOrEmail)
        {
            var user = _commonWeb.GetAsync<User>($"api/user", new { loginOrEmail });
            return user;
        }

        public void Remove(object id)
        {
            _commonWeb.Delete("api/user", new {id});
        }

        public Task RemoveAsync(object id)
        {
            return _commonWeb.DeleteAsync("api/user", new {id});
        }

        public void SetStatus(string loginOrEmail, bool status)
        {
            _commonWeb.Post("api/user/setstatus", new {loginOrEmail, status});
        }

        public Task SetStatusAsync(string loginOrEmail, bool status)
        {
            return _commonWeb.PostAsync("api/user/setstatus", new {loginOrEmail, status});
        }

        public void Update(User entity)
        {
            _commonWeb.Put("api/user", entity);
        }

        public Task UpdateAsync(User entity)
        {
            return _commonWeb.PutAsync("api/user", entity);
        }
    }
}
