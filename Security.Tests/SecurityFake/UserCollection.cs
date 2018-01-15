using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFakeDatabase
{
    public class UserCollection : IEnumerable<User>
    {
        private static List<Member> _members = Database.Members.ToList();

        private List<User> _users = new List<User>()
        {
            new User(){IdMember = _members[0].IdMember, Id = _members[0].Id, Login = _members[0].Name, },
            new User(){IdMember = _members[1].IdMember, Id = _members[1].Id, Login = _members[1].Name, },
            new User(){IdMember = _members[2].IdMember, Id = _members[2].Id, Login = _members[2].Name, },
            new User(){IdMember = _members[3].IdMember, Id = _members[3].Id, Login = _members[3].Name, },
            new User(){IdMember = _members[4].IdMember, Id = _members[4].Id, Login = _members[4].Name, },
        };

        public IEnumerator<User> GetEnumerator()
        {
            return _users.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}