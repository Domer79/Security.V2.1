using System;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class UserCollection : BaseCollection<User>
    {
        private static List<Member> _members = Database.Members.ToList();

        private List<User> _users = new List<User>();

        protected override List<User> Collection => _users;

        public User Get(string loginOrEmail)
        {
            return Collection.Single(_ => _.Login == loginOrEmail || _.Email == loginOrEmail);
        }

        public override void Add(User item)
        {
            var id = Database.Members.Identity();

            item.IdMember = id;
            item.Id = Guid.NewGuid();
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