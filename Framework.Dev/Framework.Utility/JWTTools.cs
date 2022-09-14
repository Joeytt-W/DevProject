using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Framework.Utility
{
    public class JWTTools
    {
        /// <summary>
        /// 加密
        /// </summary>
        private static readonly string key = ConfigurationManager.AppSettings["tokenKey"];
        public static string Encode(Dictionary<string, string> claimDic, int expireMinutes = 30)
        {
            List<Claim> claims = new List<Claim>();

            foreach (var item in claimDic)
            {
                if (item.Key.Equals("Username"))
                    claims.Add(new Claim(ClaimTypes.Name,claimDic["Username"]));
                if(item.Key.Equals("Company"))
                    claims.Add(new Claim(ClaimTypes.Role,claimDic["CompanyId"]));
                else
                    claims.Add(new Claim(item.Key, item.Value));
            }
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                     issuer: "yourdomain.com",
                     audience: "yourdomain.com",
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(expireMinutes),
                     signingCredentials: creds);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> Decode(string accessToken, out string msg)
        {
            msg = "";
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

                if (token.Payload.Exp < DateTimeOffset.Now.ToUnixTimeSeconds())
                {
                    //DateTimeOffset.Now.ToUnixTimeSeconds()	1663141536	long

                    //token.Payload.Exp = 1663143166

                    msg = "token超时";
                    return null;
                }
                if (!token.SecurityKey.Equals(key))
                {
                    msg = "秘钥验证失败";
                    return null;
                }

                  

                IEnumerable<Claim> claims = token.Claims;

                return claims;
            }
            catch(Exception ex)
            {
                msg = ex.Message;

                return null;
            }
        }
    }
}
