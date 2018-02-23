using System.Linq;
using System.Threading.Tasks;
using Security.V2.CommonContracts;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;
using Tools.Extensions;

namespace Security.V2.Core.DataLayer.Repositories
{
    public class UserInternalRepository : IUserInternalRepository
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _context;
        private readonly IUserRepository _userRepository;

        public UserInternalRepository(ICommonDb commonDb, IApplicationContext context, IUserRepository userRepository)
        {
            _commonDb = commonDb;
            _context = context;
            _userRepository = userRepository;
        }

        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            return _commonDb.ExecuteScalar<bool>("select sec.IsAllowByName(@secObject, @loginOrEmail, @appName)", new { secObject, loginOrEmail, _context.Application.AppName });
        }

        public Task<bool> CheckAccessAsync(string loginOrEmail, string secObject)
        {
            return _commonDb.ExecuteScalarAsync<bool>("select sec.IsAllowByName(@secObject, @loginOrEmail, @appName)", new { secObject, loginOrEmail, _context.Application.AppName });
        }

        private byte[] GetPassword(string loginOrEmail)
        {
            return _commonDb.QueryFirstOrDefault<byte[]>("select u.password from sec.Users u inner join sec.Members m on u.idMember = m.idMember where m.name = @loginOrEmail or u.email = @loginOrEmail", new {loginOrEmail});
        }

        private Task<byte[]> GetPasswordAsync(string loginOrEmail)
        {
            return _commonDb.QueryFirstOrDefaultAsync<byte[]>("select u.password from sec.Users u inner join sec.Members m on u.idMember = m.idMember where m.name = @loginOrEmail or u.email = @loginOrEmail", new { loginOrEmail });
        }

        public bool SetPassword(string loginOrEmail, string password)
        {
            var hashPassword = password.GetSHA1HashBytes();
            var user = _userRepository.GetByName(loginOrEmail);
            hashPassword = hashPassword.Concat(user.PasswordSalt.GetSHA1HashBytes()).ToArray().GetSHA1HashBytes();
            return _commonDb.ExecuteNonQuery("exec sec.SetPassword @login, @password", new {user.Login, password = hashPassword}) > 0;
        }

        public async Task<bool> SetPasswordAsync(string loginOrEmail, string password)
        {
            var hashPassword = password.GetSHA1HashBytes();
            var user = await _userRepository.GetByNameAsync(loginOrEmail);
            hashPassword = hashPassword.Concat(user.PasswordSalt.GetSHA1HashBytes()).ToArray().GetSHA1HashBytes();
            return await _commonDb.ExecuteNonQueryAsync("exec sec.SetPassword @login, @password", new { user.Login, password = hashPassword }) > 0;
        }

        public bool UserValidate(string loginOrEmail, string password)
        {
            var hashPassword = password.GetSHA1HashBytes();
            var user = _userRepository.GetByName(loginOrEmail);
            hashPassword = hashPassword.Concat(user.PasswordSalt.GetSHA1HashBytes()).ToArray().GetSHA1HashBytes();

            var hashPassword2 = GetPassword(loginOrEmail);

            return hashPassword.SequenceEqual(hashPassword2);
        }

        public async Task<bool> UserValidateAsync(string loginOrEmail, string password)
        {
            var hashPassword = password.GetSHA1HashBytes();
            var user = await _userRepository.GetByNameAsync(loginOrEmail);
            hashPassword = hashPassword.Concat(user.PasswordSalt.GetSHA1HashBytes()).ToArray().GetSHA1HashBytes();

            var hashPassword2 = await GetPasswordAsync(loginOrEmail);

            return hashPassword.SequenceEqual(hashPassword2);
        }
    }
}
