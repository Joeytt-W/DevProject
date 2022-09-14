using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using AutoMapper;
using Framework.MainWeb.Profiles;

namespace Framework.MainWeb.AutoFacModules
{
    public class MapperModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<IConfigurationProvider>(m => new MapperConfiguration(cfg =>
            {
                //添加profile文件
                cfg.AddProfile<CompanyProfile>();


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
            })).SingleInstance();
            builder.Register<IMapper>(c => new Mapper(c.Resolve<IConfigurationProvider>(), c.Resolve)).InstancePerDependency();
        }
    }
}