using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Security.WebApi.Infrastructure
{
    public class InternalServerErrorResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private HttpError _httpError;

        public InternalServerErrorResult(HttpRequestMessage request, Exception exception)
        {
            _request = request;
            _httpError = new HttpError("Что-то пошло не так! Приносим свои извиннения!");
            _httpError.Message = exception.Message;
            _httpError.MessageDetail = exception.StackTrace;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_request.CreateErrorResponse(HttpStatusCode.InternalServerError, _httpError));
        }
    }
}