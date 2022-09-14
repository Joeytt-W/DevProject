using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Framework.Service.IRepository;
using Framework.Service.Repository;

namespace Framework.MainWeb.AutoFacModules
{
    public class ServiceModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly serviceAss = Assembly.Load(ConfigurationManager.AppSettings["service_assembly"]);
            Type[] serviceTypes = serviceAss.GetTypes();
            builder.RegisterTypes(serviceTypes).Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();//.InstancePerDependency();
        }
    }
}