﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Framework.MainWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Company", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Error",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Error", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}