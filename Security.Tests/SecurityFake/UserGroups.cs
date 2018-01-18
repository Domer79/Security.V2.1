using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class UserGroups
    {
        private static List<User> _users = Database.Users.ToList();
        private static List<Group> _groups = Database.Groups.ToList();

        private List<Tuple<User, Group>> _userGroups = new List<Tuple<User, Group>>()
        {
            new Tuple<User, Group>(_users[0], _groups[0]),
            new Tuple<User, Group>(_users[1], _groups[0]),
            new Tuple<User, Group>(_users[2], _groups[0]),
            new Tuple<User, Group>(_users[3], _groups[0]),
            new Tuple<User, Group>(_users[4], _groups[1]),
            new Tuple<User, Group>(_users[5], _groups[1]),
            new Tuple<User, Group>(_users[6], _groups[1]),
            new Tuple<User, Group>(_users[0], _groups[1]),
            new Tuple<User, Group>(_users[1], _groups[1]),
            new Tuple<User, Group>(_users[2], _groups[1]),
            new Tuple<User, Group>(_users[3], _groups[1]),
            new Tuple<User, Group>(_users[4], _groups[0]),
            new Tuple<User, Group>(_users[5], _groups[0]),
            new Tuple<User, Group>(_users[6], _groups[0]),
        };

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
    }
}