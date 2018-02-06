using System;
using System.Linq;
using Security.Model;
using Security.V2.Contracts;
using Security.V2.Contracts.Repository;

namespace Security.Tests.SecurityImplement
{
    class Config : IConfig
    {
        private readonly IApplicationInternalRepository _applicationRepository;
        private readonly ISecObjectRepository _secObjectRepository;

        public Config(IApplicationInternalRepository applicationRepository, ISecObjectRepository secObjectRepository)
        {
            _applicationRepository = applicationRepository;
            _secObjectRepository = secObjectRepository;
        }

        public void RegisterApplication(string appName, string description)
        {
            var app = _applicationRepository.GetByName(appName);
            if (app != null)
                return;

            app = new Application{AppName = appName, Description = description};

            _applicationRepository.Create(app);
        }

        public void RegisterSecurityObjects(string appName, params ISecurityObject[] securityObjects)
        {
            var idApplication = _applicationRepository.GetByName(appName).IdApplication;
            foreach (var securityObject in securityObjects)
            {
                var secObject = new SecObject()
                {
                    IdApplication = idApplication,
                    ObjectName = securityObject.ObjectName
                };

                _secObjectRepository.Create(secObject);
            }
        }

        public void RegisterSecurityObjects(string appName, params string[] securityObjects)
        {
            RegisterSecurityObjects(appName, securityObjects.Select(_ => new SecurityObject(){ObjectName = _}).ToArray());
        }

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }
}
