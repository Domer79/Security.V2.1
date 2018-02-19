using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_TestSecurity.ViewModels;
using static AutoCompare.Comparer;

namespace WpfApp_TestSecurity
{
    public class Compare
    {
        public static void Config()
        {
            Configure<UserViewModel>().Compile.Now();
            Configure<RoleViewModel>().Compile.Now();
            Configure<PolicyViewModel>().Compile.Now();
        }
    }
}
