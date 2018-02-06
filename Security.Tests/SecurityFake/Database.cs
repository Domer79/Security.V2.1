using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public static class Database
    {
        private static ApplicationCollection _applications;
        private static MemberCollection _members;
        private static UserCollection _users;
        private static GroupCollection _groups;
        private static UserGroups _userGroups;
        private static RoleCollection _roles;
        private static MemberRoles _memberRoles;
        private static SecObjectCollection _secObjects;
        private static GrantCollection _grants;
        private static UserPasswords _userPasswords;

        public static ApplicationCollection Applications = _applications ?? (_applications = new ApplicationCollection());
        public static MemberCollection Members = _members ?? (_members = new MemberCollection());
        public static UserCollection Users = _users ?? (_users = new UserCollection());
        public static GroupCollection Groups = _groups ?? (_groups = new GroupCollection());
        public static UserGroups UserGroups = _userGroups ?? (_userGroups = new UserGroups());
        public static RoleCollection Roles = _roles ?? (_roles = new RoleCollection());
        public static MemberRoles MemberRoles = _memberRoles ?? (_memberRoles = new MemberRoles());
        public static SecObjectCollection SecObjects = _secObjects ?? (_secObjects = new SecObjectCollection());
        public static GrantCollection Grants = _grants ?? (_grants = new GrantCollection());
        internal static UserPasswords UserPasswords = _userPasswords ?? (_userPasswords = new UserPasswords());

        public static void Drop()
        {
            Applications.Drop();
            Members.Drop();
            Users.Drop();
            Groups.Drop();
            UserGroups.Drop();
            Roles.Drop();
            MemberRoles.Drop();
            SecObjects.Drop();
            Grants.Drop();
            UserPasswords.Drop();
        }
    }

    public static class DatabaseService
    {
        public static int Identity<T>(this BaseCollection<T> collection)
        {
            return collection.Count() + 1;
        }

        public static Member AsMember(this User user)
        {
            return Database.Members.Single(_ => _.Name == user.Login);
        }

        public static Member AsMember(this Group group)
        {
            return Database.Members.Single(_ => _.Name == group.Name);
        }
    }
}
