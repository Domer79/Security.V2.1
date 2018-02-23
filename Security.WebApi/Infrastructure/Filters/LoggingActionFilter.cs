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
        private readonly ILogger _logger;

        public LoggingActionFilter()
        {
            _logger = LogManager.GetLogger("Security.WebApi");
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            _logger.Trace($"[{actionContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{actionContext.ActionDescriptor.ActionName}] | {actionContext.Request}");

            return Task.FromResult(0);
        }

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var actionContext = actionExecutedContext.ActionContext;
            _logger.Trace($"[{actionContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{actionContext.ActionDescriptor.ActionName}] | {actionContext.Response}");

            return Task.FromResult(0);
        }
    }
}