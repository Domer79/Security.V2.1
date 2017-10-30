using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.DataLayer;
using Security.Model;
using Security.V2.Contracts;

namespace Security.V2.DataLayer
{
    public class SecurityDataService: ISecurityDataService
    {
        private readonly CommonDbExecuter _commonDbExecuter;
        private readonly ISecurityContext _securityContext;

        public SecurityDataService(CommonDbExecuter commonDbExecuter, ISecurityContext securityContext)
        {
            _commonDbExecuter = commonDbExecuter;
            _securityContext = securityContext;
        }

        public IEnumerable<AccessType> GetAccessTypes()
        {
            var query = "select idAccessType, name, idApplication from sec.AccessTypes where idApplication = (select idApplication from sec.Applications where appName = @appName)";
            return _commonDbExecuter.Query<AccessType>(query, new QueryParameter("appName", _securityContext.ApplicationName));
        }

        public AccessType GetAccessTypeByName(string name)
        {
            throw new NotImplementedException();
        }

        public void AddAccessTypes(string[] accessTypes)
        {
            throw new NotImplementedException();
        }

        public void RemoveAccessTypes(string[] accessTypes)
        {
            throw new NotImplementedException();
        }

        public void AddIfNotExistAndRemoveIfNotExistInSource(string[] sourceAccessTypes)
        {
            throw new NotImplementedException();
        }
    }
}
