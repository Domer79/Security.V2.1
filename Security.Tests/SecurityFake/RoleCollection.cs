using System.Collections;
using System.Collections.Generic;
using Security.Model;

namespace Security.Tests.SecurityFakeDatabase
{
    public class RoleCollection : IEnumerable<Role>
    {
        private List<Role> _roles = new List<Role>()
        {
            new Role(){IdRole = 1, Name = "Administrator", IdApplication = Database.Application.IdApplication},
            new Role(){IdRole = 2, Name = "Operator", IdApplication = Database.Application.IdApplication},
        };

        public IEnumerator<Role> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}