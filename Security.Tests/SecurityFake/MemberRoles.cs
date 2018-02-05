using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class MemberRoles
    {
        private List<Tuple<Member, Role>> _memberRoles = new List<Tuple<Member, Role>>();

        public IEnumerable<Role> GetMemberRoles(Member member, Application app)
        {
            return _memberRoles.Where(t => t.Item1.Id == member.Id && t.Item2.IdApplication == app.IdApplication).Select(t => t.Item2);
        }

        public IEnumerable<Member> GetRoleMembers(Role role)
        {
            return _memberRoles.Where(t => t.Item2.IdRole == role.IdRole).Select(t => t.Item1);
        }

        public void Add(Member member, Role role)
        {
            _memberRoles.Add(new Tuple<Member, Role>(member, role));
        }

        public void Remove(Member member, Role role)
        {
            var tuple = _memberRoles.FirstOrDefault(_ => _.Item1.IdMember == member.IdMember && _.Item2.IdRole == role.IdRole);
            if (tuple == null)
                return;

            _memberRoles.Remove(tuple);
        }

        public void Drop()
        {
            _memberRoles.Clear();
        }
    }
}