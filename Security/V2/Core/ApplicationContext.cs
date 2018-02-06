using System;
using Security.Model;
using Security.V2.Contracts;
using ICommonDb = Security.V2.CommonContracts.ICommonDb;

namespace Security.V2.Core
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly ICommonDb _commonDb;

        public ApplicationContext(ICommonDb commonDb)
        {
            _commonDb = commonDb;
            Application = GetApplication();
        }

        private Application GetApplication()
        {
            throw new NotImplementedException();
        }

        public Application Application { get; }
    }
}
