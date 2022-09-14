using Framework.Service.IService;
using System;
using System.Net;
using System.Web.Mvc;

namespace Framework.MainWeb.Filters
{
    public class FrameworkExpcetionFilter : IExceptionFilter
    {
        private readonly ILoggerService _logger;

        public FrameworkExpcetionFilter(ILoggerService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void OnException(ExceptionContext filterContext)
        {
            _logger.FileError(filterContext.Exception.Message);

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.Redirect("~/Error/Index",true);
        }
    }
}