using System.Threading.Tasks;
using Security.Contracts.Repository.Base;
using Security.Model;

namespace Security.Contracts.Repository
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    public interface IUserRepository: ISecurityBaseRepository<User>
    {
        /// <summary>
        /// Возвращает пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        new User GetByName(string loginOrEmail);

        /// <summary>
        /// Устанавливает статус пользователю
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="status"></param>
        void SetStatus(string loginOrEmail, bool status);

        #region Async

        /// <summary>
        /// Возвращает пользователя по логину или email
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <returns></returns>
        new Task<User> GetByNameAsync(string loginOrEmail);

        /// <summary>
        /// Устанавливает статус пользователю
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task SetStatusAsync(string loginOrEmail, bool status);

        #endregion
    }
}
