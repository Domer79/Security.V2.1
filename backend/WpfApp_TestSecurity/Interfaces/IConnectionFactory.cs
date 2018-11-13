using System;
using System.Data;

namespace WpfApp_TestSecurity.Interfaces
{
    public interface IConnectionFactory
    {
        Func<IDbConnection> CreateConnection { get; }
    }
}