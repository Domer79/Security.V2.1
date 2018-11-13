using System;
using System.Data;

namespace Security.Contracts
{
    public interface IConnectionFactory
    {
        Func<IDbConnection> CreateConnection { get; }
    }
}