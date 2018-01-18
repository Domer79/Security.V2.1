using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class GroupCollection : BaseCollection<Group>
    {
        private static List<Member> _members = Database.Members.ToList();

        protected override List<Group> Collection => new List<Group>()
        {
            new Group(){IdMember = _members[5].IdMember, Id = _members[5].Id, Name = _members[6].Name, },
            new Group(){IdMember = _members[6].IdMember, Id = _members[5].Id, Name = _members[6].Name, },
        };

        public override void Add(Group item)
        {
            Collection.Add(item);
        }

        public override void Remove(Group item)
        {
            var group = Collection.FirstOrDefault(m => m.IdMember == item.IdMember);
            if (group == null)
                return;

            Collection.Remove(group);
        }

        public override void Update(Group item)
        {
            var group = Collection.FirstOrDefault(m => m.IdMember == item.IdMember);
            if (group == null)
                return;

            group.Name = item.Name;
            group.Description = item.Description;
        }
    }
}