using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Model;

namespace Security.V2.Contracts
{
    /// <summary>
    /// Интерфейс настроечных параметров для системы доступа
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
        /// Устанавливает значения для ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        void SetValue<T>(string key, T value, TimeSpan? lifetime = null);

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsDeprecated(string key);

        /// <summary>
        /// Возвращает значение переданного ключа <see cref="key"/>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Значение типа <see cref="T"/></returns>
        Task<T> GetValueAsync<T>(string key);

        /// <summary>
        /// Устанавливает значения для ключа <see cref="key"/>. Если значение <see cref="lifetime"/> null, то считаем, что время жизни данного значения неограниченно
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        Task SetValueAsync<T>(string key, T value, TimeSpan? lifetime = null);

        /// <summary>
        /// Проверяет устарело ли значение в кеше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> IsDeprecatedAsync(string key);
    }
}