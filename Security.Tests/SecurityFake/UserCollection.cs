using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class UserCollection : BaseCollection<User>
    {
        private static List<Member> _members = Database.Members.ToList();

        private List<User> _users = new List<User>()
        {
            new User(){IdMember = _members[0].IdMember, Id = _members[0].Id, Login = _members[0].Name, },
            new User(){IdMember = _members[1].IdMember, Id = _members[1].Id, Login = _members[1].Name, },
            new User(){IdMember = _members[2].IdMember, Id = _members[2].Id, Login = _members[2].Name, },
            new User(){IdMember = _members[3].IdMember, Id = _members[3].Id, Login = _members[3].Name, },
            new User(){IdMember = _members[4].IdMember, Id = _members[4].Id, Login = _members[4].Name, },
        };

        protected override List<User> Collection => _users;

        public override void Add(User item)
        {
            var id = Database.Members.Identity();

            item.IdMember = id;
            Database.Members.Add(new Member() { Id = item.Id, IdMember = id, Name = item.Name });
            Collection.Add(item);
        }

        public override void Remove(User item)
        {
            var user = Collection.First(m => m.IdMember == item.IdMember);
            var member = Database.Members.First(m => m.IdMember == item.IdMember);

            if (user == null && member == null)
                return;

            if (user == null || member == null)
                throw new Exception("Группа или участник есть по отдельности");

            Database.Members.Remove(member);
            Collection.Remove(user);
        }

        public override void Update(User item)
        {
            var user = Collection.FirstOrDefault(m => m.IdMember == item.IdMember);
            var member = Database.Members.First(m => m.IdMember == item.IdMember);

            if (user == null && member == null)
                return;

            if (user == null || member == null)
                throw new Exception("Группа или участник есть по отдельности");

            user.Name = item.Name;
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.MiddleName = item.MiddleName;
            user.Email = item.Email;
        }
    }
}