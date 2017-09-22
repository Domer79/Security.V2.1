﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Interfaces.Base;
using Security.Model;

namespace Security.Interfaces.Collections
{
    /// <summary>
    /// Интерфейс, представляющий собой коллекцию разрешений
    /// </summary>    
    public interface IGrantCollection : IQueryableCollection<Grant>
    {
        /// <summary>
        /// Добавляет новое разрешение в базу данных
        /// </summary>
        /// <param name="roleName">Имя роль</param>
        /// <param name="secObjectName">Наименование объекта безопасности</param>
        /// <param name="accessTypeName">Наименование типа доступа</param>
        /// <param name="applicationName"></param>
        Grant Add(string roleName, string secObjectName, string accessTypeName, string applicationName);

        /// <summary>
        /// Добавляет новое разрешение в базу данных
        /// </summary>
        /// <param name="roleName">Имя роль</param>
        /// <param name="secObjects">Наименования объектов безопасности</param>
        /// <param name="accessTypes">Наименования типов доступа</param>3
        /// <param name="applicationName"></param>
        IEnumerable<Grant> AddRange(string roleName, SecObject[] secObjects, AccessType[] accessTypes, string applicationName);

        /// <summary>
        /// Удаляет разрешение из базы данных
        /// </summary>
        /// <param name="roleName">Имя роль</param>
        /// <param name="secObjectName">Наименование объекта безопасности</param>
        /// <param name="accessTypeName">Наименование типа доступа</param>
        /// <param name="applicationName"></param>
        /// <returns>Количество удаленных элементов</returns>
        Task<bool> RemoveAsync(string roleName, string secObjectName, string accessTypeName,
            string applicationName);

        /// <summary>
        /// Удаляет разрешение из базы данных
        /// </summary>
        /// <param name="roleName">Имя роль</param>
        /// <param name="secObjectName">Наименование объекта безопасности</param>
        /// <param name="accessTypeName">Наименование типа доступа</param>
        /// <param name="applicationName"></param>
        /// <returns>Количество удаленных элементов</returns>
        bool Remove(string roleName, string secObjectName, string accessTypeName, string applicationName);

        /// <summary>
        /// Добавляет новое разрешение в базу данных
        /// </summary>
        /// <param name="roleName">Имя роль</param>
        /// <param name="secObjectName">Наименование объекта безопасности</param>
        /// <param name="accessType">Тип доступа, представленный в виде <see cref="Enum"/></param>
        /// <param name="applicationName"></param>
        Grant Add(string roleName, string secObjectName, Enum accessType, string applicationName);

        /// <summary>
        /// Удаляет разрешение из базы данных
        /// </summary>
        /// <param name="roleName">Имя роль</param>
        /// <param name="secObjectName">Наименование объекта безопасности</param>
        /// <param name="accessType">Тип доступа, представленный в виде <see cref="Enum"/></param>
        /// <param name="applicationName"></param>
        /// <returns>Количество удаленных элементов</returns>
        bool Remove(string roleName, string secObjectName, Enum accessType, string applicationName);

        /// <summary>
        /// Производит удаление множества элементов
        /// </summary>
        /// <param name="roleNames"></param>
        /// <param name="secObjects"></param>
        /// <param name="accessTypes"></param>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        IEnumerable<Grant> RemoveRange(string[] roleNames, string[] secObjects, string[] accessTypes, string applicationName);

        /// <summary>
        /// Возвращает объект разрешения по наименованию объекта безопасности, роли, типа доступа и приложения
        /// </summary>
        /// <param name="secObject">Объект безопасности</param>
        /// <param name="role">Роль</param>
        /// <param name="accessType">Тип доступа</param>
        /// <param name="appName">Имя приложения</param>
        /// <returns></returns>
        Grant Get(string secObject, string role, string accessType, string appName);

        /// <summary>
        /// Асинхронно Возвращает объект разрешения по наименованию объекта безопасности, роли, типа доступа и приложения
        /// </summary>
        /// <param name="secObject"></param>
        /// <param name="role"></param>
        /// <param name="accessType"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        Task<Grant> GetAsync(string secObject, string role, string accessType, string appName);
    }
}