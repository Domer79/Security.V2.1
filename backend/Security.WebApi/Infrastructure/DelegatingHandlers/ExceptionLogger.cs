using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using NLog;
using Security.WebApi.Helpers;

namespace Security.WebApi.Infrastructure.DelegatingHandlers
{
    public class ExceptionLogger : IExceptionLogger
    {
        private ILogger _logger = LogManager.GetLogger("SecurityExceptionLogger");

        public async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            if (context.ExceptionContext.ControllerContext == null)
            {
                _logger.ErrorEx(context.Exception, new
                {
                    context.Request
                });

                return;
            }

            var controllerContext = context.ExceptionContext.ControllerContext;
            var controller = (ApiController)controllerContext.Controller;
            var actionArguments = controller.ActionContext.ActionArguments
                .DictionaryToString();

            _logger.ErrorEx(context.Exception, new
            {
                context.Request,
                controller = controller.ControllerContext.ControllerDescriptor.ControllerName,
                Action = controller.ActionContext.ActionDescriptor.ActionName,
                actionArguments,
            });

            await Task.Delay(0);
        }
    }
}