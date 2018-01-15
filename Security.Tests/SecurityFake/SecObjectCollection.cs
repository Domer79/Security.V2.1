using System.Collections;
using System.Collections.Generic;
using Security.Model;

namespace Security.Tests.SecurityFakeDatabase
{
    public class SecObjectCollection : IEnumerable<SecObject>
    {
        private List<SecObject> _secObjects = new List<SecObject>()
        {
            new SecObject(){IdApplication = Database.Application.IdApplication, IdSecObject = 1, ObjectName = "Home"},
            new SecObject(){IdApplication = Database.Application.IdApplication, IdSecObject = 2, ObjectName = "Contacts"},
            new SecObject(){IdApplication = Database.Application.IdApplication, IdSecObject = 3, ObjectName = "About"},
        };

        public IEnumerator<SecObject> GetEnumerator()
        {
            return _secObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}