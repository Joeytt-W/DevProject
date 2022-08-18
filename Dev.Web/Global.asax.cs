using Dev.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Dev.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoFacConfig.RegisterServices();

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevWebLogUtil.RegisterConfig();
            DevWebLogUtil.Info("Application_Start");
        }
    }
}
