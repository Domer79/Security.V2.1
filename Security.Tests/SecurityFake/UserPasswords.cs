using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Tools.Extensions;

namespace Security.Tests.SecurityFake
{
    internal class UserPasswords
    {
        private Dictionary<User, byte[]> _passwords = new Dictionary<User, byte[]>();

        public byte[] this[User user]
        {
            get { return _passwords[user]; }
        }

        public byte[] this[string loginOrEmail]
        {
            get
            {
                var user = Database.Users.Single(_ => _.Login == loginOrEmail || _.Email == loginOrEmail);
                return _passwords[user];
            }
        }

        public void SetPassword(string loginOrEmail, string password)
        {
            var hashPassword = password.GetSHA1HashBytes();
            var user = Database.Users.Single(_ => _.Login == loginOrEmail || _.Email == loginOrEmail);
            hashPassword = hashPassword.Concat(user.PasswordSalt.GetSHA1HashBytes()).ToArray().GetSHA1HashBytes();
            _passwords[user] = hashPassword;
        }

        public bool UserValidate(string loginOrEmail, string password)
        {
            var hashPassword = password.GetSHA1HashBytes();
            var user = Database.Users.Single(_ => _.Login == loginOrEmail || _.Email == loginOrEmail);
            hashPassword = hashPassword.Concat(user.PasswordSalt.GetSHA1HashBytes()).ToArray().GetSHA1HashBytes();

            var hashPassword2 = _passwords[user];

            return hashPassword.SequenceEqual(hashPassword2);
        }

        public void Drop()
        {
            _passwords.Clear();
        }
    }
}
