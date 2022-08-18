using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Dev.Web.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dev.Web
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

            //Assembly serviceAss = Assembly.Load("Dev.Services");
            //Type[] serviceTypes = serviceAss.GetTypes();
            //builder.RegisterTypes(serviceTypes).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            //Assembly repositoryAss = Assembly.Load("Dev.Services");
            //Type[] repositoryTypes = repositoryAss.GetTypes();
            //builder.RegisterTypes(repositoryTypes).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                //添加profile文件
                cfg.AddProfile<StuProfile>();
                //cfg.AddDataReaderMapping();
                // 使用 datetime 转换器
                //cfg.CreateMap<string, DateTime>().ConvertUsing<DateTimeTypeConverter>();
                // 也可以通过 lambda 来直接做简单的转换
                //cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                //cfg.CreateMap<IDataRecord, PubRoleObj>()
                //.ForMember(dest => dest.StopFlag, opt => opt.MapFrom(src => (bool)src.GetValue(4) ? "1" : "0"))
                //.ForMember(dest => dest.Crdt, opt => opt.MapFrom(src => src.GetValue(6).ToString() == "" || src.GetValue(6).ToString() == "NULL" ? DateTime.Now.ToString("yyyy-MM-dd") : DateTime.Parse(src.GetValue(6).ToString()).ToString("yyyy-MM-dd")))
                //.ForMember(dest => dest.Lmdt, opt => opt.MapFrom(src => src.GetValue(8)));

                //cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });

            Mapper mapper = new Mapper(configuration);
            builder.RegisterInstance(mapper).ExternallyOwned();

            Container = builder.Build();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
