using Autofac;
using Autofac.Integration.Mvc;
using Framework.MainWeb.Filters;
using System.Web.Mvc;

namespace Framework.MainWeb.App_Start
{
    public class FiltersConfig
    {
        public static void Register(GlobalFilterCollection filters)
        {
            filters.Add(AutofacDependencyResolver.Current.ApplicationContainer.
        Resolve<FrameworkExpcetionFilter>());
        }
    }
}