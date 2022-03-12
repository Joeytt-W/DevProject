using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dev.Web.Filters
{
    public class ExceptionHandleAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ContentResult()
            {
                Content = filterContext.Exception.Message,
                ContentType = "application/json;charset=utf-8"
            };
        }
    }
}