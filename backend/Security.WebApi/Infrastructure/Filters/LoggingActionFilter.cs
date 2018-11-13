using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NLog;

namespace Security.WebApi.Infrastructure.Filters
{
    public class LoggingActionFilter : IAutofacActionFilter
    {
        private readonly ILogger _requestLogger;
        private readonly ILogger _responseLogger;

        public LoggingActionFilter()
        {
            _requestLogger = LogManager.GetLogger("Security.WebApi.Request");
            _responseLogger = LogManager.GetLogger("Security.WebApi.Response");
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            _requestLogger.Trace($"[{actionContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{actionContext.ActionDescriptor.ActionName}] | {actionContext.Request}");

            return Task.FromResult(0);
        }

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var actionContext = actionExecutedContext.ActionContext;
            _responseLogger.Trace($"[{actionContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{actionContext.ActionDescriptor.ActionName}] | {actionContext.Response}");

            return Task.FromResult(0);
        }
    }
}