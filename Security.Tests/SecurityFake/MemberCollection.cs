using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class MemberCollection : BaseCollection<Member>
    {
        private List<Member> _collection;
        protected override List<Member> Collection => _collection ?? (_collection = new List<Member>());

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