using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Dev.CoreApi.Filters
{
    public class CustomAuthsizeFilter : AuthorizeAttribute
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                //context.Result = new ContentResult() { StatusCode = (int)HttpStatusCode.OK, Content = "验证通过" };
            }
            else
            {
                context.Result = new ContentResult() { StatusCode = (int)HttpStatusCode.Forbidden, Content = "你没有访问该资源的权限" };
            }
        }
    }
}
