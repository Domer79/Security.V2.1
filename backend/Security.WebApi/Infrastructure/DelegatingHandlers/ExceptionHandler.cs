using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
            
            context.Result = new InternalServerErrorResult(context.Request, context.Exception);
            return Task.FromResult(context.Result);
        }
    }
}