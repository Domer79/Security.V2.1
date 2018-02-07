using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Security.V2.CommonContracts
{
    public interface ICommonDb
    {
        IEnumerable<T> Query<T>(string query, object parameters = null);
        T QueryFirst<T>(string query, object parameters = null);
        T QueryFirstOrDefault<T>(string query, object parameters = null);
        T QuerySingle<T>(string query, object parameters = null);
        T QuerySingleOrDefault<T>(string query, object parameters = null);
        T ExecuteScalar<T>(string query, object parameters = null);
        int ExecuteNonQuery(string query, object parameters = null);
        void GetPageCount(int pageSize, string query, IEntityCollectionInfo collectionInfo);
        IEnumerable<TReturn> Query<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null);

        #region Async

        Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null);
        Task<T> QueryFirstAsync<T>(string query, object parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null);
        Task<T> QuerySingleAsync<T>(string query, object parameters = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null);
        Task<T> ExecuteScalarAsync<T>(string query, object parameters = null);
        Task<int> ExecuteNonQueryAsync(string query, object parameters = null);
        Task GetPageCountAsync(int pageSize, string query, IEntityCollectionInfo collectionInfo);
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null);

        #endregion
    }
}