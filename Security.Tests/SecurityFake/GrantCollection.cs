using System.Collections;
using System.Collections.Generic;
using Security.Model;

namespace Security.Tests.SecurityFakeDatabase
{
    public class GrantCollection : IEnumerable<Grant>
    {
        private List<Grant> _grants = new List<Grant>();

        public IEnumerator<Grant> GetEnumerator()
        {
            return _grants.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Grant grant)
        {
            _grants.Add(new Grant());
        }

        public void Remove(Grant grant)
        {
            _grants.Remove(grant);
        }
    }
}