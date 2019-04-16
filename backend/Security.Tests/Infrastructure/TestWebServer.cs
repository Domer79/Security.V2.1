using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using NLog;
using Security.WebApi;
using Security.WebApi.App_Start;
using DelegatingHandler = Security.WebApi.App_Start.DelegatingHandler;

namespace Security.Tests.Infrastructure
{
    using Security.WebApi.Infrastructure.DelegatingHandlers;

    public class TestWebServer
    {
        private readonly ILogger _logger;
        private readonly HttpMessageInvoker _messageInvoker;

        public TestWebServer()
        {
            _logger = LogManager.GetLogger(nameof(TestWebServer));
            _messageInvoker = new HttpMessageInvoker(new InMemoryHttpContentSerializationHandler(PrepareServer()));
        }

        public T SendRequest<T>(HttpRequestMessage request)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            _logger.Info(request.Method + ": " + request.RequestUri.AbsoluteUri);
            Console.WriteLine(request.Method + ": " + request.RequestUri.AbsoluteUri);

            using (HttpResponseMessage response = _messageInvoker.SendAsync(request, cts.Token).Result)
            {
                //Assert.IsNotNull(response.Content);
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        if (response.Content.Headers.ContentType.MediaType.Contains("image"))
                        {
                            return default(T);
                        }

                        return response.Content.ReadAsAsync<T>().Result;
                    }

                    return (T)((object)response);
                }

                HandleError(response);
                return default(T);
            }
        }

        public async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            _logger.Info(request.Method + ": " + request.RequestUri.AbsoluteUri);
            Console.WriteLine(request.Method + ": " + request.RequestUri.AbsoluteUri);

            using (HttpResponseMessage response = await _messageInvoker.SendAsync(request, cts.Token))
            {
                //Assert.IsNotNull(response.Content);
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        if (response.Content.Headers.ContentType.MediaType.Contains("image"))
                        {
                            return default(T);
                        }

                        return await response.Content.ReadAsAsync<T>(cts.Token);
                    }

                    return (T)((object)response);
                }

                await HandleErrorAsync(response);
                return default(T);
            }
        }

        private async Task HandleErrorAsync(HttpResponseMessage response)
        {
            string message;

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                try
                {
                    HttpError httpError = await response.Content.ReadAsAsync<HttpError>();
                    message = httpError.ExceptionMessage + " "
                                                         + httpError.Message + " "
                                                         + httpError.MessageDetail + " "
                                                         + httpError.StackTrace + " ";

                    string inner = httpError.ContainsKey("InnerException") ? httpError["InnerException"].ToString() : "";
                    message += inner;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    _logger.Error(ex, message);
                    throw;
                }
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                message = $"Status code: {response.StatusCode}.";
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                message = $"Status code: {response.StatusCode}.";
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }
            Debug.WriteLine(message);
            var exeption = new WebException(message);
            _logger.Error(exeption, "ERROR IN HTTP REQUEST");
            throw exeption;
        }

        private void HandleError(HttpResponseMessage response)
        {
            string message;

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                try
                {
                    HttpError httpError = response.Content.ReadAsAsync<HttpError>().Result;
                    message = httpError.Message;
                    message += httpError.StackTrace;

                    string inner = httpError.ContainsKey("InnerException") ? httpError["InnerException"].ToString() : "";
                    message += inner;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    _logger.Error(ex, message);
                    throw;
                }
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                message = $"Status code: {response.StatusCode}.";
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                message = $"Status code: {response.StatusCode}.";
            }
            else
            {
                message = response.Content.ReadAsStringAsync().Result;
            }
            Debug.WriteLine(message);
            var exception = new WebException(message);
            _logger.Error(exception, "ERROR IN HTTP REQUEST");
            throw exception;
        }

        private HttpServer PrepareServer()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.Services.Replace(typeof(IExceptionHandler), new ExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new ExceptionLogger());

            IocConfig.Register(config);
            WebApiConfig.Register(config);
            DelegatingHandler.Register(config);
            config.EnsureInitialized();

            return new HttpServer(config);
        }
    }
}
