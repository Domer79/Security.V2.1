using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class SecObjectCollection : BaseCollection<SecObject>
    {
        private List<SecObject> _secObjects = new List<SecObject>();

        protected override List<SecObject> Collection => _secObjects;

        public override void Add(SecObject item)
        {
            item.IdSecObject = Database.SecObjects.Identity();
            Collection.Add(item);
        }

        public override void Remove(SecObject item)
        {
            var secObject = Collection.FirstOrDefault(m => m.IdSecObject == item.IdSecObject);
            if (secObject == null)
                return;

            Collection.Remove(secObject);
        }

        public override void Update(SecObject item)
        {
            var secObject = Collection.FirstOrDefault(m => m.IdSecObject == item.IdSecObject);
            if (secObject == null)
                return;

            secObject.ObjectName = item.ObjectName;
        }
    }
}