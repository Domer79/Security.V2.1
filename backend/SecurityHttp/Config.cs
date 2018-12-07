﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Contracts;
using Security.Contracts.Repository;
using Security.Model;
using SecurityHttp.Interfaces;

namespace SecurityHttp
{
    public class Config : IConfig
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

            app = new Application { AppName = appName, Description = description };

            _applicationRepository.Create(app);
        }

        public async Task RegisterApplicationAsync(string appName, string description)
        {
            var app = await _applicationRepository.GetByNameAsync(appName);
            if (app != null)
                return;

            app = new Application { AppName = appName, Description = description };

            await _applicationRepository.CreateAsync(app);
        }

        public void RegisterSecurityObjects(string appName, params ISecurityObject[] securityObjects)
        {
            foreach (var securityObject in securityObjects)
            {
                var secObject = _secObjectRepository.GetByName(securityObject.ObjectName);
                if (secObject != null)
                    continue;

                secObject = new SecObject()
                {
                    ObjectName = securityObject.ObjectName
                };

                _secObjectRepository.Create(secObject);
            }
        }

        public void RegisterSecurityObjects(string appName, params string[] securityObjects)
        {
            RegisterSecurityObjects(appName, securityObjects.Select(_ => new SecurityObject() { ObjectName = _ }).ToArray());
        }

        public async Task RegisterSecurityObjectsAsync(string appName, params ISecurityObject[] securityObjects)
        {
            foreach (var securityObject in securityObjects)
            {
                var secObject = await _secObjectRepository.GetByNameAsync(securityObject.ObjectName);
                if (secObject != null)
                    continue;

                secObject = new SecObject()
                {
                    ObjectName = securityObject.ObjectName
                };

                await _secObjectRepository.CreateAsync(secObject);
            }
        }

        public Task RegisterSecurityObjectsAsync(string appName, params string[] securityObjects)
        {
            return RegisterSecurityObjectsAsync(appName, securityObjects.Select(_ => new SecurityObject() { ObjectName = _ }).ToArray());
        }

        public void RemoveApplication(string appName)
        {
            _applicationRepository.Remove(appName);
        }

        public Task RemoveApplicationAsync(string appName)
        {
            return _applicationRepository.RemoveAsync(appName);
        }

        class SecurityObject : ISecurityObject
        {
            public string ObjectName { get; set; }
        }
    }
}
