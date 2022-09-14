using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dev.CoreApi.MiddleWare
{
    /// <summary>
    /// token令牌过期自动刷新
    /// </summary>
    public class RefreshTokenExpireMiddleWare
    {
        //下一个要执行的中间件
        private RequestDelegate _next;
        //构造函数注入的方式，在IOC容器中获取服务
        private IConfiguration _configuration;
        //如果需要其他服务，可以自己加
        public RefreshTokenExpireMiddleWare(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        private DateTime ConvertStringToDateTime(string timeStamp)
        {
            Int64 begtime = Convert.ToInt64(timeStamp) * 10000000;
            DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTime
            return dt;
        }


        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authorization", out StringValues values);
            string valStr = values.ToString();
            if (!string.IsNullOrWhiteSpace(valStr) && valStr.StartsWith("Bearer "))
            {
                valStr = valStr.Split(' ')[1];
                //认证模拟basic认证：cusAuth YWRtaW46YWRtaW4=
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                try
                {
                    IDictionary<string, object> claims = new Dictionary<string, object>();
                    JwtSecurityToken jwt = handler.ReadJwtToken(valStr);
                    if (jwt != null)
                    {
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                        TokenValidationParameters validationParameters = new TokenValidationParameters
                        {
                            RequireExpirationTime = true,
                            ClockSkew = TimeSpan.Zero,
                            IssuerSigningKey = key,//签名验证算法
                            ValidateIssuer = true,//是否验证Issuer
                            ValidateAudience = true,//是否验证Audience
                            ValidateLifetime = false,//是否验证失效时间
                            ValidAudience = "yourdomain.com",
                            ValidIssuer = "yourdomain.com",
                            ValidateIssuerSigningKey = true,//是否验证签名
                        };
                        var principal = handler.ValidateToken(valStr, validationParameters, out SecurityToken securityToken);
                        foreach (Claim item in principal.Claims)
                        {
                            claims.Add(item.Type, item.Value);
                        }
                        
                        if (claims.ContainsKey("exp") && ConvertStringToDateTime(claims["exp"].ToString()) <= DateTime.Now && ConvertStringToDateTime(claims["exp"].ToString()).AddHours(4) >= DateTime.Now)
                        {
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            int expires_in = int.Parse(_configuration["Expire_in"]);
                            var claimArr = new[]
                            {
                               new Claim(ClaimTypes.Name, claims[ClaimTypes.Name].ToString()),
                               new Claim("exp", DateTime.Now.AddMinutes(expires_in).ToString())
                            };
                            var token = new JwtSecurityToken(
                                issuer: "yourdomain.com",
                                audience: "yourdomain.com",
                                claims: claimArr,
                                expires: DateTime.Now.AddMinutes(expires_in),
                                signingCredentials: creds);

                            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                            context.Response.Headers.Add("X-Refresh-Token", accessToken);
                        }else if (ConvertStringToDateTime(claims["exp"].ToString()) < DateTime.Now)
                        {
                            context.Response.Headers.Remove("X-Refresh-Token");
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await context.Response.WriteAsJsonAsync(new
                            {
                                code = (int)HttpStatusCode.Unauthorized,
                                msg = "超时，请重新登录"
                            });
                            
                            return;
                        }
                        else
                        {
                            context.Response.Headers.Remove("X-Refresh-Token");
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        code = (int)HttpStatusCode.InternalServerError,
                        msg = ex.Message
                    });
                    return;
                }
                
            }

            await _next(context);
        }
    }
}
