using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using NLog;
using Tools.Extensions;

namespace Security.WebApi.Infrastructure.Filters
{
    public class ExceptionActionFilter : IAutofacExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionActionFilter()
        {
            _logger = LogManager.GetLogger("Security.WebApi");
        }

        public Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            _logger.Error(actionExecutedContext.Exception);
            actionExecutedContext.Response =
//                actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception);
                actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.GetErrorMessage());

            return Task.FromResult(0);
        }
    }
}