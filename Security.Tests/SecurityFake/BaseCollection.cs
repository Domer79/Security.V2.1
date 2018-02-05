using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Tests.SecurityFake
{
    public abstract class BaseCollection<T> : IEnumerable<T>
    {
        protected abstract List<T> Collection { get; }

        public abstract void Add(T item);

        public abstract void Remove(T item);

        public abstract void Update(T item);

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Drop()
        {
            Collection.Clear();
        }
    }
}
