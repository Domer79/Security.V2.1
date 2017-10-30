using Security.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.V2.Contracts
{
    public interface ISecurityDataService
    {
        #region AccessTypes

        IEnumerable<AccessType> GetAccessTypes();

        AccessType GetAccessTypeByName(string name);

        void AddAccessTypes(string[] accessTypes);

        void RemoveAccessTypes(string[] accessTypes);

        void AddIfNotExistAndRemoveIfNotExistInSource(string[] sourceAccessTypes);

        #endregion
    }
}
