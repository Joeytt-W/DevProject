using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Utility.Common
{
    /// <summary>
    /// Autofac依赖注入
    /// </summary>
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //程序集注入业务服务
            var IAppServices = Assembly.Load("Dev.Services");
            var AppServices = Assembly.Load("Dev.Services");
            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IAppServices, AppServices)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();
        }
    }
}
