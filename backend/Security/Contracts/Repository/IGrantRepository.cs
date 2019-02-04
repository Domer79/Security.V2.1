using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Управление разрешениями
    /// </summary>
    public interface IGrantRepository
    {
        /// <summary>
        /// Установка разрешения для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObject">Политика безопасности</param>
        void SetGrant(string role, string secObject);

        /// <summary>
        /// Установка нескольких разрешений для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObjects">Политики безопасности</param>
        void SetGrants(string role, string[] secObjects);

        /// <summary>
        /// Удаление разрешения из роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        void RemoveGrant(string role, string secObject);

        /// <summary>
        /// Удаление нескольких разрешений для роли
        /// </summary>
        /// <param name="role">Роль</param>
        /// <param name="secObjects">Политики безопасности</param>
        void RemoveGrants(string role, string[] secObjects);

        /// <summary>
        /// Получение политик безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IEnumerable<SecObject> GetRoleGrants(string role);

        /// <summary>
        /// Получение отсутствующих у роли политик безопасности
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IEnumerable<SecObject> GetExceptRoleGrant(string role);

        #region Async

        /// <summary>
        /// Установка разрешения для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        Task SetGrantAsync(string role, string secObject);

        /// <summary>
        /// Установка нескольких разрешений для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        Task SetGrantsAsync(string role, string[] secObjects);

        /// <summary>
        /// Удаление разрешения из роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObject"></param>
        /// <returns></returns>
        Task RemoveGrantAsync(string role, string secObject);

        /// <summary>
        /// Удаление нескольких разрешений для роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="secObjects"></param>
        /// <returns></returns>
        Task RemoveGrantsAsync(string role, string[] secObjects);

        /// <summary>
        /// Получение политик безопасности для роли
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<SecObject>> GetRoleGrantsAsync(string role);

        /// <summary>
        /// Получение отсутствующих у роли политик безопасности
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<SecObject>> GetExceptRoleGrantAsync(string role);

        #endregion
    }
}