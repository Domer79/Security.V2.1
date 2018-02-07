using System.Threading.Tasks;

namespace Security.V2.Contracts.Repository
{
    internal interface IUserInternalRepository
    {
        bool CheckAccess(string loginOrEmail, string secObject);
        bool SetPassword(string loginOrEmail, string password);
        bool UserValidate(string loginOrEmail, string password);

        #region Async

        Task<bool> CheckAccessAsync(string loginOrEmail, string secObject);
        Task<bool> SetPasswordAsync(string loginOrEmail, string password);
        Task<bool> UserValidateAsync(string loginOrEmail, string password);

        #endregion
    }
}