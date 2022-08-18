﻿using Dev.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace Dev.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
