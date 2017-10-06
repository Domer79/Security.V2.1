using System.Collections.Generic;
using Security.Model;

namespace Security.Interfaces.V2
{
    public interface IAccessTypeService
    {
        void RegisterAccessTypes(IEnumerable<string> accessTypes);
        IEnumerable<AccessType> GetAccessTypeByName(string name);

    }
}