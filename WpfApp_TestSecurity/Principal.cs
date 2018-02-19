using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Security.V2.Contracts;

namespace WpfApp_TestSecurity
{
    public static class Principal
    {
        public static IIdentity Identity { get; set; } = new AnonymousIdentity();

        public static void LogIn(string login, string password)
        {
            var security = IocConfig.Resolve<ISecurity>();
            if (security.UserValidate(login, password))
            {
                Identity = new UserIdentity(login);
                return;
            }
        }

        public static bool CheckAccess(string policy)
        {
            var security = IocConfig.Resolve<ISecurity>();
            return security.CheckAccess(Identity.Name, policy);
        }
    }

    public class UserIdentity : IIdentity
    {
        private readonly string _login;

        public UserIdentity(string login)
        {
            _login = login;
        }

        public string Name => _login;

        public string AuthenticationType => "Forms";

        public bool IsAuthenticated { get; protected set; } = true;
    }

    public class AnonymousIdentity: UserIdentity
    {
        public AnonymousIdentity() : base("anonymous")
        {
            IsAuthenticated = false;
        }
    }
}
