using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Security.Exceptions;
using Security.Extensions;
using Security.Interfaces;
using Tools.Extensions;
using Security.Interfaces.Collections;
using Security.Model;

namespace Security.Configurations
{
    /// <summary>
    /// Предназначен для регистрации сборки, к которой будет применен механизм ограничения доступа
    /// </summary>
    public class Config
    {
        public const string Exec = "Exec";

        #region Application Register

        /// <summary>
        /// Регистрация приложения с передачей типов доступа, которые необходимо зарегистрировать и списка его объектов безопасности. Наименование приложения ищется в сборке вызывающей данный метод
        /// </summary>
        /// <param name="securityObjectes">Список объектов безопасности</param>
        public static void RegisterApplication(params ISecurityObjects[] securityObjectes)
        {
            var securityAssembly = Assembly.GetCallingAssembly();
            RegisterApplication(securityAssembly, securityObjectes);
        }


        /// <summary>
        /// Регистрация приложения с передачей сборкиб, типов доступа, которые необходимо зарегистрировать и списка его объектов безопасности.
        /// </summary>
        /// <param name="securityAssembly">Сборка, помеченная атрибутом <see cref="AssemblySecurityApplicationInfoAttribute"/></param>
        /// <param name="securityObjectes">Список объектов безопасности</param>
        /// <exception cref="AccessTypeMissingException">Возникает при синхронизации типов доступа, при удалении более не нужных типов, в случае их отсутствия</exception>
        /// <exception cref="AccessTypeValidException">Возникакет при попытке добавить тип доступа, значение строки которого является пустым или null</exception>
        public static void RegisterApplication(Assembly securityAssembly, params ISecurityObjects[] securityObjectes)
        {
            if (securityAssembly == null)
                throw new ArgumentNullException(nameof(securityAssembly));

            if (securityObjectes == null)
                throw new ArgumentNullException(nameof(securityObjectes));

            var securityAppInfo = securityAssembly.GetSecurityInfoFromAssembly();
            SetUpSecurityObjects(securityAppInfo.ApplicationName, securityObjectes);
        }

        #endregion

        #region Helpers

        private static void SetUpSecurityObjects(CoreSecurity security, IEnumerable<string> securityObjects)
        {
            var sameInstalledObjects = security.SecObjectCollection
                .Where(e => securityObjects.Contains(e.ObjectName))
                .Select(e => e.ObjectName);

            var objects = securityObjects as string[] ?? securityObjects.ToArray();
            var newSecObjects = objects.Except(sameInstalledObjects, StringComparer.OrdinalIgnoreCase);

            foreach (var secObject in newSecObjects.Select(s => new SecObject() { ObjectName = s }))
            {
                security.SecObjectCollection.Add(secObject);
            }

            security.SaveChanges();
            DeleteExceptSecObjects(objects, security.SecObjectCollection);
        }



        private static void DeleteExceptSecObjects(IEnumerable<string> securityObjects, ISecObjectCollection objectCollection)
        {
            var secObjects = objectCollection.Where(e => !securityObjects.Contains(e.ObjectName));
            objectCollection.RemoveRange(secObjects);
            objectCollection.SaveChanges();
        }

        /// <summary>
        /// Возвращает список всех зарегистрированных объектов безопасности
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<ISecurityObject> GetSecurityObjects(ISecurityObjects[] securityObjectses)
        {
            var securityObjects = new List<ISecurityObject>();
            foreach (var sObjects in securityObjectses)
            {
                securityObjects.AddRange(sObjects);
            }

            return securityObjects;
        }

        private static void SetUpSecurityObjects(string applicationName, ISecurityObjects[] securityObjectses)
        {
            using (var security = new CoreSecurity(applicationName))
            {
                var securityObjects = GetSecurityObjects(securityObjectses);
                SetUpSecurityObjects(security, securityObjects.Select(so => so.ObjectName));
            }
        }

        #endregion
    }
}
