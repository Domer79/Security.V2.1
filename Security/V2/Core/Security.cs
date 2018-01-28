using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core.Ioc;

namespace Security.V2.Core
{
    public class Security : ISecurity
    {
        private readonly string _appName;
        private IServiceLocator _serviceLocator;
        private ISecurityFactory _securityFactory;

        public Security(string appName)
        {
            _appName = appName;
            _securityFactory = _serviceLocator.Resolve<ISecurityFactory>();
        }

        public IApplicationContext ApplicationContext => throw new NotImplementedException();

        public IUserRepository UserRepository => throw new NotImplementedException();

        public IGroupRepository GroupRepository => throw new NotImplementedException();

        public IApplicationRepository ApplicationRepository => throw new NotImplementedException();

        public IUserGroupRepository UserGroupRepository => throw new NotImplementedException();

        public IMemberRoleRepository MemberRoleRepository => throw new NotImplementedException();

        public IRoleRepository RoleRepository => throw new NotImplementedException();

        public ISecObjectRepository SecObjectRepository => throw new NotImplementedException();

        public IGrantRepository GrantRepository => throw new NotImplementedException();

        public ISecuritySettings SecuritySettings => throw new NotImplementedException();

        public IConfig Config => throw new NotImplementedException();

        public bool CheckAccess(string login, string secObject)
        {
            throw new NotImplementedException();
        }

        public bool UserValidate(string login, string password)
        {
            throw new NotImplementedException();
        }

        internal IServiceLocator ServiceLocator
        {
            get => _serviceLocator ?? (_serviceLocator = GetServiceLocator());
            set => _serviceLocator = value;
        }

        private IServiceLocator GetServiceLocator()
        {
            var serviceLocator = new ServiceLocator();
            throw new NotImplementedException();
//            serviceLocator.RegisterType<ISecurityFactory>().AsSingle<SecurityFactory>();
        }
    }

    internal class SecurityFactory : ISecurityFactory
    {
        public IUserRepository UserRepository { get; }

        public IGroupRepository GroupRepository { get; }

        public IApplicationRepository ApplicationRepository { get; }

        public IUserGroupRepository UserGroupRepository { get; }

        public IMemberRoleRepository MemberRoleRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public ISecObjectRepository SecObjectRepository { get; }

        public IGrantRepository GrantRepository { get; }

        public ISecuritySettings SecuritySettings { get; }

        public IConfig Config { get; }
    }
}
