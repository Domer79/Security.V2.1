using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Security.Model;
using Security.V2.CommonContracts;
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

        public bool Delete(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = "DELETE";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                return response.StatusCode == HttpStatusCode.OK;
            }
        }

        public async Task<bool> DeleteAsync(string path, object queryData = null)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Get;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        return (T) JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(T));
                    }
                }

                throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);
            }
        }

        public async Task<object> GetAsync<T>(string path, object queryData = null)
        {
            throw new NotImplementedException();
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

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);

                return true;
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

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var value = sr.ReadToEnd();
                        return (T)JsonConvert.DeserializeObject(value, typeof(T));
                    }
                }

                throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);
            }
        }

        public Task<T> PostAndGetAsync<T>(string path, object data, object queryData = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostAsync(string path, object data, object queryData = null)
        {
            throw new NotImplementedException();
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

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }

                throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);
            }
        }

        public Task<bool> PutAsync(string path, object data, object queryData = null)
        {
            throw new NotImplementedException();
        }

        Task<T> ICommonWeb.GetAsync<T>(string path, object queryData)
        {
            throw new NotImplementedException();
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

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var value = sr.ReadToEnd();
                        return (T)JsonConvert.DeserializeObject(value, typeof(T));
                    }
                }

                throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);
            }
        }

        public Task<T> PutAndGetAsync<T>(string path, object data, object queryData = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetCollection<T>(string path, object queryData = null)
        {
            var builder = new UriBuilder(_host);
            var queryBuilder = new QueryBuilder(queryData);
            builder.Path = path;
            builder.Query = queryBuilder.ToString();

            var request = WebRequest.Create(builder.Uri);
            request.Method = WebRequestMethods.Http.Get;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        return (IEnumerable<T>)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(IEnumerable<T>));
                    }
                }

                throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);
            }
        }

        public Task<IEnumerable<T>> GetCollectionAsync<T>(string path, object queryDsta = null)
        {
            throw new NotImplementedException();
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

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);

                return true;
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

            using (HttpWebResponse response = await request.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw ThrowsHelper.WebExceptionThrow("Произошла ошибка при выполнении запроса", response);

                return true;
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
                    _params[propertyInfo.Name] = propertyInfo.GetValue(data);
                }
            }
        }

        public override string ToString()
        {
            var aggregateString = _params.Aggregate("", (c, n) => $"{c}&{n.Key}={n.Value}");
            var trim = aggregateString.Trim('&');
            return trim;
        }
    }
}
