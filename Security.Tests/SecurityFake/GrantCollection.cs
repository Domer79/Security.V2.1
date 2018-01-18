using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Security.Model;

namespace Security.Tests.SecurityFake
{
    public class GrantCollection : BaseCollection<Grant>
    {
        protected override List<Grant> Collection => new List<Grant>();

        public override void Add(Grant grant)
        {
            Collection.Add(new Grant());
        }

        public override void Remove(Grant item)
        {
            var grant = Collection.FirstOrDefault(_ => _.IdRole == item.IdRole && _.IdSecObject == item.IdSecObject);
            if (grant == null)
                return;

            Collection.Remove(grant);
        }

        public override void Update(Grant item)
        {
            throw new NotSupportedException();
        }
    }
}