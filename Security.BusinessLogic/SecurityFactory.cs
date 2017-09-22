using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Interfaces;
using Security.Interfaces.Collections;
using Security.Model;

namespace Security.BusinessLogic
{
    public class SecurityFactory: ISecurityFactory
    {
        public delegate SecurityFactory Factory();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string ApplicationName { get; set; }
        public IAccessTypeCollection CreateAccessTypeCollection()
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

        public IUserCollection CreateUserCollection()
        {
            throw new NotImplementedException();
        }

        public IApplicationCollection CreateApplicationCollection()
        {
            throw new NotImplementedException();
        }

        public ISecurityTools CreateSecurityTools()
        {
            throw new NotImplementedException();
        }

        public AccessType GetAccessType()
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

        public ISecurityTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public ISecuritySettings Settings { get; }
        public Application CurrentApplication { get; }
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
    }
}
