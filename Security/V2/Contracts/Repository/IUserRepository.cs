using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository.Base;

namespace Security.V2.Contracts.Repository
{
    public interface IUserRepository: ISecurityBaseRepository<User>
    {
        User Get(string loginOrEmail);
        User GetByEmail(string email);

        #region Async

        Task<User> GetAsync(string loginOrEmail);
        Task<User> GetByEmailAsync(string email);

        #endregion
    }
}
