using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonContracts;
using Security.Model;
using Security.V2.Contracts;

namespace Security.V2.DataLayer
{
    public class SecurityDataService: ISecurityDataService
    {
        private readonly ICommonDb _commonDb;
        private readonly IApplicationContext _applicationContext;

        public SecurityDataService(ICommonDb commonDb, IApplicationContext applicationContext)
        {
            _commonDb = commonDb;
            _applicationContext = applicationContext;
        }

        private void SetAccessTypes(string[] accessTypes)
        {
            if (accessTypes == null)
                throw new ArgumentNullException(nameof(accessTypes));

            if (accessTypes.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(accessTypes));

            var query = "insert into sec.AccessType(idAccessType, name, idApplication) values(1, 'Exec', @idApplication)";

            _commonDb.ExecuteNonQuery(query);
        }


    }
}
