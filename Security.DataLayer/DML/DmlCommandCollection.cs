using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Interfaces.V2.DataLayer;

namespace Security.DataLayer.DML
{
    public class DmlCommandCollection: IDmlCommandCollection
    {
        private readonly List<ExecuteAction> _actions = new List<ExecuteAction>();

        public void Add(ExecuteAction command)
        {
            _actions.Add(command);
        }

        public IEnumerator<ExecuteAction> GetEnumerator()
        {
            return _actions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
