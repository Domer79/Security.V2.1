using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Model;

namespace Security.Tests.Scenarios
{
    public class CreateUserAndGrantAccessScenario: BaseScenario<CreateUserAndGrantAccessResult>
    {
        private string _login;
        private SecObject _secObject;
        private Role _role;
        private User _user;

        protected override async Task<CreateUserAndGrantAccessResult> _RunAsync(ISecurity security, Func<object, Task<object>> someTask)
        {
            _login = "testForCheckAccessLogin";
            var user = new User
            {
                Login = $"{_login}",
                Email = $"{_login}@mail.ru",
                FirstName = $"{_login}First",
                LastName = $"{_login}Last",
                MiddleName = $"{_login}Middle",
                Status = true,
                DateCreated = DateTime.Now
            };
            _user = await security.UserRepository.CreateAsync(user);
            await security.SetPasswordAsync(_login, "test");

            var secObject = new SecObject() { ObjectName = "TestObjectForCheckAccess" };
            var role = new Role() { Name = "TestRole", Description = "TestRoleDescription" };
            _secObject = await security.SecObjectRepository.CreateAsync(secObject);
            _role = await security.RoleRepository.CreateAsync(role);
            await security.GrantRepository.SetGrantAsync(_role.Name, _secObject.ObjectName);
            await security.MemberRoleRepository.AddMembersToRoleAsync(new[] { _login }, _role.Name);

            var token1 = await security.CreateTokenAsync(_login, "test");
            var token2 = await security.CreateTokenAsync(_login, "test");
            var token3 = await security.CreateTokenAsync(_login, "test");
            var token4 = await security.CreateTokenAsync(_login, "test");

            return new CreateUserAndGrantAccessResult()
            {
                Login = _login,
                UserPolicies = new []{_secObject.ObjectName},
                UserTokens = new []{token1, token2, token3, token4}
            };
        }

        protected override async Task _RollbackAsync(ISecurity security)
        {
            await security.MemberRoleRepository.DeleteMembersFromRoleAsync(new[] { _login }, _role.Name);
            await security.GrantRepository.RemoveGrantAsync(_role.Name, _secObject.ObjectName);
            await security.RoleRepository.RemoveAsync(_role.IdRole);
            await security.SecObjectRepository.RemoveAsync(_secObject.IdSecObject);
            await security.UserRepository.RemoveAsync(_user.IdMember);
        }

        protected override CreateUserAndGrantAccessResult _Run(ISecurity security, Func<object, Task<object>> someTask)
        {
            _login = "testForCheckAccessLogin";
            var user = new User
            {
                Login = $"{_login}",
                Email = $"{_login}@mail.ru",
                FirstName = $"{_login}First",
                LastName = $"{_login}Last",
                MiddleName = $"{_login}Middle",
                Status = true,
                DateCreated = DateTime.Now
            };
            _user = security.UserRepository.Create(user);
            security.SetPassword(_login, "test");

            var secObject = new SecObject() { ObjectName = "TestObjectForCheckAccess" };
            var role = new Role() { Name = "TestRole", Description = "TestRoleDescription" };
            _secObject = security.SecObjectRepository.Create(secObject);
            _role = security.RoleRepository.Create(role);
            security.GrantRepository.SetGrant(_role.Name, _secObject.ObjectName);
            security.MemberRoleRepository.AddMembersToRole(new[] { _login }, _role.Name);

            var token1 = security.CreateToken(_login, "test");
            var token2 = security.CreateToken(_login, "test");
            var token3 = security.CreateToken(_login, "test");
            var token4 = security.CreateToken(_login, "test");

            return new CreateUserAndGrantAccessResult()
            {
                Login = _login,
                UserPolicies = new[] { _secObject.ObjectName },
                UserTokens = new[] { token1, token2, token3, token4 }
            };
        }

        protected override void _Rollback(ISecurity security)
        {
            security.MemberRoleRepository.DeleteMembersFromRole(new []{_login}, _role.Name);
            security.GrantRepository.RemoveGrant(_role.Name, _secObject.ObjectName);
            security.RoleRepository.Remove(_role.IdRole);
            security.SecObjectRepository.Remove(_secObject.IdSecObject);
            security.UserRepository.Remove(_user.IdMember);
        }
    }

    public class CreateUserAndGrantAccessResult
    {
        public string Login { get; set; }
        public string[] UserPolicies { get; set; }
        public string[] UserTokens { get; set; }
    }
}
