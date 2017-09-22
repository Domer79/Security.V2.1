using System.Reflection;
using Security.Configurations;
using Security.Web;

namespace Security.Manager.App_Start
{
    public class SecurityConfig
    {
        public static void RegisterSecurity()
        {
//            Config.RegisterCommonModule<CommonModule>();
//            Config.RegisterApplication(new ExecSecurityObjects(new []{Assembly.GetExecutingAssembly()}));
        }
    }

    public enum EAccessType
    {
        Exec
    }
}