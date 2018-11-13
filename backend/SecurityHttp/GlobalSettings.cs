using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.CommonContracts;

namespace SecurityHttp
{
    class GlobalSettings : IGlobalSettings
    {
        public string MigrationAssemblyName => throw new NotImplementedException();

        public string DefaultConnectionString => throw new NotImplementedException();

        public string SecurityServerAddress => ConfigurationManager.AppSettings["SecurityServerAddress"];
    }
}
