using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Framework.MainAPI.AutoFacModules;
using Framework.Service.IRepository;
using Framework.Service.Repository;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Framework.MainAPI
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
            // 注册api容器需要使用HTTPConfiguration对象
            HttpConfiguration config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            #region 注册Filter
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterModule<FilterModule>();
            #endregion

            #region 注册服务
            builder.RegisterModule<ServiceModule>();
            #endregion

            #region 注册AutoMapper
            builder.RegisterModule<MapperModule>();
            #endregion

            Container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
