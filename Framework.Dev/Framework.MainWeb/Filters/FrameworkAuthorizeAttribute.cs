using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Framework.MainWeb.Filters
{
    public class FrameworkAuthorizeAttribute:AuthorizeAttribute
    {
        /// <summary>
        /// 验证入口
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {


            //前端请求api时会将token存放在名为"auth"的请求头中
            var authHeader = filterContext.HttpContext.Request.Headers["auth"];
            if (authHeader == null)
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new RedirectResult("/Auth");
                filterContext.HttpContext.Response.Redirect("/Auth/Login");
            }
            else
            {
                #region 权限校验规则
                IEnumerable<Claim> claims = JWTTools.Decode(authHeader.Split(' ')[1], out string msg);
                //举个例子      
                //这个地方用jwtToken当作key 获取实体val   然后看看jwtToken根据redis是否一样
                if (claims.FirstOrDefault(c => c.ValueType == ClaimTypes.Name).Value == "admin")// && claims.FirstOrDefault(c => c.ValueType == ClaimTypes.Role).Value == "123"
                    return;
                else
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new RedirectResult("/Auth");
                    filterContext.HttpContext.Response.Redirect("/Auth/Login");
                }
                   
                #endregion
            }


        }
    }
}