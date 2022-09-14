using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Framework.MainWeb.App_Start
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));



            bundles.Add(new ScriptBundle("~/Content/main.js").Include(
                      "~/lib/bootstrap/js/bootstrap.js",
                      "~/lib/jquery/jquery.min.js"));

            bundles.Add(new StyleBundle("~/Content/main.css").Include(
                      "~/lib/bootstrap/css/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}