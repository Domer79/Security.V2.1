using System;
using System.Data;

namespace CommonContracts
{
    public interface IConnectionFactory
    {
        Func<IDbConnection> CreateConnection { get; }
    }
}