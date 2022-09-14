using Dev.Entity.ViewModel;
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

namespace Dev.CoreApi
{
    public class CustomerAuthenticationHandler : IAuthenticationHandler
    {
        /// <summary>
        /// 自定义认证Scheme名称
        /// </summary>
        public const string CustomerSchemeName = "cusAuth";

        private AuthenticationScheme _scheme;
        private HttpContext _context;
        private readonly IConfiguration _configuration;

        public CustomerAuthenticationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 认证逻辑：认证校验主要逻辑
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            AuthenticateResult result;
            _context.Request.Headers.TryGetValue("Authorization", out StringValues values);
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
                    if (jwt == null)
                    {
                        result = AuthenticateResult.Fail("未登陆");
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                    TokenValidationParameters validationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = key,//签名验证算法
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ValidAudience = "yourdomain.com",
                        ValidIssuer = "yourdomain.com",
                        ValidateIssuerSigningKey = true,//是否验证签名
                    };
                    var principal = handler.ValidateToken(valStr, validationParameters, out SecurityToken securityToken);
                    foreach (Claim item in principal.Claims)
                    {
                        claims.Add(item.Type, item.Value);
                    }

                    var ticket = GetAuthTicket(claims[ClaimTypes.Name].ToString(), "admin");

                    result = AuthenticateResult.Success(ticket);
                    return Task.FromResult(result);
                }
                catch (SecurityTokenInvalidLifetimeException ex)
                {
                    result = AuthenticateResult.Fail(ex.Message);
                    return Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    result = AuthenticateResult.Fail(ex.Message);
                    return Task.FromResult(result);
                }

            }
            else
            {
                result = AuthenticateResult.Fail("未登陆");
                return Task.FromResult(result);
            }
            
        }

        /// <summary>
        /// 未登录时的处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _context.Response.WriteAsJsonAsync(new
            {
                code = (int)HttpStatusCode.Unauthorized,
                msg = "已超时"
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 权限不足时处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            _context.Response.WriteAsJsonAsync(new
            {
                code = (int)HttpStatusCode.Forbidden,
                msg = "没有权限"
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 初始化认证
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            if (_context.Request.Query["Username"].Equals("admin") && _context.Request.Query["Username"].Equals("1"))
            {
                var claims = new[]
                {
                   new Claim(ClaimTypes.Name, _context.Request.Query["Username"])
                };
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                /**
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:
                    iss: The issuer of the token，token 是给谁的
                    sub: The subject of the token，token 主题
                    exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                    iat: Issued At。 token 创建时间， Unix 时间戳格式
                    jti: JWT ID。针对当前 token 的唯一标识
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                 * */
                int expires_in = int.Parse(_configuration["Expire_in"]);
                var token = new JwtSecurityToken(
                    issuer: "yourdomain.com",
                    audience: "yourdomain.com",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(expires_in),
                    signingCredentials: creds);

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                var response = new
                {

                    username = _context.Request.Query["Username"],
                    access_token = accessToken,
                    expires_in = 1800
                };
                _context.Response.StatusCode = (int)HttpStatusCode.OK;
                _context.Response.WriteAsJsonAsync(response);
            }
            return Task.CompletedTask;
        }

        private AuthenticationTicket GetAuthTicket(string name, string role)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
    new Claim(ClaimTypes.Name, name)
            }, CustomerSchemeName);
            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, _scheme.Name);
        }
    }
}
