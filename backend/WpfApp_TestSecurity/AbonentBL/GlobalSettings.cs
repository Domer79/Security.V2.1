using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonContracts;

namespace WpfApp_TestSecurity.AbonentBL
{
    public class GlobalSettings : IGlobalSettings
    {
        public string MigrationAssemblyName => throw new NotSupportedException();

        public string DefaultConnectionString => ConfigurationManager.ConnectionStrings["abonentsdb"].ConnectionString;
    }
}
