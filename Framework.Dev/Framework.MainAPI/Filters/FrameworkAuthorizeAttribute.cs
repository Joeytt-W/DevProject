using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Framework.MainAPI.Filters
{
    public class FrameworkAuthorizeAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //前端请求api时会将token存放在名为"Authorization"的请求头中
            if (!actionContext.Request.Headers.Contains("Authorization"))
            {
                HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                HttpContext.Current.Response.Write("未包含Authorization请求头");
                return;
            }

            var authHeader = actionContext.Request.Headers.FirstOrDefault(c => c.Key == "Authorization").Value.FirstOrDefault();
            if (authHeader == null)
            {
                HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                HttpContext.Current.Response.Write("未包含Authorization请求头");
            }
            else
            {
                #region 权限校验规则
                string pattern = "^Bearer (.*?)$";
                if (!Regex.IsMatch(authHeader, pattern))
                {
                    HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    HttpContext.Current.Response.Write("token生成规则不正确");
                    return;
                }

                authHeader = Regex.Match(authHeader, pattern).Groups[1]?.ToString();
                if (authHeader == "null" || string.IsNullOrEmpty(authHeader))
                {
                    HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    HttpContext.Current.Response.Write("token生成规则不正确");
                    return;
                }

                IEnumerable<Claim> claims = JWTTools.Decode(authHeader, out string msg);

                if(!string.IsNullOrWhiteSpace(msg))
                {
                    HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    HttpContext.Current.Response.Write(msg);

                    return;
                }


                if (claims.Count() <= 0)
                {
                    HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    HttpContext.Current.Response.Write("token验证失败");

                    return;
                }
                //举个例子      
                //这个地方用jwtToken当作key 获取实体val   然后看看jwtToken根据redis是否一样
                if (!(claims.FirstOrDefault(c=>c.Type == ClaimTypes.Name).Value == "admin"))// && claims.FirstOrDefault(c => c.ValueType == ClaimTypes.Role).Value == "123"
                {
                    HttpContext.Current.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    HttpContext.Current.Response.Write("没有权限");
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                }

                #endregion
            }

        }
    }
}