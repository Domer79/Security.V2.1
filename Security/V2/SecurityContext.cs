using Security.V2.Contracts;

namespace Security.V2
{
    public class SecurityContext : ISecurityContext
    {
        public string ApplicationName { get; set; }

        public static SecurityContext Current { get; set; }
    }
}
