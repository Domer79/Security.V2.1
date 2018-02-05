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
    }

    internal interface IUserInternalRepository
    {
        bool CheckAccess(string loginOrEmail, string secObject);
        bool SetPassword(string loginOrEmail, string password);
        bool UserValidate(string loginOrEmail, string password);
    }
}
