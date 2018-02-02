using System;
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
            var id = Database.Members.Identity();

            item.IdMember = id;
            Database.Members.Add(new Member(){Id = item.Id, IdMember = id, Name = item.Name});
            Collection.Add(item);
        }

        public override void Remove(Group item)
        {
            var group = Collection.First(m => m.IdMember == item.IdMember);
            var member = Database.Members.First(m => m.IdMember == item.IdMember);

            if (group == null && member == null)
                return;

            if (group == null || member == null)
                throw new Exception("Группа или участник есть по отдельности");

            Database.Members.Remove(member);
            Collection.Remove(group);
        }

        public override void Update(Group item)
        {
            var group = Collection.FirstOrDefault(m => m.IdMember == item.IdMember);
            var member = Database.Members.First(m => m.IdMember == item.IdMember);

            if (group == null && member == null)
                return;

            if (group == null || member == null)
                throw new Exception("Группа или участник есть по отдельности");

            member.Name = item.Name;
            group.Name = item.Name;
            group.Description = item.Description;
        }
    }
}