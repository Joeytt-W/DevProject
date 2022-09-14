using Microsoft.AspNetCore.Builder;

namespace Dev.CoreApi.MiddleWare
{
    public static class MiddlewareExtensions
    {/// <summary>
     /// 使用自定义的鉴权中间件
     /// </summary>
     /// <param name="app"></param>
     /// <returns></returns>
        public static IApplicationBuilder UseRefreshTokenExpire(this IApplicationBuilder app)
        {
            //这样在startup.configure方法中直接用app.UseRefreshTokenExpire();就可以了
            //如果不写这个扩展方法就把下面这句直接放在startup.configure方法中就可以了
            return app.UseMiddleware<RefreshTokenExpireMiddleWare>();
        }
    }
}
