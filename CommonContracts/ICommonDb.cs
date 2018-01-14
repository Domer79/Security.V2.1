using System;
using System.Collections.Generic;
using System.Data;

namespace CommonContracts
{
    public interface ICommonDb
    {
        string GetConnectionString();
        IDbConnection GetConnection();
        IEnumerable<T> Query<T>(string query, object parameters = null);
        T ExecuteScalar<T>(string query, object parameters = null);
        int ExecuteNonQuery(string query, object parameters = null);
        void CreateDatabaseIfNotExists();
        void GetPageCount(int pageSize, string query, IEntityCollectionInfo collectionInfo);
        IEnumerable<TReturn> Query<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null);
    }
}