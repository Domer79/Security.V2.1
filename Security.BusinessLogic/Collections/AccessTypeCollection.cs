using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Security.Interfaces.Collections;
using Security.Model;

namespace Security.BusinessLogic.Collections
{
    public class AccessTypeCollection: IAccessTypeCollection
    {
        public IEnumerator<AccessType> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(AccessType item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(AccessType item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(AccessType[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(AccessType item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }
        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(AccessType item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccessType> RemoveRange(IEnumerable<AccessType> items)
        {
            throw new NotImplementedException();
        }
    }
}
