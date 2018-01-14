using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Interfaces;

namespace Security.V2.Contracts
{
    public interface IConfig
    {
        void RegisterApplication(string appName, string description);
        void RegisterSecurityObjects(params ISecurityObject[] securityObjects);
    }
}
