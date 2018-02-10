using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository.Base;

namespace Security.V2.Contracts.Repository
{
    public interface IUserRepository: ISecurityBaseRepository<User>
    {
        new User GetByName(string loginOrEmail);
        new Task<User> GetByNameAsync(string loginOrEmail);
        void SetStatus(string loginOrEmail, bool status);
        Task SetStatusAsync(string loginOrEmail, bool status);
    }
}
