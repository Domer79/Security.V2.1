using System.Collections.Generic;

namespace Security.Interfaces.V2.DataLayer
{
    public interface IDmlCommandCollection: IEnumerable<ExecuteAction>
    {
        void Add(ExecuteAction command);
    }
}