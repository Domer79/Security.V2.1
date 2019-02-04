using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Security.CommonContracts;
using Security.Model;
using SecurityHttp.Helpers;
using SecurityHttp.Interfaces;

namespace SecurityHttp
{
    public class CommonWeb : ICommonWeb
    {
        private readonly IGlobalSettings _settings;
        private string _host;

        public CommonWeb(IGlobalSettings settings)
        {
            _settings = settings;
            _host = settings.SecurityServerAddress;
        }

        public T Get<T>(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var rawValue = sr.ReadToEnd();
                        return (T)JsonConvert.DeserializeObject(rawValue, typeof(T));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<T> GetAsync<T>(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var rawValue = await sr.ReadToEndAsync().ConfigureAwait(false);
                        return (T)JsonConvert.DeserializeObject(rawValue, typeof(T));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
                throw;
            }
        }

        public bool Post(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Post;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/json";
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                request.GetResponse();
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public T PostAndGet<T>(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Post;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var value = sr.ReadToEnd();
                        return (T)JsonConvert.DeserializeObject(value, typeof(T));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> PostAsync(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Post;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/json";
            using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await requestStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
            }

            try
            {
                await request.GetResponseAsync().ConfigureAwait(false);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<T> PostAndGetAsync<T>(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Post;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await requestStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
            }

            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var value = await sr.ReadToEndAsync().ConfigureAwait(false);
                        return (T)JsonConvert.DeserializeObject(value, typeof(T));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public bool Put(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Put;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                request.GetResponse();
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> PutAsync(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Put;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await requestStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
            }

            try
            {
                await request.GetResponseAsync().ConfigureAwait(false);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public T PutAndGet<T>(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Put;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var value = sr.ReadToEnd();
                        return (T)JsonConvert.DeserializeObject(value, typeof(T));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<T> PutAndGetAsync<T>(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Put;
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await requestStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
            }

            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var value = await sr.ReadToEndAsync().ConfigureAwait(false);
                        return (T)JsonConvert.DeserializeObject(value, typeof(T));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public IEnumerable<T> GetCollection<T>(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        return (IEnumerable<T>)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(IEnumerable<T>));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<IEnumerable<T>> GetCollectionAsync<T>(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Get;
            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        return (IEnumerable<T>)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(IEnumerable<T>));
                    }
                }
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public bool Delete(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = "DELETE";
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/json";
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                request.GetResponse();
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> DeleteAsync(string path, object data, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = "DELETE";
            var byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/json";
            using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await requestStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
            }

            try
            {
                await request.GetResponseAsync().ConfigureAwait(false);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public bool Delete(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = "DELETE";
            try
            {
                request.GetResponse();
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> DeleteAsync(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = "DELETE";
            try
            {
                await request.GetResponseAsync();
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }
    }

    public class QueryBuilder
    {
        private Dictionary<string, object> _params = new Dictionary<string, object>();

        public QueryBuilder(object data)
        {
            if (data != null)
            {
                var properties = data.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    var value = propertyInfo.GetValue(data);
                    _params[propertyInfo.Name] = HttpUtility.UrlEncode(value?.ToString());
                }
            }
        }

        public override string ToString()
        {
            var aggregateString = _params.Aggregate("", (c, n) => $"{c}&{n.Key}={n.Value}");
            var trim = aggregateString.Trim('&');
            return trim;
        }

        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var keyValuePairs = _params.ToDictionary(pair => pair.Key, pair => pair.ToString());
            return keyValuePairs;
        }
    }
}
