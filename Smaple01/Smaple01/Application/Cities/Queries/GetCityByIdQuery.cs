using MediatR;
using Smaple01.Entities;

namespace Smaple01.Application.Cities.Queries
{
    public record GetCityByIdQuery(int CityId, bool incloudPointsOfInterest) : IRequest<City?>;

}
