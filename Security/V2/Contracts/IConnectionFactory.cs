using System;
using System.Data;

namespace Security.V2.Contracts
{
    public interface IConnectionFactory
    {
        Func<IDbConnection> CreateConnection { get; }
    }
}