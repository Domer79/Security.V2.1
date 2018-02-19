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

        public static string[] GetAllPolicies()
        {
            var members = typeof(Policy).GetFields().Where(fi => fi.FieldType == typeof(Policy));
            return members.Select(fi => fi.Name).ToArray();
        }
    }
}
