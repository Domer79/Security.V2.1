using Security.Contracts;
using Security.Model;
using ICommonDb = Security.CommonContracts.ICommonDb;

namespace Security.Core
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly ICommonDb _commonDb;
        private readonly string _appName;
        private Application _application;

        public ApplicationContext(ICommonDb commonDb, string appName)
        {
            _commonDb = commonDb;
            _appName = appName;
        }

        private Application GetApplication()
        {
            return _commonDb.QuerySingle<Application>("select * from sec.Applications where appName = @appName", new {appName = _appName});
        }

        public Application Application => _application ?? (_application = GetApplication());
    }
}
