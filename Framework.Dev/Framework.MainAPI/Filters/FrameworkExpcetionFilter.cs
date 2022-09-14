using Framework.Service.IService;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Framework.MainAPI.Filters
{
    public class FrameworkExpcetionFilter : IExceptionFilter
    {
        private readonly ILoggerService _logger;

        public FrameworkExpcetionFilter(ILoggerService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool AllowMultiple => true;

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            _logger.FileError(actionExecutedContext.Exception.Message);

            actionExecutedContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.InternalServerError);

            return Task.CompletedTask;
        }
    }
}