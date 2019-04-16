using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;

namespace SecurityHttp
{
    /// <summary>
    /// Контекст приложения
    /// </summary>
    public class ApplicationContext : IApplicationContext
    {
        private readonly IApplicationRepository _repo;
        private readonly string _appName;
        private Application _application;

        /// <summary>
        /// Контекст приложения
        /// </summary>
        public ApplicationContext(IApplicationRepository repo, string appName)
        {
            _repo = repo;
            _appName = appName;
        }

        /// <summary>
        /// Приложение
        /// </summary>
        public Application Application => _application ?? (_application = GetApplication());

        private Application GetApplication()
        {
            return _repo.GetByName(_appName);
        }
    }
}
