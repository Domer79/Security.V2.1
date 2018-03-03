using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Interfaces;
using Security.Interfaces.Collections;
using Security.Model;
using Security.V2.Contracts;
using ISecuritySettings = Security.Interfaces.ISecuritySettings;

namespace SecurityHttp
{
    public class SecurityFactory : ISecurityFactory
    {
        private readonly IServiceLocator _locator;

        public SecurityFactory(IServiceLocator locator)
        {
            _locator = locator;
        }

        public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ISecuritySettings Settings => throw new NotImplementedException();

        public Application CurrentApplication => throw new NotImplementedException();

        public ISecurityTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CreateAppIfNoExists()
        {
            throw new NotImplementedException();
        }

        public void CreateAppIfNoExists(ISecurityApplicationInfo securityApplicationInfo)
        {
            throw new NotImplementedException();
        }

        public void CreateAppIfNoExists(string applicationName, string description)
        {
            throw new NotImplementedException();
        }

        public IApplicationCollection CreateApplicationCollection()
        {
            throw new NotImplementedException();
        }

        public IGrantCollection CreateGrantCollection()
        {
            throw new NotImplementedException();
        }

        public IGroupCollection CreateGroupCollection()
        {
            throw new NotImplementedException();
        }

        public IMemberCollection CreateMemberCollection()
        {
            throw new NotImplementedException();
        }

        public IRoleCollection CreateRoleCollection()
        {
            throw new NotImplementedException();
        }

        public ISecObjectCollection CreateSecObjectCollection()
        {
            throw new NotImplementedException();
        }

        public ISecurityTools CreateSecurityTools()
        {
            throw new NotImplementedException();
        }

        public IUserCollection CreateUserCollection()
        {
            return _locator.Resolve<IUserCollection>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
