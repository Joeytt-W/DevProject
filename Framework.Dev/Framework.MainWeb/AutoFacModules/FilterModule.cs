using Autofac;
using Autofac.Integration.Mvc;
using Framework.MainWeb.Filters;

namespace Framework.MainWeb.AutoFacModules
{
    public class FilterModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FrameworkExpcetionFilter>().SingleInstance();
            builder.RegisterFilterProvider();
        }
    }
}