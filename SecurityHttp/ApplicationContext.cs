using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Model;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace SecurityHttp
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly IApplicationRepository _repo;
        private readonly string _appName;
        private Application _application;

        public ApplicationContext(IApplicationRepository repo, string appName)
        {
            _repo = repo;
            _appName = appName;
        }

        public Application Application => _application ?? (_application = GetApplication());

        private Application GetApplication()
        {
            return _repo.GetByName(_appName);
        }
    }
}
