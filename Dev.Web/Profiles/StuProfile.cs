using AutoMapper;
using Dev.Web.DbEntities;
using Dev.Web.ViewModel;

namespace Dev.Web.Profiles
{
    public class StuProfile:Profile
    {
        public StuProfile()
        {
            CreateMap<DbStu,Student>().ForMember(sour => sour.Name, dest => dest.MapFrom(m => m.DbName));
        }
    }
}
