using System.Security.Principal;

namespace Security.Interfaces.V2
{
    public interface ISecurityContext
    {
        ISecurityPrincipal Pricipal { get; set; }
        bool CheckAccess();
        bool CheckAccess(string accessType);
    }

    public interface ISecurityPrincipal : IPrincipal
    {
        string ApplicationName { get; }
    }
}