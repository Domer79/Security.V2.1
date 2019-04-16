using System;
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
    /// <summary>
    /// Создание и настройка политик безопасности
    /// </summary>
    public class Config : IConfig
    {
        private readonly IApplicationInternalRepository _applicationRepository;
        private readonly ISecObjectRepository _secObjectRepository;

        /// <summary>
        /// Создание и настройка политик безопасности
        /// </summary>
        public Config(IApplicationInternalRepository applicationRepository, ISecObjectRepository secObjectRepository)
        {
            _applicationRepository = applicationRepository;
            _secObjectRepository = secObjectRepository;
        }

        /// <summary>
        /// Регистрация приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="description"></param>
        public void RegisterApplication(string appName, string description)
        {
            var app = _applicationRepository.GetByName(appName);
            if (app != null)
                return;

            app = new Application { AppName = appName, Description = description };

            _applicationRepository.Create(app);
        }

        /// <summary>
        /// Регистрация приложения
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="description"></param>
        public async Task RegisterApplicationAsync(string appName, string description)
        {
            var app = await _applicationRepository.GetByNameAsync(appName);
            if (app != null)
                return;

            app = new Application { AppName = appName, Description = description };

            await _applicationRepository.CreateAsync(app);
        }

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
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

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
        public void RegisterSecurityObjects(string appName, params string[] securityObjects)
        {
            RegisterSecurityObjects(appName, securityObjects.Select(_ => new SecurityObject() { ObjectName = _ }).ToArray());
        }

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
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

        /// <summary>
        /// Регистрация политик безопасности
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="securityObjects"></param>
        public Task RegisterSecurityObjectsAsync(string appName, params string[] securityObjects)
        {
            return RegisterSecurityObjectsAsync(appName, securityObjects.Select(_ => new SecurityObject() { ObjectName = _ }).ToArray());
        }

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
        public void RemoveApplication(string appName)
        {
            _applicationRepository.Remove(appName);
        }

        /// <summary>
        /// Удаление приложения
        /// </summary>
        /// <param name="appName"></param>
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
