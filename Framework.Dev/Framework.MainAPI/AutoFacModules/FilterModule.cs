using Autofac;
using Autofac.Integration.WebApi;
using Framework.MainAPI.Filters;

namespace Framework.MainAPI.AutoFacModules
{
    public class FilterModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FrameworkExpcetionFilter>().SingleInstance();

        }
    }
}