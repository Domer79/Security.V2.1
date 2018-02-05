using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.Tests.SecurityFake;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement.Repository
{
    class UserInternalRepository : IUserInternalRepository
    {
        private readonly IApplicationContext _appContext;

        public UserInternalRepository(IApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public bool CheckAccess(string loginOrEmail, string secObject)
        {
            var user = Database.Users.Get(loginOrEmail);
            var userGroups = Database.UserGroups.GetUserGroups(user);
            var memberRoles = new List<Role>();
            memberRoles.AddRange(Database.MemberRoles.GetMemberRoles(user.AsMember(), _appContext.Application));

            foreach (var userGroup in userGroups)
            {
                memberRoles.AddRange(Database.MemberRoles.GetMemberRoles(userGroup.AsMember(), _appContext.Application));
            }

            var grants = Database.Grants.Where(_ => memberRoles.Select(r => r.IdRole).Contains(_.IdRole)).ToArray();
            return Database.SecObjects.Where(_ => grants.Select(g => g.IdSecObject).Contains(_.IdSecObject))
                .Any(_ => _.ObjectName == secObject);
        }

        public bool SetPassword(string loginOrEmail, string password)
        {
            Database.UserPasswords.SetPassword(loginOrEmail, password);
            return true;
        }

        public bool UserValidate(string loginOrEmail, string password)
        {
            return Database.UserPasswords.UserValidate(loginOrEmail, password);
        }
    }
}
