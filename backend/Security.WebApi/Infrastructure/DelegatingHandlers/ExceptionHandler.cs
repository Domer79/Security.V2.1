using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace Security.WebApi.Infrastructure.DelegatingHandlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private ILogger _logger = LogManager.GetLogger("SecurityExceptionHandler");

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            _logger.Debug($"Handle exception: {context.ExceptionContext.Exception}");
            
            context.Result = new SecurityApiInternalServerError(context.Request, context.Exception);
            return Task.FromResult(context.Result);
        }
    }

    /// <summary>
    /// Общая ошибка сервера
    /// </summary>
    public class SecurityApiInternalServerError: IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly HttpError _httpError;

        public SecurityApiInternalServerError(HttpRequestMessage request, Exception exception)
        {
            _request = request;
            _httpError = new HttpError("Что-то пошло не так! Приносим свои извинения!");

            foreach (DictionaryEntry entry in exception.Data)
            {
                _httpError[entry.Key.ToString()] = entry.Value;
            }

            _httpError.Message = exception.Message;
            _httpError.ExceptionType = exception.GetType().FullName;
            _httpError.StackTrace = exception.StackTrace;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_request.CreateErrorResponse(HttpStatusCode.InternalServerError, _httpError));
        }
    }
}