using System.Web;
using System.Web.Mvc;
using Dev.Web.Filters;

namespace Dev.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionHandleAttribute());
        }
    }
}
