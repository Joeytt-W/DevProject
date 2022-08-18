using Dev.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Dev.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            //全局过滤器
            config.Filters.Add(new CustomExcetionFilter());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
