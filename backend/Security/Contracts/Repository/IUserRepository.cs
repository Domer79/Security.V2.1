using System.Threading.Tasks;
using Security.Contracts.Repository.Base;
using Security.Model;

namespace Security.Contracts.Repository
{
    public interface IUserRepository: ISecurityBaseRepository<User>
    {
        new User GetByName(string loginOrEmail);
        new Task<User> GetByNameAsync(string loginOrEmail);
        void SetStatus(string loginOrEmail, bool status);
        Task SetStatusAsync(string loginOrEmail, bool status);
    }
}
