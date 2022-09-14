using AutoMapper;
using Framework.MainEntity.DbEntity;
using Framework.MainEntity.Dto;

namespace Framework.MainAPI.Profiles
{
    public class CompanyProfile:Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyQueryDto>();
            CreateMap<CompanyAddDto, Company>();
            CreateMap<Company, CompanyUpdateDto>().ReverseMap();
        }
    }
}