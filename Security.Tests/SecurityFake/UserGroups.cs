using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class UserGroups
    {
        private List<Tuple<User, Group>> _userGroups = new List<Tuple<User, Group>>();

        public IEnumerable<Group> GetUserGroups(User user)
        {
            return _userGroups.Where(t => t.Item1.Id == user.Id).Select(t => t.Item2);
        }

        public IEnumerable<User> GetGroupUsers(Group group)
        {
            return _userGroups.Where(t => t.Item2.Id == group.Id).Select(t => t.Item1);
        }

        public void Add(User user, Group group)
        {
            _userGroups.Add(new Tuple<User, Group>(user, group));
        }

        public void Remove(User user, Group @group)
        {
            var tuple = _userGroups.FirstOrDefault(_ => _.Item1.IdMember == user.IdMember && _.Item2.IdMember == @group.IdMember);
            if (tuple == null)
                return;

            _userGroups.Remove(tuple);
        }

        public void Drop()
        {
            _userGroups.Clear();
        }
    }
}