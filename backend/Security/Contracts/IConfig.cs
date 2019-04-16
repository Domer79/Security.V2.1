using System.Threading.Tasks;

namespace Security.Contracts
{
    /// <summary>
    /// Создание и настройка политик безопасности
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Регистрация приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="description"></param>
        void RegisterApplication(string appName, string description);

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
        void RegisterSecurityObjects(string appName, params ISecurityObject[] securityObjects);

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
        void RegisterSecurityObjects(string appName, params string[] securityObjects);

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        void RemoveApplication(string appName);

        #region Async

        /// <summary>
        /// Регистрация приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="description"></param>
        Task RegisterApplicationAsync(string appName, string description);

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
        Task RegisterSecurityObjectsAsync(string appName, params ISecurityObject[] securityObjects);

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
        Task RegisterSecurityObjectsAsync(string appName, params string[] securityObjects);

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        Task RemoveApplicationAsync(string appName);

        #endregion
    }
}
