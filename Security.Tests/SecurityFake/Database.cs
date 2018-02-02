using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class Database
    {
        public static ApplicationCollection Applications = new ApplicationCollection();
        public static MemberCollection Members = new MemberCollection();
        public static UserCollection Users = new UserCollection();
        public static GroupCollection Groups = new GroupCollection();
        public static UserGroups UserGroups = new UserGroups();
        public static Application Application = new Application(){AppName = "TestApp", IdApplication = 1};
        public static RoleCollection Roles = new RoleCollection();
        public static MemberRoles MemberRoles = new MemberRoles();
        public static SecObjectCollection SecObjects = new SecObjectCollection();
        public static GrantCollection Grants = new GrantCollection();
    }

    public static class DatabaseService
    {
        public static int Identity<T>(this BaseCollection<T> collection)
        {
            if (typeof(T) == typeof(Application))
                return collection.OfType<Application>().Max(_ => _.IdApplication) + 1;

            if (typeof(T) == typeof(Group))
                return collection.OfType<Group>().Max(_ => _.IdMember) + 1;

            if (typeof(T) == typeof(Member))
                return collection.OfType<Member>().Max(_ => _.IdMember) + 1;

            if (typeof(T) == typeof(Role))
                return collection.OfType<Role>().Max(_ => _.IdRole) + 1;

            if (typeof(T) == typeof(SecObject))
                return collection.OfType<SecObject>().Max(_ => _.IdSecObject) + 1;

            if (typeof(T) == typeof(User))
                return collection.OfType<User>().Max(_ => _.IdMember) + 1;

            throw new InvalidOperationException("Don't known collection type");
        }
    }
}
