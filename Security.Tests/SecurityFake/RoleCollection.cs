using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class RoleCollection : BaseCollection<Role>
    {
        private List<Role> _collection;
        protected override List<Role> Collection => _collection ?? (_collection = new List<Role>());

        public override void Add(Role item)
        {
            item.IdRole = Database.Roles.Identity();
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