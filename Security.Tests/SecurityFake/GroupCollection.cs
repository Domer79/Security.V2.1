using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFakeDatabase
{
    public class GroupCollection : IEnumerable<Group>
    {
        private static List<Member> _members = Database.Members.ToList();

        private List<Group> _groups = new List<Group>()
        {
            new Group(){IdMember = _members[5].IdMember, Id = _members[5].Id, Name = _members[6].Name, },
            new Group(){IdMember = _members[6].IdMember, Id = _members[5].Id, Name = _members[6].Name, },
        };
        public IEnumerator<Group> GetEnumerator()
        {
            return _groups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}