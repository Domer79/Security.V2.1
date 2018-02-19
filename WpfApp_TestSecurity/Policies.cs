using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_TestSecurity
{
    public class Policies
    {
        public const string AbonentsPage = "AbonentsPage";

        public static string[] GetAllPolicies()
        {
            var members = typeof(Policies).GetFields().Where(fi => fi.IsLiteral && !fi.IsInitOnly);
            return members.Select(fi => (string)fi.GetValue(null)).ToArray();
        }
    }
}
