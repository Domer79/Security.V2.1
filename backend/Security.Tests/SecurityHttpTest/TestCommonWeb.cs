using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Security.Tests.Infrastructure;
using SecurityHttp;
using SecurityHttp.Helpers;
using SecurityHttp.Interfaces;

namespace Security.Tests.SecurityHttpTest
{
    public class TestCommonWeb: ICommonWeb
    {
        private readonly TestWebServer _webServer = new TestWebServer();
        private readonly string _host = "http://foo/";

        public bool Delete(string path, object queryData = null)
        {
            try
            {
                var queryBuilder = new QueryBuilder(queryData);
                var uriBuilder = new UriBuilder(_host);
                uriBuilder.Path = path;
                uriBuilder.Query = queryBuilder.ToString();
                var request = new HttpRequestMessage(HttpMethod.Delete, uriBuilder.Uri);
                _webServer.SendRequest<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public bool Delete(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Delete;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                _webServer.SendRequest<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public T Get<T>(string path, object queryData = null)
        {
            try
            {
                var queryBuilder = new QueryBuilder(queryData);
                var uriBuilder = new UriBuilder(_host);
                uriBuilder.Path = path;
                uriBuilder.Query = queryBuilder.ToString();
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
                return _webServer.SendRequest<T>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public IEnumerable<T> GetCollection<T>(string path, object queryData = null)
        {
            try
            {
                var queryBuilder = new QueryBuilder(queryData);
                var uriBuilder = new UriBuilder(_host);
                uriBuilder.Path = path;
                uriBuilder.Query = queryBuilder.ToString();
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
                return _webServer.SendRequest<IEnumerable<T>>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public bool Post(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                _webServer.SendRequest<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public bool Put(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Put;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                _webServer.SendRequest<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public T PutAndGet<T>(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Put;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return _webServer.SendRequest<T>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public T PostAndGet<T>(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return _webServer.SendRequest<T>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> DeleteAsync(string path, object queryData = null)
        {
            try
            {
                var queryBuilder = new QueryBuilder(queryData);
                var uriBuilder = new UriBuilder(_host);
                uriBuilder.Path = path;
                uriBuilder.Query = queryBuilder.ToString();
                var request = new HttpRequestMessage(HttpMethod.Delete, uriBuilder.Uri);
                await _webServer.SendRequestAsync<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> DeleteAsync(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Delete;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await _webServer.SendRequestAsync<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public Task<T> GetAsync<T>(string path, object queryData = null)
        {
            try
            {
                var queryBuilder = new QueryBuilder(queryData);
                var uriBuilder = new UriBuilder(_host);
                uriBuilder.Path = path;
                uriBuilder.Query = queryBuilder.ToString();
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
                return _webServer.SendRequestAsync<T>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public Task<IEnumerable<T>> GetCollectionAsync<T>(string path, object queryData = null)
        {
            try
            {
                var queryBuilder = new QueryBuilder(queryData);
                var uriBuilder = new UriBuilder(_host);
                uriBuilder.Path = path;
                uriBuilder.Query = queryBuilder.ToString();
                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
                return _webServer.SendRequestAsync<IEnumerable<T>>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> PostAsync(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await _webServer.SendRequestAsync<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public Task<T> PostAndGetAsync<T>(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return _webServer.SendRequestAsync<T>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public async Task<bool> PutAsync(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Put;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await _webServer.SendRequestAsync<object>(request);
                return true;
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        public Task<T> PutAndGetAsync<T>(string path, object data, object queryData = null)
        {
            try
            {
                var builder = new UriBuilder(_host);
                var queryBuilder = new QueryBuilder(queryData);
                builder.Path = path;
                builder.Query = queryBuilder.ToString();

                var json = "";
                if (queryData != null)
                {
                    json = NewtonsoftJsonSerialize(data);
                    Console.WriteLine(json);
                }

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Put;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return _webServer.SendRequestAsync<T>(request);
            }
            catch (WebException e)
            {
                throw ThrowsHelper.WebException("Произошла ошибка при выполнении запроса", e);
            }
        }

        private static string NewtonsoftJsonSerialize(object objectForSend)
        {
            return JsonConvert.SerializeObject(objectForSend);
        }
    }
}