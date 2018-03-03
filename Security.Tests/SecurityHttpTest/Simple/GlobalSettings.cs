using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.V2.CommonContracts;

namespace Security.Tests.SecurityHttpTest.Simple
{
    class GlobalSettings : IGlobalSettings
    {
        public string MigrationAssemblyName => throw new NotImplementedException();

        public string DefaultConnectionString => throw new NotImplementedException();

        public string SecurityServerAddress => "http://localhost:8080/";
    }
}
