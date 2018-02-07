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
        void RegisterSecurityObjects(string appName, params ISecurityObject[] securityObjects);
        void RegisterSecurityObjects(string appName, params string[] securityObjects);

        #region Async

        Task RegisterApplicationAsync(string appName, string description);
        Task RegisterSecurityObjectsAsync(string appName, params ISecurityObject[] securityObjects);
        Task RegisterSecurityObjectsAsync(string appName, params string[] securityObjects);

        #endregion
    }
}
