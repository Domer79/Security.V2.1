using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class MemberCollection : BaseCollection<Member>
    {
        protected override List<Member> Collection => new List<Member>()
        {
            new Member(){Id = MemberGuids.Keys[1], IdMember = 1, Name = "John"},
            new Member(){Id = MemberGuids.Keys[2], IdMember = 2, Name = "Jake"},
            new Member(){Id = MemberGuids.Keys[3], IdMember = 3, Name = "Ivan"},
            new Member(){Id = MemberGuids.Keys[4], IdMember = 4, Name = "Mary"},
            new Member(){Id = MemberGuids.Keys[5], IdMember = 5, Name = "Admins"},
            new Member(){Id = MemberGuids.Keys[6], IdMember = 6, Name = "Users"},
            new Member(){Id = MemberGuids.Keys[7], IdMember = 7, Name = "Public"},
        };

        public override void Add(Member item)
        {
            Collection.Add(item);
        }

        public override void Remove(Member item)
        {
            var member = Collection.FirstOrDefault(m => m.IdMember == item.IdMember);
            if (member == null)
                return;

            Collection.Remove(member);
        }

        public override void Update(Member item)
        {
            var member = Collection.FirstOrDefault(m => m.IdMember == item.IdMember);
            if (member == null)
                return;

            member.Name = item.Name;
        }
    }
}