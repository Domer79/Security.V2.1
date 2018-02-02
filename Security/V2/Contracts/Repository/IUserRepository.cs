using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts.Repository.Base;

namespace Security.V2.Contracts.Repository
{
    public interface IUserRepository: ISecurityBaseRepository<User>
    {
        User GetByEmail(string email);
    }
}
