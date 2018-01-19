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
}
