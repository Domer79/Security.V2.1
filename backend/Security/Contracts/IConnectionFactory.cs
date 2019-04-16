using System;
using System.Data;

namespace Security.Contracts
{
    /// <summary>
    /// Управление соединением с БД
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Создание соединения с БД
        /// </summary>
        Func<IDbConnection> CreateConnection { get; }
    }
}