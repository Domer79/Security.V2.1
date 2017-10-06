using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Security.Interfaces.V2;
using Security.Interfaces.V2.DataLayer;
using Security.Model;

namespace Security.BusinessLogic
{
    public class SecurityContext: ISecurityContext
    {
        private readonly ISecurityDataService _securityDataService;

        public SecurityContext(ISecurityDataService securityDataService)
        {
            _securityDataService = securityDataService;
        }

        public ISecurityPrincipal Pricipal { get; set; }

        public bool CheckAccess()
        {
            return _securityDataService.CheckAccess(Pricipal.Identity);
        }

        public bool CheckAccess(string accessType)
        {
            throw new NotImplementedException();
        }


    }

    public class AnonymousUser : User
    {
        public static string Token = "Anonymous";
    }
}
