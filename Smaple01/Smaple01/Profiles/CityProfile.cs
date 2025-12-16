using AutoMapper;
using Smaple01.Entities;
using Smaple01.Models;

namespace Smaple01.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            //default mapping
            CreateMap<City, CityWithOutPointsOfInterestDto>();
            CreateMap<City, CityDto>();

            //CreateMap<City, CityDto>().ForMember(
            //    d => d.CityName,
            //    o => o.MapFrom(s => s.Name)
            //    );
        }
    }
}