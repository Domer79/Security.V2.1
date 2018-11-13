using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityHttp.Interfaces
{
    public interface ICommonWeb
    {
        bool Delete(string path, object queryData = null);
        bool Delete(string path, object data, object queryData = null);
        T Get<T>(string path, object queryData = null);
        IEnumerable<T> GetCollection<T>(string path, object queryDsta = null);
        bool Post(string path, object data, object queryData = null);
        bool Put(string path, object data, object queryData = null);
        T PutAndGet<T>(string path, object data, object queryData = null);
        T PostAndGet<T>(string path, object data, object queryData = null);

        Task<bool> DeleteAsync(string path, object queryData = null);
        Task<bool> DeleteAsync(string path, object data, object queryData = null);
        Task<T> GetAsync<T>(string path, object queryData = null);
        Task<IEnumerable<T>> GetCollectionAsync<T>(string path, object queryDsta = null);
        Task<bool> PostAsync(string path, object data, object queryData = null);
        Task<T> PostAndGetAsync<T>(string path, object data, object queryData = null);
        Task<bool> PutAsync(string path, object data, object queryData = null);
        Task<T> PutAndGetAsync<T>(string path, object data, object queryData = null);
    }
}