using System.Collections;
using System.Collections.Generic;
using Security.Model;

namespace Security.Tests.SecurityFakeDatabase
{
    public class MemberCollection : IEnumerable<Member>
    {
        private List<Member> _members = new List<Member>()
        {
            new Member(){Id = MemberGuids.Keys[1], IdMember = 1, Name = "John"},
            new Member(){Id = MemberGuids.Keys[2], IdMember = 2, Name = "Jake"},
            new Member(){Id = MemberGuids.Keys[3], IdMember = 3, Name = "Ivan"},
            new Member(){Id = MemberGuids.Keys[4], IdMember = 4, Name = "Mary"},
            new Member(){Id = MemberGuids.Keys[5], IdMember = 5, Name = "Admins"},
            new Member(){Id = MemberGuids.Keys[6], IdMember = 6, Name = "Users"},
            new Member(){Id = MemberGuids.Keys[7], IdMember = 7, Name = "Public"},
        };

        public IEnumerator<Member> GetEnumerator()
        {
            return _members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}