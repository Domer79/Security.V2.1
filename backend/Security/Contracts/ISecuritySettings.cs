﻿using System;
using System.Threading.Tasks;

namespace Security.Contracts
{
    /// <summary>
    /// Управление параметрами для системы доступа
    /// </summary>
    public interface ISecuritySettings
    {
        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Значение типа <see cref="T"/></returns>
        T GetValue<T>(string key);

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="type">Тип значения</param>
        /// <returns>Значение типа <see cref="Object"/></returns>
        object GetValue(string key, Type type);

        /// <summary>
        /// Устанавливает значения для ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <param name="lifetime"></param>
        void SetValue(string key, object value, TimeSpan? lifetime = null);

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsDeprecated(string key);

        /// <summary>
        /// Удаляет значение из кеша
        /// </summary>
        /// <param name="key"></param>
        void RemoveValue(string key);

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Значение типа <see cref="T"/></returns>
        Task<T> GetValueAsync<T>(string key);

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="type">Тип значения</param>
        /// <returns>Значение типа <see cref="Task{TResult}"/></returns>
        Task<object> GetValueAsync(string key, Type type);

        /// <summary>
        /// Устанавливает значения для ключа <see cref="key"/>
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <param name="lifetime"></param>
        Task SetValueAsync(string key, object value, TimeSpan? lifetime = null);

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> IsDeprecatedAsync(string key);

        /// <summary>
        /// Удаляет значение из кеша
        /// </summary>
        /// <param name="key"></param>
        Task RemoveValueAsync(string key);
    }
}