﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Security.V2.Core.Ioc.Interfaces;

namespace Security.V2.Core
{
    public class Security : ISecurity
    {
        private readonly string _appName;

        public Security(string appName, string description = null)
            :this(appName, description, IocConfig.GetLocator())
        {
        }

        internal Security(string appName, string description, IServiceLocator locator)
        {
            _appName = appName;
            Locator = locator;

            //Register application if not exists
            Config.RegisterApplication(appName, description);
        }

        public IApplicationContext ApplicationContext => Locator.Resolve<IApplicationContext>();

        public IUserRepository UserRepository => Factory.UserRepository;

        public IGroupRepository GroupRepository => Factory.GroupRepository;

        public IApplicationRepository ApplicationRepository => Factory.ApplicationRepository;

        public IUserGroupRepository UserGroupRepository => Factory.UserGroupRepository;

        public IMemberRoleRepository MemberRoleRepository => Factory.MemberRoleRepository;

        public IRoleRepository RoleRepository => Factory.RoleRepository;

        public ISecObjectRepository SecObjectRepository => Factory.SecObjectRepository;

        public IGrantRepository GrantRepository => Factory.GrantRepository;

        public ISecuritySettings SecuritySettings => Factory.SecuritySettings;

        public IConfig Config => Factory.Config;

        internal IServiceLocator Locator { get; set; }

        internal IUserInternalRepository UserInternalRepository => Factory.UserInternalRepository;

        private ISecurityFactory Factory => Locator.Resolve<ISecurityFactory>();

        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            return UserInternalRepository.CheckAccess(loginOrEmail, secObject);
        }

        public bool SetPassword(string loginOrEmail, string password)
        {
            return UserInternalRepository.SetPassword(loginOrEmail, password);
        }

        public bool UserValidate(string loginOrEmail, string password)
        {
            return UserInternalRepository.UserValidate(loginOrEmail, password);
        }

        public void Dispose()
        {
            Locator.Dispose();
        }
    }
}
