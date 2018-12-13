using System.Collections.Generic;
using Security.Model;

namespace Security.Tests.Infrastructure
{
    public class GroupComparer : Comparer<Group>
    {
        public override int Compare(Group x, Group y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
                return x.Id.CompareTo(y.Id);
            if (x.IdMember.CompareTo(y.IdMember) != 0)
                return x.IdMember.CompareTo(y.IdMember);
            if (x.Name.CompareTo(y.Name) != 0)
                return x.Name.CompareTo(y.Name);
            if (x.Description.CompareTo(y.Description) != 0)
                return x.Description.CompareTo(y.Description);

            return 0;
        }
    }
}