using AutoMapper;
using Smaple01.Entities;
using Smaple01.Models;
using System.Drawing;

namespace Smaple01.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDto>();
            CreateMap<PointOfInterestCreationDto, PointOfInterest>();
            CreateMap<PointsOfInterestUpdateDto, PointOfInterest>();
            CreateMap<PointOfInterest, PointsOfInterestUpdateDto>();
        }
    }
}
