using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class MemberRoles
    {
        private static List<Member> _members = Database.Members.ToList();
        private static List<Role> _roles = Database.Roles.ToList();

        private List<Tuple<Member, Role>> _memberRoles = new List<Tuple<Member, Role>>()
        {
            new Tuple<Member, Role>(_members[0], _roles[0]),
            new Tuple<Member, Role>(_members[1], _roles[0]),
            new Tuple<Member, Role>(_members[2], _roles[0]),
            new Tuple<Member, Role>(_members[3], _roles[0]),
            new Tuple<Member, Role>(_members[4], _roles[0]),
            new Tuple<Member, Role>(_members[5], _roles[0]),
            new Tuple<Member, Role>(_members[6], _roles[0]),
            new Tuple<Member, Role>(_members[0], _roles[1]),
            new Tuple<Member, Role>(_members[2], _roles[1]),
            new Tuple<Member, Role>(_members[3], _roles[1]),
            new Tuple<Member, Role>(_members[5], _roles[1]),
        };

        public IEnumerable<Member> GetMemberRoles(Member member)
        {
            return _memberRoles.Where(t => t.Item1.Id == member.Id).Select(t => t.Item1);
        }

        public IEnumerable<Role> GetRoleMembers(Role role)
        {
            return _memberRoles.Where(t => t.Item2.IdRole == role.IdRole).Select(t => t.Item2);
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
    }
}