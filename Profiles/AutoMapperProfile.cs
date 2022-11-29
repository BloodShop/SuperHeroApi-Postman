using AutoMapper;
using SuperHeroApi.DataAccess.Models;
using SuperHeroApi.DataAccess.Models.Dto;

namespace SuperHeroApi.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SuperHero, SuperHeroDto>();
            CreateMap<SuperHeroDto, SuperHero>();
        }
    }
}
