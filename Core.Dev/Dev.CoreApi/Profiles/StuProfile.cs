using AutoMapper;
using Dev.Entity.DbEntities;
using Dev.Entity.ViewModel;

namespace Dev.CoreApi.Profiles
{
    public class StuProfile:Profile
    {
        public StuProfile()
        {
            CreateMap<DbStu,Student>().ForMember(sour => sour.Name, dest => dest.MapFrom(m => m.DbName));
        }
    }
}
