using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Framework.MainWeb.AutoFacModules;
using Framework.MainWeb.Filters;
using Framework.MainWeb.Profiles;
using Framework.Service.IRepository;
using Framework.Service.Repository;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Framework.MainWeb
{
    public class AutoFacConfig
    {/// <summary>
     /// IOC组件容器。
     /// </summary>
        public static IContainer Container { get; set; }

        /// <summary>
        /// 获取组件。
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            try
            {
                if (Container != null)
                {
                    return Container.Resolve<T>();
                }
                return default(T);
            }
            catch (Exception ex)
            {
                throw new Exception("IOC实例化出错。" + ex.Message);
            }
        }

        /// <summary>
        /// 注册服务。
        /// </summary>
        public static void RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #region 注册Filter
            builder.RegisterModule<FilterModule>();
            #endregion

            #region 注册服务
            builder.RegisterModule<ServiceModule>();
            #endregion

            #region 注册AutoMapper
            builder.RegisterModule<MapperModule>();
            #endregion

            Container = builder.Build();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
