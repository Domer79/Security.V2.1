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
        private readonly IAccessTypeCollection _accessTypeCollection;
        private readonly IGrantCollection _grantCollection;
        private readonly IGroupCollection _groupCollection;
        private readonly IMemberCollection _memberCollection;
        private readonly IRoleCollection _roleCollection;
        private readonly ISecObjectCollection _secObjectCollection;
        private readonly IUserCollection _userCollection;
        private readonly IApplicationCollection _applicationCollection;
        private readonly ISecurityTools _securityTools;
        private readonly ISecurityTransaction _securityTransaction;

        public SecurityFactory(
            IAccessTypeCollection accessTypeCollection,
            IGrantCollection grantCollection,
            IGroupCollection groupCollection,
            IMemberCollection memberCollection,
            IRoleCollection roleCollection,
            ISecObjectCollection secObjectCollection,
            IUserCollection userCollection,
            IApplicationCollection applicationCollection,
            ISecurityTools securityTools,
            ISecurityTransaction securityTransaction,
            ISecuritySettings securitySettings
            )
        {
            _accessTypeCollection = accessTypeCollection;
            _grantCollection = grantCollection;
            _groupCollection = groupCollection;
            _memberCollection = memberCollection;
            _roleCollection = roleCollection;
            _secObjectCollection = secObjectCollection;
            _userCollection = userCollection;
            _applicationCollection = applicationCollection;
            _securityTools = securityTools;
            _securityTransaction = securityTransaction;
            Settings = securitySettings;
        }

        public string ApplicationName { get; set; }

        public IAccessTypeCollection CreateAccessTypeCollection()
        {
            return _accessTypeCollection;
        }

        public IGrantCollection CreateGrantCollection()
        {
            return _grantCollection;
        }

        public IGroupCollection CreateGroupCollection()
        {
            return _groupCollection;
        }

        public IMemberCollection CreateMemberCollection()
        {
            return _memberCollection;
        }

        public IRoleCollection CreateRoleCollection()
        {
            return _roleCollection;
        }

        public ISecObjectCollection CreateSecObjectCollection()
        {
            return _secObjectCollection;
        }

        public IUserCollection CreateUserCollection()
        {
            return _userCollection;
        }

        public IApplicationCollection CreateApplicationCollection()
        {
            return _applicationCollection;
        }

        public ISecurityTools CreateSecurityTools()
        {
            return _securityTools;
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

        public Application CurrentApplication => GetCurrentApplication(ApplicationName);

        public void CreateAppIfNoExists(ISecurityApplicationInfo securityApplicationInfo)
        {
            throw new NotImplementedException();
        }

        public void CreateAppIfNoExists(string applicationName, string description)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private Application GetCurrentApplication(string applicationName)
        {
            throw new NotImplementedException();
        }
    }
}
