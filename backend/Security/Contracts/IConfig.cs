using System.Threading.Tasks;

namespace Security.Contracts
{
    public interface IConfig
    {
        void RegisterApplication(string appName, string description);
        void RegisterSecurityObjects(string appName, params ISecurityObject[] securityObjects);
        void RegisterSecurityObjects(string appName, params string[] securityObjects);
        void RemoveApplication(string appName);

        #region Async

        Task RegisterApplicationAsync(string appName, string description);
        Task RegisterSecurityObjectsAsync(string appName, params ISecurityObject[] securityObjects);
        Task RegisterSecurityObjectsAsync(string appName, params string[] securityObjects);
        Task RemoveApplicationAsync(string appName);

        #endregion
    }
}
