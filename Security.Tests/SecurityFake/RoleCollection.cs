using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class RoleCollection : BaseCollection<Role>
    {
        private List<Role> _roles = new List<Role>()
        {
            new Role(){IdRole = 1, Name = "Administrator", IdApplication = Database.Application.IdApplication},
            new Role(){IdRole = 2, Name = "Operator", IdApplication = Database.Application.IdApplication},
        };

        protected override List<Role> Collection => _roles;

        public override void Add(Role item)
        {
            Collection.Add(item);
        }

        public override void Remove(Role item)
        {
            var role = Collection.FirstOrDefault(m => m.IdRole == item.IdRole);
            if (role == null)
                return;

            Collection.Remove(role);
        }

        public override void Update(Role item)
        {
            var role = Collection.FirstOrDefault(m => m.IdRole == item.IdRole);
            if (role == null)
                return;

            role.Description = item.Description;
            role.Name = item.Name;
        }
    }
}